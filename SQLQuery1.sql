-- =========================================================================
-- SCRIPT TONG HOP GOM CHUNG TOAN BO CAC PHAN (GIU NGUYEN CODE GOC + BO SUNG SP)
-- =========================================================================

-- 2.0. Script Khoi tao CSDL, Bang Rang buoc & Du lieu mau (DDL & Seed Data)
USE master;
GO

IF DB_ID('CineStarDB') IS NOT NULL
BEGIN
    ALTER DATABASE CineStarDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CineStarDB;
END
GO

CREATE DATABASE CineStarDB;
GO

USE CineStarDB;
GO

-- 1. Bang KHACHHANG
CREATE TABLE dbo.KhachHang (
    MaKH VARCHAR(15) NOT NULL PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NOT NULL UNIQUE,
    Email VARCHAR(100) NULL UNIQUE,
    NgaySinh DATE NULL,
    DiemTichLuy INT NOT NULL DEFAULT 0 CHECK (DiemTichLuy >= 0),
    HangThanhVien NVARCHAR(30) NOT NULL DEFAULT N'Member' 
        CHECK (HangThanhVien IN (N'Member', N'VIP', N'VVIP'))
);

-- 2. Bang NHANVIEN
CREATE TABLE dbo.NhanVien (
    MaNV VARCHAR(15) NOT NULL PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NOT NULL UNIQUE,
    ChucVu NVARCHAR(50) NOT NULL,
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhauHash VARCHAR(256) NOT NULL,
    VaiTro VARCHAR(20) NOT NULL CHECK (VaiTro IN ('Admin', 'User')),
    TrangThaiLamViec BIT NOT NULL DEFAULT 1
);

-- 3. Bang PHIM
CREATE TABLE dbo.Phim (
    MaPhim VARCHAR(15) NOT NULL PRIMARY KEY,
    TenPhim NVARCHAR(200) NOT NULL,
    TheLoai NVARCHAR(100) NOT NULL,
    ThoiLuong INT NOT NULL CHECK (ThoiLuong > 0),
    DaoDien NVARCHAR(100) NULL,
    DoTuoiQuyDinh VARCHAR(10) NOT NULL CHECK (DoTuoiQuyDinh IN ('P', 'K', 'T13', 'T16', 'T18')),
    NgayKhoiChieu DATE NOT NULL,
    TrangThaiPhim NVARCHAR(50) NOT NULL DEFAULT N'Dang chieu'
        CHECK (TrangThaiPhim IN (N'Sap chieu', N'Dang chieu', N'Ngung chieu'))
);

-- 4. Bang SUATCHIEU
CREATE TABLE dbo.SuatChieu (
    MaSuatChieu VARCHAR(15) NOT NULL PRIMARY KEY,
    MaPhim VARCHAR(15) NOT NULL,
    MaPhong VARCHAR(15) NOT NULL CHECK (MaPhong IN ('RAP01', 'RAP02', 'RAP03', 'RAP04', 'RAP05')),
    ThoiGianBatDau DATETIME NOT NULL,
    ThoiGianKetThuc DATETIME NOT NULL,
    DinhDangChieu VARCHAR(20) NOT NULL CHECK (DinhDangChieu IN ('2D', '3D', 'IMAX')),
    GiaVeCoBan DECIMAL(18,2) NOT NULL CHECK (GiaVeCoBan > 0),
    TrangThaiSuat NVARCHAR(30) NOT NULL DEFAULT N'Mo ban'
        CHECK (TrangThaiSuat IN (N'Mo ban', N'Dong ban', N'Da huy')),
    CONSTRAINT FK_SuatChieu_Phim FOREIGN KEY (MaPhim) REFERENCES dbo.Phim(MaPhim),
    CONSTRAINT UQ_Phong_ThoiGian UNIQUE (MaPhong, ThoiGianBatDau),
    CONSTRAINT CK_ThoiGianSuatChieu CHECK (ThoiGianKetThuc > ThoiGianBatDau)
);

