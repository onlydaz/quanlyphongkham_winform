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
using NhaKhoa.UI;

namespace NhaKhoa.Thuoc
{
    public partial class FRM_Thuoc : Form
    {
        private readonly ThuocBUS _thuocBus;

        public FRM_Thuoc()
        {
            InitializeComponent();
            _thuocBus = new ThuocBUS();
        }
        void LoadThuoc()
        {
            var list = _thuocBus.LayDanhSach();
            dgvThuoc.DataSource = ConvertToDataTable(list);
            SetupThuocGrid();
        }

        private DataTable ConvertToDataTable(List<DAL.Models.Thuoc> list)
        {
            var dt = new DataTable();
            dt.Columns.Add("MaThuoc", typeof(string));
            dt.Columns.Add("TenThuoc", typeof(string));
            dt.Columns.Add("DVT", typeof(string));
            dt.Columns.Add("DonGia", typeof(decimal));
            dt.Columns.Add("SoLuongTon", typeof(int));

            foreach (var item in list)
            {
                dt.Rows.Add(item.MaThuoc, item.TenThuoc, item.DVT, item.DonGia, item.SoLuongTon);
            }

            return dt;
        }
        void LoadDVT()
        {
            cb_dvt.Items.Clear();
            cb_dvt.Items.Add("Viên");
            cb_dvt.Items.Add("Vỉ");
            cb_dvt.Items.Add("Chai");
            cb_dvt.Items.Add("Ống");
            cb_dvt.SelectedIndex = 0;
        }

        private void FRM_Thuoc_Load(object sender, EventArgs e)
        {
            LoadThuoc();
            LoadDVT();
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_mathuoc.Text)) return;

            if (MessageBox.Show("Xóa thuốc này?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                _thuocBus.XoaThuoc(txt_mathuoc.Text);
                LoadThuoc();
                ClearForm();
                MessageBox.Show("Xóa thuốc thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            try
            {
                var list = _thuocBus.TimTheoTen(txt_tenthuoc.Text);
                dgvThuoc.DataSource = ConvertToDataTable(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvThuoc.Rows[e.RowIndex];

            txt_mathuoc.Text = row.Cells["MaThuoc"].Value.ToString();
            txt_tenthuoc.Text = row.Cells["TenThuoc"].Value.ToString();
            cb_dvt.Text = row.Cells["DVT"].Value.ToString();
            txt_dongia.Text = row.Cells["DonGia"].Value.ToString();
            txt_sl.Text = row.Cells["SoLuongTon"].Value.ToString();
        }
        void SetupThuocGrid()
        {
            dgvThuoc.RowHeadersVisible = false;
            dgvThuoc.AllowUserToAddRows = false;
            dgvThuoc.ReadOnly = true;
            dgvThuoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThuoc.MultiSelect = false;

            dgvThuoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvThuoc.Columns["MaThuoc"].HeaderText = "Mã thuốc";
            dgvThuoc.Columns["TenThuoc"].HeaderText = "Tên thuốc";
            dgvThuoc.Columns["DVT"].HeaderText = "Đơn vị tính";
            dgvThuoc.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvThuoc.Columns["SoLuongTon"].HeaderText = "Số lượng tồn";

            dgvThuoc.Columns["MaThuoc"].FillWeight = 10;
            dgvThuoc.Columns["TenThuoc"].FillWeight = 35;
            dgvThuoc.Columns["DVT"].FillWeight = 15;
            dgvThuoc.Columns["DonGia"].FillWeight = 20;
            dgvThuoc.Columns["SoLuongTon"].FillWeight = 20;

            dgvThuoc.Columns["DonGia"].DefaultCellStyle.Format = "#,##0 VNĐ";
        }


        void ClearForm()
        {
            txt_mathuoc.Clear();
            txt_tenthuoc.Clear();
            txt_sl.Clear();
            txt_dongia.Clear();
            cb_dvt.SelectedIndex = 0;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_tenthuoc.Text) ||
                    string.IsNullOrWhiteSpace(txt_dongia.Text) ||
                    string.IsNullOrWhiteSpace(txt_sl.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                var thuoc = new DAL.Models.Thuoc
                {
                    MaThuoc = txt_mathuoc.Text, // Có thể để trống để BUS tự sinh
                    TenThuoc = txt_tenthuoc.Text,
                    DVT = cb_dvt.Text,
                    DonGia = decimal.Parse(txt_dongia.Text),
                    SoLuongTon = int.Parse(txt_sl.Text)
                };

                _thuocBus.ThemThuoc(thuoc);
                MessageBox.Show("Thêm thuốc thành công!");
                LoadThuoc();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_mathuoc.Text))
                {
                    MessageBox.Show("Vui lòng chọn thuốc cần sửa!");
                    return;
                }

                var thuoc = new DAL.Models.Thuoc
                {
                    MaThuoc = txt_mathuoc.Text,
                    TenThuoc = txt_tenthuoc.Text,
                    DVT = cb_dvt.Text,
                    DonGia = decimal.Parse(txt_dongia.Text),
                    SoLuongTon = int.Parse(txt_sl.Text)
                };

                _thuocBus.CapNhatThuoc(thuoc);
                MessageBox.Show("Cập nhật thuốc thành công!");
                LoadThuoc();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FormInDanhMuc frmInDanhMuc = new FormInDanhMuc();
            frmInDanhMuc.Show();
        }
    }
}
