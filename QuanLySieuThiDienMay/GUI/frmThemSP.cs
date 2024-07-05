using BLL;
using DAL;
using Google.Cloud.Storage.V1;
using Guna.UI2.WinForms;
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
    public partial class frmThemSP : Form
    {
        Basis ba = new Basis();

        BLL_NhaCungCap ncc = new BLL_NhaCungCap();
        BLL_LoaiSanPham loai = new BLL_LoaiSanPham();
        BLL_SanPham sp = new BLL_SanPham();
        BLL_HinhAnhSanPham ha = new BLL_HinhAnhSanPham();

        public int MaKho { get; set; }
        string imagepath;

        // folderPath = @"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\SanPham";

        public frmThemSP()
        {
            InitializeComponent();
            Load += frmThemSP_Load;
        }

        private void tsBtn_Close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void tsBtn_Reset_Click(object sender, EventArgs e)
        {
            
        }

        private void frmThemSP_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }

        public void LoadComboBox()
        {
            try
            {
                cboLoaiSP.DisplayMember = "MoTa";
                cboLoaiSP.ValueMember = "MaLoaiSP";
                cboLoaiSP.DataSource = loai.getLoaiSanPham();

                cboNCC.DisplayMember = "TenNCC";
                cboNCC.ValueMember = "MaNCC";
                cboNCC.DataSource = ncc.getNhaCungCap();
            }
            catch (Exception ex)
            {
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string tenSP = txtTenSP.Text;
            //    string moTa = txtMoTa.Text;
            //    DateTime ngaySX = dtaNSX.Value;
            //    int maLoaiSP = (int)cboLoaiSP.SelectedValue;
            //    int maNCC = (int)cboNCC.SelectedValue;
            //    SanPham newProduct = sp.AddProduct(tenSP, moTa, ngaySX, maLoaiSP, maNCC);
            //    int maSP = newProduct.MaSP;
            //    //ChiTietKho newwarehouse = sp.AddWarehouse(MaKho, maSP);

            //    if (!Directory.Exists(folderPath))
            //    {
            //        Directory.CreateDirectory(folderPath);
            //    }

            //    for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            //    {
            //        TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)flowLayoutPanel1.Controls[i];
            //        PictureBox pictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(0, 0);

            //        if (pictureBox.Image != null)
            //        {
            //            string tenHinhAnh = $"{tenSP}_{i}.jpg";
            //            string imagePath = Path.Combine(folderPath, tenHinhAnh);
            //            pictureBox.Image.Save(imagePath, ImageFormat.Jpeg);
            //            ha.AddProductImage(maSP, tenHinhAnh);
            //        }
            //    }

            //    MessageBox.Show("Thêm sản phẩm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Đã xảy ra lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            //try
            //{
            //    string tenSP = txtTenSP.Text;
            //    string moTa = txtMoTa.Text;
            //    DateTime ngaySX = dtaNSX.Value;
            //    int maLoaiSP = (int)cboLoaiSP.SelectedValue;
            //    int maNCC = (int)cboNCC.SelectedValue;
            //    SanPham newProduct = sp.AddProduct(tenSP, moTa, ngaySX, maLoaiSP, maNCC);
            //    int maSP = newProduct.MaSP;

            //    //string folderPath = @"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\SanPham";
            //    //if (!Directory.Exists(folderPath))
            //    //{
            //    //    Directory.CreateDirectory(folderPath);
            //    //}

            //    // Khởi tạo Firebase Storage client
            //    var storage = StorageClient.Create();

            //    for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            //    {
            //        TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)flowLayoutPanel1.Controls[i];
            //        PictureBox pictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(0, 0);

            //        if (pictureBox.Image != null)
            //        {
            //            string tenHinhAnh = $"{tenSP}_{i}.jpg";
            //            //string imagePath = Path.Combine(folderPath, tenHinhAnh);
            //            //pictureBox.Image.Save(imagePath, ImageFormat.Jpeg);

            //            // Lưu ảnh vào Firebase Storage
            //            try
            //            {
            //                string firebasePath = $"images/{tenHinhAnh}";

            //                using (var fileStream = File.OpenRead(imagepath))
            //                {
            //                    await storage.UploadObjectAsync("sieuthidienmay-6e8af.appspot.com", firebasePath, null, fileStream);
            //                }

            //                ha.AddProductImage(maSP, tenHinhAnh);
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show($"Không thể tải ảnh {tenHinhAnh} lên Firebase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //    }

            //    MessageBox.Show("Thêm sản phẩm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Đã xảy ra lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            try
            {
                string tenSP = txtTenSP.Text;
                string moTa = txtMoTa.Text;
                DateTime ngaySX = dtaNSX.Value;
                int maLoaiSP = (int)cboLoaiSP.SelectedValue;
                int maNCC = (int)cboNCC.SelectedValue;
                SanPham newProduct = sp.AddProduct(tenSP, moTa, ngaySX, maLoaiSP, maNCC);
                int maSP = newProduct.MaSP;

                var storage = StorageClient.Create();

                for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                {
                    TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)flowLayoutPanel1.Controls[i];
                    PictureBox pictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(0, 0);

                    if (pictureBox.Image != null && pictureBox.Tag is string imagePath)
                    {
                        string tenHinhAnh = $"{tenSP}_{i}.jpg";
                        string firebasePath = $"images/{tenHinhAnh}";

                        // Upload ảnh lên Firebase Storage
                        try
                        {
                            using (var fileStream = File.OpenRead(imagePath))
                            {
                                await storage.UploadObjectAsync("sieuthidienmay-6e8af.appspot.com", firebasePath, null, fileStream);
                            }

                            // Thêm đường dẫn của ảnh vào sản phẩm
                            ha.AddProductImage(maSP, tenHinhAnh);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Không thể tải ảnh {tenHinhAnh} lên Firebase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                MessageBox.Show("Thêm sản phẩm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSLA_TextChanged(object sender, EventArgs e)
        {
            //flowLayoutPanel1.Controls.Clear();
            //int soLuongMax = 8;
            //int soLuong;
            //if (int.TryParse(txtSLA.Text, out soLuong) && soLuong > 0)
            //{
            //    if (soLuong > soLuongMax)
            //    {
            //        MessageBox.Show("Số lượng ảnh tối đa là 8 bức ảnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        soLuong = soLuongMax;
            //        txtSLA.Text = "8";
            //    }

            //    for (int i = 0; i < soLuong; i++)
            //    {
            //        TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            //        tableLayoutPanel.Width = 100;
            //        tableLayoutPanel.Height = 300;
            //        tableLayoutPanel.RowCount = 2;
            //        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            //        tableLayoutPanel.AutoSize = false;

            //        PictureBox pictureBox = new PictureBox();
            //        Button button = new Button();

            //        pictureBox.Width = 100;
            //        pictureBox.Height = 180;
            //        pictureBox.BorderStyle = BorderStyle.FixedSingle;
            //        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //        button.Text = "Chọn ảnh";
            //        button.Width = 100;
            //        button.Height = 40;
            //        button.BackColor = Color.Cyan;
            //        button.ForeColor = Color.Black;
            //        button.Click += Button_Click;

            //        tableLayoutPanel.Controls.Add(pictureBox, 0, 0);

            //        tableLayoutPanel.Controls.Add(button, 0, 1);
            //        flowLayoutPanel1.Controls.Add(tableLayoutPanel);
            //    }
            //}

            flowLayoutPanel1.Controls.Clear();
            int soLuongMax = 8;
            int soLuong;
            if (int.TryParse(txtSLA.Text, out soLuong) && soLuong > 0)
            {
                if (soLuong > soLuongMax)
                {
                    MessageBox.Show("Số lượng ảnh tối đa là 8 bức ảnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    soLuong = soLuongMax;
                    txtSLA.Text = "8";
                }

                for (int i = 0; i < soLuong; i++)
                {
                    TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                    tableLayoutPanel.Width = 100;
                    tableLayoutPanel.Height = 300;
                    tableLayoutPanel.RowCount = 2;
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                    tableLayoutPanel.AutoSize = false;

                    PictureBox pictureBox = new PictureBox();
                    Button button = new Button();

                    pictureBox.Width = 100;
                    pictureBox.Height = 180;
                    pictureBox.BorderStyle = BorderStyle.FixedSingle;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                    button.Text = "Chọn ảnh";
                    button.Width = 100;
                    button.Height = 40;
                    button.BackColor = Color.Cyan;
                    button.ForeColor = Color.Black;
                    button.Click += Button_Click;

                    tableLayoutPanel.Controls.Add(pictureBox, 0, 0);

                    tableLayoutPanel.Controls.Add(button, 0, 1);
                    flowLayoutPanel1.Controls.Add(tableLayoutPanel);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            //Button button = (Button)sender;

            //TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)button.Parent;

            //PictureBox pictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(0, 0);

            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        pictureBox.Image = Image.FromFile(openFileDialog.FileName);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Đã xảy ra lỗi khi tải hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

            //Button button = (Button)sender;

            //TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)button.Parent;

            //PictureBox pictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(0, 0);

            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            //// Mở hộp thoại để chọn tệp hình ảnh
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        // Tải hình ảnh từ tệp được chọn và hiển thị trong PictureBox
            //        pictureBox.Image = Image.FromFile(openFileDialog.FileName);

            //        // Lưu đường dẫn tệp hình ảnh vào biến imagepath
            //        imagepath = openFileDialog.FileName;
            //    }
            //    catch (Exception ex)
            //    {
            //        // Hiển thị thông báo lỗi nếu có lỗi xảy ra khi tải hình ảnh
            //        MessageBox.Show("Đã xảy ra lỗi khi tải hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

            Button button = (Button)sender;
            TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)button.Parent;
            PictureBox pictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(0, 0);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBox.Tag = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi tải hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                ba.clearTextBoxs(this.Controls);
                LoadComboBox();
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
