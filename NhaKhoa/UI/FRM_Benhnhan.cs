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
using NhaKhoa.Models;

namespace NhaKhoa.BenhNhan
{
    public partial class FRM_Benhnhan : Form
    {
        private readonly BenhNhanBUS _benhNhanBus;
        private string currentMaBN = "";

        public FRM_Benhnhan()
        {
            InitializeComponent();
            _benhNhanBus = new BenhNhanBUS();
        }

        private void SetupDataGridViewColumns()
        {
            // Setup DataPropertyName cho các columns để map với DataTable
            if (dgvDSBN.Columns["MaBN"] != null)
                dgvDSBN.Columns["MaBN"].DataPropertyName = "MaBN";
            if (dgvDSBN.Columns["TenBN"] != null)
                dgvDSBN.Columns["TenBN"].DataPropertyName = "TenBN";
            if (dgvDSBN.Columns["SDT"] != null)
                dgvDSBN.Columns["SDT"].DataPropertyName = "SDT";
            if (dgvDSBN.Columns["GioiTinh"] != null)
                dgvDSBN.Columns["GioiTinh"].DataPropertyName = "GioiTinh";
            if (dgvDSBN.Columns["NamSinh"] != null)
                dgvDSBN.Columns["NamSinh"].DataPropertyName = "NamSinh";
            if (dgvDSBN.Columns["DiaChi"] != null)
                dgvDSBN.Columns["DiaChi"].DataPropertyName = "DiaChi";
            if (dgvDSBN.Columns["NgayTao"] != null)
                dgvDSBN.Columns["NgayTao"].DataPropertyName = "NgayKham";
            if (dgvDSBN.Columns["GhiChu"] != null)
                dgvDSBN.Columns["GhiChu"].DataPropertyName = "LyDoKham";
            if (dgvDSBN.Columns["TongTien"] != null)
                dgvDSBN.Columns["TongTien"].DataPropertyName = "TongTien";
        }

        private void LoadData()
        {
            try
            {
                var list = _benhNhanBus.LayDanhSach();
                var dt = ConvertToDataTable(list);
                dgvDSBN.AutoGenerateColumns = false;
                SetupDataGridViewColumns(); // Setup mapping trước khi set DataSource
                dgvDSBN.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private DataTable ConvertToDataTable(List<Models.BenhNhan> list)
        {
            var dt = new DataTable();
            dt.Columns.Add("MaBN", typeof(string));
            dt.Columns.Add("TenBN", typeof(string));
            dt.Columns.Add("SDT", typeof(string));
            dt.Columns.Add("DiaChi", typeof(string));
            dt.Columns.Add("NamSinh", typeof(int));
            dt.Columns.Add("GioiTinh", typeof(string));
            dt.Columns.Add("NgayKham", typeof(DateTime));
            dt.Columns.Add("LyDoKham", typeof(string));
            // Thêm columns mà DataGridView có nhưng model không có
            dt.Columns.Add("TongTien", typeof(decimal));

            foreach (var bn in list.OrderBy(x => x.MaBN))
            {
                dt.Rows.Add(
                    bn.MaBN,
                    bn.TenBN,
                    bn.SDT ?? "",
                    bn.DiaChi ?? "",
                    bn.NamSinh,
                    bn.GioiTinh ?? "",
                    bn.NgayKham,
                    bn.LyDoKham ?? "",
                    0m // TongTien - không có trong model, mặc định 0
                );
            }

            return dt;
        }

        private void ClearInputFields()
        {
            txtMaBN.Clear();
            txtTenBN.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            dtpNgaySinh.Value = DateTime.Today;
            rbtnNam.Checked = true;
            dtpNgayKham.Value = DateTime.Today;
            txtLyDoKham.Clear();
            currentMaBN = "";
        }

        private void FillFormFromSelectedRow()
        {
            if (dgvDSBN.SelectedRows.Count == 0) return;

            var row = dgvDSBN.SelectedRows[0];
            if (row.IsNewRow) return;

            // Lấy dữ liệu từ DataTable thay vì từ row.Cells để tránh lỗi column name
            if (dgvDSBN.DataSource is DataTable dt)
            {
                int rowIndex = row.Index;
                if (rowIndex >= dt.Rows.Count) return;

                var dataRow = dt.Rows[rowIndex];

                txtMaBN.Text = dataRow["MaBN"]?.ToString() ?? "";
                txtTenBN.Text = dataRow["TenBN"]?.ToString() ?? "";
                txtSDT.Text = dataRow["SDT"]?.ToString() ?? "";
                txtDiaChi.Text = dataRow["DiaChi"]?.ToString() ?? "";

                object namSinhObj = dataRow["NamSinh"];
                if (namSinhObj != DBNull.Value && namSinhObj != null)
                {
                    int namSinh = Convert.ToInt32(namSinhObj);
                    dtpNgaySinh.Value = new DateTime(namSinh, 1, 1);
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Today;
                }

                string gioiTinh = dataRow["GioiTinh"]?.ToString() ?? "Nam";
                rbtnNam.Checked = (gioiTinh == "Nam");
                rbtnNu.Checked = (gioiTinh == "Nữ");

                object ngayKhamObj = dataRow["NgayKham"];
                if (ngayKhamObj != DBNull.Value && ngayKhamObj != null)
                {
                    dtpNgayKham.Value = Convert.ToDateTime(ngayKhamObj);
                }
                else
                {
                    dtpNgayKham.Value = DateTime.Today;
                }

                txtLyDoKham.Text = dataRow["LyDoKham"]?.ToString() ?? "";
            }
            else
            {
                // Fallback: Lấy từ row.Cells nếu không phải DataTable
                txtMaBN.Text = row.Cells["MaBN"].Value?.ToString() ?? "";
                txtTenBN.Text = row.Cells["TenBN"].Value?.ToString() ?? "";
                txtSDT.Text = row.Cells["SDT"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";

                object namSinhObj = row.Cells["NamSinh"].Value;
                if (namSinhObj != DBNull.Value && namSinhObj != null)
                {
                    int namSinh = Convert.ToInt32(namSinhObj);
                    dtpNgaySinh.Value = new DateTime(namSinh, 1, 1);
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Today;
                }

                string gioiTinh = row.Cells["GioiTinh"].Value?.ToString() ?? "Nam";
                rbtnNam.Checked = (gioiTinh == "Nam");
                rbtnNu.Checked = (gioiTinh == "Nữ");

                // Lấy từ column "NgayTao" thay vì "NgayKham" vì trong DataGridView column tên là "NgayTao"
                object ngayKhamObj = null;
                if (row.Cells["NgayTao"] != null)
                    ngayKhamObj = row.Cells["NgayTao"].Value;
                else if (row.Cells["NgayKham"] != null)
                    ngayKhamObj = row.Cells["NgayKham"].Value;

                if (ngayKhamObj != DBNull.Value && ngayKhamObj != null)
                {
                    dtpNgayKham.Value = Convert.ToDateTime(ngayKhamObj);
                }
                else
                {
                    dtpNgayKham.Value = DateTime.Today;
                }

                // Lấy LyDoKham từ column "GhiChu" vì trong DataGridView column tên là "GhiChu"
                if (row.Cells["GhiChu"] != null)
                    txtLyDoKham.Text = row.Cells["GhiChu"].Value?.ToString() ?? "";
                else if (row.Cells["LyDoKham"] != null)
                    txtLyDoKham.Text = row.Cells["LyDoKham"].Value?.ToString() ?? "";
            }

            currentMaBN = txtMaBN.Text;
        }
        private void SearchPatient(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadData();
                    return;
                }

                // Tìm kiếm theo mã hoặc tên
                var list = _benhNhanBus.TimKiem(ma: keyword, ten: keyword);
                var dt = ConvertToDataTable(list);
                dgvDSBN.AutoGenerateColumns = false;
                SetupDataGridViewColumns(); // Setup mapping trước khi set DataSource
                dgvDSBN.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void FRM_Benhnhan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvDSBN_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDSBN.SelectedRows.Count > 0)
            {
                FillFormFromSelectedRow();
            }
        }

        private void mnuThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenBN.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tên BN!");
                    return;
                }

