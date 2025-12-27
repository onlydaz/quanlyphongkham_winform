namespace NhaKhoa.NhanVien
{
    partial class FRM_Nhanvien
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboChucVu = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rbtnNu = new System.Windows.Forms.RadioButton();
            this.rbtnNam = new System.Windows.Forms.RadioButton();
            this.dtpNgayVaoLam = new System.Windows.Forms.DateTimePicker();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDSNV = new System.Windows.Forms.DataGridView();
            this.frm_NVMaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frm_NVTenNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frm_NVChucVu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frm_NVGioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frm_NVNamSinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frm_NVSDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frm_NVEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frm_NVDiaChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTimKiemNV = new System.Windows.Forms.Button();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuXoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLamMoi = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCapNhat = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSNV)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboChucVu
            // 
            this.cboChucVu.FormattingEnabled = true;
            this.cboChucVu.Location = new System.Drawing.Point(194, 148);
            this.cboChucVu.Margin = new System.Windows.Forms.Padding(4);
            this.cboChucVu.Name = "cboChucVu";
            this.cboChucVu.Size = new System.Drawing.Size(317, 24);
            this.cboChucVu.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(46, 150);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "Chức vụ";
            // 
            // rbtnNu
            // 
            this.rbtnNu.AutoSize = true;
            this.rbtnNu.Location = new System.Drawing.Point(787, 89);
            this.rbtnNu.Margin = new System.Windows.Forms.Padding(4);
            this.rbtnNu.Name = "rbtnNu";
            this.rbtnNu.Size = new System.Drawing.Size(45, 20);
            this.rbtnNu.TabIndex = 12;
            this.rbtnNu.Text = "Nữ";
            this.rbtnNu.UseVisualStyleBackColor = true;
            // 
            // rbtnNam
            // 
            this.rbtnNam.AutoSize = true;
            this.rbtnNam.Checked = true;
            this.rbtnNam.Location = new System.Drawing.Point(672, 89);
            this.rbtnNam.Margin = new System.Windows.Forms.Padding(4);
            this.rbtnNam.Name = "rbtnNam";
            this.rbtnNam.Size = new System.Drawing.Size(57, 20);
            this.rbtnNam.TabIndex = 11;
            this.rbtnNam.TabStop = true;
            this.rbtnNam.Text = "Nam";
            this.rbtnNam.UseVisualStyleBackColor = true;
            // 
            // dtpNgayVaoLam
            // 
            this.dtpNgayVaoLam.Location = new System.Drawing.Point(672, 36);
            this.dtpNgayVaoLam.Margin = new System.Windows.Forms.Padding(4);
            this.dtpNgayVaoLam.Name = "dtpNgayVaoLam";
            this.dtpNgayVaoLam.Size = new System.Drawing.Size(347, 22);
            this.dtpNgayVaoLam.TabIndex = 10;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(672, 145);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(347, 22);
            this.txtEmail.TabIndex = 9;
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(194, 207);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(4);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(317, 22);
            this.txtSDT.TabIndex = 9;
            // 
            // txtTenNV
            // 
            this.txtTenNV.Location = new System.Drawing.Point(194, 91);
            this.txtTenNV.Margin = new System.Windows.Forms.Padding(4);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.Size = new System.Drawing.Size(317, 22);
            this.txtTenNV.TabIndex = 8;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(672, 206);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(347, 22);
            this.txtDiaChi.TabIndex = 7;
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(194, 39);
            this.txtMaNV.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(317, 22);
            this.txtMaNV.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(572, 93);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Giới tính";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label11.Location = new System.Drawing.Point(572, 150);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 20);
            this.label11.TabIndex = 3;
            this.label11.Text = "Email";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(533, 41);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "Ngày vào làm";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label9.Location = new System.Drawing.Point(46, 210);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 20);
            this.label9.TabIndex = 2;
            this.label9.Text = "Số điện thoại";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(46, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tên nhân viên";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(46, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã nhân viên";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 89);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 38);
            this.label1.TabIndex = 4;
            this.label1.Text = "Quản lý nhân viên";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(572, 210);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Địa chỉ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboChucVu);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.rbtnNu);
            this.panel1.Controls.Add(this.rbtnNam);
            this.panel1.Controls.Add(this.dtpNgayVaoLam);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.txtSDT);
            this.panel1.Controls.Add(this.txtTenNV);
            this.panel1.Controls.Add(this.txtDiaChi);
            this.panel1.Controls.Add(this.txtMaNV);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(156, 120);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1471, 349);
            this.panel1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDSNV);
            this.groupBox2.Controls.Add(this.btnTimKiemNV);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox2.Location = new System.Drawing.Point(56, 493);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1571, 434);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách";
            // 
            // dgvDSNV
            // 
            this.dgvDSNV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDSNV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSNV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.frm_NVMaNV,
            this.frm_NVTenNV,
            this.frm_NVChucVu,
            this.frm_NVGioiTinh,
            this.frm_NVNamSinh,
            this.frm_NVSDT,
            this.frm_NVEmail,
            this.frm_NVDiaChi});
            this.dgvDSNV.Location = new System.Drawing.Point(19, 122);
            this.dgvDSNV.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDSNV.Name = "dgvDSNV";
            this.dgvDSNV.RowHeadersWidth = 51;
            this.dgvDSNV.Size = new System.Drawing.Size(1544, 305);
            this.dgvDSNV.TabIndex = 3;
            this.dgvDSNV.SelectionChanged += new System.EventHandler(this.dgvDSNV_SelectionChanged);
            // 
            // frm_NVMaNV
            // 
            this.frm_NVMaNV.HeaderText = "Mã nhân viên";
            this.frm_NVMaNV.MinimumWidth = 6;
            this.frm_NVMaNV.Name = "frm_NVMaNV";
            // 
            // frm_NVTenNV
            // 
            this.frm_NVTenNV.HeaderText = "Tên nhân viên";
            this.frm_NVTenNV.MinimumWidth = 6;
            this.frm_NVTenNV.Name = "frm_NVTenNV";
            // 
            // frm_NVChucVu
            // 
            this.frm_NVChucVu.HeaderText = "Chức vụ";
            this.frm_NVChucVu.MinimumWidth = 6;
            this.frm_NVChucVu.Name = "frm_NVChucVu";
            // 
            // frm_NVGioiTinh
            // 
            this.frm_NVGioiTinh.HeaderText = "Giới tính";
            this.frm_NVGioiTinh.MinimumWidth = 6;
            this.frm_NVGioiTinh.Name = "frm_NVGioiTinh";
            // 
            // frm_NVNamSinh
            // 
            this.frm_NVNamSinh.HeaderText = "Năm sinh";
            this.frm_NVNamSinh.MinimumWidth = 6;
            this.frm_NVNamSinh.Name = "frm_NVNamSinh";
            // 
            // frm_NVSDT
            // 
            this.frm_NVSDT.HeaderText = "SĐT";
            this.frm_NVSDT.MinimumWidth = 6;
            this.frm_NVSDT.Name = "frm_NVSDT";
            // 
            // frm_NVEmail
            // 
            this.frm_NVEmail.HeaderText = "Email";
            this.frm_NVEmail.MinimumWidth = 6;
            this.frm_NVEmail.Name = "frm_NVEmail";
            // 
            // frm_NVDiaChi
            // 
            this.frm_NVDiaChi.HeaderText = "Địa chỉ";
            this.frm_NVDiaChi.MinimumWidth = 6;
            this.frm_NVDiaChi.Name = "frm_NVDiaChi";
            // 
            // btnTimKiemNV
            // 
            this.btnTimKiemNV.Location = new System.Drawing.Point(409, 74);
            this.btnTimKiemNV.Margin = new System.Windows.Forms.Padding(4);
            this.btnTimKiemNV.Name = "btnTimKiemNV";
            this.btnTimKiemNV.Size = new System.Drawing.Size(97, 27);
            this.btnTimKiemNV.TabIndex = 2;
            this.btnTimKiemNV.Text = "Tìm kiếm";
            this.btnTimKiemNV.UseVisualStyleBackColor = true;
            this.btnTimKiemNV.Click += new System.EventHandler(this.btnTimKiemNV_Click);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(19, 74);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(364, 26);
            this.textBox7.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tìm kiếm theo tên";
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain,
            this.mnuXoa,
            this.mnuCapNhat,
            this.mnuThem,
            this.mnuLamMoi});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(1672, 40);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuMain
            // 
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(129, 36);
            this.mnuMain.Text = "Xuất file Excel";
            // 
            // mnuXoa
            // 
            this.mnuXoa.Name = "mnuXoa";
            this.mnuXoa.Size = new System.Drawing.Size(53, 36);
            this.mnuXoa.Text = "Xoá";
            this.mnuXoa.Click += new System.EventHandler(this.mnuXoa_Click);
            // 
            // mnuThem
            // 
            this.mnuThem.Name = "mnuThem";
            this.mnuThem.Size = new System.Drawing.Size(67, 36);
            this.mnuThem.Text = "Thêm";
            this.mnuThem.Click += new System.EventHandler(this.mnuThem_Click);
            // 
            // mnuLamMoi
            // 
            this.mnuLamMoi.Name = "mnuLamMoi";
            this.mnuLamMoi.Size = new System.Drawing.Size(90, 36);
            this.mnuLamMoi.Text = "Làm mới";
            this.mnuLamMoi.Click += new System.EventHandler(this.mnuLamMoi_Click);
            // 
            // mnuCapNhat
            // 
            this.mnuCapNhat.Name = "mnuCapNhat";
            this.mnuCapNhat.Size = new System.Drawing.Size(94, 36);
            this.mnuCapNhat.Text = "Cập nhật";
            this.mnuCapNhat.Click += new System.EventHandler(this.mnuCapNhat_Click);
            // 
            // FRM_Nhanvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1672, 1033);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "FRM_Nhanvien";
            this.Text = "Quản lý nhân viên";
            this.Load += new System.EventHandler(this.FRM_Nhanvien_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSNV)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboChucVu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbtnNu;
        private System.Windows.Forms.RadioButton rbtnNam;
        private System.Windows.Forms.DateTimePicker dtpNgayVaoLam;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDSNV;
        private System.Windows.Forms.Button btnTimKiemNV;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuXoa;
        private System.Windows.Forms.ToolStripMenuItem mnuThem;
        private System.Windows.Forms.DataGridViewTextBoxColumn frm_NVMaNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn frm_NVTenNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn frm_NVChucVu;
        private System.Windows.Forms.DataGridViewTextBoxColumn frm_NVGioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn frm_NVNamSinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn frm_NVSDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn frm_NVEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn frm_NVDiaChi;
        private System.Windows.Forms.ToolStripMenuItem mnuCapNhat;
        private System.Windows.Forms.ToolStripMenuItem mnuLamMoi;
    }
}