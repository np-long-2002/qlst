using BLL;
using FirebaseAdmin.Auth.Multitenancy;
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
    public partial class frmThemNV : Form
    {

        Basis ba = new Basis();
        BLL_NhanVien nv = new BLL_NhanVien();
        BLL_ChucVu cv = new BLL_ChucVu();

        //string folderPath = @"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\NhanVien";

        private string imageName { get; set; }
        public string imagePath { get; set; }

        public frmThemNV()
        {
            InitializeComponent();
            LoadComBoxChucVu();
            txtMaNV.Enabled = false;
        }

        public void LoadComBoxChucVu()
        {
            try
            {
                cbxChucVu.DataSource = cv.Get_ChucVu();
                cbxChucVu.DisplayMember = "MoTa";
                cbxChucVu.ValueMember = "MaCV";
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi tải chức vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void tsBtn_Reset_Click(object sender, EventArgs e)
        {
            
        }

        private void frmThemNV_Load(object sender, EventArgs e)
        {

        }

        private void btn_EditChucVu_Click(object sender, EventArgs e)
        {
            try
            {
                frmChucVu frm = new frmChucVu();
                frm.ShowDialog();
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

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string maCV = cbxChucVu.SelectedValue.ToString();
                string gtinh = rdoNam.Checked ? "Nam" : rdoNu.Checked ? "Nữ" : "";
                string tenNV = txtTenNV.Text;
                string dchi = txtDiaChi.Text;
                string sdt = txtSDT.Text;
                string email = txtEmail.Text;
                string cccd = txtCCCD.Text;
                string mk = txtMK.Text;
                int maChucVu = int.Parse(maCV);

                nv.Add_NV(tenNV, gtinh, dchi, sdt, email, cccd, imageName, mk, maChucVu);

                //// Lưu ảnh vào thư mục cục bộ
                //if (!string.IsNullOrEmpty(imageName))
                //{
                //string folderPath = @"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\NhanVien";
                //if (!Directory.Exists(imagePath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}
                string tenHinhAnh = $"{tenNV}.jpg";
                //string imagePath = Path.Combine(folderPath, tenHinhAnh);
                //imgAvtNV.Image.Save(imagePath, ImageFormat.Jpeg);

                // Khởi tạo Firebase Storage client
                var storage = StorageClient.Create();

                // Tải ảnh lên Firebase Storage
                try
                {
                    string firebasePath = $"staff/{tenHinhAnh}";

                    using (var fileStream = File.OpenRead(imagePath))
                    {
                        await storage.UploadObjectAsync("sieuthidienmay-6e8af.appspot.com", firebasePath, null, fileStream);
                    }
                    MessageBox.Show("Đã thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể tải ảnh {tenHinhAnh} lên Firebase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //}


            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateImage_Click(object sender, EventArgs e)
        {
            string tenNV = txtTenNV.Text;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";
                openFileDialog.Title = "Chọn ảnh mới";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string tenHinhAnh = $"{tenNV}.jpg";
                    imagePath = openFileDialog.FileName;
                    imgAvtNV.Image = Image.FromFile(imagePath);
                    //imageName = Path.GetFileName(imagePath);
                    imageName = tenHinhAnh;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Đã xảy ra lỗi khi chọn ảnh nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtn_Camera_Click(object sender, EventArgs e)
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

        private void btnChupAnh_Click(object sender, EventArgs e)
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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                ba.clearTextBoxs(this.Controls);
                rdoNam.Checked = true;
                LoadComBoxChucVu();
                imgAvtNV.Image = null;
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
