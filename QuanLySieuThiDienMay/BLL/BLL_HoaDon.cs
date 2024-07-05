using DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
	public class BLL_HoaDon
	{
		public BLL_HoaDon()
		{
		}

		QLSieuThiDataContext qlst = new QLSieuThiDataContext();

		public List<HoaDon> getListHD(int mahd)
		{
			var kq = qlst.HoaDons.Where(o => o.MaHD.Equals(mahd) && o.TrangThaiXoa == 1);
			return kq.ToList();
		}

		public HoaDon getHD(string mahd)
		{
			var kq = from hd in qlst.HoaDons where hd.MaHD.Equals(mahd) && hd.TrangThaiXoa == 1 select hd;
			return kq.FirstOrDefault();
		}

		public List<ListFrmHD> getHDs(int maKho)
		{
			var kq = (from hd in qlst.HoaDons
					  join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
					  join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
					  join ht in qlst.HinhThucBanHangs on hd.MaHT equals ht.MaHT
					  join tt in qlst.TrangThaiHoaDons on hd.MaTrangThaiHD equals tt.MaTrangThaiHD
					  join kho in qlst.Khos on hd.MaKho equals kho.MaKho
					  where hd.TrangThaiXoa == 1 && hd.MaKho == maKho
					  select new ListFrmHD
					  {
						  MaHD = hd.MaHD,
						  NgayLap = hd.NgayLap,
						  LoaiHinhThuc = ht.LoaiHinhThuc,
						  TongTien = hd.TongTien,
						  MoTaTrangThai = tt.MoTaTrangThai,
						  MaNV = nv.TenNV,
						  MaKH = kh.TenKH
					  }).ToList();
			return kq;
		}


		public List<ListFrmHD_IsDelete> getHDlist()
		{
            //return qlst.HoaDons.Where(q => q.TrangThaiXoa == 0).ToList();
            var query = from hd in qlst.HoaDons
                        where hd.TrangThaiXoa == 0
                        select new ListFrmHD_IsDelete
                        {
                            MaHD = hd.MaHD,
                            NgayLap = hd.NgayLap,
							TongTien = hd.TongTien
                        };
            return query.ToList();
        }

		public List<HoaDon> Get_HD()
		{
			return qlst.HoaDons.Where(q => q.TrangThaiXoa == 1).ToList();
		}

		public HoaDon Search_HD(int maHD)
		{
			return qlst.HoaDons.FirstOrDefault(hd => hd.MaHD == maHD && hd.TrangThaiXoa == 1);
		}

		//public List<ListFrmHD> Search_ListHD_TenNV(string tenNV)
		//{
		//    var searchList = (
		//        from hd in qlst.HoaDons
		//        join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
		//        join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
		//        where nv.TenNV.Contains(tenNV) && hd.TrangThaiXoa == 1
		//        select new ListFrmHD
		//        {
		//            MaHD = hd.MaHD,
		//            NgayLap = hd.NgayLap,
		//            TongTien = hd.TongTien,
		//            //TrangThai = hd.TrangThaiHoaDon,
		//            //DiemTichDuoc = hd.DiemTichDuoc,
		//            MaNV = nv.TenNV,
		//            MaKH = kh.TenKH
		//        }).ToList();

		//    return searchList;
		//}

		//public List<ListFrmHD> Search_ListHD_TenKH(string tenKH)
		//{
		//    var searchList = (
		//        from hd in qlst.HoaDons
		//        join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
		//        join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
		//        where kh.TenKH.Contains(tenKH) && hd.TrangThaiXoa == 1
		//        select new ListFrmHD
		//        {
		//            MaHD = hd.MaHD,
		//            NgayLap = hd.NgayLap,
		//            TongTien = hd.TongTien,
		//            //TrangThai = hd.TrangThai,
		//            //DiemTichDuoc = hd.DiemTichDuoc,
		//            MaNV = nv.TenNV,
		//            MaKH = kh.TenKH
		//        }).ToList();

		//    return searchList;
		//}

		//public List<ListFrmHD> Search_ListHD_TT(string tthai)
		//{
		//    var searchList = (
		//        from hd in qlst.HoaDons
		//        join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
		//        join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
		//        //where hd.TrangThaiHoaDon.Contains(tthai) && hd.TrangThaiXoa == 1
		//        select new ListFrmHD
		//        {
		//            MaHD = hd.MaHD,
		//            NgayLap = hd.NgayLap,
		//            TongTien = hd.TongTien,
		//            //TrangThai = hd.TrangThai,
		//           // DiemTichDuoc = hd.DiemTichDuoc,
		//            MaNV = nv.TenNV,
		//            MaKH = kh.TenKH
		//        }).ToList();

		//    return searchList;
		//}

		//public void sort(DataGridView dgv)
		//{
		//    var kq = from hd in qlst.HoaDons
		//             join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
		//             join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
		//             where hd.TrangThaiXoa == 1
		//             orderby hd.TongTien descending
		//             select new
		//             {
		//                 hd.MaHD,
		//                 hd.NgayLap,
		//                 hd.TongTien,
		//                // hd.TrangThai,
		//                 hd.DiemTichDuoc,
		//                 nv.TenNV,
		//                 kh.TenKH
		//             };
		//    dgv.DataSource = kq;
		//}
		public List<ListFrmHD> Search_ListHD_TenNV(string tenNV, int maKho)
		{
			var searchList = (
				from hd in qlst.HoaDons
				join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
				join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
				join ht in qlst.HinhThucBanHangs on hd.MaHT equals ht.MaHT
				join tt in qlst.TrangThaiHoaDons on hd.MaTrangThaiHD equals tt.MaTrangThaiHD
				where nv.TenNV.Contains(tenNV) && hd.TrangThaiXoa == 1 && hd.MaKho == maKho
				select new ListFrmHD
				{
					MaHD = hd.MaHD,
					NgayLap = hd.NgayLap,
					LoaiHinhThuc = ht.LoaiHinhThuc,
					TongTien = hd.TongTien,
					MoTaTrangThai = tt.MoTaTrangThai,
					MaNV = nv.TenNV,
					MaKH = kh.TenKH
				}).ToList();

			return searchList;
		}

		public List<ListFrmHD> Search_ListHD_TenKH(string tenKH, int maKho)
		{
			var searchList = (
				from hd in qlst.HoaDons
				join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
				join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
				join ht in qlst.HinhThucBanHangs on hd.MaHT equals ht.MaHT
				join tt in qlst.TrangThaiHoaDons on hd.MaTrangThaiHD equals tt.MaTrangThaiHD
				where kh.TenKH.Contains(tenKH) && hd.TrangThaiXoa == 1 && hd.MaKho == maKho
				select new ListFrmHD
				{
					MaHD = hd.MaHD,
					NgayLap = hd.NgayLap,
					LoaiHinhThuc = ht.LoaiHinhThuc,
					TongTien = hd.TongTien,
					MoTaTrangThai = tt.MoTaTrangThai,
					MaNV = nv.TenNV,
					MaKH = kh.TenKH
				}).ToList();

			return searchList;
		}

		public List<ListFrmHD> Search_ListHD_TT(string tthai, int maKho)
		{
			var searchList = (
				from hd in qlst.HoaDons
				join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
				join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
				join ht in qlst.HinhThucBanHangs on hd.MaHT equals ht.MaHT
				join tt in qlst.TrangThaiHoaDons on hd.MaTrangThaiHD equals tt.MaTrangThaiHD
				where tt.MoTaTrangThai.Contains(tthai) && hd.TrangThaiXoa == 1 && hd.MaKho == maKho
				select new ListFrmHD
				{
					MaHD = hd.MaHD,
					NgayLap = hd.NgayLap,
					LoaiHinhThuc = ht.LoaiHinhThuc,
					TongTien = hd.TongTien,
					MoTaTrangThai = tt.MoTaTrangThai,
					MaNV = nv.TenNV,
					MaKH = kh.TenKH
				}).ToList();

			return searchList;
		}

		public List<ListFrmHD> sort(int maKho)
		{
			var sortedList = (from hd in qlst.HoaDons
							  join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
							  join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
							  join ht in qlst.HinhThucBanHangs on hd.MaHT equals ht.MaHT
							  join tt in qlst.TrangThaiHoaDons on hd.MaTrangThaiHD equals tt.MaTrangThaiHD
							  where hd.TrangThaiXoa == 1 && hd.MaKho == maKho
							  orderby hd.TongTien descending
							  select new ListFrmHD
							  {
								  MaHD = hd.MaHD,
								  NgayLap = hd.NgayLap,
								  LoaiHinhThuc = ht.LoaiHinhThuc,
								  TongTien = hd.TongTien,
								  MoTaTrangThai = tt.MoTaTrangThai,
								  MaNV = nv.TenNV,
								  MaKH = kh.TenKH
							  }).ToList();

			return sortedList;
		}

        //public int Add_HD(DateTime NgayLap, float TongTien, string dchi, int manv, int makh, int maKho)
        //{
        //	HoaDon hd = new HoaDon();
        //	hd.NgayLap = NgayLap;
        //	hd.TongTien = TongTien;
        //	hd.DiaChiGiaoHang = dchi;
        //	hd.MaNV = manv;
        //	hd.MaKH = makh;
        //	hd.MaKho = maKho;
        //	hd.MaTrangThaiHD = 7;
        //	hd.MaHT = 1;
        //	hd.TrangThaiXoa = 1;
        //	qlst.HoaDons.InsertOnSubmit(hd);
        //	qlst.SubmitChanges();
        //	return hd.MaHD;
        //}

        public int Add_HD(DateTime NgayLap, float TongTien, string dchi, int manv, int makh, int maKho)
        {
            HoaDon hd = new HoaDon();
            hd.NgayLap = NgayLap;
            hd.TongTien = TongTien;
            hd.DiaChiGiaoHang = dchi;
            hd.MaNV = manv;
            hd.MaKH = makh;
            hd.MaKho = maKho;
            hd.MaTrangThaiHD = 7;
            hd.MaHT = 1;
            hd.TrangThaiXoa = 1;

            if (makh != 1)
            {
                var cauHinhTichDiem = qlst.CauHinhTichDiems.FirstOrDefault(chtd => chtd.TrangThaiApDung == 1);
                if (cauHinhTichDiem != null)
                {
                    hd.DiemTichDuoc = (int)(hd.TongTien * cauHinhTichDiem.SoDiemTich / cauHinhTichDiem.SoTienTich);
                    hd.MaTichDiem = cauHinhTichDiem.MaTichDiem;
                }
                else
                {
                    hd.DiemTichDuoc = 0;
                    hd.MaTichDiem = null;
                }
            }
            else
            {
                hd.DiemTichDuoc = 0;
                hd.MaTichDiem = null;
            }

            qlst.HoaDons.InsertOnSubmit(hd);
            qlst.SubmitChanges();
            if (makh != 1)
            {
                var khachHang = qlst.KhachHangs.FirstOrDefault(kh => kh.MaKH == makh);
                if (khachHang != null)
                {
                    khachHang.DiemTichLuy += hd.DiemTichDuoc;
                    qlst.SubmitChanges();
                }
            }

            return hd.MaHD;
        }

        public string GetTenKhachHangByMaHD(int maHD)
		{
			var tenKhachHang = (from hd in qlst.HoaDons
								join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
								where hd.MaHD == maHD && hd.TrangThaiXoa == 1
								select kh.TenKH).FirstOrDefault();

			return tenKhachHang;
		}

		public string GetTenNhanVienByMaHD(int maHD)
		{
			var tenNhanVien = (from hd in qlst.HoaDons
							   join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
							   where hd.MaHD == maHD && hd.TrangThaiXoa == 1
							   select nv.TenNV).FirstOrDefault();

			return tenNhanVien;
		}

		public DateTime GetNgayLapHoaDonByMaHD(int maHD)
		{
			var ngayLapHoaDon = (from hd in qlst.HoaDons
								 where hd.MaHD == maHD && hd.TrangThaiXoa == 1
								 select hd.NgayLap).FirstOrDefault();

			return ngayLapHoaDon;
		}

		public bool DeleteHD(int mahd)
		{
			var hd = qlst.HoaDons.FirstOrDefault(p => p.MaHD == mahd);

			if (hd != null)
			{
				hd.TrangThaiXoa = 0;
				qlst.SubmitChanges();
				return true;
			}
			return false;
		}

		public void RestoreHD(int mahd)
		{
			var hd = qlst.HoaDons.FirstOrDefault(p => p.MaHD == mahd);

			if (hd != null)
			{
				hd.TrangThaiXoa = 1;
				qlst.SubmitChanges();
			}
        }

		//public List<ListDoanhThu> TinhDoanhThuTheoThang(int maKho)
		//{
		//    var currentYear = DateTime.Now.Year;
		//    var currentMonth = DateTime.Now.Month;

		//    var query = from hd in qlst.HoaDons
		//                join cthd in qlst.ChiTietHoaDons on hd.MaHD equals cthd.MaHD
		//                join sp in qlst.SanPhams on cthd.MaSP equals sp.MaSP
		//                join ctpk in qlst.ChiTietPhieuKhos on sp.MaSP equals ctpk.MaSP into ctpkGroup
		//                from ctpk in ctpkGroup.DefaultIfEmpty()
		//                where hd.NgayLap.Year == currentYear
		//                      && hd.NgayLap.Month == currentMonth
		//                      && hd.MaKho == maKho
		//                      && (hd.MaTrangThaiHD == 4 || hd.MaTrangThaiHD == 7)
		// && hd.TrangThaiXoa == 1
		//                select new
		//                {
		//                    hd.NgayLap,
		//                    sp.GiaSP,
		//                    cthd.SoLuong,
		//                    DonGia = ctpk != null ? ctpk.DonGia : 0
		//                };

		//    var result = query
		//        .GroupBy(item => item.NgayLap.Day)
		//        .Select(group => new ListDoanhThu
		//        {
		//            ThoiGian = $"Ngày {group.Key} Tháng {currentMonth}",
		//            //DoanhThu = group.Sum(item => item.SoLuong * (item.GiaSP - item.DonGia))
		//            DoanhThu = group.Sum(item => item.SoLuong * (item.GiaSP >= item.DonGia ? item.GiaSP - item.DonGia : 1))
		//        })
		//        .ToList();

		//    return result;
		//}

		public List<ListDoanhThu> TinhDoanhThuTheoThang(int maKho)
		{
			var currentYear = DateTime.Now.Year;
			var currentMonth = DateTime.Now.Month;

			var result = (from hd in qlst.HoaDons
						  join cthd in qlst.ChiTietHoaDons on hd.MaHD equals cthd.MaHD
						  join sp in qlst.SanPhams on cthd.MaSP equals sp.MaSP
						  join k in qlst.Khos on hd.MaKho equals k.MaKho
						  // Lấy giá nhập mới nhất từ ChiTietPhieuKho với loại phiếu kho có mã là 1
						  let latestCtpk = qlst.ChiTietPhieuKhos
											.Where(ctpk => ctpk.MaSP == sp.MaSP && ctpk.MaPK == (
												qlst.PhieuKhos
												.Where(pk => pk.MaKho == maKho && pk.MaLPK == 1) // Điều kiện MaLPK là 1
												.OrderByDescending(pk => pk.NgayNhapXuatKho)
												.Select(pk => pk.MaPK)
												.FirstOrDefault()
											))
											.OrderByDescending(ctpk => ctpk.DonGia)
											.FirstOrDefault()
						  where hd.NgayLap.Year == currentYear
								&& hd.NgayLap.Month == currentMonth
								&& hd.MaKho == maKho
								&& (hd.MaTrangThaiHD == 4 || hd.MaTrangThaiHD == 7)
								&& hd.TrangThaiXoa == 1
						  select new
						  {
							  NgayLap = hd.NgayLap.Day,
							  sp.GiaSP,
							  DonGia = (latestCtpk != null) ? (float)latestCtpk.DonGia : 0, // Chuyển đổi DonGia thành float và xử lý giá trị null
							  cthd.SoLuong
						  })
						  .ToList()
						  .GroupBy(item => item.NgayLap)
						  .Select(group => new ListDoanhThu
						  {
							  ThoiGian = $"Ngày {group.Key} Tháng {currentMonth}",
							  DoanhThu = group.Sum(item => (item.GiaSP - item.DonGia) * item.SoLuong)
						  })
						  .ToList();

			foreach (var doanhThu in result)
			{
				Console.WriteLine($"Thời gian: {doanhThu.ThoiGian}, Doanh thu: {doanhThu.DoanhThu}");
			}

			return result;
		}


		//public List<ListDoanhThu> TinhDoanhThuTheoQuy(int maKho)
		//{
		//    var currentYear = DateTime.Now.Year;

		//    var query = from hd in qlst.HoaDons
		//                join cthd in qlst.ChiTietHoaDons on hd.MaHD equals cthd.MaHD
		//                join sp in qlst.SanPhams on cthd.MaSP equals sp.MaSP
		//                join ctpk in qlst.ChiTietPhieuKhos on sp.MaSP equals ctpk.MaSP into ctpkGroup
		//                from ctpk in ctpkGroup.DefaultIfEmpty()
		//                where hd.NgayLap.Year == currentYear && hd.MaKho == maKho && (hd.MaTrangThaiHD == 4 || hd.MaTrangThaiHD == 7) && hd.TrangThaiXoa == 1
		//                select new
		//                {
		//                    hd.NgayLap,
		//                    sp.GiaSP,
		//                    //cthd.DonGia,
		//                    DonGia = ctpk != null ? ctpk.DonGia : 0,
		//                    cthd.SoLuong
		//                };

		//    var result = query
		//        .ToList()
		//        .GroupBy(item => (item.NgayLap.Month - 1) / 3 + 1)
		//        .Select(group => new ListDoanhThu
		//        {
		//            ThoiGian = "Quý " + group.Key,
		//            //DoanhThu = group.Sum(item => (item.GiaSP - item.DonGia) * item.SoLuong)
		//            DoanhThu = group.Sum(item => item.SoLuong * (item.GiaSP >= item.DonGia ? item.GiaSP - item.DonGia : 1))
		//        }).ToList();

		//    return result;
		//}

		public List<ListDoanhThu> TinhDoanhThuTheoQuy(int maKho)
		{
			var currentYear = DateTime.Now.Year;

			var query = from hd in qlst.HoaDons
						join cthd in qlst.ChiTietHoaDons on hd.MaHD equals cthd.MaHD
						join sp in qlst.SanPhams on cthd.MaSP equals sp.MaSP
						join k in qlst.Khos on hd.MaKho equals k.MaKho
						// Lấy giá nhập mới nhất từ ChiTietPhieuKho với loại phiếu kho có mã là 1
						let latestCtpk = qlst.ChiTietPhieuKhos
										  .Where(ctpk => ctpk.MaSP == sp.MaSP && ctpk.MaPK == (
											  qlst.PhieuKhos
											  .Where(pk => pk.MaKho == maKho && pk.MaLPK == 1) // Điều kiện MaLPK là 1
											  .OrderByDescending(pk => pk.NgayNhapXuatKho)
											  .Select(pk => pk.MaPK)
											  .FirstOrDefault()
										  ))
										  .OrderByDescending(ctpk => ctpk.DonGia)
										  .FirstOrDefault()
						where hd.NgayLap.Year == currentYear
							  && hd.MaKho == maKho
							  && (hd.MaTrangThaiHD == 4 || hd.MaTrangThaiHD == 7)
							  && hd.TrangThaiXoa == 1
						select new
						{
							hd.NgayLap,
							sp.GiaSP,
							DonGia = latestCtpk != null ? (float)latestCtpk.DonGia : 0, // Chuyển đổi DonGia thành float và xử lý giá trị null
							cthd.SoLuong
						};

			var result = query
				.ToList()
				.GroupBy(item => (item.NgayLap.Month - 1) / 3 + 1)
				.Select(group => new ListDoanhThu
				{
					ThoiGian = "Quý " + group.Key,
					DoanhThu = group.Sum(item => (item.GiaSP - item.DonGia) * item.SoLuong)
				}).ToList();

			foreach (var doanhThu in result)
			{
				Console.WriteLine($"Thời gian: {doanhThu.ThoiGian}, Doanh thu: {doanhThu.DoanhThu}");
			}

			return result;
		}


		//public List<ListDoanhThu> TinhDoanhThuTheoTuan(int maKho)
		//{
		//    var currentYear = DateTime.Now.Year;
		//    var currentMonth = DateTime.Now.Month;

		//    var query = from hd in qlst.HoaDons
		//                join cthd in qlst.ChiTietHoaDons on hd.MaHD equals cthd.MaHD
		//                join sp in qlst.SanPhams on cthd.MaSP equals sp.MaSP
		//                join ctpk in qlst.ChiTietPhieuKhos on sp.MaSP equals ctpk.MaSP into ctpkGroup
		//                from ctpk in ctpkGroup.DefaultIfEmpty()
		//                where hd.NgayLap.Year == currentYear
		//                      && hd.NgayLap.Month == currentMonth
		//                      && hd.MaKho == maKho && (hd.MaTrangThaiHD == 4 || hd.MaTrangThaiHD == 7) && hd.TrangThaiXoa == 1
		//                select new
		//                {
		//                    hd.NgayLap,
		//                    sp.GiaSP,
		//                    //cthd.DonGia,
		//                    DonGia = ctpk != null ? ctpk.DonGia : 0,
		//                    cthd.SoLuong
		//                };

		//    var doanhThuTheoTuan = query
		//        .ToList()
		//        .GroupBy(item => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(item.NgayLap, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
		//        .Select(group => new ListDoanhThu
		//        {
		//            ThoiGian = "Tuần " + group.Key,
		//            //DoanhThu = group.Sum(item => (item.GiaSP - item.DonGia) * item.SoLuong)
		//            DoanhThu = group.Sum(item => item.SoLuong * (item.GiaSP >= item.DonGia ? item.GiaSP - item.DonGia : 1))
		//        }).ToList();

		//    var firstDayOfMonth = new DateTime(currentYear, currentMonth, 1);
		//    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

		//    var firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(firstDayOfMonth, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
		//    var lastWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(lastDayOfMonth, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

		//    var allWeeks = Enumerable.Range(firstWeek, lastWeek - firstWeek + 1)
		//                             .Select(week => new ListDoanhThu
		//                             {
		//                                 ThoiGian = "Tuần " + week,
		//                                 DoanhThu = 0
		//                             }).ToList();

		//    foreach (var doanhThu in doanhThuTheoTuan)
		//    {
		//        var week = allWeeks.First(w => w.ThoiGian == doanhThu.ThoiGian);
		//        week.DoanhThu = doanhThu.DoanhThu;
		//    }

		//    return allWeeks;
		//}

		public List<ListDoanhThu> TinhDoanhThuTheoTuan(int maKho)
		{
			var currentYear = DateTime.Now.Year;
			var currentMonth = DateTime.Now.Month;

			var query = from hd in qlst.HoaDons
						join cthd in qlst.ChiTietHoaDons on hd.MaHD equals cthd.MaHD
						join sp in qlst.SanPhams on cthd.MaSP equals sp.MaSP
						join k in qlst.Khos on hd.MaKho equals k.MaKho
						// Lấy giá nhập mới nhất từ ChiTietPhieuKho với loại phiếu kho có mã là 1
						let latestCtpk = qlst.ChiTietPhieuKhos
										  .Where(ctpk => ctpk.MaSP == sp.MaSP && ctpk.MaPK == (
											  qlst.PhieuKhos
											  .Where(pk => pk.MaKho == maKho && pk.MaLPK == 1) // Điều kiện MaLPK là 1
											  .OrderByDescending(pk => pk.NgayNhapXuatKho)
											  .Select(pk => pk.MaPK)
											  .FirstOrDefault()
										  ))
										  .OrderByDescending(ctpk => ctpk.DonGia)
										  .FirstOrDefault()
						where hd.NgayLap.Year == currentYear
							  && hd.NgayLap.Month == currentMonth
							  && hd.MaKho == maKho
							  && (hd.MaTrangThaiHD == 4 || hd.MaTrangThaiHD == 7)
							  && hd.TrangThaiXoa == 1
						select new
						{
							hd.NgayLap,
							sp.GiaSP,
							DonGia = latestCtpk != null ? (float)latestCtpk.DonGia : 0, // Chuyển đổi DonGia thành float và xử lý giá trị null
							cthd.SoLuong
						};

			var doanhThuTheoTuan = query
				.ToList()
				.GroupBy(item => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(item.NgayLap, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
				.Select(group => new ListDoanhThu
				{
					ThoiGian = "Tuần " + group.Key,
					DoanhThu = group.Sum(item => (item.GiaSP - item.DonGia) * item.SoLuong)
				}).ToList();

			var firstDayOfMonth = new DateTime(currentYear, currentMonth, 1);
			var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

			var firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(firstDayOfMonth, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
			var lastWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(lastDayOfMonth, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

			var allWeeks = Enumerable.Range(firstWeek, lastWeek - firstWeek + 1)
									 .Select(week => new ListDoanhThu
									 {
										 ThoiGian = "Tuần " + week,
										 DoanhThu = 0
									 }).ToList();

			foreach (var doanhThu in doanhThuTheoTuan)
			{
				var week = allWeeks.First(w => w.ThoiGian == doanhThu.ThoiGian);
				week.DoanhThu = doanhThu.DoanhThu;
			}

			return allWeeks;
		}

		public List<ListDoanhThuKho> TinhDoanhThuTheoNgay(DateTime ngay)
		{
			var result = (from hd in qlst.HoaDons
						  join cthd in qlst.ChiTietHoaDons on hd.MaHD equals cthd.MaHD
						  join sp in qlst.SanPhams on cthd.MaSP equals sp.MaSP
						  join k in qlst.Khos on hd.MaKho equals k.MaKho
						  // Lấy giá nhập mới nhất từ ChiTietPhieuKho với loại phiếu kho có mã là 1
						  let latestCtpk = qlst.ChiTietPhieuKhos
											.Where(ctpk => ctpk.MaSP == sp.MaSP && ctpk.MaPK == (
												qlst.PhieuKhos
												.Where(pk => pk.MaKho == k.MaKho && pk.MaLPK == 1) // Điều kiện MaLPK là 1
												.OrderByDescending(pk => pk.NgayNhapXuatKho)
												.Select(pk => pk.MaPK)
												.FirstOrDefault()
											))
											.OrderByDescending(ctpk => ctpk.DonGia)
											.FirstOrDefault()
						  where hd.NgayLap.Date == ngay.Date
								&& (hd.MaTrangThaiHD == 4 || hd.MaTrangThaiHD == 7)
								&& hd.TrangThaiXoa == 1
						  select new
						  {
							  k.TenKho,
							  sp.GiaSP,
							  DonGia = latestCtpk != null ? (float)latestCtpk.DonGia : 0, // Chuyển đổi DonGia thành float và xử lý giá trị null
							  cthd.SoLuong
						  })
						  .ToList()
						  .GroupBy(item => item.TenKho)
						  .Select(group => new ListDoanhThuKho
						  {
							  TenKho = group.Key,
							  DoanhThu = group.Sum(item => (item.GiaSP - item.DonGia) * item.SoLuong)
						  })
						  .ToList();

			foreach (var kho in result)
			{
				Console.WriteLine($"Kho: {kho.TenKho}, Doanh thu: {kho.DoanhThu}");
			}

			return result;
		}


		public List<ListFrmHD> GetHoaDonBySDT(string sdt)
		{
			var invoices = from hd in qlst.HoaDons
						   join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
						   join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
						   join ht in qlst.HinhThucBanHangs on hd.MaHT equals ht.MaHT
						   join tt in qlst.TrangThaiHoaDons on hd.MaTrangThaiHD equals tt.MaTrangThaiHD
						   where kh.SDT == sdt
						   orderby hd.NgayLap descending
						   select new ListFrmHD
						   {
							   MaHD = hd.MaHD,
							   NgayLap = hd.NgayLap,
							   LoaiHinhThuc = ht.LoaiHinhThuc,
							   TongTien = hd.TongTien,
							   MoTaTrangThai = tt.MoTaTrangThai,
							   MaNV = nv.TenNV,
							   MaKH = kh.TenKH
						   };

			return invoices.ToList();
		}
		public ListFrmHD GetHDMAHD(int maHD)
		{
			var invoice = (from hd in qlst.HoaDons
						   join kh in qlst.KhachHangs on hd.MaKH equals kh.MaKH
						   join nv in qlst.NhanViens on hd.MaNV equals nv.MaNV
						   join ht in qlst.HinhThucBanHangs on hd.MaHT equals ht.MaHT
						   join tt in qlst.TrangThaiHoaDons on hd.MaTrangThaiHD equals tt.MaTrangThaiHD
						   where hd.MaHD == maHD
						   select new ListFrmHD
						   {
							   MaHD = hd.MaHD,
							   NgayLap = hd.NgayLap,
							   LoaiHinhThuc = ht.LoaiHinhThuc,
							   TongTien = hd.TongTien,
							   MoTaTrangThai = tt.MoTaTrangThai,
							   MaNV = nv.TenNV,
							   MaKH = kh.TenKH
						   }).FirstOrDefault();

			return invoice;
		}
	}
}
