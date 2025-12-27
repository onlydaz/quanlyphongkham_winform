namespace NhaKhoa.BenhNhan
{
    partial class FRM_Benhnhan
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
            this.label9 = new System.Windows.Forms.Label();
            this.txtLyDoKham = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rbtnNu = new System.Windows.Forms.RadioButton();
            this.rbtnNam = new System.Windows.Forms.RadioButton();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtTenBN = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtMaBN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpNgayKham = new System.Windows.Forms.DateTimePicker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuXoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCapNhat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChiTietLamSan = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDSBN = new System.Windows.Forms.DataGridView();
            this.MaBN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenBN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamSinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiaChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTimKiemBN = new System.Windows.Forms.Button();
            this.txtTimKiemBN = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuLamMoi = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSBN)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label9.Location = new System.Drawing.Point(717, 36);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "ngày khám:";
            // 
            // txtLyDoKham
            // 
            this.txtLyDoKham.Location = new System.Drawing.Point(827, 105);
            this.txtLyDoKham.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLyDoKham.Name = "txtLyDoKham";
            this.txtLyDoKham.Size = new System.Drawing.Size(372, 130);
            this.txtLyDoKham.TabIndex = 15;
            this.txtLyDoKham.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(717, 105);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "Lý do khám";
            // 
            // rbtnNu
            // 
            this.rbtnNu.AutoSize = true;
            this.rbtnNu.Location = new System.Drawing.Point(307, 294);
            this.rbtnNu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.rbtnNam.Location = new System.Drawing.Point(192, 294);
            this.rbtnNam.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtnNam.Name = "rbtnNam";
            this.rbtnNam.Size = new System.Drawing.Size(57, 20);
            this.rbtnNam.TabIndex = 11;
            this.rbtnNam.TabStop = true;
            this.rbtnNam.Text = "Nam";
            this.rbtnNam.UseVisualStyleBackColor = true;
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Location = new System.Drawing.Point(192, 241);
            this.dtpNgaySinh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(317, 22);
            this.dtpNgaySinh.TabIndex = 10;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(192, 186);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(317, 22);
            this.txtDiaChi.TabIndex = 9;
            // 
            // txtTenBN
            // 
            this.txtTenBN.Location = new System.Drawing.Point(192, 82);
            this.txtTenBN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTenBN.Name = "txtTenBN";
            this.txtTenBN.Size = new System.Drawing.Size(317, 22);
            this.txtTenBN.TabIndex = 8;
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(192, 135);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(317, 22);
            this.txtSDT.TabIndex = 7;
            // 
            // txtMaBN
            // 
            this.txtMaBN.Location = new System.Drawing.Point(192, 31);
            this.txtMaBN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMaBN.Name = "txtMaBN";
            this.txtMaBN.Size = new System.Drawing.Size(317, 22);
            this.txtMaBN.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(44, 294);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Giới tính";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(44, 241);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "Ngày sinh";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(44, 187);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Địa chỉ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(44, 137);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Số điện thoại";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(44, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tên bệnh nhân";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(44, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã bệnh nhân";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpNgayKham);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtLyDoKham);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.rbtnNu);
            this.panel1.Controls.Add(this.rbtnNam);
            this.panel1.Controls.Add(this.dtpNgaySinh);
            this.panel1.Controls.Add(this.txtDiaChi);
            this.panel1.Controls.Add(this.txtTenBN);
            this.panel1.Controls.Add(this.txtSDT);
            this.panel1.Controls.Add(this.txtMaBN);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(145, 105);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1475, 389);
            this.panel1.TabIndex = 3;
            // 
            // dtpNgayKham
            // 
            this.dtpNgayKham.Location = new System.Drawing.Point(827, 32);
            this.dtpNgayKham.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpNgayKham.Name = "dtpNgayKham";
            this.dtpNgayKham.Size = new System.Drawing.Size(317, 22);
            this.dtpNgayKham.TabIndex = 22;
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
            this.mnuChiTietLamSan,
            this.mnuLamMoi});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(1672, 39);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuMain
            // 
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(129, 35);
            this.mnuMain.Text = "Xuất file Excel";
            // 
            // mnuXoa
            // 
            this.mnuXoa.Name = "mnuXoa";
            this.mnuXoa.Size = new System.Drawing.Size(53, 35);
            this.mnuXoa.Text = "Xoá";
            this.mnuXoa.Click += new System.EventHandler(this.mnuXoa_Click);
            // 
            // mnuCapNhat
            // 
            this.mnuCapNhat.Name = "mnuCapNhat";
            this.mnuCapNhat.Size = new System.Drawing.Size(94, 35);
            this.mnuCapNhat.Text = "Cập nhật";
            this.mnuCapNhat.Click += new System.EventHandler(this.mnuCapNhat_Click);
            // 
            // mnuChiTietLamSan
            // 
            this.mnuChiTietLamSan.Name = "mnuChiTietLamSan";
            this.mnuChiTietLamSan.Size = new System.Drawing.Size(143, 35);
            this.mnuChiTietLamSan.Text = "Chi tiết lâm sàn";
            this.mnuChiTietLamSan.Click += new System.EventHandler(this.mnuChiTietLamSan_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDSBN);
            this.groupBox2.Controls.Add(this.btnTimKiemBN);
            this.groupBox2.Controls.Add(this.txtTimKiemBN);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox2.Location = new System.Drawing.Point(73, 502);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1547, 462);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách";
            // 
            // dgvDSBN
            // 
            this.dgvDSBN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDSBN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSBN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaBN,
            this.TenBN,
            this.SDT,
            this.GioiTinh,
            this.NamSinh,
            this.DiaChi,
            this.NgayTao,
            this.TongTien,
            this.GhiChu});
            this.dgvDSBN.Location = new System.Drawing.Point(0, 122);
            this.dgvDSBN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDSBN.Name = "dgvDSBN";
            this.dgvDSBN.RowHeadersWidth = 51;
            this.dgvDSBN.Size = new System.Drawing.Size(1547, 331);
            this.dgvDSBN.TabIndex = 3;
            this.dgvDSBN.SelectionChanged += new System.EventHandler(this.dgvDSBN_SelectionChanged);
            // 
            // MaBN
            // 
            this.MaBN.HeaderText = "Mã bệnh nhân";
            this.MaBN.MinimumWidth = 6;
            this.MaBN.Name = "MaBN";
            // 
            // TenBN
            // 
            this.TenBN.HeaderText = "Tên bệnh nhân";
            this.TenBN.MinimumWidth = 6;
            this.TenBN.Name = "TenBN";
            // 
            // SDT
            // 
            this.SDT.HeaderText = "SĐT";
            this.SDT.MinimumWidth = 6;
            this.SDT.Name = "SDT";
            // 
            // GioiTinh
            // 
            this.GioiTinh.HeaderText = "Giới tính";
            this.GioiTinh.MinimumWidth = 6;
            this.GioiTinh.Name = "GioiTinh";
            // 
            // NamSinh
            // 
            this.NamSinh.HeaderText = "Năm sinh";
            this.NamSinh.MinimumWidth = 6;
            this.NamSinh.Name = "NamSinh";
            // 
            // DiaChi
            // 
            this.DiaChi.HeaderText = "Địa chỉ";
            this.DiaChi.MinimumWidth = 6;
            this.DiaChi.Name = "DiaChi";
            // 
            // NgayTao
            // 
            this.NgayTao.HeaderText = "Ngày khám";
            this.NgayTao.MinimumWidth = 6;
            this.NgayTao.Name = "NgayTao";
            // 
            // TongTien
            // 
            this.TongTien.HeaderText = "Tổng tiền";
            this.TongTien.MinimumWidth = 6;
            this.TongTien.Name = "TongTien";
            // 
            // GhiChu
            // 
            this.GhiChu.HeaderText = "Ghi chú";
            this.GhiChu.MinimumWidth = 6;
            this.GhiChu.Name = "GhiChu";
            // 
            // btnTimKiemBN
            // 
            this.btnTimKiemBN.Location = new System.Drawing.Point(409, 74);
            this.btnTimKiemBN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTimKiemBN.Name = "btnTimKiemBN";
            this.btnTimKiemBN.Size = new System.Drawing.Size(97, 27);
            this.btnTimKiemBN.TabIndex = 2;
            this.btnTimKiemBN.Text = "Tìm kiếm";
            this.btnTimKiemBN.UseVisualStyleBackColor = true;
            this.btnTimKiemBN.Click += new System.EventHandler(this.btnTimKiemBN_Click);
            // 
            // txtTimKiemBN
            // 
            this.txtTimKiemBN.Location = new System.Drawing.Point(19, 74);
            this.txtTimKiemBN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTimKiemBN.Name = "txtTimKiemBN";
            this.txtTimKiemBN.Size = new System.Drawing.Size(364, 26);
            this.txtTimKiemBN.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 37);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "Tìm kiếm theo tên";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(83, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 38);
            this.label1.TabIndex = 6;
            this.label1.Text = "QUẢN LÝ BỆNH NHÂN";
            // 
            // mnuLamMoi
            // 
            this.mnuLamMoi.Name = "mnuLamMoi";
            this.mnuLamMoi.Size = new System.Drawing.Size(90, 35);
            this.mnuLamMoi.Text = "Làm mới";
            this.mnuLamMoi.Click += new System.EventHandler(this.mnuLamMoi_Click);
            // 
            // mnuThem
            // 
            this.mnuThem.Name = "mnuThem";
            this.mnuThem.Size = new System.Drawing.Size(67, 35);
            this.mnuThem.Text = "Thêm";
            this.mnuThem.Click += new System.EventHandler(this.mnuThem_Click);
            // 
            // FRM_Benhnhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1672, 1033);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FRM_Benhnhan";
            this.Text = "Quản lý bệnh nhân";
            this.Load += new System.EventHandler(this.FRM_Benhnhan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSBN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox txtLyDoKham;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbtnNu;
        private System.Windows.Forms.RadioButton rbtnNam;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtTenBN;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtMaBN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuXoa;
        private System.Windows.Forms.ToolStripMenuItem mnuCapNhat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDSBN;
        private System.Windows.Forms.Button btnTimKiemBN;
        private System.Windows.Forms.TextBox txtTimKiemBN;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpNgayKham;
        private System.Windows.Forms.ToolStripMenuItem mnuChiTietLamSan;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaBN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenBN;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn GioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamSinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiaChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayTao;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn GhiChu;
        private System.Windows.Forms.ToolStripMenuItem mnuLamMoi;
        private System.Windows.Forms.ToolStripMenuItem mnuThem;
    }
}