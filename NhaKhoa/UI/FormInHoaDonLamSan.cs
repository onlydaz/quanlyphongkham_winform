using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NhaKhoa.BUS;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.UI
{
    public partial class FormInHoaDonLamSan : Form
    {
        private string _maBN;

        public FormInHoaDonLamSan(string maBN)
        {
            InitializeComponent();
            _maBN = maBN;
        }

        private void FormInHoaDonLamSan_Load(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin bệnh nhân
                var benhNhanBus = new BenhNhanBUS();
                var benhNhan = benhNhanBus.LayBenhNhanTheoMa(_maBN);

                if (benhNhan == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin bệnh nhân!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Lấy danh sách lâm sàn từ database
                List<object> hoaDonData = new List<object>();
                using (var ctx = new NhaKhoaContext())
                {
                    var query = from ls in ctx.LamSans.AsNoTracking()
                                where ls.MaBN == _maBN
                                join cd in ctx.ChanDoans.AsNoTracking() on ls.MaCD equals cd.MaCD into cdGroup
                                from cd in cdGroup.DefaultIfEmpty()
                                join dt in ctx.DieuTris.AsNoTracking() on ls.MaDT equals dt.MaDT into dtGroup
                                from dt in dtGroup.DefaultIfEmpty()
                                select new
                                {
                                    MaLS = ls.MaLS ?? "",
                                    TenChuanDoan = cd != null ? cd.TenChuanDoan : "",
                                    TenDieuTri = dt != null ? dt.TenDieuTri : "",
                                    DonViTinh = dt != null ? dt.DonViTinh : "",
                                    DonGia = dt != null ? dt.DonGia : (decimal?)0,
                                    NgayKham = ls.NgayKham ?? DateTime.Now,
                                    TrieuChung = ls.TrieuChung ?? ""
                                };

                    hoaDonData = query.ToList<object>();
                }

                // Thiết lập file rdlc cho ReportViewer
                string reportPath = Path.Combine(Application.StartupPath, "ReportHoaDonLamSan.rdlc");
                
                // Nếu file không tồn tại ở thư mục thực thi, thử dùng embedded resource
                if (!File.Exists(reportPath))
                {
                    // Sử dụng embedded resource stream
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    // Thử các tên resource có thể có
                    string[] possibleResourceNames = new string[]
                    {
                        "NhaKhoa.ReportHoaDonLamSan.rdlc",
                        "ReportHoaDonLamSan.rdlc"
                    };
                    
                    Stream stream = null;
                    foreach (string resourceName in possibleResourceNames)
                    {
                        stream = assembly.GetManifestResourceStream(resourceName);
                        if (stream != null) break;
                    }
                    
                    if (stream != null)
                    {
                        // Tạo file tạm từ embedded resource
                        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".rdlc");
                        using (FileStream fileStream = new FileStream(tempPath, FileMode.Create))
                        {
                            stream.CopyTo(fileStream);
                        }
                        stream.Dispose();
                        reportPath = tempPath;
                    }
                    else
                    {
                        // Thử tìm trong thư mục gốc của project
                        string projectPath = Path.Combine(Application.StartupPath, "..", "..", "ReportHoaDonLamSan.rdlc");
                        if (File.Exists(projectPath))
                        {
                            reportPath = projectPath;
                        }
                        else
                        {
                            throw new FileNotFoundException("Không tìm thấy file ReportHoaDonLamSan.rdlc. Vui lòng đảm bảo file được copy vào thư mục thực thi.");
                        }
                    }
                }

                rpInHoaDonLamSan.LocalReport.ReportPath = reportPath;

                string ngayThang = "Ngày " + DateTime.Now.Day + " Tháng " + DateTime.Now.Month + " Năm " + DateTime.Now.Year;

                // Pass value cho tham số trong báo cáo
                Microsoft.Reporting.WinForms.ReportParameter[] reportParameters = new Microsoft.Reporting.WinForms.ReportParameter[]
                {
                    new Microsoft.Reporting.WinForms.ReportParameter("maBN", benhNhan.MaBN ?? ""),
                    new Microsoft.Reporting.WinForms.ReportParameter("tenBN", benhNhan.TenBN ?? ""),
                    new Microsoft.Reporting.WinForms.ReportParameter("ngaySinh", benhNhan.NamSinh.ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("gioiTinh", benhNhan.GioiTinh ?? ""),
                    new Microsoft.Reporting.WinForms.ReportParameter("ngayThang", ngayThang)
                };

                rpInHoaDonLamSan.LocalReport.SetParameters(reportParameters);

                rpInHoaDonLamSan.LocalReport.DataSources.Clear();
                rpInHoaDonLamSan.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSetHoaDonLamSan", hoaDonData));

                rpInHoaDonLamSan.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message + "\n\nChi tiết: " + ex.StackTrace, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

