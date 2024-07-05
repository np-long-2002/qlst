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
    public partial class frmBanHang : Form
    {
        BLL_SanPham sp = new BLL_SanPham();
        Basis ba = new Basis();

        public int a = 0;
        public int y = 0;

        public int MaKho { get; set; }

        public frmBanHang()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            sp.getSPs(dgv1);
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgv1_Click(object sender, EventArgs e)
        {
            int curow = dgv1.CurrentRow.Index;
            txtMaSP.Text = dgv1.Rows[curow].Cells[0].Value.ToString();
            txtTenSP.Text = dgv1.Rows[curow].Cells[1].Value.ToString();
            txtMoTa.Text = dgv1.Rows[curow].Cells[2].Value.ToString();
            txtSLSP.Text = dgv1.Rows[curow].Cells[3].Value.ToString();
            txtGiaSP.Text = dgv1.Rows[curow].Cells[4].Value.ToString();
            btnXoa.Enabled = false;
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtMoTa.Text = "";
            txtSLSP.Text = "";
            txtGiaSP.Text = "";
            txtSLM.Text = "1";
            txtSLM.Enabled = true;
            txtChietKhau.Text = "0";
            txtChietKhau.Enabled = true;
            btnXoa.Enabled = false;          
            btnThem.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaSP.Text == "" || txtTenSP.Text == "" || txtMoTa.Text == "" || txtSLSP.Text == "" || txtGiaSP.Text == "")
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin ", "Thông Báo", MessageBoxButtons.OK);
            else
            {
                int a = int.Parse(txtGiaSP.Text);
                int b = int.Parse(txtSLM.Text);
                int d = int.Parse(txtSLSP.Text);
                if (txtChietKhau.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập chiết khấu ! ", "Thông báo", MessageBoxButtons.OK);

                }
                else if (txtSLM.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập số lượng sản phẩm cần bán ! ", "Thông báo", MessageBoxButtons.OK);
                }

                else if (b > d)
                {
                    MessageBox.Show("Số lượng sản phẩm trong kho không đủ để bán\nBạn hãy nhập thêm hàng", "Thông báos", MessageBoxButtons.OK);

                }

                else
                {
                    int x = d - b;
                    int s = a * b;
                    int n = dgv2.Rows.Add();
                    double thanhtien = 0;
                    dgv2.Rows[n].Cells[0].Value = txtMaSP.Text;
                    dgv2.Rows[n].Cells[1].Value = txtTenSP.Text;
                    dgv2.Rows[n].Cells[2].Value = txtMoTa.Text;
                    dgv2.Rows[n].Cells[3].Value = txtGiaSP.Text;
                    dgv2.Rows[n].Cells[4].Value = txtSLM.Text;
                    dgv2.Rows[n].Cells[5].Value = s.ToString();
                    sp.UpdateSoLuong(int.Parse(txtMaSP.Text), int.Parse(x.ToString()));
                    LoadData();
                    txtMaSP.Text = "";
                    txtTenSP.Text = "";
                    txtMoTa.Text = "";
                    txtSLSP.Text = "";
                    txtGiaSP.Text = "";
                    txtSLM.Text = "1";
                    //txtChietKhau.Text = "0";
                    int sc = dgv2.Rows.Count;
                    LoadData();
                    for (int i = 0; i < sc - 1; i++)
                    {
                        thanhtien += float.Parse(dgv2.Rows[i].Cells[5].Value.ToString());
                    }
                    double g = double.Parse(txtChietKhau.Text.ToString());
                    double m = thanhtien + thanhtien * g / 100;
                    lbThanhTien.Text = m.ToString() + " VNĐ";
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int h = a + y;
                sp.UpdateSoLuong(int.Parse(txtMaSP.Text), int.Parse(h.ToString()));
                LoadData();
                int seleRow = dgv2.CurrentCell.RowIndex;
                dgv2.Rows.RemoveAt(seleRow);
                double thanhtien = 0;
                int sc = dgv2.Rows.Count;
                txtMaSP.Text = "";
                txtTenSP.Text = "";
                txtMoTa.Text = "";
                txtSLSP.Text = "";
                txtGiaSP.Text = "";
                txtSLM.Text = "1";
                btnXoa.Enabled = false;

                for (int i = 0; i < sc - 1; i++)
                {
                    thanhtien += float.Parse(dgv2.Rows[i].Cells[5].Value.ToString());
                }
                btnThem.Enabled = true;
                txtSLSP.Enabled = true;
                btnXoa.Enabled = false;
                double g = double.Parse(txtChietKhau.Text.ToString());
                double m = thanhtien + thanhtien * g / 100;
                lbThanhTien.Text = m.ToString();
            }
            catch
            {
                MessageBox.Show("Ban chưa chọn sản phẩm để xóa ! ", "Thông báo", MessageBoxButtons.OK);
            }
            LoadData();
        }

        private void dgv2_Click(object sender, EventArgs e)
        {
            try
            {
                btnThem.Enabled = false;
                txtSLSP.Enabled = false;
                btnXoa.Enabled = true;
                int curow = dgv2.CurrentRow.Index;
                txtMaSP.Text = dgv2.Rows[curow].Cells[0].Value.ToString();
                txtTenSP.Text = dgv2.Rows[curow].Cells[1].Value.ToString();
                txtMoTa.Text = dgv2.Rows[curow].Cells[2].Value.ToString();
                txtGiaSP.Text = dgv2.Rows[curow].Cells[3].Value.ToString();
                txtSLM.Text = dgv2.Rows[curow].Cells[4].Value.ToString();
                y = int.Parse(txtSLM.Text);
                SanPham sanPham = sp.getSL(txtMaSP.Text);
                a = sanPham.SoLuong;
            }
            catch
            {
                MessageBox.Show("Trống ! ", "Thông báo !", MessageBoxButtons.OK);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgv2.RowCount == 0)
                {
                    MessageBox.Show("Danh sách trống không thể xuất file!");
                }
                else
                {
                    double thanhtien = 0;
                    string s = "";
                    int sc = dgv2.Rows.Count;
                    for (int i = 0; i < sc - 1; i++)
                    {
                        s += dgv2.Rows[i].Cells[1].Value.ToString() + " : " + dgv2.Rows[i].Cells[4].Value.ToString() + ",  ";
                        thanhtien += double.Parse(dgv2.Rows[i].Cells[5].Value.ToString());
                    }
                    double g = double.Parse(txtChietKhau.Text.ToString());
                    double m = thanhtien + thanhtien * g / 100;
                    ba.XuatExcel(dgv2);
                    MessageBox.Show("Xuất file excel thành công !");
                    dgv2.Rows.Clear();
                    lbThanhTien.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTk_Click(object sender, EventArgs e)
        {
            if (dgv1.RowCount < 1)
            {
                MessageBox.Show("Chưa có dữ liệu !");
            }
            else
            {
                    List<ListFrmSP> searchList = sp.Search_ListSP_TenSP(txtBoxSearch.Text);
                    dgv1.DataSource = searchList;
                    MessageBox.Show("Số lượng hoá đơn tìm thấy: " + searchList.Count);
            }
        }

        private void btnRs_Click(object sender, EventArgs e)
        {
            LoadData();
            txtBoxSearch.Text = "";
            txtSLM.Text = "1";
            txtChietKhau.Text = "0";
        }
    }
}
