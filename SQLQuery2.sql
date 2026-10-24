USE CineStarDB;
GO
GRANT SELECT ON dbo.NhanVien TO Role_User;
GO

SELECT MaNV, TenDangNhap, MatKhauHash, VaiTro, TrangThaiLamViec 
FROM dbo.NhanVien;

USE CineStarDB;
GO

CREATE OR ALTER PROCEDURE dbo.P_DangKyKhachHang
    @HoTen NVARCHAR(100),
    @SoDienThoai VARCHAR(15),
    @Email VARCHAR(100),
    @NgaySinh DATE,
    @ErrorCode INT OUTPUT,
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra số điện thoại đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM dbo.KhachHang WHERE SoDienThoai = @SoDienThoai)
    BEGIN
        SET @ErrorCode = 1;
        SET @ErrorMessage = N'Số điện thoại này đã được đăng ký tài khoản thành viên!';
        RETURN;
    END

    -- Tự động sinh mã khách hàng tiếp theo (VD: KH003, KH004...)
    DECLARE @MaxID INT;
    SELECT @MaxID = ISNULL(MAX(CAST(SUBSTRING(MaKH, 3, LEN(MaKH)) AS INT)), 0) FROM dbo.KhachHang WHERE MaKH LIKE 'KH%';
    DECLARE @NewMaKH VARCHAR(15) = 'KH' + RIGHT('000' + CAST(@MaxID + 1 AS VARCHAR(3)), 3);

    BEGIN TRY
        INSERT INTO dbo.KhachHang (MaKH, HoTen, SoDienThoai, Email, NgaySinh, DiemTichLuy, HangThanhVien)
        VALUES (@NewMaKH, @HoTen, @SoDienThoai, @Email, @NgaySinh, 0, N'Member');

        SET @ErrorCode = 0;
        SET @ErrorMessage = N'Đăng ký tài khoản thành công! Mã thành viên của bạn là: ' + @NewMaKH;
    END TRY
    BEGIN CATCH
        SET @ErrorCode = ERROR_NUMBER();
        SET @ErrorMessage = ERROR_MESSAGE();
    END CATCH
END
GO



USE CineStarDB;
GO

DECLARE @ErrCode INT, @ErrMsg NVARCHAR(500);
EXEC dbo.P_DangKyKhachHang 
    @HoTen = N'Test User', 
    @SoDienThoai = '0999999999', 
    @Email = 'test@gmail.com', 
    @NgaySinh = '2000-01-01', 
    @ErrorCode = @ErrCode OUTPUT, 
    @ErrorMessage = @ErrMsg OUTPUT;

SELECT @ErrCode AS ErrorCode, @ErrMsg AS ErrorMessage;

USE CineStarDB;
GO

CREATE OR ALTER PROCEDURE dbo.P_DangKyKhachHang
    @HoTen NVARCHAR(100),
    @SoDienThoai VARCHAR(15),
    @Email VARCHAR(100),
    @NgaySinh DATE,
    @ErrorCode INT OUTPUT,
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra số điện thoại đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM dbo.KhachHang WHERE SoDienThoai = @SoDienThoai)
    BEGIN
        SET @ErrorCode = 1;
        SET @ErrorMessage = N'Số điện thoại này đã được đăng ký tài khoản thành viên!';
        RETURN;
    END

    -- Tự động sinh mã khách hàng tiếp theo (VD: KH003, KH004...)
    DECLARE @MaxID INT;
    SELECT @MaxID = ISNULL(MAX(CAST(SUBSTRING(MaKH, 3, LEN(MaKH)) AS INT)), 0) FROM dbo.KhachHang WHERE MaKH LIKE 'KH%';
    DECLARE @NewMaKH VARCHAR(15) = 'KH' + RIGHT('000' + CAST(@MaxID + 1 AS VARCHAR(3)), 3);

    BEGIN TRY
        INSERT INTO dbo.KhachHang (MaKH, HoTen, SoDienThoai, Email, NgaySinh, DiemTichLuy, HangThanhVien)
        VALUES (@NewMaKH, @HoTen, @SoDienThoai, @Email, @NgaySinh, 0, N'Member');

        SET @ErrorCode = 0;
        SET @ErrorMessage = N'Đăng ký tài khoản thành công! Mã thành viên của bạn là: ' + @NewMaKH;
    END TRY
    BEGIN CATCH
        SET @ErrorCode = ERROR_NUMBER();
        SET @ErrorMessage = ERROR_MESSAGE();
    END CATCH
END
GO

USE CineStarDB;
GO

-- Cấp quyền thực thi thủ tục cho mọi tài khoản kết nối (hoặc bạn có thể thay PUBLIC bằng tên user cụ thể nếu ứng dụng dùng user riêng)
GRANT EXECUTE ON dbo.P_DangKyKhachHang TO PUBLIC;
GO

SELECT * FROM dbo.KhachHang ORDER BY MaKH DESC;

SELECT * FROM dbo.NhanVien;



USE CineStarDB;
GO
SELECT * FROM dbo.GheNgoi WHERE MaPhong = 'RAP02';

SELECT COUNT(*) FROM dbo.GheNgoi WHERE MaPhong = 'Mã_Phòng_Bạn_Đang_Test'
SELECT COUNT(*) FROM dbo.GheNgoi WHERE MaPhong = 'RAP02'

USE CineStarDB;
GO

-- 1. Đảm bảo cột MatKhauHash đủ độ rộng chứa chuỗi SHA-256 (64 ký tự Hex)
ALTER TABLE dbo.NhanVien
ALTER COLUMN MatKhauHash VARCHAR(256) NOT NULL;
GO

