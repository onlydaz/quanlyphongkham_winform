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

namespace NhaKhoa.Vatlieu
{
    public partial class FRM_Vatlieu : Form
    {
        private readonly VatLieuBUS _vatLieuBus;

        public FRM_Vatlieu()
        {
            InitializeComponent();
            _vatLieuBus = new VatLieuBUS();
        }
        void LoadDungCu()
        {
            try
            {
                var list = _vatLieuBus.LayDanhSach();
                var dt = new DataTable();
                dt.Columns.Add("MaDC", typeof(string));
                dt.Columns.Add("TenDC", typeof(string));
                dt.Columns.Add("SoLuong", typeof(int));
                dt.Columns.Add("DVT", typeof(string));
                dt.Columns.Add("DonGia", typeof(decimal));

                foreach (var vl in list)
                {
                    dt.Rows.Add(vl.MaDC, vl.TenDC, vl.SoLuong, vl.DVT, vl.DonGia);
                }

                dgvDungCu.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetupDataGridView()
        {
            dgvDungCu.AutoGenerateColumns = true;
            dgvDungCu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDungCu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDungCu.MultiSelect = false;
            dgvDungCu.ReadOnly = true;

            dgvDungCu.Columns["MaDC"].HeaderText = "Mã dụng cụ";
            dgvDungCu.Columns["TenDC"].HeaderText = "Tên dụng cụ";
            dgvDungCu.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvDungCu.Columns["DVT"].HeaderText = "Đơn vị tính";
            dgvDungCu.Columns["DonGia"].HeaderText = "Đơn giá";
        }


        private void FRM_Vatlieu_Load(object sender, EventArgs e)
        {
            LoadDungCu();
            SetupDataGridView();
            cbDVT.Items.AddRange(new string[] { "Cái", "Hộp", "Bộ", "Chiếc" });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var vatLieu = new Models.VatLieu
                {
                    MaDC = txtmaDC.Text, // Có thể để trống để BUS tự sinh
                    TenDC = txttenDC.Text,
                    SoLuong = int.Parse(txtsl.Text),
                    DVT = cbDVT.Text,
                    DonGia = decimal.Parse(txtDongia.Text)
                };

                _vatLieuBus.ThemVatLieu(vatLieu);
                LoadDungCu();
                MessageBox.Show("Thêm dụng cụ thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                var vatLieu = new Models.VatLieu
                {
                    MaDC = txtmaDC.Text,
                    TenDC = txttenDC.Text,
                    SoLuong = int.Parse(txtsl.Text),
                    DVT = cbDVT.Text,
                    DonGia = decimal.Parse(txtDongia.Text)
                };

                _vatLieuBus.CapNhatVatLieu(vatLieu);
                LoadDungCu();
                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                _vatLieuBus.XoaVatLieu(txtmaDC.Text);
                LoadDungCu();
                MessageBox.Show("Xóa thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            try
            {
                var list = _vatLieuBus.TimTheoTen(txtmaDC.Text);
                var dt = new DataTable();
                dt.Columns.Add("MaDC", typeof(string));
                dt.Columns.Add("TenDC", typeof(string));
                dt.Columns.Add("SoLuong", typeof(int));
                dt.Columns.Add("DVT", typeof(string));
                dt.Columns.Add("DonGia", typeof(decimal));

                foreach (var vl in list)
                {
                    dt.Rows.Add(vl.MaDC, vl.TenDC, vl.SoLuong, vl.DVT, vl.DonGia);
                }

                dgvDungCu.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvDungCu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dgvDungCu.Rows[e.RowIndex];

                txtmaDC.Text = r.Cells["MaDC"].Value?.ToString();
                txttenDC.Text = r.Cells["TenDC"].Value?.ToString();
                txtsl.Text = r.Cells["SoLuong"].Value?.ToString();
                cbDVT.Text = r.Cells["DVT"].Value?.ToString();
                txtDongia.Text = r.Cells["DonGia"].Value?.ToString();

                dgvDungCu.Columns["DonGia"].DefaultCellStyle.Format = "#,##0 VNĐ";
            }
        }
    }
}
