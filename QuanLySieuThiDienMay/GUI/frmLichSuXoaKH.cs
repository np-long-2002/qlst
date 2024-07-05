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
    public partial class frmLichSuXoaKH : Form
    {
        BLL_KhachHang kh = new BLL_KhachHang();

        public int makh;
        public string tenkh;

        public frmLichSuXoaKH()
        {
            InitializeComponent();
            Load += FrmLichSuXoaKH_Load;
        }

        private void FrmLichSuXoaKH_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();

                if (dgvListKH.Rows.Count == 0)
                {
                    MessageBox.Show("Không có khách hàng nào trong thùng rác !!! ", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadData()
        {
            try
            {
                dgvListKH.DataSource = kh.GetListKH_IsDeleted();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void tsBtn_Khoiphuc_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListKH.SelectedRows.Count > 0)
                {
                    kh.RestoreKH(makh);
                    MessageBox.Show("Đã khôi phục khách hàng : " + tenkh, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    txtTenKH.Text = "";
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 khách hàng để khôi phục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtn_Lammoi_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                txtTenKH.Text = "";
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void dgvListKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			try
			{
				if (e.RowIndex >= 0)
				{
					DataGridViewRow selectedRow = dgvListKH.Rows[e.RowIndex];
					makh = Convert.ToInt32(selectedRow.Cells["Column1"].Value);
					tenkh = selectedRow.Cells["Column2"].Value.ToString();
					txtTenKH.Text = tenkh;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi chọn khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnTk_Click(object sender, EventArgs e)
        {

        }
    }
}