-- 5. Bang GHENGOI
CREATE TABLE dbo.GheNgoi (
    MaGhe VARCHAR(15) NOT NULL PRIMARY KEY,
    MaPhong VARCHAR(15) NOT NULL,
    HangGhe CHAR(2) NOT NULL,
    SoGhe INT NOT NULL CHECK (SoGhe > 0),
    LoaiGhe NVARCHAR(30) NOT NULL CHECK (LoaiGhe IN (N'Thuong', N'VIP', N'Couple')),
    PhuThuGhe DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (PhuThuGhe >= 0),
    TrangThaiBaoTri BIT NOT NULL DEFAULT 0,
    CONSTRAINT UQ_ViTriGhe UNIQUE (MaPhong, HangGhe, SoGhe)
);

-- 6. Bang KHUYENMAI
CREATE TABLE dbo.KhuyenMai (
    MaKM VARCHAR(15) NOT NULL PRIMARY KEY,
    TenChuongTrinh NVARCHAR(150) NOT NULL,
    TyLeGiam DECIMAL(5,2) NOT NULL CHECK (TyLeGiam BETWEEN 0 AND 100),
    GiamToiDa DECIMAL(18,2) NOT NULL CHECK (GiamToiDa >= 0),
    NgayBatDau DATETIME NOT NULL,
    NgayKetThuc DATETIME NOT NULL,
    SoLuongPhatHanh INT NOT NULL CHECK (SoLuongPhatHanh > 0),
    SoLuongDaDung INT NOT NULL DEFAULT 0 CHECK (SoLuongDaDung >= 0),
    CONSTRAINT CK_NgayKhuyenMai CHECK (NgayKetThuc >= NgayBatDau)
);

-- 7. Bang VEXEMPHIM (Master)
CREATE TABLE dbo.VeXemPhim (
    MaVe VARCHAR(20) NOT NULL PRIMARY KEY,
    MaKH VARCHAR(15) NULL,
    MaNV VARCHAR(15) NOT NULL,
    MaSuatChieu VARCHAR(15) NOT NULL,
    MaKM VARCHAR(15) NULL,
    NgayLapVe DATETIME NOT NULL DEFAULT GETDATE(),
    PhuongThucThanhToan NVARCHAR(50) NOT NULL DEFAULT N'Tien mat'
        CHECK (PhuongThucThanhToan IN (N'Tien mat', N'Chuyen khoan', N'The tin dung', N'Vi dien tu')),
    TrangThaiThanhToan NVARCHAR(30) NOT NULL DEFAULT N'Chua thanh toan'
        CHECK (TrangThaiThanhToan IN (N'Chua thanh toan', N'Da thanh toan', N'Da huy')),
    CONSTRAINT FK_Ve_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH),
    CONSTRAINT FK_Ve_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV),
    CONSTRAINT FK_Ve_SuatChieu FOREIGN KEY (MaSuatChieu) REFERENCES dbo.SuatChieu(MaSuatChieu),
    CONSTRAINT FK_Ve_KhuyenMai FOREIGN KEY (MaKM) REFERENCES dbo.KhuyenMai(MaKM)
);

-- 8. Bang CHITIETVE (Detail - BCNF M:N Resolution)
CREATE TABLE dbo.ChiTietVe (
    MaVe VARCHAR(20) NOT NULL,
    MaGhe VARCHAR(15) NOT NULL,
    DonGiaBan DECIMAL(18,2) NOT NULL CHECK (DonGiaBan >= 0),
    CONSTRAINT PK_ChiTietVe PRIMARY KEY (MaVe, MaGhe),
    CONSTRAINT FK_ChiTietVe_Ve FOREIGN KEY (MaVe) REFERENCES dbo.VeXemPhim(MaVe) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietVe_Ghe FOREIGN KEY (MaGhe) REFERENCES dbo.GheNgoi(MaGhe)
);
GO

-- Chen du lieu mau (Seed Data)
INSERT INTO dbo.KhachHang VALUES
('KH001', N'Nguyen Minh Tuan', '0901234567', 'tuan.nm@gmail.com', '2000-05-15', 150, N'VIP'),
('KH002', N'Tran Le Bao Ngoc', '0912345678', 'ngoc.tlb@gmail.com', '2003-10-20', 40, N'Member');

