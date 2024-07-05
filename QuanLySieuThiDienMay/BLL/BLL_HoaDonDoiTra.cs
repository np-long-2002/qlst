using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_HoaDonDoiTra
    {
        QLSieuThiDataContext qlst = new QLSieuThiDataContext();
        public List<ListFrmHDDT> Get_HDDT()
        {
            var hddt = qlst.HoaDonDoiTras
                .Select(h => new ListFrmHDDT
                {
                    MaHDDT = h.MaHDDT,
                    MaHDD = h.MaHD,
                    NgayDoiTra = h.NgayDoiTra,
                    TrangThai = h.TrangThai
                }).ToList();
            return hddt;
        }

        public List<ListFrmHDDT> Get_HDDT_HD(int mahd)
        {
            var hddt = (from ct in qlst.HoaDonDoiTras
                        where ct.MaHD == mahd
                        select new ListFrmHDDT
                        {
                            MaHDDT = ct.MaHDDT,
                            MaHDD = ct.MaHD,
                            NgayDoiTra = ct.NgayDoiTra,
                            TrangThai = ct.TrangThai
                        }).ToList();
            return hddt;
        }

        public int AddHDDT(int maHD)
        {
            HoaDonDoiTra hoaDonDT = new HoaDonDoiTra();
            hoaDonDT.MaHD = maHD;
            hoaDonDT.NgayDoiTra = DateTime.Now.Date;
            //hoaDonDT.TrangThai = "Đợi phản hồi từ nhà cung cấp";
            hoaDonDT.TrangThai = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes("Wait for processing"));
            qlst.HoaDonDoiTras.InsertOnSubmit(hoaDonDT);
            qlst.SubmitChanges();
            return hoaDonDT.MaHDDT;
        }

        public bool DeleteHDDT(int mahddt)
        {
            HoaDonDoiTra hddt = qlst.HoaDonDoiTras.SingleOrDefault(hd => hd.MaHDDT == mahddt);
            if (hddt == null)
            {
                return false;
            }
            foreach (ChiTietHoaDonDoiTra ct in hddt.ChiTietHoaDonDoiTras)
            {
                qlst.ChiTietHoaDonDoiTras.DeleteOnSubmit(ct);
            }
            qlst.HoaDonDoiTras.DeleteOnSubmit(hddt);
            qlst.SubmitChanges();
            return true;
        }

        public HoaDonDoiTra GetHDDTById(int maHDDT)
        {
            return qlst.HoaDonDoiTras.SingleOrDefault(h => h.MaHDDT == maHDDT);
        }

        public List<ListFrmHDDT> Get_HDDTTheoThang(int thang)
        {
            int currentYear = DateTime.Now.Year;
            var hddt = qlst.HoaDonDoiTras
                .Where(h => h.NgayDoiTra.Month == thang && h.NgayDoiTra.Year == currentYear)
                .Select(h => new ListFrmHDDT
                {
                    MaHDDT = h.MaHDDT,
                    MaHDD = h.MaHD,
                    NgayDoiTra = h.NgayDoiTra,
                    TrangThai = h.TrangThai
                }).ToList();
            return hddt;
        }

        public List<ListFrmCTHDDT> Get_HDDTDetails(int maHDDT)
        {
            var details = qlst.ChiTietHoaDonDoiTras
                .Where(ct => ct.MaHDDT == maHDDT)
                .Select(ct => new ListFrmCTHDDT
                {
                    MaSP = ct.MaSP,
                    SoLuong = ct.SoLuong,
                    LyDo = ct.LyDo
                }).ToList();

            return details;
        }

        public int GetMaHD(int maHDDT)
        {
            var hddt = qlst.HoaDonDoiTras.SingleOrDefault(h => h.MaHDDT == maHDDT);
            if (hddt != null)
            {
                return hddt.MaHD;
            }
            return -1;
        }

        public string GetTenKhachHang(int maHD)
        {
            var hoaDon = qlst.HoaDons.SingleOrDefault(h => h.MaHD == maHD);
            if (hoaDon != null)
            {
                int maKH = hoaDon.MaKH;
                var khachHang = qlst.KhachHangs.SingleOrDefault(kh => kh.MaKH == maKH);
                if (khachHang != null)
                {
                    return khachHang.TenKH;
                }
            }
            return null;
        }

        public string GetTenNhanVien(int maHD)
        {
            var hoaDon = qlst.HoaDons.SingleOrDefault(h => h.MaHD == maHD);
            if (hoaDon != null)
            {
                int maNV = hoaDon.MaNV;
                var nhanVien = qlst.NhanViens.SingleOrDefault(nv => nv.MaNV == maNV);
                if (nhanVien != null)
                {
                    return nhanVien.TenNV;
                }
            }
            return null;
        }
    }
}
