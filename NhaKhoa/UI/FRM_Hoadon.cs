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
                string maHD = txtMaHoaDon.Text.Trim();
                string maNV = cboNhanVien.SelectedIndex == -1 ? "" : cboNhanVien.SelectedValue?.ToString();
                string tenBN = txtTenBenhNhan.Text.Trim();
                DateTime? tuNgay = dtpTuNgay.Value.Date;
                // Tính toán denNgay là cuối ngày (23:59:59.9999999) trước khi truyền vào BUS
                DateTime? denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);

                var list = _hoaDonBus.TimKiem(maHD, maNV, tenBN, tuNgay, denNgay);

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
            txtMaHoaDon.Clear();
            txtTenBenhNhan.Clear();
            cboNhanVien.SelectedIndex = -1;
            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today;

            LoadHoaDon();
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            //if (dgvHoaDon.SelectedRows.Count == 0) return;

            //string maHD = dgvHoaDon.SelectedRows[0].Cells["MaHD"].Value.ToString();
            //FrmChiTietHoaDon frm = new FrmChiTietHoaDon(maHD);
            //frm.ShowDialog();
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
    }
}
