using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NhaKhoa.BUS;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace NhaKhoa.Hoadon
{
    public partial class FRM_Hoadon : Form
    {
        private readonly HoaDonBUS _hoaDonBus;
        private readonly NhanVienBUS _nhanVienBus;

        public FRM_Hoadon()
        {
            InitializeComponent();
            this.BackColor = Color.LightBlue;
            _hoaDonBus = new HoaDonBUS();
            _nhanVienBus = new NhanVienBUS();
            // wire up export button (menu) click
            this.btnXuatExcel.Click += btnXuatExcel_Click;
        }

        private void FRM_Hoadon_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            SetupDataGridView();
            LoadHoaDon();
        }
        private void SetupDataGridView()
        {
            dgvHoaDon.AutoGenerateColumns = false;
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoaDon.MultiSelect = false;

            dgvHoaDon.Columns.Clear();

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayLapHD",
                HeaderText = "Thời gian tạo",
                DataPropertyName = "NgayLapHD",
                Width = 160
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaHD",
                HeaderText = "Mã HĐ",
                DataPropertyName = "MaHD",
                Width = 120
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenNV",
                HeaderText = "Tên NV",
                DataPropertyName = "TenNV",
                Width = 150
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenBN",
                HeaderText = "Tên BN",
                DataPropertyName = "TenBN",
                Width = 150
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TongTien",
                HeaderText = "Tổng tiền",
                DataPropertyName = "TongTien",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "#,##0 VNĐ",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
        }
        private void LoadNhanVien()
        {
            try
            {
                var list = _nhanVienBus.LayDanhSach();
                // Only include staff with role 'Bacsi' (case- and diacritics-insensitive)
                var chucVuList = _nhanVienBus.LayDanhSachChucVu();
                string Normalize(string s)
                {
                    if (string.IsNullOrWhiteSpace(s)) return string.Empty;
                    var normalized = s.Normalize(System.Text.NormalizationForm.FormD);
                    var sb = new System.Text.StringBuilder();
                    foreach (var ch in normalized)
                    {
                        var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch);
                        if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                            sb.Append(ch);
                    }
                    return sb.ToString().Normalize(System.Text.NormalizationForm.FormC).ToLowerInvariant();
                }

                // Find roles whose role name matches 'bacsi' (normalize to ignore diacritics/case)
                var matchingRoles = chucVuList
                    .Where(cv => Normalize(cv.TenCV).Contains("bacsi"))
                    .ToList();

                var roleIds = matchingRoles.Select(cv => cv.MaCV).Where(id => !string.IsNullOrWhiteSpace(id)).ToList();
                var roleNamesNormalized = matchingRoles.Select(cv => Normalize(cv.TenCV)).Where(n => !string.IsNullOrWhiteSpace(n)).ToList();

                // Include NV where MaCV equals a role id OR MaCV (as stored) equals the role name
                var filtered = list.Where(nv =>
                    !string.IsNullOrWhiteSpace(nv.MaCV) && (
                        roleIds.Contains(nv.MaCV) ||
                        roleNamesNormalized.Contains(Normalize(nv.MaCV))
                    )
                ).ToList();

                list = filtered;
                var dt = new DataTable();
                dt.Columns.Add("MaNV", typeof(string));
                dt.Columns.Add("TenNV", typeof(string));

                foreach (var nv in list)
                {
                    dt.Rows.Add(nv.MaNV, nv.TenNV);
                }

                cboNhanVien.DataSource = dt;
                cboNhanVien.DisplayMember = "TenNV";
                cboNhanVien.ValueMember = "MaNV";
                cboNhanVien.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadHoaDon()
        {
            try
            {
                string maHD = "";
                string maNV = cboNhanVien.SelectedIndex == -1 ? "" : cboNhanVien.SelectedValue?.ToString();
                string tenNV = "";
                // If no selected value but user typed a name into the combo, use it to filter by name
                if (cboNhanVien.SelectedIndex == -1 && !string.IsNullOrWhiteSpace(cboNhanVien.Text))
                    tenNV = cboNhanVien.Text.Trim();

                string tenBN = txtTenBenhNhan.Text.Trim();
                DateTime? tuNgay = dtpTuNgay.Value.Date;
                // Tính toán denNgay là cuối ngày (23:59:59.9999999) trước khi truyền vào BUS
                DateTime? denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);

                var list = _hoaDonBus.TimKiem(maHD, maNV, tenBN, tenNV, tuNgay, denNgay);

                var dt = new DataTable();
                dt.Columns.Add("MaHD", typeof(string));
                dt.Columns.Add("NgayLapHD", typeof(DateTime));
                dt.Columns.Add("TongTien", typeof(decimal));
                dt.Columns.Add("TenNV", typeof(string));
                dt.Columns.Add("TenBN", typeof(string));

                foreach (var hd in list)
                {
                    dt.Rows.Add(
                        hd.MaHD,
                        hd.NgayLapHD,
                        hd.TongTien,
                        hd.NhanVien?.TenNV ?? "",
                        hd.BenhNhan?.TenBN ?? ""
                    );
                }

                dgvHoaDon.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenBenhNhan.Clear();
            cboNhanVien.SelectedIndex = -1;
            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today;

            LoadHoaDon();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadHoaDon();
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count == 0) return;
            string maHD = dgvHoaDon.SelectedRows[0].Cells["MaHD"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(maHD)) return;
            var frm = new FRM_ChiTietHD(maHD);
            frm.ShowDialog(this);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvHoaDon.SelectedRows.Count == 0) return;

                string maHD = dgvHoaDon.SelectedRows[0].Cells["MaHD"].Value.ToString();

                if (MessageBox.Show("Huỷ hoá đơn này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _hoaDonBus.XoaHoaDon(maHD);
                    LoadHoaDon();
                    MessageBox.Show("Xóa hóa đơn thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = $"HoaDon_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    // EPPlus 8 license API: set non-commercial use (replace name as appropriate)
                    OfficeOpenXml.ExcelPackage.License.SetNonCommercialPersonal("NhaKhoa - NonCommercial");
                    using (var package = new ExcelPackage())
                    {
                        var ws = package.Workbook.Worksheets.Add("Hoá đơn");

                        // headers
                        var headers = new[] { "Thời gian tạo", "Mã HĐ", "Tên NV", "Tên BN", "Tổng tiền" };
                        for (int c = 0; c < headers.Length; c++)
                        {
                            ws.Cells[1, c + 1].Value = headers[c];
                            ws.Cells[1, c + 1].Style.Font.Bold = true;
                            ws.Cells[1, c + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        int row = 2;
                        foreach (DataGridViewRow dgRow in dgvHoaDon.Rows)
                        {
                            if (dgRow.IsNewRow) continue;
                            var ngay = dgRow.Cells["NgayLapHD"].Value;
                            var mahd = dgRow.Cells["MaHD"].Value?.ToString() ?? "";
                            var tennv = dgRow.Cells["TenNV"].Value?.ToString() ?? "";
                            var tenbn = dgRow.Cells["TenBN"].Value?.ToString() ?? "";
                            var tong = dgRow.Cells["TongTien"].Value;

                            // Thời gian
                            if (ngay is DateTime dt)
                            {
                                ws.Cells[row, 1].Value = dt;
                                ws.Cells[row, 1].Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                            }
                            else
                            {
                                ws.Cells[row, 1].Value = ngay?.ToString() ?? "";
                            }

                            ws.Cells[row, 2].Value = mahd;
                            ws.Cells[row, 3].Value = tennv;
                            ws.Cells[row, 4].Value = tenbn;

                            if (tong != null && decimal.TryParse(tong.ToString(), out var dec))
                            {
                                ws.Cells[row, 5].Value = dec;
                                ws.Cells[row, 5].Style.Numberformat.Format = "#,##0";
                                ws.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            else
                            {
                                ws.Cells[row, 5].Value = tong?.ToString() ?? "";
                            }

                            row++;
                        }

                        ws.Cells[ws.Dimension.Address].AutoFitColumns();

                        // save
                        using (var stream = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write))
                        {
                            package.SaveAs(stream);
                        }
                    }

                    MessageBox.Show("Xuất Excel thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
