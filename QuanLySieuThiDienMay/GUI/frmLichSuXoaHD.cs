using BLL;
using Emgu.CV;
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
    public partial class frmLichSuXoaHD : Form
    {
        BLL_HoaDon hd = new BLL_HoaDon();

        public int mahd;

        public frmLichSuXoaHD()
        {
            InitializeComponent();
            Load += FrmLichSuXoaHD_Load;
        }

        private void FrmLichSuXoaHD_Load(object sender, EventArgs e)
        {
			try
			{
				LoadData();

				if (dgvListHDIsDeleted.Rows.Count == 0)
				{
					MessageBox.Show("Không có hóa đơn nào trong thùng rác !!!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
				dgvListHDIsDeleted.DataSource = hd.getHDlist();
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
                if (dgvListHDIsDeleted.SelectedRows.Count > 0)
                {
                    hd.RestoreHD(mahd);
                    MessageBox.Show("Đã khôi phục hoá đơn mã : " + mahd, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 hoá đơn để khôi phục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            LoadData();
        }

        private void dgvListHDIsDeleted_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dgvListHDIsDeleted.Rows[e.RowIndex];
                    mahd = Convert.ToInt32(selectedRow.Cells["MaHD"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTk_Click(object sender, EventArgs e)
        {

        }
    }
}
