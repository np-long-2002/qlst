using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ListFrmHDDT
    {
        public int MaHDDT { get; set; }
        public int MaHDD { get; set; }
        public DateTime NgayDoiTra { get; set; }
        public string TrangThai { get; set; }
        public List<ChiTietHoaDonDoiTra> ChiTietDoiTra { get; set; }
    }
}
