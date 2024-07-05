using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class BLL_ChucVu
    {
        QLSieuThiDataContext qlst = new QLSieuThiDataContext();
        public List<ChucVu> Get_ChucVu()
        {
            return qlst.ChucVus.ToList();
        }
        public List<ChucVu> getListCV(int macv)
        {
            var kq = qlst.ChucVus.Where(o => o.MaCV.Equals(macv));
            return kq.ToList();
        }

        public ChucVu getCV(string macv)
        {
            var kq = from cv in qlst.ChucVus where cv.MaCV.Equals(macv) select cv;
            return kq.FirstOrDefault();
        }

        public void getCVs(DataGridView dgv)
        {
            var kq = from cv in qlst.ChucVus
                     select new
                     {
                         cv.MaCV,
                         cv.MoTa
                     };
            dgv.DataSource = kq;
        }
    }
}