INSERT INTO dbo.NhanVien VALUES
('NV001', N'Le Hoang Quan Tri', '0988111222', N'Quan ly rap', 'admin', 'Admin@123', 'Admin', 1),
('NV002', N'Pham Thu Ngan', '0988333444', N'Thu ngan', 'thungan01', 'User@123', 'User', 1);

INSERT INTO dbo.Phim VALUES
('PH001', N'Mai', N'Tam ly, Tinh cam', 131, N'Tran Thanh', 'T18', '2026-10-01', N'Dang chieu'),
('PH002', N'Dune: Hanh Tinh Cat - Phan 2', N'Khoa hoc vien tuong', 166, N'Denis Villeneuve', 'T13', '2026-10-10', N'Dang chieu');

INSERT INTO dbo.SuatChieu VALUES
('SC001', 'PH001', 'RAP01', DATEADD(HOUR, 5, GETDATE()), DATEADD(HOUR, 7, GETDATE()), '2D', 90000, N'Mo ban'),
('SC002', 'PH002', 'RAP02', DATEADD(HOUR, 8, GETDATE()), DATEADD(HOUR, 11, GETDATE()), 'IMAX', 150000, N'Mo ban');

INSERT INTO dbo.GheNgoi VALUES
('R1_A01', 'RAP01', 'A', 1, N'Thuong', 0, 0),
('R1_A02', 'RAP01', 'A', 2, N'Thuong', 0, 0),
('R1_B01', 'RAP01', 'B', 1, N'VIP', 20000, 0),
('R1_B02', 'RAP01', 'B', 2, N'VIP', 20000, 0),
('R2_A01', 'RAP02', 'A', 1, N'VIP', 30000, 0);

INSERT INTO dbo.KhuyenMai VALUES
('KM001', N'Uu dai Thanh vien Moi', 10.00, 30000, '2026-01-01', '2026-12-31', 1000, 1);
GO

-- 2.1. Views Nghiep Vu (V_DanhSachChuaXuLy & V_ThongKeTongHop)
-- =========================================================================
-- 1. VIEW V_DanhSachChuaXuLy:
-- Liet ke cac don ve dang o trang thai "Chua thanh toan" HOAC cac suat chieu
-- sap dien ra nhung chua co khach dat ve de bo phan van hanh xu ly kip thoi.
-- =========================================================================
CREATE OR ALTER VIEW dbo.V_DanhSachChuaXuLy
AS
    -- Nhom 1: Cac giao dich ve da giu ghe nhung khach chua thanh toan
    SELECT 
        N'Don ve chua thanh toan' AS LoaiCanXuLy,
        v.MaVe AS MaDoiTuong,
        ISNULL(kh.HoTen, N'Khach vang lai') AS TenKhachHang,
        p.TenPhim,
        sc.MaSuatChieu,
        sc.MaPhong,
        sc.ThoiGianBatDau,
        v.NgayLapVe AS ThoiDiemPhatSinh,
        COUNT(ct.MaGhe) AS SoGheLienQuan,
        ISNULL(SUM(ct.DonGiaBan), 0) AS GiaTriTamTinh,
        v.TrangThaiThanhToan AS TrangThaiHienTai
    FROM dbo.VeXemPhim v
    INNER JOIN dbo.SuatChieu sc ON v.MaSuatChieu = sc.MaSuatChieu
    INNER JOIN dbo.Phim p ON sc.MaPhim = p.MaPhim
    LEFT JOIN dbo.KhachHang kh ON v.MaKH = kh.MaKH
    LEFT JOIN dbo.ChiTietVe ct ON v.MaVe = ct.MaVe
    WHERE v.TrangThaiThanhToan = N'Chua thanh toan'
    GROUP BY v.MaVe, kh.HoTen, p.TenPhim, sc.MaSuatChieu, sc.MaPhong, 
             sc.ThoiGianBatDau, v.NgayLapVe, v.TrangThaiThanhToan

    UNION ALL

    -- Nhom 2: Cac suat chieu sap dien ra (trong tuong lai) nhung chua co bat ky ve nao dat
    SELECT 
        N'Suat chieu chua co khach dat' AS LoaiCanXuLy,
        sc.MaSuatChieu AS MaDoiTuong,
        N'Chua phat sinh giao dich' AS TenKhachHang,
        p.TenPhim,
        sc.MaSuatChieu,
        sc.MaPhong,
        sc.ThoiGianBatDau,
        sc.ThoiGianBatDau AS ThoiDiemPhatSinh,
        0 AS SoGheLienQuan,
        sc.GiaVeCoBan AS GiaTriTamTinh,
        sc.TrangThaiSuat AS TrangThaiHienTai
    FROM dbo.SuatChieu sc
    INNER JOIN dbo.Phim p ON sc.MaPhim = p.MaPhim
    LEFT JOIN dbo.VeXemPhim v ON sc.MaSuatChieu = v.MaSuatChieu 
         AND v.TrangThaiThanhToan <> N'Da huy'
    WHERE sc.ThoiGianBatDau > GETDATE()
      AND sc.TrangThaiSuat = N'Mo ban'
    GROUP BY sc.MaSuatChieu, p.TenPhim, sc.MaPhong, sc.ThoiGianBatDau, 
             sc.GiaVeCoBan, sc.TrangThaiSuat
    HAVING COUNT(v.MaVe) = 0;
