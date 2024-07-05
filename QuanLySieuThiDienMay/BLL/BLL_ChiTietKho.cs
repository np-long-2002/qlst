using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_ChiTietKho
    {
        QLSieuThiDataContext qlst = new QLSieuThiDataContext();
        public void ThemSanPhamVaoKho(int maKho, int maSP)
        {
            ChiTietKho chiTietKho = new ChiTietKho
            {
                MaKho = maKho,
                MaSP = maSP,
                SoLuongTrongKho = 0
            };

            qlst.ChiTietKhos.InsertOnSubmit(chiTietKho);
            qlst.SubmitChanges();
        }
        public bool KiemTraSanPhamDaCoTrongKho(int maKho, int maSP)
        {
            var query = from ctk in qlst.ChiTietKhos
                        where ctk.MaKho == maKho && ctk.MaSP == maSP
                        select ctk;
            return query.Any();
        }
        public int getSLKho(int maSP, int maKho)
        {
            var stockQuantity = (from ct in qlst.ChiTietKhos
                                 where ct.MaSP == maSP && ct.MaKho == maKho
                                 select ct.SoLuongTrongKho).FirstOrDefault();

            return stockQuantity;
        }
	}
}
