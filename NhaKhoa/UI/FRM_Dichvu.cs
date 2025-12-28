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
using NhaKhoa.DAL.Models;

namespace NhaKhoa.UI
{
    public partial class FRM_Dichvu : Form
    {
        private readonly ChanDoanBUS _chanDoanBus;
        private readonly DieuTriBUS _dieuTriBus;

        public FRM_Dichvu()
        {
            InitializeComponent();
            _chanDoanBus = new ChanDoanBUS();
            _dieuTriBus = new DieuTriBUS();
            
            this.Load += FRM_Dichvu_Load;
            this.dgvChuanDoan.CellClick += dgvChuanDoan_CellClick;
            this.dgvDieuTri.CellClick += dgvDieuTri_CellClick;
        }

        private void FRM_Dichvu_Load(object sender, EventArgs e)
        {
            LoadChanDoan();
            LoadDieuTri();
            SetupDataGridViews();
        }

        private void LoadChanDoan()
        {
            try
            {
                var list = _chanDoanBus.LayDanhSach();
                dgvChuanDoan.DataSource = ConvertToChanDoanDataTable(list);
                SetupChanDoanGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu chẩn đoán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable ConvertToChanDoanDataTable(List<ChanDoan> list)
        {
            var dt = new DataTable();
            dt.Columns.Add("MaCD", typeof(string));
            dt.Columns.Add("TenChuanDoan", typeof(string));

            foreach (var cd in list)
            {
                dt.Rows.Add(cd.MaCD, cd.TenChuanDoan);
            }

            return dt;
        }

        private void SetupChanDoanGrid()
        {
            dgvChuanDoan.RowHeadersVisible = false;
            dgvChuanDoan.AllowUserToAddRows = false;
            dgvChuanDoan.ReadOnly = true;
            dgvChuanDoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChuanDoan.MultiSelect = false;

            if (dgvChuanDoan.Columns.Count > 0)
            {
                dgvChuanDoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvChuanDoan.Columns["MaCD"] != null)
                    dgvChuanDoan.Columns["MaCD"].HeaderText = "Mã chẩn đoán";
                if (dgvChuanDoan.Columns["TenChuanDoan"] != null)
                    dgvChuanDoan.Columns["TenChuanDoan"].HeaderText = "Tên chẩn đoán";
            }
        }

        private void btnThemCD_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txttenCD.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên chẩn đoán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var chanDoan = new ChanDoan
                {
                    MaCD = string.IsNullOrWhiteSpace(txtmaCD.Text) ? null : txtmaCD.Text.Trim(),
                    TenChuanDoan = txttenCD.Text.Trim()
                };

                _chanDoanBus.ThemChanDoan(chanDoan);
                MessageBox.Show("Thêm chẩn đoán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadChanDoan();
                ClearChanDoanForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaCD_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtmaCD.Text))
                {
                    MessageBox.Show("Vui lòng chọn chẩn đoán cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show($"Bạn có chắc muốn xóa chẩn đoán {txtmaCD.Text}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                _chanDoanBus.XoaChanDoan(txtmaCD.Text);
                MessageBox.Show("Xóa chẩn đoán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadChanDoan();
                ClearChanDoanForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvChuanDoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvChuanDoan.Rows[e.RowIndex];

            txtmaCD.Text = row.Cells["MaCD"].Value?.ToString() ?? "";
            txttenCD.Text = row.Cells["TenChuanDoan"].Value?.ToString() ?? "";
        }

        private void ClearChanDoanForm()
        {
            txtmaCD.Clear();
            txttenCD.Clear();
        }

        private void LoadDieuTri()
        {
            try
            {
                var list = _dieuTriBus.LayDanhSach();
                dgvDieuTri.DataSource = ConvertToDieuTriDataTable(list);
                SetupDieuTriGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu điều trị: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable ConvertToDieuTriDataTable(List<DieuTri> list)
        {
            var dt = new DataTable();
            dt.Columns.Add("MaDT", typeof(string));
            dt.Columns.Add("MaCD", typeof(string));
            dt.Columns.Add("TenDieuTri", typeof(string));
            dt.Columns.Add("DonViTinh", typeof(string));
            dt.Columns.Add("DonGia", typeof(decimal));

            foreach (var dtItem in list)
            {
                dt.Rows.Add(dtItem.MaDT, dtItem.MaCD ?? "", dtItem.TenDieuTri, dtItem.DonViTinh, dtItem.DonGia ?? 0);
            }

            return dt;
        }

        private void SetupDieuTriGrid()
        {
            dgvDieuTri.RowHeadersVisible = false;
            dgvDieuTri.AllowUserToAddRows = false;
            dgvDieuTri.ReadOnly = true;
            dgvDieuTri.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDieuTri.MultiSelect = false;

            if (dgvDieuTri.Columns.Count > 0)
            {
                dgvDieuTri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvDieuTri.Columns["MaDT"] != null)
                    dgvDieuTri.Columns["MaDT"].HeaderText = "Mã điều trị";
                if (dgvDieuTri.Columns["MaCD"] != null)
                    dgvDieuTri.Columns["MaCD"].HeaderText = "Mã chẩn đoán";
                if (dgvDieuTri.Columns["TenDieuTri"] != null)
                    dgvDieuTri.Columns["TenDieuTri"].HeaderText = "Tên điều trị";
                if (dgvDieuTri.Columns["DonViTinh"] != null)
                    dgvDieuTri.Columns["DonViTinh"].HeaderText = "Đơn vị tính";
                if (dgvDieuTri.Columns["DonGia"] != null)
                {
                    dgvDieuTri.Columns["DonGia"].HeaderText = "Đơn giá";
                    dgvDieuTri.Columns["DonGia"].DefaultCellStyle.Format = "#,##0 VNĐ";
                }
            }
        }

        private void btnThemDT_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txttenDT.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên điều trị!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDVT.Text))
                {
                    MessageBox.Show("Vui lòng nhập đơn vị tính!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal? donGia = null;
                if (!string.IsNullOrWhiteSpace(txtDongia.Text))
                {
                    decimal temp;
                    if (decimal.TryParse(txtDongia.Text, out temp))
                    {
                        donGia = temp;
                    }
                    else
                    {
                        MessageBox.Show("Đơn giá không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string maCD = null;
                if (dgvChuanDoan.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvChuanDoan.SelectedRows[0];
                    maCD = selectedRow.Cells["MaCD"].Value?.ToString();
                }
                else if (!string.IsNullOrWhiteSpace(txtmaCD.Text))
                {
                    maCD = txtmaCD.Text.Trim();
                }

                var dieuTri = new DieuTri
                {
                    MaDT = string.IsNullOrWhiteSpace(txtmaDT.Text) ? null : txtmaDT.Text.Trim(),
                    TenDieuTri = txttenDT.Text.Trim(),
                    DonViTinh = txtDVT.Text.Trim(),
                    DonGia = donGia,
                    MaCD = string.IsNullOrWhiteSpace(maCD) ? null : maCD
                };

                _dieuTriBus.ThemDieuTri(dieuTri);
                MessageBox.Show("Thêm điều trị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDieuTri();
                ClearDieuTriForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaDT_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtmaDT.Text))
                {
                    MessageBox.Show("Vui lòng chọn điều trị cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show($"Bạn có chắc muốn xóa điều trị {txtmaDT.Text}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                _dieuTriBus.XoaDieuTri(txtmaDT.Text);
                MessageBox.Show("Xóa điều trị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDieuTri();
                ClearDieuTriForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDieuTri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvDieuTri.Rows[e.RowIndex];

            txtmaDT.Text = row.Cells["MaDT"].Value?.ToString() ?? "";
            txttenDT.Text = row.Cells["TenDieuTri"].Value?.ToString() ?? "";
            txtDVT.Text = row.Cells["DonViTinh"].Value?.ToString() ?? "";
            var donGia = row.Cells["DonGia"].Value;
            txtDongia.Text = donGia != null ? donGia.ToString() : "";
        }

        private void ClearDieuTriForm()
        {
            txtmaDT.Clear();
            txttenDT.Clear();
            txtDVT.Clear();
            txtDongia.Clear();
        }

        private void SetupDataGridViews()
        {
        }

        private void mnuLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                LoadChanDoan();
                LoadDieuTri();
                ClearChanDoanForm();
                ClearDieuTriForm();
                MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtmaCD.Text) && !string.IsNullOrWhiteSpace(txttenCD.Text))
                {
                    var chanDoan = new ChanDoan
                    {
                        MaCD = txtmaCD.Text.Trim(),
                        TenChuanDoan = txttenCD.Text.Trim()
                    };

                    _chanDoanBus.CapNhatChanDoan(chanDoan);
                    MessageBox.Show("Cập nhật chẩn đoán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadChanDoan();
                    ClearChanDoanForm();
                }
                else if (!string.IsNullOrWhiteSpace(txtmaDT.Text) && !string.IsNullOrWhiteSpace(txttenDT.Text))
                {
                    decimal? donGia = null;
                    if (!string.IsNullOrWhiteSpace(txtDongia.Text))
                    {
                        decimal temp;
                        if (decimal.TryParse(txtDongia.Text, out temp))
                        {
                            donGia = temp;
                        }
                    }

                    string maCD = null;
                    if (dgvDieuTri.SelectedRows.Count > 0)
                    {
                        var selectedRow = dgvDieuTri.SelectedRows[0];
                        maCD = selectedRow.Cells["MaCD"].Value?.ToString();
                    }
                    else
                    {
                        var existing = _dieuTriBus.LayDieuTriTheoMa(txtmaDT.Text.Trim());
                        maCD = existing?.MaCD;
                    }

                    var dieuTri = new DieuTri
                    {
                        MaDT = txtmaDT.Text.Trim(),
                        TenDieuTri = txttenDT.Text.Trim(),
                        DonViTinh = txtDVT.Text.Trim(),
                        DonGia = donGia,
                        MaCD = string.IsNullOrWhiteSpace(maCD) ? null : maCD
                    };

                    _dieuTriBus.CapNhatDieuTri(dieuTri);
                    MessageBox.Show("Cập nhật điều trị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDieuTri();
                    ClearDieuTriForm();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một bản ghi để cập nhật!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
