namespace NhaKhoa.UI
{
    partial class FRM_QuanLyLichLamViec
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelTenBacSi = new System.Windows.Forms.Label();
            this.cboBacSiFilter = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cboNgayTrongTuan = new System.Windows.Forms.ComboBox();
            this.labelNgayLamViec = new System.Windows.Forms.Label();
            this.cboGioBD = new System.Windows.Forms.ComboBox();
            this.labelGioBD = new System.Windows.Forms.Label();
            this.cboGioKT = new System.Windows.Forms.ComboBox();
            this.labelGioKT = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.labelGhiChu = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(114)))), ((int)(((byte)(175)))));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(1099, 64);
            this.labelTitle.TabIndex = 100;
            this.labelTitle.Text = "QUẢN LÝ LỊCH LÀM VIỆC";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTenBacSi
            // 
            this.labelTenBacSi.AutoSize = true;
            this.labelTenBacSi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelTenBacSi.Location = new System.Drawing.Point(12, 96);
            this.labelTenBacSi.Name = "labelTenBacSi";
            this.labelTenBacSi.Size = new System.Drawing.Size(98, 25);
            this.labelTenBacSi.TabIndex = 0;
            this.labelTenBacSi.Text = "Tên bác sĩ:";
            // 
            // cboBacSiFilter
            // 
            this.cboBacSiFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBacSiFilter.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboBacSiFilter.Location = new System.Drawing.Point(152, 88);
            this.cboBacSiFilter.Name = "cboBacSiFilter";
            this.cboBacSiFilter.Size = new System.Drawing.Size(300, 33);
            this.cboBacSiFilter.TabIndex = 101;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnRefresh.Location = new System.Drawing.Point(470, 91);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 30);
            this.btnRefresh.TabIndex = 106;
            this.btnRefresh.Text = "Lọc";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cboNgayTrongTuan
            // 
            this.cboNgayTrongTuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNgayTrongTuan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboNgayTrongTuan.Location = new System.Drawing.Point(152, 136);
            this.cboNgayTrongTuan.Name = "cboNgayTrongTuan";
            this.cboNgayTrongTuan.Size = new System.Drawing.Size(168, 33);
            this.cboNgayTrongTuan.TabIndex = 102;
            // 
            // labelNgayLamViec
            // 
            this.labelNgayLamViec.AutoSize = true;
            this.labelNgayLamViec.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelNgayLamViec.Location = new System.Drawing.Point(12, 140);
            this.labelNgayLamViec.Name = "labelNgayLamViec";
            this.labelNgayLamViec.Size = new System.Drawing.Size(134, 25);
            this.labelNgayLamViec.TabIndex = 0;
            this.labelNgayLamViec.Text = "Ngày làm việc:";
            // 
            // cboGioBD
            // 
            this.cboGioBD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioBD.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboGioBD.Location = new System.Drawing.Point(470, 136);
            this.cboGioBD.Name = "cboGioBD";
            this.cboGioBD.Size = new System.Drawing.Size(140, 33);
            this.cboGioBD.TabIndex = 103;
            // 
            // labelGioBD
            // 
            this.labelGioBD.AutoSize = true;
            this.labelGioBD.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelGioBD.Location = new System.Drawing.Point(340, 140);
            this.labelGioBD.Name = "labelGioBD";
            this.labelGioBD.Size = new System.Drawing.Size(114, 25);
            this.labelGioBD.TabIndex = 0;
            this.labelGioBD.Text = "Giờ bắt đầu:";
            // 
            // cboGioKT
            // 
            this.cboGioKT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioKT.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboGioKT.Location = new System.Drawing.Point(760, 136);
            this.cboGioKT.Name = "cboGioKT";
            this.cboGioKT.Size = new System.Drawing.Size(120, 33);
            this.cboGioKT.TabIndex = 104;
            // 
            // labelGioKT
            // 
            this.labelGioKT.AutoSize = true;
            this.labelGioKT.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelGioKT.Location = new System.Drawing.Point(630, 140);
            this.labelGioKT.Name = "labelGioKT";
            this.labelGioKT.Size = new System.Drawing.Size(117, 25);
            this.labelGioKT.TabIndex = 0;
            this.labelGioKT.Text = "Giờ kết thúc:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtGhiChu.Location = new System.Drawing.Point(152, 183);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(500, 32);
            this.txtGhiChu.TabIndex = 105;
            // 
            // labelGhiChu
            // 
            this.labelGhiChu.AutoSize = true;
            this.labelGhiChu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelGhiChu.Location = new System.Drawing.Point(12, 186);
            this.labelGhiChu.Name = "labelGhiChu";
            this.labelGhiChu.Size = new System.Drawing.Size(81, 25);
            this.labelGhiChu.TabIndex = 0;
            this.labelGhiChu.Text = "Ghi chú:";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAdd.Location = new System.Drawing.Point(895, 239);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 34);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnEdit.Location = new System.Drawing.Point(895, 291);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 34);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnDelete.Location = new System.Drawing.Point(895, 342);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 34);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgv
            // 
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeight = 29;
            this.dgv.Location = new System.Drawing.Point(7, 239);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.Size = new System.Drawing.Size(860, 500);
            this.dgv.TabIndex = 107;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // FRM_QuanLyLichLamViec
            // 
            this.ClientSize = new System.Drawing.Size(1099, 600);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelTenBacSi);
            this.Controls.Add(this.cboBacSiFilter);
            this.Controls.Add(this.labelNgayLamViec);
            this.Controls.Add(this.cboNgayTrongTuan);
            this.Controls.Add(this.labelGioBD);
            this.Controls.Add(this.cboGioBD);
            this.Controls.Add(this.labelGioKT);
            this.Controls.Add(this.cboGioKT);
            this.Controls.Add(this.labelGhiChu);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgv);
            this.Name = "FRM_QuanLyLichLamViec";
            this.Text = "Quản lý lịch làm việc";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelTenBacSi;
        private System.Windows.Forms.ComboBox cboNgayTrongTuan;
        private System.Windows.Forms.Label labelNgayLamViec;
        private System.Windows.Forms.ComboBox cboGioBD;
        private System.Windows.Forms.Label labelGioBD;
        private System.Windows.Forms.ComboBox cboGioKT;
        private System.Windows.Forms.Label labelGioKT;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label labelGhiChu;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ComboBox cboBacSiFilter;
        private System.Windows.Forms.Button btnRefresh;

        #endregion
    }
}
