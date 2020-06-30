create table sanPham (
maSP nvarchar(50) primary key,
tenSP nvarchar(50),
gia float,
anh image,
soLuong int,
thongSo nvarchar(50),
phanLoai nvarchar(50),
)

create table nhomSP(
maLoai nvarchar(50) primary key,
tenLoai nvarchar(50)
)

create table chiTietSP(
maSP nvarchar(50),
soSerial nvarchar(50),
color nvarchar(50),
primary key (maSP,soSerial)
)

create table nhaCungCap(
maNCC nvarchar(50) primary key,
tenNCC nvarchar(50),
email nvarchar(50),
diaChi nvarchar(50),
sdt nvarchar(50)
)

create table khachHang(
maKH nvarchar(50) primary key,
tenKH nvarchar(50),
email nvarchar(50),
diaChi nvarchar(50),
sdt nvarchar(50)
)

create table nhanVien(
maNV nvarchar(50) primary key,
tenNV nvarchar(50),
email nvarchar(50),
diaChi nvarchar(50),
sdt nvarchar(50),
passWords nvarchar(50), 
congViec nvarchar(50)
)

create table congViec(
maCV nvarchar(50) primary key,
tenCV nvarchar(50),
bacLuong float
)

create table hoadDonNhap(
maHDN nvarchar(50) primary key,
maNV nvarchar(50),
maNCC nvarchar(50),
ngayNhap date,
tongTien float,
trangThai nvarchar(50),
)

create table chiTietHDN(
maCTHDN nvarchar(50) primary key,
maSP nvarchar(50),
donGia float,
soLuong int,
thanhTien float
)

create table hoadDonXuat(
maHDX nvarchar(50) primary key,
maNV nvarchar(50),
maKH nvarchar(50),
ngayBan date,
tongTien float,
trangThai nvarchar(50),
)

create table chiTietHDX(
machiTietHDX nvarchar(50) primary key,
maSP nvarchar(50),
donGia float,
soLuong int,
thanhTien float
)


go
alter table nhanVien add foreign key(maCV) references congViec(maCV)
go
alter table hoaDonNhap add foreign key (maNCC) references nhaCungCap (maNCC)
go
alter table hoaDonNhap add foreign key (maNV) references nhanVien(maNV)
go
alter table hoaDonXuat add foreign key (maKH) references khachHang(maKH)
go
alter table hoaDonXuat add foreign key (maNV) references nhanVien(maNV)
go
alter table sanPham add foreign key (phanLoai) references nhomSP (maLoai)
go
alter table chiTietHDN add foreign key (maSP) references sanPham(maSP)
go
alter table chiTietHDN add foreign key (maHDN) references hoaDonNhap(maHDN)
go
alter table chiTietHDX add foreign key (maSP) references sanPham(maSP)
go
alter table chiTietHDX add foreign key (maHDX) references hoaDonXuat(maHDX)
go
alter table chiTietSP add foreign key (maSP) references sanPham(maSP)

