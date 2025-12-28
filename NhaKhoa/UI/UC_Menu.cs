using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoa.GUI
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
            // Ẩn tất cả
            btn_Taikhoan.Visible = false;
            btn_Benhnhan.Visible = false;
            btn_Nhanvien.Visible = false;
            btn_Vatlieu.Visible = false;
            btn_Thuoc.Visible = false;
            btn_Hoadon.Visible = false;
            btn_Doanhthu.Visible = false;
            btn_phieukhambenh.Visible = false;
            btn_Dichvu.Visible = false;
            btn_ds.Visible = false;

            switch (role.ToLower())
            {
                case "admin":
                    btn_Taikhoan.Visible = true;
                    btn_Benhnhan.Visible = true;
                    btn_Nhanvien.Visible = true;
                    btn_Vatlieu.Visible = true;
                    btn_Thuoc.Visible = true;
                    btn_Hoadon.Visible = true;
                    btn_Doanhthu.Visible = true;
                    btn_Dichvu.Visible = true;
                    break;
                case "doctor":
                    btn_ds.Visible = true;
                    btn_Hoadon.Visible = true;
                    break;
                case "receptionist":
                    btn_phieukhambenh.Visible = true;
                    break;
            }
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
    }


    public class MenuItemEventArgs : EventArgs
    {
        public string MenuItem { get; set; }
    }
}
