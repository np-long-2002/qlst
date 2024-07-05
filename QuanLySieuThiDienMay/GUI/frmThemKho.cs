using BLL;
using DAL;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace GUI
{
    public partial class frmThemKho : Form
    {
        BLL_Kho k = new BLL_Kho();
        private int maKho;

        public frmThemKho()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            try
            {
                k.getKhos(dgvListKho);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private async Task<(string lat, string lon)> GetToaDoFromDiaChi(string diaChi)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MyWarehouseApp/1.0)");

                    string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(diaChi)}&format=json&addressdetails=1&limit=1";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JArray json = JArray.Parse(responseBody);

                    if (json.Count > 0)
                    {
                        var location = json[0];
                        string lat = location["lat"].ToString();
                        string lon = location["lon"].ToString();
                        return (lat, lon);
                    }
                    else
                    {
                        throw new Exception("Không tìm thấy tọa độ cho địa chỉ này.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi lấy tọa độ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (string.Empty, string.Empty);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string tenKho = txtTenKho.Text;
                string diaChi = txtDiaChi.Text;
                string moTa = txtMoTa.Text;
                string lat = txtLat.Text;
                string lon = txtLon.Text;
                k.ThemKho(tenKho, diaChi, moTa, lat, lon);
                MessageBox.Show("Thêm kho thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có kho này rồi");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                int makh = int.Parse(txtMaKho.Text);
                string tenKho = txtTenKho.Text;
                string diaChi = txtDiaChi.Text;
                string moTa = txtMoTa.Text;
                string lat = txtLat.Text;
                string lon = txtLon.Text;
                k.CapNhatKho(makh, tenKho, diaChi, moTa, lat, lon);
                MessageBox.Show("Cập nhật kho thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật kho: " + ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmThemKho_Load(object sender, EventArgs e)
        {
            txtMaKho.Enabled = false;
            txtLat.Enabled = false;
            txtLon.Enabled = false;
            btnCapNhat.Enabled = false;
            LoadData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKho.Text = "";
            txtTenKho.Text = "";
            txtDiaChi.Text = "";
            txtMoTa.Text = "";
            txtLat.Text = "";
            txtLon.Text = "";
            txtBoxSearch.Text = "";
            LoadData();
            btnThem.Enabled = true;
            btnCapNhat.Enabled = false;
        }

        private void dgvListKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvListKho.Rows[e.RowIndex];
                    txtMaKho.Text = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : "";
                    txtTenKho.Text = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : "";
                    txtDiaChi.Text = row.Cells[2].Value != null ? row.Cells[2].Value.ToString() : "";
                    txtMoTa.Text = row.Cells[3].Value != null ? row.Cells[3].Value.ToString() : "";
                    txtLat.Text = row.Cells[4].Value != null ? row.Cells[4].Value.ToString() : "";
                    txtLon.Text = row.Cells[5].Value != null ? row.Cells[5].Value.ToString() : "";
                }
                btnCapNhat.Enabled = true;
                btnThem.Enabled = false;
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

        private void btnTk_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListKho.RowCount < 1)
                {
                    MessageBox.Show("Chưa có dữ liệu !");
                }
                else
                {
                    List<ListFrmKho> searchList = k.Search_ListKho_TenKho(txtBoxSearch.Text);
                    dgvListKho.DataSource = searchList;
                    MessageBox.Show("Số lượng kho tìm thấy: " + searchList.Count);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi truy cập dữ liệu : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnKiemTra_Click(object sender, EventArgs e)
        {
            try
            {
                string diaChi = txtDiaChi.Text;
                // Lấy tọa độ từ địa chỉ
                var (lat, lon) = await GetToaDoFromDiaChi(diaChi);

                if (!string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(lon))
                {
                    txtLat.Text = lat;
                    txtLon.Text = lon;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tọa độ cho địa chỉ này.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tìm tọa độ: " + ex.Message);
            }
        }
    }
}
