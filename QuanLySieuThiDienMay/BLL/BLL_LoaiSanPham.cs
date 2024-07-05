using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BLL
{
    public class BLL_LoaiSanPham
    {
        public BLL_LoaiSanPham()
        {
        }

        QLSieuThiDataContext qlst = new QLSieuThiDataContext();

        public List<LoaiSanPham> getListLSP(int malsp)
        {
            var kq = qlst.LoaiSanPhams.Where(o => o.MaLoaiSP.Equals(malsp));
            return kq.ToList();
        }

        public LoaiSanPham getLSP(string malsp)
        {
            var kq = from lsp in qlst.LoaiSanPhams where lsp.MaLoaiSP.Equals(malsp) select lsp;
            return kq.FirstOrDefault();
        }

        public List<LoaiSanPham> getLoaiSanPham() 
        {
            return qlst.LoaiSanPhams.ToList();
        }

        public void getLSPs(DataGridView dgv)
        {
            var kq = from lsp in qlst.LoaiSanPhams
                     select new
                     {
                         lsp.MaLoaiSP,
                         lsp.MoTa,
                         lsp.MaDM
                     };
            dgv.DataSource = kq;
        }

        public LoaiSanPham Search_LSP(int malsp)
        {
            return qlst.LoaiSanPhams.FirstOrDefault(lsp => lsp.MaLoaiSP == malsp);
        }

        public List<ListFrmLSP> Search_ListLSP_TenLSP(string tenlsp)
        {
            var searchList = (
                from lsp in qlst.LoaiSanPhams
                where lsp.MoTa.Contains(tenlsp)
                select new ListFrmLSP
                {
                    MaLoaiSP = lsp.MaLoaiSP,
                    MoTa = lsp.MoTa,
                    MaDM = lsp.MaDM
                }).ToList();

            return searchList;
        }

        public void sort(DataGridView dgv)
        {
            var kq = from lsp in qlst.LoaiSanPhams
                     orderby lsp.MaLoaiSP descending
                     select new
                     {
                         MaLoaiSP = lsp.MaLoaiSP,
                         MoTa = lsp.MoTa,
                         MaDM = lsp.MaDM
                     };
            dgv.DataSource = kq;
        }

        public string GetTenLoaiSanPham(int malsp)
        {
            var loaiSanPham = qlst.LoaiSanPhams.FirstOrDefault(lsp => lsp.MaLoaiSP == malsp);
            if (loaiSanPham != null)
            {
                return loaiSanPham.MoTa;
            }
            return "Chưa phân loại";
        }
    }
}
