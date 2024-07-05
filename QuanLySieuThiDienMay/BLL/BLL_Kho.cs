using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class BLL_Kho
    {
        QLSieuThiDataContext qlst = new QLSieuThiDataContext()
        {
            CommandTimeout = 300 
        };

        public List<Kho> getKho()
        {
            return qlst.Khos.ToList();
        }

        public void getKhos(DataGridView dgv)
        {
            var kq = from k in qlst.Khos
                     select new
                     {
                         k.MaKho,
                         k.TenKho,
                         k.DiaChi,
                         k.MoTa,
                         k.Lat,
                         k.Lon
                     };
            dgv.DataSource = kq;
        }

        public Kho GetKhoByMaKho(int maKho)
        {
            return qlst.Khos.SingleOrDefault(q => q.MaKho == maKho);
        }

        public void ThemKho(string tenKho, string diaChi, string moTa, string lat, string lon)
        {
            Kho newKho = new Kho
            {
                TenKho = tenKho,
                DiaChi = diaChi,
                MoTa = moTa,
                Lat = lat,
                Lon = lon
            };
            qlst.Khos.InsertOnSubmit(newKho);
            qlst.SubmitChanges();
        }

        public void CapNhatKho(int maKho, string tenKho, string diaChi, string moTa, string lat, string lon)
        {
            var kho = qlst.Khos.SingleOrDefault(k=> k.MaKho == maKho);
            kho.TenKho = tenKho;
            kho.DiaChi = diaChi;
            kho.MoTa = moTa;
            kho.Lat = lat;
            kho.Lon = lon;
            qlst.SubmitChanges();
        }

        public List<ListFrmKho> Search_ListKho_TenKho(string tenKho)
        {
            var searchList = from kh in qlst.Khos
                             where kh.TenKho.Contains(tenKho)
                             select new ListFrmKho
                             {
                                 MaKho = kh.MaKho,
                                 TenKho = kh.TenKho,
                                 DiaChi = kh.DiaChi,
                                 MoTa = kh.MoTa,
                                 Lat = kh.Lat,
                                 Lon = kh.Lon
                             };
            return searchList.ToList();
        }
    }
}
