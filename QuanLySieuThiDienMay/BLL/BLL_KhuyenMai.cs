using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class BLL_KhuyenMai
	{
		QLSieuThiDataContext qlst = new QLSieuThiDataContext();
		public KhuyenMai GetKhuyenMaiForProduct(int maSP)
		{
			var chiTietKhuyenMais = qlst.ChiTietApDungs
					   .Where(ct => ct.MaSP == maSP)
					   .Select(ct => qlst.KhuyenMais
										.FirstOrDefault(km => km.MaKM == ct.MaKM &&
															  km.NgayBatDau <= DateTime.Now &&
															  km.NgayKetThuc >= DateTime.Now &&
															  (km.MaLKM == 1 || km.MaLKM == 2) && km.TrangThaiXoa == 1))
					   .Where(km => km != null)
					   .OrderByDescending(km => km.NgayBatDau)
					   .ToList();

			var khuyenMai = chiTietKhuyenMais.FirstOrDefault();
			if(khuyenMai != null)
			{
				return khuyenMai;
			}
			return null;
		}
	}
}
