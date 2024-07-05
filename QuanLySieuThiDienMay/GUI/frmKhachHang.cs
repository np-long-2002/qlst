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

namespace GUI
{
    public partial class frmKhachHang : Form
    {
        BLL_KhachHang kh = new BLL_KhachHang();
        Basis ba = new Basis();

        public frmKhachHang()
        {
            InitializeComponent();
            Load += frmKhachHang_Load;
            LoadCbo_TimKiem();
        }

        public void LoadCbo_TimKiem()
        {
			try
			{
				string[] strings = { "Mã khách hàng", "Tên khách hàng", "Giới tính" };

				cboLoai.Items.Clear();

				foreach (string s in strings)
				{
					cboLoai.Items.Add(s);
				}

				if (cboLoai.Items.Count > 0)
				{
					cboLoai.SelectedIndex = 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void ShowInfoKH(KhachHang kh)
        {
			try
			{
				frmThongTinKH infoKH = new frmThongTinKH(kh);
				infoKH.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi hiển thị thông tin khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        public void LoadDataGridView()
        {
			try
			{
				dgvListKH.DataSource = kh.Get_ListKH();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
			try
			{
				dgvListKH.DataSource = kh.Get_ListKH();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnTk_Click(object sender, EventArgs e)
        {
			if (dgvListKH.RowCount < 1)
			{
				MessageBox.Show("Chưa có dữ liệu !");
			}
			else
			{
				string check = cboLoai.SelectedItem?.ToString(); 
				if (check == "Mã khách hàng")
				{
					try
					{
						int maKH = int.Parse(txtBoxSearch.Text);
						List<ListFrmKH> searchList = kh.Search_ListKH_MaKH(maKH);
						dgvListKH.DataSource = searchList;
						MessageBox.Show("Số lượng khách hàng tìm thấy: " + searchList.Count);
					}
					catch (FormatException)
					{
						MessageBox.Show("Vui lòng nhập vào một số nguyên cho mã khách hàng.");
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Đã xảy ra lỗi khi tìm kiếm khách hàng theo mã: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else if (check == "Tên khách hàng")
				{
					try
					{
						string tenKH = txtBoxSearch.Text;
						List<ListFrmKH> searchList = kh.Search_ListKH(tenKH);
						dgvListKH.DataSource = searchList;
						MessageBox.Show("Số lượng khách hàng tìm thấy: " + searchList.Count);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Đã xảy ra lỗi khi tìm kiếm khách hàng theo tên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else if (check == "Giới tính")
				{
					try
					{
						string gioiTinh = rdoNam.Checked ? "Nam" : rdoNu.Checked ? "Nữ" : "";
						if (!string.IsNullOrEmpty(gioiTinh))
						{
							List<ListFrmKH> searchList = kh.Search_ListKH_GTinh(gioiTinh);
							dgvListKH.DataSource = searchList;
							MessageBox.Show("Số lượng khách hàng tìm thấy: " + searchList.Count);
						}
						else
						{
							MessageBox.Show("Vui lòng chọn giới tính để tìm kiếm.");
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Đã xảy ra lỗi khi tìm kiếm khách hàng theo giới tính: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					MessageBox.Show("Vui lòng chọn loại tìm kiếm.");
				}
			}
		}

		private void dgvListKH_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					string maKH = dgvListKH.Rows[e.RowIndex].Cells["MaKH"].Value.ToString();
					KhachHang infoKH = kh.Search_KH(int.Parse(maKH));
					if (infoKH != null)
					{
						ShowInfoKH(infoKH);
					}
				}
			}
			catch (FormatException ex)
			{
				MessageBox.Show("Lỗi: Mã khách hàng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemKH frm = new frmThemKH();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_SapXep_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListKH.RowCount < 1)
                {
                    MessageBox.Show("Chưa có dữ liệu !");
                }
                else
                {
                    kh.sort(dgvListKH);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi sắp xếp dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_XuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListKH.RowCount < 1)
                {
                    MessageBox.Show("Không có dữ liệu để xuất Excel !");
                    return;
                }

                ba.XuatExcel(dgvListKH);
                MessageBox.Show("Xuất file Excel thành công !");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_KhachHangCu_Click(object sender, EventArgs e)
        {
            try
            {
                frmLichSuXoaKH frm = new frmLichSuXoaKH();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_Reset_Click(object sender, EventArgs e)
        {
            try
            {
                dgvListKH.DataSource = kh.Get_ListKH();

                txtBoxSearch.Text = "";

                if (cboLoai.Items.Count > 0)
                {
                    cboLoai.SelectedIndex = 0;
                }

                rdoNam.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
