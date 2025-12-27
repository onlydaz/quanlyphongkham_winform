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
using NhaKhoa.DAL;

namespace NhaKhoa.UI
{
    public partial class FormInDanhMuc : Form
    {
        public FormInDanhMuc()
        {
            InitializeComponent();
        }

        private void FormInDanhMuc_Load(object sender, EventArgs e)
        {
            try
            {
                // lấy danh sách danh mục từ cơ sở dữ liệu và hiển thị trong báo cáo
                using (var db = new NhaKhoaContext())
                {
                    // Lấy danh sách thuốc từ cơ sở dữ liệu với đúng cấu trúc mà report cần
                    var categories = db.Thuocs
                        .Select(c => new
                        {
                            MaThuoc = c.MaThuoc,
                            TenThuoc = c.TenThuoc,
                            DVT = c.DVT ?? "",
                            DonGia = c.DonGia,
                            SoLuongTon = c.SoLuongTon
                        })
                        .ToList();

                    // Thiết lập file rdlc cho ReportViewer
                    string reportPath = Path.Combine(Application.StartupPath, "ReportInDanhMuc.rdlc");
                    
                    // Nếu file không tồn tại ở thư mục thực thi, thử dùng embedded resource
                    if (!File.Exists(reportPath))
                    {
                        // Sử dụng embedded resource stream
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        // Thử các tên resource có thể có
                        string[] possibleResourceNames = new string[]
                        {
                            "NhaKhoa.ReportInDanhMuc.rdlc",
                            "ReportInDanhMuc.rdlc"
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
                            string projectPath = Path.Combine(Application.StartupPath, "..", "..", "ReportInDanhMuc.rdlc");
                            if (File.Exists(projectPath))
                            {
                                reportPath = projectPath;
                            }
                            else
                            {
                                throw new FileNotFoundException("Không tìm thấy file ReportInDanhMuc.rdlc. Vui lòng đảm bảo file được copy vào thư mục thực thi.");
                            }
                        }
                    }

                    rpInDanhMuc.LocalReport.ReportPath = reportPath;

                    string ngayThang = "Ngày " + DateTime.Now.Day + " Tháng " + DateTime.Now.Month + " Năm " + DateTime.Now.Year;
                    string soLuongDanhMuc = "Số lượng: " + categories.Count;

                    // pass value cho tham số trong báo cáo
                    Microsoft.Reporting.WinForms.ReportParameter[] reportParameters = new Microsoft.Reporting.WinForms.ReportParameter[]
                    {
                        new Microsoft.Reporting.WinForms.ReportParameter("ngayThang", ngayThang),
                        new Microsoft.Reporting.WinForms.ReportParameter("soLuong", soLuongDanhMuc.ToString())
                    };

                    rpInDanhMuc.LocalReport.SetParameters(reportParameters);

                    rpInDanhMuc.LocalReport.DataSources.Clear();
                    rpInDanhMuc.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSetDanhMuc", categories));

                    rpInDanhMuc.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message + "\n\nChi tiết: " + ex.StackTrace, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
