using DAL;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BLL
{
    public class BLL_KhachHang
    {
        QLSieuThiDataContext qlst = new QLSieuThiDataContext();

        public List<ListFrmKH> Get_ListKH()
        {
            var listkh = (
                from kh in qlst.KhachHangs
                where kh.TrangThaiXoa == 1 && kh.MaKH != 1
                select new ListFrmKH
                {
                    MaKH = kh.MaKH,
                    TenKH = kh.TenKH,
                    GioiTinh = kh.GioiTinh,
                    DiaChi = kh.DiaChi,
                    SDT = kh.SDT,
                }).ToList();
            return listkh;
        }

        public List<ListFrmKH> GetListKH_IsDeleted()
        {
            var listkh = (
                from kh in qlst.KhachHangs
                where kh.TrangThaiXoa == 0 && kh.MaKH != 1
                select new ListFrmKH
                {
                    MaKH = kh.MaKH,
                    TenKH = kh.TenKH,
                    GioiTinh = kh.GioiTinh,
                    DiaChi = kh.DiaChi,
                    SDT = kh.SDT,
                }).ToList();
            return listkh;
        }

        public void RestoreKH(int makh)
        {
            var kh = qlst.KhachHangs.FirstOrDefault(p => p.MaKH == makh);

            if (kh != null)
            {
                kh.TrangThaiXoa = 1;
                qlst.SubmitChanges();
            }
        }

        public KhachHang Search_KH(int maKH)
        {
            return qlst.KhachHangs.FirstOrDefault(kh => kh.MaKH == maKH && kh.TrangThaiXoa == 1 && kh.MaKH != 1);
        }

        public List<ListFrmKH> Search_ListKH(string tenKH)
        {
            var searchList = (
                from kh in qlst.KhachHangs
                where kh.TenKH.Contains(tenKH) && kh.TrangThaiXoa == 1 && kh.MaKH != 1
                select new ListFrmKH
                {
                    MaKH = kh.MaKH,
                    TenKH = kh.TenKH,
                    GioiTinh = kh.GioiTinh,
                    DiaChi = kh.DiaChi,
                    SDT = kh.SDT
                }).ToList();

            return searchList;
        }

        public List<ListFrmKH> Search_ListKH_MaKH(int maKH)
        {
            var searchList = (
                from kh in qlst.KhachHangs
                where kh.MaKH == maKH && kh.TrangThaiXoa == 1 && kh.MaKH != 1
                select new ListFrmKH
                {
                    MaKH = kh.MaKH,
                    TenKH = kh.TenKH,
                    GioiTinh = kh.GioiTinh,
                    DiaChi = kh.DiaChi,
                    SDT = kh.SDT
                }).ToList();

            return searchList;
        }

        public List<ListFrmKH> Search_ListKH_GTinh(string gtinh)
        {
            var searchList = (
                from kh in qlst.KhachHangs
                where kh.GioiTinh.Contains(gtinh) && kh.TrangThaiXoa == 1 && kh.MaKH != 1
                select new ListFrmKH
                {
                    MaKH = kh.MaKH,
                    TenKH = kh.TenKH,
                    GioiTinh = kh.GioiTinh,
                    DiaChi = kh.DiaChi,
                    SDT = kh.SDT
                }).ToList();

            return searchList;
        }

        public void sort(DataGridView dgv)
        {
            var kq = from KH in qlst.KhachHangs
                     where KH.TrangThaiXoa == 1 && KH.MaKH != 1
                     orderby KH.TenKH ascending, KH.MaKH descending
                     select new { KH.MaKH, KH.TenKH, KH.GioiTinh, KH.DiaChi, KH.SDT };
            dgv.DataSource = kq;
        }

        public void Update_KH(int maKH, string tenKH, string gtinh, string dchi, string sdt, string email, string mk, int diemtichluy)
        {
            KhachHang kh = qlst.KhachHangs.Where(q => q.MaKH == maKH).FirstOrDefault();
            if (kh != null)
            {
                kh.TenKH = tenKH;
                kh.GioiTinh = gtinh;
                kh.DiaChi = dchi;
                kh.SDT = sdt;
                kh.Email = email;
                kh.MatKhau = mk;
                kh.DiemTichLuy = diemtichluy;
            }
            qlst.SubmitChanges();
        }

        public bool Delete_KH(int makh)
        {
            try
            {
                KhachHang kh = qlst.KhachHangs.Where(q => q.MaKH == makh).FirstOrDefault();
                if (kh != null)
                {
                    qlst.KhachHangs.DeleteOnSubmit(kh);
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

        public bool DeleteKH(int makh)
        {
            var kh = qlst.KhachHangs.FirstOrDefault(p => p.MaKH == makh);

            if (kh != null)
            {
                kh.TrangThaiXoa = 0;
                qlst.SubmitChanges();
                return true;
            }
            return false;
        }

        public void Add_KH(string tenKH, string gtinh, string dchi, string sdt, string email, string mk, int diemtichluy)
        {
            KhachHang kh = new KhachHang();
            kh.TenKH = tenKH;
            kh.GioiTinh = gtinh;
            kh.DiaChi = dchi;
            kh.SDT = sdt;
            kh.Email = email;
            kh.MatKhau = mk;
            kh.DiemTichLuy = diemtichluy;
            kh.TrangThaiXoa = 1;
            qlst.KhachHangs.InsertOnSubmit(kh);
            qlst.SubmitChanges();
        }

        public KhachHang Search_KH_SDT(string sdt)
        {
            return qlst.KhachHangs.Where(t => t.SDT == sdt && t.TrangThaiXoa == 1 && t.MaKH != 1).FirstOrDefault();
        }
    }
}