                string maBN = txtMaBN.Text.Trim();
                string tenBN = txtTenBN.Text.Trim();
                string sdt = txtSDT.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                int namSinh = dtpNgaySinh.Value.Year;
                string gioiTinh = rbtnNam.Checked ? "Nam" : "Nữ";
                DateTime ngayKham = dtpNgayKham.Value;
                string lyDoKham = txtLyDoKham.Text.Trim();

                var benhNhan = new Models.BenhNhan
                {
                    MaBN = string.IsNullOrWhiteSpace(maBN) ? null : maBN, // BUS sẽ tự sinh nếu để trống
                    TenBN = tenBN,
                    SDT = sdt,
                    DiaChi = diaChi,
                    NamSinh = namSinh,
                    GioiTinh = gioiTinh,
                    NgayKham = ngayKham,
                    LyDoKham = lyDoKham,
                    TrangThai = "Chờ khám" // Mặc định là "Chờ khám" khi thêm mới
                };

                _benhNhanBus.ThemBenhNhan(benhNhan);
                MessageBox.Show("Thêm thành công!");
                LoadData();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void mnuCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentMaBN))
                {
                    MessageBox.Show("Vui lòng chọn bệnh nhân để cập nhật!");
                    return;
                }

                string tenBN = txtTenBN.Text.Trim();
                string sdt = txtSDT.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                int namSinh = dtpNgaySinh.Value.Year;
                string gioiTinh = rbtnNam.Checked ? "Nam" : "Nữ";
                DateTime ngayKham = dtpNgayKham.Value;
                string lyDoKham = txtLyDoKham.Text.Trim();

                // Lấy thông tin bệnh nhân hiện tại để giữ TrangThai
                var benhNhanHienTai = _benhNhanBus.LayBenhNhanTheoMa(currentMaBN);
                
                var benhNhan = new Models.BenhNhan
                {
                    MaBN = currentMaBN,
                    TenBN = tenBN,
                    SDT = sdt,
                    DiaChi = diaChi,
                    NamSinh = namSinh,
                    GioiTinh = gioiTinh,
                    NgayKham = ngayKham,
                    LyDoKham = lyDoKham,
                    TrangThai = benhNhanHienTai?.TrangThai ?? "Chờ khám" // Giữ TrangThai hiện tại, nếu null thì mặc định "Chờ khám"
                };

                _benhNhanBus.CapNhatBenhNhan(benhNhan);
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void mnuLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void btnTimKiemBN_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiemBN.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }
            SearchPatient(keyword);


        }

        private void mnuChiTietLamSan_Click(object sender, EventArgs e)
        {

        }

        private void mnuXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentMaBN))
                {
                    MessageBox.Show("Vui lòng chọn bệnh nhân để xóa!");
                    return;
                }

                if (MessageBox.Show($"Xóa bệnh nhân {currentMaBN}?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _benhNhanBus.XoaBenhNhan(currentMaBN);
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