GO

-- =========================================================================
-- 2. VIEW V_ThongKeTongHop:
-- Tong hop doanh thu chi tiet theo Phim, Suat chieu, Nhan vien va Ngay/Thang.
-- =========================================================================
CREATE OR ALTER VIEW dbo.V_ThongKeTongHop
AS
WITH CTE_TinhTienDonVe AS (
    SELECT 
        v.MaVe,
        v.MaSuatChieu,
        v.MaNV,
        v.NgayLapVe,
        COUNT(ct.MaGhe) AS SoLuongGhe,
        ISNULL(SUM(ct.DonGiaBan), 0) AS TongTienGoc,
        CASE 
            WHEN km.MaKM IS NULL THEN 0
            WHEN (ISNULL(SUM(ct.DonGiaBan), 0) * km.TyLeGiam / 100.0) > km.GiamToiDa 
                THEN km.GiamToiDa
            ELSE (ISNULL(SUM(ct.DonGiaBan), 0) * km.TyLeGiam / 100.0)
        END AS TienKhuyenMai
    FROM dbo.VeXemPhim v
    INNER JOIN dbo.ChiTietVe ct ON v.MaVe = ct.MaVe
    LEFT JOIN dbo.KhuyenMai km ON v.MaKM = km.MaKM
    WHERE v.TrangThaiThanhToan = N'Da thanh toan'
    GROUP BY v.MaVe, v.MaSuatChieu, v.MaNV, v.NgayLapVe, km.MaKM, km.TyLeGiam, km.GiamToiDa
)
SELECT 
    CAST(t.NgayLapVe AS DATE) AS NgayGiaoDich,
    MONTH(t.NgayLapVe) AS ThangGiaoDich,
    YEAR(t.NgayLapVe) AS NamGiaoDich,
    p.MaPhim,
    p.TenPhim,
    sc.MaSuatChieu,
    sc.MaPhong,
    sc.DinhDangChieu,
    nv.MaNV,
    nv.HoTen AS TenNhanVien,
    COUNT(DISTINCT t.MaVe) AS TongSoHoaDonVe,
    SUM(t.SoLuongGhe) AS TongSoGheBanRa,
    SUM(t.TongTienGoc) AS DoanhThuTruocGiam,
    SUM(t.TienKhuyenMai) AS TongTienGiamGia,
    SUM(t.TongTienGoc - t.TienKhuyenMai) AS DoanhThuThucThu
FROM CTE_TinhTienDonVe t
INNER JOIN dbo.SuatChieu sc ON t.MaSuatChieu = sc.MaSuatChieu
INNER JOIN dbo.Phim p ON sc.MaPhim = p.MaPhim
INNER JOIN dbo.NhanVien nv ON t.MaNV = nv.MaNV
GROUP BY 
    CAST(t.NgayLapVe AS DATE),
    MONTH(t.NgayLapVe),
    YEAR(t.NgayLapVe),
    p.MaPhim, p.TenPhim,
    sc.MaSuatChieu, sc.MaPhong, sc.DinhDangChieu,
    nv.MaNV, nv.HoTen;
