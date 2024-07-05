using BLL;
using DAL;
using Guna.UI2.WinForms;
using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{

    public partial class frmThongKe : Form
    {
        BLL_HoaDon hd = new BLL_HoaDon();
        BLL_NhanVien nv = new BLL_NhanVien();
        Basis ba = new Basis();

        public int MaKho { get; set; }

        public frmThongKe()
        {
            InitializeComponent();
            Load += FrmThongKe_Load;
        }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            cboChonLoai.Items.AddRange(new string[] { "Ngày", "Tuần", "Tháng", "Quý" });
            cboChonLoai.SelectedIndex = 0;
            //dtaNgay.Value = DateTime.Now;
        }

        private void cboChonLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadChartDoanhThu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải biểu đồ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       private void LoadChartDoanhThu()
		{
			try
			{
				string filterType = cboChonLoai.SelectedItem.ToString();

				if (filterType == "Tháng")
				{
					List<ListDoanhThu> doanhThuList = hd.TinhDoanhThuTheoThang(MaKho);
					LoadChart(doanhThuList);
				}
				else if (filterType == "Quý")
				{
					List<ListDoanhThu> doanhThuList = hd.TinhDoanhThuTheoQuy(MaKho);
					LoadChart(doanhThuList);
				}
				else if (filterType == "Tuần")
				{
					List<ListDoanhThu> doanhThuList = hd.TinhDoanhThuTheoTuan(MaKho);
					LoadChart(doanhThuList);
				}
				else if (filterType == "Ngày")
				{
					DateTime selectedDate = dtaNgay.Value.Date;
					List<ListDoanhThuKho> doanhThuKhoList = hd.TinhDoanhThuTheoNgay(selectedDate);
					LoadChart(doanhThuKhoList);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi tải biểu đồ doanh thu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void LoadChart(List<ListDoanhThu> doanhThuList)
        {
            chart1.Series.Clear();
            Series series = chart1.Series.Add("Doanh thu");
            series.ChartType = SeriesChartType.Column;
            foreach (var item in doanhThuList)
            {
                series.Points.AddXY(item.ThoiGian, item.DoanhThu);
            }
            ConfigureChart();
        }

        private void LoadChart(List<ListDoanhThuKho> doanhThuKhoList)
        {
            chart1.Series.Clear();
            Series series = chart1.Series.Add("Doanh thu");
            series.ChartType = SeriesChartType.Column;
            foreach (var item in doanhThuKhoList)
            {
                series.Points.AddXY(item.TenKho, item.DoanhThu);
            }
            ConfigureChart();
        }

        private void ConfigureChart()
        {
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.LightGray;

            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.LightGray;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không ?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Hide();
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Export Excel";
            savefile.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ba.XuatExcelChart(chart1, savefile.FileName);
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dtaNgay_ValueChanged(object sender, EventArgs e)
        {
            LoadChartDoanhThu();
        }
    }
}
