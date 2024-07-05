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
    public partial class frmLichSuXoaNCC : Form
    {
        BLL_NhaCungCap ncc = new BLL_NhaCungCap();

        public int mancc;
        public string tenncc;

        public frmLichSuXoaNCC()
        {
            InitializeComponent();
            Load += FrmLichSuXoaNCC_Load;
        }

        private void FrmLichSuXoaNCC_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();

                if (dgvListNCC.Rows.Count == 0)
                {
                    MessageBox.Show("Không có nhà cung cấp nào trong thùng rác !!! ", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        public void LoadData()
        {
			try
			{
				dgvListNCC.DataSource = ncc.GetListNCC_IsDeleted();
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
                if (dgvListNCC.SelectedRows.Count > 0)
                {
                    ncc.RestoreNCC(mancc);
                    MessageBox.Show("Đã khôi phục nhà cung cấp : " + tenncc, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    txtTenNCC.Text = "";
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 nhà cung cấp để khôi phục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                txtTenNCC.Text = "";
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void dgvListNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			try
			{
				if (e.RowIndex >= 0)
				{
					DataGridViewRow selectedRow = dgvListNCC.Rows[e.RowIndex];
					mancc = Convert.ToInt32(selectedRow.Cells["Column1"].Value);
					tenncc = selectedRow.Cells["Column2"].Value.ToString();
					txtTenNCC.Text = tenncc;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnTk_Click(object sender, EventArgs e)
        {

        }
    }
}
