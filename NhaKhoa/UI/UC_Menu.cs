using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoa
{
    public partial class UC_Menu : UserControl
    {
        public event EventHandler<MenuItemEventArgs> MenuItemClicked;

        public UC_Menu()
        {
            InitializeComponent();
        }
        // Phân quyền menu
        public void SetRoleVisibility(string role)
        {
            // Ẩn tất cả trước
            btn_Taikhoan.Visible = false;
            btn_Benhnhan.Visible = false;
            btn_Nhanvien.Visible = false;
            btn_Vatlieu.Visible = false;
            btn_Thuoc.Visible = false;
            btn_Hoadon.Visible = false;
            btn_Doanhthu.Visible = false;
            btn_LichLamViec.Visible = false;
            btn_QuanLyLich.Visible = false;
            btn_phieukhambenh.Visible = false;
            btn_Dichvu.Visible = false;
            btn_ds.Visible = false;
            btn_Dangxuat.Visible = false;

            if (string.IsNullOrWhiteSpace(role)) return;

            string Normalize(string s)
            {
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

            var r = Normalize(role);

            // Admin: show full admin menu
            if (r.Contains("admin") || r.Contains("quantri") || r.Contains("quan tri") || r.Contains("quanly") || r.Contains("quan ly"))
            {
                btn_Taikhoan.Visible = true;
                btn_Benhnhan.Visible = true;
                btn_Nhanvien.Visible = true;
                btn_Vatlieu.Visible = true;
                btn_Thuoc.Visible = true;
                btn_Hoadon.Visible = true;
                btn_Doanhthu.Visible = true;
                btn_Dichvu.Visible = true;
                // Admin should see the admin schedule manager, not the doctor-specific schedule view
                btn_QuanLyLich.Visible = true;
                btn_LichLamViec.Visible = false;
                btn_Dangxuat.Visible = true;
                return;
            }

            // Doctor / Bác sĩ: show Danh sách, Hoá đơn, Đăng xuất
            if (r.Contains("bacsi") || r.Contains("bac si") || r.Contains("bác sĩ") || r.Contains("doctor"))
            {
                btn_ds.Visible = true;       // Danh sách
                btn_Hoadon.Visible = true;
                btn_Dangxuat.Visible = true;
                btn_LichLamViec.Visible = true;
                return;
            }

            // Receptionist / Lễ tân
            if (r.Contains("letan") || r.Contains("le tan") || r.Contains("le_tan") || r.Contains("receptionist"))
            {
                btn_phieukhambenh.Visible = true;
                btn_Hoadon.Visible = true;
                btn_Dangxuat.Visible = true;
                return;
            }

            // Default: only show Logout
            btn_Dangxuat.Visible = true;
        }

        private void btn_Taikhoan_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "Taikhoan" });
        }

        private void btn_Benhnhan_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "Benhnhan" });
        }

        private void btn_Nhanvien_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "Nhanvien" });
        }

        private void btn_Vatlieu_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "Vatlieu" });
        }

        private void btn_Thuoc_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "Thuoc" });
        }

        private void btn_Hoadon_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "Hoadon" });
        }

        private void btn_Doanhthu_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "Doanhthu" });
        }

        private void btn_Dangxuat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Form currentForm = this.FindForm();
                if (currentForm != null)
                {
                    currentForm.DialogResult = DialogResult.Abort;
                    currentForm.Close();
                }
            }
        }

        private void btn_phieukhambenh_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "PhieuKham" });
        }

        private void btn_ds_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "DanhSachChoKham" });
        }

        private void btn_Dichvu_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "Dichvu" });
        }

        private void btn_LichLamViec_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "LichLamViec" });
        }

        private void btn_QuanLyLich_Click(object sender, EventArgs e)
        {
            MenuItemClicked?.Invoke(this, new MenuItemEventArgs { MenuItem = "QuanLyLichLamViec" });
        }
    }


    public class MenuItemEventArgs : EventArgs
    {
        public string MenuItem { get; set; }
    }
}
