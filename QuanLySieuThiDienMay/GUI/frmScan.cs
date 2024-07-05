using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;

namespace GUI
{
    public partial class frmScan : Form
    {
        FilterInfoCollection filterInfo;
        VideoCaptureDevice videoCaptureDevice;

        private string barcodeResult;

        public string Barcode => barcodeResult;

        public event EventHandler<string> BarcodeDecoded;

        public frmScan()
        {
            InitializeComponent();
            Load += frmScan_Load;
            txtQRBR.Text = "";
        }

        private void frmScan_Load(object sender, EventArgs e)
        {
			try
			{
				filterInfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
				foreach (FilterInfo item in filterInfo)
				{
					cboCamera.Items.Add(item.Name);
				}

				if (filterInfo.Count > 0)
				{
					cboCamera.SelectedIndex = 0;
					videoCaptureDevice = new VideoCaptureDevice(filterInfo[cboCamera.SelectedIndex].MonikerString);
					videoCaptureDevice.NewFrame += new NewFrameEventHandler(VideoCaptureDevice_NewFrame);
					videoCaptureDevice.Start();

					timer1.Start(); 
				}
				else
				{
					MessageBox.Show("Không tìm thấy thiết bị camera nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi khởi động camera: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                //if (eventArgs.Frame != null)
                //{
                //    Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                //    pictureBox1.Image = bitmap;
                //}
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

                BarcodeReader reader = new BarcodeReader
                {
                    AutoRotate = true,
                    TryInverted = true,
                    Options = new ZXing.Common.DecodingOptions
                    {
                        PossibleFormats = new List<ZXing.BarcodeFormat>
                        {
                            ZXing.BarcodeFormat.QR_CODE,
                            ZXing.BarcodeFormat.CODE_128,
                            ZXing.BarcodeFormat.CODE_39,
                            ZXing.BarcodeFormat.EAN_13,
                            ZXing.BarcodeFormat.EAN_8,
                            ZXing.BarcodeFormat.UPC_A,
                            ZXing.BarcodeFormat.UPC_E,
                            ZXing.BarcodeFormat.ITF,
                            ZXing.BarcodeFormat.CODABAR,
                            ZXing.BarcodeFormat.PDF_417,
                            ZXing.BarcodeFormat.AZTEC,
                            ZXing.BarcodeFormat.DATA_MATRIX,
                            ZXing.BarcodeFormat.MSI,
                            ZXing.BarcodeFormat.MAXICODE,
                            ZXing.BarcodeFormat.RSS_14,
                            ZXing.BarcodeFormat.RSS_EXPANDED,
                            ZXing.BarcodeFormat.UPC_EAN_EXTENSION
                        }
                    }
                };

                var result = reader.Decode(bitmap);
                if (result != null)
                {
                    txtQRBR.Invoke(new MethodInvoker(delegate ()
                    {
                        txtQRBR.Text = "Sản phẩm mã số: " + result.Text;
                    }));
                }

                pictureBox1.Image = bitmap;
            }
            catch (Exception ex)
            {
				MessageBox.Show("Đã xảy ra lỗi khi cập nhật hình ảnh từ camera: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void frmScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning == true)
                {
                    videoCaptureDevice.Stop();
                }
            }       
        }

        private bool firstScanCompleted = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (pictureBox1.Image != null)
            //{
            //    try
            //    {
            //        BarcodeReader reader = new BarcodeReader();
            //        Result result = reader.Decode((Bitmap)pictureBox1.Image);
            //        if (result != null)
            //        {
            //            string decode = result.Text.Trim();
            //            if (!string.IsNullOrEmpty(decode))
            //            {
            //                label2.Text = "SP: " + decode;
            //                barcodeResult = decode;
            //                timer1.Stop();
            //                firstScanCompleted = true;
            //                DialogResult = DialogResult.OK;
            //                BarcodeDecoded?.Invoke(this, barcodeResult);
            //            }
            //        }
            //        else
            //        {
            //            videoCaptureDevice.NewFrame += new NewFrameEventHandler(VideoCaptureDevice_NewFrame);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Đã xảy ra lỗi khi quét mã vạch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

            if (pictureBox1.Image != null)
            {
                try
                {
                    BarcodeReader reader = new BarcodeReader
                    {
                        AutoRotate = true,
                        TryInverted = true,
                        Options = new ZXing.Common.DecodingOptions
                        {
                            PossibleFormats = new List<BarcodeFormat>
                            {
                                ZXing.BarcodeFormat.QR_CODE,
                                ZXing.BarcodeFormat.CODE_128,
                                ZXing.BarcodeFormat.CODE_39,
                                ZXing.BarcodeFormat.EAN_13,
                                ZXing.BarcodeFormat.EAN_8,
                                ZXing.BarcodeFormat.UPC_A,
                                ZXing.BarcodeFormat.UPC_E,
                                ZXing.BarcodeFormat.ITF,
                                ZXing.BarcodeFormat.CODABAR,
                                ZXing.BarcodeFormat.PDF_417,
                                ZXing.BarcodeFormat.AZTEC,
                                ZXing.BarcodeFormat.DATA_MATRIX,
                                ZXing.BarcodeFormat.MSI,
                                ZXing.BarcodeFormat.MAXICODE,
                                ZXing.BarcodeFormat.RSS_14,
                                ZXing.BarcodeFormat.RSS_EXPANDED,
                                ZXing.BarcodeFormat.UPC_EAN_EXTENSION
                            }
                        }
                    };

                    Result result = reader.Decode((Bitmap)pictureBox1.Image);
                    if (result != null)
                    {
                        string decode = result.Text.Trim();
                        if (!string.IsNullOrEmpty(decode))
                        {
                            txtQRBR.Text = "Sản phẩm mã số: " + decode;
                            barcodeResult = decode;
                            timer1.Stop();
                            firstScanCompleted = true;
                            DialogResult = DialogResult.OK;
                            BarcodeDecoded?.Invoke(this, barcodeResult);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không nhận diện được mã vạch.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi quét mã vạch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //this.Close();
                this.Hide();
            }
        }

        private void cboCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedCamera = cboCamera.SelectedItem.ToString();

                foreach (FilterInfo item in filterInfo)
                {
                    if (item.Name == selectedCamera)
                    {
                        videoCaptureDevice = new VideoCaptureDevice(item.MonikerString);
                        videoCaptureDevice.NewFrame += new NewFrameEventHandler(VideoCaptureDevice_NewFrame);
                        videoCaptureDevice.Start();
                    }
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show("Đã xảy ra lỗi khi chuyển đổi thiết bị camera: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
			try
			{
				txtQRBR.Text = ""; 
				barcodeResult = null; 

				timer1.Start(); 
				firstScanCompleted = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi đặt lại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
    }
}
