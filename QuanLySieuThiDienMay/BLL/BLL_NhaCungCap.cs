using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class BLL_NhaCungCap
    {
        public BLL_NhaCungCap()
        {
        }

        QLSieuThiDataContext qlst = new QLSieuThiDataContext();

        public List<NhaCungCap> getListNCC(int mancc)
        {
            var kq = qlst.NhaCungCaps.Where(o => o.MaNCC.Equals(mancc));
            return kq.ToList();
        }

        public NhaCungCap getNCC(string mancc)
        {
            var kq = from ncc in qlst.NhaCungCaps where ncc.MaNCC.Equals(mancc) select ncc;
            return kq.FirstOrDefault();
        }

        public List<NhaCungCap> getNhaCungCap() 
        {
            return qlst.NhaCungCaps.ToList();
        }

        public void getNCCs(DataGridView dgv)
        {
            var kq = from ncc in qlst.NhaCungCaps
                     where ncc.TrangThaiXoa == 1
                     select new
                     {
                         ncc.MaNCC,
                         ncc.TenNCC,
                         ncc.DiaChi,
                         ncc.SDT,
                         ncc.Email
                     };
            dgv.DataSource = kq;
        }

        public NhaCungCap Search_NCC(int mancc)
        {
            return qlst.NhaCungCaps.FirstOrDefault(ncc => ncc.MaNCC == mancc);
        }

        public List<ListFrmNCC> Search_ListNCC_TenNCC(string tenncc)
        {
            var searchList = (
                from ncc in qlst.NhaCungCaps
                where ncc.TenNCC.Contains(tenncc) && ncc.TrangThaiXoa == 1
                select new ListFrmNCC
                {
                    MaNCC = ncc.MaNCC, 
                    TenNCC = ncc.TenNCC,
                    DiaChi = ncc.DiaChi,
                    SDT = ncc.SDT,
                    Email = ncc.Email
                }).ToList();

            return searchList;
        }

        public List<ListFrmNCC> Search_ListNCC_DC(string dchi)
        {
            var searchList = (
                from ncc in qlst.NhaCungCaps
                where ncc.DiaChi.Contains(dchi) && ncc.TrangThaiXoa == 1
                select new ListFrmNCC
                {
                    MaNCC = ncc.MaNCC,
                    TenNCC = ncc.TenNCC,
                    DiaChi = ncc.DiaChi,
                    SDT = ncc.SDT,
                    Email = ncc.Email
                }).ToList();

            return searchList;
        }

        public void sort(DataGridView dgv)
        {
            var kq = from ncc in qlst.NhaCungCaps
                     orderby ncc.MaNCC descending
                     where ncc.TrangThaiXoa == 1
                     select new
                     {
                         MaNCC = ncc.MaNCC,
                         TenNCC = ncc.TenNCC,
                         DiaChi = ncc.DiaChi,
                         SDT = ncc.SDT,
                         Email = ncc.Email
                     };
            dgv.DataSource = kq;
        }

        public NhaCungCap GetNhaCungCapByMaNCC(int maNCC)
        {
            return qlst.NhaCungCaps.SingleOrDefault(ncc => ncc.MaNCC == maNCC);
        }

        public void ThemNhaCungCap(string tenNCC, string diaChi, string sdt, string email)
        {
            NhaCungCap newNCC = new NhaCungCap
            {
                TenNCC = tenNCC,
                DiaChi = diaChi,
                SDT = sdt,
                Email = email,
                TrangThaiXoa = 1
            };
            qlst.NhaCungCaps.InsertOnSubmit(newNCC);
            qlst.SubmitChanges();
        }

        public void CapNhatNhaCungCap(int maNCC, string tenNCC, string diaChi, string sdt, string email)
        {
            var nhaCungCap = qlst.NhaCungCaps.SingleOrDefault(ncc => ncc.MaNCC == maNCC);
            nhaCungCap.TenNCC = tenNCC;
            nhaCungCap.DiaChi = diaChi;
            nhaCungCap.SDT = sdt;
            nhaCungCap.Email = email;
            qlst.SubmitChanges();
        }

        public string GetTenNhaCungCap(int maNCC)
        {
            var nhaCungCap = qlst.NhaCungCaps.FirstOrDefault(ncc => ncc.MaNCC == maNCC);
            if (nhaCungCap != null)
            {
                return nhaCungCap.TenNCC;
            }
            return "Chưa thấy nhà cung cấp";
        }

        public List<ListFrmThongKeSPDT> ThongKe(string filterType, DateTime? selectedDate)
        {
            var query = qlst.NhaCungCaps.AsQueryable();

            if (selectedDate.HasValue)
            {
                if (filterType == "Quý")
                {
                    int quarter = (selectedDate.Value.Month - 1) / 3 + 1;
                    return query.Select(s => new ListFrmThongKeSPDT
                    {
                        TenNCC = s.TenNCC,
                        SoLuong = s.SanPhams
                                    .SelectMany(p => p.ChiTietHoaDonDoiTras)
                                    .Join(qlst.HoaDonDoiTras,
                                          ct => ct.MaHDDT,
                                          hd => hd.MaHDDT,
                                          (ct, hd) => hd)
                                    .Where(hd => (hd.NgayDoiTra.Month - 1) / 3 + 1 == quarter)
                                    .Count()
                    }).ToList();
                }
                else if (filterType == "Tháng")
                {
                    int month = selectedDate.Value.Month;
                    return query.Select(s => new ListFrmThongKeSPDT
                    {
                        TenNCC = s.TenNCC,
                        SoLuong = s.SanPhams
                                    .SelectMany(p => p.ChiTietHoaDonDoiTras)
                                    .Join(qlst.HoaDonDoiTras,
                                          ct => ct.MaHDDT,
                                          hd => hd.MaHDDT,
                                          (ct, hd) => hd)
                                    .Where(hd => hd.NgayDoiTra.Month == month)
                                    .Count()
                    }).ToList();
                }
                else if (filterType == "Năm")
                {
                    int year = selectedDate.Value.Year;
                    return query.Select(s => new ListFrmThongKeSPDT
                    {
                        TenNCC = s.TenNCC,
                        SoLuong = s.SanPhams
                                    .SelectMany(p => p.ChiTietHoaDonDoiTras)
                                    .Join(qlst.HoaDonDoiTras,
                                          ct => ct.MaHDDT,
                                          hd => hd.MaHDDT,
                                          (ct, hd) => hd)
                                    .Where(hd => hd.NgayDoiTra.Year == year)
                                    .Count()
                    }).ToList();
                }
                else if (filterType == "Ngày")
                {
                    DateTime date = selectedDate.Value.Date;
                    return query.Select(s => new ListFrmThongKeSPDT
                    {
                        TenNCC = s.TenNCC,
                        SoLuong = s.SanPhams
                                    .SelectMany(p => p.ChiTietHoaDonDoiTras)
                                    .Join(qlst.HoaDonDoiTras,
                                          ct => ct.MaHDDT,
                                          hd => hd.MaHDDT,
                                          (ct, hd) => hd)
                                    .Where(hd => hd.NgayDoiTra.Date == date)
                                    .Count()
                    }).ToList();
                }
            }
            return query.Select(s => new ListFrmThongKeSPDT
            {
                TenNCC = s.TenNCC,
                SoLuong = s.SanPhams.SelectMany(p => p.ChiTietHoaDonDoiTras).Count()
            }).ToList();
        }

        public bool DeleteNCC(int mancc)
        {
            var ncc = qlst.NhaCungCaps.FirstOrDefault(p => p.MaNCC == mancc);

            if (ncc != null)
            {
                ncc.TrangThaiXoa = 0;
                qlst.SubmitChanges();
                return true;
            }
            return false;
        }

        public void RestoreNCC(int mancc)
        {
            var ncc = qlst.NhaCungCaps.FirstOrDefault(p => p.MaNCC == mancc);

            if (ncc != null)
            {
                ncc.TrangThaiXoa = 1;
                qlst.SubmitChanges();
            }
        }

        public List<ListFrmNCC> GetListNCC_IsDeleted()
        {
            var listncc = (
                from ncc in qlst.NhaCungCaps
                where ncc.TrangThaiXoa == 0
                select new ListFrmNCC
                {
                    MaNCC = ncc.MaNCC,
                    TenNCC = ncc.TenNCC,
                    DiaChi = ncc.DiaChi,
                    SDT = ncc.SDT,
                    Email = ncc.Email,
                }).ToList();
            return listncc;
        }

        public List<NhaCungCap> GetNCC_Kho(int maKho)
        {
            var nhaCungCapList = (from ncc in qlst.NhaCungCaps
                                  join sp in qlst.SanPhams on ncc.MaNCC equals sp.MaNCC
                                  join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP
                                  where ctk.MaKho == maKho
                                  select ncc).Distinct().ToList();

            return nhaCungCapList;
        }

        public NhaCungCap GetNhaCungCap(int maNCC)
        {
            return qlst.NhaCungCaps.FirstOrDefault(ncc => ncc.MaNCC == maNCC);
        }
    }
}
