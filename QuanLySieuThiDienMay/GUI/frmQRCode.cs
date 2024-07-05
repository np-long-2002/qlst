using BLL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;

namespace GUI
{
    public partial class frmQRCode : Form
    {
        Basis ba = new Basis();
        BLL_SanPham sp = new BLL_SanPham();

        private string productName;
        private bool isQRCode = true;

        public frmQRCode()
        {
            InitializeComponent();
            Load += FrmQRCode_Load;
        }

        private void FrmQRCode_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            cboQRorBarcode.Items.Add("QR Code");
            cboQRorBarcode.Items.Add("Barcode");
            cboQRorBarcode.SelectedIndex = 0;
            cboQRorBarcode.SelectedIndexChanged += CboQRorBarcode_SelectedIndexChanged; ;
        }

        private void CboQRorBarcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            isQRCode = cboQRorBarcode.SelectedIndex == 0;
        }

        public void LoadSanPham()
		{
			try
			{
				dgvListSP.DataSource = sp.QR_SP();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void btnQR_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (isQRCode)
                {
                    //BarcodeDraw barcodeDraw = BarcodeDrawFactory.CodeQr;
                    //Image qrImage = barcodeDraw.Draw(txtQRBR.Text, 50);
                    //picQR.Image = qrImage;
                    QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
                    var qr1 = qr.CreateQrCode(txtQRBR.Text, QRCoder.QRCodeGenerator.ECCLevel.H);
                    var code = new QRCoder.QRCode(qr1);
                    //picQR.Image = code.GetGraphic(10, Color.DarkRed, Color.PaleGreen, true);
                    picQR.Image = code.GetGraphic(10, Color.Black, Color.LightGray, true);
                }
                else
                {
                    //BarcodeDraw barcodeDraw = BarcodeDrawFactory.Code128WithChecksum;
                    //Image barcodeImage = barcodeDraw.Draw(txtQRBR.Text, 50, 5);
                    //picQR.Image = barcodeImage;
                    Zen.Barcode.Code128BarcodeDraw br = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                    //picQR.Image = br.Draw(txtQRBR.Text, 60, 2);
                    Image barcodeImage = br.Draw(txtQRBR.Text, 60, 2);
                    //picQR.Image = ChangeBarcodeColor(barcodeImage, Color.DarkRed, Color.PaleGreen);
                    picQR.Image = ChangeBarcodeColor(barcodeImage, Color.Black, Color.LightGray);
                }

                using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
                {
                    if (folderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        string selectedPath = folderBrowser.SelectedPath;
                        string fileType = isQRCode ? "QR" : "Barcode";
                        string filePath = System.IO.Path.Combine(selectedPath, productName + "_" + fileType + ".png");
                        picQR.Image.Save(filePath, ImageFormat.Png);

                        MessageBox.Show(fileType + " đã được lưu tại: " + filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tạo mã và lưu hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Bitmap ChangeBarcodeColor(Image barcodeImage, Color foregroundColor, Color backgroundColor)
        {
            Bitmap bmp = new Bitmap(barcodeImage.Width, barcodeImage.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(backgroundColor);
                for (int y = 0; y < barcodeImage.Height; y++)
                {
                    for (int x = 0; x < barcodeImage.Width; x++)
                    {
                        Color pixelColor = ((Bitmap)barcodeImage).GetPixel(x, y);
                        if (pixelColor.R < 128 && pixelColor.G < 128 && pixelColor.B < 128)
                        {
                            bmp.SetPixel(x, y, foregroundColor);
                        }
                    }
                }
            }
            return bmp;
        }

        private void dgvListSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			try
			{
				if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
				{
					DataGridViewRow selectedRow = dgvListSP.Rows[e.RowIndex];
					object cellValue = selectedRow.Cells[0].Value;
					txtQRBR.Text = cellValue != null ? cellValue.ToString() : "";
					object productNameValue = selectedRow.Cells[1].Value;
					productName = productNameValue != null ? productNameValue.ToString() : "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Đã xảy ra lỗi khi xử lý sự kiện CellClick trên DataGridView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                ba.clearTextBoxs(this.Controls);
                picQR.Image = null;
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
