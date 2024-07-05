using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmThongTinKH : Form
    {
        BLL_KhachHang kh = new BLL_KhachHang();

        public frmThongTinKH(KhachHang kh)
        {
            InitializeComponent();
            txtBoxMaKH.Text = Convert.ToString(kh.MaKH);
            txtBoxTenKH.Text = kh.TenKH;
            txtBoxGT.Text = kh.GioiTinh;
            txtBoxDiaChi.Text = kh.DiaChi;
            txtBoxSDT.Text = kh.SDT;
            txtBoxEmail.Text = kh.Email;
            txtBoxMK.Text = kh.MatKhau;
            txtDiemTL.Text = Convert.ToString(kh.DiemTichLuy);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                kh.Update_KH(int.Parse(txtBoxMaKH.Text), txtBoxTenKH.Text, txtBoxGT.Text, txtBoxDiaChi.Text, txtBoxSDT.Text, txtBoxEmail.Text, txtBoxMK.Text, int.Parse(txtDiemTL.Text));
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (kh.DeleteKH(int.Parse(txtBoxMaKH.Text)) == true)
                    {
                        MessageBox.Show("Xóa thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmKhachHang fr = new frmKhachHang();
                        fr.LoadDataGridView();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thông tin khách hàng không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa thông tin khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }
    }
}
