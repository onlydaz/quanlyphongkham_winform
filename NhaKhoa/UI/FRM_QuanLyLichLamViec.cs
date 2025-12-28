using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using NhaKhoa.BUS;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.UI
{
    public partial class FRM_QuanLyLichLamViec : Form
    {
        private readonly NhaKhoa.BUS.LichBacSiBUS _bus = new NhaKhoa.BUS.LichBacSiBUS();
        public FRM_QuanLyLichLamViec()
        {
            InitializeComponent();
            PopulateDayAndHours();
            LoadBacSiFilter();
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
            for (int h = 0; h < 24; h++)
            {
                var ts = TimeSpan.FromHours(h);
                var text = ts.ToString();
                cboGioBD.Items.Add(text);
                cboGioKT.Items.Add(text);
            }

            cboGioBD.SelectedIndex = 8;
            cboGioKT.SelectedIndex = 17;
        }

        private void LoadSelectedFromGrid()
        {
            if (dgv.CurrentRow == null) return;
            try
            {
                var cells = dgv.CurrentRow.Cells;
                var idObj = cells["Id"].Value;
                if (idObj == null) return;
                int id = Convert.ToInt32(idObj);
                var lich = _bus.LayTheoId(id);
                if (lich == null) return;

                // select doctor in filter combo if present
                if (!string.IsNullOrEmpty(lich.MaBacSi))
                {
                    for (int i = 0; i < cboBacSiFilter.Items.Count; i++)
                    {
                        var it = cboBacSiFilter.Items[i];
                        var p = it.GetType().GetProperty("Ma");
                        if (p == null) continue;
                        var ma = (string)p.GetValue(it);
                        if (ma == lich.MaBacSi)
                        {
                            cboBacSiFilter.SelectedIndex = i;
                            break;
                        }
                    }
                }

                cboNgayTrongTuan.SelectedIndex = lich.NgayTrongTuan;
                cboGioBD.SelectedItem = lich.GioBatDau.ToString();
                cboGioKT.SelectedItem = lich.GioKetThuc.ToString();
                txtGhiChu.Text = lich.GhiChu;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadBacSiFilter()
        {
            try
            {
                using (var ctx = new DAL.NhaKhoaContext())
                {
                    // Load only employees who are linked to a user that has a doctor role
                    var doctors = ctx.NhanViens
                        .Where(nv => nv.UserId != null && ctx.UserRoles
                            .Any(ur => ur.UserId == nv.UserId && (
                                ur.Role.Name.ToLower().Contains("bacsi") ||
                                ur.Role.Name.ToLower().Contains("doctor")
                            )))
                        .ToList();

                    var items = (new[] { new { Ma = string.Empty, Ten = "-- Tất cả bác sĩ --" } })
                        .Concat(doctors.Select(nv => new { Ma = nv.MaNV, Ten = nv.TenNV }))
                        .ToList();

                    cboBacSiFilter.DataSource = items;
                    cboBacSiFilter.DisplayMember = "Ten";
                    cboBacSiFilter.ValueMember = "Ma";
                    cboBacSiFilter.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadGrid()
        {
            try
            {
                var all = _bus.LayTatCa();
                var dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("BacSi", typeof(string));
                dt.Columns.Add("Ngay", typeof(string));
                dt.Columns.Add("Tu", typeof(string));
                dt.Columns.Add("Den", typeof(string));
                dt.Columns.Add("GhiChu", typeof(string));

                string filterMa = string.Empty;
                if (cboBacSiFilter.SelectedItem != null)
                {
                    var p = cboBacSiFilter.SelectedItem.GetType().GetProperty("Ma");
                    filterMa = (string)p.GetValue(cboBacSiFilter.SelectedItem);
                }

                foreach (var l in all)
                {
                    if (!string.IsNullOrEmpty(filterMa) && l.MaBacSi != filterMa) continue;
                    dt.Rows.Add(l.Id, l.BacSi?.TenNV ?? l.MaBacSi, DayName(l.NgayTrongTuan), l.GioBatDau.ToString(), l.GioKetThuc.ToString(), l.GhiChu);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadSelectedFromGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = null;
                if (cboBacSiFilter.SelectedItem != null)
                {
                    var p = cboBacSiFilter.SelectedItem.GetType().GetProperty("Ma");
                    ma = (string)p.GetValue(cboBacSiFilter.SelectedItem);
                }

                if (string.IsNullOrEmpty(ma))
                {
                    MessageBox.Show("Vui lòng chọn một bác sĩ để thêm lịch.");
                    return;
                }

                if (cboGioBD.SelectedItem == null || cboGioKT.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn giờ.");
                    return;
                }

                var lich = new LichBacSi
                {
                    MaBacSi = ma,
                    NgayTrongTuan = (byte)cboNgayTrongTuan.SelectedIndex,
                    GioBatDau = TimeSpan.Parse(cboGioBD.SelectedItem.ToString()),
                    GioKetThuc = TimeSpan.Parse(cboGioKT.SelectedItem.ToString()),
                    GhiChu = txtGhiChu.Text,
                    IsActive = true,
                    NguoiTao = Environment.UserName
                };

                _bus.Them(lich);
                MessageBox.Show("Thêm thành công.");
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.CurrentRow == null) {
                    MessageBox.Show("Vui lòng chọn 1 dòng để sửa.");
                    return;
                }

                var idObj = dgv.CurrentRow.Cells["Id"].Value;
                if (idObj == null) return;
                int id = Convert.ToInt32(idObj);
                var lich = _bus.LayTheoId(id);
                if (lich == null) return;

                string ma = null;
                if (cboBacSiFilter.SelectedItem != null)
                {
                    var p = cboBacSiFilter.SelectedItem.GetType().GetProperty("Ma");
                    ma = (string)p.GetValue(cboBacSiFilter.SelectedItem);
                }

                if (string.IsNullOrEmpty(ma))
                {
                    MessageBox.Show("Vui lòng chọn bác sĩ.");
                    return;
                }

                lich.MaBacSi = ma;
                lich.NgayTrongTuan = (byte)cboNgayTrongTuan.SelectedIndex;
                lich.GioBatDau = TimeSpan.Parse(cboGioBD.SelectedItem.ToString());
                lich.GioKetThuc = TimeSpan.Parse(cboGioKT.SelectedItem.ToString());
                lich.GhiChu = txtGhiChu.Text;

                _bus.CapNhat(lich);
                MessageBox.Show("Cập nhật thành công.");
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.CurrentRow == null) {
                    MessageBox.Show("Vui lòng chọn 1 dòng để xóa.");
                    return;
                }

                var idObj = dgv.CurrentRow.Cells["Id"].Value;
                if (idObj == null) return;
                int id = Convert.ToInt32(idObj);

                var r = MessageBox.Show("Bạn có chắc muốn xóa lịch này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r != DialogResult.Yes) return;

                _bus.Xoa(id);
                MessageBox.Show("Xóa thành công.");
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
