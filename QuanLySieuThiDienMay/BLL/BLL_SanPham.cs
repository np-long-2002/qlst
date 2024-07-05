using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class BLL_SanPham
    {
        public BLL_SanPham()
        {
        }

        QLSieuThiDataContext qlst = new QLSieuThiDataContext();

        public List<SanPham> getListSP(int masp)
        {
            var kq = qlst.SanPhams.Where(o => o.MaSP.Equals(masp));
            return kq.ToList();
        }

        public SanPham getSP(string masp)
        {
            var kq = from sp in qlst.SanPhams where sp.MaSP.Equals(masp) select sp;
            return kq.FirstOrDefault();
        }

        public SanPham getSL(string masp)
        {
            int maSPInt = int.Parse(masp);
            var kq = qlst.SanPhams.FirstOrDefault(sp => sp.MaSP == maSPInt);
            return kq;
        }

        public void getSPs(DataGridView dgv)
        {
            var kq = from sp in qlst.SanPhams
                     select new
                     {
                         sp.MaSP,
                         sp.TenSP,
                         sp.MoTa,
                         sp.SoLuong,
                         sp.GiaSP
                     };
            dgv.DataSource = kq;
        }

        public List<SanPham> GetSPs()
        {
            return qlst.SanPhams.ToList();
        }

        public SanPham Search_SP(int maSP)
        {
            return qlst.SanPhams.FirstOrDefault(sp => sp.MaSP == maSP);
        }

        public List<ListFrmSP> Search_ListSP_TenSP(string tenSP)
        {
            var searchList = (
                from sp in qlst.SanPhams
                where sp.TenSP.Contains(tenSP)
                select new ListFrmSP
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    MoTa = sp.MoTa,
                    SoLuong = sp.SoLuong,
                    GiaSP = sp.GiaSP
                }).ToList();

            return searchList;
        }

        //public void sort(DataGridView dgv)
        //{
        //    var kq = from sp in qlst.SanPhams
        //             orderby sp.GiaSP descending
        //             select new
        //             {
        //                 MaSP = sp.MaSP,
        //                 TenSP = sp.TenSP,
        //                 MoTa = sp.MoTa,
        //                 SoLuong = sp.SoLuong,
        //                 GiaSP = sp.GiaSP
        //             };
        //    dgv.DataSource = kq;
        //}

        public void sort(DataGridView dgv)
        {
            var listSP = GetSP();
            var sortedList = listSP.OrderByDescending(sp => sp.GiaBan)
                                   .ThenBy(sp => sp.GiaNhap)
                                   .ToList();
            dgv.DataSource = sortedList;
        }

        public List<HinhAnhSanPham> GetImageProduct(int maKho)
        {
            var query = from sp in qlst.SanPhams
                        join ha in qlst.HinhAnhSanPhams on sp.MaSP equals ha.MaSP
                        join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP
                        where ctk.MaKho == maKho && sp.TrangThaiXoa == 1
                        group ha by sp into g
                        select g.FirstOrDefault();

            List<HinhAnhSanPham> images = query.ToList();
            return images;
        }

        public SanPham GetProduct(int imageID, int maKho)
        {
            var query = from sp in qlst.SanPhams
                        join ha in qlst.HinhAnhSanPhams on sp.MaSP equals ha.MaSP
                        join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP
                        where ha.MaHinh == imageID && ctk.MaKho == maKho && sp.TrangThaiXoa == 1
                        select new
                        {
                            sp.MaSP,
                            sp.TenSP,
                            sp.MoTa,
                            SoLuong = ctk.SoLuongTrongKho,
                            sp.GiaSP,
                            sp.NgaySX,
                            sp.MaLoaiSP,
                            sp.MaNCC,
                            sp.TrangThaiXoa
                        };

            var result = query.FirstOrDefault();
            if (result != null)
            {
                return new SanPham
                {
                    MaSP = result.MaSP,
                    TenSP = result.TenSP,
                    MoTa = result.MoTa,
                    SoLuong = result.SoLuong,
                    GiaSP = result.GiaSP,
                    NgaySX = result.NgaySX,
                    MaLoaiSP = result.MaLoaiSP,
                    MaNCC = result.MaNCC,
                    TrangThaiXoa = result.TrangThaiXoa
                };
            }
            else
            {
                return null;
            }
        }
        
        public ListGioHang GetGioHang(int imageID)
        {
            var query = from ha in qlst.HinhAnhSanPhams
                        where ha.MaHinh == imageID
                        join sp in qlst.SanPhams on ha.MaSP equals sp.MaSP
                        select new ListGioHang
                        {
                            MaSP = sp.MaSP,
                            TenSP = sp.TenSP,
                            GiaSP = sp.GiaSP,
                            SoLuong = sp.SoLuong
                        };

            ListGioHang productInfo = query.FirstOrDefault();
            return productInfo;
        }

        public SanPham SearchNameProduct(string name, int imageID, int maKho)
        {
            var query = from sp in qlst.SanPhams
                        join ha in qlst.HinhAnhSanPhams on sp.MaSP equals ha.MaSP
                        join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP
                        where ha.MaHinh == imageID && sp.TenSP.ToLower().Contains(name.ToLower())
                              && ctk.MaKho == maKho && sp.TrangThaiXoa == 1
                        select new
                        {
                            sp.MaSP,
                            sp.TenSP,
                            sp.MoTa,
                            SoLuong = ctk.SoLuongTrongKho,
                            sp.GiaSP,
                            sp.NgaySX,
                            sp.MaLoaiSP,
                            sp.MaNCC,
                            sp.TrangThaiXoa
                        };

            var result = query.FirstOrDefault();
            if (result != null)
            {
                return new SanPham
                {
                    MaSP = result.MaSP,
                    TenSP = result.TenSP,
                    MoTa = result.MoTa,
                    SoLuong = result.SoLuong,
                    GiaSP = result.GiaSP,
                    NgaySX = result.NgaySX,
                    MaLoaiSP = result.MaLoaiSP,
                    MaNCC = result.MaNCC,
                    TrangThaiXoa = result.TrangThaiXoa
                };
            }
            else
            {
                return null;
            }
        }

        public int GetRemainingQuantity(int productId, List<ListGioHang> gioHangs, int maKho)
        {
            int remainingQuantity = 0;

            // Lấy số lượng sản phẩm hiện tại trong kho
            //SanPham product = qlst.SanPhams.FirstOrDefault(sp => sp.MaSP == productId);
            //if (product != null)
            //{
            //    int currentQuantity = product.SoLuong;

            //    // Tính số lượng sản phẩm đã được thêm vào giỏ hàng
            //    int productQuantityInCart = gioHangs.Where(p => p.MaSP == productId).Sum(p => p.SoLuong);

            //    // Tính số lượng sản phẩm còn lại sau khi xoá khỏi giỏ hàng
            //    remainingQuantity = currentQuantity - productQuantityInCart;
            //}

            ChiTietKho product = qlst.ChiTietKhos.FirstOrDefault(sp => sp.MaSP == productId && sp.MaKho == maKho);
            if (product != null)
            {
                int currentQuantity = product.SoLuongTrongKho;
                int productQuantityInCart = gioHangs.Where(p => p.MaSP == productId).Sum(p => p.SoLuong);
                remainingQuantity = currentQuantity - productQuantityInCart;
            }

            return remainingQuantity;
        }

        public void UpdateSoLuong(int maSP, int soLuongMoi)
        {
            SanPham sp = qlst.SanPhams.FirstOrDefault(q => q.MaSP == maSP);
            if (sp != null)
            {
                sp.SoLuong = soLuongMoi;
            }
            qlst.SubmitChanges();
        }

        public void UpdateQuantityInCart(int maSP, int soLuong, int maKho)
        {
            //string query = "UPDATE ChiTietKho SET SoLuongTrongKho = SoLuongTrongKho - {0} WHERE MaSP = {1} AND MaKho = {2}";

            //// Thực thi câu lệnh SQL với các tham số
            //qlst.ExecuteCommand(query, soLuong, maSP, maKho);

            ChiTietKho ctk = qlst.ChiTietKhos.FirstOrDefault(q => q.MaSP == maSP && q.MaKho == maKho);
            SanPham sp = qlst.SanPhams.FirstOrDefault(q => q.MaSP == maSP);

            if (ctk != null && sp != null)
            {
                ctk.SoLuongTrongKho -= soLuong;
                sp.SoLuong -= soLuong;
                qlst.SubmitChanges();
            }
        }

        public void RestoreQuantityInCart(int maSP, int soLuong, int maKho)
        {
            ChiTietKho ctk = qlst.ChiTietKhos.FirstOrDefault(q => q.MaSP == maSP && q.MaKho == maKho);
            SanPham sp = qlst.SanPhams.FirstOrDefault(q => q.MaSP == maSP);

            if (ctk != null && sp != null)
            {
                ctk.SoLuongTrongKho += soLuong;
                sp.SoLuong += soLuong;
                qlst.SubmitChanges();
            }
        }

        public void UpdateQuantityAfterEdit(int maSP, int soLuongCu, int soLuongMoi, int maKho)
        {
            ChiTietKho ctk = qlst.ChiTietKhos.FirstOrDefault(q => q.MaSP == maSP && q.MaKho == maKho);
            SanPham sp = qlst.SanPhams.FirstOrDefault(q => q.MaSP == maSP);

            if (ctk != null && sp != null)
            {
                ctk.SoLuongTrongKho += soLuongCu - soLuongMoi;
                sp.SoLuong += soLuongCu - soLuongMoi;
                qlst.SubmitChanges();
            }
        }

        public SanPham GetProductByMaSP(int maSP, int maKho)
        {
            var query = from sp in qlst.SanPhams
                        join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP
                        where sp.MaSP == maSP && ctk.MaKho == maKho && sp.TrangThaiXoa == 1
                        select new
                        {
                            sp.MaSP,
                            sp.TenSP,
                            sp.MoTa,
                            SoLuong = ctk.SoLuongTrongKho,
                            sp.GiaSP,
                            sp.NgaySX,
                            sp.MaLoaiSP,
                            sp.MaNCC,
                            sp.TrangThaiXoa
                        };

            var result = query.FirstOrDefault();
            if (result != null)
            {
                return new SanPham
                {
                    MaSP = result.MaSP,
                    TenSP = result.TenSP,
                    MoTa = result.MoTa,
                    SoLuong = result.SoLuong,
                    GiaSP = result.GiaSP,
                    NgaySX = result.NgaySX,
                    MaLoaiSP = result.MaLoaiSP,
                    MaNCC = result.MaNCC,
                    TrangThaiXoa = result.TrangThaiXoa
                };
            }
            else
            {
                return null;
            }
        }

        public List<HinhAnhSanPham> GetImageProductByNhaCungCapAndLoaiSanPham(int maNCC, int maLSP, int maKho)
        {
            var query = from sp in qlst.SanPhams
                        join ha in qlst.HinhAnhSanPhams on sp.MaSP equals ha.MaSP
                        join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP
                        where sp.MaNCC == maNCC && sp.MaLoaiSP == maLSP && ctk.MaKho == maKho && sp.TrangThaiXoa == 1
                        group ha by sp.MaSP into g
                        select g.FirstOrDefault();

            List<HinhAnhSanPham> images = query.ToList();
            return images;
        }

        public List<HinhAnhSanPham> GetImageProductByNhaCungCap(int maNCC, int maKho)
        {
            var query = from sp in qlst.SanPhams
                        join ha in qlst.HinhAnhSanPhams on sp.MaSP equals ha.MaSP
                        join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP
                        where sp.MaNCC == maNCC && ctk.MaKho == maKho && sp.TrangThaiXoa == 1
                        group ha by sp.MaSP into g
                        select g.FirstOrDefault();

            List<HinhAnhSanPham> images = query.ToList();
            return images;
        }

        public List<HinhAnhSanPham> GetImageProductByLoaiSanPham(int maLSP, int maKho)
        {
            var query = from sp in qlst.SanPhams
                        join ha in qlst.HinhAnhSanPhams on sp.MaSP equals ha.MaSP
                        join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP
                        where sp.MaLoaiSP == maLSP && ctk.MaKho == maKho && sp.TrangThaiXoa == 1
                        group ha by sp.MaSP into g
                        select g.FirstOrDefault();

            List<HinhAnhSanPham> images = query.ToList();
            return images;
        }

        public List<ListFrmSP_Kho> GetSP_Kho(int maKho)
        {
            var query = from sp in qlst.SanPhams
                        join ctpk in (
                            from ctpkSub in qlst.ChiTietPhieuKhos
                            join pk in qlst.PhieuKhos on ctpkSub.MaPK equals pk.MaPK
							where pk.MaLPK == 1
							group new { ctpkSub, pk } by ctpkSub.MaSP into g
                            select g.OrderByDescending(x => x.pk.MaPK)
                                       .FirstOrDefault().ctpkSub
                        ) on sp.MaSP equals ctpk.MaSP into ctpkGroup
                        from ctpkItem in ctpkGroup.DefaultIfEmpty()
                        join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP into ctkGroup
                        from ctkItem in ctkGroup.DefaultIfEmpty()
                        where ctkItem != null && ctkItem.MaKho == maKho && sp.TrangThaiXoa == 1
                        select new ListFrmSP_Kho
                        {
                            MaSP = sp.MaSP,
                            TenSP = sp.TenSP,
                            GiaNhap = ctpkItem != null ? ctpkItem.DonGia : 0,
                            GiaBan = sp.GiaSP,
                            SoLuongNhap = ctkItem != null ? ctkItem.SoLuongTrongKho : 0,
                            SoLuongTonKho = sp.SoLuong
                        };
            return query.ToList();
        }

        public List<ListSP> GetSP()
        {
            var query = from sp in qlst.SanPhams
                        join ctpk in (
                            from ctpkSub in qlst.ChiTietPhieuKhos
                            join pk in qlst.PhieuKhos on ctpkSub.MaPK equals pk.MaPK
							where pk.MaLPK == 1
							group new { ctpkSub, pk } by ctpkSub.MaSP into g
                            select g.OrderByDescending(x => x.pk.MaPK)
                                       .FirstOrDefault().ctpkSub
                        ) on sp.MaSP equals ctpk.MaSP into ctpkGroup
                        from ctpkItem in ctpkGroup.DefaultIfEmpty()
                        where sp.TrangThaiXoa == 1
                        select new ListSP
                        {
                            MaSP = sp.MaSP,
                            TenSP = sp.TenSP,
                            GiaNhap = ctpkItem != null ? ctpkItem.DonGia : 0,
                            GiaBan = sp.GiaSP,
                            SoLuongTonKho = sp.SoLuong
                        };
            return query.ToList();
        }

        public List<ListFrmSP_IsDelete> GetSP_IsDeleted()
        {
            var query = from sp in qlst.SanPhams
                        where sp.TrangThaiXoa == 0
                        select new ListFrmSP_IsDelete
                        {
                            MaSP = sp.MaSP,
                            TenSP = sp.TenSP,
                        };
            return query.ToList();
        }

        public List<ListFrmSP_Kho> Search_ListSP_TenSP_1(string tenSP, int maKho)
        {
            var searchList = (
                from sp in qlst.SanPhams
                join ctpk in qlst.ChiTietPhieuKhos on sp.MaSP equals ctpk.MaSP into ctpkGroup
                from ctpkItem in ctpkGroup.DefaultIfEmpty()
                join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP into ctkGroup
                from ctkItem in ctkGroup.DefaultIfEmpty()
                where sp.TenSP.Contains(tenSP) && ctkItem.MaKho == maKho && sp.TrangThaiXoa == 1

				select new ListFrmSP_Kho
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    GiaNhap = ctkItem != null ? (float)(ctpkItem != null ? ctpkItem.DonGia : 0) : 0,
                    GiaBan = sp.GiaSP,
                    SoLuongNhap = ctpkItem != null ? ctpkItem.SoLuong : 0,
                    SoLuongTonKho = sp.SoLuong
                }).ToList();

            return searchList;
        }

        public List<ListFrmSP_Kho> Search_ListSP_TenSP_SP(string tenSP)
        {
            var searchList = (
                from sp in qlst.SanPhams
                join ctpk in qlst.ChiTietPhieuKhos on sp.MaSP equals ctpk.MaSP into ctpkGroup
                from ctpkItem in ctpkGroup.DefaultIfEmpty()
                join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP into ctkGroup
                from ctkItem in ctkGroup.DefaultIfEmpty()
                where sp.TenSP.Contains(tenSP) && sp.TrangThaiXoa == 1

                select new ListFrmSP_Kho
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    GiaNhap = ctkItem != null ? (float)(ctpkItem != null ? ctpkItem.DonGia : 0) : 0,
                    GiaBan = sp.GiaSP,
                    SoLuongNhap = ctpkItem != null ? ctpkItem.SoLuong : 0,
                    SoLuongTonKho = sp.SoLuong
                }).ToList();

            return searchList;
        }

        public SanPham AddProduct(string tensp, string mota, DateTime ngsx, int maloai, int mancc)
        {
            SanPham newproduct = new SanPham
            {
                TenSP = tensp,
                MoTa = mota,
                SoLuong = 0,
                //SoLuong = 1,
                GiaSP = 0,
                NgaySX = ngsx,
                MaLoaiSP = maloai,
                MaNCC = mancc,
                TrangThaiXoa = 1,
            };
            qlst.SanPhams.InsertOnSubmit(newproduct);
            qlst.SubmitChanges();

            return newproduct;
        }

        public ChiTietKho AddWarehouse(int maKho, int maSP)
        {
            var kho = qlst.Khos.FirstOrDefault(k => k.MaKho == maKho);
            if (kho == null)
            {
                throw new Exception($"Không tìm thấy kho với mã kho {maKho}");
            }
            else
            {
                ChiTietKho newwarehouse = new ChiTietKho
                {
                    MaKho = maKho,
                    MaSP = maSP,
                    SoLuongTrongKho = 0,
                };
                qlst.ChiTietKhos.InsertOnSubmit(newwarehouse);
                qlst.SubmitChanges();

                return newwarehouse;
            }

        }

        public ListFrmTTSP GetProductDetails(int maSP)
        {
            var product = qlst.SanPhams.FirstOrDefault(p => p.MaSP == maSP);
            if (product != null)
            {
                ListFrmTTSP productInfo = new ListFrmTTSP
                {
                    TenSP = product.TenSP,
                    Mota = product.MoTa,
                    GiaSP = product.GiaSP,
                    NgaySX = product.NgaySX,
                    MaLoaiSP = product.MaLoaiSP,
                    MaNCC = product.MaNCC,
                    Images = new List<string>()
                };

                var images = (from ha in qlst.HinhAnhSanPhams
                              where ha.MaSP == maSP
                              select ha.AnhSanPham).ToList();
                foreach (var image in images)
                {
                    productInfo.Images.Add(image);
                }

                return productInfo;
            }
            return null;
        }

        public void UpdateProduct(int maSP, string tenSP, string moTa, int giaSP, DateTime ngaySX, int maLoaiSP, int maNCC)
        {
            var product = qlst.SanPhams.FirstOrDefault(p => p.MaSP == maSP);
            if (product != null)
            {
                product.TenSP = tenSP;
                product.MoTa = moTa;
                product.GiaSP = giaSP;
                product.NgaySX = ngaySX;
                product.MaLoaiSP = maLoaiSP;
                product.MaNCC = maNCC;
                qlst.SubmitChanges();
            }
        }

        public void DeleteProduct(int masp)
        {
            var product = qlst.SanPhams.FirstOrDefault(p => p.MaSP == masp);

            if (product != null)
            {
                product.TrangThaiXoa = 0;
                qlst.SubmitChanges();
            }
        }

        public void RestoreProduct(int masp)
        {
            var product = qlst.SanPhams.FirstOrDefault(p => p.MaSP == masp);

            if (product != null)
            {
                product.TrangThaiXoa = 1;
                qlst.SubmitChanges();
            }
        }

        public List<ListFrmSP_IsDelete> GetSP_NCC(int maNCC)
        {
            var query = from sp in qlst.SanPhams
                        where sp.MaNCC == maNCC && sp.TrangThaiXoa == 1
                        select new ListFrmSP_IsDelete
                        {
                            MaSP = sp.MaSP,
                            TenSP = sp.TenSP
                        };
            return query.ToList();
        }

        public List<ListNCC_SP> GetSP_NCC_Kho(int maNCC, int maKho)
        {
            var query = from sp in qlst.SanPhams
                        join ctk in qlst.ChiTietKhos on sp.MaSP equals ctk.MaSP
                        where sp.MaNCC == maNCC && ctk.MaKho == maKho && sp.TrangThaiXoa == 1
                        select new ListNCC_SP
                        {
                            MaSP = sp.MaSP,
                            TenSP = sp.TenSP,
                            GiaBan = sp.GiaSP
                        };
            return query.ToList();
        }

        public List<ListFrmSP_IsDelete> QR_SP()
        {
            var sp = (from s in qlst.SanPhams
                      where s.TrangThaiXoa == 1
                      select new ListFrmSP_IsDelete
                      {
                          MaSP = s.MaSP,
                          TenSP = s.TenSP,
                      }).ToList();
            return sp;
        }

        public string GetTenSPByMaSP(int maSP)
        {
            var product = qlst.SanPhams.SingleOrDefault(sp => sp.MaSP == maSP);
            return product != null ? product.TenSP : null;
        }

        public List<SanPham> GetSanPham()
        {
            var query = from sp in qlst.SanPhams
                        select sp;

            return query.ToList();
        }

        public SanPham GetNCCSP(int masp)
        {
            return qlst.SanPhams.FirstOrDefault(sp => sp.MaSP == masp);
        }
    }
}
