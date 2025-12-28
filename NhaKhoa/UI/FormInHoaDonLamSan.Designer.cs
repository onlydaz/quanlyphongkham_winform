namespace NhaKhoa.UI
{
    partial class FormInHoaDonLamSan
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
            this.rpInHoaDonLamSan = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpInHoaDonLamSan
            // 
            this.rpInHoaDonLamSan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpInHoaDonLamSan.Location = new System.Drawing.Point(0, 0);
            this.rpInHoaDonLamSan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rpInHoaDonLamSan.Name = "rpInHoaDonLamSan";
            this.rpInHoaDonLamSan.ServerReport.BearerToken = null;
            this.rpInHoaDonLamSan.Size = new System.Drawing.Size(1077, 510);
            this.rpInHoaDonLamSan.TabIndex = 0;
            // 
            // FormInHoaDonLamSan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1077, 510);
            this.Controls.Add(this.rpInHoaDonLamSan);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormInHoaDonLamSan";
            this.Text = "In hóa đơn lâm sàn";
            this.Load += new System.EventHandler(this.FormInHoaDonLamSan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpInHoaDonLamSan;
    }
}

