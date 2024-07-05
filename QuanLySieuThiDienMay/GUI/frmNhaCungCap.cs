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
    public partial class frmNhaCungCap : Form
    {
        BLL_NhaCungCap ncc = new BLL_NhaCungCap();
        Basis ba = new Basis();

        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        public void LoadCbo_TimKiem()
        {
            try
            {
                string[] s = { "Tên nhà cung cấp", "Địa chỉ" };
                foreach (string i in s)
                {
                    cboLoai.Items.Add(i);

                }
                cboLoai.SelectedIndex = 0;
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
                ncc.getNCCs(dgv_NCC);
                tssLbl_TongCong.Text = dgv_NCC.RowCount.ToString();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void tsBtn_SapXep_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_NCC.RowCount < 1)
                {
                    MessageBox.Show("Chưa có dữ liệu !");
                }
                else
                {
                    ncc.sort(dgv_NCC);
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void tsBtn_XuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ba.XuatExcel(dgv_NCC);
                MessageBox.Show("Xuất file excel thành công !");
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void tsBtn_Reset_Click(object sender, EventArgs e)
        {
            try
            {
                cboLoai.SelectedIndex = 0;
                ba.clearTextBoxs(this.Controls);
                LoadData();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCbo_TimKiem();
        }

        private void dgv_NCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgv_NCC.Rows[e.RowIndex];
                    int maNCC = Convert.ToInt32(row.Cells["MaNCC"].Value);
                    ShowChiTietNhaCungCap(maNCC);
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void ShowChiTietNhaCungCap(int maNCC)
        {
			try
			{
				frmChiTietNhaCungCap frm = new frmChiTietNhaCungCap(maNCC);
				frm.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi hiển thị chi tiết nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_Them_Click(object sender, EventArgs e)
        {
			try
			{
				frmChiTietNhaCungCap frm = new frmChiTietNhaCungCap();
				frm.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi thêm nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_NhaCungCapCu_Click(object sender, EventArgs e)
        {
			try
			{
				frmLichSuXoaNCC frm = new frmLichSuXoaNCC();
				frm.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi hiển thị lịch sử xóa nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnTk_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_NCC.RowCount < 1)
                {
                    MessageBox.Show("Chưa có dữ liệu !");
                }
                else
                {
                    string check = cboLoai.SelectedItem.ToString();
                    if (check == "Tên nhà cung cấp")
                    {
                        List<ListFrmNCC> searchList = ncc.Search_ListNCC_TenNCC(txtBoxSearch.Text);
                        dgv_NCC.DataSource = searchList;
                        MessageBox.Show("Số lượng nhà cung cấp tìm thấy: " + searchList.Count);
                    }
                    else
                    {
                        if (check == "Địa chỉ")
                        {
                            List<ListFrmNCC> searchList = ncc.Search_ListNCC_DC(txtBoxSearch.Text);
                            dgv_NCC.DataSource = searchList;
                            MessageBox.Show("Số lượng nhà cung cấp tìm thấy: " + searchList.Count);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
