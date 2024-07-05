Create Database SieuThi
Go

Use SieuThi
Go

Drop Database SieuThi
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[ChucVu]
(
	[MaCV] [int] identity(1,1) Not Null,
	[MoTa] [nvarchar](255) Not Null,
	Constraint pk_chucvu Primary Key([MaCV])
) 
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[NhanVien]
(
	[MaNV] [int] identity(1,1) Not Null,
	[TenNV] [nvarchar](255) Not Null,
	[CCCD] [nvarchar](12) Not Null,
	[GioiTinh] [nvarchar](10) Null,
	[DiaChi] [nvarchar](255) Null,
	[SDT] [nvarchar](10) Null,
	[Email] [nvarchar](255) Not Null,	
	[AnhNV] [nvarchar](max) Null,
	[MatKhau] [nvarchar](255) Not Null,
	[MaCV] [int] Not Null,
	[Token] [varchar] (255) Null,
	[TimeReset] [datetime] Null,
	[TrangThaiXoa] [int] Null,
	Constraint pk_nhanvien Primary Key([MaNV]),
	Constraint fk_nhanvien_chucvu Foreign Key([MaCV]) References [dbo].[ChucVu]([MaCV])
)
Go
---------------------------------------------------------------------

---------------------------------------------------------------------

---------------------------------------------------------------------

---------------------------------------------------------------------

---------------------------------------------------------------------

---------------------------------------------------------------------

