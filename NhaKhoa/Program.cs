using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace NhaKhoa
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
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
                    var main = new frmMain(login.LoggedInRole, login.LoggedInUserId);
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
