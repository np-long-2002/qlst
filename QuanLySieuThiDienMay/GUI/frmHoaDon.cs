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
    public partial class frmHoaDon : Form
    {
        BLL_HoaDon hd = new BLL_HoaDon();
        Basis ba = new Basis();

		public int MaKho {  get; set; }
        public frmHoaDon()
        {
            InitializeComponent();
        }

        public void LoadCbo_TimKiem()
        {
            try
            {
                string[] s = { "Tên nhân viên", "Tên khách hàng", "Trạng thái" };
                foreach (string i in s)
                {
                    cboLoai.Items.Add(i);

                }
                cboLoai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi tải danh sách tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadData()
        {
			try
			{
				dgv_HoaDon.DataSource = hd.getHDs(MaKho);

				tssLbl_TongCong.Text = dgv_HoaDon.RowCount.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_SapXep_Click(object sender, EventArgs e)
        {
			if (dgv_HoaDon.RowCount < 1)
			{
				MessageBox.Show("Chưa có dữ liệu !");
			}
			else
			{
				try
				{
					dgv_HoaDon.DataSource=hd.sort(MaKho); 
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Đã xảy ra lỗi khi sắp xếp dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

        private void tsBtn_XuatExcel_Click(object sender, EventArgs e)
        {
			try
			{
				ba.XuatExcel(dgv_HoaDon); 

				MessageBox.Show("Xuất file excel thành công !");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi xuất file excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_Reset_Click(object sender, EventArgs e)
        {
			try
			{
				cboLoai.SelectedIndex = 0;

				ba.clearTextBoxs(this.Controls); 

				LoadData();

				MessageBox.Show("Đã đặt lại thành công!"); 
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi đặt lại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCbo_TimKiem();
        }

        private void dgv_HoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			try
			{
				if (e.RowIndex >= 0)
				{
					DataGridViewRow row = dgv_HoaDon.Rows[e.RowIndex];

					int maHD = Convert.ToInt32(row.Cells["MaHD"].Value);

					ShowChiTietHoaDon(maHD);
				}
			}
			catch (FormatException ex)
			{
				MessageBox.Show("Lỗi định dạng: Không thể chuyển đổi giá trị sang số nguyên.\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void ShowChiTietHoaDon(int maHD)
        {
			try
			{
				frmChiTietHoaDon frm = new frmChiTietHoaDon(maHD);
				frm.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi hiển thị chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_DoiTra_Click(object sender, EventArgs e)
        {
			try
			{
				frmDoiTraSanPham frm = new frmDoiTraSanPham();
				frm.MaKho = MaKho;
				frm.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi mở chức năng đổi trả sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_HoaDonCu_Click(object sender, EventArgs e)
        {
			try
			{
				frmLichSuXoaHD frm = new frmLichSuXoaHD();
				frm.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi mở lịch sử hóa đơn cũ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnTk_Click(object sender, EventArgs e)
        {
            if (dgv_HoaDon.RowCount < 1)
            {
                MessageBox.Show("Chưa có dữ liệu !");
            }
            else
            {
                string check = cboLoai.SelectedItem.ToString();
                if (check == "Tên nhân viên")
                {
                    List<ListFrmHD> searchList = hd.Search_ListHD_TenNV(txtBoxSearch.Text, MaKho);
                    dgv_HoaDon.DataSource = searchList;
                    MessageBox.Show("Số lượng hoá đơn tìm thấy: " + searchList.Count);
                }
                else
                {
                    if (check == "Tên khách hàng")
                    {
                        List<ListFrmHD> searchList = hd.Search_ListHD_TT(txtBoxSearch.Text, MaKho);
                        dgv_HoaDon.DataSource = searchList;
                        MessageBox.Show("Số lượng hoá đơn tìm thấy: " + searchList.Count);
                    }
                    else
                    {
                        List<ListFrmHD> searchList = hd.Search_ListHD_TT(txtBoxSearch.Text, MaKho);
                        dgv_HoaDon.DataSource = searchList;
                        MessageBox.Show("Số lượng hoá đơn tìm thấy: " + searchList.Count);
                    }
                }
            }
        }
    }
}
