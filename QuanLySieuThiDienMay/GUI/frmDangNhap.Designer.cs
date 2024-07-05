namespace GUI
{
	partial class frmDangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnQMK = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnDN = new Guna.UI2.WinForms.Guna2GradientButton();
            this.txtDNhap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.guna2ImageButton3 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.cbxKho = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnXoaTK = new Guna.UI2.WinForms.Guna2ImageButton();
            this.txtBoxMK = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.txtBoxTK = new Guna.UI2.WinForms.Guna2TextBox();
            this.imgAcc = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnHienMK = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnAnMK = new Guna.UI2.WinForms.Guna2ImageButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.guna2ImageButton2 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2ImageButton();
            this.tbHienAn = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.tbXoaTK = new Guna.UI2.WinForms.Guna2HtmlToolTip();
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
            this.panel1.Controls.Add(this.btnQMK);
            this.panel1.Controls.Add(this.btnDN);
            this.panel1.Controls.Add(this.txtDNhap);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 400);
            this.panel1.TabIndex = 0;
            // 
            // btnQMK
            // 
            this.btnQMK.BackColor = System.Drawing.Color.Transparent;
            this.btnQMK.BorderRadius = 10;
            this.btnQMK.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQMK.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQMK.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQMK.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQMK.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQMK.FillColor2 = System.Drawing.Color.Blue;
            this.btnQMK.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnQMK.ForeColor = System.Drawing.Color.White;
            this.btnQMK.Image = ((System.Drawing.Image)(resources.GetObject("btnQMK.Image")));
            this.btnQMK.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnQMK.ImageSize = new System.Drawing.Size(60, 60);
            this.btnQMK.IndicateFocus = true;
            this.btnQMK.Location = new System.Drawing.Point(245, 326);
            this.btnQMK.Name = "btnQMK";
            this.btnQMK.Size = new System.Drawing.Size(210, 45);
            this.btnQMK.TabIndex = 5;
            this.btnQMK.Text = "Quên mật khẩu";
            this.btnQMK.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnQMK.UseTransparentBackground = true;
            this.btnQMK.Click += new System.EventHandler(this.btnQMK_Click);
            // 
            // btnDN
            // 
            this.btnDN.BackColor = System.Drawing.Color.Transparent;
            this.btnDN.BorderRadius = 10;
            this.btnDN.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDN.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDN.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDN.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDN.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDN.FillColor2 = System.Drawing.Color.Blue;
            this.btnDN.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnDN.ForeColor = System.Drawing.Color.White;
            this.btnDN.Image = ((System.Drawing.Image)(resources.GetObject("btnDN.Image")));
            this.btnDN.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDN.ImageSize = new System.Drawing.Size(60, 60);
            this.btnDN.IndicateFocus = true;
            this.btnDN.Location = new System.Drawing.Point(19, 326);
            this.btnDN.Name = "btnDN";
            this.btnDN.Size = new System.Drawing.Size(209, 45);
            this.btnDN.TabIndex = 4;
            this.btnDN.Text = "Đăng nhập vào";
            this.btnDN.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDN.UseTransparentBackground = true;
            this.btnDN.Click += new System.EventHandler(this.btnDN_Click);
            // 
            // txtDNhap
            // 
            this.txtDNhap.BackColor = System.Drawing.Color.Transparent;
            this.txtDNhap.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDNhap.ForeColor = System.Drawing.Color.Yellow;
            this.txtDNhap.Location = new System.Drawing.Point(181, 43);
            this.txtDNhap.Name = "txtDNhap";
            this.txtDNhap.Size = new System.Drawing.Size(143, 27);
            this.txtDNhap.TabIndex = 3;
            this.txtDNhap.Text = "ĐĂNG NHẬP";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.guna2ImageButton3);
            this.panel5.Controls.Add(this.cbxKho);
            this.panel5.Controls.Add(this.btnXoaTK);
            this.panel5.Controls.Add(this.txtBoxMK);
            this.panel5.Controls.Add(this.guna2ImageButton1);
            this.panel5.Controls.Add(this.txtBoxTK);
            this.panel5.Controls.Add(this.imgAcc);
            this.panel5.Controls.Add(this.btnHienMK);
            this.panel5.Controls.Add(this.btnAnMK);
            this.panel5.Location = new System.Drawing.Point(0, 152);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(472, 146);
            this.panel5.TabIndex = 2;
            // 
            // guna2ImageButton3
            // 
            this.guna2ImageButton3.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton3.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.guna2ImageButton3.HoverState.ImageSize = new System.Drawing.Size(40, 40);
            this.guna2ImageButton3.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageButton3.Image")));
            this.guna2ImageButton3.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton3.ImageRotate = 0F;
            this.guna2ImageButton3.ImageSize = new System.Drawing.Size(40, 40);
            this.guna2ImageButton3.IndicateFocus = true;
            this.guna2ImageButton3.Location = new System.Drawing.Point(24, 102);
            this.guna2ImageButton3.Name = "guna2ImageButton3";
            this.guna2ImageButton3.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.guna2ImageButton3.PressedState.ImageSize = new System.Drawing.Size(40, 40);
            this.guna2ImageButton3.Size = new System.Drawing.Size(38, 41);
            this.guna2ImageButton3.TabIndex = 8;
            this.guna2ImageButton3.UseTransparentBackground = true;
            // 
            // cbxKho
            // 
            this.cbxKho.AutoRoundedCorners = true;
            this.cbxKho.BackColor = System.Drawing.Color.Transparent;
            this.cbxKho.BorderRadius = 17;
            this.cbxKho.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxKho.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbxKho.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbxKho.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.cbxKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbxKho.HoverState.BorderColor = System.Drawing.Color.Lime;
            this.cbxKho.ItemHeight = 30;
            this.cbxKho.Location = new System.Drawing.Point(69, 102);
            this.cbxKho.Name = "cbxKho";
            this.cbxKho.Size = new System.Drawing.Size(342, 36);
            this.cbxKho.TabIndex = 3;
            // 
            // btnXoaTK
            // 
            this.btnXoaTK.BackColor = System.Drawing.Color.Transparent;
            this.btnXoaTK.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnXoaTK.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            this.btnXoaTK.HoverState.ImageSize = new System.Drawing.Size(16, 16);
            this.btnXoaTK.Image = ((System.Drawing.Image)(resources.GetObject("btnXoaTK.Image")));
            this.btnXoaTK.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnXoaTK.ImageRotate = 0F;
            this.btnXoaTK.ImageSize = new System.Drawing.Size(16, 16);
            this.btnXoaTK.Location = new System.Drawing.Point(418, 8);
            this.btnXoaTK.Name = "btnXoaTK";
            this.btnXoaTK.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image3")));
            this.btnXoaTK.PressedState.ImageSize = new System.Drawing.Size(16, 16);
            this.btnXoaTK.Size = new System.Drawing.Size(28, 31);
            this.btnXoaTK.TabIndex = 7;
            this.btnXoaTK.UseTransparentBackground = true;
            this.btnXoaTK.Click += new System.EventHandler(this.btnXoaTK_Click);
            // 
            // txtBoxMK
            // 
            this.txtBoxMK.BorderRadius = 15;
            this.txtBoxMK.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBoxMK.DefaultText = "";
            this.txtBoxMK.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBoxMK.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBoxMK.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBoxMK.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBoxMK.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBoxMK.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtBoxMK.HoverState.BorderColor = System.Drawing.Color.Lime;
            this.txtBoxMK.Location = new System.Drawing.Point(69, 54);
            this.txtBoxMK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBoxMK.Name = "txtBoxMK";
            this.txtBoxMK.PasswordChar = '*';
            this.txtBoxMK.PlaceholderText = "";
            this.txtBoxMK.SelectedText = "";
            this.txtBoxMK.Size = new System.Drawing.Size(342, 31);
            this.txtBoxMK.TabIndex = 2;
            // 
            // guna2ImageButton1
            // 
            this.guna2ImageButton1.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image4")));
            this.guna2ImageButton1.HoverState.ImageSize = new System.Drawing.Size(32, 32);
            this.guna2ImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageButton1.Image")));
            this.guna2ImageButton1.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton1.ImageRotate = 0F;
            this.guna2ImageButton1.ImageSize = new System.Drawing.Size(32, 32);
            this.guna2ImageButton1.IndicateFocus = true;
            this.guna2ImageButton1.Location = new System.Drawing.Point(24, 50);
            this.guna2ImageButton1.Name = "guna2ImageButton1";
            this.guna2ImageButton1.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image5")));
            this.guna2ImageButton1.PressedState.ImageSize = new System.Drawing.Size(32, 32);
            this.guna2ImageButton1.Size = new System.Drawing.Size(38, 41);
            this.guna2ImageButton1.TabIndex = 2;
            this.guna2ImageButton1.UseTransparentBackground = true;
            // 
            // txtBoxTK
            // 
            this.txtBoxTK.BorderRadius = 15;
            this.txtBoxTK.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBoxTK.DefaultText = "";
            this.txtBoxTK.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBoxTK.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBoxTK.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBoxTK.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBoxTK.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBoxTK.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtBoxTK.HoverState.BorderColor = System.Drawing.Color.Lime;
            this.txtBoxTK.Location = new System.Drawing.Point(69, 8);
            this.txtBoxTK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBoxTK.Name = "txtBoxTK";
            this.txtBoxTK.PasswordChar = '\0';
            this.txtBoxTK.PlaceholderText = "";
            this.txtBoxTK.SelectedText = "";
            this.txtBoxTK.Size = new System.Drawing.Size(342, 31);
            this.txtBoxTK.TabIndex = 1;
            this.txtBoxTK.TextChanged += new System.EventHandler(this.txtBoxTK_TextChanged);
            this.txtBoxTK.Enter += new System.EventHandler(this.txtBoxTK_Enter);
            // 
            // imgAcc
            // 
            this.imgAcc.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.imgAcc.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image6")));
            this.imgAcc.HoverState.ImageSize = new System.Drawing.Size(32, 32);
            this.imgAcc.Image = ((System.Drawing.Image)(resources.GetObject("imgAcc.Image")));
            this.imgAcc.ImageOffset = new System.Drawing.Point(0, 0);
            this.imgAcc.ImageRotate = 0F;
            this.imgAcc.ImageSize = new System.Drawing.Size(32, 32);
            this.imgAcc.IndicateFocus = true;
            this.imgAcc.Location = new System.Drawing.Point(24, 3);
            this.imgAcc.Name = "imgAcc";
            this.imgAcc.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image7")));
            this.imgAcc.PressedState.ImageSize = new System.Drawing.Size(32, 32);
            this.imgAcc.Size = new System.Drawing.Size(38, 41);
            this.imgAcc.TabIndex = 0;
            this.imgAcc.UseTransparentBackground = true;
            // 
            // btnHienMK
            // 
            this.btnHienMK.BackColor = System.Drawing.Color.Transparent;
            this.btnHienMK.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnHienMK.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image8")));
            this.btnHienMK.HoverState.ImageSize = new System.Drawing.Size(16, 16);
            this.btnHienMK.Image = ((System.Drawing.Image)(resources.GetObject("btnHienMK.Image")));
            this.btnHienMK.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnHienMK.ImageRotate = 0F;
            this.btnHienMK.ImageSize = new System.Drawing.Size(16, 16);
            this.btnHienMK.Location = new System.Drawing.Point(418, 54);
            this.btnHienMK.Name = "btnHienMK";
            this.btnHienMK.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image9")));
            this.btnHienMK.PressedState.ImageSize = new System.Drawing.Size(16, 16);
            this.btnHienMK.Size = new System.Drawing.Size(28, 31);
            this.btnHienMK.TabIndex = 6;
            this.btnHienMK.UseTransparentBackground = true;
            this.btnHienMK.Click += new System.EventHandler(this.btnHienMK_Click);
            // 
            // btnAnMK
            // 
            this.btnAnMK.BackColor = System.Drawing.Color.Transparent;
            this.btnAnMK.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnAnMK.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image10")));
            this.btnAnMK.HoverState.ImageSize = new System.Drawing.Size(16, 16);
            this.btnAnMK.Image = ((System.Drawing.Image)(resources.GetObject("btnAnMK.Image")));
            this.btnAnMK.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnAnMK.ImageRotate = 0F;
            this.btnAnMK.ImageSize = new System.Drawing.Size(16, 16);
            this.btnAnMK.Location = new System.Drawing.Point(418, 54);
            this.btnAnMK.Name = "btnAnMK";
            this.btnAnMK.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image11")));
            this.btnAnMK.PressedState.ImageSize = new System.Drawing.Size(16, 16);
            this.btnAnMK.Size = new System.Drawing.Size(28, 31);
            this.btnAnMK.TabIndex = 5;
            this.btnAnMK.UseTransparentBackground = true;
            this.btnAnMK.Click += new System.EventHandler(this.btnAnMK_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.guna2ImageButton2);
            this.panel4.Location = new System.Drawing.Point(3, 70);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(469, 79);
            this.panel4.TabIndex = 1;
            // 
            // guna2ImageButton2
            // 
            this.guna2ImageButton2.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton2.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image12")));
            this.guna2ImageButton2.HoverState.ImageSize = new System.Drawing.Size(120, 100);
            this.guna2ImageButton2.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageButton2.Image")));
            this.guna2ImageButton2.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton2.ImageRotate = 0F;
            this.guna2ImageButton2.ImageSize = new System.Drawing.Size(120, 100);
            this.guna2ImageButton2.IndicateFocus = true;
            this.guna2ImageButton2.Location = new System.Drawing.Point(-3, 0);
            this.guna2ImageButton2.Name = "guna2ImageButton2";
            this.guna2ImageButton2.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image13")));
            this.guna2ImageButton2.PressedState.ImageSize = new System.Drawing.Size(120, 100);
            this.guna2ImageButton2.Size = new System.Drawing.Size(472, 79);
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
            this.panel2.Size = new System.Drawing.Size(472, 40);
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
            this.btnClose.Location = new System.Drawing.Point(410, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.PressedState.ImageSize = new System.Drawing.Size(30, 30);
            this.btnClose.Size = new System.Drawing.Size(33, 32);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.guna2ImageButton1_Click);
            // 
            // tbHienAn
            // 
            this.tbHienAn.AllowLinksHandling = true;
            this.tbHienAn.MaximumSize = new System.Drawing.Size(0, 0);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 50;
            this.guna2Elipse1.TargetControl = this;
            // 
            // tbXoaTK
            // 
            this.tbXoaTK.AllowLinksHandling = true;
            this.tbXoaTK.MaximumSize = new System.Drawing.Size(0, 0);
            // 
            // frmDangNhap
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(472, 400);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Đăng Nhập";
            this.Load += new System.EventHandler(this.frmDangNhap_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmDangNhap_KeyPress);
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
        private Guna.UI2.WinForms.Guna2ImageButton btnHienMK;
        private Guna.UI2.WinForms.Guna2ImageButton btnAnMK;
        private Guna.UI2.WinForms.Guna2HtmlToolTip tbHienAn;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2ImageButton btnXoaTK;
        private Guna.UI2.WinForms.Guna2HtmlToolTip tbXoaTK;
        private Guna.UI2.WinForms.Guna2ComboBox cbxKho;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton3;
        private Guna.UI2.WinForms.Guna2GradientButton btnQMK;
    }
}