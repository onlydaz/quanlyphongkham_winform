namespace NhaKhoa.UI
{
    partial class FRM_Dichvu
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnXoaCD = new System.Windows.Forms.Button();
            this.btnThemCD = new System.Windows.Forms.Button();
            this.txttenCD = new System.Windows.Forms.TextBox();
            this.txtmaCD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvChuanDoan = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnXoaDT = new System.Windows.Forms.Button();
            this.btnThemDT = new System.Windows.Forms.Button();
            this.txtDongia = new System.Windows.Forms.TextBox();
            this.txttenDT = new System.Windows.Forms.TextBox();
            this.txtDVT = new System.Windows.Forms.TextBox();
            this.txtmaDT = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvDieuTri = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuXuatExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCapNhat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLamMoi = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuanDoan)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDieuTri)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(259, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(456, 44);
            this.label1.TabIndex = 4;
            this.label1.Text = "CHẨN ĐOÁN - ĐIỀU TRỊ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnXoaCD);
            this.panel1.Controls.Add(this.btnThemCD);
            this.panel1.Controls.Add(this.txttenCD);
            this.panel1.Controls.Add(this.txtmaCD);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dgvChuanDoan);
            this.panel1.Location = new System.Drawing.Point(85, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 344);
            this.panel1.TabIndex = 5;
            // 
            // btnXoaCD
            // 
            this.btnXoaCD.Location = new System.Drawing.Point(192, 295);
            this.btnXoaCD.Name = "btnXoaCD";
            this.btnXoaCD.Size = new System.Drawing.Size(100, 30);
            this.btnXoaCD.TabIndex = 3;
            this.btnXoaCD.Text = "Xoá CĐ";
            this.btnXoaCD.UseVisualStyleBackColor = true;
            this.btnXoaCD.Click += new System.EventHandler(this.btnXoaCD_Click);
            // 
            // btnThemCD
            // 
            this.btnThemCD.Location = new System.Drawing.Point(36, 295);
            this.btnThemCD.Name = "btnThemCD";
            this.btnThemCD.Size = new System.Drawing.Size(100, 30);
            this.btnThemCD.TabIndex = 3;
            this.btnThemCD.Text = "Thêm CĐ";
            this.btnThemCD.UseVisualStyleBackColor = true;
            this.btnThemCD.Click += new System.EventHandler(this.btnThemCD_Click);
            // 
            // txttenCD
            // 
            this.txttenCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttenCD.Location = new System.Drawing.Point(182, 253);
            this.txttenCD.Name = "txttenCD";
            this.txttenCD.Size = new System.Drawing.Size(146, 20);
            this.txttenCD.TabIndex = 2;
            // 
            // txtmaCD
            // 
            this.txtmaCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtmaCD.Location = new System.Drawing.Point(21, 253);
            this.txtmaCD.Name = "txtmaCD";
            this.txtmaCD.Size = new System.Drawing.Size(138, 20);
            this.txtmaCD.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(179, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tên CĐ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã CĐ";
            // 
            // dgvChuanDoan
            // 
            this.dgvChuanDoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChuanDoan.Location = new System.Drawing.Point(0, 0);
            this.dgvChuanDoan.Name = "dgvChuanDoan";
            this.dgvChuanDoan.Size = new System.Drawing.Size(341, 222);
            this.dgvChuanDoan.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnXoaDT);
            this.panel2.Controls.Add(this.btnThemDT);
            this.panel2.Controls.Add(this.txtDongia);
            this.panel2.Controls.Add(this.txttenDT);
            this.panel2.Controls.Add(this.txtDVT);
            this.panel2.Controls.Add(this.txtmaDT);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.dgvDieuTri);
            this.panel2.Location = new System.Drawing.Point(499, 119);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(530, 392);
            this.panel2.TabIndex = 6;
            // 
            // btnXoaDT
            // 
            this.btnXoaDT.Location = new System.Drawing.Point(282, 331);
            this.btnXoaDT.Name = "btnXoaDT";
            this.btnXoaDT.Size = new System.Drawing.Size(100, 30);
            this.btnXoaDT.TabIndex = 4;
            this.btnXoaDT.Text = "Xoá ĐT";
            this.btnXoaDT.UseVisualStyleBackColor = true;
            this.btnXoaDT.Click += new System.EventHandler(this.btnXoaDT_Click);
            // 
            // btnThemDT
            // 
            this.btnThemDT.Location = new System.Drawing.Point(121, 331);
            this.btnThemDT.Name = "btnThemDT";
            this.btnThemDT.Size = new System.Drawing.Size(100, 30);
            this.btnThemDT.TabIndex = 4;
            this.btnThemDT.Text = "Thêm ĐT";
            this.btnThemDT.UseVisualStyleBackColor = true;
            this.btnThemDT.Click += new System.EventHandler(this.btnThemDT_Click);
            // 
            // txtDongia
            // 
            this.txtDongia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDongia.Location = new System.Drawing.Point(304, 273);
            this.txtDongia.Name = "txtDongia";
            this.txtDongia.Size = new System.Drawing.Size(135, 20);
            this.txtDongia.TabIndex = 4;
            // 
            // txttenDT
            // 
            this.txttenDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttenDT.Location = new System.Drawing.Point(303, 235);
            this.txttenDT.Name = "txttenDT";
            this.txttenDT.Size = new System.Drawing.Size(165, 20);
            this.txttenDT.TabIndex = 4;
            // 
            // txtDVT
            // 
            this.txtDVT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDVT.Location = new System.Drawing.Point(74, 273);
            this.txtDVT.Name = "txtDVT";
            this.txtDVT.Size = new System.Drawing.Size(98, 20);
            this.txtDVT.TabIndex = 4;
            // 
            // txtmaDT
            // 
            this.txtmaDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtmaDT.Location = new System.Drawing.Point(74, 235);
            this.txtmaDT.Name = "txtmaDT";
            this.txtmaDT.Size = new System.Drawing.Size(98, 20);
            this.txtmaDT.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(242, 273);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Đơn giá:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(242, 237);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Tên ĐT:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 273);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "ĐVT:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Mã ĐT:";
            // 
            // dgvDieuTri
            // 
            this.dgvDieuTri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDieuTri.Location = new System.Drawing.Point(0, 0);
            this.dgvDieuTri.Name = "dgvDieuTri";
            this.dgvDieuTri.Size = new System.Drawing.Size(530, 222);
            this.dgvDieuTri.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuXuatExcel,
            this.mnuCapNhat,
            this.mnuLamMoi});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(1064, 32);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuXuatExcel
            // 
            this.mnuXuatExcel.Name = "mnuXuatExcel";
            this.mnuXuatExcel.Size = new System.Drawing.Size(103, 28);
            this.mnuXuatExcel.Text = "Xuất file Excel";
            this.mnuXuatExcel.Click += new System.EventHandler(this.mnuXuatExcel_Click);
            // 
            // mnuCapNhat
            // 
            this.mnuCapNhat.Name = "mnuCapNhat";
            this.mnuCapNhat.Size = new System.Drawing.Size(77, 28);
            this.mnuCapNhat.Text = "Cập nhật";
            this.mnuCapNhat.Click += new System.EventHandler(this.mnuCapNhat_Click);
            // 
            // mnuLamMoi
            // 
            this.mnuLamMoi.Name = "mnuLamMoi";
            this.mnuLamMoi.Size = new System.Drawing.Size(74, 28);
            this.mnuLamMoi.Text = "Làm mới";
            this.mnuLamMoi.Click += new System.EventHandler(this.mnuLamMoi_Click);
            // 
            // FRM_Dichvu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 557);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "FRM_Dichvu";
            this.Text = "Chẩn đoán- Điều trị";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuanDoan)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDieuTri)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvChuanDoan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnXoaCD;
        private System.Windows.Forms.Button btnThemCD;
        private System.Windows.Forms.TextBox txttenCD;
        private System.Windows.Forms.TextBox txtmaCD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDieuTri;
        private System.Windows.Forms.Button btnXoaDT;
        private System.Windows.Forms.Button btnThemDT;
        private System.Windows.Forms.TextBox txtDongia;
        private System.Windows.Forms.TextBox txttenDT;
        private System.Windows.Forms.TextBox txtDVT;
        private System.Windows.Forms.TextBox txtmaDT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuXuatExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuCapNhat;
        private System.Windows.Forms.ToolStripMenuItem mnuLamMoi;
    }
}