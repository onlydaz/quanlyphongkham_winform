namespace NhaKhoa.NhaSi
{
    partial class FRM_Nhasi
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvBenhNhan = new System.Windows.Forms.DataGridView();
            this.btnChiTietLamSan = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTimKiemSDT = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTimKiemTen = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTimKiemMa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnXuatExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.btnKTKham = new System.Windows.Forms.ToolStripMenuItem();
            this.btnKham = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChiTietLamSan = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvBenhNhanDangKham = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBenhNhan)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBenhNhanDangKham)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(315, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(710, 44);
            this.label1.TabIndex = 6;
            this.label1.Text = "DANH SÁCH BỆNH NHÂN CHỜ KHÁM";
            // 
            // dgvBenhNhan
            // 
            this.dgvBenhNhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBenhNhan.Location = new System.Drawing.Point(56, 142);
            this.dgvBenhNhan.Name = "dgvBenhNhan";
            this.dgvBenhNhan.RowHeadersWidth = 51;
            this.dgvBenhNhan.Size = new System.Drawing.Size(1258, 191);
            this.dgvBenhNhan.TabIndex = 7;
            // 
            // btnChiTietLamSan
            // 
            this.btnChiTietLamSan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChiTietLamSan.Location = new System.Drawing.Point(1032, 657);
            this.btnChiTietLamSan.Name = "btnChiTietLamSan";
            this.btnChiTietLamSan.Size = new System.Drawing.Size(150, 40);
            this.btnChiTietLamSan.TabIndex = 8;
            this.btnChiTietLamSan.Text = "Chi tiết lâm sàn";
            this.btnChiTietLamSan.UseVisualStyleBackColor = true;
            this.btnChiTietLamSan.Click += new System.EventHandler(this.btnChiTietLamSan_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.panel1.Controls.Add(this.txtTimKiemSDT);
            this.panel1.Controls.Add(this.btnTimKiem);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtTimKiemTen);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtTimKiemMa);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(303, 745);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(748, 100);
            this.panel1.TabIndex = 9;
            // 
            // txtTimKiemSDT
            // 
            this.txtTimKiemSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiemSDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiemSDT.Location = new System.Drawing.Point(587, 44);
            this.txtTimKiemSDT.Name = "txtTimKiemSDT";
            this.txtTimKiemSDT.Size = new System.Drawing.Size(125, 26);
            this.txtTimKiemSDT.TabIndex = 6;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(23, 40);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(120, 30);
            this.btnTimKiem.TabIndex = 8;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(527, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "SĐT:";
            // 
            // txtTimKiemTen
            // 
            this.txtTimKiemTen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiemTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiemTen.Location = new System.Drawing.Point(380, 44);
            this.txtTimKiemTen.Name = "txtTimKiemTen";
            this.txtTimKiemTen.Size = new System.Drawing.Size(124, 26);
            this.txtTimKiemTen.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(325, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tên:";
            // 
            // txtTimKiemMa
            // 
            this.txtTimKiemMa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiemMa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiemMa.Location = new System.Drawing.Point(228, 44);
            this.txtTimKiemMa.Name = "txtTimKiemMa";
            this.txtTimKiemMa.Size = new System.Drawing.Size(79, 26);
            this.txtTimKiemMa.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(183, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Mã: ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnXuatExcel,
            this.btnKTKham,
            this.btnKham,
            this.btn_refresh,
            this.menuChiTietLamSan});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(1443, 32);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(103, 28);
            this.btnXuatExcel.Text = "Xuất file Excel";
            // 
            // btnKTKham
            // 
            this.btnKTKham.Name = "btnKTKham";
            this.btnKTKham.Size = new System.Drawing.Size(110, 28);
            this.btnKTKham.Text = "Kết thúc khám";
            // 
            // btnKham
            // 
            this.btnKham.Name = "btnKham";
            this.btnKham.Size = new System.Drawing.Size(106, 28);
            this.btnKham.Text = "Bắt đầu khám";
            // 
            // btn_refresh
            // 
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(74, 28);
            this.btn_refresh.Text = "Làm mới";
            // 
            // menuChiTietLamSan
            // 
            this.menuChiTietLamSan.Name = "menuChiTietLamSan";
            this.menuChiTietLamSan.Size = new System.Drawing.Size(116, 28);
            this.menuChiTietLamSan.Text = "Chi tiết lâm sàn";
            this.menuChiTietLamSan.Click += new System.EventHandler(this.menuChiTietLamSan_Click);
            // 
            // dgvBenhNhanDangKham
            // 
            this.dgvBenhNhanDangKham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBenhNhanDangKham.Location = new System.Drawing.Point(56, 436);
            this.dgvBenhNhanDangKham.Name = "dgvBenhNhanDangKham";
            this.dgvBenhNhanDangKham.RowHeadersWidth = 51;
            this.dgvBenhNhanDangKham.Size = new System.Drawing.Size(1258, 191);
            this.dgvBenhNhanDangKham.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(315, 365);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(736, 44);
            this.label2.TabIndex = 6;
            this.label2.Text = "DANH SÁCH BỆNH NHÂN ĐANG KHÁM";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(191, 745);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Chức năng:";
            // 
            // FRM_Nhasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 857);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnChiTietLamSan);
            this.Controls.Add(this.dgvBenhNhanDangKham);
            this.Controls.Add(this.dgvBenhNhan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FRM_Nhasi";
            this.Text = "Nha sĩ";
            this.Load += new System.EventHandler(this.FRM_Nhasi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBenhNhan)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBenhNhanDangKham)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvBenhNhan;
        private System.Windows.Forms.Button btnChiTietLamSan;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtTimKiemSDT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTimKiemTen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTimKiemMa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnXuatExcel;
        private System.Windows.Forms.ToolStripMenuItem btnKTKham;
        private System.Windows.Forms.ToolStripMenuItem btnKham;
        private System.Windows.Forms.ToolStripMenuItem btn_refresh;
        private System.Windows.Forms.DataGridView dgvBenhNhanDangKham;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem menuChiTietLamSan;
        private System.Windows.Forms.Label label6;
    }
}