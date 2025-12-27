using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NhaKhoa.BUS;
using NhaKhoa.Models;
using OfficeOpenXml;

namespace NhaKhoa.NhaSi
{
    public partial class FRM_Nhasi : Form
    {
        private readonly BenhNhanBUS _benhNhanBus;
        private string _currentMaBN = ""; // Lưu mã bệnh nhân đang được chọn

        public FRM_Nhasi()
        {
            InitializeComponent();
            _benhNhanBus = new BenhNhanBUS();
            
            // Gán event handler cho các menu items (nếu chưa được gán trong Designer)
            if (btnKham != null)
                btnKham.Click += btnKham_Click;
            if (btnKTKham != null)
                btnKTKham.Click += btnKTKham_Click;
            if (btn_refresh != null)
                btn_refresh.Click += btn_refresh_Click;
            if (btnXuatExcel != null)
                btnXuatExcel.Click += btnXuatExcel_Click;
        }
        private void FRM_Nhasi_Load(object sender, EventArgs e)
        {
            LoadDanhSachBenhNhan();
            dgvBenhNhan.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dgvBenhNhan.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);

            dgvBenhNhan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBenhNhan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBenhNhan.MultiSelect = false;

            if (dgvBenhNhan.Columns["NgayKham"] != null)
            {
                dgvBenhNhan.Columns["NgayKham"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            SetColumnHeaders();
            
            // Gán event handler cho SelectionChanged
            dgvBenhNhan.SelectionChanged += DgvBenhNhan_SelectionChanged;
        }

        private void DgvBenhNhan_SelectionChanged(object sender, EventArgs e)
        {
            // Cập nhật _currentMaBN khi chọn bệnh nhân
            _currentMaBN = GetSelectedMaBN();
        }
        private void SetColumnHeaders()
        {
            if (dgvBenhNhan.Columns["MaBN"] != null) dgvBenhNhan.Columns["MaBN"].HeaderText = "Mã BN";
            if (dgvBenhNhan.Columns["TenBN"] != null) dgvBenhNhan.Columns["TenBN"].HeaderText = "Tên bệnh nhân";
            if (dgvBenhNhan.Columns["GioiTinh"] != null) dgvBenhNhan.Columns["GioiTinh"].HeaderText = "Giới tính";
            if (dgvBenhNhan.Columns["NamSinh"] != null) dgvBenhNhan.Columns["NamSinh"].HeaderText = "Năm sinh";
            if (dgvBenhNhan.Columns["SDT"] != null) dgvBenhNhan.Columns["SDT"].HeaderText = "SĐT";
            if (dgvBenhNhan.Columns["DiaChi"] != null) dgvBenhNhan.Columns["DiaChi"].HeaderText = "Địa chỉ";
            if (dgvBenhNhan.Columns["NgayKham"] != null) dgvBenhNhan.Columns["NgayKham"].HeaderText = "Ngày khám";
            if (dgvBenhNhan.Columns["LyDoKham"] != null) dgvBenhNhan.Columns["LyDoKham"].HeaderText = "Lý do khám";
        }

        private void LoadDanhSachBenhNhan(string ma = "", string ten = "", string sdt = "")
        {
            try
            {
                // Chỉ lấy danh sách bệnh nhân có TrangThai = "Chờ khám"
                var list = _benhNhanBus.LayDanhSachChoKham("Chờ khám");

                // Filter theo điều kiện tìm kiếm
                if (!string.IsNullOrEmpty(ma))
                    list = list.Where(x => x.MaBN != null && x.MaBN.Contains(ma)).ToList();
                if (!string.IsNullOrEmpty(ten))
                    list = list.Where(x => x.TenBN != null && x.TenBN.Contains(ten)).ToList();
                if (!string.IsNullOrEmpty(sdt))
                    list = list.Where(x => x.SDT != null && x.SDT.Contains(sdt)).ToList();

                var dt = new DataTable();
                dt.Columns.Add("MaBN", typeof(string));
                dt.Columns.Add("TenBN", typeof(string));
                dt.Columns.Add("GioiTinh", typeof(string));
                dt.Columns.Add("NamSinh", typeof(int));
                dt.Columns.Add("SDT", typeof(string));
                dt.Columns.Add("DiaChi", typeof(string));
                dt.Columns.Add("NgayKham", typeof(DateTime));
                dt.Columns.Add("LyDoKham", typeof(string));

                foreach (var bn in list.OrderBy(x => x.NgayKham))
                {
                    dt.Rows.Add(
                        bn.MaBN ?? "",
                        bn.TenBN ?? "",
                        bn.GioiTinh ?? "",
                        bn.NamSinh,
                        bn.SDT ?? "",
                        bn.DiaChi ?? "",
                        bn.NgayKham,
                        bn.LyDoKham ?? ""
                    );
                }

                dgvBenhNhan.AutoGenerateColumns = true;
                dgvBenhNhan.DataSource = dt;
                SetColumnHeaders();
                
                // Reset selection
                _currentMaBN = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSelectedMaBN()
        {
            if (dgvBenhNhan.SelectedRows.Count == 0)
                return "";

            // Lấy MaBN từ DataTable để đảm bảo đúng
            if (dgvBenhNhan.DataSource is DataTable dt)
            {
                int rowIndex = dgvBenhNhan.SelectedRows[0].Index;
                if (rowIndex < dt.Rows.Count)
                {
                    return dt.Rows[rowIndex]["MaBN"]?.ToString() ?? "";
                }
            }
            else
            {
                return dgvBenhNhan.SelectedRows[0].Cells["MaBN"].Value?.ToString() ?? "";
            }
            return "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string ma = txtTimKiemMa.Text.Trim();
            string ten = txtTimKiemTen.Text.Trim();
            string sdt = txtTimKiemSDT.Text.Trim();

            LoadDanhSachBenhNhan(ma, ten, sdt);
        }

        private void btnKham_Click(object sender, EventArgs e)
        {
            try
            {
                string maBN = GetSelectedMaBN();
                if (string.IsNullOrEmpty(maBN))
                {
                    MessageBox.Show("Vui lòng chọn một bệnh nhân để bắt đầu khám!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra trạng thái hiện tại
                var benhNhan = _benhNhanBus.LayBenhNhanTheoMa(maBN);
                if (benhNhan == null)
                {
                    MessageBox.Show("Không tìm thấy bệnh nhân!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (benhNhan.TrangThai == "Đang khám")
                {
                    MessageBox.Show("Bệnh nhân này đang được khám bởi bác sĩ khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (benhNhan.TrangThai != "Chờ khám")
                {
                    MessageBox.Show($"Bệnh nhân này có trạng thái '{benhNhan.TrangThai}', không thể bắt đầu khám!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật trạng thái từ "Chờ khám" sang "Đang khám"
                _benhNhanBus.CapNhatTrangThai(maBN, "Đang khám");
                _currentMaBN = maBN; // Lưu mã bệnh nhân đang khám
                
                MessageBox.Show("Đã bắt đầu khám bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Reload danh sách để ẩn bệnh nhân đang khám
                LoadDanhSachBenhNhan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChiTietLamSan_Click(object sender, EventArgs e)
        {
            try
            {
                string maBN = GetSelectedMaBN();
                if (string.IsNullOrEmpty(maBN))
                {
                    // Nếu không chọn, dùng bệnh nhân đang khám
                    maBN = _currentMaBN;
                }

                if (string.IsNullOrEmpty(maBN))
                {
                    MessageBox.Show("Vui lòng chọn một bệnh nhân hoặc bắt đầu khám trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra trạng thái - chỉ cho phép mở khi đang khám
                var benhNhan = _benhNhanBus.LayBenhNhanTheoMa(maBN);
                if (benhNhan == null)
                {
                    MessageBox.Show("Không tìm thấy bệnh nhân!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (benhNhan.TrangThai != "Đang khám")
                {
                    MessageBox.Show($"Chỉ có thể mở chi tiết lâm sàn khi bệnh nhân đang ở trạng thái 'Đang khám'!\nTrạng thái hiện tại: '{benhNhan.TrangThai}'", 
                                  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FRM_Lamsan frm = new FRM_Lamsan(maBN);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKTKham_Click(object sender, EventArgs e)
        {
            try
            {
                string maBN = _currentMaBN;
                if (string.IsNullOrEmpty(maBN))
                {
                    // Nếu không có bệnh nhân đang khám, cho phép chọn từ danh sách
                    maBN = GetSelectedMaBN();
                }

                if (string.IsNullOrEmpty(maBN))
                {
                    MessageBox.Show("Vui lòng chọn một bệnh nhân để kết thúc khám!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra trạng thái
                var benhNhan = _benhNhanBus.LayBenhNhanTheoMa(maBN);
                if (benhNhan == null)
                {
                    MessageBox.Show("Không tìm thấy bệnh nhân!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (benhNhan.TrangThai != "Đang khám")
                {
                    MessageBox.Show($"Bệnh nhân này không ở trạng thái 'Đang khám'!\nTrạng thái hiện tại: '{benhNhan.TrangThai}'", 
                                  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận
                if (MessageBox.Show($"Xác nhận kết thúc khám cho bệnh nhân {maBN}?", "Xác nhận", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                // Cập nhật trạng thái từ "Đang khám" sang "Đã khám"
                _benhNhanBus.CapNhatTrangThai(maBN, "Đã khám");
                _currentMaBN = ""; // Reset
                
                MessageBox.Show("Đã kết thúc khám bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Reload danh sách - bệnh nhân đã khám sẽ không còn hiển thị
                LoadDanhSachBenhNhan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            // Làm mới danh sách - reload từ database
            LoadDanhSachBenhNhan();
            MessageBox.Show("Đã làm mới danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBenhNhan.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Danh sách chờ khám");

                    // Ghi header
                    for (int i = 0; i < dgvBenhNhan.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = dgvBenhNhan.Columns[i].HeaderText;
                        worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                        worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                    // Ghi dữ liệu
                    for (int i = 0; i < dgvBenhNhan.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvBenhNhan.Columns.Count; j++)
                        {
                            var value = dgvBenhNhan.Rows[i].Cells[j].Value;
                            if (value != null)
                            {
                                // Xử lý DateTime
                                if (value is DateTime)
                                {
                                    worksheet.Cells[i + 2, j + 1].Value = ((DateTime)value).ToString("dd/MM/yyyy HH:mm");
                                }
                                else
                                {
                                    worksheet.Cells[i + 2, j + 1].Value = value.ToString();
                                }
                            }
                        }
                    }

                    // Auto fit columns
                    worksheet.Cells.AutoFitColumns();

                    // Lưu file
                    SaveFileDialog saveDialog = new SaveFileDialog
                    {
                        Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                        Title = "Lưu file Excel",
                        FileName = $"DanhSachChoKham_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                    };

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var stream = File.Create(saveDialog.FileName))
                        {
                            package.SaveAs(stream);
                        }
                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuChiTietLamSan_Click(object sender, EventArgs e)
        {

        }
    }
}
