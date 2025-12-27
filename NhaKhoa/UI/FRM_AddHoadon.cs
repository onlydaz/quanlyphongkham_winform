using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoa.Hoadon
{
    public partial class FRM_AddHoadon : Form
    {
        // Dữ liệu giả lập
        private List<BacSi> danhSachBacSi = new List<BacSi>();
        private List<BenhNhan> danhSachBenhNhan = new List<BenhNhan>();
        private List<DichVu> danhSachDichVu = new List<DichVu>();

        // Biến lưu trữ hoá đơn đang tạo
        private string maHoaDon = "";
        private string maBenhNhanHienTai = "";
        private string maNhanVienHienTai = "";

        public FRM_AddHoadon()
        {
            InitializeComponent();
            LoadData(); // Tải dữ liệu mẫu
            SetupControls(); // Thiết lập ban đầu
        }
        // Class Bác sĩ
        public class BacSi
        {
            public string Ma { get; set; }
            public string Ten { get; set; }
            public string ChucVu { get; set; }
        }

        // Class Bệnh nhân
        public class BenhNhan
        {
            public string Ma { get; set; }
            public string Ten { get; set; }
            public string NgaySinh { get; set; }
        }

        // Class Dịch vụ
        public class DichVu
        {
            public string Ma { get; set; }
            public string Ten { get; set; }
            public decimal DonGia { get; set; }
        }
        private void LoadData()
        {
            // Giả lập bác sĩ đăng nhập (bạn nên lấy từ session thật)
            danhSachBacSi.Add(new BacSi { Ma = "BS001", Ten = "Dr. Trần Thị B", ChucVu = "Bác sĩ Nội khoa" });

            // Giả lập bệnh nhân
            danhSachBenhNhan.Add(new BenhNhan { Ma = "BN001", Ten = "Nguyễn Văn A", NgaySinh = "01/01/1980" });
            danhSachBenhNhan.Add(new BenhNhan { Ma = "BN002", Ten = "Trần Thị B", NgaySinh = "15/03/1990" });

            // Giả lập dịch vụ
            danhSachDichVu.Add(new DichVu { Ma = "DV001", Ten = "Khám nội tổng quát", DonGia = 150000 });
            danhSachDichVu.Add(new DichVu { Ma = "DV002", Ten = "Xét nghiệm máu", DonGia = 300000 });
            danhSachDichVu.Add(new DichVu { Ma = "DV003", Ten = "Thuốc kháng sinh", DonGia = 20000 });
        }
        private void SetupControls()
        {
            // Thiết lập DateTimePicker
            dtpNgayLap.Value = DateTime.Now;

            // Thiết lập combobox bác sĩ (chỉ có 1 người -> không cho chọn)
            cmbMaNhanVien.DataSource = danhSachBacSi;
            cmbMaNhanVien.DisplayMember = "Ma";
            cmbMaNhanVien.ValueMember = "Ma";
            cmbMaNhanVien.SelectedIndex = 0; // Chọn bác sĩ đầu tiên
            cmbMaNhanVien.Enabled = false; // Không cho chọn

            // Thiết lập combobox bệnh nhân
            cmbMaBenhNhan.DataSource = danhSachBenhNhan;
            cmbMaBenhNhan.DisplayMember = "Ma";
            cmbMaBenhNhan.ValueMember = "Ma";
            cmbMaBenhNhan.SelectedIndex = -1; // Chưa chọn

            // Thiết lập DataGridView
            dgvDichVu.AutoGenerateColumns = false;
            dgvDichVu.Columns.Clear();

            var colSTT = new DataGridViewTextBoxColumn { Name = "colSTT", HeaderText = "STT", Width = 40 };
            var colDichVu = new DataGridViewComboBoxColumn { Name = "colDichVu", HeaderText = "Dịch vụ", Width = 150 };
            var colSoLuong = new DataGridViewTextBoxColumn { Name = "colSoLuong", HeaderText = "Số lượng", Width = 80 };
            var colDonGia = new DataGridViewTextBoxColumn { Name = "colDonGia", HeaderText = "Đơn giá", Width = 100 };
            var colThanhTien = new DataGridViewTextBoxColumn { Name = "colThanhTien", HeaderText = "Thành tiền", Width = 100 };
            var colGhiChu = new DataGridViewTextBoxColumn { Name = "colGhiChu", HeaderText = "Ghi chú", Width = 120 };
            var colXoa = new DataGridViewButtonColumn { Name = "colXoa", HeaderText = "Xóa", Width = 60 };

            dgvDichVu.Columns.Add(colSTT);
            dgvDichVu.Columns.Add(colDichVu);
            dgvDichVu.Columns.Add(colSoLuong);
            dgvDichVu.Columns.Add(colDonGia);
            dgvDichVu.Columns.Add(colThanhTien);
            dgvDichVu.Columns.Add(colGhiChu);
            dgvDichVu.Columns.Add(colXoa);

            // Gán dữ liệu cho cột Dịch vụ
            ((DataGridViewComboBoxColumn)dgvDichVu.Columns["colDichVu"]).DataSource = danhSachDichVu;
            ((DataGridViewComboBoxColumn)dgvDichVu.Columns["colDichVu"]).DisplayMember = "Ten";
            ((DataGridViewComboBoxColumn)dgvDichVu.Columns["colDichVu"]).ValueMember = "Ma";

            // Thiết lập readonly cho một số trường
            txtMaHoaDon.ReadOnly = true;
            txtTenNhanVien.ReadOnly = true;
            txtChucVu.ReadOnly = true;
            txtTenBenhNhan.ReadOnly = true;
            txtNgaySinh.ReadOnly = true;
            txtThanhTien.ReadOnly = true;

            // Tự động sinh mã hoá đơn
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
            var bn = danhSachBenhNhan.FirstOrDefault(x => x.Ma == maBN);

            if (bn != null)
            {
                maBenhNhanHienTai = maBN;
                txtTenBenhNhan.Text = bn.Ten;
                txtNgaySinh.Text = bn.NgaySinh;
            }
        }

        private void cmbMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaNhanVien.SelectedValue == null) return;

            string maNV = cmbMaNhanVien.SelectedValue.ToString();
            var nv = danhSachBacSi.FirstOrDefault(x => x.Ma == maNV);

            if (nv != null)
            {
                maNhanVienHienTai = maNV;
                txtTenNhanVien.Text = nv.Ten;
                txtChucVu.Text = nv.ChucVu;
            }
        }

        private void dgvDichVu_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var row = dgvDichVu.Rows[e.RowIndex];

            if (e.ColumnIndex == dgvDichVu.Columns["colDichVu"].Index ||
                e.ColumnIndex == dgvDichVu.Columns["colSoLuong"].Index)
            {
                // Lấy mã dịch vụ
                string maDV = row.Cells["colDichVu"].Value?.ToString();
                if (string.IsNullOrEmpty(maDV)) return;

                var dv = danhSachDichVu.FirstOrDefault(x => x.Ma == maDV);
                if (dv == null) return;

                // Lấy số lượng
                int soLuong = 1;
                if (int.TryParse(row.Cells["colSoLuong"].Value?.ToString(), out int sl))
                    soLuong = sl > 0 ? sl : 1;

                // Tính thành tiền
                decimal thanhTien = soLuong * dv.DonGia;

                // Cập nhật lại các ô
                row.Cells["colDonGia"].Value = dv.DonGia.ToString("N0");
                row.Cells["colThanhTien"].Value = thanhTien.ToString("N0");

                // Cập nhật tổng tiền
                CalculateTotal();
            }
        }
            private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvDichVu.Rows)
            {
                if (row.IsNewRow) continue;
                if (decimal.TryParse(row.Cells["colThanhTien"].Value?.ToString(), out decimal tien))
                    total += tien;
            }
            txtThanhTien.Text = total.ToString("N0");
        }

        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvDichVu.Rows.Add();
            var row = dgvDichVu.Rows[rowIndex];
            row.Cells["colSTT"].Value = (rowIndex + 1).ToString();
            row.Cells["colSoLuong"].Value = "1";
            row.Cells["colDonGia"].Value = "0";
            row.Cells["colThanhTien"].Value = "0";
            row.Cells["colGhiChu"].Value = "";
        }

        private void dgvDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvDichVu.Columns["colXoa"].Index && e.RowIndex >= 0)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa dòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dgvDichVu.Rows.RemoveAt(e.RowIndex);
                    ReindexSTT();
                    CalculateTotal();
                }
            }
        }
        private void ReindexSTT()
        {
            for (int i = 0; i < dgvDichVu.Rows.Count; i++)
            {
                dgvDichVu.Rows[i].Cells["colSTT"].Value = (i + 1).ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maBenhNhanHienTai))
            {
                MessageBox.Show("Vui lòng chọn bệnh nhân!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvDichVu.Rows.Count == 0 || dgvDichVu.Rows[0].IsNewRow)
            {
                MessageBox.Show("Vui lòng thêm ít nhất 1 dịch vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Giả lập lưu vào DB
            MessageBox.Show($"Hoá đơn {maHoaDon} đã được lưu thành công!\nTổng tiền: {txtThanhTien.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset form nếu cần
            // ResetForm();
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

            foreach (DataGridViewRow row in dgvDichVu.Rows)
            {
                if (row.IsNewRow) continue;
                invoiceInfo += $"{row.Cells["colDichVu"].Value} x {row.Cells["colSoLuong"].Value} = {row.Cells["colThanhTien"].Value}\n";
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
