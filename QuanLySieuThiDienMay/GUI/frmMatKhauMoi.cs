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
    public partial class frmMatKhauMoi : Form
    {
        BLL_NhanVien nv = new BLL_NhanVien();

        private int maNV;
        private string tenNV;

        public frmMatKhauMoi(int maNV, string tenNV)
        {
            InitializeComponent();
            this.maNV = maNV;
            this.tenNV = tenNV;
        }

        private void frmMatKhauMoi_Load(object sender, EventArgs e)
        {
            txtMaNV.Text = maNV.ToString();
            txtTenNV.Text = tenNV;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                string emailOrPhone = txtEmail.Text;
                string oldPassword = txtMKCu.Text;
                string newPassword = txtMKMoi.Text;

                if (nv.KiemTraThongTinDangNhap(emailOrPhone, oldPassword))
                {
                    if (nv != null)
                    {
                        nv.DoiMatKhau(maNV, newPassword);
                        MessageBox.Show("Đổi mật khẩu thành công");
                    }
                }
                else
                {
                    MessageBox.Show("Email hoặc mật khẩu cũ không đúng");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            txtMKCu.Text = "";
            txtMKMoi.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không ?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Hide();
            }
        }
    }
}
