using DAL;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class BLL_HinhAnhSanPham
    {
        QLSieuThiDataContext qlst = new QLSieuThiDataContext();
        public void AddProductImage(int maSP, string tenHinhAnh)
        {
            HinhAnhSanPham newImage = new HinhAnhSanPham
            {
                AnhSanPham = tenHinhAnh,
                MaSP = maSP
            };
            qlst.HinhAnhSanPhams.InsertOnSubmit(newImage);
            qlst.SubmitChanges();
        }

        //public void UpdateProductImages(int maSP, List<string> imageNames)
        //{
        //    var existingImages = qlst.HinhAnhSanPhams.Where(h => h.MaSP == maSP);
        //    qlst.HinhAnhSanPhams.DeleteAllOnSubmit(existingImages);

        //    foreach (var imageName in imageNames)
        //    {
        //        HinhAnhSanPham newImage = new HinhAnhSanPham
        //        {
        //            AnhSanPham = imageName,
        //            MaSP = maSP
        //        };
        //        qlst.HinhAnhSanPhams.InsertOnSubmit(newImage);
        //    }
        //    qlst.SubmitChanges();
        //}

        //public List<string> GetProductImageNames(int maSP)
        //{
        //    List<string> imageNames = new List<string>();

        //    try
        //    {
        //        Table<SanPham> sanPhamTable = qlst.GetTable<SanPham>();
        //        var query = from sp in sanPhamTable
        //                    where sp.MaSP == maSP
        //                    from hinhAnh in sp.HinhAnhSanPhams
        //                    select hinhAnh.AnhSanPham;

        //        imageNames = query.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Đã xảy ra lỗi khi lấy danh sách tên ảnh từ cơ sở dữ liệu: " + ex.Message);
        //    }

        //    return imageNames;
        //}

        public void UpdateProductImages(int maSP, List<string> imageNames)
        {
            var existingImages = qlst.HinhAnhSanPhams.Where(h => h.MaSP == maSP).ToList();

            foreach (var existingImage in existingImages)
            {
                if (!imageNames.Contains(existingImage.AnhSanPham))
                {
                    qlst.HinhAnhSanPhams.DeleteOnSubmit(existingImage);
                }
            }

            foreach (var imageName in imageNames)
            {
                if (!existingImages.Any(e => e.AnhSanPham == imageName))
                {
                    HinhAnhSanPham newImage = new HinhAnhSanPham
                    {
                        AnhSanPham = imageName,
                        MaSP = maSP
                    };
                    qlst.HinhAnhSanPhams.InsertOnSubmit(newImage);
                }
            }
            qlst.SubmitChanges();
        }

        public List<string> GetProductImageNames(int maSP)
        {
            List<string> imageNames = new List<string>();

            try
            {
                Table<SanPham> sanPhamTable = qlst.GetTable<SanPham>();
                var query = from sp in sanPhamTable
                            where sp.MaSP == maSP
                            from hinhAnh in sp.HinhAnhSanPhams
                            select hinhAnh.AnhSanPham;

                imageNames = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lấy danh sách tên ảnh từ cơ sở dữ liệu: " + ex.Message);
            }

            return imageNames;
        }

    }
}
