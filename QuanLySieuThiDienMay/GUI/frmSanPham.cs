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
    public partial class frmSanPham : Form
    {
        BLL_SanPham sp = new BLL_SanPham();
        Basis ba = new Basis();

        public int MaKho { get; set; }

        public frmSanPham()
        {
            InitializeComponent();
            dgv_SanPham.CellFormatting += Dgv_SanPham_CellFormatting;
        }

        private void Dgv_SanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == dgv_SanPham.Columns["GiaBan"].Index && e.Value != null)
            {
                if (e.Value is float floatValue)
                {
                    e.Value = floatValue.ToString("N0");
                }
            }
            if (e.ColumnIndex == dgv_SanPham.Columns["GiaNhap"].Index && e.Value != null)
            {
                if (e.Value is float floatValue)
                {
                    e.Value = floatValue.ToString("N0");
                }
            }
        }

        public void LoadCbo_TimKiem()
        {
            string[] s = { "Tên sản phẩm"};
            foreach (string i in s)
            {
                cboLoai.Items.Add(i);

            }
            cboLoai.SelectedIndex = 0;
        }

        public void LoadData()
        {
            sp.getSPs(dgv_SanPham);
            tssLbl_TongCong.Text = dgv_SanPham.RowCount.ToString();

        }

        private void tsBtn_SapXep_Click(object sender, EventArgs e)
        {
			try
			{
				if (dgv_SanPham.RowCount < 1)
				{
					MessageBox.Show("Chưa có dữ liệu !");
				}
				else
				{
					sp.sort(dgv_SanPham);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi sắp xếp sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void tsBtn_XuatExcel_Click(object sender, EventArgs e)
        {
			try
			{
				ba.XuatExcel(dgv_SanPham);
				MessageBox.Show("Xuất file excel thành công !");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi xuất file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_Reset_Click(object sender, EventArgs e)
        {
			try
			{
				cboLoai.SelectedIndex = 0; 
				ba.clearTextBoxs(this.Controls); 
				LoadSanPham(); 

				MessageBox.Show("Đã làm mới giao diện thành công !");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi làm mới giao diện: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            //LoadData();
            LoadSanPham();
            LoadCbo_TimKiem();
        }

        public void LoadSanPham()
        {
            try
            {
                //dgv_SanPham.DataSource = sp.GetSP_Kho(MaKho);
                //tssLbl_TongCong.Text = dgv_SanPham.RowCount.ToString();

                dgv_SanPham.DataSource = sp.GetSP();
                tssLbl_TongCong.Text = dgv_SanPham.RowCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_Them_Click(object sender, EventArgs e)
        {
            frmThemSP frmTTSanPham = new frmThemSP();
            frmTTSanPham.ShowDialog();
        }

        private void dgv_SanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			try
			{
				if (e.RowIndex >= 0)
				{
					DataGridViewRow selectedRow = dgv_SanPham.Rows[e.RowIndex];
					int maSP = Convert.ToInt32(selectedRow.Cells["MaSP"].Value);

					frmThongTinSP frmTTSanPham = new frmThongTinSP(maSP);
					frmTTSanPham.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi hiển thị thông tin sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_SanPhamCu_Click(object sender, EventArgs e)
        {
			try
			{
				frmLichSuXoaSP frmLichSuXoaSP = new frmLichSuXoaSP();
				frmLichSuXoaSP.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi mở Lịch sử xoá sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnTk_Click(object sender, EventArgs e)
        {
            if (dgv_SanPham.RowCount < 1)
            {
                MessageBox.Show("Chưa có dữ liệu !");
            }
            else
            {
                try
                {
                    string check = cboLoai.SelectedItem.ToString();
                    if (check == "Tên sản phẩm")
                    {
                        //List<ListFrmSP> searchList = sp.Search_ListSP_TenSP(txtBoxSearch.Text);
                        List<ListFrmSP_Kho> searchList = sp.Search_ListSP_TenSP_1(txtBoxSearch.Text, MaKho);
                        dgv_SanPham.DataSource = searchList;
                        MessageBox.Show("Số lượng sản phẩm tìm thấy: " + searchList.Count);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi tìm kiếm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
