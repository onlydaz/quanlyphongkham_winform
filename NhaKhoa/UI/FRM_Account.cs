using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using System.Windows.Forms;
using System.IO;
using NhaKhoa.BUS;

namespace NhaKhoa.TaiKhoan
{
    public partial class FRM_Account : Form
    {
        private int currentUserId = 0;
        private readonly UserBUS _userBus;
        private readonly RoleBUS _roleBus;
        private readonly UserRoleBUS _userRoleBus;

        public FRM_Account()
        {
            InitializeComponent();
            _userBus = new UserBUS();
            _roleBus = new RoleBUS();
            _userRoleBus = new UserRoleBUS();
        }
        private void FRM_Account_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            LoadRoles();
            LoadAccounts();
        }
        private void LoadRoles()
        {
            try
            {
                var list = _roleBus.LayDanhSach();
                cbRole.Items.Clear();
                cbRole.Items.Add(new RoleItem { Id = 0, Name = "-- Chọn vai trò --" }); // Item mặc định
                foreach (var role in list.OrderBy(x => x.Name))
                {
                    cbRole.Items.Add(new RoleItem { Id = role.Id, Name = role.Name });
                }
                cbRole.SelectedIndex = 0; // Chọn item mặc định
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadAccounts()
        {
            try
            {
                var list = _userBus.LayDanhSachTaiKhoan();
                var dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Username", typeof(string));
                dt.Columns.Add("FullName", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Roles", typeof(string));

                foreach (var tk in list)
                {
                    dt.Rows.Add(tk.Id, tk.Username, tk.FullName, tk.Email, tk.Status, tk.Roles);
                }

                dgvAccount.DataSource = dt;

                dgvAccount.Columns["Username"].HeaderText = "Tên đăng nhập";
                dgvAccount.Columns["FullName"].HeaderText = "Họ và tên";
                dgvAccount.Columns["Email"].HeaderText = "Email";
                dgvAccount.Columns["Status"].HeaderText = "Trạng thái";
                dgvAccount.Columns["Roles"].HeaderText = "Vai trò";
                dgvAccount.Columns["Id"].Width = 40;
                dgvAccount.Columns["Username"].Width = 120;
                dgvAccount.Columns["FullName"].Width = 150;
                dgvAccount.Columns["Email"].Width = 200;
                dgvAccount.Columns["Status"].Width = 100;
                dgvAccount.Columns["Roles"].Width = 150;
                dgvAccount.Columns["Roles"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadData();
        }
        private void ClearForm()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtFullname.Text = "";
            txtEmail.Text = "";
            currentUserId = 0;

            if (cbRole.Items.Count > 0)
                cbRole.SelectedIndex = 0; // Chọn item mặc định
        }

        private void btn_exportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvAccount);
        }
        private void ExportToExcel(DataGridView dgv)
        {
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Danh sách tài khoản");

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dgv.Columns[i].HeaderText;
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = dgv.Rows[i].Cells[j].Value?.ToString();
                    }
                }
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    Title = "Lưu file Excel"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var stream = File.Create(saveDialog.FileName))
                    {
                        package.SaveAs(stream);
                    }
                    MessageBox.Show("Xuất file Excel thành công!");
                }
            }
        }

        public class RoleItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
        private void dgvAccount_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAccount.SelectedRows.Count > 0)
            {
                var row = dgvAccount.SelectedRows[0];
                currentUserId = Convert.ToInt32(row.Cells["Id"].Value);
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtFullname.Text = row.Cells["FullName"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();

                // Reset combobox về item mặc định
                if (cbRole.Items.Count > 0)
                    cbRole.SelectedIndex = 0;

                LoadUserRoles(currentUserId);
            }
        }
        private void LoadUserRoles(int userId)
        {
            try
            {
                var userRoles = _userRoleBus.LayUserRolesTheoUserId(userId);
                
                // Nếu user có roles, chọn role đầu tiên trong combobox
                // Lưu ý: ComboBox chỉ hiển thị 1 role, nhưng vẫn có thể gán nhiều roles cho user
                if (userRoles != null && userRoles.Count > 0)
                {
                    var firstRole = userRoles.FirstOrDefault();
                    if (firstRole != null)
                    {
                        for (int i = 0; i < cbRole.Items.Count; i++)
                        {
                            var item = cbRole.Items[i] as RoleItem;
                            if (item != null && item.Id == firstRole.RoleId)
                            {
                                cbRole.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // Không có role nào, chọn item mặc định
                    if (cbRole.Items.Count > 0)
                        cbRole.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_addUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;
                string fullName = txtFullname.Text;
                bool isActive = true;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName))
                {
                    MessageBox.Show("Vui lòng nhập Username và Họ tên!");
                    return;
                }

                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu!");
                    return;
                }

                // Kiểm tra trùng username
                var existing = _userBus.LayUserTheoUsername(username);
                if (existing != null)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!");
                    return;
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

                var user = new DAL.Models.Users
                {
                    Username = username,
                    PasswordHash = passwordHash,
                    FullName = fullName,
                    Email = txtEmail.Text,
                    IsActive = isActive,
                    CreatedAt = DateTime.Now
                };

                _userBus.ThemUser(user);

                // Lấy lại user vừa tạo để có ID
                var newUser = _userBus.LayUserTheoUsername(username);
                if (newUser != null)
                {
                    // Gán role cho user (ComboBox chỉ chọn được 1 role)
                    var selectedItem = cbRole.SelectedItem as RoleItem;
                    if (selectedItem != null && selectedItem.Id > 0) // Bỏ qua item mặc định (Id = 0)
                    {
                        _userRoleBus.GanRoleChoUser(newUser.Id, selectedItem.Id);
                    }
                }

                LoadData();
                ClearForm();
                MessageBox.Show("Thêm mới thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentUserId == 0)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản để xóa!");
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Xóa tất cả roles của user trước
                    _userRoleBus.XoaTatCaRoleCuaUser(currentUserId);
                    // Sau đó xóa user
                    _userBus.XoaUser(currentUserId);

                    LoadData();
                    ClearForm();
                    MessageBox.Show("Xóa thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentUserId == 0)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản để sửa!");
                    return;
                }

                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;
                string fullName = txtFullname.Text;
                bool isActive = true;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName))
                {
                    MessageBox.Show("Vui lòng nhập Username và Họ tên!");
                    return;
                }

                // Kiểm tra trùng username (trừ user hiện tại)
                var existing = _userBus.LayUserTheoUsername(username);
                if (existing != null && existing.Id != currentUserId)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!");
                    return;
                }

                // Lấy user hiện tại
                var user = _userBus.LayUserTheoId(currentUserId);
                if (user == null)
                {
                    MessageBox.Show("Không tìm thấy tài khoản!");
                    return;
                }

                // Cập nhật thông tin
                user.Username = username;
                user.FullName = fullName;
                user.Email = txtEmail.Text;
                user.IsActive = isActive;

                // Nếu có nhập password mới thì cập nhật
                if (!string.IsNullOrEmpty(password))
                {
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                }

                _userBus.CapNhatUser(user);

                // Xóa tất cả roles cũ và gán lại
                _userRoleBus.XoaTatCaRoleCuaUser(currentUserId);
                
                // Gán role mới (ComboBox chỉ chọn được 1 role)
                var selectedItem = cbRole.SelectedItem as RoleItem;
                if (selectedItem != null && selectedItem.Id > 0) // Bỏ qua item mặc định (Id = 0)
                {
                    _userRoleBus.GanRoleChoUser(currentUserId, selectedItem.Id);
                }

                LoadData();
                ClearForm();
                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
