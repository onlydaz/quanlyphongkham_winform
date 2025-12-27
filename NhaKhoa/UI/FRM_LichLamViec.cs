using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using NhaKhoa.BUS;
using NhaKhoa.Models;

namespace NhaKhoa.UI
{
    public partial class FRM_LichLamViec : Form
    {
        private readonly LichBacSiBUS _bus = new LichBacSiBUS();
        private int _loggedInUserId;

        public FRM_LichLamViec(int loggedInUserId = 0)
        {
            _loggedInUserId = loggedInUserId;
            InitializeComponent();
            // Ensure the name label is visible at runtime
            try
            {
                if (labelCurrentBacSi != null) labelCurrentBacSi.Visible = false;
                if (lblCurrentBacSiValue != null) lblCurrentBacSiValue.Visible = true;
            }
            catch { }
            PopulateDayAndHours();
            LoadCurrentBacSi();
            LoadGrid();
        }

        private void PopulateDayAndHours()
        {
            var days = new[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            cboNgayTrongTuan.Items.Clear();
            foreach (var d in days) cboNgayTrongTuan.Items.Add(d);
            cboNgayTrongTuan.SelectedIndex = 1;

            cboGioBD.Items.Clear();
            cboGioKT.Items.Clear();
            // Use 0..23 hours and display as HH:mm:ss (e.g. 08:00:00)
            for (int h = 0; h < 24; h++)
            {
                var ts = TimeSpan.FromHours(h);
                var text = ts.ToString(); // "08:00:00"
                cboGioBD.Items.Add(text);
                cboGioKT.Items.Add(text);
            }

            // Default: 08:00 and 17:00
            cboGioBD.SelectedIndex = 8;
            cboGioKT.SelectedIndex = 17;
        }

        private void LoadCurrentBacSi()
        {
            try
            {
                if (_loggedInUserId <= 0)
                {
                    lblCurrentBacSiValue.Text = string.Empty;
                    return;
                }

                using (var ctx = new DAL.NhaKhoaContext())
                {
                    var nv = ctx.NhanViens.FirstOrDefault(n => n.UserId == _loggedInUserId);
                    if (nv != null)
                    {
                        lblCurrentBacSiValue.Text = nv.TenNV;
                    }
                    else
                    {
                        lblCurrentBacSiValue.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateCurrentBacSiLabel()
        {
            try
            {
                if (lblCurrentBacSiValue == null) return;
                // label is populated in LoadCurrentBacSi
            }
            catch { }
        }

        private void LoadGrid()
        {
            try
            {
                var all = _bus.LayTatCa();
                // If the current user is a doctor, restrict the grid to that doctor's schedules
                string currentMaBacSi = null;
                bool isDoctor = false;
                if (_loggedInUserId > 0)
                {
                    using (var ctx = new DAL.NhaKhoaContext())
                    {
                        var nv = ctx.NhanViens.FirstOrDefault(n => n.UserId == _loggedInUserId);
                        if (nv != null)
                        {
                            currentMaBacSi = nv.MaNV;
                            isDoctor = ctx.UserRoles.Any(ur => ur.UserId == _loggedInUserId && (
                                ur.Role.Name.ToLower().Contains("bacsi") || ur.Role.Name.ToLower().Contains("doctor")
                            ));
                        }
                    }
                }
                var dt = new DataTable();
                // Only show relevant columns for doctors: Ngay, Tu, Den, GhiChu
                dt.Columns.Add("Ngay", typeof(string));
                dt.Columns.Add("Tu", typeof(string));
                dt.Columns.Add("Den", typeof(string));
                dt.Columns.Add("GhiChu", typeof(string));

                foreach (var l in all)
                {
                    if (isDoctor && !string.IsNullOrEmpty(currentMaBacSi) && l.MaBacSi != currentMaBacSi)
                        continue;

                    var tu = l.GioBatDau.ToString();
                    var den = l.GioKetThuc.ToString();
                    dt.Rows.Add(DayName(l.NgayTrongTuan), tu, den, l.GhiChu);
                }

                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string DayName(byte d)
        {
            var days = new[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            if (d <= 6) return days[d];
            return d.ToString();
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string maBacSi = null;
                if (_loggedInUserId > 0)
                {
                    using (var ctx = new DAL.NhaKhoaContext())
                    {
                        var nv = ctx.NhanViens.FirstOrDefault(n => n.UserId == _loggedInUserId);
                        if (nv != null) maBacSi = nv.MaNV;
                    }
                }

                if (string.IsNullOrEmpty(maBacSi))
                {
                    MessageBox.Show("Không xác định được mã bác sĩ.");
                    return;
                }


                if (cboGioBD.SelectedItem == null || cboGioKT.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn giờ bắt đầu và giờ kết thúc.");
                    return;
                }

                var startTs = TimeSpan.Parse(cboGioBD.SelectedItem.ToString());
                var endTs = TimeSpan.Parse(cboGioKT.SelectedItem.ToString());

                if (startTs > endTs)
                {
                    MessageBox.Show("Giờ bắt đầu phải nhỏ hơn hoặc bằng giờ kết thúc.");
                    return;
                }

                var lich = new LichBacSi
                {
                    MaBacSi = maBacSi,
                    NgayTrongTuan = (byte)cboNgayTrongTuan.SelectedIndex,
                    GioBatDau = startTs,
                    GioKetThuc = endTs,
                    GhiChu = txtGhiChu.Text,
                    IsActive = true,
                    NguoiTao = Environment.UserName
                };

                _bus.Them(lich);
                MessageBox.Show("Lưu thành công");
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
