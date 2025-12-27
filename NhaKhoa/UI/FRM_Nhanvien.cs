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

namespace NhaKhoa.NhanVien
{
    public partial class FRM_Nhanvien : Form
    {
        private readonly NhanVienBUS _nhanVienBus;
        private string currentMaNV = "";
        private Dictionary<string, string> _chucVuDict; // Dictionary để map MaCV -> TenCV

        public FRM_Nhanvien()
        {
            InitializeComponent();
            _nhanVienBus = new NhanVienBUS();
        }
        private void FillFormFromSelectedRow()
        {
            if (dgvDSNV.SelectedRows.Count == 0) return;

            var row = dgvDSNV.SelectedRows[0];
            if (row.IsNewRow) return;

            // Lấy dữ liệu từ DataTable thay vì từ row.Cells để tránh lỗi column name
            if (dgvDSNV.DataSource is DataTable dt)
            {
                int rowIndex = row.Index;
                if (rowIndex >= dt.Rows.Count) return;

                var dataRow = dt.Rows[rowIndex];

                txtMaNV.Text = dataRow["MaNV"]?.ToString() ?? "";
                txtTenNV.Text = dataRow["TenNV"]?.ToString() ?? "";

                string maCV = dataRow["MaCV"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(maCV) && cboChucVu.Items.Count > 0)
                {
                    cboChucVu.SelectedValue = maCV;
                }
                else
                {
                    cboChucVu.SelectedIndex = -1;
                }

                object ngayVaoLamObj = dataRow["NgayVaoLam"];
                if (ngayVaoLamObj != DBNull.Value && ngayVaoLamObj != null)
                {
                    dtpNgayVaoLam.Value = Convert.ToDateTime(ngayVaoLamObj);
                }
                else
                {
                    dtpNgayVaoLam.Value = DateTime.Today;
                }

                txtSDT.Text = dataRow["SDT"]?.ToString() ?? "";
                txtEmail.Text = dataRow["Email"]?.ToString() ?? "";
                txtDiaChi.Text = dataRow["DiaChi"]?.ToString() ?? "";
                
                string gioiTinh = dataRow["GioiTinh"]?.ToString() ?? "Nam";
                rbtnNam.Checked = (gioiTinh == "Nam");
                rbtnNu.Checked = (gioiTinh == "Nữ");
            }
            else
            {
                // Fallback: Lấy từ row.Cells nếu không phải DataTable
                txtMaNV.Text = row.Cells["frm_NVMaNV"].Value?.ToString() ?? "";
                txtTenNV.Text = row.Cells["frm_NVTenNV"].Value?.ToString() ?? "";

                // Lấy MaCV từ DataTable nếu có
                string maCV = "";
                if (dgvDSNV.DataSource is DataTable dt2)
                {
                    int rowIndex = row.Index;
                    if (rowIndex < dt2.Rows.Count)
                    {
                        maCV = dt2.Rows[rowIndex]["MaCV"]?.ToString() ?? "";
                    }
                }

                if (!string.IsNullOrEmpty(maCV) && cboChucVu.Items.Count > 0)
                {
                    cboChucVu.SelectedValue = maCV;
                }
                else
                {
                    cboChucVu.SelectedIndex = -1;
                }

                // Tìm column NgayVaoLam - có thể tên khác trong DataGridView
                object ngayVaoLamObj = null;
                foreach (DataGridViewColumn col in dgvDSNV.Columns)
                {
                    if (col.DataPropertyName == "NgayVaoLam" && row.Cells[col.Name] != null)
                    {
                        ngayVaoLamObj = row.Cells[col.Name].Value;
                        break;
                    }
                }

                if (ngayVaoLamObj != DBNull.Value && ngayVaoLamObj != null)
                {
                    dtpNgayVaoLam.Value = Convert.ToDateTime(ngayVaoLamObj);
                }
                else
                {
                    dtpNgayVaoLam.Value = DateTime.Today;
                }

                // Tìm column SDT và Email
                foreach (DataGridViewColumn col in dgvDSNV.Columns)
                {
                    if (col.DataPropertyName == "SDT" && row.Cells[col.Name] != null)
                    {
                        txtSDT.Text = row.Cells[col.Name].Value?.ToString() ?? "";
                        break;
                    }
                }

                foreach (DataGridViewColumn col in dgvDSNV.Columns)
                {
                    if (col.DataPropertyName == "Email" && row.Cells[col.Name] != null)
                    {
                        txtEmail.Text = row.Cells[col.Name].Value?.ToString() ?? "";
                        break;
                    }
                }

                foreach (DataGridViewColumn col in dgvDSNV.Columns)
                {
                    if (col.DataPropertyName == "DiaChi" && row.Cells[col.Name] != null)
                    {
                        txtDiaChi.Text = row.Cells[col.Name].Value?.ToString() ?? "";
                        break;
                    }
                }

                foreach (DataGridViewColumn col in dgvDSNV.Columns)
                {
                    if (col.DataPropertyName == "GioiTinh" && row.Cells[col.Name] != null)
                    {
                        string gioiTinh = row.Cells[col.Name].Value?.ToString() ?? "Nam";
                        rbtnNam.Checked = (gioiTinh == "Nam");
                        rbtnNu.Checked = (gioiTinh == "Nữ");
                        break;
                    }
                }
            }

            currentMaNV = txtMaNV.Text;
        }
        private void ClearInputFields()
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            cboChucVu.SelectedIndex = -1;
            dtpNgayVaoLam.Value = DateTime.Today;
            txtSDT.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            rbtnNam.Checked = true;
            rbtnNu.Checked = false;
            currentMaNV = "";
        }
        private DataTable ConvertToDataTable(List<Models.NhanVien> list)
        {
            var dt = new DataTable();
            dt.Columns.Add("MaNV", typeof(string));
            dt.Columns.Add("TenNV", typeof(string));
            dt.Columns.Add("ChucVu", typeof(string));
            dt.Columns.Add("NgayVaoLam", typeof(DateTime));
            dt.Columns.Add("SDT", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("MaCV", typeof(string));
            // Thêm các columns mà DataGridView có nhưng model không có (để tránh lỗi mapping)
            dt.Columns.Add("GioiTinh", typeof(string));
            dt.Columns.Add("NamSinh", typeof(int));
            dt.Columns.Add("DiaChi", typeof(string));

            foreach (var nv in list.OrderBy(x => x.MaNV))
            {
                string tenCV = "";
                if (!string.IsNullOrEmpty(nv.MaCV) && _chucVuDict != null && _chucVuDict.ContainsKey(nv.MaCV))
                {
                    tenCV = _chucVuDict[nv.MaCV];
                }

                dt.Rows.Add(
                    nv.MaNV,
                    nv.TenNV,
                    tenCV,
                    nv.NgayVaoLam,
                    nv.SDT ?? "",
                    nv.Email ?? "",
                    nv.MaCV ?? "",
                    nv.GioiTinh ?? "", // GioiTinh từ model
                    0,  // NamSinh - không có trong model
                    nv.DiaChi ?? ""  // DiaChi từ model
                );
            }

            return dt;
        }

        private void SetupDataGridViewColumns()
        {
            // Setup DataPropertyName cho các columns để map với DataTable
            if (dgvDSNV.Columns["frm_NVMaNV"] != null)
                dgvDSNV.Columns["frm_NVMaNV"].DataPropertyName = "MaNV";
            if (dgvDSNV.Columns["frm_NVTenNV"] != null)
                dgvDSNV.Columns["frm_NVTenNV"].DataPropertyName = "TenNV";
            if (dgvDSNV.Columns["frm_NVChucVu"] != null)
                dgvDSNV.Columns["frm_NVChucVu"].DataPropertyName = "ChucVu";
            if (dgvDSNV.Columns["frm_NVGioiTinh"] != null)
                dgvDSNV.Columns["frm_NVGioiTinh"].DataPropertyName = "GioiTinh";
            if (dgvDSNV.Columns["frm_NVNamSinh"] != null)
                dgvDSNV.Columns["frm_NVNamSinh"].DataPropertyName = "NamSinh";
            if (dgvDSNV.Columns["frm_NVSDT"] != null)
                dgvDSNV.Columns["frm_NVSDT"].DataPropertyName = "SDT";
            if (dgvDSNV.Columns["frm_NVEmail"] != null)
                dgvDSNV.Columns["frm_NVEmail"].DataPropertyName = "Email";
            if (dgvDSNV.Columns["frm_NVDiaChi"] != null)
                dgvDSNV.Columns["frm_NVDiaChi"].DataPropertyName = "DiaChi";
        }

        private void LoadData()
        {
            try
            {
                var list = _nhanVienBus.LayDanhSach();
                var dt = ConvertToDataTable(list);
                dgvDSNV.AutoGenerateColumns = false;
                SetupDataGridViewColumns(); // Setup mapping trước khi set DataSource
                dgvDSNV.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadChucVu()
        {
            try
            {
                // Load ChucVu từ BUS
                var chucVuList = _nhanVienBus.LayDanhSachChucVu();

                var dt = new DataTable();
                dt.Columns.Add("MaCV", typeof(string));
                dt.Columns.Add("TenCV", typeof(string));

                _chucVuDict = new Dictionary<string, string>();

                foreach (var cv in chucVuList)
                {
                    dt.Rows.Add(cv.MaCV, cv.TenCV);
                    _chucVuDict[cv.MaCV] = cv.TenCV;
                }

                cboChucVu.DataSource = dt;
                cboChucVu.DisplayMember = "TenCV";
                cboChucVu.ValueMember = "MaCV";
                cboChucVu.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chức vụ: " + ex.Message);
            }
        }

        private void mnuCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentMaNV))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên để cập nhật!");
                    return;
                }

