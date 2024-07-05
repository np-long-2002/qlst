using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class BLL_ChiTietHoaDon
    {
        QLSieuThiDataContext qlst = new QLSieuThiDataContext();
        public List<ChiTietHoaDon> Get_ChiTietHoaDon()
        {
            return qlst.ChiTietHoaDons.ToList();
        }
        public List<ChiTietHoaDon> getListCTHD(int mahd)
        {
            var kq = qlst.ChiTietHoaDons.Where(o => o.MaHD.Equals(mahd));
            return kq.ToList();
        }

        public ChiTietHoaDon getCTHD(string mahd)
        {
            var kq = from cthd in qlst.ChiTietHoaDons where cthd.MaHD.Equals(mahd) select cthd;
            return kq.FirstOrDefault();
        }

        public void getCTHDs(DataGridView dgv)
        {
            var kq = from cthd in qlst.ChiTietHoaDons
                     join hd in qlst.HoaDons on cthd.MaHD equals hd.MaHD
                     join sp in qlst.SanPhams on cthd.MaSP equals sp.MaSP
                     select new
                     {
                         cthd.MaHD,
                         sp.TenSP,
                         cthd.SoLuong,
                         cthd.DonGia,
                         cthd.ThanhTien
                     };
            dgv.DataSource = kq;
        }

        public void AddCTHD(int maHoaDon, int maSanPham, int soLuong, int donGia, float thanhTien)
        {
            ChiTietHoaDon chiTiet = new ChiTietHoaDon();
            chiTiet.MaHD = maHoaDon;
            chiTiet.MaSP = maSanPham;
            chiTiet.SoLuong = soLuong;
            chiTiet.DonGia = donGia;
            chiTiet.ThanhTien = thanhTien;
            qlst.ChiTietHoaDons.InsertOnSubmit(chiTiet);
            qlst.SubmitChanges();
        }

        public List<ListFrmCTHD> TimKiemChiTietHoaDon(int maHoaDon)
        {
            var query = from cthd in qlst.ChiTietHoaDons
                        join sp in qlst.SanPhams on cthd.MaSP equals sp.MaSP
                        where cthd.MaHD == maHoaDon
                        select new ListFrmCTHD
                        {
                            MaSP = sp.TenSP,
                            SoLuong = cthd.SoLuong,
                            DonGia = cthd.DonGia,
                            ThanhTien = cthd.ThanhTien
                        };

            return query.ToList();
        }

        public List<ListFrmCTHD_SP> GetSP_CTHD(int mahd)
        {
            var listdoitra = (from ct in qlst.ChiTietHoaDons
                              join s in qlst.SanPhams on ct.MaSP equals s.MaSP
                              where ct.MaHD == mahd
                              select new ListFrmCTHD_SP { MaSP = s.MaSP, TenSP = s.TenSP }).ToList();
            return listdoitra;
        }
		public List<ListCTHD_SP> GetSPCTHD(int maHD)
		{

			var products = from cthd in qlst.ChiTietHoaDons
						   join sp in qlst.SanPhams on cthd.MaSP equals sp.MaSP
						   where cthd.MaHD == maHD
						   select new ListCTHD_SP
						   {
							   MaSP = sp.MaSP,
							   TenSP = sp.TenSP,
							   SoLuong = cthd.SoLuong
						   };

			return products.ToList();
		}
	}
}
