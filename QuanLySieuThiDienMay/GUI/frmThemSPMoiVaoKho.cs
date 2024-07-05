using BLL;
using DAL;
using Emgu.CV;
using Guna.UI2.WinForms;
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
    public partial class frmThemSPMoiVaoKho : Form
    {
        BLL_NhaCungCap ncc = new BLL_NhaCungCap();
        BLL_SanPham sp = new BLL_SanPham();
        BLL_ChiTietKho ctk = new BLL_ChiTietKho();

        private int masp;
        public int MaKho { get; set; }

        public frmThemSPMoiVaoKho()
        {
            InitializeComponent();
            Load += FrmThemSPMoiVaoKho_Load;
        }

        private void FrmThemSPMoiVaoKho_Load(object sender, EventArgs e)
        {
            LoadNhaCungCap();
        }

        public void LoadNhaCungCap()
        {
            try
            {
                cboNCC.DataSource = ncc.getNhaCungCap();
                cboNCC.DisplayMember = "TenNCC";
                cboNCC.ValueMember = "MaNCC";
                cboNCC.SelectedIndex = 0;
                cboNCC_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
			try
			{
				if (ctk.KiemTraSanPhamDaCoTrongKho(MaKho, masp))
				{
					MessageBox.Show("Sản phẩm đã có trong kho!");
					return;
				}

				ctk.ThemSanPhamVaoKho(MaKho, masp);
				MessageBox.Show("Đã thêm sản phẩm vào kho thành công!");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi thêm sản phẩm vào kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void cboNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboNCC.SelectedItem != null)
                {
                    int maNCC;
                    if (int.TryParse(cboNCC.SelectedValue.ToString(), out maNCC))
                    {
                        dgvListSP.DataSource = sp.GetSP_NCC(maNCC);
                    }
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void dgvListSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			try
			{
				if (e.RowIndex >= 0)
				{
					DataGridViewRow row = dgvListSP.Rows[e.RowIndex];
					masp = int.Parse(row.Cells[0].Value.ToString());
					string tensp = row.Cells[1].Value.ToString();
					lbTenSP.Text = tensp;
				}
			}
			catch (FormatException ex)
			{
				MessageBox.Show("Đã xảy ra lỗi định dạng khi lấy dữ liệu từ ô Cell: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không ?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Hide();
            }
        }
    }
}
