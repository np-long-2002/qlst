namespace GUI
{
	partial class DANGNHAP
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DANGNHAP));
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtDNhap = new Guna.UI2.WinForms.Guna2HtmlLabel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.txtBoxTK = new Guna.UI2.WinForms.Guna2TextBox();
			this.imgAcc = new Guna.UI2.WinForms.Guna2ImageButton();
			this.panel4 = new System.Windows.Forms.Panel();
			this.guna2ImageButton2 = new Guna.UI2.WinForms.Guna2ImageButton();
			this.panel2 = new System.Windows.Forms.Panel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnClose = new Guna.UI2.WinForms.Guna2ImageButton();
			this.txtBoxMK = new Guna.UI2.WinForms.Guna2TextBox();
			this.guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
			this.btnDN = new Guna.UI2.WinForms.Guna2GradientButton();
			this.panel1.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.panel1.Controls.Add(this.btnDN);
			this.panel1.Controls.Add(this.txtDNhap);
			this.panel1.Controls.Add(this.panel5);
			this.panel1.Controls.Add(this.panel4);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(327, 359);
			this.panel1.TabIndex = 0;
			// 
			// txtDNhap
			// 
			this.txtDNhap.BackColor = System.Drawing.Color.Transparent;
			this.txtDNhap.ForeColor = System.Drawing.Color.Yellow;
			this.txtDNhap.Location = new System.Drawing.Point(129, 46);
			this.txtDNhap.Name = "txtDNhap";
			this.txtDNhap.Size = new System.Drawing.Size(82, 18);
			this.txtDNhap.TabIndex = 3;
			this.txtDNhap.Text = "ĐĂNG NHẬP";
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.Transparent;
			this.panel5.Controls.Add(this.txtBoxMK);
			this.panel5.Controls.Add(this.guna2ImageButton1);
			this.panel5.Controls.Add(this.txtBoxTK);
			this.panel5.Controls.Add(this.imgAcc);
			this.panel5.Location = new System.Drawing.Point(3, 194);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(321, 97);
			this.panel5.TabIndex = 2;
			// 
			// txtBoxTK
			// 
			this.txtBoxTK.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtBoxTK.DefaultText = "";
			this.txtBoxTK.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtBoxTK.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtBoxTK.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtBoxTK.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtBoxTK.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtBoxTK.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.txtBoxTK.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtBoxTK.Location = new System.Drawing.Point(48, 5);
			this.txtBoxTK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtBoxTK.Name = "txtBoxTK";
			this.txtBoxTK.PasswordChar = '\0';
			this.txtBoxTK.PlaceholderText = "";
			this.txtBoxTK.SelectedText = "";
			this.txtBoxTK.Size = new System.Drawing.Size(247, 31);
			this.txtBoxTK.TabIndex = 1;
			// 
			// imgAcc
			// 
			this.imgAcc.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
			this.imgAcc.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
			this.imgAcc.HoverState.ImageSize = new System.Drawing.Size(32, 32);
			this.imgAcc.Image = ((System.Drawing.Image)(resources.GetObject("imgAcc.Image")));
			this.imgAcc.ImageOffset = new System.Drawing.Point(0, 0);
			this.imgAcc.ImageRotate = 0F;
			this.imgAcc.ImageSize = new System.Drawing.Size(32, 32);
			this.imgAcc.IndicateFocus = true;
			this.imgAcc.Location = new System.Drawing.Point(3, 3);
			this.imgAcc.Name = "imgAcc";
			this.imgAcc.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image3")));
			this.imgAcc.PressedState.ImageSize = new System.Drawing.Size(32, 32);
			this.imgAcc.Size = new System.Drawing.Size(38, 41);
			this.imgAcc.TabIndex = 0;
			this.imgAcc.UseTransparentBackground = true;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.Transparent;
			this.panel4.Controls.Add(this.guna2ImageButton2);
			this.panel4.Location = new System.Drawing.Point(3, 70);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(324, 118);
			this.panel4.TabIndex = 1;
			// 
			// guna2ImageButton2
			// 
			this.guna2ImageButton2.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
			this.guna2ImageButton2.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image4")));
			this.guna2ImageButton2.HoverState.ImageSize = new System.Drawing.Size(64, 64);
			this.guna2ImageButton2.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageButton2.Image")));
			this.guna2ImageButton2.ImageOffset = new System.Drawing.Point(0, 0);
			this.guna2ImageButton2.ImageRotate = 0F;
			this.guna2ImageButton2.IndicateFocus = true;
			this.guna2ImageButton2.Location = new System.Drawing.Point(0, 2);
			this.guna2ImageButton2.Name = "guna2ImageButton2";
			this.guna2ImageButton2.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image5")));
			this.guna2ImageButton2.PressedState.ImageSize = new System.Drawing.Size(64, 64);
			this.guna2ImageButton2.Size = new System.Drawing.Size(321, 116);
			this.guna2ImageButton2.TabIndex = 0;
			this.guna2ImageButton2.UseTransparentBackground = true;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Transparent;
			this.panel2.Controls.Add(this.flowLayoutPanel1);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Controls.Add(this.btnClose);
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(327, 40);
			this.panel2.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 43);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(324, 160);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// panel3
			// 
			this.panel3.Location = new System.Drawing.Point(0, 46);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(200, 100);
			this.panel3.TabIndex = 1;
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.Transparent;
			this.btnClose.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
			this.btnClose.HoverState.ImageSize = new System.Drawing.Size(30, 30);
			this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
			this.btnClose.ImageOffset = new System.Drawing.Point(0, 0);
			this.btnClose.ImageRotate = 0F;
			this.btnClose.ImageSize = new System.Drawing.Size(30, 30);
			this.btnClose.IndicateFocus = true;
			this.btnClose.Location = new System.Drawing.Point(291, 5);
			this.btnClose.Name = "btnClose";
			this.btnClose.PressedState.ImageSize = new System.Drawing.Size(30, 30);
			this.btnClose.Size = new System.Drawing.Size(33, 32);
			this.btnClose.TabIndex = 0;
			this.btnClose.UseTransparentBackground = true;
			this.btnClose.Click += new System.EventHandler(this.guna2ImageButton1_Click);
			// 
			// txtBoxMK
			// 
			this.txtBoxMK.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtBoxMK.DefaultText = "";
			this.txtBoxMK.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtBoxMK.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtBoxMK.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtBoxMK.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtBoxMK.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtBoxMK.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.txtBoxMK.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtBoxMK.Location = new System.Drawing.Point(48, 46);
			this.txtBoxMK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtBoxMK.Name = "txtBoxMK";
			this.txtBoxMK.PasswordChar = '\0';
			this.txtBoxMK.PlaceholderText = "";
			this.txtBoxMK.SelectedText = "";
			this.txtBoxMK.Size = new System.Drawing.Size(247, 31);
			this.txtBoxMK.TabIndex = 3;
			// 
			// guna2ImageButton1
			// 
			this.guna2ImageButton1.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
			this.guna2ImageButton1.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
			this.guna2ImageButton1.HoverState.ImageSize = new System.Drawing.Size(32, 32);
			this.guna2ImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageButton1.Image")));
			this.guna2ImageButton1.ImageOffset = new System.Drawing.Point(0, 0);
			this.guna2ImageButton1.ImageRotate = 0F;
			this.guna2ImageButton1.ImageSize = new System.Drawing.Size(32, 32);
			this.guna2ImageButton1.IndicateFocus = true;
			this.guna2ImageButton1.Location = new System.Drawing.Point(3, 44);
			this.guna2ImageButton1.Name = "guna2ImageButton1";
			this.guna2ImageButton1.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
			this.guna2ImageButton1.PressedState.ImageSize = new System.Drawing.Size(32, 32);
			this.guna2ImageButton1.Size = new System.Drawing.Size(38, 41);
			this.guna2ImageButton1.TabIndex = 2;
			this.guna2ImageButton1.UseTransparentBackground = true;
			// 
			// btnDN
			// 
			this.btnDN.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnDN.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnDN.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnDN.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnDN.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnDN.FillColor2 = System.Drawing.Color.Blue;
			this.btnDN.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnDN.ForeColor = System.Drawing.Color.White;
			this.btnDN.Location = new System.Drawing.Point(77, 297);
			this.btnDN.Name = "btnDN";
			this.btnDN.Size = new System.Drawing.Size(180, 45);
			this.btnDN.TabIndex = 4;
			this.btnDN.Text = "Đăng Nhập";
			this.btnDN.Click += new System.EventHandler(this.btnDN_Click);
			// 
			// DANGNHAP
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(327, 359);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "DANGNHAP";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " Đăng Nhập";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private Guna.UI2.WinForms.Guna2ImageButton btnClose;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel4;
		private Guna.UI2.WinForms.Guna2HtmlLabel txtDNhap;
		private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton2;
		private Guna.UI2.WinForms.Guna2ImageButton imgAcc;
		private Guna.UI2.WinForms.Guna2TextBox txtBoxTK;
		private Guna.UI2.WinForms.Guna2GradientButton btnDN;
		private Guna.UI2.WinForms.Guna2TextBox txtBoxMK;
		private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
	}
}