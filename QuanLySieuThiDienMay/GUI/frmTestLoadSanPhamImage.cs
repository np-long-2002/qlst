using BLL;
using DAL;
using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Firebase.Storage;
using System.Net.Http;
using System.Threading.Tasks;
using Guna.UI2.WinForms;


namespace GUI
{
    public partial class frmTestLoadSanPhamImage : Form
    {
        StringBuilder hd = new StringBuilder();

        BLL_SanPham sp = new BLL_SanPham();
        BLL_LoaiSanPham lsp = new BLL_LoaiSanPham();
        BLL_NhaCungCap ncc = new BLL_NhaCungCap();
        BLL_KhachHang kh = new BLL_KhachHang();
        BLL_HoaDon hdd = new BLL_HoaDon();
        BLL_ChiTietHoaDon cthd = new BLL_ChiTietHoaDon();
        BLL_KhuyenMai km = new BLL_KhuyenMai();
        Basis ba = new Basis();

        private List<ListGioHang> gioHang = new List<ListGioHang>();
        private int selectedProductId = -1;

        List<ListGioHang> gioHangs = new List<ListGioHang>();

        public int MaNV { get; set; }
        public string TenNhanVien { get; set; }
        public float thanhTienn { get; set; }
        public int MaKho { get; set; }
        public int maKH { get; set; }
        public int diemtich { get; set; }

        public int a = 0;
        public int y = 0;

        private int currentPage = 1;
        private int productsPerPage = 12;

        private enum FilterType { None, LoaiSanPham, NhaCungCap, Both }
        private FilterType currentFilterType = FilterType.None;
        private int currentFilterValue;

        //string url = @"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\SanPham";

        public frmTestLoadSanPhamImage()
        {
            InitializeComponent();
            Load += FrmTestLoadSanPhamImage_Load;
            txtTenKH.Enabled = false;
        }

		private void FrmTestLoadSanPhamImage_Load(object sender, EventArgs e)
		{
            tbao.SetToolTip(btnQuetMa, "Quét mã sản phẩm");
            tbao.SetToolTip(btnBoLoc, "Lọc sản phẩm");
            tbao.SetToolTip(btnTimKiem, "Tìm kiếm sản phẩm");

            lbTenNV.Text = "Nhân viên: " + TenNhanVien;
            LoadAnhSanPham(currentPage);
            UpdatePageNumber();
            LoadNhaCungCap();
            LoadLoaiSanPham();

            cboNCC.SelectedIndex = -1;
            cboLSP.SelectedIndex = -1;
        }

