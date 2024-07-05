namespace GUI
{
    partial class frmChucVu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChucVu));
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtTenCV = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbten = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbma = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtMaCV = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgv_ChucVu = new System.Windows.Forms.DataGridView();
            this.MaCV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ChucVu)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 35;
            this.guna2Elipse1.TargetControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(139, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 29);
            this.label1.TabIndex = 75;
            this.label1.Text = "Thông tin chức vụ";
            // 
            // txtTenCV
            // 
            this.txtTenCV.BorderRadius = 15;
            this.txtTenCV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenCV.DefaultText = "";
            this.txtTenCV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenCV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenCV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenCV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenCV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenCV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenCV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenCV.Location = new System.Drawing.Point(217, 141);
            this.txtTenCV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenCV.Name = "txtTenCV";
            this.txtTenCV.PasswordChar = '\0';
            this.txtTenCV.PlaceholderText = "";
            this.txtTenCV.SelectedText = "";
            this.txtTenCV.Size = new System.Drawing.Size(229, 31);
            this.txtTenCV.TabIndex = 78;
            // 
            // lbten
            // 
            this.lbten.AutoSize = false;
            this.lbten.BackColor = System.Drawing.Color.Transparent;
            this.lbten.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbten.ForeColor = System.Drawing.Color.White;
            this.lbten.Location = new System.Drawing.Point(71, 142);
            this.lbten.Name = "lbten";
            this.lbten.Size = new System.Drawing.Size(140, 30);
            this.lbten.TabIndex = 77;
            this.lbten.Text = "Mô tả chức vụ";
            // 
            // lbma
            // 
            this.lbma.AutoSize = false;
            this.lbma.BackColor = System.Drawing.Color.Transparent;
            this.lbma.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbma.ForeColor = System.Drawing.Color.White;
            this.lbma.Location = new System.Drawing.Point(71, 94);
            this.lbma.Name = "lbma";
            this.lbma.Size = new System.Drawing.Size(140, 30);
            this.lbma.TabIndex = 76;
            this.lbma.Text = "Mã chức vụ";
            // 
            // txtMaCV
            // 
            this.txtMaCV.BorderRadius = 15;
            this.txtMaCV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaCV.DefaultText = "";
            this.txtMaCV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMaCV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMaCV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaCV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaCV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaCV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaCV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaCV.Location = new System.Drawing.Point(217, 93);
            this.txtMaCV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaCV.Name = "txtMaCV";
            this.txtMaCV.PasswordChar = '\0';
            this.txtMaCV.PlaceholderText = "";
            this.txtMaCV.SelectedText = "";
            this.txtMaCV.Size = new System.Drawing.Size(229, 31);
            this.txtMaCV.TabIndex = 79;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.guna2Panel1.Location = new System.Drawing.Point(0, 35);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(517, 183);
            this.guna2Panel1.TabIndex = 81;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.dgv_ChucVu);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 224);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(517, 287);
            this.guna2Panel2.TabIndex = 82;
            // 
            // dgv_ChucVu
            // 
            this.dgv_ChucVu.AllowUserToAddRows = false;
            this.dgv_ChucVu.AllowUserToDeleteRows = false;
            this.dgv_ChucVu.AllowUserToResizeColumns = false;
            this.dgv_ChucVu.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.dgv_ChucVu.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ChucVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ChucVu.BackgroundColor = System.Drawing.Color.White;
            this.dgv_ChucVu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_ChucVu.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_ChucVu.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ChucVu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_ChucVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ChucVu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaCV,
            this.MoTa});
            this.dgv_ChucVu.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ChucVu.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_ChucVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ChucVu.EnableHeadersVisualStyles = false;
            this.dgv_ChucVu.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgv_ChucVu.Location = new System.Drawing.Point(0, 0);
            this.dgv_ChucVu.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_ChucVu.Name = "dgv_ChucVu";
            this.dgv_ChucVu.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ChucVu.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_ChucVu.RowHeadersVisible = false;
            this.dgv_ChucVu.RowHeadersWidth = 62;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            this.dgv_ChucVu.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_ChucVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ChucVu.Size = new System.Drawing.Size(517, 287);
            this.dgv_ChucVu.TabIndex = 23;
            this.dgv_ChucVu.Click += new System.EventHandler(this.dgv_ChucVu_Click);
            // 
            // MaCV
            // 
            this.MaCV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MaCV.DataPropertyName = "MaCV";
            this.MaCV.HeaderText = "Mã chức vụ";
            this.MaCV.MinimumWidth = 8;
            this.MaCV.Name = "MaCV";
            this.MaCV.ReadOnly = true;
            this.MaCV.Width = 88;
            // 
            // MoTa
            // 
            this.MoTa.DataPropertyName = "MoTa";
            this.MoTa.HeaderText = "Mô tả chức vụ";
            this.MoTa.MinimumWidth = 8;
            this.MoTa.Name = "MoTa";
            this.MoTa.ReadOnly = true;
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
            this.btnClose.Location = new System.Drawing.Point(472, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.PressedState.ImageSize = new System.Drawing.Size(30, 30);
            this.btnClose.Size = new System.Drawing.Size(33, 32);
            this.btnClose.TabIndex = 150;
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmChucVu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(517, 511);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.txtMaCV);
            this.Controls.Add(this.txtTenCV);
            this.Controls.Add(this.lbten);
            this.Controls.Add(this.lbma);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChucVu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmChucVu";
            this.Load += new System.EventHandler(this.frmChucVu_Load);
            this.guna2Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ChucVu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtTenCV;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbten;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbma;
        private Guna.UI2.WinForms.Guna2TextBox txtMaCV;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.DataGridView dgv_ChucVu;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaCV;
        private System.Windows.Forms.DataGridViewTextBoxColumn MoTa;
        private Guna.UI2.WinForms.Guna2ImageButton btnClose;
    }
}