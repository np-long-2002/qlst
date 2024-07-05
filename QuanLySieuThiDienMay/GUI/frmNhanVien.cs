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
    public partial class frmNhanVien : Form
    {
        
        BLL_NhanVien nv = new BLL_NhanVien();
        Basis ba = new Basis();

        public frmNhanVien()
        {
            InitializeComponent();
            Load += frmNhanVien_Load;
            tbTK = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            tbTK.SetToolTip(btnTk, "Tìm Kiếm");
            LoadCbo_TimKiem();
        }

        public void LoadCbo_TimKiem()
        {
            try
            {
                string[] strings = { "Mã nhân viên", "Tên nhân viên", "Giới tính" };
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
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void btnTk_Click(object sender, EventArgs e)
        {
			try
			{
				if (dgvListNV.RowCount < 1)
				{
					MessageBox.Show("Chưa có dữ liệu!");
				}
				else
				{
					string check = cboLoai.SelectedItem.ToString();
					if (check == "Mã nhân viên")
					{
						List<ListFrmNV> searchList = nv.Search_ListNV_MaNV(int.Parse(txtBoxSearch.Text));
						dgvListNV.DataSource = searchList;
						MessageBox.Show("Số lượng nhân viên tìm thấy: " + searchList.Count);
					}
					else if (check == "Tên nhân viên")
					{
						List<ListFrmNV> searchList = nv.Search_ListNV(txtBoxSearch.Text);
						dgvListNV.DataSource = searchList;
						MessageBox.Show("Số lượng nhân viên tìm thấy: " + searchList.Count);
					}
					else if (check == "Giới tính")
					{
						if (rdoNam.Checked)
						{
							List<ListFrmNV> searchList = nv.Search_ListNV_GTinh("Nam");
							dgvListNV.DataSource = searchList;
							MessageBox.Show("Số lượng nhân viên tìm thấy: " + searchList.Count);
						}
						else if (rdoNu.Checked)
						{
							List<ListFrmNV> searchList = nv.Search_ListNV_GTinh("Nữ");
							dgvListNV.DataSource = searchList;
							MessageBox.Show("Số lượng nhân viên tìm thấy: " + searchList.Count);
						}
						else
						{
							MessageBox.Show("Không tìm thấy nhân viên với: " + txtBoxSearch.Text);
						}
					}
				}
			}
			catch (FormatException ex)
			{
				MessageBox.Show("Định dạng dữ liệu không hợp lệ. Vui lòng kiểm tra lại dữ liệu nhập vào.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void ShowInfoNV(NhanVien nv)
        {
			try
			{
				frmThongTinNV infoNV = new frmThongTinNV(nv);
				infoNV.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi hiển thị thông tin nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        public void LoadDataGridView()
        {
            try
            {
                dgvListNV.DataSource = nv.Get_ListNV();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                dgvListNV.DataSource = nv.Get_ListNV();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemNV frm = new frmThemNV();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi sử dụng chức năng thêm nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 

        private void dgvListNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			try
			{
				if (e.RowIndex >= 0)
				{
					string maNV = dgvListNV.Rows[e.RowIndex].Cells["MaNV"].Value.ToString();
					NhanVien infoNV = nv.Search_NV(int.Parse(maNV));
					if (infoNV != null)
					{
						ShowInfoNV(infoNV);
					}
				}
			}
			catch (Exception ex)
			{
				// Xử lý ngoại lệ tại đây
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_SapXep_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListNV.RowCount < 1)
                {
                    MessageBox.Show("Chưa có dữ liệu !");
                }
                else
                {
                    nv.sort(dgvListNV);
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
                ba.XuatExcel(dgvListNV);
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
                dgvListNV.DataSource = nv.Get_ListNV();
                txtBoxSearch.Text = "";
                if (cboLoai.Items.Count > 0)
                {
                    cboLoai.SelectedIndex = 0;
                }
                rdoNam.Checked = true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_NhanVienCu_Click(object sender, EventArgs e)
        {
            try
            {
                frmLichSuXoaNV frm = new frmLichSuXoaNV();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemNV frm = new frmThemNV();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