                string tenNV = txtTenNV.Text.Trim();
                DateTime ngayVaoLam = dtpNgayVaoLam.Value;
                string maCV = cboChucVu.SelectedValue?.ToString() ?? "";
                string sdt = txtSDT.Text.Trim();
                string email = txtEmail.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string gioiTinh = rbtnNam.Checked ? "Nam" : "Nữ";

                var nhanVien = new Models.NhanVien
                {
                    MaNV = currentMaNV,
                    TenNV = tenNV,
                    NgayVaoLam = ngayVaoLam,
                    MaCV = string.IsNullOrEmpty(maCV) ? null : maCV,
                    SDT = string.IsNullOrEmpty(sdt) ? null : sdt,
                    Email = string.IsNullOrEmpty(email) ? null : email,
                    DiaChi = string.IsNullOrEmpty(diaChi) ? null : diaChi,
                    GioiTinh = gioiTinh
                };

                _nhanVienBus.CapNhatNhanVien(nhanVien);
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void mnuThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenNV.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tên nhân viên!");
                    return;
                }

                string maNV = txtMaNV.Text.Trim();
                string tenNV = txtTenNV.Text.Trim();
                DateTime ngayVaoLam = dtpNgayVaoLam.Value;
                string maCV = cboChucVu.SelectedValue?.ToString() ?? "";
                string sdt = txtSDT.Text.Trim();
                string email = txtEmail.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string gioiTinh = rbtnNam.Checked ? "Nam" : "Nữ";

                var nhanVien = new Models.NhanVien
                {
                    MaNV = string.IsNullOrWhiteSpace(maNV) ? null : maNV, // BUS sẽ tự sinh nếu để trống
                    TenNV = tenNV,
                    NgayVaoLam = ngayVaoLam,
                    MaCV = string.IsNullOrEmpty(maCV) ? null : maCV,
                    SDT = string.IsNullOrEmpty(sdt) ? null : sdt,
                    Email = string.IsNullOrEmpty(email) ? null : email,
                    DiaChi = string.IsNullOrEmpty(diaChi) ? null : diaChi,
                    GioiTinh = gioiTinh
                };

                _nhanVienBus.ThemNhanVien(nhanVien);
                MessageBox.Show("Thêm thành công!");
                LoadData();
                ClearInputFields();
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

        private void mnuXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentMaNV))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên để xóa!");
                    return;
                }

                if (MessageBox.Show($"Xóa nhân viên {currentMaNV}?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _nhanVienBus.XoaNhanVien(currentMaNV);
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

        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = textBox7.Text.Trim();
                if (string.IsNullOrEmpty(keyword))
                {
                    LoadData();
                    return;
                }

                // Tìm kiếm theo mã hoặc tên
                var list = _nhanVienBus.TimKiem(ma: keyword, ten: keyword);
                var dt = ConvertToDataTable(list);
                dgvDSNV.AutoGenerateColumns = false;
                SetupDataGridViewColumns(); // Setup mapping trước khi set DataSource
                dgvDSNV.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void FRM_Nhanvien_Load(object sender, EventArgs e)
        {
            LoadChucVu(); // Load ChucVu trước để khởi tạo _chucVuDict
            LoadData();
        }

        private void dgvDSNV_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDSNV.SelectedRows.Count > 0)
            {
                FillFormFromSelectedRow();
            }
        }
    }
}
