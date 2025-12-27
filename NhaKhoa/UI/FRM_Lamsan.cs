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

namespace NhaKhoa
{
    public partial class FRM_Lamsan : Form
    {
        private string _maBN;
        private readonly BenhNhanBUS _benhNhanBus;
        private readonly ChuanDoanBUS _chuanDoanBus;

        public FRM_Lamsan(string maBN)
        {
            InitializeComponent();
            _maBN = maBN;
            _benhNhanBus = new BenhNhanBUS();
            _chuanDoanBus = new ChuanDoanBUS();
            LoadThongTinBenhNhan();
            LoadDanhSachLamSan();
        }
        private void LoadThongTinBenhNhan()
        {
            try
            {
                var bn = _benhNhanBus.LayBenhNhanTheoMa(_maBN);
                if (bn != null)
                {
                    lblMaBN.Text = bn.MaBN;
                    lblTenBN.Text = bn.TenBN;
                    lblNgaySinh.Text = bn.NamSinh.ToString();
                    lblGioiTinh.Text = bn.GioiTinh;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadDanhSachLamSan()
        {
            try
            {
                var list = _chuanDoanBus.LayChuanDoanTheoMaBN(_maBN);
                var dt = new DataTable();
                dt.Columns.Add("Mã chẩn đoán", typeof(string));
                dt.Columns.Add("Tên chẩn đoán", typeof(string));
                // Theo ERD mới: CHANDOAN không có MaLS, liên kết được quản lý ở LAMSAN

                foreach (var cd in list)
                {
                    dt.Rows.Add(cd.MaCD, cd.TenChuanDoan ?? "");
                }

                dgvLamSan.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemLamSan_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtChanDoan.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên chẩn đoán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Theo ERD mới: ChanDoan chỉ có MaCD và TenChuanDoan (không có MaLS)
                // Liên kết được quản lý ở bảng LAMSAN (LAMSAN có MaCD và MaDT)
                var chuanDoan = new Models.ChanDoan
                {
                    MaCD = "", // BUS sẽ tự sinh
                    TenChuanDoan = txtChanDoan.Text.Trim()
                    // MaLS không còn trong database
                };

                _chuanDoanBus.ThemChuanDoan(chuanDoan);
                MessageBox.Show("Thêm chẩn đoán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtChanDoan.Clear();
                LoadDanhSachLamSan();
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
                if (dgvLamSan.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một chẩn đoán để xóa!");
                    return;
                }

                string maCD = dgvLamSan.SelectedRows[0].Cells["Mã chẩn đoán"].Value.ToString();

                var result = MessageBox.Show($"Bạn có chắc muốn xóa chẩn đoán {maCD}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                _chuanDoanBus.XoaChuanDoan(maCD);
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachLamSan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Không tự động cập nhật trạng thái - để FRM_Nhasi xử lý
            this.Close();
        }

        private void btnInToaThuoc_Click(object sender, EventArgs e)
        {
            string toaThuoc = $"TOA THUỐC\n\n" +
                      $"Bệnh nhân: {lblTenBN.Text} ({lblMaBN.Text})\n" +
                      $"Ngày sinh: {lblNgaySinh.Text}\n" +
                      $"Giới tính: {lblGioiTinh.Text}\n\n" +
                      $"Chẩn đoán:\n";

            foreach (DataGridViewRow row in dgvLamSan.Rows)
            {
                toaThuoc += $"- {row.Cells["Tên chẩn đoán"].Value}\n";
            }

            MessageBox.Show(toaThuoc, "Toa thuốc", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
