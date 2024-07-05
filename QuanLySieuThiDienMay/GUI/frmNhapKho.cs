using BLL;
using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{

    public partial class frmNhapKho : Form
    {
        BLL_NhaCungCap ncc = new BLL_NhaCungCap();
        BLL_SanPham sp = new BLL_SanPham();
        BLL_PhieuKho pk = new BLL_PhieuKho();
        BLL_ChiTietPhieuKho ctpk = new BLL_ChiTietPhieuKho();
        Basis ba = new Basis();

        public int maKho { get; set; }
        public int manv { get; set; }

        private string tenNhanVienSelected = "";

        public frmNhapKho()
        {
            InitializeComponent();
            Load += FrmNhapXuatKho_Load;
        }

        private void FrmNhapXuatKho_Load(object sender, EventArgs e)
        {
            LoadNhaCungCap(maKho);
            LoadPhieuKho(maKho);
        }

		private void LoadNhaCungCap(int maKho)
		{
			try
			{
				List<NhaCungCap> nhaCungCapList = ncc.GetNCC_Kho(maKho);
				cboNCC.DataSource = nhaCungCapList;
				cboNCC.DisplayMember = "TenNCC";
				cboNCC.ValueMember = "MaNCC";
				if (nhaCungCapList.Count > 0)
				{
					cboNCC.SelectedIndex = 0;
					cboNCC_SelectedIndexChanged(null, null);
				}
				else
				{
					MessageBox.Show("Không có nhà cung cấp nào cho kho này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void LoadPhieuKho(int maKho)
		{
			try
			{
				dgvPhieuKho.DataSource = pk.Get_PhieuKho_Kho(maKho);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void tsBtn_Close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void tsBtn_XacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                int mapk = pk.AddPhieuKho(manv, maKho);
                foreach (DataGridViewRow row in dgvNhapSP.Rows)
                {
                    int masp = Convert.ToInt32(row.Cells[0].Value);
                    int soluong = Convert.ToInt32(row.Cells[2].Value);
                    int dongia = Convert.ToInt32(row.Cells[3].Value);

                    ctpk.AddChiTietPhieuKho(mapk, masp, soluong, dongia);
                }

                MessageBox.Show("Đã thêm lần lượt các sản phẩm vào chi tiết phiếu kho thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
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
						dgvListSV.DataSource = sp.GetSP_NCC_Kho(maNCC, maKho);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void dgvPhieuKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			try
			{
				if (e.RowIndex >= 0)
				{
					int maPK = int.Parse(dgvPhieuKho.Rows[e.RowIndex].Cells[0].Value.ToString());
					dgvChiTietPhieuKho.DataSource = ctpk.GetChiTietPhieuKho(maPK);
                    tenNhanVienSelected = pk.GetTenNhanVien(maPK);
                }
			}
			catch (FormatException ex)
			{
				MessageBox.Show("Lỗi định dạng dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnThem_Click(object sender, EventArgs e)
        {
			try
			{
				if (dgvListSV.SelectedRows.Count > 0)
				{
					var selectedRow = dgvListSV.SelectedRows[0];
					int maSP = (int)selectedRow.Cells[0].Value;
					string tenSP = (string)selectedRow.Cells[1].Value;
					int soLuong = int.Parse(txtSL.Text);
					int donGia = int.Parse(txtGT.Text);
					if (!ctpk.KiemTraGiaNhap(maSP, donGia))
					{
						MessageBox.Show("Đơn giá nhập phải nhỏ hơn giá bán của sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					double thanhTien = soLuong * donGia;
					dgvNhapSP.Rows.Add(maSP, tenSP, soLuong, donGia, thanhTien);

					txtSL.Clear();
					txtGT.Clear();
				}
				else
				{
					MessageBox.Show("Vui lòng chọn một sản phẩm từ danh sách.");
				}
			}
			catch (FormatException ex)
			{
				MessageBox.Show("Lỗi định dạng dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                int mapk = pk.AddPhieuKho(manv, maKho);
                List<ListNhapHang> exportDataList = new List<ListNhapHang>();

                foreach (DataGridViewRow row in dgvNhapSP.Rows)
                {
                    int masp = Convert.ToInt32(row.Cells[0].Value);
                    int soluong = Convert.ToInt32(row.Cells[2].Value);
                    int dongia = Convert.ToInt32(row.Cells[3].Value);

                    ctpk.AddChiTietPhieuKho(mapk, masp, soluong, dongia);
                    var product = sp.GetNCCSP(masp);
                    var tenSP = product.TenSP;
                    var maNCC = product.MaNCC;
                    var supplier = ncc.GetNhaCungCapByMaNCC(maNCC);
                    var tenNCC = supplier?.TenNCC;
                    exportDataList.Add(new ListNhapHang
                    {
                        TenNCC = tenNCC,
                        NgayNhapXuatKho = DateTime.Now.Date,
                        TenSP = tenSP,
                        MaSP = masp,
                        SoLuong = soluong,
                        DonGia = dongia,
                        ThanhTien = soluong * dongia
                    });
                }

                ba.ExportToExcelNCC(exportDataList);
                dgvNhapSP.Rows.Clear();
                MessageBox.Show("Đã thêm lần lượt các sản phẩm vào chi tiết phiếu kho thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                LoadNhaCungCap(maKho);
                LoadPhieuKho(maKho);
                dgvChiTietPhieuKho.DataSource = null;
                dgvChiTietPhieuKho.Rows.Clear();
                txtSL.Clear();
                txtGT.Clear();
                dgvNhapSP.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       
    }
}
