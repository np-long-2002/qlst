using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDangNhap());
            //Application.Run(new frmQRCode());
            //Application.Run(new frmTestChamCong());
            //Application.Run(new frmTestLoadSanPhamImage());
            //Application.Run(new frmDoiTraSanPham());
            //Application.Run(new frmMain());
            //Application.Run(new frmDoiMatKhau());
            //Application.Run(new frmThongKe());
        }
	}
}
