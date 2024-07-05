using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_PhieuKho
    {
        QLSieuThiDataContext qlst = new QLSieuThiDataContext();

        //public List<PhieuKho> Get_PhieuKho_Kho(int maKho)
        //{
        //    var query = qlst.PhieuKhos.Where(pk => pk.MaKho == maKho).ToList();
        //    return query;
        //}

        public List<ListPhieuKho> Get_PhieuKho_Kho(int maKho)
        {
            var query = from pk in qlst.PhieuKhos
                        join lpk in qlst.LoaiPhieuKhos on pk.MaLPK equals lpk.MaLPK
                        join nv in qlst.NhanViens on pk.MaNV equals nv.MaNV
                        where pk.MaKho == maKho
                        select new ListPhieuKho
                        {
                            MaPK = pk.MaPK,
                            TenLoaiPK = lpk.MoTa,
                            Mota = pk.MoTa,
                            NgayNhapXuatKho = pk.NgayNhapXuatKho,
                            TongTien = pk.TongTien,
                            TenNhanVien = nv.TenNV,
                            MaKho = pk.MaKho
                        };

            return query.ToList();
        }

        public int AddPhieuKho(int manv, int makho)
        {
            PhieuKho pk = new PhieuKho();
            pk.MaLPK = 1;
            pk.MoTa = "Đơn nhập hàng";
            pk.NgayNhapXuatKho = DateTime.Now.Date;
            pk.TongTien = 0;
            pk.MaNV = manv;
            pk.MaKho = makho;
            qlst.PhieuKhos.InsertOnSubmit(pk);
            qlst.SubmitChanges();
            return pk.MaPK;
        }

        public string GetTenNhanVien(int maPK)
        {
            var phieuKho = qlst.PhieuKhos.SingleOrDefault(h => h.MaPK == maPK);
            if (phieuKho != null)
            {
                int maNV = phieuKho.MaNV;
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
