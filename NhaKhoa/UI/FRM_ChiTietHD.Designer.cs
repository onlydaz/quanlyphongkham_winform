namespace NhaKhoa.Hoadon
{
    partial class FRM_ChiTietHD
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.lblBenhNhan = new System.Windows.Forms.Label();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.lblMaHD_Title = new System.Windows.Forms.Label();
            this.lblNgayLap_Title = new System.Windows.Forms.Label();
            this.lblBenhNhan_Title = new System.Windows.Forms.Label();
            this.lblNhanVien_Title = new System.Windows.Forms.Label();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(16, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(900, 28);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Chi tiết hoá đơn";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTongTien);
            this.groupBox1.Controls.Add(this.lblNhanVien);
            this.groupBox1.Controls.Add(this.lblBenhNhan);
            this.groupBox1.Controls.Add(this.lblNgayLap);
            this.groupBox1.Controls.Add(this.lblMaHD);
            this.groupBox1.Controls.Add(this.lblMaHD_Title);
            this.groupBox1.Controls.Add(this.lblNgayLap_Title);
            this.groupBox1.Controls.Add(this.lblBenhNhan_Title);
            this.groupBox1.Controls.Add(this.lblNhanVien_Title);
            this.groupBox1.Location = new System.Drawing.Point(16, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 110);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin hoá đơn";
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.Location = new System.Drawing.Point(500, 25);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(97, 18);
            this.lblTongTien.TabIndex = 4;
            this.lblTongTien.Text = "Tổng tiền: 0";
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Location = new System.Drawing.Point(322, 45);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(67, 16);
            this.lblNhanVien.TabIndex = 3;
            this.lblNhanVien.Text = "Nhân viên";
            // 
            // lblBenhNhan
            // 
            this.lblBenhNhan.AutoSize = true;
            this.lblBenhNhan.Location = new System.Drawing.Point(322, 25);
            this.lblBenhNhan.Name = "lblBenhNhan";
            this.lblBenhNhan.Size = new System.Drawing.Size(70, 16);
            this.lblBenhNhan.TabIndex = 2;
            this.lblBenhNhan.Text = "Bệnh nhân";
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(110, 45);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(62, 16);
            this.lblNgayLap.TabIndex = 1;
            this.lblNgayLap.Text = "Ngày lập";
            // 
            // lblMaHD
            // 
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Location = new System.Drawing.Point(110, 25);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(46, 16);
            this.lblMaHD.TabIndex = 0;
            this.lblMaHD.Text = "MãHD";
            // 
            // lblMaHD_Title
            // 
            this.lblMaHD_Title.AutoSize = true;
            this.lblMaHD_Title.Location = new System.Drawing.Point(12, 25);
            this.lblMaHD_Title.Name = "lblMaHD_Title";
            this.lblMaHD_Title.Size = new System.Drawing.Size(81, 16);
            this.lblMaHD_Title.TabIndex = 0;
            this.lblMaHD_Title.Text = "Mã hoá đơn:";
            // 
            // lblNgayLap_Title
            // 
            this.lblNgayLap_Title.AutoSize = true;
            this.lblNgayLap_Title.Location = new System.Drawing.Point(12, 45);
            this.lblNgayLap_Title.Name = "lblNgayLap_Title";
            this.lblNgayLap_Title.Size = new System.Drawing.Size(65, 16);
            this.lblNgayLap_Title.TabIndex = 1;
            this.lblNgayLap_Title.Text = "Ngày lập:";
            // 
            // lblBenhNhan_Title
            // 
            this.lblBenhNhan_Title.AutoSize = true;
            this.lblBenhNhan_Title.Location = new System.Drawing.Point(242, 25);
            this.lblBenhNhan_Title.Name = "lblBenhNhan_Title";
            this.lblBenhNhan_Title.Size = new System.Drawing.Size(73, 16);
            this.lblBenhNhan_Title.TabIndex = 2;
            this.lblBenhNhan_Title.Text = "Bệnh nhân:";
            // 
            // lblNhanVien_Title
            // 
            this.lblNhanVien_Title.AutoSize = true;
            this.lblNhanVien_Title.Location = new System.Drawing.Point(242, 45);
            this.lblNhanVien_Title.Name = "lblNhanVien_Title";
            this.lblNhanVien_Title.Size = new System.Drawing.Size(49, 16);
            this.lblNhanVien_Title.TabIndex = 3;
            this.lblNhanVien_Title.Text = "Bác sĩ:";
            // 
            // dgvDetails
            // 
            this.dgvDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Location = new System.Drawing.Point(16, 170);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.RowHeadersWidth = 51;
            this.dgvDetails.Size = new System.Drawing.Size(900, 360);
            this.dgvDetails.TabIndex = 2;
            // 
            // FRM_ChiTietHD
            // 
            this.ClientSize = new System.Drawing.Size(940, 550);
            this.Controls.Add(this.dgvDetails);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHeader);
            this.Name = "FRM_ChiTietHD";
            this.Text = "Chi tiết hoá đơn";
            this.Load += new System.EventHandler(this.FRM_ChiTietHD_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMaHD_Title;
        private System.Windows.Forms.Label lblNgayLap_Title;
        private System.Windows.Forms.Label lblBenhNhan_Title;
        private System.Windows.Forms.Label lblNhanVien_Title;
        private System.Windows.Forms.Label lblMaHD;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.Label lblBenhNhan;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.DataGridView dgvDetails;
    }
}
