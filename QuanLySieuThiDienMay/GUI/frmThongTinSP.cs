using BLL;
using DAL;
using Emgu.CV;
using Emgu.CV.Structure;
using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmThongTinSP : Form
    {
        Basis ba = new Basis();
        BLL_NhaCungCap ncc = new BLL_NhaCungCap();
        BLL_LoaiSanPham loai = new BLL_LoaiSanPham();
        BLL_SanPham sp = new BLL_SanPham();
        BLL_HinhAnhSanPham ha = new BLL_HinhAnhSanPham();

        private int maSP;
        //string folderPath = @"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\SanPham";
        //string folderPath = @"D:\QuanLySieuThiDienMay\Images\SanPham";
        bool isProductUpdated = false;
        bool areImagesUpdated = false;

        private List<string> loadedImagePaths = new List<string>();

        public frmThongTinSP(int maSP)
        {
            InitializeComponent();
            LoadComboBox();
            Load += frmThongTinSP_Load;
            this.maSP = maSP;
        }

        private void tsBtn_Close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

		public void LoadComboBox()
		{
			try
			{
				cboLSP.DisplayMember = "MoTa";
				cboLSP.ValueMember = "MaLoaiSP";
				cboLSP.DataSource = loai.getLoaiSanPham();

				cboNCC.DisplayMember = "TenNCC";
				cboNCC.ValueMember = "MaNCC";
				cboNCC.DataSource = ncc.getNhaCungCap();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void ShowProductDetails(ListFrmTTSP productInfo)
        {         
            flowLayoutPanel1.Controls.Clear(); 
            loadedImagePaths.Clear();

            try
            {
                if (productInfo != null)
                {
                    txtTenSP.Text = productInfo.TenSP;
                    txtMoTa.Text = productInfo.Mota;
                    txtGiaSP.Text = productInfo.GiaSP.ToString("N2") + "VNĐ";
                    dtaNSX.Value = productInfo.NgaySX;
                    string tenLoaiSP = loai.GetTenLoaiSanPham(productInfo.MaLoaiSP);
                    string tenNCC = ncc.GetTenNhaCungCap(productInfo.MaNCC);
                    cboLSP.Text = tenLoaiSP;
                    cboNCC.Text = tenNCC;

                    for (int i = 0; i < productInfo.Images.Count; i++)
                    {
                        string imageName = productInfo.Images[i];

                        if (!loadedImagePaths.Contains(imageName))
                        {
                            loadedImagePaths.Add(imageName);

                            PictureBox pictureBox = new PictureBox();
                            pictureBox.Image = Properties.Resources.placeholder_image;
                            pictureBox.BorderStyle = BorderStyle.FixedSingle;
                            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox.Width = 100;
                            pictureBox.Height = 180;

                            Button button = new Button();
                            button.Text = "Cập nhật ảnh";
                            button.Tag = imageName;
                            button.Width = 100;
                            button.Height = 40;
                            button.BackColor = Color.Cyan;
                            button.ForeColor = Color.Black;
                            button.Click += UpdateImageButton_Click;

                            FlowLayoutPanel imagePanel = new FlowLayoutPanel();
                            imagePanel.FlowDirection = FlowDirection.TopDown;
                            imagePanel.AutoSize = true;
                            imagePanel.Controls.Add(pictureBox);
                            imagePanel.Controls.Add(button);

                            flowLayoutPanel1.Controls.Add(imagePanel);

                            await LoadImageAsync(pictureBox, imageName);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin của sản phẩm !");
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }       

        private async Task LoadImageAsync(PictureBox pictureBox, string imageName)
        {
            try
            {
                string firebasePath = $"images/{imageName}";
                var storage = StorageClient.Create();

                using (var imageStream = new MemoryStream())
                {
                    await storage.DownloadObjectAsync("sieuthidienmay-6e8af.appspot.com", firebasePath, imageStream);

                    imageStream.Position = 0;

                    MemoryStream memoryStreamCopy = new MemoryStream();
                    await imageStream.CopyToAsync(memoryStreamCopy);
                    memoryStreamCopy.Position = 0;

                    pictureBox.Image = Image.FromStream(memoryStreamCopy);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải ảnh {imageName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     

        private async void UpdateImageButton_Click(object sender, EventArgs e)
        {           
            {
                Button button = (Button)sender;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Tệp hình ảnh (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string newImagePath = openFileDialog.FileName;

                        string tenSP = txtTenSP.Text;
                        string tenHinhAnh = $"{tenSP}_{DateTime.Now.Ticks}.jpg";
                        var storage = StorageClient.Create();

                        using (var fileStream = new FileStream(newImagePath, FileMode.Open))
                        {
                            await storage.UploadObjectAsync("sieuthidienmay-6e8af.appspot.com", $"images/{tenHinhAnh}", null, fileStream);
                        }

                        button.Tag = tenHinhAnh;

                        FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)button.Parent;
                        PictureBox pictureBox = flowLayoutPanel.Controls.OfType<PictureBox>().FirstOrDefault();
                        if (pictureBox != null)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await storage.DownloadObjectAsync("sieuthidienmay-6e8af.appspot.com", $"images/{tenHinhAnh}", stream);
                                stream.Position = 0;
                                pictureBox.Image = Image.FromStream(stream);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi tải hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private List<string> GetNewImageNamesFromControls()
        {
            List<string> newImageNames = new List<string>();
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is FlowLayoutPanel imagePanel)
                {
                    foreach (Control childControl in imagePanel.Controls)
                    {
                        if (childControl is Button button)
                        {
                            string imageName = Path.GetFileName(button.Tag.ToString());
                            newImageNames.Add(imageName);
                        }
                    }
                }
            }
            return newImageNames;
        }

        private void frmThongTinSP_Load(object sender, EventArgs e)
        {
            var productInfo = sp.GetProductDetails(maSP);
            ShowProductDetails(productInfo);
        }

        private void tsBtn_Xoa_Click(object sender, EventArgs e)
        {
            
        }

        private async void tsBtn_CapNhat_Click(object sender, EventArgs e)
        {            
            try
            {
                string tenSP = txtTenSP.Text;
                string moTa = txtMoTa.Text;
                int giaSP = int.Parse(txtGiaSP.Text.Replace("VNĐ", "").Replace(",", ""));
                DateTime ngaySX = dtaNSX.Value;
                int maLoaiSP = (int)cboLSP.SelectedValue;
                int maNCC = (int)cboNCC.SelectedValue;
                List<string> newImageNames = GetNewImageNamesFromControls();

                sp.UpdateProduct(maSP, tenSP, moTa, giaSP, ngaySX, maLoaiSP, maNCC);
                isProductUpdated = true;

                await UpdateProductImagesAsync(maSP, newImageNames);
                areImagesUpdated = true;

                MessageBox.Show("Sản phẩm và hình ảnh sản phẩm đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (!isProductUpdated && !areImagesUpdated)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi cập nhật sản phẩm và hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!isProductUpdated)
                {
                    MessageBox.Show("Cập nhật thông tin sản phẩm không thành công! Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!areImagesUpdated)
                {
                    MessageBox.Show("Cập nhật hình ảnh sản phẩm không thành công! Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }       

        private async Task UpdateProductImagesAsync(int maSP, List<string> newImageNames)
        {
            try
            {
                List<string> oldImageNames = ha.GetProductImageNames(maSP);

                var storage = StorageClient.Create();

                var imagesToDelete = oldImageNames.Except(newImageNames).ToList();
                var imagesToAdd = newImageNames.Except(oldImageNames).ToList();

                foreach (var imageName in imagesToDelete)
                {
                    string imagePath = $"images/{imageName}";
                    try
                    {
                        await storage.DeleteObjectAsync("sieuthidienmay-6e8af.appspot.com", imagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể xóa ảnh {imageName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                ha.UpdateProductImages(maSP, newImageNames);

                foreach (var imageName in imagesToAdd)
                {
                    PictureBox pictureBox = GetPictureBoxFromImageName(imageName);
                    if (pictureBox != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            pictureBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            stream.Position = 0;

                            try
                            {
                                await storage.UploadObjectAsync("sieuthidienmay-6e8af.appspot.com", $"images/{imageName}", null, stream);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Không thể tải ảnh {imageName} lên Firebase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private PictureBox GetPictureBoxFromImageName(string imageName)
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is FlowLayoutPanel imagePanel)
                {
                    foreach (Control childControl in imagePanel.Controls)
                    {
                        if (childControl is Button button && button.Tag.ToString() == imageName)
                        {
                            return imagePanel.Controls.OfType<PictureBox>().FirstOrDefault();
                        }
                    }
                }
            }
            return null;
        }

        private async void btnCapNhat_Click(object sender, EventArgs e)
        {            
            try
            {
                string tenSP = txtTenSP.Text;
                string moTa = txtMoTa.Text;
                int giaSP = int.Parse(txtGiaSP.Text.Replace("VNĐ", "").Replace(",", ""));
                DateTime ngaySX = dtaNSX.Value;
                int maLoaiSP = (int)cboLSP.SelectedValue;
                int maNCC = (int)cboNCC.SelectedValue;
                List<string> newImageNames = GetNewImageNamesFromControls();

                sp.UpdateProduct(maSP, tenSP, moTa, giaSP, ngaySX, maLoaiSP, maNCC);
                isProductUpdated = true;

                await UpdateProductImagesAsync(maSP, newImageNames);
                areImagesUpdated = true;

                MessageBox.Show("Sản phẩm và hình ảnh sản phẩm đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (!isProductUpdated && !areImagesUpdated)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi cập nhật sản phẩm và hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!isProductUpdated)
                {
                    MessageBox.Show("Cập nhật thông tin sản phẩm không thành công! Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!areImagesUpdated)
                {
                    MessageBox.Show("Cập nhật hình ảnh sản phẩm không thành công! Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmResult = MessageBox.Show("Bạn chắc muốn xóa sản phẩm này vào thùng rác chứ?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    sp.DeleteProduct(maSP);
                    MessageBox.Show("Thông tin sản phẩm đã được chuyển vào thùng rác", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmSanPham fsp = new frmSanPham();
                    fsp.LoadSanPham();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã bị lỗi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
