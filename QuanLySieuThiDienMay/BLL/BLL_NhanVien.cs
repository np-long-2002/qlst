using DAL;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using Microsoft.Office.Interop.Excel;
using System.Data.Linq;
using Google.Cloud.Storage.V1;
using System.Net.Mail;
using System.Net;

namespace BLL
{
	public class BLL_NhanVien
	{
        QLSieuThiDataContext qlst = new QLSieuThiDataContext();

        //string folderPath = @"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\NhanVien";

        //public bool CheckDangNhap(string tk, string mk)
        //{
        //    var check = qlst.NhanViens.Any(nv => (nv.SDT == tk || nv.Email == tk) && nv.MatKhau == mk);
        //    return check;
        //}

        public bool CheckDangNhap(string tk, string mk)
        {
            var check = qlst.NhanViens.Any(nv => (nv.SDT == tk || nv.Email == tk) && nv.MatKhau == mk && nv.TrangThaiXoa == 1 && nv.MaNV != 1 && nv.MaNV != 2);
            return check;
        }

        public string GetTenNhanVien(string tk, string mk)
        {
            var nhanVien = qlst.NhanViens.FirstOrDefault(nv => (nv.SDT == tk || nv.Email == tk) && nv.MatKhau == mk && nv.TrangThaiXoa == 1 && nv.MaNV != 1 && nv.MaNV != 2);
            if (nhanVien != null)
            {
                return nhanVien.TenNV;
            }
            return null;
        }

        public int GetMaNV(string tk, string mk)
        {
            var nhanVien = qlst.NhanViens.FirstOrDefault(nv => (nv.SDT == tk || nv.Email == tk) && nv.MatKhau == mk && nv.TrangThaiXoa == 1 && nv.MaNV != 1 && nv.MaNV != 2);
            if (nhanVien != null)
            {
                return nhanVien.MaNV;
            }
            return -1;
        }

        //public System.Drawing.Image GetAnhNhanVien(string tk, string mk, Size newSize)
        //{
        //    var nhanVien = qlst.NhanViens.FirstOrDefault(nv => (nv.SDT == tk || nv.Email == tk) && nv.MatKhau == mk && nv.TrangThaiXoa == 1 && nv.MaNV != 1);
        //    if (nhanVien != null)
        //    {
        //        string tenAnh = nhanVien.AnhNV;
        //        string duongDanAnh = Path.Combine(@"C:\Users\Admin\Downloads\QuanLySieuThiDienMay\Images\NhanVien", tenAnh);
        //        //string duongDanAnh = Path.Combine(@"D:\QuanLySieuThiDienMay\Images\NhanVien", tenAnh);
        //        if (File.Exists(duongDanAnh))
        //        {
        //            // Đọc ảnh từ đường dẫn
        //            System.Drawing.Image anhGoc = System.Drawing.Image.FromFile(duongDanAnh);
        //            // Tạo ảnh nhỏ với kích thước mới
        //            System.Drawing.Image anhNho = (System.Drawing.Image)(new Bitmap(anhGoc, newSize));
        //            // Giải phóng bộ nhớ của ảnh gốc
        //            anhGoc.Dispose();
        //            return anhNho;
        //        }
        //    }
        //    return null;
        //}

