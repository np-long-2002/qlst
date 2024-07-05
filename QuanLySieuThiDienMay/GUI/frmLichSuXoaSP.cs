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
    public partial class frmLichSuXoaSP : Form
    {
        BLL_SanPham sp = new BLL_SanPham();

        private int masp;
        private string tensp;

        public frmLichSuXoaSP()
        {
            InitializeComponent();
            Load += FrmLichSuXoaSP_Load;
        }

        private void FrmLichSuXoaSP_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();

                if (dgvSP_Xoa.Rows.Count == 0)
                {
                    MessageBox.Show("Không có sản phẩm nào trong thùng rác !!! ", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        public void LoadData()
        {
            try
            {
                dgvSP_Xoa.DataSource = sp.GetSP_IsDeleted();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void tsBtn_Khoiphuc_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSP_Xoa.SelectedRows.Count > 0)
                {

                    sp.RestoreProduct(masp);
                    MessageBox.Show("Đã khôi phục sản phẩm : " + tensp, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    txtTenSP.Text = "";
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 sản phẩm để khôi phục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_Thoat_Click(object sender, EventArgs e)
        {
            try
            {
                frmSanPham fsp = new frmSanPham();
                fsp.LoadSanPham();
                this.Close();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void tsBtn_Lammoi_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                txtTenSP.Text = "";
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void dgvSP_Xoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dgvSP_Xoa.Rows[e.RowIndex];
                    masp = Convert.ToInt32(selectedRow.Cells["Column1"].Value);
                    tensp = selectedRow.Cells["Column2"].Value.ToString();
                    txtTenSP.Text = tensp;
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }
    }
}
