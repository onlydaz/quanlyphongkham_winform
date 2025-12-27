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

namespace NhaKhoa
{
    public partial class Login : Form
    {
        public string LoggedInRole { get; private set; }
        private readonly AuthBUS _authBus;

        public Login()
        {
            InitializeComponent();
            _authBus = new AuthBUS();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUserName.Text.Trim();
                string password = txtPassWord.Text;

                string role = _authBus.Login(username, password);

                if (role != null)
                {
                    LoggedInRole = role;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
