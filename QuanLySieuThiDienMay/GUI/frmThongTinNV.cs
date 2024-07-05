using BLL;
using DAL;
using Emgu.CV;
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
    public partial class frmThongTinNV : Form
    {
        BLL_ChucVu cv = new BLL_ChucVu();
        BLL_NhanVien nv = new BLL_NhanVien();

        //string folderPath = @"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\NhanVien";

        public frmThongTinNV(NhanVien nv)
        {
            InitializeComponent();
            txtBoxMaNV.Text = Convert.ToString(nv.MaNV);
            txtBoxTenNV.Text = nv.TenNV;
            txtBoxGT.Text = nv.GioiTinh;
            txtBoxDiaChi.Text = nv.DiaChi;
            txtBoxSDT.Text = nv.SDT;
            txtBoxEmail.Text = nv.Email;
            txtBoxCCCD.Text = nv.CCCD;
            txtBoxMK.Text = nv.MatKhau;
            LoadComBoxChucVu(Convert.ToString(nv.MaCV));
            if (nv.MaCV != null)
            {
                cbxChucVu.SelectedValue = nv.MaCV;
            }
            LoadAnhNhanVien(nv.AnhNV);
        }
        public void LoadComBoxChucVu(string maCV)
        {
            try
            {
                cbxChucVu.DataSource = cv.Get_ChucVu();
                cbxChucVu.DisplayMember = "MoTa";
                cbxChucVu.ValueMember = "MaCV";
                cbxChucVu.SelectedValue = maCV;
            }
            catch (Exception ex)
            {
				MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu chức vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private async void LoadAnhNhanVien(string anhNV)
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(anhNV))
            //    {
            //        string url = @"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\NhanVien";
            //        //string url = @"D:\QuanLySieuThiDienMay\Images\SanPham";
            //        string file = Path.Combine(url, anhNV);

            //        if (File.Exists(file))
            //        {
            //            imgAvtNV.Image = Image.FromFile(file);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Không tìm thấy ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            try
            {
                if (!string.IsNullOrEmpty(anhNV))
                {
                    imgAvtNV.Image = Properties.Resources.placeholder_image_human; // Sử dụng ảnh tạm thời trong tài nguyên của bạn

                    await LoadImageAsync(imgAvtNV, anhNV);
                }
                else
                {
                    MessageBox.Show("Ảnh nhân viên không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadImageAsync(Guna2ImageButton pictureBox, string imageName)
        {
            try
            {
                string firebasePath = $"staff/{imageName}";
                var storage = StorageClient.Create();
                using (var imageStream = new MemoryStream())
                {
                    await storage.DownloadObjectAsync("sieuthidienmay-6e8af.appspot.com", firebasePath, imageStream);
                    imageStream.Position = 0;
                    pictureBox.Image = Image.FromStream(imageStream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải ảnh {imageName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string maCV = cbxChucVu.SelectedValue.ToString();
                nv.Update_NV(int.Parse(txtBoxMaNV.Text), txtBoxTenNV.Text, txtBoxGT.Text, txtBoxDiaChi.Text, txtBoxSDT.Text, txtBoxEmail.Text, txtBoxCCCD.Text, txtBoxMK.Text, int.Parse(maCV));
                MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (nv.DeleteNV(int.Parse(txtBoxMaNV.Text)) == true)
                    {
                        MessageBox.Show("Xóa thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);                       
                        frmNhanVien fr = new frmNhanVien();
                        fr.LoadDataGridView();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thông tin nhân viên không thành công vì còn lịch làm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa thông tin nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdateImage_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    OpenFileDialog openFileDialog = new OpenFileDialog();
            //    openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";
            //    openFileDialog.Title = "Chọn ảnh mới";
            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string imagePath = openFileDialog.FileName;
            //        imgAvtNV.Image = Image.FromFile(imagePath);
            //        string imageName = Path.GetFileName(imagePath);
            //        nv.Update_ImageNV(imageName, int.Parse(txtBoxMaNV.Text));

            //        //Luu(imageName);
            //        //string folderPath = @"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\NhanVien";

            //        if (!Directory.Exists(folderPath))
            //        {
            //            Directory.CreateDirectory(folderPath);
            //        }
            //        else
            //        {
            //            string image = Path.Combine(folderPath, imageName);
            //            imgAvtNV.Image.Save(image, ImageFormat.Jpeg);
            //        }    

            //        MessageBox.Show("Đã cập nhật ảnh nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Đã xảy ra lỗi khi cập nhật ảnh nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";
                openFileDialog.Title = "Chọn ảnh mới";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string tenNV = txtBoxTenNV.Text;
                    string imagePath = openFileDialog.FileName;
                    //imgAvtNV.Image = Image.FromFile(imagePath);
                    string imageName = $"{tenNV}_{DateTime.Now.Ticks}.jpg";
                    int maNV = int.Parse(txtBoxMaNV.Text);

                    // Update image in database, local folder, and Firebase Storage
                    nv.Update_ImageNV(imageName, maNV);

                    // Save image to local folder
                    //if (!Directory.Exists(folderPath))
                    //{
                    //    Directory.CreateDirectory(folderPath);
                    //}
                    //string localImagePath = Path.Combine(folderPath, imageName);
                    //imgAvtNV.Image.Save(localImagePath, ImageFormat.Jpeg);

                    // Upload image to Firebase Storage
                    var storage = StorageClient.Create();
                    string remoteImagePath = $"staff/{imageName}";
                    using (var stream = new FileStream(imagePath, FileMode.Open))
                    {
                        await storage.UploadObjectAsync("sieuthidienmay-6e8af.appspot.com", remoteImagePath, null, stream);
                    }

                    MessageBox.Show("Đã cập nhật ảnh nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadImageAsync(imgAvtNV, imageName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật ảnh nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private async Task DeleteOldImageFromFirebase(int maNV, string oldImageName)
        //{
        //    try
        //    {
        //        var storage = StorageClient.Create();
        //        string remoteImagePath = $"staff/{oldImageName}";

        //        if (!string.IsNullOrEmpty(oldImageName))
        //        {
        //            await storage.DeleteObjectAsync("sieuthidienmay-6e8af.appspot.com", remoteImagePath);

        //            // Delete old image from local folder
        //            string oldLocalPath = Path.Combine(folderPath, oldImageName);
        //            if (File.Exists(oldLocalPath))
        //            {
        //                File.Delete(oldLocalPath);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Không tìm thấy tên ảnh cũ để xoá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Đã xảy ra lỗi khi xoá ảnh cũ trên Firebase Storage: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Luu(string imagename)
        //{
        //    string filname = @"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\NhanVien\" + imagename;
        //    var bitmap = new Bitmap(imgAvtNV.Width, imgAvtNV.Height);
        //    imgAvtNV.DrawToBitmap(bitmap, imgAvtNV.ClientRectangle);
        //    System.Drawing.Imaging.ImageFormat imageFormat = null;
        //    imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
        //    bitmap.Save(filname, imageFormat);
        //    MessageBox.Show("Lưu ảnh thành công !");
        //}

        private void btnCaptureImage_Click(object sender, EventArgs e)
        {
			try
			{
				frmSavePicture frm = new frmSavePicture();
				frm.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }
    }
}
