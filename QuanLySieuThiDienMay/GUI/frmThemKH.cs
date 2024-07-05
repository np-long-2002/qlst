using BLL;
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
    public partial class frmThemKH : Form
    {
        Basis ba = new Basis();
        BLL_KhachHang kh = new BLL_KhachHang();

        public frmThemKH()
        {
            InitializeComponent();
            txtMaKH.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string gtinh = "";
                if (rdoNam.Checked)
                {
                    gtinh = "Nam";
                }
                if (rdoNu.Checked)
                {
                    gtinh = "Nữ";
                }
                kh.Add_KH(txtTenNV.Text, gtinh, txtDiaChi.Text, txtSDT.Text, txtEmail.Text, txtMK.Text, int.Parse(txtDiemTL.Text));
                MessageBox.Show("Đã thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                ba.clearTextBoxs(this.Controls);
                rdoNam.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
