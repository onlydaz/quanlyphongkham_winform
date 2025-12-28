using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using NhaKhoa.DAL;

namespace NhaKhoa.GUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Tự động tạo database và bảng nếu chưa tồn tại
            // Chỉ tạo database nếu chưa có, không xóa dữ liệu hiện có
            Database.SetInitializer(new CreateDatabaseIfNotExists<NhaKhoaContext>());
            
            // Set license cho EPPlus 8+ (chỉ set một lần khi khởi động ứng dụng)
            ExcelPackage.License.SetNonCommercialPersonal("NhaKhoa");
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool shouldRestart;
            do
            {
                shouldRestart = false;
                var login = new Login();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    var main = new frmMain(login.LoggedInRole);
                    main.FormClosed += (s, args) =>
                    {
                        if (main.DialogResult == DialogResult.Abort)
                            shouldRestart = true;
                    };
                    Application.Run(main);
                }
                else
                {
                    break; 
                }
            } while (shouldRestart);
        }
    }
}