		private void LoadNhaCungCap()
		{
			try
			{
				cboNCC.DataSource = null;
                List<NhaCungCap> listNCC = ncc.getNhaCungCap();
                cboNCC.DataSource = listNCC;
                cboNCC.DisplayMember = "TenNCC";
                cboNCC.ValueMember = "MaNCC";
            }
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void LoadLoaiSanPham()
		{
			try
			{
				cboLSP.DataSource = null; 
                List<LoaiSanPham> listLSP = lsp.getLoaiSanPham();
                cboLSP.DataSource = listLSP;
                cboLSP.DisplayMember = "MoTa";
                cboLSP.ValueMember = "MaLoaiSP";
            }
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi tải danh sách loại sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private async void LoadAnhSanPham(int page)
        {
            try
            {
                btnThemGH.Enabled = false;
                btnXoaGH.Enabled = false;

                txtMaSP.Enabled = false;
                txtTenSP.Enabled = false;
                txtDonGia.Enabled = false;
                txtNgaySX.Enabled = false;
                txtNCC.Enabled = false;
                txtLSP.Enabled = false;
                txtSLSP.Enabled = false;
                txtKM.Enabled = false;

                List<HinhAnhSanPham> images = sp.GetImageProduct(MaKho);

                int buttonWidth = 145;
                int buttonHeight = 145;
                int spacing = 23;
                int maxPerRow = 6;
                int verticalSpacing = 100;

                int x = spacing;
                int y = spacing;

                int count = 0;

                pnImageProduct.Controls.Clear();

                int startIndex = (page - 1) * productsPerPage;
                int endIndex = Math.Min(startIndex + productsPerPage, images.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    var image = images[i];
                    string imageName = image.AnhSanPham;
                    string firebasePath = $"images/{imageName}";

                    SanPham product = sp.GetProduct(image.MaHinh, MaKho);
                    KhuyenMai promotion = km.GetKhuyenMaiForProduct(product.MaSP);

                    if (product != null)
                    {
                        Button btn = new Button();
                        btn.Width = buttonWidth;
                        btn.Height = buttonHeight;
                        btn.Location = new Point(x, y);
                        btn.BackgroundImageLayout = ImageLayout.Stretch;

                        btn.BackgroundImage = Properties.Resources.placeholder_image; 

                        btn.Tag = image.MaHinh;
                        btn.Click += Btn_Click;
                        btn.MouseLeave += Btn_MouseLeave;

                        pnImageProduct.Controls.Add(btn);

                        Label lbl = new Label();
                        lbl.Name = "lbl_" + product.MaSP;
                        lbl.Text = product.TenSP + "\n" + (product.SoLuong > 0 ? "Số lượng: " + product.SoLuong.ToString() : "Hết hàng");
                        lbl.TextAlign = ContentAlignment.MiddleCenter;
                        lbl.Location = new Point(x, y + buttonHeight + 5);
                        lbl.Width = buttonWidth + 10;
                        lbl.Height = 80;

                        lbl.Font = new Font("Arial", 9, FontStyle.Bold);
                        lbl.Padding = new Padding(0, 0, 0, 0);

                        if (promotion != null)
                        {
                            string promotionText = "Khuyến mãi: ";
                            if (promotion.MaLKM == 1)
                            {
                                promotionText += promotion.GiaTriGiam + "%";
                            }
                            else if (promotion.MaLKM == 2)
                            {
                                promotionText += promotion.GiaTriGiam + " VND";
                            }
                            lbl.Text += "\n" + promotionText;
                        }

                        pnImageProduct.Controls.Add(lbl);

                        btn.Enabled = product.SoLuong > 0;

                        LoadImageAsync(btn, firebasePath);

                        x += buttonWidth + spacing;
                        count++;

                        if (count >= maxPerRow)
                        {
                            x = spacing;
                            y += buttonHeight + verticalSpacing;
                            count = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Dictionary<string, Image> imageCache = new Dictionary<string, Image>();

        private async void LoadImageAsync(Button btn, string firebasePath)
        {
            try
            {
                if (imageCache.ContainsKey(firebasePath))
                {
                    btn.BackgroundImage = imageCache[firebasePath];
                    return;
                }

                var storage = StorageClient.Create();
                using (var imageStream = new MemoryStream())
                {
                    await storage.DownloadObjectAsync("sieuthidienmay-6e8af.appspot.com", firebasePath, imageStream);
                    imageStream.Position = 0;
                    //btn.BackgroundImage = Image.FromStream(imageStream);
                    var image = Image.FromStream(imageStream);
                    imageCache[firebasePath] = image;
                    btn.BackgroundImage = image;
                }
            }
            catch (Exception ex)
            {
				//MessageBox.Show($"Không thể tải ảnh: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				LogError($"Không thể tải ảnh: {ex.Message}");
			}
        }

		private void LogError(string message)
		{
			Console.WriteLine(message);
		}

		private void TinhTienThua()
		{
			try
			{
				double tongTien = double.Parse(lbThanhTien.Text.TrimEnd(new char[] { ' ', 'V', 'N', 'Đ' }));				

				if (!double.TryParse(txtTienKH.Text, out double tienKhachHang))
				{
					MessageBox.Show("Vui lòng nhập số tiền khách hàng trả đúng định dạng số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}


                if (tienKhachHang < tongTien)
                {
                    MessageBox.Show("Số tiền khách hàng trả không đủ để thanh toán hoá đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                double tienThua = tienKhachHang - tongTien;
				lbTienThua.Text = tienThua.ToString() + " VNĐ";
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void InHoaDon()
        {
            if (dgv_GioHang.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào để thanh toán và in hoá đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtTienKH.Text))
            {
                MessageBox.Show("Bạn chưa nhập số tiền khách hàng trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            double tienKhachHangTra;
            if (!double.TryParse(txtTienKH.Text, out tienKhachHangTra))
            {
                MessageBox.Show("Số tiền khách hàng trả không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double thanhTienHoaDon;
            if (!double.TryParse(lbThanhTien.Text.TrimEnd(' ', 'V', 'N', 'Đ'), out thanhTienHoaDon))
            {
                MessageBox.Show("Lỗi khi đọc tổng tiền hoá đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tienKhachHangTra < thanhTienHoaDon)
            {
                MessageBox.Show("Số tiền khách hàng trả không đủ để thanh toán hoá đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            hd.Clear();

            hd.AppendLine("HÓA ĐƠN BÁN HÀNG".PadLeft(40));
            hd.AppendLine($"Ngày {DateTime.Now.Day} tháng {DateTime.Now.Month} năm {DateTime.Now.Year}".PadLeft(40));
            hd.AppendLine("Đơn vị bán hàng: CÔNG TY TNHH LONGHAUPHAT".PadLeft(20));
            hd.AppendLine("Hình thức thanh toán: THANH TOÁN TRỰC TIẾP".PadLeft(22));
            hd.AppendLine($"Tên người bán hàng: {TenNhanVien}".PadLeft(15));
            hd.AppendLine($"Tên người mua hàng: {txtTenKH.Text.Trim()}".PadLeft(15));
            hd.AppendLine($"Địa chỉ: {txtDiaChiGiaoHang.Text.Trim()}".PadLeft(23));
            hd.AppendLine("----------------------------------------------------------");

            hd.AppendLine("STT".PadRight(5) + "Tên hàng hóa".PadRight(35) + "Số lượng".PadRight(15) + "Đơn giá".PadRight(15) + "Thành tiền".PadRight(15));
            int stt = 1;
            foreach (DataGridViewRow row in dgv_GioHang.Rows)
            {
                string tenSanPham = row.Cells[1].Value.ToString();
                string soLuong = row.Cells[6].Value.ToString();
                string donGia = row.Cells[2].Value.ToString();
                string thanhTien = row.Cells[7].Value.ToString();

                hd.AppendLine($"{stt}".PadRight(5) + $"{tenSanPham}".PadRight(35) + $"{soLuong}".PadRight(15) + $"{donGia}".PadRight(15) + $"{thanhTien}".PadRight(15));
                stt++;
            }

            hd.AppendLine("----------------------------------------------------------");
            hd.AppendLine($"Cộng tiền hàng hóa, dịch vụ: {lbThanhTien.Text}");
            hd.AppendLine("Số tiền viết bằng chữ: ..........................................");
            hd.AppendLine($"Tiền khách hàng trả: {txtTienKH.Text} VNĐ");
            hd.AppendLine($"Tiền thừa: {(double.Parse(txtTienKH.Text.Trim()) - double.Parse(lbThanhTien.Text.TrimEnd(new char[] { ' ', 'V', 'N', 'Đ' }))).ToString()} VNĐ");
            hd.AppendLine("----------------------------------------------------------");
            hd.AppendLine("Người mua hàng".PadRight(30) + "Người bán hàng");
            hd.AppendLine("(Ký, ghi rõ họ tên)".PadRight(30) + "(Ký, đóng dấu, ghi rõ họ tên)");
            hd.AppendLine("....................".PadRight(30) + "....................");

            PrintPreviewDialog ppdHoaDon = new PrintPreviewDialog();
            PrintDocument pdHoaDon = new PrintDocument();
            pdHoaDon.PrintPage += pdHoaDon_PrintPage;
            ppdHoaDon.Document = pdHoaDon;

            ppdHoaDon.PrintPreviewControl.Zoom = 1.0;
            ppdHoaDon.ClientSize = new Size(1641, 1021);
            ppdHoaDon.WindowState = FormWindowState.Maximized;

            ppdHoaDon.ShowDialog();
        }

        private void pdHoaDon_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font regularFont = new Font("Arial", 12);
            Font boldFont = new Font("Arial", 12, FontStyle.Bold);

            int startX = 50;
            int startY = 50;
            int offsetY = 0;
            int rowHeight = 30;

            e.Graphics.DrawString("HÓA ĐƠN BÁN HÀNG", boldFont, Brushes.Black, startX + 260, startY + offsetY);
            offsetY += rowHeight;
            e.Graphics.DrawString($"Ngày {DateTime.Now.Day} tháng {DateTime.Now.Month} năm {DateTime.Now.Year}", regularFont, Brushes.Black, startX + 250, startY + offsetY);
            offsetY += rowHeight;
            offsetY += rowHeight;
            e.Graphics.DrawString("Đơn vị bán hàng: CÔNG TY TNHH LONGHAUPHAT", regularFont, Brushes.Black, startX, startY + offsetY);
            offsetY += rowHeight;
            e.Graphics.DrawString("Hình thức thanh toán: THANH TOÁN TRỰC TIẾP", regularFont, Brushes.Black, startX, startY + offsetY);
            offsetY += rowHeight;
            e.Graphics.DrawString($"Tên người bán hàng: {TenNhanVien}", regularFont, Brushes.Black, startX, startY + offsetY);
            offsetY += rowHeight;
            e.Graphics.DrawString($"Tên người mua hàng: {txtTenKH.Text.Trim()}", regularFont, Brushes.Black, startX, startY + offsetY);
            offsetY += rowHeight;
            e.Graphics.DrawString($"Địa chỉ: {txtDiaChiGiaoHang.Text.Trim()}", regularFont, Brushes.Black, startX, startY + offsetY);
            offsetY += rowHeight;
            e.Graphics.DrawString("----------------------------------------------------------", regularFont, Brushes.Black, startX, startY + offsetY);
            offsetY += rowHeight;

            e.Graphics.DrawString("STT", boldFont, Brushes.Black, startX, startY + offsetY);
            e.Graphics.DrawString("Tên hàng hóa", boldFont, Brushes.Black, startX + 50, startY + offsetY);
            e.Graphics.DrawString("Số lượng", boldFont, Brushes.Black, startX + 450, startY + offsetY);
            e.Graphics.DrawString("Đơn giá", boldFont, Brushes.Black, startX + 550, startY + offsetY);
            e.Graphics.DrawString("Thành tiền", boldFont, Brushes.Black, startX + 650, startY + offsetY);
            offsetY += rowHeight;

            int stt = 1;
            foreach (DataGridViewRow row in dgv_GioHang.Rows)
            {
                string tenSanPham = row.Cells[1].Value.ToString();
                string ngaySanXuat = row.Cells[4].Value.ToString();
                string soLuong = row.Cells[6].Value.ToString();
                string donGia = row.Cells[2].Value.ToString();
                string thanhTien = row.Cells[7].Value.ToString();
                string donViTinh = row.Cells[3].Value.ToString();

                e.Graphics.DrawString($"{stt}", regularFont, Brushes.Black, startX, startY + offsetY);
                e.Graphics.DrawString($"{tenSanPham}", regularFont, Brushes.Black, startX + 50, startY + offsetY);
                e.Graphics.DrawString($"{soLuong}", regularFont, Brushes.Black, startX + 450, startY + offsetY);
                e.Graphics.DrawString($"{donGia}", regularFont, Brushes.Black, startX + 550, startY + offsetY);
                e.Graphics.DrawString($"{thanhTien}", regularFont, Brushes.Black, startX + 650, startY + offsetY);
                offsetY += rowHeight;
                stt++;

            }
            {
                e.Graphics.DrawString("----------------------------------------------------------", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                e.Graphics.DrawString($"Cộng tiền hàng hóa, dịch vụ: {lbThanhTien.Text}", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                e.Graphics.DrawString("Số tiền viết bằng chữ: ..........................................", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                e.Graphics.DrawString($"Tiền khách hàng trả: {txtTienKH.Text} VNĐ", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                e.Graphics.DrawString($"Tiền thừa: {(double.Parse(txtTienKH.Text.Trim()) - double.Parse(lbThanhTien.Text.TrimEnd(new char[] { ' ', 'V', 'N', 'Đ' }))).ToString()} VNĐ", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                e.Graphics.DrawString("----------------------------------------------------------", regularFont, Brushes.Black, startX, startY + offsetY);
                offsetY += rowHeight;
                offsetY += rowHeight;
                e.Graphics.DrawString("Người mua hàng", boldFont, Brushes.Black, startX, startY + offsetY);
                e.Graphics.DrawString("Người bán hàng", boldFont, Brushes.Black, startX + 300, startY + offsetY);
                offsetY += rowHeight;
                e.Graphics.DrawString("(Ký, ghi rõ họ tên)", regularFont, Brushes.Black, startX, startY + offsetY);
                e.Graphics.DrawString("(Ký, đóng dấu, ghi rõ họ tên)", regularFont, Brushes.Black, startX + 300, startY + offsetY);
                offsetY += rowHeight;
                e.Graphics.DrawString("....................", regularFont, Brushes.Black, startX, startY + offsetY);
                e.Graphics.DrawString("....................", regularFont, Brushes.Black, startX + 300, startY + offsetY);
            }
        }

		private void UpdateTotal()
		{
			double thanhtien = 0;
            try
            {
                foreach (DataGridViewRow row in dgv_GioHang.Rows)
                {
                    thanhtien += double.Parse(row.Cells[7].Value.ToString());
                }
                lbThanhTien.Text = thanhtien.ToString() + " VNĐ";
                thanhTienn = float.Parse(thanhtien.ToString());
            }
            catch (Exception ex)
            {
				MessageBox.Show("Đã xảy ra lỗi khi tính tổng thành tiền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            try
            {
                currentPage = 1; 

                if (!string.IsNullOrEmpty(txtsearch.Text.Trim()))
                {
                    LoadSearch(txtsearch.Text.ToLower(), currentPage);
                    UpdatePageNumber();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadSearch(string search, int page)
        {
            try
            {
                btnThemGH.Enabled = false;
                btnXoaGH.Enabled = false;

                txtMaSP.Enabled = false;
                txtTenSP.Enabled = false;
                txtDonGia.Enabled = false;
                txtNgaySX.Enabled = false;
                txtNCC.Enabled = false;
                txtLSP.Enabled = false;
                txtSLSP.Enabled = false;
                txtKM.Enabled = false;

                List<HinhAnhSanPham> images = sp.GetImageProduct(MaKho);

                int buttonWidth = 145;
                int buttonHeight = 145;
                int spacing = 23;
                int maxPerRow = 6;
                int verticalSpacing = 100;

                int x = spacing;
                int y = spacing;

                int count = 0;

                pnImageProduct.Controls.Clear();

                var filteredImages = images.Where(img => {
                    SanPham product = sp.SearchNameProduct(search.ToLower(), img.MaHinh, MaKho);
                    return product != null && product.TenSP.ToLower().Contains(search.ToLower());
                }).ToList();

                int startIndex = (page - 1) * productsPerPage;
                int endIndex = Math.Min(startIndex + productsPerPage, filteredImages.Count);

                foreach (var image in filteredImages.GetRange(startIndex, endIndex - startIndex))
                {
                    string imageName = image.AnhSanPham;
                    string firebasePath = $"images/{imageName}";

                    SanPham product = sp.SearchNameProduct(search.ToLower(), image.MaHinh, MaKho);
                    KhuyenMai promotion = km.GetKhuyenMaiForProduct(product.MaSP);

                    if (product != null)
                    {
                        Button btn = new Button();
                        btn.Width = buttonWidth;
                        btn.Height = buttonHeight;
                        btn.Location = new Point(x, y);
                        btn.BackgroundImageLayout = ImageLayout.Stretch;

                        btn.BackgroundImage = Properties.Resources.placeholder_image;

                        btn.Tag = image.MaHinh;
                        btn.Click += Btn_Click;
                        btn.MouseLeave += Btn_MouseLeave;

                        pnImageProduct.Controls.Add(btn);

                        Label lbl = new Label();
                        lbl.Name = "lbl_" + product.MaSP;
                        lbl.Text = product.TenSP + "\n" + (product.SoLuong > 0 ? "Số lượng: " + product.SoLuong.ToString() : "Hết hàng");
                        lbl.TextAlign = ContentAlignment.MiddleCenter;
                        lbl.Location = new Point(x, y + buttonHeight + 5);
                        lbl.Width = buttonWidth + 10;
                        lbl.Height = 80;

                        lbl.Font = new Font("Arial", 9, FontStyle.Bold);
                        lbl.Padding = new Padding(0, 0, 0, 0);

                        if (promotion != null)
                        {
                            string promotionText = "Khuyến mãi: ";
                            if (promotion.MaLKM == 1)
                            {
                                promotionText += promotion.GiaTriGiam + "%";
                            }
                            else if (promotion.MaLKM == 2)
                            {
                                promotionText += promotion.GiaTriGiam + " VND";
                            }
                            lbl.Text += "\n" + promotionText;
                        }

                        pnImageProduct.Controls.Add(lbl);

                        btn.Enabled = product.SoLuong > 0;

                        LoadImageAsync(btn, firebasePath);

                        x += buttonWidth + spacing;
                        count++;

                        if (count >= maxPerRow)
                        {
                            x = spacing;
                            y += buttonHeight + verticalSpacing;
                            count = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
		{
			try
			{
				btnThemGH.Enabled = true;

				int imageID = (int)((Button)sender).Tag;

				SanPham product = sp.GetProduct(imageID, MaKho);
				KhuyenMai promotion = km.GetKhuyenMaiForProduct(product.MaSP);

				if (product != null)
				{
					selectedProductId = imageID;
					txtMaSP.Text = product.MaSP.ToString();
					txtTenSP.Text = product.TenSP;
					txtDonGia.Text = product.GiaSP.ToString();
					txtNgaySX.Text = product.NgaySX.ToShortDateString();
					txtNCC.Text = product.MaNCC.ToString();
					txtLSP.Text = product.MaLoaiSP.ToString();
					txtSLSP.Text = product.SoLuong.ToString();
					if(promotion != null)
                    {
						if (promotion.MaLKM == 1)
						{
							txtKM.Text = promotion.GiaTriGiam.ToString() + "%";
						}
						else if (promotion.MaLKM == 2)
						{
							txtKM.Text = promotion.GiaTriGiam.ToString() + " VND";
						}
					}
                    else
                    {
                        txtKM.Text = "Không có khuyễn mãi";
                    }
				}
				else
				{
					MessageBox.Show("Không tìm thấy thông tin sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi tải thông tin sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Btn_MouseLeave(object sender, EventArgs e)
		{

        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            try
            {
                InHoaDon();
                DateTime ngayLap = DateTime.Now;
                float tongTien = thanhTienn;
                string diaChiGiaoHang = txtDiaChiGiaoHang.Text.ToString();
                int maNV = MaNV;
                var makhsearch = kh.Search_KH_SDT(txtSDTGiaoHang.Text);
                if (makhsearch != null)
                {
                    maKH = makhsearch.MaKH;
                    //diemtich = (int)(tongTien / 1000);
                }
                else
                {
                    maKH = 1;
                    //diemtich = 0;
                }

                if (!float.TryParse(txtTienKH.Text.Trim(), out float tienKhachHang))
                {
                    MessageBox.Show("Vui lòng nhập số tiền khách hàng trả đúng định dạng số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (tienKhachHang < thanhTienn)
                {
                    MessageBox.Show("Số tiền khách hàng trả không đủ để thanh toán hoá đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int maHD = hdd.Add_HD(ngayLap, tongTien, diaChiGiaoHang, maNV, maKH, MaKho);
                if (maHD > 0)
                {
                    foreach (DataGridViewRow row in dgv_GioHang.Rows)
                    {
                        int maSP = int.Parse(row.Cells["MaSP"].Value.ToString());
                        int soLuong = int.Parse(row.Cells["SoLuong"].Value.ToString());
                        int donGia = int.Parse(row.Cells["GiaSP"].Value.ToString());
                        float thanhTien = float.Parse(row.Cells["ThanhTien"].Value.ToString());
                        cthd.AddCTHD(maHD, maSP, soLuong, donGia, thanhTien);
                    }

                    MessageBox.Show("Đã thêm hóa đơn thành công!");
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi thêm hóa đơn!");
                }
                TinhTienThua();
            }
            catch (Exception ex)
            {
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void btnXuatFileExcel_Click(object sender, EventArgs e)
        {
			try
			{

				if (dgv_GioHang.RowCount == 0)
				{
					MessageBox.Show("Danh sách trống không thể xuất file!");
				}
				else
				{
					double thanhtien = 0;
					string s = "";
					int sc = dgv_GioHang.Rows.Count;
					for (int i = 0; i < sc - 1; i++)
					{
						s += dgv_GioHang.Rows[i].Cells[1].Value.ToString() + " : " + dgv_GioHang.Rows[i].Cells[6].Value.ToString() + ",  ";
						thanhtien += double.Parse(dgv_GioHang.Rows[i].Cells[2].Value.ToString());
					}
					double m = thanhtien;
					ba.XuatExcel(dgv_GioHang);
					MessageBox.Show("Xuất file excel thành công !");
					dgv_GioHang.Rows.Clear();
					lbThanhTien.Text = "0";
					lbTienThua.Text = "0";
					txtTienKH.Text = "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void UpdatePageNumber()
		{
            try
            {
                int totalPages = (int)Math.Ceiling((double)sp.GetImageProduct(MaKho).Count / productsPerPage);
                lbTrang.Text = "Trang " + currentPage + " / " + totalPages;
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSau_Click(object sender, EventArgs e)
        {
            try
            {
                int totalPages = (int)Math.Ceiling((double)sp.GetImageProduct(MaKho).Count / productsPerPage);
                if (currentPage < totalPages && (cboNCC.SelectedIndex != -1 || cboNCC.SelectedIndex != -1))
                {
                    currentPage++;
                    LoadAnhSanPham(currentPage);
                    UpdatePageNumber();
                    LoadCurrentFilter();
                }
                else if (currentPage < totalPages && (cboNCC.SelectedIndex != 1 || cboNCC.SelectedIndex != 1))
                {
                    currentPage++;
                    LoadAnhSanPham(currentPage);
                    UpdatePageNumber();
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage > 1 && (cboNCC.SelectedIndex != -1 || cboNCC.SelectedIndex != -1))
                {
                    currentPage--;
                    LoadAnhSanPham(currentPage);
                    UpdatePageNumber();
                    LoadCurrentFilter();
                }
                if (currentPage > 1 && (cboNCC.SelectedIndex != 1 || cboNCC.SelectedIndex != 1))
                {
                    currentPage--;
                    LoadAnhSanPham(currentPage);
                    UpdatePageNumber();
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void LoadCurrentFilter()
        {
            switch (currentFilterType)
            {
                case FilterType.LoaiSanPham:
                    LoadSearchByLoaiSanPham(currentFilterValue, currentPage);
                    UpdatePageNumberByLoaiSanPham(currentFilterValue);
                    break;
                case FilterType.NhaCungCap:
                    LoadSearchByNhaCungCap(currentFilterValue, currentPage);
                    UpdatePageNumberByNhaCungCap(currentFilterValue);
                    break;
                case FilterType.None:
                    break;
            }
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
		{
			try
			{
				frmScan scanForm = new frmScan();
				scanForm.BarcodeDecoded += ScanForm_BarcodeDecoded;
				scanForm.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ScanForm_BarcodeDecoded(object sender, string barcode)
		{
			try
			{
				string decodedBarcode = barcode;
				SearchProductByBarcode(decodedBarcode);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi khi quét mã vạch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void SearchProductByBarcode(string barcode)
		{
            try
            {
                if (!int.TryParse(barcode, out int parsedBarcode))
                {
                    MessageBox.Show("Mã vạch sản phẩm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SanPham product = sp.GetProductByMaSP(parsedBarcode, MaKho);

                if (product != null)
                {
                    int b;
                    if (!int.TryParse(txtSL.Text, out b))
                    {
                        MessageBox.Show("Số lượng sản phẩm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int remainingQuantity = sp.GetRemainingQuantity(parsedBarcode, gioHang, MaKho);

                    if (b > remainingQuantity)
                    {
                        MessageBox.Show("Số lượng sản phẩm trong kho không đủ để bán!\nBạn hãy nhập thêm hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    MessageBox.Show("Quét mã thành công !");
                    AddProductToCart(product, b); 
                    UpdateSLSP();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm ở kho này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void UpdateProductLabel(int productId, int quantity)
		{
            try
            {
                Label lbl = (Label)pnImageProduct.Controls.Find("lbl_" + productId, true).FirstOrDefault();
                if (lbl != null)
                {
                    SanPham product = sp.GetProduct(productId, MaKho);
                    if (product != null)
                    {
                        lbl.Text = product.TenSP + "\n" + "Số lượng: " + product.SoLuong.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void AddProductToCart(SanPham product, int quantity)
		{
            try
            {
                ListGioHang existingProduct = gioHang.FirstOrDefault(p => p.MaSP == product.MaSP);

                if (existingProduct != null)
                {
                    existingProduct.SoLuong += quantity;
                }
                else
                {
                    gioHang.Add(new ListGioHang
                    {
                        MaSP = product.MaSP,
                        TenSP = product.TenSP,
                        GiaSP = product.GiaSP,
                        NgaySX = product.NgaySX,
                        NhaCungCap = product.MaNCC,
                        MaLoaiSP = product.MaLoaiSP,
                        SoLuong = quantity,
                    });
                }

                sp.UpdateQuantityInCart(product.MaSP, quantity, MaKho);
                RefreshCartDisplay();
                UpdateSLSP();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi thêm sản phẩm vào giỏ hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void UpdateSLSP()
		{
			foreach (ListGioHang item in gioHang)
			{
				UpdateProductLabel(item.MaSP, item.SoLuong);
			}
		}

		private void RefreshCartDisplay()
		{
            try
            {
                dgv_GioHang.Rows.Clear();
                foreach (var item in gioHang)
                {
                    int rowIndex = dgv_GioHang.Rows.Add();
                    DataGridViewRow row = dgv_GioHang.Rows[rowIndex];
                    row.Cells["MaSP"].Value = item.MaSP;
                    row.Cells["TenSP"].Value = item.TenSP;
                    row.Cells["GiaSP"].Value = item.GiaSP;
                    row.Cells["NgaySX"].Value = item.NgaySX;
                    row.Cells["NhaCungCap"].Value = item.NhaCungCap;
                    row.Cells["MaLoaiSP"].Value = item.MaLoaiSP;
                    row.Cells["SoLuong"].Value = item.SoLuong;
                    double thanhTien = item.GiaSP * item.SoLuong;
                    row.Cells["ThanhTien"].Value = thanhTien;
                }
                UpdateThanhTien();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi trong quá trình cập nhật giỏ hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void UpdateThanhTien()
		{
			try
			{
				double thanhTien = 0;

				foreach (var item in gioHang)
				{
					thanhTien += item.GiaSP * item.SoLuong;
				}

				lbThanhTien.Text = thanhTien.ToString() + " VNĐ";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi trong quá trình cập nhật tổng thành tiền: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnThoat_Click(object sender, EventArgs e)
        {
			if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				this.Hide();
			}
		}

        private void btnThemGH_Click(object sender, EventArgs e)
        {
			try
			{
				if (string.IsNullOrEmpty(txtMaSP.Text) || string.IsNullOrEmpty(txtTenSP.Text) || string.IsNullOrEmpty(txtDonGia.Text) || string.IsNullOrEmpty(txtSLSP.Text) || string.IsNullOrEmpty(txtNgaySX.Text) || string.IsNullOrEmpty(txtNCC.Text) || string.IsNullOrEmpty(txtLSP.Text))
				{
					MessageBox.Show("Bạn chưa nhập đầy đủ thông tin", "Thông Báo", MessageBoxButtons.OK);
				}
				else
				{
					int donGia = int.Parse(txtDonGia.Text);
					int slCanBan = int.Parse(txtSL.Text);
					int slTrongKho = int.Parse(txtSLSP.Text);

					if (string.IsNullOrEmpty(txtSL.Text))
					{
						MessageBox.Show("Bạn chưa nhập số lượng sản phẩm cần bán!", "Thông báo", MessageBoxButtons.OK);
					}
					else if (slCanBan > slTrongKho)
					{
						MessageBox.Show("Số lượng sản phẩm trong kho không đủ để bán\nBạn hãy nhập thêm hàng", "Thông báo", MessageBoxButtons.OK);
					}
					else
					{
						KhuyenMai promotion = km.GetKhuyenMaiForProduct(int.Parse(txtMaSP.Text));
						double finalPrice = donGia;

						if (promotion != null)
						{
							if (promotion.MaLKM == 1) 
							{
								finalPrice = donGia - (donGia * (promotion.GiaTriGiam ?? 0) / 100);
							}
							else if (promotion.MaLKM == 2) 
							{
								finalPrice = donGia - (promotion.GiaTriGiam ?? 0);
							}
						}

						bool productExists = false;
						foreach (DataGridViewRow row in dgv_GioHang.Rows)
						{
							if (row.Cells[0].Value.ToString() == txtMaSP.Text)
							{
								int currentQuantity = int.Parse(row.Cells[6].Value.ToString());
								int newQuantity = currentQuantity + slCanBan;
								if (newQuantity > slTrongKho)
								{
									MessageBox.Show("Số lượng sản phẩm trong kho không đủ để bán\nBạn hãy nhập thêm hàng", "Thông báo", MessageBoxButtons.OK);
									return;
								}
								row.Cells[6].Value = newQuantity.ToString();
								row.Cells[7].Value = (newQuantity * finalPrice).ToString("F2");
								productExists = true;
								break;
							}
						}

						if (!productExists)
						{
							int n = dgv_GioHang.Rows.Add();
							dgv_GioHang.Rows[n].Cells[0].Value = txtMaSP.Text;
							dgv_GioHang.Rows[n].Cells[1].Value = txtTenSP.Text;
							dgv_GioHang.Rows[n].Cells[2].Value = txtDonGia.Text;
							dgv_GioHang.Rows[n].Cells[3].Value = txtNgaySX.Text;
							dgv_GioHang.Rows[n].Cells[4].Value = txtNCC.Text;
							dgv_GioHang.Rows[n].Cells[5].Value = txtLSP.Text;
							dgv_GioHang.Rows[n].Cells[6].Value = txtSL.Text;
							dgv_GioHang.Rows[n].Cells[7].Value = (finalPrice * slCanBan).ToString("F2"); 
						}

						sp.UpdateQuantityInCart(int.Parse(txtMaSP.Text), slCanBan, MaKho);

						Label lbl = (Label)pnImageProduct.Controls.Find("lbl_" + txtMaSP.Text, true).FirstOrDefault();
						if (lbl != null)
						{
							SanPham product = sp.GetProduct(int.Parse(txtMaSP.Text), MaKho);
							lbl.Text = product.TenSP + "\n" + "Số lượng: " + product.SoLuong.ToString();

                            if (promotion != null)
                            {
                                string promotionText = "Khuyến mãi: ";
                                if (promotion.MaLKM == 1)
                                {
                                    promotionText += promotion.GiaTriGiam + "%";
                                }
                                else if (promotion.MaLKM == 2)
                                {
                                    promotionText += promotion.GiaTriGiam + " VND";
                                }
                                lbl.Text += "\n" + promotionText;
                            }
                        }
						txtMaSP.Text = "";
						txtTenSP.Text = "";
						txtDonGia.Text = "";
						txtSLSP.Text = "";
						txtNgaySX.Text = "";
						txtNCC.Text = "";
						txtLSP.Text = "";
						txtSL.Text = "1";
						txtKM.Text = "";
						btnThemGH.Enabled = false;
						UpdateTotal();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnXoaGH_Click(object sender, EventArgs e)
        {
			try
			{
                KhuyenMai promotion = km.GetKhuyenMaiForProduct(int.Parse(txtMaSP.Text));

                int maSP = int.Parse(txtMaSP.Text);
				int soLuong = int.Parse(txtSL.Text);

				sp.RestoreQuantityInCart(maSP, soLuong, MaKho);

				ListGioHang existingProduct = gioHang.FirstOrDefault(p => p.MaSP == maSP);
				if (existingProduct != null)
				{
					gioHang.Remove(existingProduct);
				}

				UpdateSLSP();

				Label lbl = (Label)pnImageProduct.Controls.Find("lbl_" + txtMaSP.Text, true).FirstOrDefault();
				if (lbl != null)
				{
					SanPham product = sp.GetProduct(int.Parse(txtMaSP.Text), MaKho);
					lbl.Text = product.TenSP + "\n" + "Số lượng: " + product.SoLuong.ToString();

                    if (promotion != null)
                    {
                        string promotionText = "Khuyến mãi: ";
                        if (promotion.MaLKM == 1)
                        {
                            promotionText += promotion.GiaTriGiam + "%";
                        }
                        else if (promotion.MaLKM == 2)
                        {
                            promotionText += promotion.GiaTriGiam + " VND";
                        }
                        lbl.Text += "\n" + promotionText;
                    }
                }
				//LoadAnhSanPham();
				int seleRow = dgv_GioHang.CurrentCell.RowIndex;
				dgv_GioHang.Rows.RemoveAt(seleRow);
				int sc = dgv_GioHang.Rows.Count;
				txtMaSP.Text = "";
				txtTenSP.Text = "";
				txtDonGia.Text = "";
				txtSLSP.Text = "";
				txtNgaySX.Text = "";
				txtNCC.Text = "";
				txtLSP.Text = "";
				txtSL.Text = "1";
				txtKM.Text = "";
				btnThemGH.Enabled = false;
				btnXoaGH.Enabled = false;
				UpdateTotal();
			}
			catch
			{
				MessageBox.Show("Bạn chưa chọn sản phẩm để xóa ! ", "Thông báo !", MessageBoxButtons.OK);
			}
		}

        private void btnLamMoiGH_Click(object sender, EventArgs e)
        {
            try
            {
                currentPage = 1;
                LoadSearch(txtsearch.Text, currentPage);
                UpdatePageNumber();
                LoadAnhSanPham(currentPage);

                txtsearch.Text = "";
                txtMaSP.Text = "";
                txtTenSP.Text = "";
                txtDonGia.Text = "";
                txtSLSP.Text = "";
                txtNgaySX.Text = "";
                txtNCC.Text = "";
                txtLSP.Text = "";
                txtTienKH.Text = "";
                txtKM.Text = "";
                txtSL.Text = "1";

                txtDiaChiGiaoHang.Text = "";
                txtTenKH.Text = "";
                txtSDTGiaoHang.Text = "";

                cboNCC.SelectedIndex = -1;
                cboLSP.SelectedIndex = -1;

                dgv_GioHang.Rows.Clear();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
			try
			{
                currentPage = 1;

                if (cboNCC.SelectedIndex != -1 && cboLSP.SelectedIndex != -1)
                {
                    int maNCC = (int)cboNCC.SelectedValue;
                    int maLSP = (int)cboLSP.SelectedValue;
                    currentFilterType = FilterType.Both;
                    currentFilterValue = maNCC;
                    currentFilterValue = maLSP;
                    LoadSearchByNhaCungCapAndLoaiSanPham(maNCC, maLSP, currentPage);
                    UpdatePageNumberByNhaCungCapAndLoaiSanPham(maNCC, maLSP);
                }
                else if (cboNCC.SelectedIndex != -1)
                {
                    int maNCC = (int)cboNCC.SelectedValue;
                    cboLSP.SelectedIndex = -1;
                    currentFilterType = FilterType.NhaCungCap;
                    currentFilterValue = maNCC;
                    LoadSearchByNhaCungCap(maNCC, currentPage);
                    UpdatePageNumberByNhaCungCap(maNCC);
                }
                else if (cboLSP.SelectedIndex != -1)
                {
                    int maLSP = (int)cboLSP.SelectedValue;
                    cboNCC.SelectedIndex = -1;
                    currentFilterType = FilterType.LoaiSanPham;
                    currentFilterValue = maLSP;
                    LoadSearchByLoaiSanPham(maLSP, currentPage);
                    UpdatePageNumberByLoaiSanPham(maLSP);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn Nhà cung cấp hoặc Loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //UpdatePageNumber();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private async void LoadSearchByNhaCungCapAndLoaiSanPham(int maNCC, int maLSP, int page)
        {
            try
            {
                btnThemGH.Enabled = false;
                btnXoaGH.Enabled = false;

                txtMaSP.Enabled = false;
                txtTenSP.Enabled = false;
                txtDonGia.Enabled = false;
                txtNgaySX.Enabled = false;
                txtNCC.Enabled = false;
                txtLSP.Enabled = false;
                txtSLSP.Enabled = false;
                txtKM.Enabled = false;

                List<HinhAnhSanPham> images = sp.GetImageProductByNhaCungCapAndLoaiSanPham(maNCC, maLSP, MaKho);

                int buttonWidth = 145;
                int buttonHeight = 145;
                int spacing = 23;
                int maxPerRow = 6;
                int verticalSpacing = 100;

                int x = spacing;
                int y = spacing;

                int count = 0;

                pnImageProduct.Controls.Clear();

                int startIndex = (page - 1) * productsPerPage;
                int endIndex = Math.Min(startIndex + productsPerPage, images.Count);

                foreach (var image in images.GetRange(startIndex, endIndex - startIndex))
                {
                    string imageName = image.AnhSanPham;
                    string firebasePath = $"images/{imageName}";

                    SanPham product = sp.GetProduct(image.MaHinh, MaKho);
                    KhuyenMai promotion = km.GetKhuyenMaiForProduct(product.MaSP);

                    if (product != null)
                    {
                        Button btn = new Button();
                        btn.Width = buttonWidth;
                        btn.Height = buttonHeight;
                        btn.Location = new Point(x, y);
                        btn.BackgroundImageLayout = ImageLayout.Stretch;

                        btn.BackgroundImage = Properties.Resources.placeholder_image;

                        btn.Tag = image.MaHinh;
                        btn.Click += Btn_Click;
                        btn.MouseLeave += Btn_MouseLeave;

                        pnImageProduct.Controls.Add(btn);

                        Label lbl = new Label();
                        lbl.Name = "lbl_" + product.MaSP;
                        lbl.Text = product.TenSP + "\n" + (product.SoLuong > 0 ? "Số lượng: " + product.SoLuong.ToString() : "Hết hàng");
                        lbl.TextAlign = ContentAlignment.MiddleCenter;
                        lbl.Location = new Point(x, y + buttonHeight + 5);
                        lbl.Width = buttonWidth + 10;
                        lbl.Height = 80;

                        lbl.Font = new Font("Arial", 9, FontStyle.Bold);
                        lbl.Padding = new Padding(0, 0, 0, 0);

                        if (promotion != null)
                        {
                            string promotionText = "Khuyến mãi: ";
                            if (promotion.MaLKM == 1)
                            {
                                promotionText += promotion.GiaTriGiam + "%";
                            }
                            else if (promotion.MaLKM == 2)
                            {
                                promotionText += promotion.GiaTriGiam + " VND";
                            }
                            lbl.Text += "\n" + promotionText;
                        }

                        pnImageProduct.Controls.Add(lbl);

                        LoadImageAsync(btn, firebasePath);

                        x += buttonWidth + spacing;
                        count++;

                        if (count >= maxPerRow)
                        {
                            x = spacing;
                            y += buttonHeight + verticalSpacing;
                            count = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePageNumberByNhaCungCapAndLoaiSanPham(int maNCC, int maLSP)
        {
            int totalProducts = sp.GetImageProductByNhaCungCapAndLoaiSanPham(maNCC, maLSP, MaKho).Count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / productsPerPage);
            lbTrang.Text = "Trang " + currentPage + " / " + totalPages;
        }

        private async void LoadSearchByNhaCungCap(int maNCC, int page)
        {
            try
            {
                btnThemGH.Enabled = false;
                btnXoaGH.Enabled = false;

                txtMaSP.Enabled = false;
                txtTenSP.Enabled = false;
                txtDonGia.Enabled = false;
                txtNgaySX.Enabled = false;
                txtNCC.Enabled = false;
                txtLSP.Enabled = false;
                txtSLSP.Enabled = false;
                txtKM.Enabled = false;

                List<HinhAnhSanPham> images = sp.GetImageProductByNhaCungCap(maNCC, MaKho);

                int buttonWidth = 145;
                int buttonHeight = 145;
                int spacing = 23;
                int maxPerRow = 6;
                int verticalSpacing = 100;

                int x = spacing;
                int y = spacing;

                int count = 0;

                pnImageProduct.Controls.Clear();

                int startIndex = (page - 1) * productsPerPage;
                int endIndex = Math.Min(startIndex + productsPerPage, images.Count);

                foreach (var image in images.GetRange(startIndex, endIndex - startIndex))
                {
                    string imageName = image.AnhSanPham;
                    string firebasePath = $"images/{imageName}";

                    SanPham product = sp.GetProduct(image.MaHinh, MaKho);
                    KhuyenMai promotion = km.GetKhuyenMaiForProduct(product.MaSP);

                    //if (product != null && product.SoLuong > 0)
                    if (product != null)
                    {
                        Button btn = new Button();
                        btn.Width = buttonWidth;
                        btn.Height = buttonHeight;
                        btn.Location = new Point(x, y);
                        btn.BackgroundImageLayout = ImageLayout.Stretch;

                        btn.BackgroundImage = Properties.Resources.placeholder_image; 

                        btn.Tag = image.MaHinh;
                        btn.Click += Btn_Click;
                        btn.MouseLeave += Btn_MouseLeave;

                        pnImageProduct.Controls.Add(btn);

                        Label lbl = new Label();
                        lbl.Name = "lbl_" + product.MaSP;
                        //lbl.Text = product.TenSP + "\n" + "Số lượng: " + product.SoLuong.ToString();
                        lbl.Text = product.TenSP + "\n" + (product.SoLuong > 0 ? "Số lượng: " + product.SoLuong.ToString() : "Hết hàng");
                        lbl.TextAlign = ContentAlignment.MiddleCenter;
                        lbl.Location = new Point(x, y + buttonHeight + 5);
                        lbl.Width = buttonWidth + 10;
                        lbl.Height = 80;

                        lbl.Font = new Font("Arial", 9, FontStyle.Bold);
                        lbl.Padding = new Padding(0, 0, 0, 0);

                        if (promotion != null)
                        {
                            string promotionText = "Khuyến mãi: ";
                            if (promotion.MaLKM == 1)
                            {
                                promotionText += promotion.GiaTriGiam + "%";
                            }
                            else if (promotion.MaLKM == 2)
                            {
                                promotionText += promotion.GiaTriGiam + " VND";
                            }
                            lbl.Text += "\n" + promotionText;
                        }

                        pnImageProduct.Controls.Add(lbl);

                        LoadImageAsync(btn, firebasePath);

                        x += buttonWidth + spacing;
                        count++;

                        if (count >= maxPerRow)
                        {
                            x = spacing;
                            y += buttonHeight + verticalSpacing;
                            count = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePageNumberByNhaCungCap(int maNCC)
        {
            int totalProducts = sp.GetImageProductByNhaCungCap(maNCC, MaKho).Count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / productsPerPage);
            lbTrang.Text = "Trang " + currentPage + " / " + totalPages;
        }

        private async void LoadSearchByLoaiSanPham(int maLSP, int page)
        {
            try
            {
                btnThemGH.Enabled = false;
                btnXoaGH.Enabled = false;

                txtMaSP.Enabled = false;
                txtTenSP.Enabled = false;
                txtDonGia.Enabled = false;
                txtNgaySX.Enabled = false;
                txtNCC.Enabled = false;
                txtLSP.Enabled = false;
                txtSLSP.Enabled = false;
                txtKM.Enabled = false;

                List<HinhAnhSanPham> images = sp.GetImageProductByLoaiSanPham(maLSP, MaKho);

                int buttonWidth = 145;
                int buttonHeight = 145;
                int spacing = 23;
                int maxPerRow = 6;
                int verticalSpacing = 100;

                int x = spacing;
                int y = spacing;

                int count = 0;

                pnImageProduct.Controls.Clear();

                int startIndex = (page - 1) * productsPerPage;
                int endIndex = Math.Min(startIndex + productsPerPage, images.Count);

                foreach (var image in images.GetRange(startIndex, endIndex - startIndex))
                {
                    string imageName = image.AnhSanPham;
                    string firebasePath = $"images/{imageName}";

                    SanPham product = sp.GetProduct(image.MaHinh, MaKho);
                    KhuyenMai promotion = km.GetKhuyenMaiForProduct(product.MaSP);

                    //if (product != null && product.SoLuong > 0)
                    if (product != null)
                    {
                        Button btn = new Button();
                        btn.Width = buttonWidth;
                        btn.Height = buttonHeight;
                        btn.Location = new Point(x, y);
                        btn.BackgroundImageLayout = ImageLayout.Stretch;

                        btn.BackgroundImage = Properties.Resources.placeholder_image; 

                        btn.Tag = image.MaHinh;
                        btn.Click += Btn_Click;
                        btn.MouseLeave += Btn_MouseLeave;

                        pnImageProduct.Controls.Add(btn);

                        Label lbl = new Label();
                        lbl.Name = "lbl_" + product.MaSP;
                        //lbl.Text = product.TenSP + "\n" + "Số lượng: " + product.SoLuong.ToString();
                        lbl.Text = product.TenSP + "\n" + (product.SoLuong > 0 ? "Số lượng: " + product.SoLuong.ToString() : "Hết hàng");
                        lbl.TextAlign = ContentAlignment.MiddleCenter;
                        lbl.Location = new Point(x, y + buttonHeight + 5);
                        lbl.Width = buttonWidth + 10;
                        lbl.Height = 80;

                        lbl.Font = new Font("Arial", 9, FontStyle.Bold);
                        lbl.Padding = new Padding(0, 0, 0, 0);

                        if (promotion != null)
                        {
                            string promotionText = "Khuyến mãi: ";
                            if (promotion.MaLKM == 1)
                            {
                                promotionText += promotion.GiaTriGiam + "%";
                            }
                            else if (promotion.MaLKM == 2)
                            {
                                promotionText += promotion.GiaTriGiam + " VND";
                            }
                            lbl.Text += "\n" + promotionText;
                        }

                        pnImageProduct.Controls.Add(lbl);

                        LoadImageAsync(btn, firebasePath);

                        x += buttonWidth + spacing;
                        count++;

                        if (count >= maxPerRow)
                        {
                            x = spacing;
                            y += buttonHeight + verticalSpacing;
                            count = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePageNumberByLoaiSanPham(int maLSP)
        {
            int totalProducts = sp.GetImageProductByLoaiSanPham(maLSP, MaKho).Count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / productsPerPage);
            lbTrang.Text = "Trang " + currentPage + " / " + totalPages;
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSDTGiaoHang.Text.Length == 10)
                {
                    var tenkh = kh.Search_KH_SDT(txtSDTGiaoHang.Text);
                    if (tenkh != null)
                    {
                        txtTenKH.Text = tenkh.TenKH;
                        txtDiaChiGiaoHang.Text = tenkh.DiaChi;
                    }
                    else
                    {
                        txtTenKH.Text = "Khách vãng lai";
                    }
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void lbTenNV_Click(object sender, EventArgs e)
        {

        }

        private void dgv_GioHang_Click(object sender, EventArgs e)
        {
            try
            {
                btnThemGH.Enabled = false;
                txtSLSP.Enabled = false;
                btnXoaGH.Enabled = true;
                int curow = dgv_GioHang.CurrentRow.Index;
                txtMaSP.Text = dgv_GioHang.Rows[curow].Cells[0].Value.ToString();
                txtTenSP.Text = dgv_GioHang.Rows[curow].Cells[1].Value.ToString();
                txtDonGia.Text = dgv_GioHang.Rows[curow].Cells[2].Value.ToString();
                txtNgaySX.Text = dgv_GioHang.Rows[curow].Cells[3].Value.ToString();
                txtNCC.Text = dgv_GioHang.Rows[curow].Cells[4].Value.ToString();
                txtLSP.Text = dgv_GioHang.Rows[curow].Cells[5].Value.ToString();
                txtSL.Text = dgv_GioHang.Rows[curow].Cells[6].Value.ToString();
                y = int.Parse(txtSL.Text);
                SanPham sanPham = sp.getSL(txtMaSP.Text);
                a = sanPham.SoLuong;
            }
            catch
            {
                MessageBox.Show("Trống ! ", "Thông báo", MessageBoxButtons.OK);
            }
        }
    }
}
