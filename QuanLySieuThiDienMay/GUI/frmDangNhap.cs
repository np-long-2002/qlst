using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
	public partial class frmDangNhap : Form
	{
		BLL_NhanVien nv = new BLL_NhanVien();
        BLL_Kho kho = new BLL_Kho();
        List<string> danhSachTaiKhoan = new List<string>();

        public string TaiKhoan { get; private set; }
        public string MatKhau { get; private set; }

        public frmDangNhap()
		{
			InitializeComponent();
            btnAnMK.Visible = false;
            tbHienAn = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            tbHienAn.SetToolTip(btnHienMK, "Ẩn mật khẩu");
            tbHienAn.SetToolTip(btnAnMK, "Hiển thị mật khẩu");
            tbXoaTK.SetToolTip(btnXoaTK, "Xoá tài khoản");
            txtBoxTK.Enter += txtBoxTK_Enter;
        }

		private void guna2ImageButton1_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				Application.Exit();
			}
		}

        private void btnAnMK_Click(object sender, EventArgs e)
        {
            txtBoxMK.PasswordChar = '\0';
            btnHienMK.Visible = true;
            btnAnMK.Visible = false;
        }

        private void btnHienMK_Click(object sender, EventArgs e)
        {
            txtBoxMK.PasswordChar = '*';
            btnAnMK.Visible = true;
            btnHienMK.Visible = false;
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxTK.Text) || string.IsNullOrEmpty(txtBoxMK.Text))
            {
                MessageBox.Show("Tài khoản hoặc Mật khẩu đang để trống!");
                return;
            }

            try
            {
                if (nv.CheckDangNhap(txtBoxTK.Text, txtBoxMK.Text))
                {
                    if (nv.CheckCV(txtBoxTK.Text, txtBoxMK.Text) != 1 && nv.CheckCV(txtBoxTK.Text, txtBoxMK.Text) != 2 && nv.CheckCV(txtBoxTK.Text, txtBoxMK.Text) != 3)
                    {
                        MessageBox.Show("Bạn không phải Là nhân viên trực quầy!");
                    }
                    else
                    {
                        TaiKhoan = txtBoxTK.Text;
                        MatKhau = txtBoxMK.Text;

                        MessageBox.Show("Đăng nhập thành công!");
                        LuuTaiKhoanGhiNho(TaiKhoan);

                        frmMain main = new frmMain();
                        main.TaiKhoan = TaiKhoan;
                        main.MatKhau = MatKhau;
                        main.MaKho = int.Parse(cbxKho.SelectedValue.ToString());
                        main.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDN.PerformClick();
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
			try
			{
				this.ActiveControl = txtBoxTK;
				txtBoxTK.Focus();
				LoadKho();
				LoadTaiKhoanGhiNho();
				UpdateUI();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi khởi động: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            XoaTaiKhoanGhiNho();
            txtBoxTK.Text = "";
            UpdateUI();
        }

        private void LuuTaiKhoanGhiNho(string taiKhoan)
        {
            if (!danhSachTaiKhoan.Contains(taiKhoan))
            {
                danhSachTaiKhoan.Add(taiKhoan);
                UpdateTextBox();
            }

            Properties.Settings.Default.TaiKhoanGhiNho = string.Join(",", danhSachTaiKhoan);
            Properties.Settings.Default.Save();
        }

        private void XoaTaiKhoanGhiNho()
        {
            if (!string.IsNullOrEmpty(txtBoxTK.Text))
            {
                danhSachTaiKhoan.Remove(txtBoxTK.Text);
                UpdateTextBox();
                Properties.Settings.Default.TaiKhoanGhiNho = string.Join(",", danhSachTaiKhoan);
                Properties.Settings.Default.Save();
            }
        }

        private void LoadTaiKhoanGhiNho()
        {
            string danhSach = Properties.Settings.Default.TaiKhoanGhiNho;
            if (!string.IsNullOrEmpty(danhSach))
            {
                danhSachTaiKhoan = danhSach.Split(',').ToList();
                UpdateTextBox();
            }
        }

        private void UpdateTextBox()
        {
            txtBoxTK.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBoxTK.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            collection.AddRange(danhSachTaiKhoan.ToArray());
            txtBoxTK.AutoCompleteCustomSource = collection;
        }

        private void UpdateUI()
        {
            if (!string.IsNullOrEmpty(txtBoxTK.Text))
            {
                btnXoaTK.Visible = true;
            }
            else
            {
                btnXoaTK.Visible = false;
            }
        }

        private void txtBoxTK_TextChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void txtBoxTK_Enter(object sender, EventArgs e)
        {
            UpdateTextBox();
        }

		public void LoadKho()
		{
			try
			{
				cbxKho.Enabled = false;
				var danhSachKho = kho.getKho();

				if (danhSachKho != null && danhSachKho.Count > 0)
				{
					cbxKho.DisplayMember = "TenKho";
					cbxKho.ValueMember = "MaKho";
					cbxKho.DataSource = danhSachKho;
				}
				else
				{
					MessageBox.Show("Không có kho nào để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
					cbxKho.DataSource = null;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				cbxKho.Enabled = true;
			}
		}
		private void btnQMK_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau frm = new frmDoiMatKhau();
            frm.ShowDialog();
        }
    }
}