GO

-- 2.3. User-Defined Functions (F_TinhTongGiaTri)
-- =========================================================================
-- FUNCTION: F_TinhTongGiaTri
-- Tinh tong gia tri thanh toan thuc thu theo Ma Khach Hang hoac Ma Suat Chieu.
-- He thong tu dong nhan dien tien tu ma hoac truyen truc tiep @MaDinhDanh.
-- =========================================================================
CREATE OR ALTER FUNCTION dbo.F_TinhTongGiaTri
(
    @MaDinhDanh VARCHAR(20) -- Co the truyen vao @MaKhachHang (VD: 'KH001') hoac @MaSuatChieu (VD: 'SC001')
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @TongGiaTri DECIMAL(18,2) = 0;

    ;WITH CTE_GiaTriDonVe AS (
        SELECT 
            v.MaVe,
            v.MaKH,
            v.MaSuatChieu,
            ISNULL(SUM(ct.DonGiaBan), 0) AS TienGoc,
            CASE 
                WHEN km.MaKM IS NULL THEN 0
                WHEN (ISNULL(SUM(ct.DonGiaBan), 0) * km.TyLeGiam / 100.0) > km.GiamToiDa 
                    THEN km.GiamToiDa
                ELSE (ISNULL(SUM(ct.DonGiaBan), 0) * km.TyLeGiam / 100.0)
            END AS TienGiam
        FROM dbo.VeXemPhim v
        INNER JOIN dbo.ChiTietVe ct ON v.MaVe = ct.MaVe
        LEFT JOIN dbo.KhuyenMai km ON v.MaKM = km.MaKM
        WHERE v.TrangThaiThanhToan = N'Da thanh toan'
          AND (v.MaKH = @MaDinhDanh OR v.MaSuatChieu = @MaDinhDanh)
        GROUP BY v.MaVe, v.MaKH, v.MaSuatChieu, km.MaKM, km.TyLeGiam, km.GiamToiDa
    )
    SELECT @TongGiaTri = ISNULL(SUM(TienGoc - TienGiam), 0)
    FROM CTE_GiaTriDonVe;

    RETURN @TongGiaTri;
END
GO

-- =========================================================================
-- BO SUNG: STORED PROCEDURES (P_DanhSachGiaoDich & P_ThemSuaXoa_Phim)
-- =========================================================================
CREATE OR ALTER PROCEDURE dbo.P_DanhSachGiaoDich
    @TuNgay DATETIME = NULL,
    @DenNgay DATETIME = NULL,
    @MaKH VARCHAR(15) = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 20,
    @TotalRecords INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    IF @PageNumber < 1 SET @PageNumber = 1;
    IF @PageSize < 1 SET @PageSize = 20;

    SELECT @TotalRecords = COUNT(v.MaVe)
    FROM dbo.VeXemPhim v
    WHERE (@TuNgay IS NULL OR v.NgayLapVe >= @TuNgay)
      AND (@DenNgay IS NULL OR v.NgayLapVe <= @DenNgay)
      AND (@MaKH IS NULL OR v.MaKH = @MaKH);

    SELECT 
        v.MaVe,
        v.NgayLapVe,
        ISNULL(v.MaKH, 'GUEST') AS MaKH,
        ISNULL(kh.HoTen, N'Khach vang lai') AS TenKhachHang,
        nv.HoTen AS NhanVienBan,
        p.TenPhim,
        sc.MaSuatChieu,
        sc.MaPhong,
        sc.ThoiGianBatDau,
        COUNT(ct.MaGhe) AS SoGheDat,
        STRING_AGG(ct.MaGhe, ', ') AS DanhSachGhe,
        ISNULL(SUM(ct.DonGiaBan), 0) AS TongTienGoc,
        v.PhuongThucThanhToan,
        v.TrangThaiThanhToan
    FROM dbo.VeXemPhim v
    INNER JOIN dbo.NhanVien nv ON v.MaNV = nv.MaNV
    INNER JOIN dbo.SuatChieu sc ON v.MaSuatChieu = sc.MaSuatChieu
    INNER JOIN dbo.Phim p ON sc.MaPhim = p.MaPhim
    LEFT JOIN dbo.KhachHang kh ON v.MaKH = kh.MaKH
    LEFT JOIN dbo.ChiTietVe ct ON v.MaVe = ct.MaVe
    WHERE (@TuNgay IS NULL OR v.NgayLapVe >= @TuNgay)
      AND (@DenNgay IS NULL OR v.NgayLapVe <= @DenNgay)
      AND (@MaKH IS NULL OR v.MaKH = @MaKH)
    GROUP BY 
        v.MaVe, v.NgayLapVe, v.MaKH, kh.HoTen, nv.HoTen,
        p.TenPhim, sc.MaSuatChieu, sc.MaPhong, sc.ThoiGianBatDau,
        v.PhuongThucThanhToan, v.TrangThaiThanhToan
    ORDER BY v.NgayLapVe DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;
GO

CREATE OR ALTER PROCEDURE dbo.P_ThemSuaXoa_Phim
    @Action VARCHAR(10),
    @MaPhim VARCHAR(15),
    @TenPhim NVARCHAR(200) = NULL,
    @TheLoai NVARCHAR(100) = NULL,
    @ThoiLuong INT = NULL,
    @DaoDien NVARCHAR(100) = NULL,
    @DoTuoiQuyDinh VARCHAR(10) = NULL,
    @NgayKhoiChieu DATE = NULL,
    @TrangThaiPhim NVARCHAR(50) = N'Dang chieu',
    @ErrorCode INT OUTPUT,
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @Action = 'INSERT'
        BEGIN
            IF EXISTS (SELECT 1 FROM dbo.Phim WHERE MaPhim = @MaPhim)
                RAISERROR(N'Ma phim [%s] da ton tai tren he thong.', 16, 1, @MaPhim);

            INSERT INTO dbo.Phim (MaPhim, TenPhim, TheLoai, ThoiLuong, DaoDien, DoTuoiQuyDinh, NgayKhoiChieu, TrangThaiPhim)
            VALUES (@MaPhim, @TenPhim, @TheLoai, @ThoiLuong, @DaoDien, @DoTuoiQuyDinh, @NgayKhoiChieu, @TrangThaiPhim);

            SET @ErrorCode = 0;
            SET @ErrorMessage = N'Them moi bo phim thanh cong.';
        END
        ELSE IF @Action = 'UPDATE'
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM dbo.Phim WHERE MaPhim = @MaPhim)
                RAISERROR(N'Khong tim thay ma phim [%s] de cap nhat.', 16, 1, @MaPhim);

            UPDATE dbo.Phim
            SET TenPhim = @TenPhim, TheLoai = @TheLoai, ThoiLuong = @ThoiLuong,
                DaoDien = @DaoDien, DoTuoiQuyDinh = @DoTuoiQuyDinh, NgayKhoiChieu = @NgayKhoiChieu, TrangThaiPhim = @TrangThaiPhim
            WHERE MaPhim = @MaPhim;

            SET @ErrorCode = 0;
            SET @ErrorMessage = N'Cap nhat thong tin phim thanh cong.';
        END
        ELSE IF @Action = 'DELETE'
        BEGIN
            IF EXISTS (SELECT 1 FROM dbo.SuatChieu WHERE MaPhim = @MaPhim)
                RAISERROR(N'Khong the xoa phim [%s] vi da phat sinh lich suat chieu.', 16, 1, @MaPhim);

            DELETE FROM dbo.Phim WHERE MaPhim = @MaPhim;

            SET @ErrorCode = 0;
            SET @ErrorMessage = N'Xoa bo phim thanh cong.';
        END
        ELSE
        BEGIN
            RAISERROR(N'Tham so @Action khong hop le.', 16, 1);
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        SET @ErrorCode = ERROR_NUMBER();
        SET @ErrorMessage = ERROR_MESSAGE();
    END CATCH
