using BLL;
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
	public partial class DANGNHAP : Form
	{
		BLL_NhanVien nv = new BLL_NhanVien();
		public DANGNHAP()
		{
			InitializeComponent();
		}

		private void guna2ImageButton1_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				Application.Exit();
			}
		}

		private void btnDN_Click(object sender, EventArgs e)
		{
			if(txtBoxTK.Text == null || txtBoxMK.Text == null)
			{
				MessageBox.Show("Tài Khoản Hoặc Mật Khẩu Đang Để Trống!");
			}
			if (nv.CheckDangNhap(txtBoxTK.Text, txtBoxMK.Text))
			{
				MessageBox.Show("Đăng nhập thành công!");
			}
			else
			{
				MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!");
			}
		}
	}
}
