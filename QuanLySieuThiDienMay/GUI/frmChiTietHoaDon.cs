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
    public partial class frmChiTietHoaDon : Form
    {
        BLL_HoaDon hd = new BLL_HoaDon();
        BLL_ChiTietHoaDon cv = new BLL_ChiTietHoaDon();
        Basis ba = new Basis();

        private int maHD;

        public frmChiTietHoaDon(int maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
        }

        private void LoadChiTietHoaDon(int maHD)
        {
			try
			{
				var chiTietHoaDon = cv.TimKiemChiTietHoaDon(maHD);

				if (chiTietHoaDon == null || chiTietHoaDon.Count == 0)
				{
					MessageBox.Show("Không tìm thấy chi tiết hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				double tongTien = chiTietHoaDon.Sum(ct => ct.ThanhTien);

				txtMaHD.Text = maHD.ToString();
				txtTenKH.Text = hd.GetTenKhachHangByMaHD(maHD);
				txtTenNV.Text = hd.GetTenNhanVienByMaHD(maHD);
				lbTT.Text = $"{tongTien.ToString()} VNĐ"; 
				lbNT.Text = hd.GetNgayLapHoaDonByMaHD(maHD).ToString();
				dgv_ChiTiet.DataSource = chiTietHoaDon;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        public void LoadData()
        {
            cv.getCTHDs(dgv_ChiTiet);
        }

        private void tsBtn_Thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadChiTietHoaDon(maHD);
            txtMaHD.Enabled = false;
            txtTenKH.Enabled = false;
            txtTenNV.Enabled = false;
            lbTT.Enabled = false;
            lbNT.Enabled = false;
        }

        private void tsBtn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin hóa đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (hd.DeleteHD(int.Parse(txtMaHD.Text)) == true)
                    {
                        MessageBox.Show("Xóa thông tin hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmHoaDon fr = new frmHoaDon();
                        fr.LoadData();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thông tin hóa đơn không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa thông tin hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin hóa đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (hd.DeleteHD(int.Parse(txtMaHD.Text)) == true)
                    {
                        MessageBox.Show("Xóa thông tin hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmHoaDon fr = new frmHoaDon();
                        fr.LoadData();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thông tin hóa đơn không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa thông tin hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
