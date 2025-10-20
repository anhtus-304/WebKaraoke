-- ==========================
-- TẠO DATABASE WEB KARAOKE
-- ==========================
CREATE DATABASE WebKaraokeDB;
GO
USE WebKaraokeDB;
GO

-- ====== BẢNG KHÁCH HÀNG ======
CREATE TABLE KhachHang (
  KhachHangID INT IDENTITY(1,1) PRIMARY KEY,
  HoTen NVARCHAR(100),
  SoDienThoai VARCHAR(20),
  Email NVARCHAR(100),
  DiaChi NVARCHAR(250)
);

-- ====== BẢNG PHÒNG ======
CREATE TABLE Phong (
  PhongID INT IDENTITY(1,1) PRIMARY KEY,
  TenPhong NVARCHAR(100),
  LoaiPhong NVARCHAR(50),
  GiaGio DECIMAL(10,2),
  TrangThai NVARCHAR(20)
);

-- ====== BẢNG NHÂN VIÊN ======
CREATE TABLE NhanVien (
  NhanVienID INT IDENTITY(1,1) PRIMARY KEY,
  HoTen NVARCHAR(100),
  SoDienThoai VARCHAR(20),
  ChucVu NVARCHAR(50)
);

-- ====== BẢNG KHUYẾN MÃI ======
CREATE TABLE KhuyenMai (
  KM_ID INT IDENTITY(1,1) PRIMARY KEY,
  MaKM NVARCHAR(50),
  TyLeGiam DECIMAL(5,2),
  NgayBatDau DATE,
  NgayKetThuc DATE
);

-- ====== BẢNG ĐẶT PHÒNG ======
CREATE TABLE DatPhong (
  DatPhongID INT IDENTITY(1,1) PRIMARY KEY,
  KhachHangID INT,
  PhongID INT,
  ThoiGianDat DATETIME,
  GioBatDau DATETIME,
  GioKetThuc DATETIME,
  SoLuongNguoi INT,
  TrangThai NVARCHAR(20),
  FOREIGN KEY (KhachHangID) REFERENCES KhachHang(KhachHangID),
  FOREIGN KEY (PhongID) REFERENCES Phong(PhongID)
);

-- ====== BẢNG HÓA ĐƠN ======
CREATE TABLE HoaDon (
  HoaDonID INT IDENTITY(1,1) PRIMARY KEY,
  DatPhongID INT,
  NhanVienID INT,
  KM_ID INT NULL,
  NgayLap DATETIME,
  TongTien DECIMAL(12,2),
  FOREIGN KEY (DatPhongID) REFERENCES DatPhong(DatPhongID),
  FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID),
  FOREIGN KEY (KM_ID) REFERENCES KhuyenMai(KM_ID)
);

-- ====== BẢNG MÓN ĂN / NƯỚC UỐNG ======
CREATE TABLE MonAnNuocUong (
  MonID INT IDENTITY(1,1) PRIMARY KEY,
  TenMon NVARCHAR(150),
  DonGia DECIMAL(10,2),
  DanhMuc NVARCHAR(50)
);

-- ====== BẢNG CHI TIẾT HÓA ĐƠN ======
CREATE TABLE ChiTietHoaDon (
  CTHD_ID INT IDENTITY(1,1) PRIMARY KEY,
  HoaDonID INT,
  MonID INT,
  SoLuong INT,
  DonGia DECIMAL(10,2),
  FOREIGN KEY (HoaDonID) REFERENCES HoaDon(HoaDonID),
  FOREIGN KEY (MonID) REFERENCES MonAnNuocUong(MonID)
);

-- ====== BẢNG TÀI KHOẢN ======
CREATE TABLE TaiKhoan (
  TaiKhoanID INT IDENTITY(1,1) PRIMARY KEY,
  Username NVARCHAR(100) UNIQUE,
  MatKhauHash NVARCHAR(200),
  Role NVARCHAR(20),
  KhachHangID INT NULL,
  NhanVienID INT NULL,
  FOREIGN KEY (KhachHangID) REFERENCES KhachHang(KhachHangID),
  FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
);

-- ===============================
-- DỮ LIỆU MẪU
-- ===============================

INSERT INTO KhachHang (HoTen, SoDienThoai, Email, DiaChi)
VALUES
(N'Nguyễn Văn A', '0901234567', 'vana@gmail.com', N'123 Trần Hưng Đạo, Q1'),
(N'Lê Thị B', '0987654321', 'leb@example.com', N'45 Nguyễn Huệ, Q1'),
(N'Trần Minh C', '0911222333', 'minhc@gmail.com', N'78 Lý Thường Kiệt, Q10');

INSERT INTO Phong (TenPhong, LoaiPhong, GiaGio, TrangThai)
VALUES
(N'Phòng VIP 1', N'VIP', 250000.00, N'Trong'),
(N'Phòng VIP 2', N'VIP', 250000.00, N'DangSuDung'),
(N'Phòng Thường 1', N'Thuong', 150000.00, N'Trong'),
(N'Phòng Thường 2', N'Thuong', 150000.00, N'DaDat');

INSERT INTO NhanVien (HoTen, SoDienThoai, ChucVu)
VALUES
(N'Phạm Văn D', '0909988776', N'Lễ tân'),
(N'Ngô Thị E', '0911002200', N'Quản lý'),
(N'Võ Văn F', '0988776655', N'Phục vụ');

INSERT INTO KhuyenMai (MaKM, TyLeGiam, NgayBatDau, NgayKetThuc)
VALUES
(N'KM10', 10.00, '2025-10-01', '2025-10-31'),
(N'KM20', 20.00, '2025-11-01', '2025-11-30');

INSERT INTO DatPhong (KhachHangID, PhongID, ThoiGianDat, GioBatDau, GioKetThuc, SoLuongNguoi, TrangThai)
VALUES
(1, 2, GETDATE(), '2025-10-20 19:00', '2025-10-20 21:00', 5, N'DangSuDung'),
(2, 4, GETDATE(), '2025-10-21 20:00', '2025-10-21 22:00', 3, N'DaXacNhan'),
(3, 1, GETDATE(), '2025-10-22 18:00', '2025-10-22 20:00', 4, N'DaHuy');

INSERT INTO HoaDon (DatPhongID, NhanVienID, KM_ID, NgayLap, TongTien)
VALUES
(1, 1, 1, GETDATE(), 450000.00),
(2, 2, NULL, GETDATE(), 300000.00);

INSERT INTO MonAnNuocUong (TenMon, DonGia, DanhMuc)
VALUES
(N'Coca-Cola', 20000.00, N'Nước uống'),
(N'Bia Tiger', 30000.00, N'Nước uống'),
(N'Khoai tây chiên', 40000.00, N'Đồ ăn'),
(N'Gà rán', 80000.00, N'Đồ ăn');

INSERT INTO ChiTietHoaDon (HoaDonID, MonID, SoLuong, DonGia)
VALUES
(1, 1, 2, 20000.00),
(1, 3, 1, 40000.00),
(2, 2, 3, 30000.00),
(2, 4, 1, 80000.00);

INSERT INTO TaiKhoan (Username, MatKhauHash, Role, KhachHangID, NhanVienID)
VALUES
(N'khach1', N'hashed_pass_1', N'Khach', 1, NULL),
(N'admin1', N'hashed_pass_admin', N'Admin', NULL, 2),
(N'nhanvien1', N'hashed_pass_nv', N'NhanVien', NULL, 1);
GO
