using System;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Forms;
using System.Data.Entity.Validation;
using NhaKhoa.BUS;
using NhaKhoa.DAL;

namespace NhaKhoa.Letan
{
    public partial class FRM_Letan : Form
    {
        private readonly LichBacSiBUS _lichBus;
        private readonly NhaKhoaContext _ctx;

        public FRM_Letan()
        {
            InitializeComponent();
            _lichBus = new LichBacSiBUS();
            _ctx = new NhaKhoaContext();

            // Initialize booking UI
            try
            {
                LoadDoctors();
                LoadTimeSlots();
            }
            catch { /* ignore UI load errors */ }

            // Wire up button click for "Thêm mới lịch hẹn" (button4)
            this.button4.Click -= Button4_Click; // ensure not double-wired
            this.button4.Click += Button4_Click;
        }

        private void LoadDoctors()
        {
            // Load all staff ordered by name; filtering by job code is removed (kept simple)
            var doctors = _ctx.NhanViens.OrderBy(n => n.TenNV).ToList();

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

                // Save LamSan using a short-lived context to avoid unrelated tracked-entity validation
                // Ensure MaLS fits DB nvarchar(10)
                var maLS = Guid.NewGuid().ToString("N").Substring(0, 10);
                maBN = string.IsNullOrWhiteSpace(maBN) ? null : maBN;

                var lamsan = new Models.LamSan
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
                foreach (var prop in typeof(Models.LamSan).GetProperties(BindingFlags.Public | BindingFlags.Instance))
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
                        // Capture EF SQL for diagnosis
                        ctx.Database.Log = s => sqlLog.Append(s);
                        ctx.LamSans.Add(lamsan);
                        ctx.SaveChanges();
                    }
                    MessageBox.Show("Đặt lịch thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
