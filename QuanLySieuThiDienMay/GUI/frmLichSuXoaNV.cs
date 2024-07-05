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
    public partial class frmLichSuXoaNV : Form
    {
        BLL_NhanVien nv = new BLL_NhanVien();

        public int manv;
        public string tennv;

        public frmLichSuXoaNV()
        {
            InitializeComponent();
            Load += FrmLichSuXoaNV_Load;
        }

        private void FrmLichSuXoaNV_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();

                if (dgvListNV.Rows.Count == 0)
                {
                    MessageBox.Show("Không có nhân viên nào trong thùng rác !!! ", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        public void LoadData()
        {
            try
            {
                dgvListNV.DataSource = nv.GetListNV_IsDeleted();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void tsBtn_Khoiphuc_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListNV.SelectedRows.Count > 0)
                {
                    nv.RestoreNV(manv);
                    MessageBox.Show("Đã khôi phục nhân viên : " + tennv, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    txtTenNV.Text = "";
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 nhân viên để khôi phục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_Lammoi_Click(object sender, EventArgs e)
        {
            LoadData();
            txtTenNV.Text = "";
        }

        private void tsBtn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvListNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dgvListNV.Rows[e.RowIndex];
                    manv = Convert.ToInt32(selectedRow.Cells["MaNV"].Value);
                    tennv = selectedRow.Cells["TenNV"].Value.ToString();
                    txtTenNV.Text = tennv;
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
