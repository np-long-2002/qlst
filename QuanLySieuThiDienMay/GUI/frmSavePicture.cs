using AForge.Video;
using AForge.Video.DirectShow;
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
    public partial class frmSavePicture : Form
    {
        VideoCaptureDevice videocapture;
        FilterInfoCollection filterinfo;

        public frmSavePicture()
        {
            InitializeComponent();
        }

        private void StarCamera()
        {
            try
            {
                filterinfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                videocapture = new VideoCaptureDevice(filterinfo[0].MonikerString);
                videocapture.NewFrame += new NewFrameEventHandler(Camera_On);
                videocapture.Start();
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Camera_On(object sender, NewFrameEventArgs eventArgs)
        {
            pic_stream.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void btn_Stream_Click(object sender, EventArgs e)
        {
			try
			{
				StarCamera();
				btn_Stream.Text = "Streaming";
				MessageBox.Show("Bắt đầu mở camera!");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi bắt đầu stream camera: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btn_Capture_Click(object sender, EventArgs e)
        {
            try
            {
                pic_Image.Image = pic_stream.Image;
                MessageBox.Show("Chụp ảnh thành công !");
            }
            catch (Exception ex)
            {
				MessageBox.Show("Đã xảy ra lỗi khi chụp ảnh từ camera: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "JPEG Files (*.jpg)|*.jpg|All files (*.*)|*.*";
                saveDialog.DefaultExt = "jpg"; 
                saveDialog.AddExtension = true;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveDialog.FileName;

                    var bitmap = new Bitmap(pic_Image.Width, pic_Image.Height);
                    pic_Image.DrawToBitmap(bitmap, pic_Image.ClientRectangle);
                    System.Drawing.Imaging.ImageFormat imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;

                    bitmap.Save(fileName, imageFormat);
                    MessageBox.Show("Lưu ảnh thành công !");
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show("Đã xảy ra lỗi khi lưu ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
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
