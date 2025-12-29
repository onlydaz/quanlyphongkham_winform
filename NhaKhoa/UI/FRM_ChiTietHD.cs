using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NhaKhoa.BUS;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.Hoadon
{
    public partial class FRM_ChiTietHD : Form
    {
        private readonly string _maHD;
        private readonly HoaDonBUS _hoaDonBus = new HoaDonBUS();

        public FRM_ChiTietHD(string maHD)
        {
            InitializeComponent();
            _maHD = maHD;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void FRM_ChiTietHD_Load(object sender, EventArgs e)
        {
            LoadHoaDonDetails();
        }

        private void LoadHoaDonDetails()
        {
            try
            {
                var hd = _hoaDonBus.LayHoaDonTheoMa(_maHD);
                if (hd == null)
                {
                    MessageBox.Show("Không tìm thấy hoá đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                lblMaHD.Text = hd.MaHD;
                lblNgayLap.Text = hd.NgayLapHD.ToString("dd/MM/yyyy HH:mm");
                // Update and center header to include invoice id
                lblHeader.Text = $"Chi tiết hoá đơn - {hd.MaHD}";
                lblBenhNhan.Text = hd.BenhNhan?.TenBN ?? hd.MaBN;
                lblNhanVien.Text = hd.NhanVien?.TenNV ?? hd.MaNV;
                lblTongTien.Text = "Tổng tiền: " + hd.TongTien.ToString("N0");

                // Load related LamSan rows for this patient around the invoice date
                using (var ctx = new NhaKhoa.DAL.NhaKhoaContext())
                {
                    var lamSans = ctx.LamSans
                        .Where(x => x.MaBN == hd.MaBN)
                        .OrderByDescending(x => x.NgayKham)
                        .ToList();

                    var dt = new DataTable();
                    dt.Columns.Add("MaLS");
                    dt.Columns.Add("NgayKham");
                    dt.Columns.Add("GioKham");
                    dt.Columns.Add("TrieuChung");
                    dt.Columns.Add("TenCD");
                    dt.Columns.Add("TenDT");
                    dt.Columns.Add("ThanhTien", typeof(decimal));

                    foreach (var ls in lamSans)
                    {
                        var tenCD = "";
                        var tenDT = "";
                        decimal dongia = 0;
                        if (!string.IsNullOrEmpty(ls.MaCD))
                        {
                            var cd = ctx.ChanDoans.AsNoTracking().SingleOrDefault(c => c.MaCD == ls.MaCD);
                            if (cd != null) tenCD = cd.TenChuanDoan ?? cd.MaCD;
                        }
                        if (!string.IsNullOrEmpty(ls.MaDT))
                        {
                            var dtv = ctx.DieuTris.AsNoTracking().SingleOrDefault(d => d.MaDT == ls.MaDT);
                            if (dtv != null)
                            {
                                tenDT = dtv.TenDieuTri ?? dtv.MaDT;
                                if (dtv.DonGia.HasValue) dongia = dtv.DonGia.Value;
                            }
                        }

                        string gio = "";
                        try { gio = ls.GioBatDau != null && ls.GioKetThuc != null ? $"{ls.GioBatDau:hh\\:mm} - {ls.GioKetThuc:hh\\:mm}" : (ls.GioBatDau?.ToString() ?? ""); } catch { gio = ""; }

                        dt.Rows.Add(ls.MaLS, ls.NgayKham?.ToString("dd/MM/yyyy"), gio, ls.TrieuChung, tenCD, tenDT, dongia);
                    }

                    dgvDetails.DataSource = dt;

                    // Rename column headers to Vietnamese and format amount
                    if (dgvDetails.Columns.Contains("MaLS")) dgvDetails.Columns["MaLS"].HeaderText = "Mã Lâm Sàn";
                    if (dgvDetails.Columns.Contains("NgayKham")) dgvDetails.Columns["NgayKham"].HeaderText = "Ngày khám";
                    if (dgvDetails.Columns.Contains("GioKham")) dgvDetails.Columns["GioKham"].HeaderText = "Giờ khám";
                    if (dgvDetails.Columns.Contains("TrieuChung")) dgvDetails.Columns["TrieuChung"].HeaderText = "Triệu chứng";
                    if (dgvDetails.Columns.Contains("TenCD")) dgvDetails.Columns["TenCD"].HeaderText = "Chuẩn đoán";
                    if (dgvDetails.Columns.Contains("TenDT")) dgvDetails.Columns["TenDT"].HeaderText = "Điều trị";
                    if (dgvDetails.Columns.Contains("ThanhTien"))
                    {
                        dgvDetails.Columns["ThanhTien"].HeaderText = "Thành tiền";
                        dgvDetails.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
                        dgvDetails.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
