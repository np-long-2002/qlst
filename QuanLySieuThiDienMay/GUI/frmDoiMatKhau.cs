using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;


namespace GUI
{
    public partial class frmDoiMatKhau : Form
    {
        BLL_NhanVien nv = new BLL_NhanVien();

        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private static string[] Scopes = { GmailService.Scope.GmailSend };
        private static string ApplicationName = "QuanLySieuThiDienMay";

        public UserCredential GetCredentials()
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".credentials/gmail-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            return credential;
        }

        //public void SendEmail(string subject, string body)
        //{
        //    UserCredential credential = GetCredentials();

        //    if (credential != null)
        //    {
        //        var service = new GmailService(new BaseClientService.Initializer()
        //        {
        //            HttpClientInitializer = credential,
        //            ApplicationName = ApplicationName,
        //        });

        //        var msg = new AE.Net.Mail.MailMessage
        //        {
        //            Subject = subject,
        //            Body = body,
        //            From = new MailAddress("biphatnguyen0302@gmail.com")
        //        };
        //        msg.To.Add(new MailAddress(txtTaiKhoan.Text));

        //        var mimeMessage = MimeKit.MimeMessage.CreateFromMailMessage(msg);
        //        var gmailMessage = new Google.Apis.Gmail.v1.Data.Message
        //        {
        //            Raw = ToBase64UrlSafeString(mimeMessage.ToString())
        //        };

        //        try
        //        {
        //            service.Users.Messages.Send(gmailMessage, "me").Execute();
        //            MessageBox.Show("Email đã được gửi thành công.");
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Có lỗi xảy ra khi gửi email: " + ex.Message);
        //        }
        //    }
        //}

        public string ToBase64UrlSafeString(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            string base64String = Convert.ToBase64String(inputBytes);
            return base64String.Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //string email = txtTaiKhoan.Text;

            //string token = nv.GeneratePasswordResetToken(email);

            //if (token != null)
            //{
            //    string subject = "Đặt lại mật khẩu";
            //    string body = "Xin chào,\n\nBạn đã yêu cầu đặt lại mật khẩu. Vui lòng sử dụng mã sau để đặt lại mật khẩu của bạn:\n\n" + token;

            //    SendEmail(subject, body);
            //    MessageBox.Show("Email chứa liên kết để đặt lại mật khẩu đã được gửi.");
            //}
            //else
            //{
            //    MessageBox.Show("Email không tồn tại trong hệ thống.");
            //}

            try
            {
                string email = txtTaiKhoan.Text;

                string token = nv.GeneratePasswordResetToken(email);

                if (token != null)
                {
                    string subject = "Đặt lại mật khẩu";
                    string body = "Xin chào,\n\nBạn đã yêu cầu đặt lại mật khẩu. Vui lòng sử dụng mã sau để đặt lại mật khẩu của bạn:\n\n" + token;

                    bool emailSent = nv.SendEmail(email, subject, body);
                    if (emailSent)
                    {
                        MessageBox.Show("Email chứa liên kết để đặt lại mật khẩu đã được gửi.");
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi gửi email. Vui lòng thử lại sau.");
                    }
                }
                else
                {
                    MessageBox.Show("Email không tồn tại trong hệ thống.");
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtNhapMa.Text = "";
            txtMatKhauMoi.Text = "";
            txtTaiKhoan.Text = "";
            txtTaiKhoan.Focus();
        }

        private void btnKiemtra_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtTaiKhoan.Text;
                string token = txtNhapMa.Text;
                string newPassword = txtMatKhauMoi.Text;

                if (nv.ResetPassword(token, email, newPassword))
                {
                    MessageBox.Show("Mật khẩu đã được đổi thành công.");
                    txtNhapMa.Text = "";
                    txtMatKhauMoi.Text = "";
                    txtTaiKhoan.Text = "";
                    txtTaiKhoan.Focus();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra khi đổi mật khẩu.");
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
