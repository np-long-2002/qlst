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
    public partial class frmKho : Form
    {
        BLL_SanPham sp = new BLL_SanPham();
        Basis ba = new Basis();

        public int MaKho { get; set; }
        public int MaNV { get; set; }

        public frmKho()
        {
            InitializeComponent();
            dgvSP.CellFormatting += dgvSP_CellFormatting;
        }

        public void LoadCbo_TimKiem()
        {
			try
			{
				string[] s = { "Tên sản phẩm" };
				foreach (string i in s)
				{
					cboLoai.Items.Add(i);
				}
				cboLoai.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        public void LoadData()
        {
            sp.getSPs(dgvSP);
            tssLbl_TongCong.Text = dgvSP.RowCount.ToString();

        }

        private void tsBtn_Reset_Click(object sender, EventArgs e)
        {
			try
			{
				cboLoai.SelectedIndex = 0;
				ba.clearTextBoxs(this.Controls); 
				LoadSanPham();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_XuatExcel_Click(object sender, EventArgs e)
        {
			try
			{
				ba.XuatExcel(dgvSP); 
				MessageBox.Show("Xuất file excel thành công !");
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
				if (dgvSP.RowCount < 1)
				{
					MessageBox.Show("Chưa có dữ liệu !");
				}
				else
				{
					sp.sort(dgvSP);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi sắp xếp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void tsBtn_Them_Click(object sender, EventArgs e)
        {
			try
			{
				frmThemSPMoiVaoKho frm = new frmThemSPMoiVaoKho();
				frm.MaKho = MaKho;
				frm.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi sử dụng chức năng thêm sản phẩm mới cho kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void frmKho_Load(object sender, EventArgs e)
        {
            //LoadData();
            LoadSanPham();
            LoadCbo_TimKiem();
        }

        public void LoadSanPham()
        {
            try
            {
                dgvSP.DataSource = sp.GetSP_Kho(MaKho);
                tssLbl_TongCong.Text = dgvSP.RowCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSP_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvSP.Columns["dataGridViewTextBoxColumn4"].Index && e.Value != null)
            {
                if (e.Value is float floatValue)
                {
                    e.Value = floatValue.ToString("N0");
                }
            }
            if (e.ColumnIndex == dgvSP.Columns["dataGridViewTextBoxColumn3"].Index && e.Value != null)
            {
                if (e.Value is float floatValue)
                {
                    e.Value = floatValue.ToString("N0");
                }
            }
        }

        private void tsBtn_NhapKho_Click(object sender, EventArgs e)
        {
			try
			{
				frmNhapKho frm = new frmNhapKho();
				frm.maKho = MaKho; 
				frm.manv = MaNV; 
				frm.WindowState = FormWindowState.Maximized;
				frm.FormBorderStyle = FormBorderStyle.None;
				frm.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi sử dụng chức năng nhập kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnTk_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSP.RowCount < 1)
                {
                    MessageBox.Show("Chưa có dữ liệu !");
                }
                else
                {
                    string check = cboLoai.SelectedItem.ToString();
                    if (check == "Tên sản phẩm")
                    {
                        //List<ListFrmSP> searchList = sp.Search_ListSP_TenSP(txtBoxSearch.Text);
                        List<ListFrmSP_Kho> searchList = sp.Search_ListSP_TenSP_1(txtBoxSearch.Text, MaKho);
                        dgvSP.DataSource = searchList;
                        MessageBox.Show("Số lượng sản phẩm tìm thấy: " + searchList.Count);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_ThemKho_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemKho frm = new frmThemKho();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi thêm nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
