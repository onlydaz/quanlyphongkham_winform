using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using NhaKhoa.BUS;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.Hoadon
{
    public partial class FRM_AddHoadon : Form
    {
            // Services
            private readonly HoaDonBUS _hoaDonBus;
            private readonly BenhNhanBUS _benhNhanBus = new BenhNhanBUS();
            private readonly NhanVienBUS _nhanVienBus = new NhanVienBUS();
            private string maHoaDon = "";
            private string maBenhNhanHienTai = "";
            private string maNhanVienHienTai = "";

            public FRM_AddHoadon()
            {
                InitializeComponent();
                _hoaDonBus = new HoaDonBUS();
                LoadPatients();
                SetupControls();
            }

            private void LoadPatients()
            {
                try
                {
                    var list = _benhNhanBus.LayDanhSach();
                    cmbMaBenhNhan.DataSource = list;
                    cmbMaBenhNhan.DisplayMember = "MaBN";
                    cmbMaBenhNhan.ValueMember = "MaBN";
                    cmbMaBenhNhan.SelectedIndex = -1;
                }
                catch
                {
                    // ignore - UI still usable
                }
            }

            private void SetupControls()
            {
                dtpNgayLap.Value = DateTime.Now;
                dtpNgayLap.Format = DateTimePickerFormat.Custom;
                dtpNgayLap.CustomFormat = "dd/MM/yyyy";

                // Populate employee combobox with available staff (for selecting who issues the invoice)
                // Staff selection is locked — MaNV is set from selected LamSan. Leave txtMaNV empty.

                txtMaHoaDon.ReadOnly = true;
                txtTenNhanVien.ReadOnly = true;
                txtChucVu.ReadOnly = true;
                txtTenBenhNhan.ReadOnly = true;
                txtNgaySinh.ReadOnly = true;
                txtThanhTien.ReadOnly = true;

                // Ensure dgvLamSan has the correct columns for Lâm sàng
                try
                {
                    dgvLamSan.Columns.Clear();
                    dgvLamSan.Columns.Add("ls_STT", "STT");
                    dgvLamSan.Columns.Add("ls_MaLS", "Mã LS");
                    dgvLamSan.Columns.Add("ls_NgayKham", "Ngày khám");
                    // merged time column
                    dgvLamSan.Columns.Add("ls_GioKham", "Giờ khám");
                    dgvLamSan.Columns.Add("ls_TrieuChung", "Triệu chứng");
                    // show diagnosis and treatment NAMES (not codes)
                    dgvLamSan.Columns.Add("ls_TenCD", "Chẩn đoán");
                    dgvLamSan.Columns.Add("ls_TenDT", "Điều trị");
                    // final column for line total
                    dgvLamSan.Columns.Add("ls_ThanhTien", "Thành tiền");
                }
                catch
                {
                    // ignore if designer doesn't have dgvLamSan at design-time
                }

                // old service grid removed — nothing to hide

                GenerateInvoiceCode();
            }
        private void GenerateInvoiceCode()
        {
            string datePart = dtpNgayLap.Value.ToString("yyyyMMdd");
            int count = 1; // Giả lập số thứ tự
            maHoaDon = $"HD{datePart}{count:D3}";
            txtMaHoaDon.Text = maHoaDon;
        }

        private void cmbMaBenhNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaBenhNhan.SelectedValue == null) return;

            string maBN = cmbMaBenhNhan.SelectedValue.ToString();
            maBenhNhanHienTai = maBN;

            try
            {
                var bn = _benhNhanBus.LayBenhNhanTheoMa(maBN);
                if (bn != null)
                {
                    txtTenBenhNhan.Text = bn.TenBN;
                    txtNgaySinh.Text = bn.NamSinh.ToString();
                }
            }
            catch
            {
            }

            LoadLamSanForPatient(maBN);
        }

        // Staff textbox is readonly; MaNV is set from selected LamSan row.

        private void dgvLamSan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var row = dgvLamSan.Rows[e.RowIndex];
            // If user edits the treatment code column (ls_MaDT index 6), try to resolve price
            try
            {
                if (e.ColumnIndex == 6)
                {
                    // user edited treatment name column: try to resolve by MaDT or TenDieuTri
                    string val = row.Cells[6].Value?.ToString();
                    decimal dongia = 0;
                    string foundName = "";
                    if (!string.IsNullOrEmpty(val))
                    {
                        using (var ctx = new NhaKhoa.DAL.NhaKhoaContext())
                        {
                            // try by MaDT first
                            var dt = ctx.DieuTris.AsNoTracking().SingleOrDefault(d => d.MaDT == val);
                            if (dt == null)
                                dt = ctx.DieuTris.AsNoTracking().SingleOrDefault(d => d.TenDieuTri == val);
                            if (dt != null)
                            {
                                foundName = dt.TenDieuTri ?? dt.MaDT;
                                if (dt.DonGia.HasValue) dongia = dt.DonGia.Value;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(foundName)) row.Cells[6].Value = foundName;
                    row.Cells[7].Value = dongia.ToString("N0");
                    CalculateTotal();
                }
                else if (e.ColumnIndex == 7)
                {
                    // line total edited directly -> recalc overall total
                    CalculateTotal();
                }
            }
            catch
            {
                // ignore
            }
        }
            private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvLamSan.Rows)
            {
                if (row.IsNewRow) continue;
                if (decimal.TryParse(row.Cells[7].Value?.ToString(), NumberStyles.AllowThousands | NumberStyles.Number, CultureInfo.CurrentCulture, out decimal tien))
                    total += tien;
                else
                {
                    // fallback: try invariant parsing after removing non-digit
                    var cleaned = new string((row.Cells[7].Value?.ToString() ?? "").Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
                    if (decimal.TryParse(cleaned, NumberStyles.AllowThousands | NumberStyles.Number, CultureInfo.InvariantCulture, out tien))
                        total += tien;
                }
            }
            txtThanhTien.Text = total.ToString("N0");
        }

        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvLamSan.Rows.Add();
            var row = dgvLamSan.Rows[rowIndex];
            row.Cells[0].Value = (rowIndex + 1).ToString();
            row.Cells[1].Value = ""; // MaLS
            row.Cells[2].Value = dtpNgayLap.Value.ToString("yyyy-MM-dd");
            row.Cells[3].Value = ""; // GioKham
            row.Cells[4].Value = ""; // TrieuChung
            row.Cells[5].Value = ""; // MaCD
            row.Cells[6].Value = ""; // MaDT
            row.Cells[7].Value = "0"; // ThanhTien
            CalculateTotal();
        }

        // Load LAMSAN rows for a patient and display in dgvLamSan
        private void LoadLamSanForPatient(string maBN)
        {
            try
            {
                using (var ctx = new NhaKhoa.DAL.NhaKhoaContext())
                {
                    var lamSans = ctx.LamSans.Where(x => x.MaBN == maBN).OrderByDescending(x => x.NgayKham).ToList();

                    dgvLamSan.Rows.Clear();

                    // Load chuc vu mapping once
                    var chucVuList = _nhanVienBus.LayDanhSachChucVu();
                    var chucVuMap = chucVuList.ToDictionary(c => c.MaCV, c => c.TenCV);

                    int idx = 1;
                    foreach (var ls in lamSans)
                    {
                        // build merged time string
                        string gioKham = "";
                        try
                        {
                            if (ls.GioBatDau != null && ls.GioKetThuc != null)
                                gioKham = $"{ls.GioBatDau:hh\\:mm} - {ls.GioKetThuc:hh\\:mm}";
                            else if (ls.GioBatDau != null)
                                gioKham = ls.GioBatDau?.ToString();
                            else if (ls.GioKetThuc != null)
                                gioKham = ls.GioKetThuc?.ToString();
                        }
                        catch
                        {
                            gioKham = (ls.GioBatDau?.ToString() ?? "") + (ls.GioKetThuc != null ? (" - " + ls.GioKetThuc?.ToString()) : "");
                        }

                        string maCD = ls.MaCD ?? "";
                        string maDT = ls.MaDT ?? "";

                        // lookup human-friendly names
                        string tenCD = "";
                        string tenDT = "";
                        decimal dongia = 0;
                        try
                        {
                            if (!string.IsNullOrEmpty(maCD))
                            {
                                var cd = ctx.ChanDoans.AsNoTracking().SingleOrDefault(c => c.MaCD == maCD);
                                if (cd != null) tenCD = cd.TenChuanDoan ?? cd.MaCD;
                            }
                            if (!string.IsNullOrEmpty(maDT))
                            {
                                var dt = ctx.DieuTris.AsNoTracking().SingleOrDefault(d => d.MaDT == maDT);
                                if (dt != null)
                                {
                                    tenDT = dt.TenDieuTri ?? dt.MaDT;
                                    if (dt.DonGia.HasValue) dongia = dt.DonGia.Value;
                                }
                            }
                        }
                        catch
                        {
                        }

                        decimal thanhTien = dongia; // single-item per lamSan

                        dgvLamSan.Rows.Add(idx++, ls.MaLS, ls.NgayKham?.ToString("yyyy-MM-dd"), gioKham, ls.TrieuChung, tenCD, tenDT, thanhTien.ToString("N0"));
                    }
                    // If any lamSan exists, prefill top staff info with first row (MaNV -> TenNV + ChucVu)
                    if (lamSans.Any())
                    {
                        var first = lamSans.First();
                        if (!string.IsNullOrEmpty(first.MaNV))
                        {
                            try
                            {
                                txtMaNV.Text = first.MaNV;
                                maNhanVienHienTai = first.MaNV;
                                var nv = ctx.NhanViens.AsNoTracking().SingleOrDefault(n => n.MaNV == first.MaNV);
                                if (nv != null)
                                {
                                    txtTenNhanVien.Text = nv.TenNV;
                                    if (!string.IsNullOrEmpty(nv.MaCV) && chucVuMap != null && chucVuMap.ContainsKey(nv.MaCV))
                                        txtChucVu.Text = chucVuMap[nv.MaCV];
                                    else
                                        txtChucVu.Text = nv.MaCV ?? "";
                                }
                            }
                            catch
                            {
                                // ignore
                            }
                        }
                    }

                    // Update total after rows populated
                    CalculateTotal();
                }
            }
            catch
            {
                // ignore errors silently for UI
            }
        }

        private void dgvLamSan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No cell-based delete action in this view; keep handler to avoid designer errors.
        }
        private void ReindexSTT()
        {
            for (int i = 0; i < dgvLamSan.Rows.Count; i++)
            {
                dgvLamSan.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(maBenhNhanHienTai))
                {
                    MessageBox.Show("Vui lòng chọn bệnh nhân!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dgvLamSan.Rows.Count == 0 || dgvLamSan.Rows[0].IsNewRow)
                {
                    MessageBox.Show("Vui lòng thêm ít nhất 1 dịch vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Parse tổng tiền (có thể có dấu phân cách hàng nghìn)
                if (!decimal.TryParse(txtThanhTien.Text, NumberStyles.AllowThousands | NumberStyles.Number, CultureInfo.CurrentCulture, out decimal total))
                {
                    // Thử loại bỏ ký tự không phải số và parse bằng invariant
                    var cleaned = new string(txtThanhTien.Text.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
                    if (!decimal.TryParse(cleaned, NumberStyles.AllowThousands | NumberStyles.Number, CultureInfo.InvariantCulture, out total))
                    {
                        MessageBox.Show("Tổng tiền không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var hd = new HoaDon
                {
                    MaHD = null, // để BUS/DAL sinh MaHD
                    NgayLapHD = dtpNgayLap.Value,
                    TongTien = total,
                    MaBN = maBenhNhanHienTai,
                    MaNV = maNhanVienHienTai
                };

                _hoaDonBus.ThemHoaDon(hd);

                // Hiển thị mã hoá đơn thực tế được sinh
                txtMaHoaDon.Text = hd.MaHD;

                MessageBox.Show($"Hoá đơn {hd.MaHD} đã được lưu thành công!\nTổng tiền: {txtThanhTien.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset một số control
                dgvLamSan.Rows.Clear();
                CalculateTotal();
                GenerateInvoiceCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hoá đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Chưa có hoá đơn để in!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị thông tin hoá đơn để in (giả lập)
            string invoiceInfo = $"HOÁ ĐƠN\n" +
                                 $"Mã hoá đơn: {maHoaDon}\n" +
                                 $"Ngày lập: {dtpNgayLap.Value:dd/MM/yyyy}\n" +
                                 $"Bệnh nhân: {txtTenBenhNhan.Text} ({txtNgaySinh.Text})\n" +
                                 $"Bác sĩ: {txtTenNhanVien.Text} - {txtChucVu.Text}\n" +
                                 $"----------------------------------------\n";

            foreach (DataGridViewRow row in dgvLamSan.Rows)
            {
                if (row.IsNewRow) continue;
                var maLS = row.Cells["ls_MaLS"].Value?.ToString() ?? "";
                var tenCD = row.Cells["ls_TenCD"].Value?.ToString() ?? "";
                var tenDT = row.Cells["ls_TenDT"].Value?.ToString() ?? "";
                var thanh = row.Cells["ls_ThanhTien"].Value?.ToString() ?? "0";
                invoiceInfo += $"{maLS} | {tenCD} | {tenDT} = {thanh}\n";
            }

            invoiceInfo += $"----------------------------------------\n" +
                           $"Tổng tiền: {txtThanhTien.Text}";

            // Mở hộp thoại in (giả lập)
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Đã gửi lệnh in thành công!", "In hoá đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