---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[DanhMucSanPham]
(
    [MaDM] [int] identity(1,1) Not Null,
    [TenDM] [nvarchar](255) Not Null,
    Constraint pk_danhmucsanpham Primary Key([MaDM])
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[LoaiSanPham]
(
    [MaLoaiSP] [int] identity(1,1) Not Null,
    [MoTa] [nvarchar](255) Not Null,
    [MaDM] [int] Not Null,
    Constraint pk_loaisanpham Primary Key([MaLoaiSP]),
    Constraint fk_loaisanpham_danhmucsanpham Foreign Key([MaDM]) References [dbo].[DanhMucSanPham]([MaDM])
)
Go
--------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[NhaCungCap]
(
    [MaNCC] [int] identity(1,1) Not Null,
    [TenNCC] [nvarchar](255) Not Null,
    [DiaChi] [nvarchar](255) Not Null,
    [SDT] [nvarchar](10) Not Null,
    [Email] [nvarchar](255) Not Null,
	[TrangThaiXoa] [int] Null,
    Constraint pk_nhacungcap Primary Key([MaNCC])
)
Go
--------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[SanPham]
(
    [MaSP] [int] identity(1,1) Not Null,
    [TenSP] [nvarchar](255) Not Null,
	[MoTa] [nvarchar](255) Not Null,
    [SoLuong] [int] Not Null,
    [GiaSP] [int] Not Null,
    [NgaySX] [date] Not Null,
    [MaLoaiSP] [int] Not Null,
    [MaNCC] [int] Not Null,
	[TrangThaiXoa] [int] Null,
    Constraint pk_sanpham Primary Key([MaSP]),
    Constraint fk_sanpham_loaisanpham Foreign Key([MaLoaiSP]) References [dbo].[LoaiSanPham]([MaLoaiSP]),
    Constraint fk_sanpham_nhacungcap Foreign Key([MaNCC]) References [dbo].[NhaCungCap]([MaNCC])
)
Go
--------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[HinhAnhSanPham]
(
    [MaHinh] [int] identity(1,1) Not Null,
    [AnhSanPham] [nvarchar](max) Null,
	[MaSP] [int] Not Null,
    Constraint pk_hinhanh Primary Key([MaHinh]),
	Constraint fk_sanpham_hinhanh Foreign Key([MaSP]) References [dbo].[SanPham]([MaSP])
)
Go
--------------------------------------------------------------------

---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[KhachHang]
(
	[MaKH] [int] identity(1,1) Not Null,
	[TenKH] [nvarchar](255) Not Null,
	[GioiTinh] [nvarchar](10) Null,
	[DiaChi] [nvarchar](255) Null,
	[SDT] [nvarchar](10) Null,
	[Email] [nvarchar](255) Null,
	[MatKhau] [nvarchar](255) Null,
	[DiemTichLuy] int Null,
	[Token] [varchar] (255) Null,
	[TimeReset] [datetime] Null,
	[TrangThaiXoa] [int] Null,
	Constraint pk_khachhang Primary Key([MaKH])
)
Go
--------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[GioHang]
(
	[MaGH] [int] identity(1,1) Not Null,
    [MaKH] [int] Not Null,
    [TongTien] [float] Null,
    Constraint pk_giohang Primary Key([MaGH]),
	Constraint fk_giohang_khachhang Foreign Key([MaKH]) References [dbo].[KhachHang]([MaKH])
)
Go
--------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[ChiTietGioHang]
(
	[MaGH] [int] Not Null,
    [MaSP] [int] Not Null,
	[SoLuong] [int] Null,
    Constraint fk_ctgiohang_giohang Foreign Key([MaGH]) References [dbo].[GioHang]([MaGH]),
	Constraint fk_ctgiohang_sanpham Foreign Key([MaSP]) References [dbo].[SanPham]([MaSP])
)
Go
--------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[BinhLuanDanhGia]
(
	[MaSP] [int] Not Null,
    [MaKH] [int] Not Null,
	[SoSao] [int] Null,
	[BinhLuan] [nvarchar](max) Null,
	[NgayBinhLuan] [date] Not Null,
    Constraint fk_bldg_sanpham Foreign Key([MaSP]) References [dbo].[SanPham]([MaSP]),
	Constraint fk_bldg_khachhang Foreign Key([MaKH]) References [dbo].[KhachHang]([MaKH])
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[CauHinhTichDiem]
(
	[MaTichDiem] [int] identity(1,1) Not Null,
	[SoTienTich] [int] Not Null,
	[SoDiemTich] [int] Not Null,
	[TrangThaiApDung] [int] Not Null,
	Constraint pk_cauhinhtichdiem Primary Key([MaTichDiem])
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[Kho]
(
	[MaKho] [int] identity(1,1) Not Null,
	[TenKho] [nvarchar](255) Not Null,
	[DiaChi] [nvarchar] (Max) Not Null,
	[MoTa] [nvarchar](255) Not Null,
	[Lat] [varchar] (Max) Null,
	[Lon] [varchar] (Max) Null,
	Constraint pk_kho Primary Key([MaKho])
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[ChiTietKho]
(
	[MaKho] [int] Not Null,
	[MaSP] [int] Not Null,
	[SoLuongTrongKho] [int] Not Null,
	Constraint pk_ctkho Primary Key([MaKho],[MaSP]),
	Constraint fk_ctkho_kho Foreign Key([MaKho]) References [dbo].[Kho]([MaKho]),
	Constraint fk_ctkho_sanpham Foreign Key([MaSP]) References [dbo].[SanPham]([MaSP])
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[HinhThucBanHang]
(
	[MaHT] [int] identity(1,1) Not Null,
	[LoaiHinhThuc] [nvarchar](255) Null,
	Constraint pk_hinhthuchoadon Primary Key([MaHT])
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[TrangThaiHoaDon]
(
	[MaTrangThaiHD] [int] identity(1,1) Not Null,
	[MoTaTrangThai] [nvarchar](255) Null,
	Constraint pk_trangthaihoadon Primary Key([MaTrangThaiHD])
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[HoaDon]
(
	[MaHD] [int] identity(1,1) Not Null,
	[NgayLap] [date] Not Null,
	[TongTien] [float] Not Null,
	[DiaChiGiaoHang] [nvarchar](255) Not Null,	
	[GhiChu] [nvarchar](255),
	[DiemTichDuoc] [int] Null,
	[MaTichDiem] [int] Null,
	[MaNV] [int] Not Null,
	[MaKH] [int] Not Null,
	[MaKho] [int] Not Null,
	[MaTrangThaiHD] [int] Not Null,
	[MaHT] [int] Not Null,
	[TrangThaiXoa] [int] Null,
	Constraint pk_hoadon Primary Key([MaHD]),
	Constraint fk_hoadon_cauhinhtichdiem Foreign Key([MaTichDiem]) References [dbo].[CauHinhTichDiem]([MaTichDiem]),
	Constraint fk_hoadon_nhanvien Foreign Key([MaNV]) References [dbo].[NhanVien]([MaNV]),
	Constraint fk_hoadon_khachhang Foreign Key([MaKH]) References [dbo].[KhachHang]([MaKH]),
	Constraint fk_hoadon_kho Foreign Key([MaKho]) References [dbo].[Kho]([MaKho]),
	Constraint fk_hoadon_trangthai Foreign Key([MaTrangThaiHD]) References [dbo].[TrangThaiHoaDon]([MaTrangThaiHD]),
	Constraint fk_hoadon_hinhthuc Foreign Key([MaHT]) References [dbo].[HinhThucBanHang]([MaHT])
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[ChiTietHoaDon]
(
	[MaHD] [int] Not Null,
	[MaSP] [int] Not Null,
	[SoLuong] [int] Not Null,
	[DonGia] [int] Not Null,
	[ThanhTien] [float] Not Null,
	Constraint fk_cthoadon_hoadon Foreign Key([MaHD]) References [dbo].[HoaDon]([MaHD]),
	Constraint fk_cthoadon_sanpham Foreign Key([MaSP]) References [dbo].[SanPham]([MaSP]),
	Constraint pl_ChiTietHoaDon Primary Key (MaHD, MaSP)
)
Go
---------------------------------------------------------------------

---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[HoaDonDoiTra]
(
	[MaHDDT] [int] identity(1,1) Not Null,
	[MaHD] [int] Not Null,
	[NgayDoiTra] [date] Not Null,	
	[TrangThai] [varchar](255),
	Constraint pk_hddt Primary Key([MaHDDT]),
	Constraint fk_hddt_hoadon Foreign Key([MaHD]) References [dbo].[HoaDon]([MaHD]),
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[ChiTietHoaDonDoiTra]
(
	[MaHDDT] [int] Not Null,
	[MaSP] [int] Not Null,
	[SoLuong] [int],
	[LyDo] [nvarchar](255) Null,
	Constraint fk_cthddt_hddt Foreign Key([MaHDDT]) References [dbo].[HoaDonDoiTra]([MaHDDT]),
	Constraint fk_cthddt_sanpham Foreign Key([MaSP]) References [dbo].[SanPham]([MaSP]),
	Constraint pk_ChiTietHoaDonDoiTra Primary Key (MaHDDT, MaSP)
)
Go
---------------------------------------------------------------------
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[LoaiPhieuKho]
(
	[MaLPK] [int] identity(1,1) Not Null,
	[MoTa] [nvarchar](255) Not Null,
	Constraint pk_phieuxuat Primary Key([MaLPK])
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[PhieuKho]
(
	[MaPK] [int] identity(1,1) Not Null,
	[MaLPK] [int] Not Null,
	[MoTa] [nvarchar](255) Not Null,
	[NgayNhapXuatKho] [date] Not Null,
	[TongTien] [float] Not Null,
	[MaNV] [int] Not Null,
	[MaKho] [int] Not Null,
	Constraint pk_phieukho Primary Key([MaPK]),
	Constraint fk_phieukho_loaiPK Foreign Key([MaLPK]) References [dbo].[LoaiPhieuKho]([MaLPK]),
	Constraint fk_phieukho_nhanvien Foreign Key([MaNV]) References [dbo].[NhanVien]([MaNV]),
	Constraint fk_phieukho_kho Foreign Key([MaKho]) References [dbo].[Kho]([MaKho]),
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[ChiTietPhieuKho]
(
	[MaPK] [int] Not Null,
	[MaSP] [int] Not Null,
	[SoLuong] [int] Not Null,
	[DonGia] [int] Not Null,
	[ThanhTien] [float] Not Null,
	Constraint pk_ctphieukho Primary Key([MaPK],[MaSP]),
	Constraint fk_ctphieukho_phieukho Foreign Key([MaPK]) References [dbo].[PhieuKho]([MaPK]),
	Constraint fk_ctphieukho_sanpham Foreign Key([MaSP]) References [dbo].[SanPham]([MaSP])
)
Go
---------------------------------------------------------------------

---------------------------------------------------------------------

---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[LoaiKhuyenMai]
(
    [MaLKM] [int] identity(1,1) Not Null,
    [Mota] [nvarchar](255) Not Null,
    Constraint pk_loaikhuyenmai Primary Key([MaLKM])
)
Go
---------------------------------------------------------------------

---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[KhuyenMai]
(
    [MaKM] [nvarchar](255) Not Null,
    [Mota] [nvarchar](255) Not Null,
	[GiaTriDonHangToiThieu] [float] Null,
	[GiaTriGiam] [float] Null,
	[NgayBatDau] [date] Not Null,
	[NgayKetThuc] [date] Not Null,
	[SoDiemDoiDuoc] [int] Null,
	[SoLuong] [int] Null,
    [MaLKM] [int] Not Null,
	[TrangThaiXoa] [int] Null,
    Constraint pk_khuyenmai Primary Key([MaKM]),
    Constraint fk_khuyenmai_loaikhuyenmai Foreign Key([MaLKM]) References [dbo].[LoaiKhuyenMai]([MaLKM]),
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[ChiTietApDungKhuyenMai]
(
    [MaKM] [nvarchar](255) Not Null,
	[MaHD] [int] Not Null,
    [UuDai] [nvarchar](255) Not Null,
	Constraint fk_ctadkhuyenmai_khuyenmai Foreign Key([MaKM]) References [dbo].[KhuyenMai]([MaKM]),
	Constraint fk_ctadkhuyenmai_hoadon Foreign Key([MaHD]) References [dbo].[HoaDon]([MaHD])
)
Go
---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[ChiTietDoiDiem]
(
    [MaKM] [nvarchar](255) Not Null,
	[MaKH] [int] Not Null,
    [UuDaiDoiDuoc] [nvarchar](255) Not Null,
	Constraint fk_ctdoidiem_khuyenmai Foreign Key([MaKM]) References [dbo].[KhuyenMai]([MaKM]),
	Constraint fk_ctdoidiem_khachhang Foreign Key([MaKH]) References [dbo].[KhachHang]([MaKH])
)
Go
---------------------------------------------------------------------

---------------------------------------------------------------------

---------------------------------------------------------------------
SET ANSI_NullS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Table [dbo].[ChiTietApDung]
(
    [MaSP] [int] Not Null,
    [MaKM] [nvarchar](255) Not Null,
	[SoLuong] [int] Not Null,
    Constraint fk_ctapdung_sanpham Foreign Key([MaSP]) References [dbo].[SanPham]([MaSP]),
	Constraint fk_ctapdung_khuyenmai Foreign Key([MaKM]) References [dbo].[KhuyenMai]([MaKM]),
)
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[ChucVu] ON 
Insert Into [dbo].[ChucVu] ([MaCV], [MoTa])
Values 
	(1, N'Quản lý'),
	(2, N'Nhân viên bán hàng'),
	(3, N'Nhân viên kho'),
	(4, N'Nhân viên duyệt đơn'),
	(5, N'Quản lý Website');
SET IDENTITY_INSERT [dbo].[ChucVu] OFF
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[NhanVien] ON 
Insert Into [dbo].[NhanVien] ([MaNV], [TenNV], [CCCD], [GioiTinh], [DiaChi], [SDT], [Email], [AnhNV], [MatKhau], [MaCV], [Token], [TimeReset], [TrangThaiXoa])
Values 
	(1, N'Công ty PhatLongHau', '000000000000', Null, Null, Null, 'admin@gmail.com', Null, '123', 1, Null, Null, 0),
	(2, N'Admin Website', '000000000001', Null, Null, '0245217563', 'adminwebsite@gmail.com', Null, '123', 5, Null, Null, 0),
    (3, N'Nguyễn Văn An', '123456789012', N'Nam', N'123 Đường số 1, Quận 1, TP. HCM', '0123456789', 'nguyenvanan@gmail.com', 'nhanvien1.jpg', '123', 1, Null, Null, 1),
    (4, N'Trần Thị Bình', '234567890123', N'Nữ', N'456 Đường số 5, Quận 2, Hà Nội', '0987654321', 'tranthibinh@gmail.com', 'nhanvien2.png', '123', 2, Null, Null, 1),
    (5, N'Lê Văn Cừ', '345678901234', N'Nam', N'789 Đường số 3, Quận 3, Hà Nộ', '0369852147', 'levancu@gmail.com', 'nhanvien3.jpg', '123', 3, Null, Null, 1),
    (6, N'Phạm Thị Duyên', '456789012345', N'Nữ', N'101 Đường số 7, Quận 4, Đà Nẵng', '0765432198', 'phamthiduyen@gmail.com', 'nhanvien4.jpg', '123', 4, Null, Null, 1),
    (7, N'Hoàng Văn Em', '567890123456', N'Nam', N'111 Đường só 7, Quận 5, TP. HCM', '0897412586', 'hoangvanem@gmail.com', 'nhanvien5.jpg', '123', 1, Null, Null, 1),
    (8, N'Nguyễn Thị Thảo', '678901234567', N'Nữ', N'222 Đường số 11, Quận 6, Long An', '0975318642', 'nguyenthithao@gmail.com', 'nhanvien6.jpg', '123', 2, Null, Null, 1),
    (9, N'Trần Văn Giang', '789012345678', N'Nam', N'333 Đường số 1, Quận 7, Huế', '0321586497', 'tranvangiang@gmail.com', 'nhanvien7.jpg', '123', 3, Null, Null, 1),
    (10, N'Lê Thị Hoài', '890123456789', N'Nữ', N'444 Đường số 2, Quận 8, TP. HCM', '0952684173', 'lethihoai@gmail.com', 'nhanvien8.jpg', '123', 4, Null, Null, 1),
    (11, N'Phạm Văn Anh', '901234567890', N'Nam', N'555 Đường số 9, Quận 9, Hà Nội', '0684235719', 'phamvananh@gmail.com', 'nhanvien9.jpg', '123', 2, Null, Null, 1),
    (12, N'Đặng Thị Hà', '012345678902', N'Nữ', N'666 Đường số 8, Quận 10, Tiền Giang', '0381974256', 'dangthiha@gmail.com', 'nhanvien10.png', '123', 3, Null, Null, 1),
	(13, N'Nguyễn Phước Long', '012345678901', N'Nam', N'666 Đường số 8, Quận 10, Tiền Giang', '0903104340', 'np.long.2002.it@gmail.com', 'nhanvien11.png', '123', 1, Null, Null, 1),
	(14, N'Hậu Đập Chai', '076467017947', N'Nam', N'9/5E, XTD1. XTD', '0903154123', N'vanhau98.nhd@gmail.com', 'nhanvien11.png', N'123', 1, NULL, NULL, 1);
SET IDENTITY_INSERT [dbo].[NhanVien] OFF
Go
---------------------------------------------------------------------

---------------------------------------------------------------------

---------------------------------------------------------------------

--------------------------------------------------------------------- 

---------------------------------------------------------------------

---------------------------------------------------------------------

---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[DanhMucSanPham] ON 
Insert Into [dbo].[DanhMucSanPham] ([MaDM],[TenDM])
Values 
    (1, N'Điện thoại, Tablet'),
	(2, N'Máy ảnh'),
    (3, N'Laptop'),
    (4, N'Tivi, Loa'),	
    (5, N'Máy giặt'),
    (6, N'Tủ lạnh'),
    (7, N'Máy lạnh'),
	(8, N'Quạt điện'),
    (9, N'Lọc nước, Máy nước nóng'),
	(10, N'Gia dụng'),
    (11, N'Đồ dùng nhà bếp'),
    (12, N'Phòng khách, Phòng ăn'),
	(13, N'Phòng ngủ, Trang trí');
SET IDENTITY_INSERT [dbo].[DanhMucSanPham] OFF
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[LoaiSanPham] ON 
Insert Into [dbo].[LoaiSanPham] ([MaLoaiSP],[MoTa], [MaDM])
Values 
    (1, N'Điện thoại Iphone', 1),
	(2, N'Điện thoại SamSung', 1),
    (3, N'Laptop Asus', 3),
	(4, N'Laptop Acer', 3),
    (5, N'Tivi LG', 4),
	(6, N'Tivi Sony', 4),
    (7, N'Máy giặt cửa trên', 5),
	(8, N'Máy giặt cửa ngang', 5),
    (9, N'Tủ lạnh side-by-side', 6),
	(10, N'Tủ lạnh ngăn đá trên', 6),
    (11, N'Máy lạnh Panasonic', 7),
	(12, N'Máy lạnh Daikin', 7),
    (13, N'Máy ảnh Canon', 2),
    (14, N'Máy ảnh Fujifilm', 2),
    (15, N'Quạt điện Kangaroo', 8),
    (16, N'Quạt điện Empire', 8),
	(17, N'Lọc nước nóng lạnh', 9),
	(18, N'Lọc nước lắp âm', 9),
    (19, N'Nồi cơm điện', 10),
	(20, N'Bình thuỷ điện', 10),
    (21, N'Nồi - chảo', 11),
	(22, N'Dao', 11),
    (23, N'Sofa đơn', 12),
	(24, N'Bàn ăn', 12),
    (25, N'Giường đôi', 13),
	(26, N'Tủ gỗ', 13),
	(27, N'Máy tính bảng', 3),
	(28, N'Ipad', 1),
	(29, N'Macbook', 3),
	(30, N'Laptop Dell', 3),
	(31, N'Laptop HP', 3),
	(32, N'Điện thoại Xiaomi', 1),
	(33, N'Laptop LG', 3),
	(34, N'Máy ảnh Sony', 2),
	(35, N'Tivi Panasonic', 4),
	(36, N'Laptop lenovo', 3);
SET IDENTITY_INSERT [dbo].[LoaiSanPham] OFF
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[NhaCungCap] ON 
Insert Into [dbo].[NhaCungCap] ([MaNCC],[TenNCC], [DiaChi], [SDT], [Email], [TrangThaiXoa])
Values 
    (1, N'Công ty TNHH số 1', N'123 Đường số 14, Quận 1, TP. HCM', '0123456789', 's1@gmail.com', 1),
    (2, N'Công ty TNHH số 2', N'456 Đường số 7, Quận 2, TP. HCM', '0987654321', 's2@gmail.com', 1),
    (3, N'Công ty TNHH số 3', N'789 Đường số 5, Quận 3, Hà Nội', '0369852147', 's3@gmail.com', 1),
    (4, N'Công ty TNHH số 4', N'101 Đường số 20, Quận 4, Hà Nội', '0765432198', 's4@gmail.com', 1),
    (5, N'Công ty TNHH số 5', N'111 Đường số 3, Quận 5, Đà Nẵng', '0897412586', 's5@gmail.com', 1),
    (6, N'Công ty TNHH số 6', N'222 Đường số 3, Quận 6, Hà Nội', '0975318642', 's6@gmail.com', 1),
    (7, N'Công ty TNHH số 7', N'333 Đường số 17, Quận 7, Bà Rịa - Vũng Tàu', '0321586497', 's7@gmail.com', 1),
    (8, N'Công ty TNHH số 8', N'444 Đường số 9, Quận 8, Long An', '0952684173', 's8@gmail.com', 1),
    (9, N'Công ty TNHH số 9', N'555 Đường số 5, Quận 9, Tiền Giang', '0684235719', 's9@gmail.com', 1),
    (10, N'Công ty TNHH số 10', N'666 Đường số 6, Quận 10, Bình Dương', '0381974256', 's10@gmail.com', 1);
SET IDENTITY_INSERT [dbo].[NhaCungCap] OFF
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[SanPham] ON 
Insert Into [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [GiaSP], [NgaySX], [MoTa], [MaLoaiSP], [MaNCC], [TrangThaiXoa])
Values 
    (1, N'iPhone 13 Pro Max 512GB', 200, 32990000, '2024-04-30', N'Siêu phẩm smartphone của Apple', 1, 1, 1),
    (2, N'Samsung Galaxy S21 Ultra 256GB', 150, 29990000, '2024-04-30', N'Siêu phẩm của Samsung với camera zoom 100x', 2, 2, 1),
    (3, N'iPad Pro 11 inch 512GB', 200, 27990000, '2024-04-30', N'Máy tính bảng cao cấp của Apple', 28, 1, 1),
    (4, N'MacBook Pro 13 inch M1 512GB', 150, 44990000, '2024-04-30', N'Laptop cao cấp của Apple với chip M1', 29, 1, 1),
    (5, N'Sony A7 III', 200, 29990000, '2024-04-30', N'Máy ảnh mirrorless chất lượng cao của Sony', 34, 3, 1),
    (6, N'Tivi OLED LG 65 inch OLED65C1PTB', 150, 35990000, '2024-04-30', N'Tivi OLED siêu mỏng và sắc nét', 5, 2, 1),
    (7, N'Máy giặt LG FC1409H3E', 200, 15990000, '2024-04-30', N'Máy giặt tiết kiệm điện nước', 7, 2, 1),
    (8, N'Tủ lạnh Panasonic NR-BX468XSPV', 150, 26990000, '2024-04-30', N'Tủ lạnh side-by-side hiện đại', 9, 3, 1),
    (9, N'Máy lạnh Daikin FTKV25NVMV/RKV25NVMV 1HP', 150, 14990000, '2024-04-30', N'Máy lạnh inverter tiết kiệm điện', 12, 3, 1),
    (10, N'Dell XPS 13 9310 2021', 150, 35990000, '2024-04-30', N'Laptop siêu mỏng và nhẹ', 30, 2, 1),
	(11, N'Asus ROG Phone 5 256GB', 200, 24990000, '2024-04-30', N'Smartphone gaming cao cấp của Asus', 3, 2, 1),
    (12, N'Xiaomi Redmi Note 11 Pro 256GB', 150, 7990000, '2024-04-30', N'Smartphone tầm trung của Xiaomi', 32, 3, 1),
    (13, N'HP Spectre x360 14 inch 2022', 200, 30990000, '2024-04-30', N'Laptop 2 trong 1 cao cấp của HP', 31, 2, 1),
    (14, N'LG Gram 17 inch 2022', 150, 33990000, '2024-04-30', N'Laptop siêu nhẹ và bền bỉ của LG', 33, 3, 1),
    (15, N'Sony Alpha 7 IV', 200, 34990000, '2024-04-30', N'Máy ảnh mirrorless chuyên nghiệp của Sony', 34, 3, 1),
    (16, N'Panasonic TX-65JX800V 4K HDR', 150, 19990000, '2024-04-30', N'Tivi LED 4K HDR chất lượng cao', 35, 4, 1),
    (17, N'Samsung WW90K54E0UX 9kg', 200, 19990000, '2024-04-30', N'Máy giặt tiết kiệm nước của Samsung', 8, 2, 1),
    (18, N'Electrolux ESE5687SC 524L', 150, 19990000, '2024-04-30', N'Tủ lạnh side-by-side sang trọng', 10, 4, 1),
    (19, N'Mitsubishi Electric MS-HN13VF 1HP', 150, 10990000, '2024-04-30', N'Máy lạnh inverter tiết kiệm điện', 11, 5, 1),
    (20, N'Acer Predator Helios 300 PH315-54-782Q', 150, 39990000, '2024-04-30', N'Laptop gaming cao cấp của Acer', 4, 6, 1),
    (21, N'Google Pixel 6 Pro 256GB', 200, 23990000, '2024-04-30', N'Smartphone cao cấp của Google', 2, 7, 1),
    (22, N'OnePlus 9 Pro 256GB', 150, 24990000, '2024-04-30', N'Smartphone flagship của OnePlus', 2, 8, 1),
    (23, N'Lenovo Yoga Slim 7 Pro 14 inch', 200, 26990000, '2024-04-30', N'Laptop siêu mỏng và nhẹ của Lenovo', 36, 7, 1),
    (24, N'Dell Alienware m15 R6', 150, 47990000, '2024-04-30', N'Laptop gaming cao cấp của Dell', 30, 8, 1),
    (25, N'Canon EOS R5', 200, 59990000, '2024-04-30', N'Máy ảnh mirrorless chuyên nghiệp của Canon', 13, 4, 1),
    (26, N'Philips 55OLED706 4K UHD', 150, 31990000, '2024-04-30', N'Tivi OLED 4K UHD cao cấp của Philips', 6, 5, 1),
    (27, N'Haier HW80-B14636 8kg', 200, 7999000, '2024-04-30', N'Máy giặt tiết kiệm điện năng', 7, 6, 1),
    (28, N'Samsung RS63R5561SL 620L', 150, 28990000, '2024-04-30', N'Tủ lạnh side-by-side sang trọng của Samsung', 9, 7, 1),
    (29, N'Hitachi R-WB800PGV 722L', 150, 31990000, '2024-04-30', N'Tủ lạnh side-by-side cao cấp của Hitachi', 9, 8, 1),
    (30, N'LG Inverter V 1.5HP V12ENZ', 150, 15990000, '2024-04-30', N'Máy lạnh inverter tiết kiệm điện năng của LG', 12, 9, 1),
    (31, N'Microsoft Surface Laptop 4', 200, 32990000, '2024-04-30', N'Laptop cao cấp của Microsoft', 30, 9, 1),
    (32, N'Xiaomi Mi 11 Ultra 256GB', 150, 23990000, '2024-04-30', N'Smartphone cao cấp của Xiaomi', 32, 10, 1),
    (33, N'HP Pavilion x360 14-dw1020TU', 200, 20990000, '2024-04-30', N'Laptop 2 trong 1 đa năng của HP', 31, 10, 1),
    (34, N'Asus ZenBook 14 UX425EA-KI572T', 150, 23990000, '2024-04-30', N'Laptop siêu mỏng và nhẹ của ASUS', 3, 1, 1),
    (35, N'Fujifilm X-T4', 200, 35990000, '2024-04-30', N'Máy ảnh mirrorless chuyên nghiệp của Fujifilm', 14, 5, 1),
    (36, N'LG OLED77C1PTB 77 inch 4K UHD', 150, 79990000, '2024-04-30', N'Tivi OLED 4K UHD siêu mỏng', 5, 6, 1),
    (37, N'Bosch WAN24261BY 8kg', 200, 9999000, '2024-04-30', N'Máy giặt tiết kiệm nước của Bosch', 7, 7, 1),
    (38, N'Electrolux ESE5689SC 563L', 150, 29990000, '2024-04-30', N'Tủ lạnh side-by-side hiện đại', 9, 9, 1),
    (39, N'Pioneer 1.5HP PA1.5GRV', 150, 14990000, '2024-04-30', N'Máy lạnh inverter tiết kiệm điện năng', 11, 10, 1),
    (40, N'Lenovo Legion 5 Pro 16ACH6H', 150, 35990000, '2024-04-30', N'Laptop gaming cao cấp của Lenovo', 36, 2, 1),
    (41, N'iPhone 12 Pro Max 512GB', 200, 30990000, '2024-04-30', N'Smartphone cao cấp của Apple', 1, 1, 1),
    (42, N'Samsung Galaxy Z Fold 3 256GB', 150, 45990000, '2024-04-30', N'Smartphone gập lại cao cấp của Samsung', 2, 2, 1),
    (43, N'Asus Zenfone 8 Flip 256GB', 200, 19990000, '2024-04-30', N'Smartphone camera xoay của Asus', 3, 3, 1),
    (44, N'Dell XPS 15 9510', 150, 45990000, '2024-04-30', N'Laptop siêu mỏng và nhẹ của Dell', 30, 1, 1),
    (45, N'LG Gram 16 inch 2022', 200, 33990000, '2024-04-30', N'Laptop siêu nhẹ và bền bỉ của LG', 4, 2, 1),
    (46, N'Sony Alpha 7C', 150, 29990000, '2024-04-30', N'Máy ảnh mirrorless nhỏ gọn của Sony', 14, 3, 1),
    (47, N'TCL 55P725 4K UHD', 200, 10990000, '2024-04-30', N'Tivi LED 4K UHD giá rẻ', 5, 4, 1),
    (48, N'Electrolux EWF9045BDW 9kg', 150, 14990000, '2024-04-30', N'Máy giặt tiết kiệm nước và năng lượng', 7, 5, 1),
    (49, N'Hitachi R-VG620PG5 566L', 150, 21990000, '2024-04-30', N'Tủ lạnh side-by-side hiện đại của Hitachi', 9, 6, 1),
    (50, N'Daikin FTKM20MVMV/RKM20MVMV 2HP', 150, 18990000, '2024-04-30', N'Máy lạnh inverter tiết kiệm điện năng', 12, 7, 1),
    (51, N'Lenovo ThinkPad X1 Carbon Gen 9', 200, 42990000, '2024-04-30', N'Laptop doanh nhân cao cấp của Lenovo', 36, 8, 1),
    (52, N'Google Pixel 6 128GB', 150, 16990000, '2024-04-30', N'Smartphone cao cấp của Google', 2, 9, 1),
    (53, N'HP Envy x360 13-ay0205TU', 200, 18990000, '2024-04-30', N'Laptop 2 trong 1 đa năng của HP', 31, 10, 1),
    (54, N'Asus ZenBook 14 UX425JA-KI380T', 150, 16990000, '2024-04-30', N'Laptop siêu mỏng và nhẹ của ASUS', 3, 1, 1),
    (55, N'Fujifilm X-T30 II', 200, 20990000, '2024-04-30', N'Máy ảnh mirrorless tầm trung của Fujifilm', 14, 2, 1),
    (56, N'Philips 65OLED706 4K UHD', 150, 36990000, '2024-04-30', N'Tivi OLED 4K UHD cao cấp của Philips', 5, 3, 1),
    (57, N'Beko WTV8744XD0 7kg', 200, 10990000, '2024-04-30', N'Máy giặt tiết kiệm nước của Beko', 8, 4, 1),
    (58, N'Samsung RS67A8810S9 677L', 150, 39990000, '2024-04-30', N'Tủ lạnh side-by-side sang trọng của Samsung', 9, 5, 1),
    (59, N'Panasonic NR-BX468XSXV 450L', 150, 29990000, '2024-04-30', N'Tủ lạnh side-by-side hiện đại của Panasonic', 9, 6, 1),
    (60, N'Mitsubishi Electric MS-HN10VF 1HP', 150, 12990000, '2024-04-30', N'Máy lạnh inverter tiết kiệm điện năng', 11, 7, 1),
    (61, N'Acer Nitro 5 AN515-57-76SJ', 200, 23990000, '2024-04-30', N'Laptop gaming tầm trung của Acer', 4, 8, 1),
    (62, N'iPhone 11 128GB', 150, 15990000, '2024-04-30', N'Smartphone tầm trung của Apple', 1, 1, 1),
    (63, N'OnePlus Nord CE 5G 128GB', 200, 8490000, '2024-04-30', N'Smartphone tầm trung của OnePlus', 2, 2, 1),
    (64, N'Asus Zenfone 8 128GB', 150, 16990000, '2024-04-30', N'Smartphone cao cấp của Asus', 3, 3, 1),
    (65, N'HP Pavilion 14-dv0065TU', 200, 16990000, '2024-04-30', N'Laptop phổ thông của HP', 31, 4, 1),
    (66, N'LG Gram 14 inch 2022', 150, 26990000, '2024-04-30', N'Laptop siêu nhẹ và bền bỉ của LG', 4, 5, 1),
    (67, N'Sony Alpha 6400', 200, 24990000, '2024-04-30', N'Máy ảnh mirrorless tầm trung của Sony', 14, 6, 1),
    (68, N'TCL 55P715 4K UHD', 150, 9999000, '2024-04-30', N'Tivi LED 4K UHD giá rẻ', 6, 7, 1),
    (69, N'Beko WTIK76151F 7kg', 150, 9999000, '2024-04-30', N'Máy giặt tiết kiệm nước của Beko', 8, 8, 1),
    (70, N'LG InstaView Door-in-Door GR-Q31FGLN 910L', 150, 45990000, '2024-04-30', N'Tủ lạnh side-by-side cao cấp của LG', 9, 9, 1),
    (71, N'Hitachi R-BG480PGV 405L', 200, 26990000, '2024-04-30', N'Tủ lạnh side-by-side sang trọng của Hitachi', 9, 10, 1),
    (72, N'Daikin FTKV25NVMV/RKV25NVMV 1HP', 150, 15990000, '2024-04-30', N'Máy lạnh inverter tiết kiệm điện năng', 12, 1, 1),
    (73, N'Lenovo ThinkPad E14 Gen 3', 200, 16990000, '2024-04-30', N'Laptop doanh nhân phổ thông của Lenovo', 36, 2, 1),
    (74, N'Google Pixel 5a 5G 128GB', 150, 10990000, '2024-04-30', N'Smartphone tầm trung của Google', 2, 3, 1),
    (75, N'HP Pavilion x360 14-dw1021TU', 200, 18990000, '2024-04-30', N'Laptop 2 trong 1 đa năng của HP', 31, 4, 1),
    (76, N'Asus VivoBook S14 S433EQ-AM702T', 150, 18990000, '2024-04-30', N'Laptop phổ thông của ASUS', 3, 5, 1),
    (77, N'Fujifilm X-T200', 200, 13990000, '2024-04-30', N'Máy ảnh mirrorless phổ thông của Fujifilm', 14, 6, 1),
    (78, N'Philips 43PUS7855 4K UHD', 150, 7999000, '2024-04-30', N'Tivi LED 4K UHD giá rẻ của Philips', 6, 7, 1),
    (79, N'Beko WTY87150BY 8kg', 150, 14990000, '2024-04-30', N'Máy giặt tiết kiệm nước của Beko', 7, 8, 1),
    (80, N'Samsung RT46K6000S8 455L', 150, 10990000, '2024-04-30', N'Tủ lạnh hai cánh sang trọng của Samsung', 10, 9, 1),
    (81, N'Panasonic NR-BX468XSXV 450L', 200, 29990000, '2024-04-30', N'Tủ lạnh side-by-side hiện đại của Panasonic', 9, 2, 1),
    (82, N'Mitsubishi Electric MS-HN10VF 1HP', 150, 12990000, '2024-04-30', N'Máy lạnh inverter tiết kiệm điện năng', 11, 2, 1),
    (83, N'Acer Nitro 7', 200, 23990000, '2024-04-30', N'Laptop gaming tầm trung của Acer', 4, 2, 1),
    (84, N'Asus VivoBook S14 S433EA', 150, 16990000, '2024-04-30', N'Laptop phổ thông với thiết kế mỏng nhẹ', 3, 9, 1),
    (85, N'Tivi QLED Samsung 75 inch QN75Q70AA', 200, 38990000, '2024-04-30', N'Tivi QLED 4K chất lượng cao', 5, 10, 1),
    (86, N'Máy giặt Electrolux EWF7525EQWA 7.5kg', 150, 8990000, '2024-04-30', N'Máy giặt cửa trước tiết kiệm nước', 7, 1, 1),
    (87, N'Tủ lạnh LG GR-M762HSHM 706L', 200, 27990000, '2024-04-30', N'Tủ lạnh side-by-side hiện đại', 9, 2, 1),
    (88, N'Máy lạnh Samsung AR12TSFTAWKXSV 1.5HP', 150, 10990000, '2024-04-30', N'Máy lạnh inverter tiết kiệm điện', 11, 3, 1),
    (89, N'iPhone 13 128GB', 150, 23990000, '2024-04-30', N'Smartphone cao cấp của Apple', 1, 4, 1),
    (90, N'Samsung Galaxy S20 FE 128GB', 150, 16990000, '2024-04-30', N'Smartphone cao cấp của Samsung', 2, 5, 1),
    (91, N'Lenovo ThinkPad X1 Fold', 200, 69990000, '2024-04-30', N'Laptop màn hình gập đầu tiên trên thế giới', 36, 6, 1),
    (92, N'Dell Inspiron 14 5410', 150, 19990000, '2024-04-30', N'Laptop phổ thông với thiết kế mỏng nhẹ', 30, 2, 1),
    (93, N'iPhone SE 2022 128GB', 200, 12990000, '2024-04-30', N'Smartphone giá rẻ của Apple', 1, 7, 1),
    (94, N'Samsung Galaxy A12 64GB', 150, 4490000, '2024-04-30', N'Smartphone giá rẻ của Samsung', 2, 8, 1),
    (95, N'iPhone 12 Pro Max 256GB', 200, 27990000, '2024-04-30', N'Smartphone cao cấp của Apple', 1, 1, 1),
    (96, N'Asus ZenBook Duo 14 UX482EA', 150, 26990000, '2024-04-30', N'Laptop 2 màn hình với màn hình cảm ứng phụ', 3, 6, 1),
    (97, N'Máy lạnh Daikin FTKV50NVMV/RKV50NVMV 2HP', 200, 18990000, '2024-04-30', N'Máy lạnh inverter tiết kiệm điện', 12, 10, 1),
    (98, N'Dell Latitude 7420', 150, 32990000, '2024-04-30', N'Laptop doanh nhân siêu nhẹ và bền bỉ', 30, 2, 1),
    (99, N'Samsung Galaxy A32 128GB', 150, 6490000, '2024-04-30', N'Smartphone giá rẻ của Samsung', 2, 8, 1),
    (100, N'Asus ZenBook Flip 13 UX363EA', 150, 24990000, '2024-04-30', N'Laptop 2 trong 1 với màn hình cảm ứng 360 độ', 3, 9, 1),
	(101, N'Sản phẩm mẫu', 0, 0, '2024-07-01', N'Sản phẩm mẫu của siêu thị', 1, 1, 1);
SET IDENTITY_INSERT [dbo].[SanPham] OFF
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[HinhAnhSanPham] ON 
Insert Into [dbo].[HinhAnhSanPham] ([MaHinh],[AnhSanPham], [MaSP])
Values 
    (1, 'iPhone.jpg', 1), (2, 'Samsung.jpg', 2), (3, 'iPad.jpg', 3), (4, 'MacBook.jpg', 4), (5, 'Sony.jpg', 5), (6, 'Tivi.jpg', 6), (7, 'MayGiat.jpg', 7), (8, 'TuLanh.jpg', 8), (9, 'MayLanh.jpg', 9), (10, 'Dell.jpg', 10),
	(11, 'Asus.jpg', 11), (12, 'Xiaomi.jpg', 12), (13, 'HP.jpg', 13), (14, 'LG.jpg', 14), (15, 'Sony1.jpg', 15), (16, 'Panasonic.jpg', 16), (17, 'Samsung1.jpg', 17), (18, 'Electrolux.jpg', 18), (19, 'Mitsubishi.jpg', 19), (20, 'Acer.jpg', 20),
	(21, 'Google.jpg', 21), (22, 'OnePlus.jpg', 22), (23, 'Lenovo.jpg', 23), (24, 'Dell1.jpg', 24), (25, 'Canon.jpg', 25), (26, 'Philips.jpg', 26), (27, 'Haier.jpg', 27), (28, 'Samsung2.jpg', 28), (29, 'Hitachi.jpg', 29), (30, 'LG1.jpg', 30),
	(31, 'Microsoft.jpg', 31), (32, 'Xiaomi1.jpg', 32), (33, 'HP1.jpg', 33), (34, 'Asus1.jpg', 34), (35, 'Fujifilm.jpg', 35), (36, 'LG2.jpg', 36), (37, 'Bosch.jpg', 37), (38, 'Electrolux1.jpg', 38), (39, 'Pioneer.jpg', 39), (40, 'Lenovo1.jpg', 40),
	(41, 'iPhone1.jpg', 41), (42, 'Samsung3.jpg', 42), (43, 'Asus2.jpg', 43), (44, 'Dell2.jpg', 44), (45, 'LG3.jpg', 45), (46, 'Sony2.jpg', 46), (47, 'TCL.jpg', 47), (48, 'Electrolux2.jpg', 48), (49, 'Hitachi1.jpg', 49), (50, 'Daikin.jpg', 50),
	(51, 'Lenovo2.jpg', 51), (52, 'Google1.jpg', 52), (53, 'HP2.jpg', 53), (54, 'Asus3.jpg', 54), (55, 'Fujifilm1.jpg', 55), (56, 'Philips1.jpg', 56), (57, 'Beko.jpg', 57), (58, 'Samsung4.jpg', 58), (59, 'Panasonic1.jpg', 59), (60, 'Mitsubishi1.jpg', 60),
	(61, 'Acer1.jpg', 61), (62, 'iPhone2.jpg', 62), (63, 'OnePlus1.jpg', 63), (64, 'Asus4.jpg', 64), (65, 'HP3.jpg', 65), (66, 'LG4.jpg', 66), (67, 'Sony3.jpg', 67), (68, 'TCL1.jpg', 68), (69, 'Beko1.jpg', 69), (70, 'LG5.jpg', 70),
	(71, 'Hitachi2.jpg', 71), (72, 'Daikin1.jpg', 72), (73, 'Lenovo3.jpg', 73), (74, 'Google2.jpg', 74), (75, 'HP4.jpg', 75), (76, 'Asus5.jpg', 76), (77, 'Fujifilm2.jpg', 77), (78, 'Philips2.jpg', 78), (79, 'Beko2.jpg', 79), (80, 'Samsung5.jpg', 80),
	(81, 'Panasonic2.jpg', 81), (82, 'Mitsubishi2.jpg', 82), (83, 'Acer2.jpg', 83), (84, 'Asus6.jpg', 84), (85, 'Tivi1.jpg', 85), (86, 'MayGiat1.jpg', 86), (87, 'TuLanh1.jpg', 87), (88, 'MayLanh1.jpg', 88), (89, 'iPhone3.jpg', 89), (90, 'Samsung6.jpg', 90),
	(91, 'Lenovo4.jpg', 91), (92, 'Dell3.jpg', 92), (93, 'iPhone4.jpg', 93), (94, 'Samsung7.jpg', 94), (95, 'iPhone5.jpg', 95), (96, 'Asus7.jpg', 96), (97, 'MayLanh2.jpg', 97), (98, 'Dell4.jpg', 98), (99, 'Samsung8.jpg', 99), (100, 'Asus8.jpg', 100),
	(101, N'iPhone11.jpg', 1), (102, N'iPhone12.jpg', 1), 
	(103, N'Samsung11.jpg', 2), (104, N'Samsung12.jpg', 2), 
	(105, N'iPad1.jpg', 3), (106, N'iPad2.jpg', 3), 
	(107, N'MacBook1.jpg', 4), (108, N'MacBook2.jpg', 4), 
	(109, N'Sony11.jpg', 5), (110, N'Sony12.jpg', 5),
	(111, N'Tivi11.jpg', 6), (112, N'Tivi12.jpg', 6), 
	(113, N'MayGiat11.jpg', 7), (114, N'MayGiat12.jpg', 7), 
	(115, N'TuLanh11.jpg', 8), (116, N'TuLanh12.jpg', 8), 
	(117, N'MayLanh11.jpg', 9), (118, N'MayLanh12.jpg', 9), 
	(119, N'Dell11.jpg', 10), (120, N'Dell12.jpg', 10),
	(121, N'Asus11.jpg', 11), (122, N'Asus12.jpg', 11), 
	(123, N'Xiaomi11.jpg', 12), (124, N'Xiaomi12.jpg', 12), 
	(125, N'HP11.jpg', 13), (126, N'HP12.jpg', 13), 
	(127, N'LG11.jpg', 14), (128, N'LG12.jpg', 14), 
	(129, N'Sony13.jpg', 15), (130, N'Sony14.jpg', 15),
	(131, N'Panasonic11.jpg', 16), (132, N'Panasonic12.jpg', 16), 
	(133, N'Samsung13.jpg', 17), (134, N'Samsung14.jpg', 17), 
	(135, N'Electrolux11.jpg', 18), (136, N'Electrolux12.jpg', 18), 
	(137, N'Mitsubishi11.jpg', 19), (138, N'Mitsubishi12.jpg', 19), 
	(139, N'Acer11.jpg', 20), (140, N'Acer12.jpg', 20),
	(141, N'Google11.jpg', 21), (142, N'Google12.jpg', 21), 
	(143, N'OnePlus11.jpg', 22), (144, N'OnePlus12.jpg', 22), 
	(145, N'Lenovo11.jpg', 23), (146, N'Lenovo12.jpg', 23), 
	(147, N'Dell13.jpg', 24), (148, N'Dell14.jpg', 24), 
	(149, N'Canon1.jpg', 25), (150, N'Canon2.jpg', 25),
	(151, N'Philips11.jpg', 26), (152, N'Philips12.jpg', 26), 
	(153, N'Haier11.jpg', 27), (154, N'Haier12.jpg', 27), 
	(155, N'Samsung15.jpg', 28), (156, N'Samsung16.jpg', 28), 
	(157, N'Hitachi11.jpg', 29), (158, N'Hitachi12.jpg', 29), 
	(159, N'LG13.jpg', 30), (160, N'LG14.jpg', 30),
	(161, N'Microsoft1.jpg',31), (162, N'Microsoft2.jpg', 31), 
	(163, N'Xiaomi13.jpg', 32), (164, N'Xiaomi14.jpg', 32), 
	(165, N'HP13.jpg', 33), (166, N'HP14.jpg', 33), 
	(167, N'Asus13.jpg', 34), (168, N'Asus14.jpg', 34), 
	(169, N'Fujifilm11.jpg', 35), (170, N'Fujifilm12.jpg', 35),
	(171, N'LG15.jpg', 36), (172, N'LG16.jpg', 36), 
	(173, N'Bosch1.jpg', 37), (174, N'Bosch2.jpg', 37), 
	(175, N'Electrolux13.jpg', 38), (176, N'Electrolux14.jpg', 38), 
	(177, N'Pioneer1.jpg', 39), (178, N'Pioneer2.jpg', 39), 
	(179, N'Lenovo13.jpg', 40), (180, N'Lenovo14.jpg', 40),
	(181, N'iPhone13.jpg',41), (182, N'iPhone14.jpg', 41), 
	(183, N'Samsung17.jpg', 42), (184, N'Samsung18.jpg', 42), 
	(185, N'Asus15.jpg', 43), (186, N'Asus16.jpg', 43), 
	(187, N'Dell15.jpg', 44), (188, N'Dell16.jpg', 44), 
	(189, N'LG17.jpg', 45), (190, N'LG18.jpg', 45),
	(191, N'Sony15.jpg', 46), (192, N'Sony16.jpg', 46), 
	(193, N'TCL11.jpg', 47), (194, N'TCL12.jpg', 47), 
	(195, N'Electrolux15.jpg', 48), (196, N'Electrolux16.jpg', 48), 
	(197, N'Hitachi13.jpg', 49), (198, N'Hitachi14.jpg', 49), 
	(199, N'Daikin11.jpg', 50), (200, N'Daikin12.jpg', 50),
	(201, N'Lenovo15.jpg', 51), (202, N'Lenovo16.jpg', 51), 
	(203, N'Google13.jpg', 52), (204, N'Google14.jpg', 52), 
	(205, N'HP15.jpg', 53), (206, N'HP16.jpg', 53), 
	(207, N'Asus17.jpg', 54), (208, N'Asus18.jpg', 54), 
	(209, N'Fujifilm13.jpg', 55), (210, N'Fujifilm14.jp', 55),
	(211, N'Philips15.jpg', 56), (212, N'Philips16.jpg', 56), 
	(213, N'Beko11.jpg', 57), (214, N'Beko12.jpg', 57), 
	(215, N'Samsung21.jpg', 58), (216, N'Samsung22.jpg', 58), 
	(217, N'Panasonic13.jpg', 59), (218, N'Panasonic14.jpg', 59), 
	(219, N'Mitsubishi13.jpg', 60), (220, N'Mitsubishi14.jpg', 60),
	(221, N'Acer13.jpg', 61), (222, N'Acer14.jpg', 61), 
	(223, N'iPhone15.jpg', 62), (224, N'iPhone16.jpg', 62), 
	(225, N'OnePlus13.jpg', 63), (226, N'OnePlus14.jpg', 63), 
	(227, N'Asus21.jpg', 64), (228, N'Asus22.jpg', 64), 
	(229, N'HP17.jpg', 65), (230, N'HP18.jpg', 65),
	(231, N'LG21.jpg', 66), (232, N'LG22.jpg', 66), 
	(233, N'Sony17.jpg', 67), (234, N'Sony18.jpg', 67), 
	(235, N'TCL13.jpg', 68), (236, N'TCL14.jpg', 68), 
	(237, N'Beko13.jpg', 69), (238, N'Beko14.jpg', 69), 
	(239, N'LG23.jpg', 70), (240, N'LG24.jpg', 70),
	(241, N'Hitachi15.jpg', 71), (242, N'Hitachi16.jpg', 71), 
	(243, N'Daikin13.jpg', 72), (244, N'Daikin14.jpg', 72), 
	(245, N'Lenovo17.jpg', 73), (246, N'Lenovo18.jpg', 73), 
	(247, N'Google15.jpg', 74), (248, N'Google16.jpg', 74), 
	(249, N'HP21.jpg', 75), (250, N'HP22.jpg', 75),
	(251, N'Asus23.jpg', 76), (252, N'Asus24.jpg', 76), 
	(253, N'Fujifilm17.jpg', 77), (254, N'Fujifilm18.jpg', 77), 
	(255, N'Philips13.jpg', 78), (256, N'Philips14.jpg', 78), 
	(257, N'Beko15.jpg', 79), (258, N'Beko16.jpg', 79), 
	(259, N'Samsung23.jpg', 80), (260, N'Samsung24.jpg', 80),
	(261, N'Panasonic15.jpg', 81), (262, N'Panasonic16.jpg', 81), 
	(263, N'Mitsubishi15.jpg', 82), (264, N'Mitsubishi16.jpg', 82), 
	(265, N'Acer15.jpg', 83), (266, N'Acer16.jpg', 83), 
	(267, N'Asus25.jpg', 84), (268, N'Asus26.jpg', 84), 
	(269, N'Samsung25.jpg', 85), (270, N'Samsung26.jpg', 85),
	(271, N'MayGiat13.jpg', 86), (272, N'MayGiat14.jpg', 86), 
	(273, N'TuLanh13.jpg', 87), (274, N'TuLanh14.jpg', 87), 
	(275, N'MayLanh13.jpg', 88), (276, N'MayLanh14.jpg', 88), 
	(277, N'iPhone17.jpg', 89), (278, N'iPhone18.jpg', 89), 
	(279, N'Samsung27.jpg', 90), (280, N'Samsung28.jpg', 90),
	(281, N'Lenovo21.jpg', 91), (282, N'Lenovo22.jpg', 91), 
	(283, N'Dell17.jpg', 92), (284, N'Dell18.jpg', 92), 
	(285, N'iPhone21.jpg', 93), (286, N'iPhone22.jpg', 93), 
	(287, N'Samsung31.jpg', 94), (288, N'Samsung32.jpg', 94), 
	(289, N'iPhone23.jpg', 95), (290, N'iPhone24.jpg', 95),
	(291, N'Asus27.jpg', 96), (292, N'Asus28.jpg', 96), 
	(293, N'Daikin15.jpg', 97), (294, N'Daikin16.jpg', 97), 
	(295, N'Dell21.jpg', 98), (296, N'Dell22.jpg', 98), 
	(297, N'Samsung33.jpg', 99), (298, N'Samsung34.jpg', 99), 
	(299, N'Asus31.jpg', 100), (300, N'Asus32.jpg', 100),
	(301, N'SanPhamMau.jpg', 101);
SET IDENTITY_INSERT [dbo].[HinhAnhSanPham] OFF
Go
---------------------------------------------------------------------

---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[KhachHang] ON 
Insert Into [dbo].[KhachHang] ([MaKH], [TenKH], [GioiTinh], [DiaChi], [SDT], [Email], [MatKhau], [DiemTichLuy], [Token], [TimeReset], [TrangThaiXoa])
Values 
	(1, N'Khách hàng vãng lai', Null, Null, Null, Null, Null, 0, Null, Null, 0),
    (2, N'Nguyễn Thị Lan', N'Nữ', N'123 Đường số 14, Quận 1, TP. HCM', '0123456789', 'lannguyen@gmail.com', '123', 100, Null, Null, 1),
    (3, N'Trần Văn Tú', N'Nam', N'456 Đường số 7, Quận 2, TP. HCM', '0987654321', 'tutran@gmail.com', '123', 50, Null, Null, 1),
    (4, N'Lê Thị Hương', N'Nữ', N'789 Đường số 5, Quận 3, Hà Nội', '0369852147', 'huongle@gmail.com', '123', 20, Null, Null, 1),
    (5, N'Phạm Văn Đức', N'Nam', N'101 Đường số 20, Quận 4, Hà Nội', '0765432198', 'phamvanduc@gmail.com', '123', 50, Null, Null, 1),
    (6, N'Hồ Thị Thu', N'Nữ', N'111 Đường số 3, Quận 5, Đà Nẵng', '0897412586', 'thuthu@gmail.com', '123', 30, Null, Null, 1),
    (7, N'Nguyễn Văn Nam', N'Nam', N'222 Đường số 3, Quận 6, Hà Nội', '0975318642', 'namnguyen@gmail.com', '123', 70, Null, Null, 1),
    (8, N'Trần Thị Hằng', N'Nữ', N'333 Đường số 17, Quận 7, Bà Rịa - Vũng Tàu', '0321586497', 'hangtran@gmail.com', '123', 10, Null, Null, 1),
    (9, N'Lê Văn Phú', N'Nam', N'444 Đường số 9, Quận 8, Long An', '0952684173', 'phule@gmail.com', '123', 80, Null, Null, 1),
    (10, N'Phạm Thị Mai', N'Nữ', N'555 Đường số 5, Quận 9, TP. HCM', '0684235719', 'maipham@gmail.com', '123', 90, Null, Null, 1),
    (11, N'Đặng Văn Thanh', N'Nam', N'666 Đường số 4, Quận 10, TP. HCM', '0381974256', 'thanhdang@gmail.com', '123', 25, Null, Null, 1),
	(12, N'Nguyễn Phước Long', N'Nam', N'666 Đường số 8, Quận 10, Tiền Giang', '0903104340', 'np.long.2002.it@gmail.com', '123', 30, Null, Null, 1),
	(13, N'Hậu Đập Chai', N'Nam', N'9/5E, XTD1. XTD', N'0764670179', N'vanhau98.nhd@gmail.com', N'123', 0, NULL, NULL, 1);
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[GioHang] ON 
Insert Into [dbo].[GioHang] ([MaGH], [MaKH], [TongTien])
Values 
    (1, 1, 79979000),
    (2, 2, 89960000),
    (3, 3, 73970000),
    (4, 4, 499000),
    (5, 5, 29980000),
    (6, 6, 31990000),
    (7, 7, 10470000),
    (8, 8, 0),
    (9, 9, 0),
    (10, 10, 0),
	(11, 12, Null);
SET IDENTITY_INSERT [dbo].[GioHang] OFF
Go
---------------------------------------------------------------------
Insert Into [dbo].[ChiTietGioHang] ([MaGH], [MaSP], [SoLuong])
Values 
    (1, 1, 2),
    (1, 3, 1),
    (2, 2, 1),
    (2, 6, 3),
    (3, 5, 2),
    (3, 7, 1),
    (4, 8, 1),
    (5, 4, 2),
    (6, 9, 1),
    (7, 10, 3),
	(11, 98, 1),
	(11, 99, 1);
Go
---------------------------------------------------------------------
Insert Into [dbo].[BinhLuanDanhGia] ([MaSP], [MaKH], [SoSao], [BinhLuan], [NgayBinhLuan])
Values 
    (1, 1, 5, N'Sản phẩm rất tốt, đáng đồng tiền bát gạo', '2024-04-01'),
    (2, 2, 4, N'Laptop chạy mượt, nhưng hơi nặng', '2024-04-02'),
    (3, 1, 3, N'Chưa thực sự hài lòng về chất lượng hình ảnh', '2024-04-03'),
    (4, 5, 4, N'Máy giặt tiết kiệm điện và làm sạch hiệu quả', '2024-04-04'),
    (5, 3, 5, N'Tủ lạnh rộng rãi và tiện ích', '2024-04-05'),
    (6, 2, 4, N'Máy điều hòa hoạt động ổn định', '2024-04-06'),
    (7, 3, 3, N'iPad mượt mà nhưng giá hơi cao', '2024-04-07'),
    (8, 4, 5, N'iPhone mạnh mẽ và đẹp', '2024-04-08'),
    (9, 6, 4, N'Máy ảnh chụp ảnh sắc nét', '2024-04-09'),
    (10, 7, 5, N'Robot hút bụi rất tiện ích', '2024-04-10'),
	(71, 12, Null, N'Rất đẹp', '2024-06-15');
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[CauHinhTichDiem] ON 
Insert Into [dbo].[CauHinhTichDiem] ([MaTichDiem], [SoTienTich], [SoDiemTich], [TrangThaiApDung])
Values 
    (1, 100000, 10, 1),
    (2, 200000, 10, 0),
    (3, 300000, 10, 0),
    (4, 400000, 10, 0),
    (5, 500000, 10, 0);
SET IDENTITY_INSERT [dbo].[CauHinhTichDiem] OFF
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[Kho] ON 
Insert Into [dbo].[Kho] ([MaKho], [TenKho], [DiaChi], [MoTa], [Lat], [Lon])
Values 
	--(1, N'Kho Bình Dương', N'123 Đường số 4, Quận 4, Bình Dương', N'Kho hàng tại Bình Dương'),
	--(2, N'Kho Long An', N'456 Đường số 5, Quận 5, Long An', N'Kho hàng tại Long An'),
	--(3, N'Kho Vũng Tàu', N'789 Đường số 6, Quận 6, Vũng Tàu', N'Kho hàng tại Vũng Tàu'),
	--(4, N'Kho Đồng Nai', N'101 Đường số 7, Quận 7, Đồng Nai', N'Kho hàng tại Đồng Nai'),
	--(5, N'Kho Bà Rịa - Vũng Tàu', N'111 Đường số 8, Quận 8, Bà Rịa - Vũng Tàu', N'Kho hàng tại Bà Rịa - Vũng Tàu'),
	--(6, N'Kho Tiền Giang', N'222 Đường số 9, Quận 9, Tiền Giang', N'Kho hàng tại Tiền Giang'),
	--(7, N'Kho Huế', N'333 Đường số 10, Quận 10, Huế', N'Kho hàng tại Huế'),
	--(8, N'Kho Cần Thơ', N'444 Đường số 11, Quận 11, Cần Thơ', N'Kho hàng tại Cần Thơ'),
	--(9, N'Kho Bắc Giang', N'555 Đường số 12, Quận 12, Bắc Giang', N'Kho hàng tại Bắc Giang'),
	--(10, N'Kho Bắc Ninh', N'666 Đường số 13, Quận 13, Bắc Ninh', N'Kho hàng tại Bắc Ninh');
	(1,	N'Kho quận 1', N'123 Mạc Đĩnh Chi, Phường Đa Kao, Quận 1, Thành phố Hồ Chí Minh, 73000, Việt Nam', N'Kho hàng tại quận 1', '10.7883712', '106.6966403'),
	(2,	N'Kho quận 5', N'1 Đường Thuận Kiều, Phường 12, Quận 5, Thành phố Hồ Chí Minh, 72415, Việt Nam',	N'Kho hàng tại quận 5', '10.7577524', '106.6584009'),
	(3,	N'Kho quận 6', N'94 Đường Lý Chiêu Hoàng, Phường 10, Quận 6, Thành phố Hồ Chí Minh, 73118, Việt Nam', N'Kho hàng tại quận 6', '10.7360230', '106.6338509'),
	(4,	N'Kho quận 7', N'132 Đường số 65, Phường Tân Phong, Quận 7, Thành phố Hồ Chí Minh, 72915, Việt Nam', N'Kho hàng tại quận 7', '10.7393720', '106.7068349'),
	(5,	N'Kho quận 8', N'7 Đường 195 Bùi Minh Trực, Phường 5, Quận 8, Thành phố Hồ Chí Minh, 72400, Việt Nam', N'Kho hàng tại quận 8', '10.7341368', '106.6585337'),
	(6,	N'Kho quận 9', N'24 Đường D3, Khu biệt thự Riviera Cove, Phường Phước Long B, Thành phố Thủ Đức, Thành phố Hồ Chí Minh, 71350, Việt Nam', N'Kho hàng tại quận 9', '10.8115192', '106.7865653'),
	(7,	N'Kho quận 10',	N'16 Đường Lê Hồng Phong, Phường 12, Quận 10, Thành phố Hồ Chí Minh, 72406, Việt Nam', N'Kho hàng tại quận 10', '10.7736509', '106.6730929'),
	(8,	N'Kho quận Tân Phú', N'140 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh, 71913, Việt Nam', N'Kho hàng tại Tân Phú', '10.8088428', '106.6182290'),
	(9,	N'Kho quận 11',	N'119 Đường Lạc Long Quân, Phường 10, Quận 11, Thành phố Hồ Chí Minh, 72000, Việt Nam', N'Kho hàng tại quận 11', '10.7628666', '106.6421519'),
	(10, N'Kho huyện Hóc Môn', N'406 Lê Thị Hà, Xã Tân Xuân, Huyện Hóc Môn, Thành phố Hồ Chí Minh, 71717, Việt Nam', N'Kho hàng tại huyện Hóc Môn', '10.8834575', '106.5993427');
SET IDENTITY_INSERT [dbo].[Kho] OFF
Go
---------------------------------------------------------------------
Insert Into [dbo].[ChiTietKho] ([MaKho], [MaSP], [SoLuongTrongKho])
Values
	(1, 1, 20), (1, 2, 15), (1, 3, 20), (1, 4, 15), (1, 5, 20), (1, 6, 15), (1, 7, 20), (1, 8, 15), (1, 9, 15), (1, 10, 15),
	(1, 11, 20), (1, 12, 15), (1, 13, 20), (1, 14, 15), (1, 15, 20), (1, 16, 15), (1, 17, 20), (1, 18, 15), (1, 19, 15), (1, 20, 15),
	(1, 21, 20), (1, 22, 15), (1, 23, 20), (1, 24, 15), (1, 25, 20), (1, 26, 15), (1, 27, 20), (1, 28, 15), (1, 29, 15), (1, 30, 15),
	(1, 31, 20), (1, 32, 15), (1, 33, 20), (1, 34, 15), (1, 35, 20), (1, 36, 15), (1, 37, 20), (1, 38, 15), (1, 39, 15), (1, 40, 15),
	(1, 41, 20), (1, 42, 15), (1, 43, 20), (1, 44, 15), (1, 45, 20), (1, 46, 15), (1, 47, 20), (1, 48, 15), (1, 49, 15), (1, 50, 15),
	(1, 51, 20), (1, 52, 15), (1, 53, 20), (1, 54, 15), (1, 55, 20), (1, 56, 15), (1, 57, 20), (1, 58, 15), (1, 59, 15), (1, 60, 15),
	(1, 61, 20), (1, 62, 15), (1, 63, 20), (1, 64, 15), (1, 65, 20), (1, 66, 15), (1, 67, 20), (1, 68, 15), (1, 69, 15), (1, 70, 15),
	(1, 71, 20), (1, 72, 15), (1, 73, 20), (1, 74, 15), (1, 75, 20), (1, 76, 15), (1, 77, 20), (1, 78, 15), (1, 79, 15), (1, 80, 15),
	(1, 81, 20), (1, 82, 15), (1, 83, 20), (1, 84, 15), (1, 85, 20), (1, 86, 15), (1, 87, 20), (1, 88, 15), (1, 89, 15), (1, 90, 15),
	(1, 91, 20), (1, 92, 15), (1, 93, 20), (1, 94, 15), (1, 95, 20), (1, 96, 15), (1, 97, 20), (1, 98, 15), (1, 99, 15), (1, 100, 15),

	(2, 1, 20), (2, 2, 15), (2, 3, 20), (2, 4, 15), (2, 5, 20), (2, 6, 15), (2, 7, 20), (2, 8, 15), (2, 9, 15), (2, 10, 15),
	(2, 11, 20), (2, 12, 15), (2, 13, 20), (2, 14, 15), (2, 15, 15), (2, 16, 15), (2, 17, 15), (2, 18, 15), (2, 19, 15), (2, 20, 15),
	(2, 21, 20), (2, 22, 15), (2, 23, 20), (2, 24, 15), (2, 25, 20), (2, 26, 15), (2, 27, 20), (2, 28, 15), (2, 29, 15), (2, 30, 15),
	(2, 31, 20), (2, 32, 15), (2, 33, 20), (2, 34, 15), (2, 35, 20), (2, 36, 15), (2, 37, 20), (2, 38, 15), (2, 39, 15), (2, 40, 15),
	(2, 41, 20), (2, 42, 15), (2, 43, 20), (2, 44, 15), (2, 45, 20), (2, 46, 15), (2, 47, 20), (2, 48, 15), (2, 49, 15), (2, 50, 15),
	(2, 51, 20), (2, 52, 15), (2, 53, 20), (2, 54, 15), (2, 55, 20), (2, 56, 15), (2, 57, 20), (2, 58, 15), (2, 59, 15), (2, 60, 15),
	(2, 61, 20), (2, 62, 15), (2, 63, 20), (2, 64, 15), (2, 65, 20), (2, 66, 15), (2, 67, 20), (2, 68, 15), (2, 69, 15), (2, 70, 15),
	(2, 71, 20), (2, 72, 15), (2, 73, 20), (2, 74, 15), (2, 75, 20), (2, 76, 15), (2, 77, 20), (2, 78, 15), (2, 79, 15), (2, 80, 15),
	(2, 81, 20), (2, 82, 15), (2, 83, 20), (2, 84, 15), (2, 85, 20), (2, 86, 15), (2, 87, 20), (2, 88, 15), (2, 89, 15), (2, 90, 15),
	(2, 91, 20), (2, 92, 15), (2, 93, 20), (2, 94, 15), (2, 95, 20), (2, 96, 15), (2, 97, 20), (2, 98, 15), (2, 99, 15), (2, 100, 15),

	(3, 1, 20), (3, 2, 15), (3, 3, 20), (3, 4, 15), (3, 5, 20), (3, 6, 15), (3, 7, 20), (3, 8, 15), (3, 9, 15), (3, 10, 15),
	(3, 11, 20), (3, 12, 15), (3, 13, 20), (3, 14, 15), (3, 15, 15), (3, 16, 15), (3, 17, 15), (3, 18, 15), (3, 19, 15), (3, 20, 15),
	(3, 21, 20), (3, 22, 15), (3, 23, 20), (3, 24, 15), (3, 25, 20), (3, 26, 15), (3, 27, 20), (3, 28, 15), (3, 29, 20), (3, 30, 15),
	(3, 31, 20), (3, 32, 15), (3, 33, 20), (3, 34, 15), (3, 35, 20), (3, 36, 15), (3, 37, 20), (3, 38, 15), (3, 39, 15), (3, 40, 15),
	(3, 41, 20), (3, 42, 15), (3, 43, 20), (3, 44, 15), (3, 45, 20), (3, 46, 15), (3, 47, 20), (3, 48, 15), (3, 49, 15), (3, 50, 15),
	(3, 51, 20), (3, 52, 15), (3, 53, 20), (3, 54, 15), (3, 55, 20), (3, 56, 15), (3, 57, 20), (3, 58, 15), (3, 59, 15), (3, 60, 15),
	(3, 61, 20), (3, 62, 15), (3, 63, 20), (3, 64, 15), (3, 65, 20), (3, 66, 15), (3, 67, 20), (3, 68, 15), (3, 69, 15), (3, 70, 15),
	(3, 71, 20), (3, 72, 15), (3, 73, 20), (3, 74, 15), (3, 75, 20), (3, 76, 15), (3, 77, 20), (3, 78, 15), (3, 79, 15), (3, 80, 15),
	(3, 81, 20), (3, 82, 15), (3, 83, 20), (3, 84, 15), (3, 85, 20), (3, 86, 15), (3, 87, 20), (3, 88, 15), (3, 89, 15), (3, 90, 15),
	(3, 91, 20), (3, 92, 15), (3, 93, 20), (3, 94, 15), (3, 95, 20), (3, 96, 15), (3, 97, 20), (3, 98, 15), (3, 99, 15), (3, 100, 15),

	(4, 1, 20), (4, 2, 15), (4, 3, 20), (4, 4, 15), (4, 5, 20), (4, 6, 15), (4, 7, 20), (4, 8, 15), (4, 9, 15), (4, 10, 15),
	(4, 11, 20), (4, 12, 15), (4, 13, 20), (4, 14, 15), (4, 15, 15), (4, 16, 15), (4, 17, 15), (4, 18, 15), (4, 19, 15), (4, 20, 15),
	(4, 21, 20), (4, 22, 15), (4, 23, 20), (4, 24, 15), (4, 25, 20), (4, 26, 15), (4, 27, 20), (4, 28, 15), (4, 29, 20), (4, 30, 15),
    (4, 31, 20), (4, 32, 15), (4, 33, 20), (4, 34, 15), (4, 35, 20), (4, 36, 15), (4, 37, 20), (4, 38, 15), (4, 39, 20), (4, 40, 15),
	(4, 41, 20), (4, 42, 15), (4, 43, 20), (4, 44, 15), (4, 45, 20), (4, 46, 15), (4, 47, 20), (4, 48, 15), (4, 49, 15), (4, 50, 15),
	(4, 51, 20), (4, 52, 15), (4, 53, 20), (4, 54, 15), (4, 55, 20), (4, 56, 15), (4, 57, 20), (4, 58, 15), (4, 59, 15), (4, 60, 15),
	(4, 61, 20), (4, 62, 15), (4, 63, 20), (4, 64, 15), (4, 65, 20), (4, 66, 15), (4, 67, 20), (4, 68, 15), (4, 69, 15), (4, 70, 15),
	(4, 71, 20), (4, 72, 15), (4, 73, 20), (4, 74, 15), (4, 75, 20), (4, 76, 15), (4, 77, 20), (4, 78, 15), (4, 79, 15), (4, 80, 15),
	(4, 81, 20), (4, 82, 15), (4, 83, 20), (4, 84, 15), (4, 85, 20), (4, 86, 15), (4, 87, 20), (4, 88, 15), (4, 89, 15), (4, 90, 15),
	(4, 91, 20), (4, 92, 15), (4, 93, 20), (4, 94, 15), (4, 95, 20), (4, 96, 15), (4, 97, 20), (4, 98, 15), (4, 99, 15), (4, 100, 15),

	(5, 1, 20), (5, 2, 15), (5, 3, 20), (5, 4, 15), (5, 5, 20), (5, 6, 15), (5, 7, 20), (5, 8, 15), (5, 9, 15), (5, 10, 15),
	(5, 11, 20), (5, 12, 15), (5, 13, 20), (5, 14, 15), (5, 15, 15), (5, 16, 15), (5, 17, 15), (5, 18, 15), (5, 19, 15), (5, 20, 15),
	(5, 21, 20), (5, 22, 15), (5, 23, 20), (5, 24, 15), (5, 25, 20), (5, 26, 15), (5, 27, 20), (5, 28, 15), (5, 29, 20), (5, 30, 15),
    (5, 31, 20), (5, 32, 15), (5, 33, 20), (5, 34, 15), (5, 35, 20), (5, 36, 15), (5, 37, 20), (5, 38, 15), (5, 39, 20), (5, 40, 15),
    (5, 41, 25), (5, 42, 30), (5, 43, 25), (5, 44, 30), (5, 45, 20), (5, 46, 15), (5, 47, 20), (5, 48, 15), (5, 49, 20), (5, 50, 15),
	(5, 51, 20), (5, 52, 15), (5, 53, 20), (5, 54, 15), (5, 55, 20), (5, 56, 15), (5, 57, 20), (5, 58, 15), (5, 59, 15), (5, 60, 15),
	(5, 61, 20), (5, 62, 15), (5, 63, 20), (5, 64, 15), (5, 65, 20), (5, 66, 15), (5, 67, 20), (5, 68, 15), (5, 69, 15), (5, 70, 15),
	(5, 71, 20), (5, 72, 15), (5, 73, 20), (5, 74, 15), (5, 75, 20), (5, 76, 15), (5, 77, 20), (5, 78, 15), (5, 79, 15), (5, 80, 15),
	(5, 81, 20), (5, 82, 15), (5, 83, 20), (5, 84, 15), (5, 85, 20), (5, 86, 15), (5, 87, 20), (5, 88, 15), (5, 89, 15), (5, 90, 15),
	(5, 91, 20), (5, 92, 15), (5, 93, 20), (5, 94, 15), (5, 95, 20), (5, 96, 15), (5, 97, 20), (5, 98, 15), (5, 99, 15), (5, 100, 15),

	(6, 1, 20), (6, 2, 15), (6, 3, 20), (6, 4, 15), (6, 5, 20), (6, 6, 15), (6, 7, 20), (6, 8, 15), (6, 9, 15), (6, 10, 15),
	(6, 11, 20), (6, 12, 15), (6, 13, 20), (6, 14, 15), (6, 15, 15), (6, 16, 15), (6, 17, 15), (6, 18, 15), (6, 19, 15), (6, 20, 15),
	(6, 21, 20), (6, 22, 15), (6, 23, 20), (6, 24, 15), (6, 25, 20), (6, 26, 15), (6, 27, 20), (6, 28, 15), (6, 29, 20), (6, 30, 15),
    (6, 31, 20), (6, 32, 15), (6, 33, 20), (6, 34, 15), (6, 35, 20), (6, 36, 15), (6, 37, 20), (6, 38, 15), (6, 39, 20), (6, 40, 15),
    (6, 41, 25), (6, 42, 30), (6, 43, 25), (6, 44, 30), (6, 45, 20), (6, 46, 15), (6, 47, 20), (6, 48, 15), (6, 49, 20), (6, 50, 15),
    (6, 51, 40), (6, 52, 35), (6, 53, 40), (6, 54, 35), (6, 55, 20), (6, 56, 15), (6, 57, 20), (6, 58, 15), (6, 59, 20), (6, 60, 15),
	(6, 61, 20), (6, 62, 15), (6, 63, 20), (6, 64, 15), (6, 65, 20), (6, 66, 15), (6, 67, 20), (6, 68, 15), (6, 69, 15), (6, 70, 15),
	(6, 71, 20), (6, 72, 15), (6, 73, 20), (6, 74, 15), (6, 75, 20), (6, 76, 15), (6, 77, 20), (6, 78, 15), (6, 79, 15), (6, 80, 15),
	(6, 81, 20), (6, 82, 15), (6, 83, 20), (6, 84, 15), (6, 85, 20), (6, 86, 15), (6, 87, 20), (6, 88, 15), (6, 89, 15), (6, 90, 15),
	(6, 91, 20), (6, 92, 15), (6, 93, 20), (6, 94, 15), (6, 95, 20), (6, 96, 15), (6, 97, 20), (6, 98, 15), (6, 99, 15), (6, 100, 15),

	(7, 1, 20), (7, 2, 15), (7, 3, 20), (7, 4, 15), (7, 5, 20), (7, 6, 15), (7, 7, 20), (7, 8, 15), (7, 9, 15), (7, 10, 15),
	(7, 11, 20), (7, 12, 15), (7, 13, 20), (7, 14, 15), (7, 15, 15), (7, 16, 15), (7, 17, 15), (7, 18, 15), (7, 19, 15), (7, 20, 15),
	(7, 21, 20), (7, 22, 15), (7, 23, 20), (7, 24, 15), (7, 25, 20), (7, 26, 15), (7, 27, 20), (7, 28, 15), (7, 29, 20), (7, 30, 15),
    (7, 31, 20), (7, 32, 15), (7, 33, 20), (7, 34, 15), (7, 35, 20), (7, 36, 15), (7, 37, 20), (7, 38, 15), (7, 39, 20), (7, 40, 15),
    (7, 41, 25), (7, 42, 30), (7, 43, 25), (7, 44, 30), (7, 45, 20), (7, 46, 15), (7, 47, 20), (7, 48, 15), (7, 49, 20), (7, 50, 15),
    (7, 51, 40), (7, 52, 35), (7, 53, 40), (7, 54, 35), (7, 55, 20), (7, 56, 15), (7, 57, 20), (7, 58, 15), (7, 59, 20), (7, 60, 15),
    (7, 61, 50), (7, 62, 45), (7, 63, 50), (7, 64, 45), (7, 65, 20), (7, 66, 15), (7, 67, 20), (7, 68, 15), (7, 69, 20), (7, 70, 15),
	(7, 71, 20), (7, 72, 15), (7, 73, 20), (7, 74, 15), (7, 75, 20), (7, 76, 15), (7, 77, 20), (7, 78, 15), (7, 79, 15), (7, 80, 15),
	(7, 81, 20), (7, 82, 15), (7, 83, 20), (7, 84, 15), (7, 85, 20), (7, 86, 15), (7, 87, 20), (7, 88, 15), (7, 89, 15), (7, 90, 15),
	(7, 91, 20), (7, 92, 15), (7, 93, 20), (7, 94, 15), (7, 95, 20), (7, 96, 15), (7, 97, 20), (7, 98, 15), (7, 99, 15), (7, 100, 15),

	(8, 1, 20), (8, 2, 15), (8, 3, 20), (8, 4, 15), (8, 5, 20), (8, 6, 15), (8, 7, 20), (8, 8, 15), (8, 9, 15), (8, 10, 15),
	(8, 11, 20), (8, 12, 15), (8, 13, 20), (8, 14, 15), (8, 15, 15), (8, 16, 15), (8, 17, 15), (8, 18, 15), (8, 19, 15), (8, 20, 15),
	(8, 21, 20), (8, 22, 15), (8, 23, 20), (8, 24, 15), (8, 25, 20), (8, 26, 15), (8, 27, 20), (8, 28, 15), (8, 29, 20), (8, 30, 15),
    (8, 31, 20), (8, 32, 15), (8, 33, 20), (8, 34, 15), (8, 35, 20), (8, 36, 15), (8, 37, 20), (8, 38, 15), (8, 39, 20), (8, 40, 15),
    (8, 41, 25), (8, 42, 30), (8, 43, 25), (8, 44, 30), (8, 45, 20), (8, 46, 15), (8, 47, 20), (8, 48, 15), (8, 49, 20), (8, 50, 15),
    (8, 51, 40), (8, 52, 35), (8, 53, 40), (8, 54, 35), (8, 55, 20), (8, 56, 15), (8, 57, 20), (8, 58, 15), (8, 59, 20), (8, 60, 15),
    (8, 61, 50), (8, 62, 45), (8, 63, 50), (8, 64, 45), (8, 65, 20), (8, 66, 15), (8, 67, 20), (8, 68, 15), (8, 69, 20), (8, 70, 15),
    (8, 71, 60), (8, 72, 55), (8, 73, 60), (8, 74, 55), (8, 75, 20), (8, 76, 15), (8, 77, 20), (8, 78, 15), (8, 79, 20), (8, 80, 15),
	(8, 81, 20), (8, 82, 15), (8, 83, 20), (8, 84, 15), (8, 85, 20), (8, 86, 15), (8, 87, 20), (8, 88, 15), (8, 89, 15), (8, 90, 15),
	(8, 91, 20), (8, 92, 15), (8, 93, 20), (8, 94, 15), (8, 95, 20), (8, 96, 15), (8, 97, 20), (8, 98, 15), (8, 99, 15), (8, 100, 15),

	(9, 1, 20), (9, 2, 15), (9, 3, 20), (9, 4, 15), (9, 5, 20), (9, 6, 15), (9, 7, 20), (9, 8, 15), (9, 9, 15), (9, 10, 15),
	(9, 11, 20), (9, 12, 15), (9, 13, 20), (9, 14, 15), (9, 15, 15), (9, 16, 15), (9, 17, 15), (9, 18, 15), (9, 19, 15), (9, 20, 15),
	(9, 21, 20), (9, 22, 15), (9, 23, 20), (9, 24, 15), (9, 25, 20), (9, 26, 15), (9, 27, 20), (9, 28, 15), (9, 29, 20), (9, 30, 15),
    (9, 31, 20), (9, 32, 15), (9, 33, 20), (9, 34, 15), (9, 35, 20), (9, 36, 15), (9, 37, 20), (9, 38, 15), (9, 39, 20), (9, 40, 15),
    (9, 41, 25), (9, 42, 30), (9, 43, 25), (9, 44, 30), (9, 45, 20), (9, 46, 15), (9, 47, 20), (9, 48, 15), (9, 49, 20), (9, 50, 15),
    (9, 51, 40), (9, 52, 35), (9, 53, 40), (9, 54, 35), (9, 55, 20), (9, 56, 15), (9, 57, 20), (9, 58, 15), (9, 59, 20), (9, 60, 15),
    (9, 61, 50), (9, 62, 45), (9, 63, 50), (9, 64, 45), (9, 65, 20), (9, 66, 15), (9, 67, 20), (9, 68, 15), (9, 69, 20), (9, 70, 15),
    (9, 71, 60), (9, 72, 55), (9, 73, 60), (9, 74, 55), (9, 75, 20), (9, 76, 15), (9, 77, 20), (9, 78, 15), (9, 79, 20), (9, 80, 15),
	(9, 81, 20), (9, 82, 15), (9, 83, 20), (9, 84, 15), (9, 85, 20), (9, 86, 15), (9, 87, 20), (9, 88, 15), (9, 89, 20), (9, 90, 15),
	(9, 91, 20), (9, 92, 15), (9, 93, 20), (9, 94, 15), (9, 95, 20), (9, 96, 15), (9, 97, 20), (9, 98, 15), (9, 99, 15), (9, 100, 15),

	(10, 1, 20), (10, 2, 15), (10, 3, 20), (10, 4, 15), (10, 5, 20), (10, 6, 15), (10, 7, 20), (10, 8, 15), (10, 9, 15), (10, 10, 15),
	(10, 11, 20), (10, 12, 15), (10, 13, 20), (10, 14, 15), (10, 15, 15), (10, 16, 15), (10, 17, 15), (10, 18, 15), (10, 19, 15), (10, 20, 15),
	(10, 21, 20), (10, 22, 15), (10, 23, 20), (10, 24, 15), (10, 25, 20), (10, 26, 15), (10, 27, 20), (10, 28, 15), (10, 29, 20), (10, 30, 15),
    (10, 31, 20), (10, 32, 15), (10, 33, 20), (10, 34, 15), (10, 35, 20), (10, 36, 15), (10, 37, 20), (10, 38, 15), (10, 39, 20), (10, 40, 15),
    (10, 41, 25), (10, 42, 30), (10, 43, 25), (10, 44, 30), (10, 45, 20), (10, 46, 15), (10, 47, 20), (10, 48, 15), (10, 49, 20), (10, 50, 15),
    (10, 51, 40), (10, 52, 35), (10, 53, 40), (10, 54, 35), (10, 55, 20), (10, 56, 15), (10, 57, 20), (10, 58, 15), (10, 59, 20), (10, 60, 15),
    (10, 61, 50), (10, 62, 45), (10, 63, 50), (10, 64, 45), (10, 65, 20), (10, 66, 15), (10, 67, 20), (10, 68, 15), (10, 69, 20), (10, 70, 15),
    (10, 71, 60), (10, 72, 55), (10, 73, 60), (10, 74, 55), (10, 75, 20), (10, 76, 15), (10, 77, 20), (10, 78, 15), (10, 79, 20), (10, 80, 15),
	(10, 81, 20), (10, 82, 15), (10, 83, 20), (10, 84, 15), (10, 85, 20), (10, 86, 15), (10, 87, 20), (10, 88, 15), (10, 89, 20), (10, 90, 15),
	(10, 91, 20), (10, 92, 15), (10, 93, 20), (10, 94, 15), (10, 95, 20), (10, 96, 15), (10, 97, 20), (10, 98, 15), (10, 99, 20), (10, 100, 15);
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[HinhThucBanHang] ON 
Insert Into [dbo].[HinhThucBanHang] ([MaHT], [LoaiHinhThuc])
Values
    (1, N'Hoá đơn trực tiếp'),
	(2, N'Hoá đơn trực tuyến');
SET IDENTITY_INSERT [dbo].[HinhThucBanHang] OFF
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[TrangThaiHoaDon] ON 
Insert Into [dbo].[TrangThaiHoaDon] ([MaTrangThaiHD], [MoTaTrangThai])
Values
    (1, N'Chưa duyệt'),
	(2, N'Đã duyệt'),
	(3, N'Đang vận chyển'),
	(4, N'Giao thành công'),
	(5, N'Giao hàng thất bại'),
	(6, N'Trả hàng'),
	(7, N'Đã thanh toán');
SET IDENTITY_INSERT [dbo].[TrangThaiHoaDon] OFF
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[HoaDon] ON 
Insert Into [dbo].[HoaDon] ([MaHD], [NgayLap], [TongTien], [DiaChiGiaoHang], [GhiChu], [DiemTichDuoc], [MaTichDiem], [MaNV], [MaKH], [MaKho], [MaTrangThaiHD],  [MaHT], [TrangThaiXoa])
Values
    (1, '2024-10-01', 29990000, N'123 Đường ABC, Quận 2, Thành phố HCM', Null, 0, 1, 4, 1, 1, 7, 1, 1),
    (2, '2024-09-15', 59990000, N'456 Đường DEF, Quận Tân Phú, Thành phố HCM', Null, 0, 2, 4, 2, 1, 7, 1, 1),
    (3, '2024-08-20', 19999000, N'789 Đường GHI, Quận Tân Phú, Thành phố HCM', Null, 0, 1, 4, 3, 1, 7, 1, 1),
    (4, '2024-07-10', 14990000, N'101 Đường JKL, Quận 3, Thành phố HCM', Null, 0, 3, 8, 4, 2, 7, 1, 1),
    (5, '2024-06-05', 24990000, N'202 Đường STU, Quận 5, Thành phố HCM', Null, 0, 2, 8, 5, 2, 7, 1, 1),
    (6, '2024-05-15', 9990000, N'303 Đường YZA, Quận Tân Bình, Thành phố HCM', Null, 0, 1, 8, 6, 3, 7, 1, 1),
    (7, '2024-04-20', 23990000, N'404 Đường EFG, Quận 5, Thành phố HCM', Null, 0, 4, 8, 7, 3, 7, 1, 1),
    (8, '2024-03-10', 499000, N'505 Đường KLM, Quận 7, Thành phố HCM', Null, 0, 5, 11, 8, 4, 7, 1, 1),
    (9, '2024-02-05', 31990000, N'606 Đường QRS, Quận 1, Thành phố HCM', Null, 0, 3, 11, 9, 5, 7, 1, 1),
    (10, '2024-01-15', 3490000, N'707 Đường WXY, Quận 1, Thành phố HCM', Null, 0, 2, 11, 11, 5, 7, 1, 1),
	(11, '2024-06-12', 58470000, N'140 Lê Trọng Tấn Tân Phú', NULL, NULL, NULL, 1, 12, 5, 7, 2, 1),
	(12, '2024-06-13', 58470000, N'140 Lê Trọng Tấn Tân Phú', NULL, NULL, NULL, 1, 12, 6, 7, 2, 1),
	(13, '2024-06-13', 58470000, N'144 Đường Lê Trọng Tấn, Tây Thạnh, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 6, 7, 2, 1),
	(14, '2024-06-15', 58470000, N'144 Đường Lê Trọng Tấn, Tây Thạnh, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 6, 7, 2, 1),
	(15, '2024-06-15', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 7, 7, 2, 1),
	(16, '2024-06-15', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 7, 7, 2, 1),
	(17, '2024-06-13', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 7, 7, 2, 1),
	(18, '2024-06-13', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 7, 7, 2, 1),
	(19, '2024-06-11', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 8, 7, 2, 1),
	(20, '2024-06-12', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 8, 7, 2, 1),
	(21, '2024-06-12', 58470000, N'144 Lê Trọng Tấn, Tây Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 8, 7, 2, 1),
	(22, '2024-06-16', 53980000, N'144 Lê Trọng Tân, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 9, 7, 2, 1),
	(23, '2024-06-16', 53980000, N'144 Lê Trọng Tân, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 9, 7, 2, 1),
	(24, '2024-06-16', 53980000, N'144 Lê Trọng Tân, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 10, 7, 2, 1),
	(25, '2024-06-16', 53980000, N'144 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 10, 7, 2, 1),
	(26, '2024-06-17', 26990000, N'144 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 10, 7, 2, 1),
	(27, '2024-06-17', 70970000, N'144 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', N'Cẩn thận', NULL, NULL, 1, 12, 5, 2, 2, 1),
	(28, '2024-06-17', 16990000, N'168 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 4, 2, 2, 1),
	(29, '2024-06-17', 43980000, N'164 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', N'Cẩn thận', NULL, NULL, 2, 12, 4, 2, 2, 1),
	(30, '2024-06-17', 26990000, N'144 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 3, 7, 2, 1),
	(31, '2024-06-17', 48582000, N'144 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 6, 7, 2, 1),
	(32, '2024-06-17', 48582000, N'164 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', N'Cẩn thận giúp anh', NULL, NULL, 1, 12, 8, 7, 2, 1),

	(33, '2024-06-01', 32990000, N'500 Đường A, Quận 1, Thành phố HCM', Null, 0, 1, 4, 1, 1, 7, 1, 1),
    (34, '2024-06-02', 41990000, N'501 Đường B, Quận 2, Thành phố HCM', Null, 0, 2, 8, 2, 2, 7, 1, 1),
    (35, '2024-06-03', 52990000, N'502 Đường C, Quận 3, Thành phố HCM', Null, 0, 3, 8, 3, 2, 7, 1, 1),
    (36, '2024-06-04', 38990000, N'503 Đường D, Quận 4, Thành phố HCM', Null, 0, 4, 8, 4, 1, 7, 1, 1),
    (37, '2024-06-05', 44990000, N'504 Đường E, Quận 5, Thành phố HCM', Null, 0, 5, 11, 5, 1, 7, 1, 1),
    (38, '2024-06-06', 55990000, N'505 Đường F, Quận 6, Thành phố HCM', Null, 0, 1, 8, 6, 3, 7, 1, 1),
    (39, '2024-06-07', 47990000, N'506 Đường G, Quận 7, Thành phố HCM', Null, 0, 2, 11, 7, 3, 7, 1, 1),
    (40, '2024-06-08', 31990000, N'507 Đường H, Quận 8, Thành phố HCM', Null, 0, 3, 11, 8, 3, 7, 1, 1),
    (41, '2024-06-09', 29990000, N'508 Đường I, Quận 9, Thành phố HCM', Null, 0, 4, 11, 9, 4, 7, 1, 1),
    (42, '2024-06-10', 33990000, N'509 Đường J, Quận 10, Thành phố HCM', Null, 0, 5, 8, 10, 4, 7, 1, 1),
    (43, '2024-06-11', 42990000, N'510 Đường K, Quận 11, Thành phố HCM', Null, 0, 1, 8, 11, 5, 7, 1, 1),
    (44, '2024-06-12', 31990000, N'511 Đường L, Quận 12, Thành phố HCM', Null, 0, 2, 8, 12, 5, 7, 1, 1),
    (45, '2024-06-13', 28990000, N'512 Đường M, Quận 1, Thành phố HCM', Null, 0, 3, 4, 1, 4, 7, 1, 1),
    (46, '2024-06-14', 49990000, N'513 Đường N, Quận 2, Thành phố HCM', Null, 0, 4, 4, 2, 4, 7, 1, 1),
    (47, '2024-06-15', 32990000, N'514 Đường O, Quận 3, Thành phố HCM', Null, 0, 5, 4, 3, 6, 7, 1, 1),
    (48, '2024-06-16', 43990000, N'515 Đường P, Quận 4, Thành phố HCM', Null, 0, 1, 8, 4, 6, 7, 1, 1),
    (49, '2024-06-17', 28990000, N'516 Đường Q, Quận 5, Thành phố HCM', Null, 0, 2, 4, 5, 2, 7, 1, 1),
    (50, '2024-06-18', 56990000, N'517 Đường R, Quận 6, Thành phố HCM', Null, 0, 3, 11, 6, 7, 7, 1, 1),
    (51, '2024-06-19', 42990000, N'518 Đường S, Quận 7, Thành phố HCM', Null, 0, 4, 11, 7, 8, 7, 1, 1),
    (52, '2024-06-20', 38990000, N'519 Đường T, Quận 8, Thành phố HCM', Null, 0, 5, 4, 8, 8, 7, 1, 1),
    (53, '2024-06-21', 46990000, N'520 Đường U, Quận 9, Thành phố HCM', Null, 0, 1, 4, 9, 8, 7, 1, 1),
    (54, '2024-06-22', 31990000, N'521 Đường V, Quận 10, Thành phố HCM', Null, 0, 2, 4, 10, 9, 7, 1, 1),
    (55, '2024-06-23', 34990000, N'522 Đường W, Quận 11, Thành phố HCM', Null, 0, 3, 8, 11, 10, 7, 1, 1),
    (56, '2024-06-24', 52990000, N'523 Đường X, Quận 12, Thành phố HCM', Null, 0, 4, 8, 12, 10, 7, 1, 1),
    (57, '2024-06-25', 38990000, N'524 Đường Y, Quận 1, Thành phố HCM', Null, 0, 5, 8, 1, 9, 7, 1, 1),
    (58, '2024-06-26', 31990000, N'525 Đường Z, Quận 2, Thành phố HCM', Null, 0, 1, 8, 2, 7, 7, 1, 1),
    (59, '2024-06-27', 34990000, N'526 Đường AA, Quận 3, Thành phố HCM', Null, 0, 2, 11, 3, 7, 7, 1, 1),
	(60, '2024-06-25', 35990000, N'527 Đường AB, Quận 4, Thành phố HCM', Null, 0, 3, 11, 4, 1, 7, 1, 1),
    (61, '2024-06-26', 32990000, N'528 Đường AC, Quận 5, Thành phố HCM', Null, 0, 4, 11, 5, 1, 7, 1, 1),
    (62, '2024-06-27', 41990000, N'529 Đường AD, Quận 6, Thành phố HCM', Null, 0, 5, 8, 6, 1, 7, 1, 1),
    (63, '2024-06-01', 52990000, N'530 Đường AE, Quận 7, Thành phố HCM', Null, 0, 1, 8, 7, 1, 7, 1, 1),
    (64, '2024-06-02', 38990000, N'531 Đường AF, Quận 8, Thành phố HCM', Null, 0, 2, 4, 8, 2, 7, 1, 1),
    (65, '2024-06-03', 44990000, N'532 Đường AG, Quận 9, Thành phố HCM', Null, 0, 3, 4, 9, 2, 7, 1, 1),
    (66, '2024-06-04', 55990000, N'533 Đường AH, Quận 10, Thành phố HCM', Null, 0, 4, 4, 10, 2, 7, 1, 1),
    (67, '2024-06-05', 47990000, N'534 Đường AI, Quận 11, Thành phố HCM', Null, 0, 5, 4, 11, 2, 7, 1, 1),
    (68, '2024-06-06', 31990000, N'535 Đường AJ, Quận 12, Thành phố HCM', Null, 0, 1, 8, 12, 2, 7, 1, 1),
    (69, '2024-06-07', 29990000, N'536 Đường AK, Quận 1, Thành phố HCM', Null, 0, 2, 8, 1, 3, 7, 1, 1),
    (70, '2024-06-08', 33990000, N'537 Đường AL, Quận 2, Thành phố HCM', Null, 0, 3, 11, 2, 3, 7, 1, 1),
    (71, '2024-06-09', 42990000, N'538 Đường AM, Quận 3, Thành phố HCM', Null, 0, 4, 11, 3, 3, 7, 1, 1),
    (72, '2024-06-10', 31990000, N'539 Đường AN, Quận 4, Thành phố HCM', Null, 0, 5, 8, 4, 5, 7, 1, 1),
    (73, '2024-06-11', 28990000, N'540 Đường AO, Quận 5, Thành phố HCM', Null, 0, 1, 8, 5, 5, 7, 1, 1),
    (74, '2024-06-12', 49990000, N'541 Đường AP, Quận 6, Thành phố HCM', Null, 0, 2, 4, 6, 6, 7, 1, 1),
    (75, '2024-06-13', 32990000, N'542 Đường AQ, Quận 7, Thành phố HCM', Null, 0, 3, 4, 7, 6, 7, 1, 1),
    (76, '2024-06-14', 43990000, N'543 Đường AR, Quận 8, Thành phố HCM', Null, 0, 4, 4, 8, 7, 7, 1, 1),
    (77, '2024-06-15', 28990000, N'544 Đường AS, Quận 9, Thành phố HCM', Null, 0, 5, 4, 9, 4, 7, 1, 1),
    (78, '2024-06-16', 56990000, N'545 Đường AT, Quận 10, Thành phố HCM', Null, 0, 1, 8, 10, 7, 7, 1, 1),
    (79, '2024-06-17', 42990000, N'546 Đường AU, Quận 11, Thành phố HCM', Null, 0, 2, 8, 11, 7, 7, 1, 1),
    (80, '2024-06-18', 38990000, N'547 Đường AV, Quận 12, Thành phố HCM', Null, 0, 3, 11, 12, 8, 7, 1, 1),
    (81, '2024-06-19', 46990000, N'548 Đường AW, Quận 1, Thành phố HCM', Null, 0, 4, 11, 1, 9, 7, 1, 1),
    (82, '2024-06-20', 31990000, N'549 Đường AX, Quận 2, Thành phố HCM', Null, 0, 5, 11, 2, 10, 7, 1, 1),

	(83, '2024-07-01', 58470000, N'140 Lê Trọng Tấn Tân Phú', NULL, NULL, NULL, 1, 12, 5, 7, 2, 1),
	(84, '2024-07-01', 58470000, N'140 Lê Trọng Tấn Tân Phú', NULL, NULL, NULL, 1, 12, 6, 7, 2, 1),
	(85, '2024-07-01', 58470000, N'144 Đường Lê Trọng Tấn, Tây Thạnh, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 6, 7, 2, 1),
	(86, '2024-07-01', 58470000, N'144 Đường Lê Trọng Tấn, Tây Thạnh, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 6, 7, 2, 1),
	(87, '2024-07-01', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 7, 7, 2, 1),
	(88, '2024-07-01', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 7, 7, 2, 1),
	(89, '2024-07-02', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 7, 7, 2, 1),
	(90, '2024-07-02', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 7, 7, 2, 1),
	(91, '2024-07-02', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 8, 7, 2, 1),
	(92, '2024-07-02', 58470000, N'140 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 8, 7, 2, 1),
	(93, '2024-07-02', 58470000, N'144 Lê Trọng Tấn, Tây Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 8, 7, 2, 1),
	(94, '2024-07-03', 53980000, N'144 Lê Trọng Tân, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 9, 7, 2, 1),
	(95, '2024-07-03', 53980000, N'144 Lê Trọng Tân, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 9, 7, 2, 1),
	(96, '2024-07-03', 53980000, N'144 Lê Trọng Tân, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 10, 7, 2, 1),
	(97, '2024-07-03', 53980000, N'144 Lê Trọng Tấn, Tân Phú, Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 10, 7, 2, 1),
	(98, '2024-07-03', 26990000, N'144 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 10, 7, 2, 1),
	(99, '2024-07-03', 70970000, N'144 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', N'Cẩn thận', NULL, NULL, 1, 12, 5, 2, 2, 1),
	(100, '2024-07-04', 16990000, N'168 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 4, 2, 2, 1),
	(101, '2024-07-04', 43980000, N'164 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', N'Cẩn thận', NULL, NULL, 2, 12, 4, 2, 2, 1),
	(102, '2024-07-04', 26990000, N'144 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 3, 7, 2, 1),
	(103, '2024-07-04', 48582000, N'144 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', NULL, NULL, NULL, 1, 12, 6, 7, 2, 1),
	(104, '2024-07-04', 48582000, N'164 Lê Trọng Tấn, Phường Tây Thạnh, Quận Tân Phú, Thành phố Hồ Chí Minh', N'Cẩn thận giúp anh', NULL, NULL, 1, 12, 8, 7, 2, 1);
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
Go
---------------------------------------------------------------------
Insert Into [dbo].[ChiTietHoaDon] ([MaHD], [MaSP], [SoLuong], [DonGia], [ThanhTien])
Values 
    (1, 1, 1, 29990000, 29990000),
    (2, 2, 1, 59990000, 59990000),
    (3, 3, 1, 19999000, 19999000),
    (4, 4, 1, 14990000, 14990000),
    (5, 5, 1, 24990000, 24990000),
    (6, 6, 1, 9990000, 9990000),
    (7, 7, 1, 23990000, 23990000),
    (8, 8, 1, 499000, 499000),
    (9, 9, 1, 31990000, 31990000),
    (10, 10, 1, 3490000, 3490000),
	(11, 96, 1, 26990000, 26990000),
	(11, 99, 1, 6490000, 6490000),
	(11, 100, 1, 24990000, 24990000),
	(12, 96, 1, 26990000, 26990000),
	(12, 99, 1, 6490000, 6490000),
	(12, 100, 1, 24990000, 24990000),
	(13, 96, 1, 26990000, 26990000),
	(13, 99, 1, 6490000, 6490000),
	(13, 100, 1, 24990000, 24990000),
	(14, 96, 1, 26990000, 26990000),
	(14, 99, 1, 6490000, 6490000),
	(14, 100, 1, 24990000, 24990000),
	(15, 96, 1, 26990000, 26990000),
	(15, 99, 1, 6490000, 6490000),
	(15, 100, 1, 24990000, 24990000),
	(16, 96, 1, 26990000, 26990000),
	(16, 99, 1, 6490000, 6490000),
	(16, 100, 1, 24990000, 24990000),
	(17, 96, 1, 26990000, 26990000),
	(17, 99, 1, 6490000, 6490000),
	(17, 100, 1, 24990000, 24990000),
	(18, 96, 1, 26990000, 26990000),
	(18, 99, 1, 6490000, 6490000),
	(18, 100, 1, 24990000, 24990000),
	(19, 96, 1, 26990000, 26990000),
	(19, 99, 1, 6490000, 6490000),
	(19, 100, 1, 24990000, 24990000),
	(20, 96, 1, 26990000, 26990000),
	(20, 99, 1, 6490000, 6490000),
	(20, 100, 1, 24990000, 24990000),
	(21, 96, 1, 26990000, 26990000),
	(21, 99, 1, 6490000, 6490000),
	(21, 100, 1, 24990000, 24990000),
	(22, 71, 2, 26990000, 53980000),
	(23, 71, 2, 26990000, 53980000),
	(24, 71, 2, 26990000, 53980000),
	(25, 71, 2, 26990000, 53980000),
	(26, 71, 1, 26990000, 26990000),
	(27, 71, 2, 26990000, 53980000),
	(27, 73, 1, 16990000, 16990000),
	(28, 73, 1, 16990000, 16990000),
	(29, 71, 1, 26990000, 26990000),
	(29, 73, 1, 16990000, 16990000),
	(30, 71, 1, 26990000, 26990000),
	(31, 71, 2, 24291000, 48582000),
	(32, 71, 2, 24291000, 48582000),

	(33, 1, 1, 14990000, 14990000),
    (33, 2, 1, 18000000, 18000000),
    (34, 3, 1, 20990000, 20990000),
    (34, 4, 1, 21000000, 21000000),
    (35, 5, 1, 25990000, 25990000),
    (35, 6, 1, 27000000, 27000000),
    (36, 7, 1, 18990000, 18990000),
    (36, 8, 1, 20000000, 20000000),
    (37, 9, 1, 22990000, 22990000),
    (37, 10, 1, 22000000, 22000000),
    (38, 11, 1, 25990000, 25990000),
    (38, 12, 1, 30000000, 30000000),
    (39, 13, 1, 23990000, 23990000),
    (39, 14, 1, 24000000, 24000000),
    (40, 15, 1, 17990000, 17990000),
    (40, 16, 1, 14000000, 14000000),
    (41, 17, 1, 14990000, 14990000),
    (41, 18, 1, 15000000, 15000000),
    (42, 19, 1, 16990000, 16990000),
    (42, 20, 1, 17000000, 17000000),
    (43, 21, 1, 19990000, 19990000),
    (43, 22, 1, 20000000, 20000000),
    (44, 23, 1, 12990000, 12990000),
    (44, 24, 1, 19000000, 19000000),
    (45, 25, 1, 13990000, 13990000),
    (45, 26, 1, 15000000, 15000000),
    (46, 27, 1, 24990000, 24990000),
    (46, 28, 1, 25000000, 25000000),
    (47, 29, 1, 17990000, 17990000),
    (47, 30, 1, 15000000, 15000000),
    (48, 31, 1, 19990000, 19990000),
    (48, 32, 1, 24000000, 24000000),
    (49, 33, 1, 13990000, 13990000),
    (49, 34, 1, 15000000, 15000000),
    (50, 35, 1, 30990000, 30990000),
    (50, 36, 1, 26000000, 26000000),
    (51, 37, 1, 19990000, 19990000),
    (51, 38, 1, 23000000, 23000000),
    (52, 39, 1, 18990000, 18990000),
    (52, 40, 1, 20000000, 20000000),
    (53, 41, 1, 22990000, 22990000),
    (53, 42, 1, 24000000, 24000000),
    (54, 43, 1, 17990000, 17990000),
    (54, 44, 1, 14000000, 14000000),
    (55, 45, 1, 14990000, 14990000),
    (55, 46, 1, 20000000, 20000000),
    (56, 47, 1, 28990000, 28990000),
    (56, 48, 1, 24000000, 24000000),
    (57, 49, 1, 17990000, 17990000),
    (57, 50, 1, 21000000, 21000000),
    (58, 51, 1, 13990000, 13990000),
    (58, 52, 1, 18000000, 18000000),
    (59, 53, 1, 14990000, 14990000),
    (59, 54, 1, 20000000, 20000000),
	(60, 55, 1, 14990000, 14990000),
    (60, 56, 1, 21000000, 21000000),
    (61, 57, 1, 15990000, 15990000),
    (61, 58, 1, 17000000, 17000000),
    (62, 59, 1, 20990000, 20990000),
    (62, 60, 1, 21000000, 21000000),
    (63, 61, 1, 25990000, 25990000),
    (63, 62, 1, 27000000, 27000000),
    (64, 63, 1, 18990000, 18990000),
    (64, 64, 1, 20000000, 20000000),
    (65, 65, 1, 22990000, 22990000),
    (65, 66, 1, 22000000, 22000000),
    (66, 67, 1, 25990000, 25990000),
    (66, 68, 1, 30000000, 30000000),
    (67, 69, 1, 23990000, 23990000),
    (67, 70, 1, 24000000, 24000000),
    (68, 71, 1, 17990000, 17990000),
    (68, 72, 1, 14000000, 14000000),
    (69, 73, 1, 14990000, 14990000),
    (69, 74, 1, 15000000, 15000000),
    (70, 75, 1, 16990000, 16990000),
    (70, 76, 1, 17000000, 17000000),
    (71, 77, 1, 19990000, 19990000),
    (71, 78, 1, 20000000, 20000000),
    (72, 79, 1, 12990000, 12990000),
    (72, 80, 1, 19000000, 19000000),
    (73, 81, 1, 13990000, 13990000),
    (73, 82, 1, 15000000, 15000000),
    (74, 83, 1, 24990000, 24990000),
    (74, 84, 1, 25000000, 25000000),
    (75, 85, 1, 17990000, 17990000),
    (75, 86, 1, 15000000, 15000000),
    (76, 87, 1, 19990000, 19990000),
    (76, 88, 1, 24000000, 24000000),
    (77, 89, 1, 13990000, 13990000),
    (77, 90, 1, 15000000, 15000000),
    (78, 91, 1, 30990000, 30990000),
    (78, 92, 1, 26000000, 26000000),
    (79, 93, 1, 19990000, 19990000),
    (79, 94, 1, 23000000, 23000000),
    (80, 95, 1, 18990000, 18990000),
    (80, 96, 1, 20000000, 20000000),
    (81, 97, 1, 22990000, 22990000),
    (81, 98, 1, 24000000, 24000000),
    (82, 99, 1, 17990000, 17990000),
    (82, 100, 1, 14000000, 14000000),

	(83, 96, 1, 26990000, 26990000),
	(83, 99, 1, 6490000, 6490000),
	(83, 100, 1, 24990000, 24990000),
	(84, 96, 1, 26990000, 26990000),
	(84, 99, 1, 6490000, 6490000),
	(84, 100, 1, 24990000, 24990000),
	(85, 96, 1, 26990000, 26990000),
	(85, 99, 1, 6490000, 6490000),
	(85, 100, 1, 24990000, 24990000),
	(86, 96, 1, 26990000, 26990000),
	(86, 99, 1, 6490000, 6490000),
	(86, 100, 1, 24990000, 24990000),
	(87, 96, 1, 26990000, 26990000),
	(87, 99, 1, 6490000, 6490000),
	(87, 100, 1, 24990000, 24990000),
	(88, 96, 1, 26990000, 26990000),
	(88, 99, 1, 6490000, 6490000),
	(88, 100, 1, 24990000, 24990000),
	(89, 96, 1, 26990000, 26990000),
	(89, 99, 1, 6490000, 6490000),
	(89, 100, 1, 24990000, 24990000),
	(90, 96, 1, 26990000, 26990000),
	(90, 99, 1, 6490000, 6490000),
	(90, 100, 1, 24990000, 24990000),
	(91, 96, 1, 26990000, 26990000),
	(91, 99, 1, 6490000, 6490000),
	(91, 100, 1, 24990000, 24990000),
	(92, 96, 1, 26990000, 26990000),
	(92, 99, 1, 6490000, 6490000),
	(92, 100, 1, 24990000, 24990000),
	(93, 96, 1, 26990000, 26990000),
	(93, 99, 1, 6490000, 6490000),
	(93, 100, 1, 24990000, 24990000),
	(94, 71, 2, 26990000, 53980000),
	(95, 71, 2, 26990000, 53980000),
	(96, 71, 2, 26990000, 53980000),
	(97, 71, 2, 26990000, 53980000),
	(98, 71, 1, 26990000, 26990000),
	(99, 71, 2, 26990000, 53980000),
	(99, 73, 1, 16990000, 16990000),
	(100, 73, 1, 16990000, 16990000),
	(101, 71, 1, 26990000, 26990000),
	(101, 73, 1, 16990000, 16990000),
	(102, 71, 1, 26990000, 26990000),
	(103, 71, 2, 24291000, 48582000),
	(104, 71, 2, 24291000, 48582000);
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[LoaiPhieuKho] ON 
Insert Into [dbo].[LoaiPhieuKho] ([MaLPK], [MoTa])
Values 
    (1, N'Phiếu nhập kho'),
	(2, N'Phiếu xuất kho')
SET IDENTITY_INSERT [dbo].[LoaiPhieuKho] OFF
Go
---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[PhieuKho] ON 
Insert Into [dbo].[PhieuKho] ([MaPK], [MaLPK], [MoTa], [NgayNhapXuatKho], [TongTien], [MaNV], [MaKho])
Values 
    (1, 1, N'Phiếu nhập kho 1', '2024-10-02', 34990000, 9, 3),
    (2, 2, N'Phiếu xuất kho 1', '2024-09-16', 44990000, 5, 7),
    (3, 1, N'Phiếu nhập kho 2', '2024-08-21', 14990000, 12, 8),
	(4, 2, N'Phiếu xuất kho 2', '2024-12-05', 14990000, 12, 8),
	(5, 2, N'Xuất hàng hóa đơn 18', '2024-06-05', 58470000, 1, 8),
	(6, 2, N'Xuất hàng hóa đơn 19', '2024-06-05', 58470000, 1, 8),
	(7, 2, N'Xuất hàng hóa đơn 20', '2024-06-05', 58470000, 1, 8),
	(8, 2, N'Xuất hàng hóa đơn 21', '2024-06-15', 58470000, 1, 8),
	(9, 2, N'Xuất hàng hóa đơn 22', '2024-06-15', 53980000, 1, 8),
	(10, 2, N'Xuất hàng hóa đơn 23', '2024-06-17', 53980000, 1, 8),
	(11, 2, N'Xuất hàng hóa đơn 24', '2024-06-17', 53980000, 1, 8),
	(12, 2, N'Xuất hàng hóa đơn 25', '2024-06-17', 53980000, 1, 8),
	(13, 2, N'Xuất hàng hóa đơn 26', '2024-06-17', 26990000, 1, 8),
	(14, 2, N'Xuất hàng hóa đơn 27', '2024-06-17', 70970000, 1, 8),
	(15, 2, N'Xuất hàng hóa đơn 28', '2024-06-17', 16990000, 1, 8),
	(16, 2, N'Xuất hàng hóa đơn 29', '2024-06-17', 43980000, 2, 8),
	(17, 2, N'Xuất hàng hóa đơn 31', '2024-06-17', 48582000, 1, 8),
	(18, 2, N'Xuất hàng hóa đơn 32', '2024-06-17', 48582000, 1, 8),

	(19, 1, N'Nhập hàng hóa đơn 18', '2024-07-01', 58470000, 1, 8),
	(20, 1, N'Nhập hàng hóa đơn 19', '2024-07-01', 58470000, 1, 8),
	(21, 1, N'Nhập hàng hóa đơn 20', '2024-07-01', 58470000, 1, 8),
	(22, 1, N'Nhập hàng hóa đơn 21', '2024-07-02', 58470000, 1, 8),
	(23, 1, N'Nhập hàng hóa đơn 22', '2024-07-02', 53980000, 1, 8),
	(24, 1, N'Nhập hàng hóa đơn 23', '2024-07-02', 53980000, 1, 8),
	(25, 1, N'Nhập hàng hóa đơn 24', '2024-07-02', 53980000, 1, 8),
	(26, 1, N'Nhập hàng hóa đơn 25', '2024-07-02', 53980000, 1, 8),
	(27, 1, N'Nhập hàng hóa đơn 26', '2024-07-03', 26990000, 1, 8),
	(28, 1, N'Nhập hàng hóa đơn 27', '2024-07-03', 70970000, 1, 8),
	(29, 1, N'Nhập hàng hóa đơn 28', '2024-07-03', 16990000, 1, 8),
	(30, 1, N'Nhập hàng hóa đơn 29', '2024-07-04', 43980000, 2, 8),
	(31, 1, N'Nhập hàng hóa đơn 31', '2024-07-04', 48582000, 1, 8),
	(32, 1, N'Nhập hàng hóa đơn 32', '2024-07-04', 48582000, 1, 8);
SET IDENTITY_INSERT [dbo].[PhieuKho] OFF
Go
---------------------------------------------------------------------
Insert Into [dbo].[ChiTietPhieuKho] ([MaPK], [MaSP], [SoLuong], [DonGia], [ThanhTien])
Values 
    (1, 7, 10, 2000000, 20000000),
    (1, 8, 20, 1000000, 20000000),
    (2, 9, 15, 2000000, 30000000),
    (2, 10, 10, 3000000, 30000000),
    (3, 1, 5, 2000000, 10000000),
    (3, 2, 7, 3000000, 21000000),
	(8, 96, 1, 25990000, 25990000),
	(8, 99, 1, 6290000, 6290000),
	(8, 100, 1, 22990000, 22990000),
	(9, 71, 2, 26990000, 53980000),
	(10, 71, 2, 26990000, 53980000),
	(11, 71, 2, 26990000, 53980000),
	(12, 71, 2, 26990000, 53980000),
	(13, 71, 1, 26990000, 26990000),
	(14, 71, 2, 26990000, 53980000),
	(14, 73, 1, 15990000, 15990000),
	(15, 73, 1, 15990000, 15990000),
	(16, 71, 1, 26990000, 26990000),
	(16, 73, 1, 16990000, 16990000),
	(17, 71, 2, 24291000, 48582000),
	(18, 71, 2, 24291000, 48582000),

	(22, 96, 1, 25990000, 25990000),
	(22, 99, 1, 6290000, 6290000),
	(22, 100, 1, 22990000, 22990000),
	(23, 71, 2, 26990000, 53980000),
	(24, 71, 2, 26990000, 53980000),
	(25, 71, 2, 26990000, 53980000),
	(26, 71, 2, 26990000, 53980000),
	(27, 71, 1, 26990000, 26990000),
	(28, 71, 2, 26990000, 53980000),
	(28, 73, 1, 15990000, 15990000),
	(29, 73, 1, 15990000, 15990000),
	(30, 71, 1, 26990000, 26990000),
	(30, 73, 1, 16990000, 16990000),
	(31, 71, 2, 24291000, 48582000),
	(32, 71, 2, 24291000, 48582000);
Go
---------------------------------------------------------------------

---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[LoaiKhuyenMai] ON 
Insert Into [dbo].[LoaiKhuyenMai] ([MaLKM], [Mota])
Values 
	(1, N'Khuyến mãi giảm % các sản phẩn'),
	(2, N'Khuyến mãi giảm giá các sản phẩm'),
	(3, N'Mã giảm %'),
	(4, N'Mã giảm tiền'),
    (5, N'Voucher giảm %'),
    (6, N'Voucher giảm tiền');
SET IDENTITY_INSERT [dbo].[LoaiKhuyenMai] OFF
Go
---------------------------------------------------------------------

---------------------------------------------------------------------

---------------------------------------------------------------------
Insert Into [dbo].[KhuyenMai] ([MaKM], [Mota], [GiaTriDonHangToiThieu], [GiaTriGiam], [NgayBatDau], [NgayKetThuc], [SoDiemDoiDuoc], [SoLuong], [MaLKM], [TrangThaiXoa])
Values 
    ('KM001', N'Giảm giá 10%', 500000, 10, '2024-07-01', '2024-07-30', 100, Null, 1, 1),
    ('KM002', N'Free ship', 1000000, 100000, '2024-07-01', '2024-07-30', 35, Null, 2, 1),
    ('KM003', N'Voucher 200k', 1500000, 200000, '2024-07-01', '2024-07-30', 75, Null, 3, 1),
    ('KM004', N'combo 3 sản phẩm', 2000000, 10, '2024-07-01', '2024-07-30', 60, Null, 4, 1),
    ('KM005', N'Mua 1 tặng 1', 2500000, 10, '2024-07-01', '2024-07-30', 20, Null, 5, 1),
	('KH006', N'BAC', 100000, 10, '2024-07-01', '2024-07-30', 100, 10, 5, 1);
Go
---------------------------------------------------------------------
Insert Into [dbo].[ChiTietApDung] ([MaSP], [MaKM], [SoLuong])
Values 
    (1, 'KM001', 10),
    (2, 'KM001', 15),
    (3, 'KM001', 8),
    (4, 'KM002', 50),
    (5, 'KM002', 10),
    (6, 'KM002', 3),
    (7, 'KM003', 20),
    (8, 'KM003', 50),
    (9, 'KM003', 10),
    (10, 'KM004', 1),
	(71, 'KM005', 1),
	(1, 'KM005', 1);
Go
---------------------------------------------------------------------
Insert Into [dbo].[ChiTietApDungKhuyenMai] ([MaKM], [MaHD], [UuDai])
Values 
    ('KM001', 1, N'Giảm giá 10% cho đơn hàng này'),
    ('KM002', 2, N'Miễn phí vận chuyển cho đơn hàng này'),
    ('KM003', 3, N'Nhận voucher trị giá 200,000 đồng cho đơn hàng này'),
    ('KM004', 4, N'Nhận combo 3 sản phẩm cho đơn hàng này');
Go
---------------------------------------------------------------------
Insert Into [dbo].[ChiTietDoiDiem] ([MaKM], [MaKH], [UuDaiDoiDuoc])
Values 
    ('KM001', 1, N'Giảm giá 10% cho đơn hàng tiếp theo'),
    ('KM002', 2, N'Miễn phí vận chuyển cho đơn hàng tiếp theo'),
    ('KM003', 3, N'Nhận voucher trị giá 200,000 đồng cho đơn hàng tiếp theo'),
    ('KM004', 4, N'Nhận combo 3 sản phẩm cho đơn hàng tiếp theo'),
    ('KM005', 5, N'Nhận sản phẩm tặng kèm cho đơn hàng tiếp theo');
Go
---------------------------------------------------------------------

---------------------------------------------------------------------

---------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[HoaDonDoiTra] ON 
INSERT INTO [dbo].[HoaDonDoiTra] ([MaHDDT], [MaHD], [NgayDoiTra], [TrangThai])
VALUES 
    (1, 1, '2024-04-25', 'Wait for processing'),
    (2, 2, '2024-04-26', 'Wait for processing'),
    (3, 3, '2024-04-27', 'Wait for processing'),
    (4, 4, '2024-04-28', 'Wait for processing'),
    (5, 5, '2024-04-29', 'Wait for processing');
SET IDENTITY_INSERT [dbo].[HoaDonDoiTra] OFF
Go
---------------------------------------------------------------------
INSERT INTO [dbo].[ChiTietHoaDonDoiTra] ([MaHDDT], [MaSP], [SoLuong], [LyDo])
VALUES 
    (1, 1, 1, N'Sản phẩm bị lỗi kỹ thuật'),
    (2, 2, 1, N'Sản phẩm bị hư hỏng hoặc bị vỡ'),
    (3, 3, 1, N'Sản phẩm không đúng với thông số kỹ thuật hoặc mô tả'),
    (4, 4, 1, N'Sản phẩm bị trầy xước hoặc có vấn đề về ngoại hình'),
    (5, 5, 1, N'Sản phẩm không tương thích hoặc không hoạt động tốt với thiết bị khác');
Go
---------------------------------------------------------------------
Select * From [dbo].[ChucVu]
Select * From [dbo].[NhanVien]
Select * From [dbo].[DanhMucSanPham]
Select * From [dbo].[LoaiSanPham]
Select * From [dbo].[NhaCungCap]
Select * From [dbo].[SanPham]
Select * From [dbo].[HinhAnhSanPham]
Select * From [dbo].[KhachHang]
Select * From [dbo].[GioHang]
Select * From [dbo].[ChiTietGioHang]
Select * From [dbo].[BinhLuanDanhGia]
Select * From [dbo].[CauHinhTichDiem]
Select * From [dbo].[HinhThucBanHang]
Select * From [dbo].[TrangThaiHoaDon]
Select * From [dbo].[HoaDon]
Select * From [dbo].[HoaDonDoiTra]
Select * From [dbo].[ChiTietHoaDonDoiTra]
Select * From [dbo].[ChiTietHoaDon]
Select * From [dbo].[Kho]
Select * From [dbo].[ChiTietKho]
Select * From [dbo].[LoaiPhieuKho]
Select * From [dbo].[PhieuKho]
Select * From [dbo].[ChiTietPhieuKho]
Select * From [dbo].[LoaiKhuyenMai]
Select * From [dbo].[KhuyenMai]
Select * From [dbo].[ChiTietApDung]
Select * From [dbo].[ChiTietApDungKhuyenMai]
Select * From [dbo].[ChiTietDoiDiem]
---------------------------------------------------------------------
ALTER TABLE NhanVien
ADD CONSTRAINT unique_sdt UNIQUE (SDT);

ALTER TABLE NhanVien
ADD CONSTRAINT unique_cccd UNIQUE (CCCD);

ALTER TABLE KhachHang
ADD CONSTRAINT unique_sdt1 UNIQUE (SDT);

ALTER TABLE NhaCungCap
ADD CONSTRAINT unique_sdt2 UNIQUE (SDT);

ALTER TABLE NhaCungCap
ADD CONSTRAINT unique_email UNIQUE (Email);
---------------------------------------------------------------------
