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
        public void SetRoleVisibility(string role)
        {
            btn_Benhnhan.Visible = false;
            btn_Nhanvien.Visible = false;
            btn_Vatlieu.Visible = false;
            btn_Thuoc.Visible = false;
            btn_Hoadon.Visible = false;
            btn_Doanhthu.Visible = false;

            if (role.ToLower() == "admin")
            {
                btn_Benhnhan.Visible = true;
                btn_Nhanvien.Visible = true;
                btn_Vatlieu.Visible = true;
                btn_Thuoc.Visible = true;
                btn_Hoadon.Visible = true;
                btn_Doanhthu.Visible = true;
            }
            else if (role.ToLower() == "lễ tân")
            {
                btn_Benhnhan.Visible = true;
                btn_Hoadon.Visible = true;
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
    }


    public class MenuItemEventArgs : EventArgs
    {
        public string MenuItem { get; set; }
    }
}
