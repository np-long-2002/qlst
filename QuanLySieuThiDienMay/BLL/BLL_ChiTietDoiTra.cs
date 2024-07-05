using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class BLL_ChiTietDoiTra
    {
        QLSieuThiDataContext qlst = new QLSieuThiDataContext();
        public List<ListFrmCTHDDT> GetListCTDT(int maHDDT)
        {
            var cthddt = (from ct in qlst.ChiTietHoaDonDoiTras
                          join sp in qlst.SanPhams on ct.MaSP equals sp.MaSP
                          where ct.MaHDDT == maHDDT
                          select new ListFrmCTHDDT
                          {
                              MaSP = ct.MaSP,
                              TenSP = sp.TenSP,
                              SoLuong = ct.SoLuong,
                              LyDo = ct.LyDo
                          }).ToList();
            return cthddt;
        }

        public void AddCTHDDT(int maHDDT, int maSP,int soLuong, string lydo)
        {
            try
            {
				if (!qlst.ChiTietHoaDonDoiTras.Any(ct => ct.MaHDDT == maHDDT && ct.MaSP == maSP))
				{
					ChiTietHoaDonDoiTra cthddt = new ChiTietHoaDonDoiTra();
					cthddt.MaHDDT = maHDDT;
					cthddt.MaSP = maSP;
					cthddt.SoLuong = soLuong;
					cthddt.LyDo = lydo;

					qlst.ChiTietHoaDonDoiTras.InsertOnSubmit(cthddt);
					qlst.SubmitChanges();
				}
				else
                {
                    MessageBox.Show("Đã có sản phẩm này!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm trong bảng trên!");
            }
        }

        public List<ListSanPhamTraVe> GetDoiTraNCCThang(int thang)
        {
            int currentYear = DateTime.Now.Year;
            var result = from ctdt in qlst.ChiTietHoaDonDoiTras
                         join hddt in qlst.HoaDonDoiTras on ctdt.MaHDDT equals hddt.MaHDDT
                         join sp in qlst.SanPhams on ctdt.MaSP equals sp.MaSP
                         join ncc in qlst.NhaCungCaps on sp.MaNCC equals ncc.MaNCC
                         where hddt.NgayDoiTra.Month == thang && hddt.NgayDoiTra.Year == currentYear
                         select new ListSanPhamTraVe
                         {
                             TenNCC = ncc.TenNCC,
                             TenSP = sp.TenSP,
                             NgayDoiTra = hddt.NgayDoiTra,
                             SoLuong = (int)ctdt.SoLuong,
                             LyDo = ctdt.LyDo
                         };
            return result.ToList();
        }

        public bool DeleteCTHDDT(int mahddt, int masp)
        {
            ChiTietHoaDonDoiTra cthddt = qlst.ChiTietHoaDonDoiTras.SingleOrDefault(ct => ct.MaHDDT == mahddt && ct.MaSP == masp);

            if (cthddt == null)
            {
                return false;
            }
            qlst.ChiTietHoaDonDoiTras.DeleteOnSubmit(cthddt);
            qlst.SubmitChanges();
            return true;
        }

        public string GetTenSPByMaHDDT(int maHDDT)
        {
            var productName = (from ctdt in qlst.ChiTietHoaDonDoiTras
                               join sp in qlst.SanPhams on ctdt.MaSP equals sp.MaSP
                               where ctdt.MaHDDT == maHDDT
                               select sp.TenSP).FirstOrDefault();
            return productName;
        }

    }
}
