using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ListFrmTTSP
    {
        public string TenSP { get; set; }
        public string Mota { get; set; }
        public float GiaSP { get; set; }
        public DateTime NgaySX { get; set; }
        public int MaLoaiSP { get; set; }
        public int MaNCC { get; set; }
        public List<string> Images { get; set; }
    }
}
