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
    public partial class frmChiTietNhaCungCap : Form
    {
		BLL_NhaCungCap ncc = new BLL_NhaCungCap();
		private int maNCC;

		public frmChiTietNhaCungCap(int maNCC)
		{
			InitializeComponent();
			this.maNCC = maNCC;
			txtMaNCC.Enabled = false;
			LoadCTNCC(maNCC);
			btnThem.Enabled = false;
		}

		public frmChiTietNhaCungCap()
		{
			InitializeComponent();
			txtMaNCC.Enabled = false;
			txtMaNCC.Visible = false;
			lbNCC.Visible = false;
			btnCapNhat.Enabled = false;
            btnXoa.Enabled = false;

        }

		public void LoadCTNCC(int maNCC)
		{
			try
			{
				var tt = ncc.GetNhaCungCapByMaNCC(maNCC);
				if (tt != null)
				{
					txtTenNCC.Text = tt.TenNCC.ToString();
					txtDiaChi.Text = tt.DiaChi.ToString();
					txtSDT.Text = tt.SDT.ToString();
					txtEmail.Text = tt.Email.ToString();
					txtMaNCC.Text = tt.MaNCC.ToString();
				}
				else
				{
					MessageBox.Show("Lỗi!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnThem_Click(object sender, EventArgs e)
        {
			try
			{
				string tenNCC = txtTenNCC.Text;
				string diaChi = txtDiaChi.Text;
				string sdt = txtSDT.Text;
				string email = txtEmail.Text;
				ncc.ThemNhaCungCap(tenNCC, diaChi, sdt, email);
				MessageBox.Show("Thêm nhà cung cấp thành công!");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã có nhà cung cấp với số điện thoại hoặc email này rồi");
			}
		}

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
			try
			{
				string tenNCC = txtTenNCC.Text;
				string diaChi = txtDiaChi.Text;
				string sdt = txtSDT.Text;
				string email = txtEmail.Text;
				ncc.CapNhatNhaCungCap(maNCC, tenNCC, diaChi, sdt, email);
				MessageBox.Show("Cập nhật nhà cung cấp thành công!");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi cập nhật nhà cung cấp: " + ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void frmChiTietNhaCungCap_Load(object sender, EventArgs e)
        {
			try
			{
				if (maNCC != 0)
				{
					LoadCTNCC(maNCC);
				}
			}
			catch (Exception ex) 
			{ 
				MessageBox.Show($"Đã xảy ra lỗi khi lấy thông tin nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
		}

        private void tsBtn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin nhà cung cấp này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (ncc.DeleteNCC(int.Parse(txtMaNCC.Text)) == true)
                    {
                        MessageBox.Show("Xóa thông tin nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmNhaCungCap fr = new frmNhaCungCap();
                        fr.LoadData();
						fr.LoadCbo_TimKiem();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thông tin nhà cung cấp không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa thông tin nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_Thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin nhà cung cấp này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (ncc.DeleteNCC(int.Parse(txtMaNCC.Text)) == true)
                    {
                        MessageBox.Show("Xóa thông tin nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmNhaCungCap fr = new frmNhaCungCap();
                        fr.LoadData();
                        fr.LoadCbo_TimKiem();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thông tin nhà cung cấp không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa thông tin nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
