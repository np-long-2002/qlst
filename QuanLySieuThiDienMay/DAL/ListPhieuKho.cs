using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ListPhieuKho
    {
        public int MaPK { get; set; }
        public string TenLoaiPK { get; set; }
        public string Mota { get; set; }
        public DateTime NgayNhapXuatKho { get; set; }
        public double TongTien { get; set; }
        public string TenNhanVien { get; set; }
        public int MaKho { get; set; }
    }
}
