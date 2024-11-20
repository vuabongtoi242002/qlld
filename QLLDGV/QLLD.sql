CREATE DATABASE QLLD 
go
use QLLD
go
CREATE TABLE PHONG(
	MaPhong varchar(5) primary key,
	TenPhong nvarchar(MAX) not null,
	ThongTinPhong nvarchar(MAX)
)
go
CREATE TABLE THU(
	MaThu varchar(2) primary key,
	TenThu nvarchar(max) not null
)
go
CREATE TABLE TietHoc(
	MaTiet varchar(3) primary key,
	Tiet int not null,
	TGianBD varchar(max) not null,
	TGianKT varchar(max) not null
)
go
CREATE TABLE Nganh(
	MaNganh varchar(6) primary key,
	TenNganh nvarchar(max) not null
)
go
CREATE TABLE MonHoc(
	MaMH varchar(8) not null primary key,
	TenMH nvarchar(MAX) not null,
	Nganh varchar(6) not null REFERENCES Nganh(MaNganh),
	TinChi int
)
go
CREATE TABLE HocPhan(
	MaNhom uniqueidentifier primary key,
	MonHoc varchar(8) not null REFERENCES MonHoc(MaMH),
	TenNhom nvarchar(MAX) not null,
)
go
CREATE TABLE GIAOVIEN(
	MaGV uniqueidentifier not null primary key,
	HoTenGV nvarchar(MAX) not null,
	BoMonPhuTrach varchar(8) not null REFERENCES MonHoc(MaMH),
	SodtGV varchar(15) not null,
	EmailGV varchar(MAX) not null,
	QueQuan nvarchar(MAX),
	MatKhauGV varchar(MAX) not null
)
go
CREATE TABLE LichDay(
	MaLich uniqueidentifier not null primary key,
	TenMH varchar(8) not null REFERENCES MonHoc(MaMH),
	NhomHP uniqueidentifier not null REFERENCES HocPhan(MaNhom),
	PhongHoc varchar(5) REFERENCES PHONG(MaPhong),
	GVDay uniqueidentifier REFERENCES GIAOVIEN(MaGV),
	ThuNgay varchar(2) REFERENCES THU(MaThu),
	NgayBatDau date,
	NgayKetThuc date,
	GioBatDau varchar(3) REFERENCES TietHoc(MaTiet),
	GioKetThuc varchar(3) REFERENCES TietHoc(MaTiet),
	check (NgayBatDau < NgayKetThuc)
)
go
CREATE TABLE SuKien(
	MaSuKien uniqueidentifier not null primary key,
	TenHP varchar(8) not null REFERENCES MonHoc(MaMH),
	Nhom uniqueidentifier not null REFERENCES HocPhan(MaNhom),
	GiaoVien uniqueidentifier REFERENCES GIAOVIEN(MaGV),
	TenSuKien nvarchar(MAX),
	StartDate date
)
go
CREATE TABLE QuanTri(
	MaAdmin uniqueidentifier not null primary key,
	TenAdmin nvarchar(MAX),
	SDT varchar(MAX),
	Email varchar(MAX),
	MatKhau varchar(MAX)
)
go
CREATE TABLE Message(
	MaMail uniqueidentifier not null primary key,
	NguoiGui uniqueidentifier not null REFERENCES GIAOVIEN(MaGV),
	NoiDung nvarchar(MAX),
	NgayGui date,
	Tinhtrang nvarchar(MAX)
)
go

INSERT INTO THU
VALUES
('0',N'Chủ nhật'),
('1',N'Thứ 2'),
('2',N'Thứ 3'),
('3',N'Thứ 4'),
('4',N'Thứ 5'),
('5',N'Thứ 6'),
('6',N'Thứ 7')

GO
INSERT INTO QuanTri 
VALUES 
(NEWID(),'Mạnh', '0327743411', 'lem531785@gmail.com', '123456789')
GO