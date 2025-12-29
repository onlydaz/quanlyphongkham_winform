using System;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Forms;
using System.Data.Entity.Validation;
using NhaKhoa.BUS;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.Letan
{
    public partial class FRM_Letan : Form
    {
        private readonly NhaKhoa.BUS.LichBacSiBUS _lichBus;
        private readonly NhaKhoaContext _ctx;

        public FRM_Letan()
        {
            InitializeComponent();
            _lichBus = new NhaKhoa.BUS.LichBacSiBUS();
            _ctx = new NhaKhoaContext();

            // Initialize booking UI
            try
            {
                LoadDoctors();
                LoadTimeSlots();
                LoadDanhSachBenhNhan();
            }
            catch { /* ignore UI load errors */ }

            // Wire up button click for "Thêm mới lịch hẹn" (button4)
            this.button4.Click -= Button4_Click; // ensure not double-wired
            this.button4.Click += Button4_Click;

            // Wire up selection changed to populate fields for edit
            if (this.dgvDSBN != null)
            {
                this.dgvDSBN.SelectionChanged -= DgvDSBN_SelectionChanged;
                this.dgvDSBN.SelectionChanged += DgvDSBN_SelectionChanged;
            }

            // Wire up edit and delete buttons
            if (this.button2 != null)
            {
                this.button2.Click -= Button2_EditAppointment_Click;
                this.button2.Click += Button2_EditAppointment_Click;
            }
            if (this.button3 != null)
            {
                this.button3.Click -= Button3_DeleteAppointment_Click;
                this.button3.Click += Button3_DeleteAppointment_Click;
            }
        }

        private void LoadDoctors()
        {
            // Load only staff whose linked user has a doctor role
            var doctors = _ctx.NhanViens
                .Where(nv => nv.UserId != null && _ctx.UserRoles
                    .Any(ur => ur.UserId == nv.UserId && (
                        ur.Role.Name.ToLower().Contains("bacsi") ||
                        ur.Role.Name.ToLower().Contains("doctor")
                    )))
                .OrderBy(n => n.TenNV)
                .ToList();

            // If control not present (older UI), skip
            if (this.cboBacSi != null)
            {
                cboBacSi.DisplayMember = "TenNV";
                cboBacSi.ValueMember = "MaNV";
                cboBacSi.DataSource = doctors;
            }
        }

        private void LoadTimeSlots()
        {
            if (this.cboGioBD == null || this.cboGioKT == null) return;
            cboGioBD.Items.Clear();
            cboGioKT.Items.Clear();
            // Hourly slots from 07:00 (start) to 24:00 (end). Start hours: 7..23, end = start+1 (8..24)
            for (int h = 7; h <= 23; h++)
            {
                var start = TimeSpan.FromHours(h);
                TimeSpan end;
                if (h < 23)
                    end = TimeSpan.FromHours(h + 1);
                else
                    // avoid 24:00:00 which is invalid for SQL TIME; use 23:59:59 as last-slot end
                    end = TimeSpan.FromSeconds(23 * 3600 + 59 * 60 + 59);

                cboGioBD.Items.Add(start.ToString());
                cboGioKT.Items.Add(end.ToString());
            }
            if (cboGioBD.Items.Count > 0) cboGioBD.SelectedIndex = 0;
            if (cboGioKT.Items.Count > 0) cboGioKT.SelectedIndex = 0;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            // Booking flow: read inputs from FRM_Letan UI and save LamSan after checks
            try
            {
                // MaBN is in textBox1
                string maBN = this.textBox1?.Text?.Trim();

                if (cboBacSi == null || cboGioBD == null || cboGioKT == null)
                {
                    MessageBox.Show("Giao diện chưa có các điều khiển đặt lịch.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboBacSi.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn bác sĩ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var maBacSi = cboBacSi.SelectedValue.ToString();
                var date = dateTimePicker2.Value.Date; // reuse existing control

                if (!TimeSpan.TryParse(cboGioBD.SelectedItem?.ToString(), out var gioBD) ||
                    !TimeSpan.TryParse(cboGioKT.SelectedItem?.ToString(), out var gioKT))
                {
                    MessageBox.Show("Khung giờ không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (gioKT <= gioBD)
                {
                    MessageBox.Show("Giờ kết thúc phải lớn hơn giờ bắt đầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check doctor's working schedule
                // Model stores weekday with either 0=Sunday..6=Saturday or 1=Monday..7=Sunday.
                byte dow0 = (byte)date.DayOfWeek; // 0=Sunday..6=Saturday
                byte dow1 = dow0 == 0 ? (byte)7 : dow0; // 1=Monday..7=Sunday
                var schedules = _lichBus.LayTheoBacSi(maBacSi);
                var matched = schedules.Any(s => (s.NgayTrongTuan == dow0 || s.NgayTrongTuan == dow1)
                                                 && gioBD >= s.GioBatDau && gioKT <= s.GioKetThuc);
                if (!matched)
                {
                    MessageBox.Show("Bác sĩ không làm việc vào khung giờ đã chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check detailed overlap with existing LamSan for the same doctor and date
                var conflict = _ctx.LamSans.Any(l => l.MaNV == maBacSi
                    && System.Data.Entity.DbFunctions.TruncateTime(l.NgayKham) == date
                    && (
                        l.GioBatDau == null || l.GioKetThuc == null
                        || !(gioKT <= l.GioBatDau || gioBD >= l.GioKetThuc)
                    )
                );

                if (conflict)
                {
                    MessageBox.Show("Đã có lịch khám trùng giờ với bác sĩ này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Save LamSan and ensure patient exists/updated in BENHNHAN
                // Ensure MaLS fits DB nvarchar(10)
                var maLS = Guid.NewGuid().ToString("N").Substring(0, 10);
                maBN = string.IsNullOrWhiteSpace(maBN) ? null : maBN;

                var lamsan = new LamSan
                {
                    MaLS = maLS,
                    MaBN = maBN,
                    NgayKham = date,
                    MaNV = maBacSi,
                    TrieuChung = richTextBox1?.Text,
                    GioBatDau = gioBD,
                    GioKetThuc = gioKT
                };

                // Pre-validate string lengths based on model's StringLength attributes
                var tooLong = new StringBuilder();
                foreach (var prop in typeof(LamSan).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (prop.PropertyType != typeof(string)) continue;
                    var val = prop.GetValue(lamsan) as string;
                    if (string.IsNullOrEmpty(val)) continue;
                    var sla = prop.GetCustomAttribute<StringLengthAttribute>();
                    if (sla != null && val.Length > sla.MaximumLength)
                    {
                        tooLong.AppendLine($"{prop.Name}: {val.Length} > {sla.MaximumLength}");
                    }
                }
                if (tooLong.Length > 0)
                {
                    MessageBox.Show("Một hoặc nhiều trường vượt kích thước mô tả:\n" + tooLong.ToString(), "Lỗi trước khi lưu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    var sqlLog = new StringBuilder();
                    using (var ctx = new NhaKhoaContext())
                    {
                        // Ensure patient info is saved in BENHNHAN
                        if (string.IsNullOrWhiteSpace(maBN))
                        {
                            MessageBox.Show("Vui lòng nhập Mã bệnh nhân.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var bn = ctx.BenhNhans.SingleOrDefault(b => b.MaBN == maBN);

                        // Gather patient fields from UI
                        var ten = textBox3?.Text?.Trim();
                        var sdt = textBox2?.Text?.Trim();
                        var diachi = textBox4?.Text?.Trim();
                        var namsinh = dateTimePicker1?.Value.Year ?? 0;
                        var gioitinh = radioButton1 != null && radioButton1.Checked ? "Nam" : "Nu";
                        var ngayKhamBn = date;
                        var lyDo = richTextBox1?.Text;

                        if (bn == null)
                        {
                            // Insert new patient
                            bn = new NhaKhoa.DAL.Models.BenhNhan
                            {
                                MaBN = maBN,
                                TenBN = ten,
                                SDT = sdt,
                                DiaChi = diachi,
                                NamSinh = namsinh,
                                GioiTinh = gioitinh,
                                NgayKham = ngayKhamBn,
                                LyDoKham = lyDo,
                                TrangThai = "Chờ khám"
                            };
                            ctx.BenhNhans.Add(bn);
                        }
                        else
                        {
                            // Existing MaBN: ask confirmation to update patient info
                            var res = MessageBox.Show($"Mã bệnh nhân '{maBN}' đã tồn tại. Cập nhật thông tin bệnh nhân hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.No)
                            {
                                return;
                            }
                            bn.TenBN = ten;
                            bn.SDT = sdt;
                            bn.DiaChi = diachi;
                            bn.NamSinh = namsinh;
                            bn.GioiTinh = gioitinh;
                            bn.NgayKham = ngayKhamBn;
                            bn.LyDoKham = lyDo;
                        }

                        // Capture EF SQL for diagnosis
                        ctx.Database.Log = s => sqlLog.Append(s);
                        // Add the appointment
                        ctx.LamSans.Add(lamsan);
                        ctx.SaveChanges();
                    }
                        MessageBox.Show("Đặt lịch thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Refresh patient list shown to receptionist
                        try { LoadDanhSachBenhNhan(); } catch { }
                }
                catch (DbEntityValidationException dbex)
                {
                    var sb = new StringBuilder();
                    foreach (var eve in dbex.EntityValidationErrors)
                    {
                        sb.AppendLine("Entity of type '" + eve.Entry.Entity.GetType().Name + "' in state '" + eve.Entry.State + "' has the following validation errors:");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sb.AppendLine("- " + ve.PropertyName + ": " + ve.ErrorMessage);
                        }
                    }
                    MessageBox.Show("Validation failed:\n" + sb.ToString(), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Try to find inner DbEntityValidationException if wrapped
                    var inner = ex;
                    DbEntityValidationException found = null;
                    while (inner != null)
                    {
                        if (inner is DbEntityValidationException dve)
                        {
                            found = dve;
                            break;
                        }
                        inner = inner.InnerException;
                    }
                    if (found != null)
                    {
                        var sb = new StringBuilder();
                        foreach (var eve in found.EntityValidationErrors)
                        {
                            sb.AppendLine("Entity of type '" + eve.Entry.Entity.GetType().Name + "' in state '" + eve.Entry.State + "' has the following validation errors:");
                            foreach (var ve in eve.ValidationErrors)
                            {
                                sb.AppendLine("- " + ve.PropertyName + ": " + ve.ErrorMessage);
                            }
                        }
                        MessageBox.Show("Validation failed (inner):\n" + sb.ToString(), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Show full exception details to help debug inner SQL/EF errors
                        MessageBox.Show("Lỗi khi đặt lịch:\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (DbEntityValidationException dbex)
            {
                var msgs = dbex.EntityValidationErrors
                    .SelectMany(ev => ev.ValidationErrors)
                    .Select(ve => ve.PropertyName + ": " + ve.ErrorMessage);
                MessageBox.Show("Validation failed:\n" + string.Join("\n", msgs), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Outer catch: show full exception stack for diagnosis
                MessageBox.Show("Lỗi khi đặt lịch:\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachBenhNhan()
        {
            try
            {
                // Build a DataTable matching designer columns: MaBN, TenBN, GioiTinh, NamSinh, SDT, DiaChi, NgayTao, BacSi, GioKham, Column1 (GhiChu)
                var dt = new System.Data.DataTable();
                dt.Columns.Add("MaBN", typeof(string));
                dt.Columns.Add("TenBN", typeof(string));
                dt.Columns.Add("GioiTinh", typeof(string));
                dt.Columns.Add("NamSinh", typeof(int));
                dt.Columns.Add("SDT", typeof(string));
                dt.Columns.Add("DiaChi", typeof(string));
                dt.Columns.Add("NgayTao", typeof(string));
                dt.Columns.Add("BacSi", typeof(string));
                dt.Columns.Add("GioKham", typeof(string));
                dt.Columns.Add("GhiChu", typeof(string));

                using (var ctx = new NhaKhoaContext())
                {
                    // Only include MaBNs that have at least one LamSan (appointments)
                    var grouped = ctx.LamSans
                        .Where(l => !string.IsNullOrEmpty(l.MaBN))
                        .GroupBy(l => l.MaBN)
                        .Select(g => new { MaBN = g.Key, Latest = g.OrderByDescending(x => x.NgayKham).FirstOrDefault() })
                        .ToList();

                    foreach (var g in grouped)
                    {
                        var b = ctx.BenhNhans.FirstOrDefault(x => x.MaBN == g.MaBN);
                        if (b == null) continue; // skip if patient record missing

                        var latest = g.Latest;
                        string bacSiName = string.Empty;
                        string gioKham = string.Empty;
                        string ngayKhamStr = latest?.NgayKham?.ToString("dd/MM/yyyy") ?? (b.NgayKham != default(DateTime) ? b.NgayKham.ToString("dd/MM/yyyy") : string.Empty);

                        if (latest != null)
                        {
                            var nv = ctx.NhanViens.FirstOrDefault(n => n.MaNV == latest.MaNV);
                            bacSiName = nv?.TenNV ?? latest.MaNV;
                            var tu = latest.GioBatDau?.ToString() ?? string.Empty;
                            var den = latest.GioKetThuc?.ToString() ?? string.Empty;
                            gioKham = string.IsNullOrEmpty(tu) ? den : (tu + " - " + den);
                        }

                        dt.Rows.Add(b.MaBN, b.TenBN, b.GioiTinh, b.NamSinh, b.SDT, b.DiaChi, ngayKhamStr, bacSiName, gioKham, b.LyDoKham);
                    }
                }

                dgvDSBN.AutoGenerateColumns = false;
                // Ensure each designer column maps to a DataTable column
                foreach (DataGridViewColumn col in dgvDSBN.Columns)
                {
                    if (string.IsNullOrWhiteSpace(col.DataPropertyName)) col.DataPropertyName = col.Name;
                }
                dgvDSBN.DataSource = dt;
                // Select first row if available
                if (dgvDSBN.Rows.Count > 0)
                {
                    dgvDSBN.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bệnh nhân: " + ex.Message);
            }
        }

        private void DgvDSBN_SelectionChanged(object sender, EventArgs e)
        {
            PopulateFieldsFromSelectedRow();
        }

        private void PopulateFieldsFromSelectedRow()
        {
            try
            {
                if (dgvDSBN.SelectedRows.Count == 0) return;
                var row = dgvDSBN.SelectedRows[0];
                var maBN = row.Cells["MaBN"].Value?.ToString();
                if (string.IsNullOrEmpty(maBN)) return;

                // Fill patient fields
                textBox1.Text = maBN;
                textBox3.Text = row.Cells["TenBN"].Value?.ToString() ?? string.Empty;
                textBox2.Text = row.Cells["SDT"].Value?.ToString() ?? string.Empty;
                textBox4.Text = row.Cells["DiaChi"].Value?.ToString() ?? string.Empty;
                if (int.TryParse(row.Cells["NamSinh"].Value?.ToString(), out int ns))
                {
                    dateTimePicker1.Value = new DateTime(Math.Max(1900, ns), 1, 1);
                }
                var gt = row.Cells["GioiTinh"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(gt) && gt.ToLower().StartsWith("n"))
                {
                    radioButton2.Checked = true; // Nữ
                }
                else
                {
                    radioButton1.Checked = true; // Nam
                }

                // Appointment-specific: load latest appointment for this patient
                using (var ctx = new NhaKhoaContext())
                {
                    var latest = ctx.LamSans.Where(l => l.MaBN == maBN).OrderByDescending(l => l.NgayKham).FirstOrDefault();
                    if (latest != null)
                    {
                        dateTimePicker2.Value = latest.NgayKham ?? DateTime.Now;
                        // set doctor selection if present
                        try
                        {
                            cboBacSi.SelectedValue = latest.MaNV;
                        }
                        catch { }
                        if (latest.GioBatDau.HasValue)
                        {
                            cboGioBD.SelectedItem = latest.GioBatDau.Value.ToString();
                        }
                        if (latest.GioKetThuc.HasValue)
                        {
                            cboGioKT.SelectedItem = latest.GioKetThuc.Value.ToString();
                        }
                        richTextBox1.Text = latest.TrieuChung ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                // don't break selection on minor errors
                Console.WriteLine(ex.Message);
            }
        }

        private void Button2_EditAppointment_Click(object sender, EventArgs e)
        {
            // Edit the latest appointment for selected patient
            if (dgvDSBN.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bệnh nhân để sửa lịch hẹn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var maBN = dgvDSBN.SelectedRows[0].Cells["MaBN"].Value?.ToString();
            if (string.IsNullOrEmpty(maBN)) return;

            try
            {
                using (var ctx = new NhaKhoaContext())
                {
                    var latest = ctx.LamSans.Where(l => l.MaBN == maBN).OrderByDescending(l => l.NgayKham).FirstOrDefault();
                    if (latest == null)
                    {
                        MessageBox.Show("Không tìm thấy lịch hẹn để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Update fields from UI
                    latest.NgayKham = dateTimePicker2.Value.Date;
                    if (TimeSpan.TryParse(cboGioBD.SelectedItem?.ToString(), out var bd)) latest.GioBatDau = bd;
                    if (TimeSpan.TryParse(cboGioKT.SelectedItem?.ToString(), out var kt)) latest.GioKetThuc = kt;
                    if (cboBacSi.SelectedValue != null) latest.MaNV = cboBacSi.SelectedValue.ToString();
                    latest.TrieuChung = richTextBox1.Text;

                    // Update patient info
                    var bn = ctx.BenhNhans.SingleOrDefault(b => b.MaBN == maBN);
                    if (bn != null)
                    {
                        bn.TenBN = textBox3.Text;
                        bn.SDT = textBox2.Text;
                        bn.DiaChi = textBox4.Text;
                        bn.NamSinh = dateTimePicker1.Value.Year;
                        bn.GioiTinh = radioButton2.Checked ? "Nu" : "Nam";
                        bn.LyDoKham = richTextBox1.Text;
                    }

                    ctx.SaveChanges();
                }

                MessageBox.Show("Sửa lịch hẹn thành công.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachBenhNhan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa lịch hẹn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button3_DeleteAppointment_Click(object sender, EventArgs e)
        {
            // Delete latest appointment for selected patient after confirmation
            if (dgvDSBN.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bệnh nhân để xoá lịch hẹn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var maBN = dgvDSBN.SelectedRows[0].Cells["MaBN"].Value?.ToString();
            if (string.IsNullOrEmpty(maBN)) return;

            if (MessageBox.Show("Bạn có chắc muốn xoá lịch hẹn mới nhất của bệnh nhân này?","Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                using (var ctx = new NhaKhoaContext())
                {
                    var latest = ctx.LamSans.Where(l => l.MaBN == maBN).OrderByDescending(l => l.NgayKham).FirstOrDefault();
                    if (latest == null)
                    {
                        MessageBox.Show("Không tìm thấy lịch hẹn để xoá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    ctx.LamSans.Remove(latest);
                    ctx.SaveChanges();
                }

                MessageBox.Show("Xoá lịch hẹn thành công.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachBenhNhan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xoá lịch hẹn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
