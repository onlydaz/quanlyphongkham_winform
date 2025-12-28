using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NhaKhoa.BUS;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;
using NhaKhoa.UI;

namespace NhaKhoa
{
    public partial class FRM_Lamsan : Form
    {
        private string _maBN;
        private readonly BenhNhanBUS _benhNhanBus;
        private readonly ChanDoanBUS _chuanDoanBus;
        private readonly DieuTriBUS _dieuTriBus;

        public FRM_Lamsan(string maBN)
        {
            InitializeComponent();
            _maBN = maBN;
            _benhNhanBus = new BenhNhanBUS();
            _chuanDoanBus = new ChanDoanBUS();
            _dieuTriBus = new DieuTriBUS();
            LoadThongTinBenhNhan();
            LoadComboBoxes();
            LoadDanhSachLamSan();
        }
        private void LoadThongTinBenhNhan()
        {
            try
            {
                var bn = _benhNhanBus.LayBenhNhanTheoMa(_maBN);
                if (bn != null)
                {
                    lblMaBN.Text = bn.MaBN;
                    lblTenBN.Text = bn.TenBN;
                    lblNgaySinh.Text = bn.NamSinh.ToString();
                    lblGioiTinh.Text = bn.GioiTinh;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadComboBoxes()
        {
            try
            {
                var listChanDoan = _chuanDoanBus.LayDanhSach();
                cbChanDoan.DataSource = listChanDoan;
                cbChanDoan.DisplayMember = "TenChuanDoan";
                cbChanDoan.ValueMember = "MaCD";
                cbChanDoan.SelectedIndex = -1;

                var listDieuTri = _dieuTriBus.LayDanhSach();
                cboDieuTri.DataSource = listDieuTri;
                cboDieuTri.DisplayMember = "TenDieuTri";
                cboDieuTri.ValueMember = "MaDT";
                cboDieuTri.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu combobox: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachLamSan()
        {
            try
            {
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
                                    MaLS = ls.MaLS,
                                    TenChuanDoan = cd != null ? cd.TenChuanDoan : "",
                                    TenDieuTri = dt != null ? dt.TenDieuTri : "",
                                    DonViTinh = dt != null ? dt.DonViTinh : "",
                                    DonGia = dt != null ? dt.DonGia : (decimal?)null,
                                    NgayKham = ls.NgayKham,
                                    TrieuChung = ls.TrieuChung
                                };

                    var list = query.ToList();

                    var dataTable = new DataTable();
                    dataTable.Columns.Add("MaLS", typeof(string));
                    dataTable.Columns.Add("Chẩn đoán", typeof(string));
                    dataTable.Columns.Add("Điều trị", typeof(string));
                    dataTable.Columns.Add("Tên", typeof(string));
                    dataTable.Columns.Add("Đơn vị tính", typeof(string));
                    dataTable.Columns.Add("Đơn giá", typeof(decimal));
                    dataTable.Columns.Add("Ngày khám", typeof(DateTime));
                    dataTable.Columns.Add("Triệu chứng", typeof(string));

                    foreach (var item in list)
                    {
                        dataTable.Rows.Add(
                            item.MaLS ?? "",
                            item.TenChuanDoan ?? "",
                            item.TenDieuTri ?? "",
                            item.TenDieuTri ?? "",
                            item.DonViTinh ?? "",
                            item.DonGia ?? 0m,
                            item.NgayKham ?? DateTime.Now,
                            item.TrieuChung ?? ""
                        );
                    }

                    dgvLamSan.DataSource = dataTable;
                    SetupDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu lâm sàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvLamSan.Columns.Count > 0)
            {
                dgvLamSan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvLamSan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvLamSan.MultiSelect = false;
                dgvLamSan.ReadOnly = true;

                if (dgvLamSan.Columns["MaLS"] != null)
                {
                    dgvLamSan.Columns["MaLS"].Visible = false;
                }

                if (dgvLamSan.Columns["Đơn giá"] != null)
                {
                    dgvLamSan.Columns["Đơn giá"].DefaultCellStyle.Format = "#,##0 VNĐ";
                    dgvLamSan.Columns["Đơn giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dgvLamSan.Columns["Ngày khám"] != null)
                {
                    dgvLamSan.Columns["Ngày khám"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
            }
        }

        private void btnThemLamSan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbChanDoan.SelectedValue == null || string.IsNullOrWhiteSpace(cbChanDoan.SelectedValue.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn chẩn đoán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboDieuTri.SelectedValue == null || string.IsNullOrWhiteSpace(cboDieuTri.SelectedValue.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn điều trị!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maCD = cbChanDoan.SelectedValue.ToString();
                string maDT = cboDieuTri.SelectedValue.ToString();

                using (var ctx = new NhaKhoaContext())
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    ctx.Configuration.AutoDetectChangesEnabled = false;

                    string maLS = GetNewMaLS(ctx);

                    var lamSan = new LamSan
                    {
                        MaLS = maLS,
                        MaBN = _maBN,
                        MaCD = maCD,
                        MaDT = maDT,
                        NgayKham = DateTime.Now,
                        TrieuChung = "",
                        MaNV = ""
                    };

                    ctx.LamSans.Add(lamSan);
                    ctx.SaveChanges();

                    MessageBox.Show("Thêm lâm sàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    cbChanDoan.SelectedIndex = -1;
                    cboDieuTri.SelectedIndex = -1;
                    
                    LoadDanhSachLamSan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetNewMaLS(NhaKhoaContext ctx)
        {
            var last = ctx.LamSans
                .AsNoTracking()
                .Where(x => x.MaLS != null && x.MaLS.StartsWith("LS"))
                .ToList();

            if (!last.Any()) return "LS001";

            int max = last
                .Select(x =>
                {
                    int n;
                    return int.TryParse(x.MaLS.Substring(2), out n) ? n : 0;
                })
                .Max();

            return "LS" + (max + 1).ToString("D3");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLamSan.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một bản ghi để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maLS = dgvLamSan.SelectedRows[0].Cells["MaLS"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(maLS))
                {
                    MessageBox.Show("Không tìm thấy mã lâm sàn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = MessageBox.Show("Bạn có chắc muốn xóa bản ghi này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                using (var ctx = new NhaKhoaContext())
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    ctx.Configuration.AutoDetectChangesEnabled = false;

                    var entity = new LamSan { MaLS = maLS };
                    ctx.LamSans.Attach(entity);
                    ctx.LamSans.Remove(entity);
                    ctx.SaveChanges();

                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachLamSan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInToaThuoc_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở form in hóa đơn với ReportViewer
                var formInHoaDon = new NhaKhoa.UI.FormInHoaDonLamSan(_maBN);
                formInHoaDon.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
