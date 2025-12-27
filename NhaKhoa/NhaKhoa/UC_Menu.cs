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
            btn_LichLamViec.Visible = false;
            btn_QuanLyLich.Visible = false;

            if (string.IsNullOrWhiteSpace(role))
                return;

            // Normalize: lowercase and remove diacritics so comparisons are robust
            string Normalize(string s)
            {
                if (string.IsNullOrWhiteSpace(s)) return string.Empty;
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

            // Admin variants
            if (r.Contains("admin") || r.Contains("quantri") || r.Contains("quảntri") || r.Contains("quản trị"))
            {
                btn_Benhnhan.Visible = true;
                btn_Nhanvien.Visible = true;
                btn_Vatlieu.Visible = true;
                btn_Thuoc.Visible = true;
                btn_Hoadon.Visible = true;
                btn_Doanhthu.Visible = true;
                // Admin should open the management schedule screen
                btn_QuanLyLich.Visible = true;
                btn_LichLamViec.Visible = false;
                return;
            }

            // Receptionist variants
            if (r.Contains("letan") || r.Contains("le tan") || r.Contains("receptionist") || r.Contains("reception"))
            {
                btn_Benhnhan.Visible = true;
                btn_Hoadon.Visible = true;
                btn_LichLamViec.Visible = true;
                return;
            }

            // Doctor variants
            if (r.Contains("bacsi") || r.Contains("bac si") || r.Contains("bacs") || r.Contains("doctor"))
            {
                btn_LichLamViec.Visible = true;
                return;
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
