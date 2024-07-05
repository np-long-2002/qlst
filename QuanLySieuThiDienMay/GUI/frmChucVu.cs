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
using TheArtOfDevHtmlRenderer.Adapters;

namespace GUI
{
    public partial class frmChucVu : Form
    {
        BLL_ChucVu cv = new BLL_ChucVu();
        Basis ba = new Basis();

        public frmChucVu()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
			try
			{
				cv.getCVs(dgv_ChucVu);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu chức vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_Thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void frmChucVu_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                txtMaCV.Enabled = false;
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu chức vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void dgv_ChucVu_Click(object sender, EventArgs e)
        {
			try
			{
				if (dgv_ChucVu.CurrentRow != null)
				{
					int curow = dgv_ChucVu.CurrentRow.Index;

					if (dgv_ChucVu.Rows[curow].Cells[0].Value != null)
						txtMaCV.Text = dgv_ChucVu.Rows[curow].Cells[0].Value.ToString();
					else
						txtMaCV.Text = "";

					if (dgv_ChucVu.Rows[curow].Cells[1].Value != null)
						txtTenCV.Text = dgv_ChucVu.Rows[curow].Cells[1].Value.ToString();
					else
						txtTenCV.Text = "";
				}
				else
				{
					txtMaCV.Text = "";
					txtTenCV.Text = "";
					MessageBox.Show("Vui lòng chọn một hàng để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi hiển thị chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
