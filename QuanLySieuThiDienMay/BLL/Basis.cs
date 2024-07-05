using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms.DataVisualization.Charting;
using DAL;
using System.Runtime.InteropServices;

namespace BLL
{
    public class Basis
    {
        public void UpLoadImage(PictureBox pictureBox)
        {
            string path = "";
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Pictures files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                path = openFile.FileName;
                pictureBox.ImageLocation = path;
            }
        }

        public void clearTextBoxs(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox)
                    control.Text = string.Empty;
                else
                    clearTextBoxs(control.Controls);
            }
        }

        public void XuatExcel(DataGridView dgv)
        {
            if (dgv.RowCount > 0)
            {
                Excel.Application AppExcel = new Excel.Application();
                AppExcel.Application.Workbooks.Add(Type.Missing);
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    AppExcel.Cells[1, i + 1] = dgv.Columns[i].HeaderText;

                }
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        AppExcel.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value;
                    }
                }
                AppExcel.Columns.AutoFit();
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.Title = "Export Excel";
                savefile.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        AppExcel.ActiveWorkbook.SaveCopyAs(savefile.FileName);
                        AppExcel.ActiveWorkbook.Saved = true;

                    }
                    catch (Exception e)
                    {

                    }
                }

            }

        }

        public void XuatExcelChart(Chart chart, string filePath)
        {
            if (chart.Series.Count > 0)
            {
                Excel.Application AppExcel = new Excel.Application();
                AppExcel.Application.Workbooks.Add(Type.Missing);

                AppExcel.Cells[1, 1] = "Thời gian / Tên Kho";
                AppExcel.Cells[1, 2] = "Doanh Thu";

                int rowIndex = 2;
                foreach (var series in chart.Series)
                {
                    foreach (var point in series.Points)
                    {
                        AppExcel.Cells[rowIndex, 1] = point.AxisLabel;
                        AppExcel.Cells[rowIndex, 2] = point.YValues[0];
                        rowIndex++;
                    }
                }

                AppExcel.Columns.AutoFit();

                try
                {
                    AppExcel.ActiveWorkbook.SaveCopyAs(filePath);
                    AppExcel.ActiveWorkbook.Saved = true;
                }
                catch (Exception e)
                {
                    throw new Exception("Đã xảy ra lỗi khi lưu file: " + e.Message);
                }

                AppExcel.Quit();
            }
        }

        public void ExportToExcel(List<ListSanPhamTraVe> data)
        {
            if (data.Any())
            {
                Excel.Application AppExcel = new Excel.Application();
                AppExcel.Application.Workbooks.Add(Type.Missing);

                var groupedData = data.GroupBy(x => x.TenNCC).ToList();

                foreach (var group in groupedData)
                {
                    Excel.Worksheet worksheet = AppExcel.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    worksheet.Name = group.Key;

                    worksheet.Cells[1, 1] = "Tên Sản Phẩm";
                    worksheet.Cells[1, 2] = "Ngày Đổi Trả";
                    worksheet.Cells[1, 3] = "Số Lượng";
                    worksheet.Cells[1, 4] = "Lý Do";

                    int rowIndex = 2;
                    foreach (var item in group)
                    {
                        worksheet.Cells[rowIndex, 1] = item.TenSP;
                        worksheet.Cells[rowIndex, 2] = item.NgayDoiTra.ToString("dd/MM/yyyy");
                        worksheet.Cells[rowIndex, 3] = item.SoLuong;
                        worksheet.Cells[rowIndex, 4] = item.LyDo;
                        rowIndex++;
                    }

                    worksheet.Columns.AutoFit();
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Title = "Export Excel",
                    Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls",
                    FileName = $"ThongKeDoiTra_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        AppExcel.ActiveWorkbook.SaveCopyAs(saveFileDialog.FileName);
                        AppExcel.ActiveWorkbook.Saved = true;
                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi lưu file: " + e.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                AppExcel.Quit();
            }
        }

        public void ExportToExcelNCC(List<ListNhapHang> data)
        {
            if (data.Any())
            {
                Excel.Application AppExcel = null;
                Excel.Workbook workbook = null;

                try
                {
                    AppExcel = new Excel.Application();
                    workbook = AppExcel.Workbooks.Add(Type.Missing);

                    var groupedData = data.GroupBy(x => x.TenNCC).ToList();

                    foreach (var group in groupedData)
                    {
                        Excel.Worksheet worksheet = AppExcel.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        worksheet.Name = group.Key;

                        worksheet.Cells[1, 1] = "Mã Sản Phẩm";
                        worksheet.Cells[1, 2] = "Tên Sản Phẩm";
                        worksheet.Cells[1, 3] = "Ngày Lập Phiếu";
                        worksheet.Cells[1, 4] = "Số Lượng";
                        worksheet.Cells[1, 5] = "Đơn Giá";
                        worksheet.Cells[1, 6] = "Thành Tiền";

                        int rowIndex = 2;
                        foreach (var item in group)
                        {
                            worksheet.Cells[rowIndex, 1] = item.MaSP;
                            worksheet.Cells[rowIndex, 2] = item.TenSP;
                            worksheet.Cells[rowIndex, 3] = item.NgayNhapXuatKho.ToString("dd/MM/yyyy");
                            worksheet.Cells[rowIndex, 4] = item.SoLuong;
                            worksheet.Cells[rowIndex, 5] = item.DonGia;
                            worksheet.Cells[rowIndex, 6] = item.ThanhTien;
                            rowIndex++;
                        }

                        worksheet.Columns.AutoFit();
                    }

                    using (var saveFileDialog = new SaveFileDialog
                    {
                        Title = "Export Excel",
                        Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls",
                        FileName = $"ThongKeNhaCungCap_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                    })
                    {
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                workbook.SaveAs(saveFileDialog.FileName);
                                MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Đã xảy ra lỗi khi lưu file: " + e.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (workbook != null)
                    {
                        workbook.Close(false);
                        Marshal.ReleaseComObject(workbook);
                    }
                    if (AppExcel != null)
                    {
                        AppExcel.Quit();
                        Marshal.ReleaseComObject(AppExcel);
                    }
                    workbook = null;
                    AppExcel = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }
    }
}
