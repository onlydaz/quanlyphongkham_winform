using NhaKhoa.BenhNhan;
using NhaKhoa.NhanVien;
using NhaKhoa.TaiKhoan;
using NhaKhoa.Thuoc;
using NhaKhoa.Vatlieu;
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
    public partial class frmMain : Form
    {
        private Control currentContent = null;

        public frmMain()
        {
            InitializeComponent();
            UC_Menu menu = new UC_Menu();
            menu.Dock = DockStyle.Left;
            menu.MenuItemClicked += UC_Menu_MenuItemClicked;
            this.Controls.Add(menu);
        }

        //Mở form con bên trong panel
        private void ShowContent(Form form)
        {
            //Xóa nội dung cũ
            if (currentContent != null)
            {
                pnlContent.Controls.Remove(currentContent);
                currentContent.Dispose();
            }

            //Thiết lập form con như một control
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            
            pnlContent.Controls.Add(form);
            form.Show();
            
            currentContent = form;
        }

        //Xử lý sự kiện menu
        private void UC_Menu_MenuItemClicked(object sender, MenuItemEventArgs e)
        {
            switch (e.MenuItem)
            {
                case "Taikhoan":
                    ShowContent(new FRM_Account());
                    break;
                case "Benhnhan":
                    ShowContent(new FRM_Benhnhan());
                    break;
                case "Nhanvien":
                    ShowContent(new FRM_Nhanvien());
                    break;
                case "Vatlieu":
                    ShowContent(new FRM_Vatlieu());
                    break;
                case "Thuoc":
                    ShowContent(new FRM_Thuoc());
                    break;
                case "Hoadon":
                    //ShowContent(new frmHoaDon());
                    break;
                case "Doanhthu":
                    //ShowContent(new frmDoanhThu());
                    break;
                case "Datlich":
                    //ShowContent(new frmDatlich());
                    break;
            }
        }

        //Form đặt lịch
        private void mnuDatlich_Click(object sender, EventArgs e)
        {
            ShowContent(new frmDatlich());
        }
    }
}