END;
GO

-- 2.4. Triggers Kiem tra Rang buoc Nghiep vu
-- =========================================================================
-- 1. TRIGGER TRG_KiemTraSuatChieu_HopLe (Tren bang SuatChieu)
-- Kiem tra thoi gian chieu khong duoc o qua khu khi them moi va khong trung
-- lich phong chieu voi suat chieu khac dang mo ban.
-- =========================================================================
CREATE OR ALTER TRIGGER dbo.TRG_KiemTraSuatChieu_HopLe
ON dbo.SuatChieu
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Kiem tra gio chieu khong duoc nam trong qua khu khi tao moi
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN deleted d ON i.MaSuatChieu = d.MaSuatChieu
        WHERE d.MaSuatChieu IS NULL -- Lenh INSERT
          AND i.ThoiGianBatDau < GETDATE()
    )
    BEGIN
        RAISERROR(N'Loi nghiep vu: Thoi gian bat dau suat chieu moi khong duoc nho hon thoi diem hien tai!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 2. Kiem tra xung dot trung phong chieu trong cung khung gio
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        INNER JOIN dbo.SuatChieu sc ON i.MaPhong = sc.MaPhong
        WHERE i.MaSuatChieu <> sc.MaSuatChieu
          AND sc.TrangThaiSuat <> N'Da huy'
          AND i.ThoiGianBatDau < sc.ThoiGianKetThuc
          AND i.ThoiGianKetThuc > sc.ThoiGianBatDau
    )
    BEGIN
        RAISERROR(N'Loi nghiep vu: Phong chieu da co suat chieu khac trung khung gio nay!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

-- =========================================================================
-- 2. TRIGGER TRG_KiemTraDatGhe_ChiTietVe (Tren bang ChiTietVe)
-- Kiem tra toan dien truoc khi hoan tat INSERT/UPDATE ghe dat:
--   + Suat chieu phai dang Mo ban va chua qua gio chieu.
--   + Ghe ngoi phai thuoc dung phong cua Suat chieu do.
--   + Ghe ngoi khong nam trong trang thai bao tri.
--   + Ghe ngoi chua bi dat boi bat ky don ve hop le nao khác trong cung suat chieu.
-- =========================================================================
CREATE OR ALTER TRIGGER dbo.TRG_KiemTraDatGhe_ChiTietVe
ON dbo.ChiTietVe
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Kiem tra suat chieu da qua gio chieu hoac da dong/huy
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN dbo.VeXemPhim v ON i.MaVe = v.MaVe
        INNER JOIN dbo.SuatChieu sc ON v.MaSuatChieu = sc.MaSuatChieu
        WHERE sc.ThoiGianBatDau < GETDATE() OR sc.TrangThaiSuat <> N'Mo ban'
    )
    BEGIN
        RAISERROR(N'Loi nghiep vu: Suat chieu da bat dau/ket thuc hoac da ngung mo ban ve!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 2. Kiem tra ghe co dang bao tri hay khong
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN dbo.GheNgoi g ON i.MaGhe = g.MaGhe
        WHERE g.TrangThaiBaoTri = 1
    )
    BEGIN
        RAISERROR(N'Loi nghiep vu: Ghe ngoi duoc chon dang trong trang thai bao tri, khong the ban!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 3. Kiem tra ghe co thuoc dung phong chieu cua suat chieu hay khong
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN dbo.VeXemPhim v ON i.MaVe = v.MaVe
        INNER JOIN dbo.SuatChieu sc ON v.MaSuatChieu = sc.MaSuatChieu
        INNER JOIN dbo.GheNgoi g ON i.MaGhe = g.MaGhe
        WHERE sc.MaPhong <> g.MaPhong
    )
    BEGIN
        RAISERROR(N'Loi nghiep vu: Ma ghe khong thuoc phong chieu cua suat chieu da chon!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 4. Kiem tra trung ghe (Double-Booking) trong cung mot suat chieu
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN dbo.VeXemPhim vMoi ON i.MaVe = vMoi.MaVe
        INNER JOIN dbo.VeXemPhim vCu ON vMoi.MaSuatChieu = vCu.MaSuatChieu
        INNER JOIN dbo.ChiTietVe ctCu ON vCu.MaVe = ctCu.MaVe AND i.MaGhe = ctCu.MaGhe
        WHERE vMoi.MaVe <> vCu.MaVe
          AND vCu.TrangThaiThanhToan <> N'Da huy'
          AND vMoi.TrangThaiThanhToan <> N'Da huy'
    )
    BEGIN
        RAISERROR(N'Loi nghiep vu: Ghe ngoi nay da co khach hang khac dat trong cung suat chieu!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

-- 2.5. Quan tri Phan quyen (Security: Logins, Users, Roles & Permissions)
USE master;
GO

-- 1. Tao SQL Server Logins o cap Server
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'Login_CineStar_Admin')
    CREATE LOGIN Login_CineStar_Admin WITH PASSWORD = 'AdminPassword@2026!', CHECK_POLICY = ON;

IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'Login_CineStar_User')
    CREATE LOGIN Login_CineStar_User WITH PASSWORD = 'UserPassword@2026!', CHECK_POLICY = ON;
GO

USE CineStarDB;
GO

-- 2. Tao Database Users anh xa voi Logins
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'User_Admin')
    CREATE USER User_Admin FOR LOGIN Login_CineStar_Admin;

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'User_Staff')
    CREATE USER User_Staff FOR LOGIN Login_CineStar_User;
GO

-- 3. Tao 2 Database Roles: Role_Admin va Role_User
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'Role_Admin' AND type = 'R')
    CREATE ROLE Role_Admin;

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'Role_User' AND type = 'R')
    CREATE ROLE Role_User;
GO

-- Gan User vao Role tuong ung
ALTER ROLE Role_Admin ADD MEMBER User_Admin;
ALTER ROLE Role_User ADD MEMBER User_Staff;
GO

-- 4. Phan quyen cho Role_Admin (Toan quyen tren CSDL CineStarDB)
GRANT CONTROL ON DATABASE::CineStarDB TO Role_Admin;
GO

-- 5. Phan quyen chi tiet cho Role_User (Nhan vien ban ve / User)
-- 5.1. Duoc phép XEM danh muc Phim, Suat chieu, Ghe ngoi, Khuyen mai
GRANT SELECT ON dbo.Phim TO Role_User;
GRANT SELECT ON dbo.SuatChieu TO Role_User;
GRANT SELECT ON dbo.GheNgoi TO Role_User;
GRANT SELECT ON dbo.KhuyenMai TO Role_User;

