namespace NhaKhoa.UI
{
    partial class FRM_LichLamViec
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.labelCurrentBacSi = new System.Windows.Forms.Label();
            this.lblCurrentBacSiValue = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.cboNgayTrongTuan = new System.Windows.Forms.ComboBox();
            this.cboGioBD = new System.Windows.Forms.ComboBox();
            this.cboGioKT = new System.Windows.Forms.ComboBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.labelTenBacSi = new System.Windows.Forms.Label();
            this.labelNgayLamViec = new System.Windows.Forms.Label();
            this.labelGioBD = new System.Windows.Forms.Label();
            this.labelGioKT = new System.Windows.Forms.Label();
            this.labelGhiChu = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCurrentBacSi
            // 
            this.labelCurrentBacSi.AutoSize = true;
            this.labelCurrentBacSi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelCurrentBacSi.Location = new System.Drawing.Point(147, 82);
            this.labelCurrentBacSi.Name = "labelCurrentBacSi";
            this.labelCurrentBacSi.Size = new System.Drawing.Size(208, 25);
            this.labelCurrentBacSi.TabIndex = 2;
            this.labelCurrentBacSi.Text = "Bác sĩ đang đăng nhập:";
            this.labelCurrentBacSi.Visible = false;
            // 
            // lblCurrentBacSiValue
            // 
            this.lblCurrentBacSiValue.AutoSize = true;
            this.lblCurrentBacSiValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentBacSiValue.Location = new System.Drawing.Point(152, 82);
            this.lblCurrentBacSiValue.Name = "lblCurrentBacSiValue";
            this.lblCurrentBacSiValue.Size = new System.Drawing.Size(0, 25);
            this.lblCurrentBacSiValue.TabIndex = 3;
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(114)))), ((int)(((byte)(175)))));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(896, 64);
            this.labelTitle.TabIndex = 99;
            this.labelTitle.Text = "LỊCH LÀM VIỆC CỦA BÁC SĨ";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboNgayTrongTuan
            // 
            this.cboNgayTrongTuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNgayTrongTuan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboNgayTrongTuan.Location = new System.Drawing.Point(152, 133);
            this.cboNgayTrongTuan.Name = "cboNgayTrongTuan";
            this.cboNgayTrongTuan.Size = new System.Drawing.Size(200, 33);
            this.cboNgayTrongTuan.TabIndex = 3;
            // 
            // cboGioBD
            // 
            this.cboGioBD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioBD.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboGioBD.Location = new System.Drawing.Point(152, 179);
            this.cboGioBD.Name = "cboGioBD";
            this.cboGioBD.Size = new System.Drawing.Size(120, 33);
            this.cboGioBD.TabIndex = 5;
            // 
            // cboGioKT
            // 
            this.cboGioKT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioKT.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboGioKT.Location = new System.Drawing.Point(443, 178);
            this.cboGioKT.Name = "cboGioKT";
            this.cboGioKT.Size = new System.Drawing.Size(120, 33);
            this.cboGioKT.TabIndex = 7;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtGhiChu.Location = new System.Drawing.Point(152, 224);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(500, 32);
            this.txtGhiChu.TabIndex = 9;
            // 
            // labelTenBacSi
            // 
            this.labelTenBacSi.AutoSize = true;
            this.labelTenBacSi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelTenBacSi.Location = new System.Drawing.Point(12, 82);
            this.labelTenBacSi.Name = "labelTenBacSi";
            this.labelTenBacSi.Size = new System.Drawing.Size(98, 25);
            this.labelTenBacSi.TabIndex = 0;
            this.labelTenBacSi.Text = "Tên bác sĩ:";
            // 
            // labelNgayLamViec
            // 
            this.labelNgayLamViec.AutoSize = true;
            this.labelNgayLamViec.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelNgayLamViec.Location = new System.Drawing.Point(12, 140);
            this.labelNgayLamViec.Name = "labelNgayLamViec";
            this.labelNgayLamViec.Size = new System.Drawing.Size(134, 25);
            this.labelNgayLamViec.TabIndex = 2;
            this.labelNgayLamViec.Text = "Ngày làm việc:";
            // 
            // labelGioBD
            // 
            this.labelGioBD.AutoSize = true;
            this.labelGioBD.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelGioBD.Location = new System.Drawing.Point(12, 182);
            this.labelGioBD.Name = "labelGioBD";
            this.labelGioBD.Size = new System.Drawing.Size(119, 25);
            this.labelGioBD.TabIndex = 4;
            this.labelGioBD.Text = "Giờ làm việc:";
            // 
            // labelGioKT
            // 
            this.labelGioKT.AutoSize = true;
            this.labelGioKT.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelGioKT.Location = new System.Drawing.Point(320, 182);
            this.labelGioKT.Name = "labelGioKT";
            this.labelGioKT.Size = new System.Drawing.Size(117, 25);
            this.labelGioKT.TabIndex = 6;
            this.labelGioKT.Text = "Giờ kết thúc:";
            // 
            // labelGhiChu
            // 
            this.labelGhiChu.AutoSize = true;
            this.labelGhiChu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelGhiChu.Location = new System.Drawing.Point(12, 227);
            this.labelGhiChu.Name = "labelGhiChu";
            this.labelGhiChu.Size = new System.Drawing.Size(81, 25);
            this.labelGhiChu.TabIndex = 8;
            this.labelGhiChu.Text = "Ghi chú:";
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnLuu.Location = new System.Drawing.Point(660, 178);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(80, 32);
            this.btnLuu.TabIndex = 10;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.BtnLuu_Click);
            // 
            // dgv
            // 
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeight = 29;
            this.dgv.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgv.Location = new System.Drawing.Point(17, 300);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.Size = new System.Drawing.Size(760, 420);
            this.dgv.TabIndex = 11;
            // 
            // FRM_LichLamViec
            // 
            this.ClientSize = new System.Drawing.Size(896, 634);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelTenBacSi);
            this.Controls.Add(this.labelCurrentBacSi);
            this.Controls.Add(this.lblCurrentBacSiValue);
            this.Controls.Add(this.labelNgayLamViec);
            this.Controls.Add(this.cboNgayTrongTuan);
            this.Controls.Add(this.labelGioBD);
            this.Controls.Add(this.cboGioBD);
            this.Controls.Add(this.labelGioKT);
            this.Controls.Add(this.cboGioKT);
            this.Controls.Add(this.labelGhiChu);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.dgv);
            this.Name = "FRM_LichLamViec";
            this.Text = "Lịch làm việc";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label labelCurrentBacSi;
        private System.Windows.Forms.Label lblCurrentBacSiValue;
        private System.Windows.Forms.ComboBox cboNgayTrongTuan;
        private System.Windows.Forms.ComboBox cboGioBD;
        private System.Windows.Forms.ComboBox cboGioKT;
        private System.Windows.Forms.Label labelTenBacSi;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelNgayLamViec;
        private System.Windows.Forms.Label labelGioBD;
        private System.Windows.Forms.Label labelGioKT;
        private System.Windows.Forms.Label labelGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.DataGridView dgv;

        #endregion
    }
}
