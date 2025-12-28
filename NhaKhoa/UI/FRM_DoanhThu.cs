using NhaKhoa.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NhaKhoa.UI
{
    public partial class FRM_DoanhThu : Form
    {
        private readonly HoaDonBUS _hoaDonBUS;

        public FRM_DoanhThu()
        {
            InitializeComponent();
            _hoaDonBUS = new HoaDonBUS();
            rbtnThang.Checked = true;
        }

        private void ClearChart()
        {
            ChartDoanhThu.Series.Clear();
            ChartDoanhThu.Titles.Clear();
            ChartDoanhThu.ChartAreas[0].AxisX.Title = "";
            ChartDoanhThu.ChartAreas[0].AxisY.Title = "";
        }
        // Hàm tải dữ liệu và hiển thị biểu đồ theo tháng
        private void LoadDataTheoThang()
        {
            ClearChart();

            var series = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.DodgerBlue,
                IsValueShownAsLabel = true,
                LabelFormat = "N0"
            };

            var dsHoaDon = _hoaDonBUS.LayDanhSach();

            var doanhThuTheoThang = dsHoaDon
                .GroupBy(hd => hd.NgayLapHD.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    DoanhThu = g.Sum(hd => hd.TongTien)
                })
                .OrderBy(x => x.Thang)
                .ToList();

            foreach (var item in doanhThuTheoThang)
            {
                series.Points.AddXY(item.Thang, item.DoanhThu);
            }

            ChartDoanhThu.Series.Add(series);
            ChartDoanhThu.Titles.Add(new Title("Biểu Đồ Doanh Thu Theo Tháng", Docking.Top, new Font("Arial", 12, FontStyle.Bold), Color.Black));
            ChartDoanhThu.ChartAreas[0].AxisX.Title = "Tháng";
            ChartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (VND)";
            ChartDoanhThu.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
        }

        private void LoadDataTheoQuy()
        {
            ClearChart();

            var series = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.OrangeRed,
                IsValueShownAsLabel = true,
                LabelFormat = "N0"
            };

            var dsHoaDon = _hoaDonBUS.LayDanhSach();

            var doanhThuTheoQuy = dsHoaDon
                .GroupBy(hd => ((hd.NgayLapHD.Month - 1) / 3) + 1)
                .Select(g => new
                {
                    Quy = g.Key,
                    DoanhThu = g.Sum(hd => hd.TongTien)
                })
                .OrderBy(x => x.Quy)
                .ToList();

            foreach (var item in doanhThuTheoQuy)
            {
                series.Points.AddXY(item.Quy, item.DoanhThu);
            }

            ChartDoanhThu.Series.Add(series);
            ChartDoanhThu.Titles.Add(new Title("Biểu Đồ Doanh Thu Theo Quý", Docking.Top, new Font("Arial", 12, FontStyle.Bold), Color.Black));
            ChartDoanhThu.ChartAreas[0].AxisX.Title = "Quý";
            ChartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (VND)";
            ChartDoanhThu.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
        }

        private void LoadDataTheoNam()
        {
            ClearChart();

            var series = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.Green,
                IsValueShownAsLabel = true,
                LabelFormat = "N0"
            };

            var dsHoaDon = _hoaDonBUS.LayDanhSach();

            var doanhThuTheoNam = dsHoaDon
                .GroupBy(hd => hd.NgayLapHD.Year)
                .Select(g => new
                {
                    Nam = g.Key,
                    DoanhThu = g.Sum(hd => hd.TongTien)
                })
                .OrderBy(x => x.Nam)
                .ToList();

            foreach (var item in doanhThuTheoNam)
            {
                series.Points.AddXY(item.Nam, item.DoanhThu);
            }

            ChartDoanhThu.Series.Add(series);
            ChartDoanhThu.Titles.Add(new Title("Biểu Đồ Doanh Thu Theo Năm", Docking.Top, new Font("Arial", 12, FontStyle.Bold), Color.Black));
            ChartDoanhThu.ChartAreas[0].AxisX.Title = "Năm";
            ChartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (VND)";
            ChartDoanhThu.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
        }

        private void FRM_DoanhThu_Load(object sender, EventArgs e)
        {
            LoadDataTheoThang();
        }

        private void rbtnThang_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnThang.Checked) LoadDataTheoThang();
        }

        private void rbtnNam_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnNam.Checked) LoadDataTheoNam();
        }

        private void rbtnQuy_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnQuy.Checked) LoadDataTheoQuy();
        }

    }
}
