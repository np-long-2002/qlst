using BLL;
using DAL;
using Emgu.CV;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
	public partial class frmDoiTraSanPham : Form
	{

		BLL_HoaDon hd = new BLL_HoaDon();
		BLL_ChiTietHoaDon cthd = new BLL_ChiTietHoaDon();
		BLL_SanPham sp = new BLL_SanPham();
		BLL_HoaDonDoiTra hddt = new BLL_HoaDonDoiTra();
		BLL_ChiTietDoiTra cthddt = new BLL_ChiTietDoiTra();
		BLL_NhaCungCap ncc = new BLL_NhaCungCap();
		BLL_ChiTietKho ctk = new BLL_ChiTietKho();
        Basis ba = new Basis();

        public int MaKho {  get; set; }
        private string tenKhachHangSelected = "";
        private string tenNhanVienSelected = "";
        private string ngayDoiTraSelected = "";
        private string tenSanPhamSelected = "";
        private string tenSanPham = "";

        public frmDoiTraSanPham()
		{
			InitializeComponent();
		}

		private void frmDoiTraSanPham_Load(object sender, EventArgs e)
		{
            LoadHoaDonDoiTra();
            cboLyDo.Items.Add("Sản phẩm bị lỗi kỹ thuật");
            cboLyDo.Items.Add("Sản phẩm bị hư hỏng hoặc bị vỡ");
            cboLyDo.Items.Add("Sản phẩm không đúng với thông số kỹ thuật hoặc mô tả");
            cboLyDo.Items.Add("Sản phẩm bị trầy xước hoặc có vấn đề về ngoại hình");
            cboLyDo.Items.Add("Sản phẩm không tương thích hoặc không hoạt động tốt với thiết bị khác");
            if (cboLyDo.Items.Count > 0)
            {
                cboLyDo.SelectedIndex = 0;
            }
            for (int i = 1; i <= 12; i++)
            {
                cbxThang.Items.Add(i);
            }
            cbxThang.SelectedIndex = DateTime.Now.Month - 1;
        }

		private void LoadComboBoxHoaDon(string phoneNumber)
		{
            var invoices = hd.GetHoaDonBySDT(phoneNumber);
            cboHoaDon.DataSource = invoices;
            cboHoaDon.DisplayMember = "NgayLap";
            cboHoaDon.ValueMember = "MaHD";
            if (invoices.Any())
            {
                cboHoaDon.SelectedIndex = 0;
            }
        }

		public void LoadHoaDonDoiTra()
		{
            try
            {
                dgv_hddt.DataSource = hddt.Get_HDDT();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu hóa đơn đổi trả: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void btnThoat_Click_1(object sender, EventArgs e)
		{
			if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				this.Hide();
			}
		}

		private void btnKhoiTaoHoaDon_Click(object sender, EventArgs e)
		{
            try
            {
                int maHD = 0;

                if (cboHoaDon.SelectedValue != null && int.TryParse(cboHoaDon.SelectedValue.ToString(), out maHD))
                {
                    ProcessHoaDonDoiTra(maHD);
                }
                else if (!string.IsNullOrEmpty(txtHD.Text) && int.TryParse(txtHD.Text, out maHD))
                {
                    ProcessHoaDonDoiTra(maHD);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một hóa đơn để đổi trả hoặc nhập mã hóa đơn vào ô 'Mã hóa đơn'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tạo hóa đơn đổi trả: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void ProcessHoaDonDoiTra(int maHD)
		{
            int maHDDT = hddt.AddHDDT(maHD);

            if (maHDDT > 0)
            {
                foreach (DataGridViewRow row in dgv_sp.Rows)
                {
                    if (row.Cells["MaSP"].Value != null && row.Cells["SoLuong"].Value != null && row.Cells["LyDo"].Value != null)
                    {
                        int maSP = int.Parse(row.Cells["MaSP"].Value.ToString());
                        int soLuong = int.Parse(row.Cells["SoLuong"].Value.ToString());
                        string lydo = row.Cells["LyDo"].Value.ToString();

                        cthddt.AddCTHDDT(maHDDT, maSP, soLuong, lydo);
                    }
                }

                MessageBox.Show("Đã tạo hóa đơn đổi trả và thêm chi tiết thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadHoaDonDoiTra();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi tạo hóa đơn đổi trả!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void guna2ImageButton1_Click(object sender, EventArgs e)
		{
            string maHDText = txtHD.Text;
            string phoneNumber = txtTimKiemSDT.Text;

            if (!string.IsNullOrEmpty(maHDText))
            {
                if (int.TryParse(maHDText, out int maHD))
                {
                    var invoice = hd.GetHDMAHD(maHD);
                    if (invoice != null)
                    {
                        DisplayInvoiceDetails(invoice);
                        LoadSPCTHD(invoice.MaHD);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy hóa đơn với mã này.");
                    }
                }
                else
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ.");
                }
            }
            else if (!string.IsNullOrEmpty(phoneNumber))
            {
                LoadComboBoxHoaDon(phoneNumber);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn hoặc số điện thoại khách hàng.");
            }
        }

		private void cbHoaDon_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (cboHoaDon.SelectedItem != null)
            {
                var selectedInvoice = (ListFrmHD)cboHoaDon.SelectedItem;
                DisplayInvoiceDetails(selectedInvoice);
                LoadSPCTHD(selectedInvoice.MaHD);
            }
        }

		private void DisplayInvoiceDetails(ListFrmHD invoice)
		{
            txtTenKH.Text = invoice.MaKH;
            txtTenNV.Text = invoice.MaNV;
            txtHinhThuc.Text = invoice.LoaiHinhThuc;
            txtLoaiHoaDon.Text = invoice.MoTaTrangThai;
        }

		public void LoadSPCTHD(int maHD)
		{
            var products = cthd.GetSPCTHD(maHD);
            cboSPHD.DataSource = products;
            cboSPHD.DisplayMember = "TenSP";
            cboSPHD.ValueMember = "MaSP";
            if (products.Any())
            {
                cboSPHD.SelectedIndex = 0;
            }
        }

		private void cboSPHD_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (cboSPHD.SelectedItem != null)
            {
                var selectedProduct = (ListCTHD_SP)cboSPHD.SelectedItem;
                DisplayProductDetails(selectedProduct);
                CheckSLSP(selectedProduct.MaSP, MaKho);
            }
        }

		private void DisplayProductDetails(ListCTHD_SP product)
		{
            txtSoLuong.Text = product.SoLuong.ToString();
        }

		private void CheckSLSP(int maSP,int maKho)
		{
            int quantityInWarehouse = ctk.getSLKho(maSP, maKho);
            if (quantityInWarehouse > 0)
            {
                txtSLKho.Text = quantityInWarehouse.ToString();
            }
            else
            {
                txtSLKho.Text = "Sản phẩm đã hết hàng trong kho.";
            }
        }

		private void btnDoiTra_Click(object sender, EventArgs e)
		{
            if (cboSPHD.SelectedItem != null)
            {
                var selectedProduct = (ListCTHD_SP)cboSPHD.SelectedItem;
                int maSP = selectedProduct.MaSP;
                string tenSP = selectedProduct.TenSP;
                int soLuongsp = selectedProduct.SoLuong;

                if (int.TryParse(txtSoLuong.Text, out int soLuong) && int.TryParse(txtSLKho.Text, out int soLuongKho))
                {
                    string selectedReason = cboLyDo.SelectedItem?.ToString();

                    if (soLuong <= 0)
                    {
                        MessageBox.Show("Số lượng phải lớn hơn 0.");
                        return;
                    }

                    if (soLuong > soLuongKho)
                    {
                        MessageBox.Show("Số lượng sản phẩm trong kho không đủ.");
                        return;
                    }

                    if (selectedReason == null)
                    {
                        MessageBox.Show("Vui lòng chọn lý do.");
                        return;
                    }

                    int totalSoLuongInDataGridView = 0;

                    foreach (DataGridViewRow row in dgv_sp.Rows)
                    {
                        if (row.Cells["MaSP"].Value != null && (int)row.Cells["MaSP"].Value == maSP)
                        {
                            totalSoLuongInDataGridView += (int)row.Cells["SoLuong"].Value;
                        }
                    }

                    if (totalSoLuongInDataGridView + soLuong > soLuongsp)
                    {
                        MessageBox.Show("Tổng số lượng sản phẩm đổi trả không được vượt quá số lượng trên hóa đơn.");
                        return;
                    }

                    AddProductToDataGridView(maSP, tenSP, soLuong, selectedReason);
                    sp.UpdateQuantityInCart(maSP, soLuongsp, MaKho);
                    CheckSLSP(maSP, MaKho);
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập thông tin hợp lệ.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm từ hóa đơn.");
            }
        }

		private void AddProductToDataGridView(int maSP, string tenSP, int soLuong,string lydo)
		{
            bool found = false;

            foreach (DataGridViewRow row in dgv_sp.Rows)
            {
                if (row.Cells["MaSP"].Value != null && (int)row.Cells["MaSP"].Value == maSP)
                {
                    int currentSoLuong = (int)row.Cells["SoLuong"].Value;
                    string currentLyDo = row.Cells["LyDo"].Value.ToString();

                    if (currentLyDo == lydo)
                    {
                        if (currentSoLuong + soLuong <= int.Parse(txtSLKho.Text))
                        {
                            row.Cells["SoLuong"].Value = currentSoLuong + soLuong;
                            MessageBox.Show("Cập nhật số lượng sản phẩm thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Số lượng sản phẩm trong kho không đủ để cập nhật thêm.");
                        }

                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                dgv_sp.Rows.Add(maSP, tenSP, soLuong, lydo);
                MessageBox.Show("Thêm sản phẩm mới thành công.");
            }
        }

		private void btnXoaSP_Click(object sender, EventArgs e)
		{
            if (dgv_sp.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv_sp.SelectedRows[0];

                if (selectedRow != null)
                {
                    int maSP = (int)selectedRow.Cells["MaSP"].Value;
                    int soLuongsp = (int)selectedRow.Cells["SoLuong"].Value;

                    dgv_sp.Rows.Remove(selectedRow);
                    MessageBox.Show("Xóa sản phẩm thành công.");
                    sp.RestoreQuantityInCart(maSP, soLuongsp, MaKho);
                    CheckSLSP(maSP, MaKho);
                }
                else
                {
                    MessageBox.Show("Không thể xóa sản phẩm. Vui lòng thử lại.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.");
            }
        }

		private void dgv_hddt_CellClick(object sender, DataGridViewCellEventArgs e)
		{
            //if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            //{
            //    DataGridViewRow selectedRow = dgv_hddt.Rows[e.RowIndex];

            //    int maHDDT = Convert.ToInt32(selectedRow.Cells[0].Value);

            //    LoadCTHDDT(maHDDT);
            //}

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dgv_hddt.Rows[e.RowIndex];
                int maHDDT = Convert.ToInt32(selectedRow.Cells["MaHDDT"].Value);
                int maHD = hddt.GetMaHD(maHDDT);
                tenKhachHangSelected = hddt.GetTenKhachHang(maHD);
                tenNhanVienSelected = hddt.GetTenNhanVien(maHD);
                ngayDoiTraSelected = selectedRow.Cells["NgayDoiTra"].Value.ToString();
                tenSanPhamSelected = cthddt.GetTenSPByMaHDDT(maHDDT);
                LoadCTHDDT(maHDDT);
            }
        }

        //private void LoadCTHDDT(int maHDDT)
        //{
        //          dgv_cthddt.DataSource = cthddt.GetListCTDT(maHDDT);
        //          dgv_hddt.ClearSelection();
        //      }

        private void LoadCTHDDT(int maHDDT)
        {
            dgv_cthddt.DataSource = cthddt.GetListCTDT(maHDDT);
            //dgv_hddt.ClearSelection();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maHDText = txtHD.Text;
            string phoneNumber = txtTimKiemSDT.Text;

            if (!string.IsNullOrEmpty(maHDText))
            {
                if (int.TryParse(maHDText, out int maHD))
                {
                    var invoice = hd.GetHDMAHD(maHD);
                    if (invoice != null)
                    {
                        DisplayInvoiceDetails(invoice);
                        LoadSPCTHD(invoice.MaHD);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy hóa đơn với mã này.");
                    }
                }
                else
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ.");
                }
            }
            else if (!string.IsNullOrEmpty(phoneNumber))
            {
                LoadComboBoxHoaDon(phoneNumber);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn hoặc số điện thoại khách hàng.");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHD.Text = "";
            txtTimKiemSDT.Text = "";
            txtLoaiHoaDon.Text = "";
            txtHinhThuc.Text = "";
            txtSoLuong.Text = "";
            txtTenNV.Text = "";
            txtTenKH.Text = "";
            cboHoaDon.SelectedIndex = -1;
            cboSPHD.SelectedIndex = -1;
            cboLyDo.SelectedIndex = 0;
            txtSLKho.Text = "...";

            LoadHoaDonDoiTra();
            //dgv_hddt.DataSource = null;
            dgv_cthddt.DataSource = null;
            //dgv_sp.DataSource = null;
            //dgv_cthddt.Rows.Clear();
            dgv_sp.Rows.Clear();

        }

        private void PrintReturnInvoice(List<ListFrmCTHDDT> hddtDetails)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (s, e) =>
            {
                Graphics graphics = e.Graphics;
                Font regularFont = new Font("Arial", 12);
                Font boldFont = new Font("Arial", 12, FontStyle.Bold);
                float fontHeight = regularFont.GetHeight(e.Graphics);
                int startX = 50;
                int startY = 50;
                int offsetY = 0;
                int rowHeight = 30;

                graphics.DrawString("HÓA ĐƠN ĐỔI HÀNG".PadLeft(30), boldFont, Brushes.Black, startX + 225, startY + offsetY);
                offsetY += rowHeight;
                graphics.DrawString($"Ngày {DateTime.Now.Day} tháng {DateTime.Now.Month} năm {DateTime.Now.Year}".PadLeft(30), regularFont, Brushes.Black, startX + 250, startY + offsetY);
                offsetY += rowHeight;
                offsetY += rowHeight;
                graphics.DrawString("Đơn vị bán hàng: CÔNG TY TNHH LONGHAUPHAT".PadLeft(20), regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                graphics.DrawString("Hình thức thanh toán: ĐỔI HÀNG".PadLeft(15), regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                graphics.DrawString($"Nhân viên đổi hàng: {tenNhanVienSelected}", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;            
                graphics.DrawString($"Khách hàng: {tenKhachHangSelected}".PadLeft(15), regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                graphics.DrawString("----------------------------------------------------------", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;

                graphics.DrawString("STT".PadRight(5), boldFont, Brushes.Black, startX, startY + offsetY);
                graphics.DrawString("Tên sản phẩm".PadRight(35), boldFont, Brushes.Black, startX + 50, startY + offsetY);
                graphics.DrawString("Số lượng".PadRight(15), boldFont, Brushes.Black, startX + 450, startY + offsetY);
                graphics.DrawString("Lý do".PadRight(15), boldFont, Brushes.Black, startX + 550, startY + offsetY);
                offsetY += rowHeight;

                int stt = 1;
                foreach (var item in hddtDetails)
                {
                    //string tenSanPham = tenSanPhamSelected;
                    string tenSanPham = sp.GetTenSPByMaSP(item.MaSP);
                    string lyDo = item.LyDo;

                    graphics.DrawString($"{stt}".PadRight(5), regularFont, Brushes.Black, startX, startY + offsetY);
                    graphics.DrawString($"{tenSanPham}".PadRight(35), regularFont, Brushes.Black, startX + 50, startY + offsetY);
                    graphics.DrawString($"{item.SoLuong}".PadRight(15), regularFont, Brushes.Black, startX + 450, startY + offsetY);
                    graphics.DrawString($"{lyDo}".PadRight(15), regularFont, Brushes.Black, startX + 550, startY + offsetY);

                    offsetY += rowHeight;

                    stt++;
                }

                graphics.DrawString("----------------------------------------------------------", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                graphics.DrawString($"Điều khoản:", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                graphics.DrawString($"- Phải có hoá đơn gốc", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                graphics.DrawString($"- Sản phẩm phải còn nguyên vẹn", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                graphics.DrawString($"- Sản phẩm không bị hư hỏng, không bị dơ bẩn", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                graphics.DrawString($"- Có đầy đủ phụ kiện kèm theo", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                offsetY += rowHeight;
                graphics.DrawString("Khách hàng".PadRight(30), boldFont, Brushes.Black, startX, startY + offsetY);
                graphics.DrawString("Nhân viên", boldFont, Brushes.Black, startX + 300, startY + offsetY);
                offsetY += rowHeight;
                graphics.DrawString("(Ký, ghi rõ họ tên)".PadRight(30), regularFont, Brushes.Black, startX, startY + offsetY);
                graphics.DrawString("(Ký, đóng dấu, ghi rõ họ tên)", regularFont, Brushes.Black, startX + 300, startY + offsetY);
                offsetY += rowHeight;
                graphics.DrawString("....................".PadRight(30), regularFont, Brushes.Black, startX, startY + offsetY);
                graphics.DrawString("....................", regularFont, Brushes.Black, startX + 300, startY + offsetY);
            };

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.PrintPreviewControl.Zoom = 1.0;
            printPreviewDialog.ClientSize = new Size(1641, 1021);
            printPreviewDialog.WindowState = FormWindowState.Maximized;
            printPreviewDialog.ShowDialog();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            int selectedMonth = (int)cbxThang.SelectedItem;
            var doitra = cthddt.GetDoiTraNCCThang(selectedMonth);
            ba.ExportToExcel(doitra);
        }

        private void cbxThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedMonth = (int)cbxThang.SelectedItem;
            var data = hddt.Get_HDDTTheoThang(selectedMonth);
            dgv_hddt.DataSource = data;
        }

        private void btnXuatHD_Click(object sender, EventArgs e)
        {
            if (dgv_hddt.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv_hddt.SelectedRows[0];
                if (selectedRow != null)
                {
                    int maHDDT = Convert.ToInt32(selectedRow.Cells["MaHDDT"].Value);
                    var hddtDetails = hddt.Get_HDDTDetails(maHDDT);
                    if (hddtDetails != null && hddtDetails.Count > 0)
                    {
                        PrintReturnInvoice(hddtDetails);
                    }
                    else
                    {
                        MessageBox.Show("Không có chi tiết hóa đơn đổi trả để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một hóa đơn đổi trả để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Không thể xuất hoá đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_sp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dgv_sp.Rows[e.RowIndex];
                    int maSP = Convert.ToInt32(row.Cells["MaSP"].Value);
                    tenSanPham = sp.GetTenSPByMaSP(maSP);

                    if (!string.IsNullOrEmpty(tenSanPham))
                    {
                        foreach (var item in cboSPHD.Items)
                        {
                            var product = item as ListCTHD_SP;
                            if (product != null && product.TenSP == tenSanPham)
                            {
                                cboSPHD.SelectedItem = product;
                                break;
                            }
                        }
                    }

                    txtSoLuong.Text = row.Cells["SoLuong"].Value.ToString();
                    string lyDo = row.Cells["LyDo"].Value.ToString();
                    cboLyDo.SelectedItem = lyDo;
                }
            }
        }

        private void dgv_cthddt_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