        public async Task<System.Drawing.Image> GetAnhNhanVienAsync(string tk, string mk, Size newSize)
        {
            var nhanVien = qlst.NhanViens.FirstOrDefault(nv => (nv.SDT == tk || nv.Email == tk) && nv.MatKhau == mk && nv.TrangThaiXoa == 1 && nv.MaNV != 1 && nv.MaNV != 2);
            if (nhanVien != null)
            {
                string tenAnh = nhanVien.AnhNV;
                string firebasePath = $"staff/{tenAnh}";

                try
                {
                    var storage = StorageClient.Create();
                    using (var imageStream = new MemoryStream())
                    {
                        await storage.DownloadObjectAsync("sieuthidienmay-6e8af.appspot.com", firebasePath, imageStream);
                        imageStream.Position = 0;
                        System.Drawing.Image anhGoc = System.Drawing.Image.FromStream(imageStream);

                        System.Drawing.Image anhNho = (System.Drawing.Image)(new Bitmap(anhGoc, newSize));

                        anhGoc.Dispose();

                        return anhNho;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể tải ảnh {tenAnh}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return null;
        }

        public List<ListFrmNV> Get_ListNV()
        {
            var listnv = (
                from nv in qlst.NhanViens
                join cv in qlst.ChucVus on nv.MaCV equals cv.MaCV
                where nv.TrangThaiXoa == 1 && nv.MaNV != 1 && nv.MaNV != 2
                select new ListFrmNV
                {
                    MaNV = nv.MaNV,
                    TenNV = nv.TenNV,
                    GioiTinh = nv.GioiTinh,
                    Email = nv.Email,
                    MaCV = cv.MoTa,
                }).ToList();
            return listnv;
        }

        public NhanVien Search_NV(int maNV)
        {
            return qlst.NhanViens.FirstOrDefault(nv => nv.MaNV == maNV && nv.TrangThaiXoa == 1 && nv.MaNV != 1 && nv.MaNV != 2);
        }

        public List<ListFrmNV> Search_ListNV(string tenNV)
        {
            var searchList = (
                from nv in qlst.NhanViens
                join cv in qlst.ChucVus on nv.MaCV equals cv.MaCV
                where nv.TenNV.Contains(tenNV) && nv.TrangThaiXoa == 1 && nv.MaNV != 1 && nv.MaNV != 2
                select new ListFrmNV
                {
                    MaNV = nv.MaNV,
                    TenNV = nv.TenNV,
                    GioiTinh = nv.GioiTinh,
                    Email = nv.Email,
                    MaCV = cv.MoTa
                }).ToList();

            return searchList;
        }

        public List<ListFrmNV> Search_ListNV_MaNV(int maNV)
        {
            var searchList = (
                from nv in qlst.NhanViens
                join cv in qlst.ChucVus on nv.MaCV equals cv.MaCV
                where nv.MaNV == maNV && nv.TrangThaiXoa == 1 && nv.MaNV != 1 && nv.MaNV != 2

                select new ListFrmNV
                {
                    MaNV = nv.MaNV,
                    TenNV = nv.TenNV,
                    GioiTinh = nv.GioiTinh,
                    Email = nv.Email,
                    MaCV = cv.MoTa
                }).ToList();

            return searchList;
        }

        public List<ListFrmNV> Search_ListNV_GTinh(string gtinh)
        {
            var searchList = (
                from nv in qlst.NhanViens
                join cv in qlst.ChucVus on nv.MaCV equals cv.MaCV
                where nv.GioiTinh.Contains(gtinh) && nv.TrangThaiXoa == 1 && nv.MaNV != 1 && nv.MaNV != 2
                select new ListFrmNV
                {
                    MaNV = nv.MaNV,
                    TenNV = nv.TenNV,
                    GioiTinh = nv.GioiTinh,
                    Email = nv.Email,
                    MaCV = cv.MoTa
                }).ToList();

            return searchList;
        }

        public void sort(DataGridView dgv)
        {
            var kq = from NV in qlst.NhanViens
                     orderby NV.TenNV ascending, NV.MaNV descending
                     where NV.TrangThaiXoa == 1 && NV.MaNV != 1 && NV.MaNV != 2
                     select new { NV.MaNV, NV.TenNV, NV.GioiTinh, NV.Email, NV.MaCV };
            dgv.DataSource = kq;
        }

        public void Update_NV(int maNV, string tenNV, string gtinh, string dchi, string sdt, string email, string cccd, string mk, int maCV)
        {
            NhanVien nv = qlst.NhanViens.Where(q => q.MaNV == maNV).FirstOrDefault();
            if (nv != null)
            {
                nv.TenNV = tenNV;
                nv.GioiTinh = gtinh;
                nv.DiaChi = dchi;
                nv.SDT = sdt;
                nv.Email = email;
                nv.CCCD = cccd;
                nv.MatKhau = mk;
                nv.MaCV = maCV;
            }
            qlst.SubmitChanges();
        }

        public async void Update_ImageNV(string imageName, int maNV)
        {
            try
            {
                NhanVien nv = qlst.NhanViens.FirstOrDefault(q => q.MaNV == maNV);
                if (nv != null)
                {
                    string oldImageName = nv.AnhNV;

                    nv.AnhNV = imageName;
                    qlst.SubmitChanges();

                    DeleteOldImageFromFirebase(maNV, oldImageName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật ảnh nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DeleteOldImageFromFirebase(int maNV, string oldImageName)
        {
            try
            {
                var storage = StorageClient.Create();
                string remoteImagePath = $"staff/{oldImageName}";

                if (!string.IsNullOrEmpty(oldImageName))
                {
                    await storage.DeleteObjectAsync("sieuthidienmay-6e8af.appspot.com", remoteImagePath);

                    // Delete old image from local folder
                    //string oldLocalPath = Path.Combine(folderPath, oldImageName);
                    //if (File.Exists(oldLocalPath))
                    //{
                    //    File.Delete(oldLocalPath);
                    //}
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tên ảnh cũ để xoá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xoá ảnh cũ trên Firebase Storage: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool Delete_NV(int maNV)
        { 
            try
            {
                NhanVien nv = qlst.NhanViens.Where(q => q.MaNV == maNV).FirstOrDefault();
                if (nv != null)
                {
                    qlst.NhanViens.DeleteOnSubmit(nv);
                    qlst.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa thông tin khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteNV(int maNV)
        {
            var nv = qlst.NhanViens.FirstOrDefault(p => p.MaNV == maNV);

            if (nv != null)
            {
                nv.TrangThaiXoa = 0;
                qlst.SubmitChanges();
                return true;
            }
            return false;
        }

        public void Add_NV(string tenNV, string gtinh, string dchi, string sdt, string email, string cccd, string anhNV, string mk, int maCV)
        {
            NhanVien nv = new NhanVien();
            nv.TenNV = tenNV;
            nv.GioiTinh = gtinh;
            nv.DiaChi = dchi;
            nv.SDT = sdt;
            nv.Email = email;
            nv.CCCD = cccd;
            nv.AnhNV = anhNV;
            nv.MatKhau = mk;
            nv.MaCV = maCV;
            nv.TrangThaiXoa = 1;
            qlst.NhanViens.InsertOnSubmit(nv);
            qlst.SubmitChanges();
        }

        public List<ListFrmNV_IsDelete> GetListNV_IsDeleted()
        {
            var listkh = (
                from nv in qlst.NhanViens
                where nv.TrangThaiXoa == 0 && nv.MaNV != 1 && nv.MaNV != 2
                select new ListFrmNV_IsDelete
                {
                    MaNV = nv.MaNV,
                    TenNV = nv.TenNV,
                    GioiTinh = nv.GioiTinh,
                }).ToList();
            return listkh;
        }

        public void RestoreNV(int manv)
        {
            var nv = qlst.NhanViens.FirstOrDefault(p => p.MaNV == manv);

            if (nv != null)
            {
                nv.TrangThaiXoa = 1;
                qlst.SubmitChanges();
            }
        }

        //public string GeneratePasswordResetToken(string email)
        //{
        //    var nv = qlst.NhanViens.FirstOrDefault(p => p.Email == email);
        //    if (nv != null)
        //    {
        //        byte[] tokenBytes = new byte[64];
        //        using (var rng = RandomNumberGenerator.Create())
        //        {
        //            rng.GetBytes(tokenBytes);
        //        }
        //        string token = Convert.ToBase64String(tokenBytes);
        //        nv.Token = token;
        //        //nv.TimeReset = DateTime.Now.AddHours(1);
        //        nv.TimeReset = DateTime.Now.AddSeconds(300);
        //        qlst.SubmitChanges();

        //        return token;
        //    }
        //    return null;
        //}

        //public bool ResetPassword(string token, string email, string newPassword)
        //{
        //    var nv = qlst.NhanViens.FirstOrDefault(p => p.Email == email);
        //    if (nv != null)
        //    {
        //        if (nv.Token == token)
        //        {
        //            if (DateTime.Now <= nv.TimeReset)
        //            {
        //                nv.MatKhau = newPassword;
        //                nv.Token = null;
        //                nv.TimeReset = null;
        //                qlst.SubmitChanges();
        //                return true;
        //            }
        //            else
        //            {
        //                MessageBox.Show("Mã code đã hết hạn.");
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Mã code không chính xác.");
        //        }
        //    }
        //    return false;
        //}

        public string GeneratePasswordResetToken(string email)
        {
            var nv = qlst.NhanViens.FirstOrDefault(p => p.Email == email);
            if (nv != null)
            {
                byte[] tokenBytes = new byte[64];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(tokenBytes);
                }
                string token = Convert.ToBase64String(tokenBytes);
                nv.Token = token;
                nv.TimeReset = DateTime.Now.AddMinutes(5);
                qlst.SubmitChanges();

                return token;
            }
            return null;
        }

        public bool ResetPassword(string token, string email, string newPassword)
        {
            var nv = qlst.NhanViens.FirstOrDefault(p => p.Email == email);
            if (nv != null)
            {
                if (nv.Token == token)
                {
                    if (DateTime.Now <= nv.TimeReset)
                    {
                        nv.MatKhau = newPassword;
                        nv.Token = null;
                        nv.TimeReset = null;
                        qlst.SubmitChanges();
                        MessageBox.Show("Mật khẩu đã được đặt lại thành công.");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Mã code đã hết hạn.");
                    }
                }
                else
                {
                    MessageBox.Show("Mã code không chính xác.");
                }
            }
            else
            {
                MessageBox.Show("Email không tồn tại trong hệ thống.");
            }
            return false;
        }

        public bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress("nplongkl2024@gmail.com", "LongHauPhat");
                var toAddress = new MailAddress(toEmail);
                const string fromPassword = "pocu gbgy nfuu pwrz";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gửi mail thất bại : " + ex.Message);
                return false;
            }
        }

        public List<ListTopNV> Top10NhanVienDoanhThuTrongThang()
        {
            DateTime now = DateTime.Now;
            int currentMonth = now.Month;
            int currentYear = now.Year;

            var query = from nv in qlst.NhanViens
                        join hd in qlst.HoaDons on nv.MaNV equals hd.MaNV
                        where hd.NgayLap.Month == currentMonth && hd.NgayLap.Year == currentYear && nv.TrangThaiXoa == 1 && nv.MaNV != 1 && nv.MaNV != 2
                        join cthd in qlst.ChiTietHoaDons on hd.MaHD equals cthd.MaHD
                        join sp in qlst.SanPhams on cthd.MaSP equals sp.MaSP
                        join ctpk in qlst.ChiTietPhieuKhos on sp.MaSP equals ctpk.MaSP
                        select new
                        {
                            TenNV = nv.TenNV,
                            DoanhThu = cthd.SoLuong * (sp.GiaSP - ctpk.DonGia)
                        };

            var doanhThuNhanVien = query.GroupBy(item => item.TenNV)
                                        .Select(group => new ListTopNV
                                        {
                                            TenNV = group.Key,
                                            DoanhThu = group.Sum(item => item.DoanhThu)
                                        })
                                        .OrderByDescending(item => item.DoanhThu)
                                        .Take(10)
                                        .ToList();
            return doanhThuNhanVien;
        }

        public string GetStaffImageName(int maNV)
        {
            string imageName = null;

            try
            {
                Table<NhanVien> nhanVienTable = qlst.GetTable<NhanVien>();
                var query = from nv in nhanVienTable
                            where nv.MaNV == maNV
                            select nv.AnhNV;

                imageName = query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lấy tên ảnh từ cơ sở dữ liệu: " + ex.Message);
            }

            return imageName;
        }

        public bool KiemTraThongTinDangNhap(string emailOrPhone, string oldPassword)
        {
            NhanVien nv = qlst.NhanViens.FirstOrDefault(q => (q.Email == emailOrPhone || q.SDT == emailOrPhone) && q.MatKhau == oldPassword);

            if (nv != null)
            {
                return true;
            }

            return false;
        }

        public void DoiMatKhau(int maNV, string newPass)
        {
            NhanVien nv = qlst.NhanViens.FirstOrDefault(q => q.MaNV == maNV);
            if (nv != null)
            {
                nv.MatKhau = newPass;
            }
            qlst.SubmitChanges();
        }

        public int CheckCV(string tk, string mk)
        {
            var nhanVien = qlst.NhanViens.SingleOrDefault(nv => (nv.SDT == tk || nv.Email == tk) && nv.MatKhau == mk);

            if (nhanVien != null)
            {
                return nhanVien.MaCV;
            }
            else
            {
                throw new Exception("Nhân viên không tìm thấy");
            }
        }
    }
}