-- 5.2. Duoc phép Xem, Them moi va Cap nhat diem Khach hang & Giao dich dat ve
GRANT SELECT, INSERT, UPDATE ON dbo.KhachHang TO Role_User;
GRANT SELECT, INSERT, UPDATE ON dbo.VeXemPhim TO Role_User;
GRANT SELECT, INSERT ON dbo.ChiTietVe TO Role_User;

-- 5.3. Quyen tren Views, Stored Procedures va Functions
GRANT SELECT ON dbo.V_DanhSachChuaXuLy TO Role_User;
GRANT EXECUTE ON dbo.P_DanhSachGiaoDich TO Role_User;
GRANT EXECUTE ON dbo.F_TinhTongGiaTri TO Role_User;

-- 5.4. Cam (DENY) tuyet doi Role_User sua/xoa du lieu danh muc goc hoac xem doanh thu tong hop
DENY INSERT, UPDATE, DELETE ON dbo.Phim TO Role_User;
DENY INSERT, UPDATE, DELETE ON dbo.SuatChieu TO Role_User;
DENY DELETE ON dbo.VeXemPhim TO Role_User;
DENY SELECT ON dbo.V_ThongKeTongHop TO Role_User;
DENY EXECUTE ON dbo.P_ThemSuaXoa_Phim TO Role_User;

-- 5.5. Mau lenh REVOKE khi can thu hoi quyen cap nhat trang thai ve cua Role_User
-- REVOKE UPDATE ON dbo.VeXemPhim FROM Role_User;
GO

