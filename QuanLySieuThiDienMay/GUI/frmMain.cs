using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QRCoder.PayloadGenerator;

namespace GUI
{
    public partial class frmMain : Form
    {
        BLL_NhanVien nv = new BLL_NhanVien();
        BLL_BackupRestore bk = new BLL_BackupRestore();

        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public int MaKho { get; set; }

        bool QuanLyCollapsed;
        bool NhanVienBanHangCollapsed;
        bool NhanVienKhoCollapsed;
        bool ChucNangCollapsed;

        public frmMain()
        {
            InitializeComponent();
            Load += FrmMain_Load;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                int check = nv.CheckCV(TaiKhoan, MatKhau);
                switch (check)
                {
                    case 1:
                        btnQuanLy.Enabled = true;
                        btnNVBH.Enabled = true;
                        btnNVK.Enabled = true;
                        break;
                    case 2:
                        btnQuanLy.Enabled = false;
                        btnNVBH.Enabled = true;
                        btnNVK.Enabled = false;
                        break;
                    case 3:
                        btnQuanLy.Enabled = false;
                        btnNVBH.Enabled = false;
                        btnNVK.Enabled = true;
                        break;
                    default:
                        btnQuanLy.Enabled = false;
                        btnNVBH.Enabled = false;
                        btnNVK.Enabled = false;
                        MessageBox.Show("Vai trò người dùng không xác định!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi tải giao diện chính: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private Form currentFormChild;

        private void openChildForm(Form childForm)
        {
            try
            {
                if (currentFormChild != null)
                {
                    currentFormChild.Close();
                }
                currentFormChild = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                panel_body.Controls.Add(childForm);
                panel_body.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
            }
            catch (Exception ex)
            {

				MessageBox.Show($"Đã xảy ra lỗi khi mở chức năng : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        private void btn_nhanvien_Click(object sender, EventArgs e)
        {
            openChildForm(new frmNhanVien());
        }

        private void btn_hoadon_Click(object sender, EventArgs e)
        {
            frmHoaDon frm = new frmHoaDon();
            frm.MaKho = MaKho;
            openChildForm(frm);
        }

        private void btn_banhang_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFormChild != null)
                {
                    currentFormChild.Close();
                }
                frmTestLoadSanPhamImage frm = new frmTestLoadSanPhamImage();
                frm.TenNhanVien = btn_Info.Text.Substring(10);
                frm.MaNV = nv.GetMaNV(TaiKhoan, MatKhau);
                frm.MaKho = MaKho;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
				MessageBox.Show($"Đã xảy ra lỗi khi mở chức năng bán hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btn_thongke_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFormChild != null)
                {
                    currentFormChild.Close();
                }
                frmThongKe frm = new frmThongKe();
                frm.MaKho = MaKho;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi mở chức năng thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không ?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                frmDangNhap a = new frmDangNhap();
                a.Show();
                this.Hide();
            }
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }

            CollapseAllPanels();
            UpdateControlPositions();
            ResetButtonColors((Guna.UI2.WinForms.Guna2Button)sender);
        }

        private void btn_Info_Click(object sender, EventArgs e)
        {
            string currentYear = DateTime.Now.Year.ToString();
            String tt = "";
            tt += "Phần mềm : Quản lý Siêu thị Điện máy \n";

            tt += "\n ";
            tt += "\nversion : 1.1 @ năm " + currentYear + "  \n\n";
            tt += "\n\n";
            tt += " Học phần : ";
            tt += "\t";
            tt += "____Khoá luận____";
            tt += "\n";
            tt += "\nSinh viên thực hiện : \nNguyễn Phước Long - Nguyễn Văn Hậu - Nguyễn Tấn Phát";
            tt += "\n\nEmail : abc0@gmail.com  \n\n";
            tt += "\n";
            MessageBox.Show((tt), "Thông tin", MessageBoxButtons.OK);
        }

        private void btn_nhacungcap_Click(object sender, EventArgs e)
        {
            openChildForm(new frmNhaCungCap());
        }

        private void btn_sanpham_Click(object sender, EventArgs e)
        {
            frmSanPham sp = new frmSanPham();
            sp.MaKho = MaKho;
            openChildForm(sp);
        }

        private void btn_khachhang_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKhachHang());
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {

            pbAnhNV.Image = Properties.Resources.placeholder_image_human;
            string tenNhanVien = nv.GetTenNhanVien(TaiKhoan, MatKhau);

            if (tenNhanVien != null)
            {
                btn_Info.Text = "Xin chào, " + tenNhanVien;
            }
            else
            {
                MessageBox.Show("Không thể lấy tên nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Image anhNhanVien = await nv.GetAnhNhanVienAsync(TaiKhoan, MatKhau, pbAnhNV.Size);

                if (anhNhanVien != null)
                {
                    pbAnhNV.Image = anhNhanVien;
                }
                else
                {
                    MessageBox.Show("Không thể tải ảnh nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải ảnh nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_kho_Click(object sender, EventArgs e)
        {
            frmKho kh = new frmKho();
            kh.MaKho = MaKho;
            kh.MaNV = nv.GetMaNV(TaiKhoan,MatKhau);
            openChildForm(kh);
        }

        private void QuanLyTimer_Tick(object sender, EventArgs e)
        {
            if (QuanLyCollapsed)
            {
                QLPanel.Height += 10;
                if (QLPanel.Height >= QLPanel.MaximumSize.Height)
                {
                    QLPanel.Height = QLPanel.MaximumSize.Height;
                    QuanLyCollapsed = false;
                    QuanLyTimer.Stop();
                }
            }
            else
            {
                QLPanel.Height -= 10;
                if (QLPanel.Height <= QLPanel.MinimumSize.Height)
                {
                    QLPanel.Height = QLPanel.MinimumSize.Height;
                    QuanLyCollapsed = true;
                    QuanLyTimer.Stop();
                }
            }

            UpdateControlPositions();
            CollapseOtherPanels(QLPanel);
        }

        private void NhanVienBanHangTimer_Tick(object sender, EventArgs e)
        {
            if (NhanVienBanHangCollapsed)
            {
                NVPanel.Height += 10;
                if (NVPanel.Height >= NVPanel.MaximumSize.Height)
                {
                    NVPanel.Height = NVPanel.MaximumSize.Height;
                    NhanVienBanHangCollapsed = false;
                    NhanVienBanHangTimer.Stop();
                }
            }
            else
            {
                NVPanel.Height -= 10;
                if (NVPanel.Height <= NVPanel.MinimumSize.Height)
                {
                    NVPanel.Height = NVPanel.MinimumSize.Height;
                    NhanVienBanHangCollapsed = true;
                    NhanVienBanHangTimer.Stop();
                }
            }

            UpdateControlPositions1();
            CollapseOtherPanels(NVPanel);
        }

        private void NhanVienKhoTimer_Tick(object sender, EventArgs e)
        {
            if (NhanVienKhoCollapsed)
            {
                NVKPanel.Height += 10;
                if (NVKPanel.Height >= NVKPanel.MaximumSize.Height)
                {
                    NVKPanel.Height = NVKPanel.MaximumSize.Height;
                    NhanVienKhoCollapsed = false;
                    NhanVienKhoTimer.Stop();
                }
            }
            else
            {
                NVKPanel.Height -= 10;
                if (NVKPanel.Height <= NVKPanel.MinimumSize.Height)
                {
                    NVKPanel.Height = NVKPanel.MinimumSize.Height;
                    NhanVienKhoCollapsed = true;
                    NhanVienKhoTimer.Stop();
                }
            }

            UpdateControlPositions2();
            CollapseOtherPanels(NVKPanel);
        }

        private void ChucNangTimer_Tick(object sender, EventArgs e)
        {
            if (ChucNangCollapsed)
            {
                CNPanel.Height += 10;
                if (CNPanel.Height >= CNPanel.MaximumSize.Height)
                {
                    CNPanel.Height = CNPanel.MaximumSize.Height;
                    ChucNangCollapsed = false;
                    ChucNangTimer.Stop();
                }
            }
            else
            {
                CNPanel.Height -= 10;
                if (CNPanel.Height <= CNPanel.MinimumSize.Height)
                {
                    CNPanel.Height = CNPanel.MinimumSize.Height;
                    ChucNangCollapsed = true;
                    ChucNangTimer.Stop();
                }
            }

            //UpdateControlPositions();
            CollapseOtherPanels(CNPanel);
        }

        private void UpdateControlPositions()
        {
            NVPanel.Top = QLPanel.Bottom;
            NVKPanel.Top = NVPanel.Bottom;
            CNPanel.Top = NVKPanel.Bottom;
        }

        private void UpdateControlPositions1()
        {
            NVKPanel.Top = NVPanel.Bottom;
            CNPanel.Top = NVKPanel.Bottom;
        }

        private void UpdateControlPositions2()
        {
            CNPanel.Top = NVKPanel.Bottom;
        }

        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            NhanVienBanHangTimer.Stop();
            NhanVienKhoTimer.Stop();
            ChucNangTimer.Stop();
            QuanLyTimer.Start();
            UpdateButtonColors((Guna.UI2.WinForms.Guna2Button)sender);
        }

        private void btnNVBH_Click(object sender, EventArgs e)
        {
            QuanLyTimer.Stop();
            NhanVienKhoTimer.Stop();
            ChucNangTimer.Stop();
            NhanVienBanHangTimer.Start();
            UpdateButtonColors((Guna.UI2.WinForms.Guna2Button)sender);
        }

        private void btnNVK_Click(object sender, EventArgs e)
        {
            QuanLyTimer.Stop();
            NhanVienBanHangTimer.Stop();
            ChucNangTimer.Stop();
            NhanVienKhoTimer.Start();
            UpdateButtonColors((Guna.UI2.WinForms.Guna2Button)sender);
        }

        private void btnChucNang_Click(object sender, EventArgs e)
        {
            QuanLyTimer.Stop();
            NhanVienKhoTimer.Stop();
            NhanVienBanHangTimer.Stop();
            ChucNangTimer.Start();
            UpdateButtonColors((Guna.UI2.WinForms.Guna2Button)sender);
        }

        private void CollapseOtherPanels(Panel expandedPanel)
        {
            if (expandedPanel != QLPanel && QLPanel.Height != QLPanel.MinimumSize.Height)
            {
                QLPanel.Height = QLPanel.MinimumSize.Height;
                QuanLyCollapsed = true;
            }
            if (expandedPanel != NVPanel && NVPanel.Height != NVPanel.MinimumSize.Height)
            {
                NVPanel.Height = NVPanel.MinimumSize.Height;
                NhanVienBanHangCollapsed = true;
            }
            if (expandedPanel != NVKPanel && NVKPanel.Height != NVKPanel.MinimumSize.Height)
            {
                NVKPanel.Height = NVKPanel.MinimumSize.Height;
                NhanVienKhoCollapsed = true;
            }
            if (expandedPanel != CNPanel && CNPanel.Height != CNPanel.MinimumSize.Height)
            {
                CNPanel.Height = CNPanel.MinimumSize.Height;
                ChucNangCollapsed = true;
            }
            UpdateControlPositions();
        }

        private void CollapseAllPanels()
        {
            QuanLyTimer.Stop();
            NhanVienBanHangTimer.Stop();
            NhanVienKhoTimer.Stop();
            ChucNangTimer.Stop();

            if (QLPanel.Height != QLPanel.MinimumSize.Height)
            {
                QLPanel.Height = QLPanel.MinimumSize.Height;
                QuanLyCollapsed = true;
            }
            if (NVPanel.Height != NVPanel.MinimumSize.Height)
            {
                NVPanel.Height = NVPanel.MinimumSize.Height;
                NhanVienBanHangCollapsed = true;
            }
            if (NVKPanel.Height != NVKPanel.MinimumSize.Height)
            {
                NVKPanel.Height = NVKPanel.MinimumSize.Height;
                NhanVienKhoCollapsed = true;
            }
            if (CNPanel.Height != CNPanel.MinimumSize.Height)
            {
                CNPanel.Height = CNPanel.MinimumSize.Height;
                ChucNangCollapsed = true;
            }
        }

        private void UpdateButtonColors(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            Color defaultColor = Color.FromArgb(52, 49, 130);
            Color activeColor = Color.Lime;

            btnQuanLy.FillColor = defaultColor;
            btnNVBH.FillColor = defaultColor;
            btnNVK.FillColor = defaultColor;
            btnChucNang.FillColor = defaultColor;

            activeButton.FillColor = activeColor;
        }

        private void ResetButtonColors(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            Color defaultColor = Color.FromArgb(52, 49, 130);

            btnQuanLy.FillColor = defaultColor;
            btnNVBH.FillColor = defaultColor;
            btnNVK.FillColor = defaultColor;
            btnChucNang.FillColor = defaultColor;
        }

        private void btnTaoMa_Click(object sender, EventArgs e)
        {
            openChildForm(new frmQRCode());
            //if (currentFormChild != null)
            //{
            //    currentFormChild.Close();
            //}
            //frmQRCode frm = new frmQRCode();
            //frm.ShowDialog();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string backupPath = bk.BackupDatabase(folderBrowser.SelectedPath);
                        MessageBox.Show($"Tạo file Backup thành công ở {backupPath}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Bị lỗi khi Backup: {ex.Message}", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKhoiPhuc());
            //if (currentFormChild != null)
            //{
            //    currentFormChild.Close();
            //}
            //frmKhoiPhuc frm = new frmKhoiPhuc();
            //frm.ShowDialog();
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            //if (currentFormChild != null)
            //{
            //    currentFormChild.Close();
            //}
            int ma = nv.GetMaNV(TaiKhoan, MatKhau);
            string ten = nv.GetTenNhanVien(TaiKhoan, MatKhau);
            openChildForm(new frmMatKhauMoi(ma, ten));
            //frmMatKhauMoi frm = new frmMatKhauMoi(ma, ten);
            //frm.ShowDialog();
        }
    }
}
