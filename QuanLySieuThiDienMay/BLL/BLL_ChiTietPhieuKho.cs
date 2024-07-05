using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_ChiTietPhieuKho
    {
        QLSieuThiDataContext qlst = new QLSieuThiDataContext();

        public void AddChiTietPhieuKho(int mapk, int masp, int soluong, int dongia)
        {
            ChiTietPhieuKho ctpk = new ChiTietPhieuKho();
            ctpk.MaSP = masp;
            ctpk.MaPK = mapk;
            ctpk.DonGia = dongia;
            ctpk.SoLuong = soluong;
            ctpk.ThanhTien = ctpk.SoLuong * ctpk.DonGia;
            qlst.ChiTietPhieuKhos.InsertOnSubmit(ctpk);
            qlst.SubmitChanges();
            var maKho = qlst.PhieuKhos.FirstOrDefault(pk => pk.MaPK == mapk)?.MaKho;
            var chiTietKho = qlst.ChiTietKhos.FirstOrDefault(ck => ck.MaKho == maKho && ck.MaSP == masp);
            if (chiTietKho != null)
            {
                chiTietKho.SoLuongTrongKho += soluong;
            }
            var sanPham = qlst.SanPhams.FirstOrDefault(sp => sp.MaSP == masp);
            if (sanPham != null)
            {
                sanPham.SoLuong += soluong;
            }
            var phieuKho = qlst.PhieuKhos.FirstOrDefault(pk => pk.MaPK == mapk);
            if (phieuKho != null)
            {
                var tongTien = qlst.ChiTietPhieuKhos.Where(ct => ct.MaPK == mapk).Sum(ct => ct.ThanhTien);
                phieuKho.TongTien = tongTien;
            }
            qlst.SubmitChanges();
        }

        //public List<ChiTietPhieuKho> GetChiTietPhieuKho(int mapk)
        //{
        //    return qlst.ChiTietPhieuKhos.Where(q => q.MaPK == mapk).ToList();
        //}

        public List<ListChiTietPhieuKho> GetChiTietPhieuKho(int mapk)
        {
            var query = from ctpk in qlst.ChiTietPhieuKhos
                        join sp in qlst.SanPhams on ctpk.MaSP equals sp.MaSP
                        where ctpk.MaPK == mapk
                        select new ListChiTietPhieuKho
                        {
                            MaPK = ctpk.MaPK,
                            TenSP = sp.TenSP,
                            SoLuong = ctpk.SoLuong,
                            DonGia = ctpk.DonGia,
                            ThanhTien = ctpk.ThanhTien
                        };

            return query.ToList();
        }

        public bool KiemTraGiaNhap(int masp, int dongia)
        {
            var sanPham = qlst.SanPhams.FirstOrDefault(sp => sp.MaSP == masp);
            if (sanPham == null)
            {
                throw new InvalidOperationException("Sản phẩm không tồn tại.");
            }
            if (dongia >= sanPham.GiaSP)
            {
                return false;
            }
            return true;
        }

        public List<ListChiTietPhieuKho> Get_HDNHDetails(int maPK)
        {
            var details = (from ctpk in qlst.ChiTietPhieuKhos
                           join sp in qlst.SanPhams on ctpk.MaSP equals sp.MaSP
                           join ncc in qlst.NhaCungCaps on sp.MaNCC equals ncc.MaNCC
                           where ctpk.MaPK == maPK
                           select new ListChiTietPhieuKho
                           {
                               TenSP = sp.TenSP,
                               SoLuong = ctpk.SoLuong,
                               DonGia = ctpk.DonGia,
                               ThanhTien = ctpk.ThanhTien
                           }).ToList();

            return details;
        }
    }
}
