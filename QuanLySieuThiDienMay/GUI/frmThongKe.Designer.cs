namespace GUI
{
    partial class frmThongKe
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThongKe));
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.cboChonLoai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel10 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnThoat = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnXuatFile = new Guna.UI2.WinForms.Guna2Button();
            this.dtaNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel3.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 35;
            this.guna2Elipse1.TargetControl = this;
            // 
            // cboChonLoai
            // 
            this.cboChonLoai.AutoRoundedCorners = true;
            this.cboChonLoai.BackColor = System.Drawing.Color.Transparent;
            this.cboChonLoai.BorderRadius = 17;
            this.cboChonLoai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboChonLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChonLoai.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboChonLoai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboChonLoai.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboChonLoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboChonLoai.ItemHeight = 30;
            this.cboChonLoai.Location = new System.Drawing.Point(248, 18);
            this.cboChonLoai.Name = "cboChonLoai";
            this.cboChonLoai.Size = new System.Drawing.Size(214, 36);
            this.cboChonLoai.TabIndex = 6;
            this.cboChonLoai.SelectedIndexChanged += new System.EventHandler(this.cboChonLoai_SelectedIndexChanged);
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel3.BorderColor = System.Drawing.Color.White;
            this.guna2Panel3.BorderRadius = 15;
            this.guna2Panel3.BorderThickness = 2;
            this.guna2Panel3.Controls.Add(this.guna2HtmlLabel10);
            this.guna2Panel3.Controls.Add(this.btnThoat);
            this.guna2Panel3.Location = new System.Drawing.Point(12, 12);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(1617, 46);
            this.guna2Panel3.TabIndex = 58;
            // 
            // guna2HtmlLabel10
            // 
            this.guna2HtmlLabel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2HtmlLabel10.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel10.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel10.ForeColor = System.Drawing.Color.Red;
            this.guna2HtmlLabel10.Location = new System.Drawing.Point(682, 5);
            this.guna2HtmlLabel10.Name = "guna2HtmlLabel10";
            this.guna2HtmlLabel10.Size = new System.Drawing.Size(244, 37);
            this.guna2HtmlLabel10.TabIndex = 32;
            this.guna2HtmlLabel10.Text = "Quản lý thống kê";
            // 
            // btnThoat
            // 
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnThoat.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.btnThoat.HoverState.ImageSize = new System.Drawing.Size(30, 30);
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnThoat.ImageRotate = 0F;
            this.btnThoat.ImageSize = new System.Drawing.Size(30, 30);
            this.btnThoat.Location = new System.Drawing.Point(1554, 5);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.btnThoat.PressedState.ImageSize = new System.Drawing.Size(30, 30);
            this.btnThoat.Size = new System.Drawing.Size(44, 36);
            this.btnThoat.TabIndex = 17;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel1.BorderColor = System.Drawing.Color.White;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 2;
            this.guna2Panel1.Controls.Add(this.chart1);
            this.guna2Panel1.Controls.Add(this.btnXuatFile);
            this.guna2Panel1.Controls.Add(this.dtaNgay);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2Panel1.Controls.Add(this.cboChonLoai);
            this.guna2Panel1.Location = new System.Drawing.Point(12, 64);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1617, 790);
            this.guna2Panel1.TabIndex = 59;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(17, 125);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(1581, 647);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "chart1";
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.BorderRadius = 15;
            this.btnXuatFile.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatFile.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatFile.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXuatFile.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXuatFile.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatFile.ForeColor = System.Drawing.Color.White;
            this.btnXuatFile.HoverState.FillColor = System.Drawing.Color.Lime;
            this.btnXuatFile.Image = ((System.Drawing.Image)(resources.GetObject("btnXuatFile.Image")));
            this.btnXuatFile.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnXuatFile.ImageSize = new System.Drawing.Size(30, 30);
            this.btnXuatFile.Location = new System.Drawing.Point(17, 69);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(200, 36);
            this.btnXuatFile.TabIndex = 55;
            this.btnXuatFile.Text = "Xuất file excel";
            this.btnXuatFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // dtaNgay
            // 
            this.dtaNgay.Animated = true;
            this.dtaNgay.AutoRoundedCorners = true;
            this.dtaNgay.BackColor = System.Drawing.Color.Transparent;
            this.dtaNgay.BorderRadius = 17;
            this.dtaNgay.Checked = true;
            this.dtaNgay.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtaNgay.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtaNgay.IndicateFocus = true;
            this.dtaNgay.Location = new System.Drawing.Point(248, 69);
            this.dtaNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtaNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtaNgay.Name = "dtaNgay";
            this.dtaNgay.Size = new System.Drawing.Size(214, 36);
            this.dtaNgay.TabIndex = 53;
            this.dtaNgay.UseTransparentBackground = true;
            this.dtaNgay.Value = new System.DateTime(2024, 6, 25, 22, 11, 34, 754);
            this.dtaNgay.ValueChanged += new System.EventHandler(this.dtaNgay_ValueChanged);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(17, 25);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(201, 21);
            this.guna2HtmlLabel1.TabIndex = 30;
            this.guna2HtmlLabel1.Text = "Thống kê doanh thu theo";
            // 
            // frmThongKe
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(1641, 866);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.guna2Panel3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmThongKe";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel3.PerformLayout();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2ComboBox cboChonLoai;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel10;
        private Guna.UI2.WinForms.Guna2ImageButton btnThoat;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtaNgay;
        private Guna.UI2.WinForms.Guna2Button btnXuatFile;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}