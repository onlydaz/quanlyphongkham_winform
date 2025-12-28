using NhaKhoa.BenhNhan;
using NhaKhoa.Hoadon;
using NhaKhoa.Letan;
using NhaKhoa.NhanVien;
using NhaKhoa.NhaSi;
using NhaKhoa.TaiKhoan;
using NhaKhoa.Thuoc;
using NhaKhoa.UI;
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
        private string _userRole;
        private int _loggedInUserId;
        private Form _currentContent;

        public frmMain(string role, int loggedInUserId = 0)
        {
            InitializeComponent();
            _userRole = role;
            _loggedInUserId = loggedInUserId;

            UC_Menu menu = new UC_Menu();
            menu.Dock = DockStyle.Left;
            menu.SetRoleVisibility(_userRole);

            menu.MenuItemClicked += UC_Menu_MenuItemClicked; 

            this.Controls.Add(menu);

            switch (_userRole.ToLower())
            {
                case "admin":
                    break;

                case "receptionist":
                    ShowContent(new FRM_Letan());
                    break;

                case "doctor":
                    ShowContent(new FRM_Nhasi());
                    break;

                default:
                    break;
            }
        }
        public void OpenForm(Form f)
        {
            pnlContent.Controls.Clear();

            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(f);
            f.Show();
        }


        //Mở form con bên trong panel
        private void ShowContent(Form form)
        {
            // Đóng form hiện tại (nếu có)
            if (_currentContent != null)
            {
                _currentContent.Close(); // hoặc Dispose() nếu không cần giữ trạng thái
                _currentContent = null;
            }

            // Thiết lập form con
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // Thêm vào panel và hiển thị
            pnlContent.Controls.Clear(); // Xóa toàn bộ control cũ (an toàn hơn)
            pnlContent.Controls.Add(form);
            form.Show();

            _currentContent = form;
        }

        //Xử lý sự kiện menu
        private void UC_Menu_MenuItemClicked(object sender, MenuItemEventArgs e)
        {
            Form formToOpen = null;

            switch (e.MenuItem)
            {
                case "Taikhoan":
                    formToOpen = new FRM_Account();
                    break;

                case "Benhnhan":
                    formToOpen = new FRM_Benhnhan();
                    break;

                case "Nhanvien":
                    formToOpen = new FRM_Nhanvien();
                    break;

                case "Vatlieu":
                    formToOpen = new FRM_Vatlieu();
                    break;

                case "Thuoc":
                    formToOpen = new FRM_Thuoc();
                    break;
                case "Dichvu":
                    formToOpen = new FRM_Dichvu();
                    break;

                case "Hoadon":
                    if (_userRole == "doctor")
                        formToOpen = new FRM_AddHoadon();
                    else
                        formToOpen = new FRM_Hoadon();
                    break;

                case "Doanhthu":
                    formToOpen = new FRM_DoanhThu();
                    break;

                case "LichLamViec":
                    // Pass logged in user id so doctor form can auto-select the corresponding NhanVien
                    formToOpen = new NhaKhoa.UI.FRM_LichLamViec(_loggedInUserId);
                    break;

                case "QuanLyLichLamViec":
                    formToOpen = new NhaKhoa.UI.FRM_QuanLyLichLamViec();
                    break;

                case "PhieuKham":
                    formToOpen = new FRM_Letan();
                    break;

                case "DanhSachChoKham":
                    formToOpen = new FRM_Nhasi();
                    break;
            }

            if (formToOpen != null)
            {
                ShowContent(formToOpen);
            }
        }
    }
}
