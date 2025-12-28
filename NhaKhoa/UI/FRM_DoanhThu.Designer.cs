namespace NhaKhoa.UI
{
    partial class FRM_DoanhThu
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnQuy = new System.Windows.Forms.RadioButton();
            this.rbtnNam = new System.Windows.Forms.RadioButton();
            this.rbtnThang = new System.Windows.Forms.RadioButton();
            this.ChartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartDoanhThu)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(517, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "DOANH THU";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnQuy);
            this.groupBox1.Controls.Add(this.rbtnNam);
            this.groupBox1.Controls.Add(this.rbtnThang);
            this.groupBox1.Location = new System.Drawing.Point(78, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 59);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Doanh Thu";
            // 
            // rbtnQuy
            // 
            this.rbtnQuy.AutoSize = true;
            this.rbtnQuy.Location = new System.Drawing.Point(266, 27);
            this.rbtnQuy.Name = "rbtnQuy";
            this.rbtnQuy.Size = new System.Drawing.Size(72, 17);
            this.rbtnQuy.TabIndex = 0;
            this.rbtnQuy.TabStop = true;
            this.rbtnQuy.Text = "Theo Quý";
            this.rbtnQuy.UseVisualStyleBackColor = true;
            this.rbtnQuy.CheckedChanged += new System.EventHandler(this.rbtnQuy_CheckedChanged);
            // 
            // rbtnNam
            // 
            this.rbtnNam.AutoSize = true;
            this.rbtnNam.Location = new System.Drawing.Point(141, 27);
            this.rbtnNam.Name = "rbtnNam";
            this.rbtnNam.Size = new System.Drawing.Size(75, 17);
            this.rbtnNam.TabIndex = 0;
            this.rbtnNam.TabStop = true;
            this.rbtnNam.Text = "Theo Năm";
            this.rbtnNam.UseVisualStyleBackColor = true;
            this.rbtnNam.CheckedChanged += new System.EventHandler(this.rbtnNam_CheckedChanged);
            // 
            // rbtnThang
            // 
            this.rbtnThang.AutoSize = true;
            this.rbtnThang.Location = new System.Drawing.Point(10, 27);
            this.rbtnThang.Name = "rbtnThang";
            this.rbtnThang.Size = new System.Drawing.Size(84, 17);
            this.rbtnThang.TabIndex = 0;
            this.rbtnThang.TabStop = true;
            this.rbtnThang.Text = "Theo Tháng";
            this.rbtnThang.UseVisualStyleBackColor = true;
            this.rbtnThang.CheckedChanged += new System.EventHandler(this.rbtnThang_CheckedChanged);
            // 
            // ChartDoanhThu
            // 
            chartArea4.Name = "ChartArea1";
            this.ChartDoanhThu.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.ChartDoanhThu.Legends.Add(legend4);
            this.ChartDoanhThu.Location = new System.Drawing.Point(2, 140);
            this.ChartDoanhThu.Name = "ChartDoanhThu";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.ChartDoanhThu.Series.Add(series4);
            this.ChartDoanhThu.Size = new System.Drawing.Size(1266, 588);
            this.ChartDoanhThu.TabIndex = 2;
            this.ChartDoanhThu.Text = "chart1";
            // 
            // FRM_DoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 740);
            this.Controls.Add(this.ChartDoanhThu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "FRM_DoanhThu";
            this.Text = "Doanh Thu";
            this.Load += new System.EventHandler(this.FRM_DoanhThu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartDoanhThu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnQuy;
        private System.Windows.Forms.RadioButton rbtnNam;
        private System.Windows.Forms.RadioButton rbtnThang;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartDoanhThu;
    }
}