-- 2. Chuyển đổi mật khẩu của tài khoản 'admin' (Admin@123) và 'thungan01' (User@123) sang SHA-256
UPDATE dbo.NhanVien
SET MatKhauHash = CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CAST('Admin@123' AS VARCHAR(100))), 2)
WHERE TenDangNhap = 'admin';

UPDATE dbo.NhanVien
SET MatKhauHash = CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CAST('User@123' AS VARCHAR(100))), 2)
WHERE TenDangNhap = 'thungan01';
GO

-- 3. Kiểm tra kết quả sau khi băm (Mật khẩu đã ẩn hoàn toàn thành chuỗi 64 ký tự Hex)
SELECT MaNV, HoTen, TenDangNhap, MatKhauHash, VaiTro, TrangThaiLamViec
FROM dbo.NhanVien;
GO

USE CineStarDB;
GO

-- Chỉ cập nhật riêng suất chiếu SC002 đang test sang tương lai và trỏ về phòng RAP02
UPDATE dbo.SuatChieu 
SET MaPhong = 'RAP02',
    TrangThaiSuat = N'Mở bán',
    ThoiGianBatDau = DATEADD(DAY, 1, GETDATE()),
    ThoiGianKetThuc = DATEADD(HOUR, 2, DATEADD(DAY, 1, GETDATE())) -- Thời gian kết thúc sau 2 tiếng
WHERE MaSuatChieu = 'SC002';
GO


USE CineStarDB;
GO

SELECT COUNT(*) AS SoLuongGhePhapRAP02 
FROM dbo.GheNgoi 
WHERE MaPhong = 'RAP02';

USE CineStarDB;
GO

-- Thêm 40 ghế mới cho phòng RAP02 (Hàng A, B, C, D - mỗi hàng 10 ghế)
INSERT INTO dbo.GheNgoi (MaGhe, MaPhong, HangGhe, SoGhe, LoaiGhe, PhuThuGhe, TrangThaiBaoTri) VALUES
-- Hàng A (Ghế Thường - từ số 2 đến 10)
('R2_A02', 'RAP02', 'A', 2, N'Thuong', 0, 0),
('R2_A03', 'RAP02', 'A', 3, N'Thuong', 0, 0),
('R2_A04', 'RAP02', 'A', 4, N'Thuong', 0, 0),
('R2_A05', 'RAP02', 'A', 5, N'Thuong', 0, 0),
('R2_A06', 'RAP02', 'A', 6, N'Thuong', 0, 0),
('R2_A07', 'RAP02', 'A', 7, N'Thuong', 0, 0),
('R2_A08', 'RAP02', 'A', 8, N'Thuong', 0, 0),
('R2_A09', 'RAP02', 'A', 9, N'Thuong', 0, 0),
('R2_A10', 'RAP02', 'A', 10, N'Thuong', 0, 0),

-- Hàng B (Ghế Thường - từ số 1 đến 10)
('R2_B01', 'RAP02', 'B', 1, N'Thuong', 0, 0),
('R2_B02', 'RAP02', 'B', 2, N'Thuong', 0, 0),
('R2_B03', 'RAP02', 'B', 3, N'Thuong', 0, 0),
('R2_B04', 'RAP02', 'B', 4, N'Thuong', 0, 0),
('R2_B05', 'RAP02', 'B', 5, N'Thuong', 0, 0),
('R2_B06', 'RAP02', 'B', 6, N'Thuong', 0, 0),
('R2_B07', 'RAP02', 'B', 7, N'Thuong', 0, 0),
('R2_B08', 'RAP02', 'B', 8, N'Thuong', 0, 0),
('R2_B09', 'RAP02', 'B', 9, N'Thuong', 0, 0),
('R2_B10', 'RAP02', 'B', 10, N'Thuong', 0, 0),

-- Hàng C (Ghế VIP - từ số 1 đến 10)
('R2_C01', 'RAP02', 'C', 1, N'VIP', 30000, 0),
('R2_C02', 'RAP02', 'C', 2, N'VIP', 30000, 0),
('R2_C03', 'RAP02', 'C', 3, N'VIP', 30000, 0),
('R2_C04', 'RAP02', 'C', 4, N'VIP', 30000, 0),
('R2_C05', 'RAP02', 'C', 5, N'VIP', 30000, 0),
('R2_C06', 'RAP02', 'C', 6, N'VIP', 30000, 0),
('R2_C07', 'RAP02', 'C', 7, N'VIP', 30000, 0),
('R2_C08', 'RAP02', 'C', 8, N'VIP', 30000, 0),
('R2_C09', 'RAP02', 'C', 9, N'VIP', 30000, 0),
('R2_C10', 'RAP02', 'C', 10, N'VIP', 30000, 0),

-- Hàng D (Ghế Couple - từ số 1 đến 10)
('R2_D01', 'RAP02', 'D', 1, N'Couple', 50000, 0),
('R2_D02', 'RAP02', 'D', 2, N'Couple', 50000, 0),
('R2_D03', 'RAP02', 'D', 3, N'Couple', 50000, 0),
('R2_D04', 'RAP02', 'D', 4, N'Couple', 50000, 0),
('R2_D05', 'RAP02', 'D', 5, N'Couple', 50000, 0),
('R2_D06', 'RAP02', 'D', 6, N'Couple', 50000, 0),
('R2_D07', 'RAP02', 'D', 7, N'Couple', 50000, 0),
('R2_D08', 'RAP02', 'D', 8, N'Couple', 50000, 0),
('R2_D09', 'RAP02', 'D', 9, N'Couple', 50000, 0),
('R2_D10', 'RAP02', 'D', 10, N'Couple', 50000, 0);
GO