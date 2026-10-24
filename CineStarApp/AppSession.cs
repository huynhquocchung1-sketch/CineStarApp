using System;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace CineStarApp
{
    /// <summary>
    /// Lớp tĩnh (Static Class) lưu trữ phiên làm việc của nhân viên
    /// và khởi tạo chuỗi kết nối động theo phân quyền SQL Server và App.config.
    /// </summary>
    public static class AppSession
    {
        // Đọc tự động tên Server từ App.config (có cơ chế Fallback an toàn về .\SQLEXPRESS nếu thiếu)
        public static string ServerName =>
            ConfigurationManager.AppSettings["ServerName"] ?? @".\SQLEXPRESS";

        // Đọc tự động tên Database từ App.config
        public static string DatabaseName =>
            ConfigurationManager.AppSettings["DatabaseName"] ?? "CineStarDB";

        // Đọc giới hạn số ghế tối đa trong 1 giao dịch (mặc định là 8 ghế)
        public static int MaxSeatPerTransaction =>
            int.TryParse(ConfigurationManager.AppSettings["MaxSeatPerTransaction"], out int max) ? max : 8;

        // Thông tin nhân viên đang đăng nhập trong phiên hiện tại
        public static string MaNV { get; set; } = string.Empty;
        public static string HoTen { get; set; } = string.Empty;
        public static string ChucVu { get; set; } = string.Empty;
        public static string Role { get; set; } = string.Empty; // Giá trị: "Admin" hoặc "User"
       

        // Chuỗi kết nối hiện hành được sinh theo Role
        public static string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Khởi tạo chuỗi kết nối SQL Server dựa trên nhóm quyền người dùng chọn lúc đăng nhập.
        /// Ưu tiên đọc từ <connectionStrings> trong App.config, tự động đồng bộ Server/Database và có Fallback an toàn.
        /// </summary>
        /// <param name="selectedRole">"Admin" hoặc "User"</param>
        /// 

        /// <summary>
        /// Mã hóa mật khẩu một chiều theo chuẩn SHA-256 (Trả về chuỗi 64 ký tự Hex viết hoa)
        /// </summary>
        public static string ComputeSha256Hash(string rawData)
        {
            if (string.IsNullOrEmpty(rawData)) return string.Empty;

            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
            return Convert.ToHexString(bytes); // Trả về chuỗi Hex in hoa khớp với CONVERT(..., 2) của SQL Server
        }
        public static string BuildRoleConnectionString(string selectedRole)
        {
            string configName = string.Equals(selectedRole, "Admin", StringComparison.OrdinalIgnoreCase)
                ? "CineStar_Admin"
                : "CineStar_User";

            // 1. Ưu tiên đọc từ thẻ <connectionStrings> trong App.config
            ConnectionStringSettings? settings = ConfigurationManager.ConnectionStrings[configName];
            if (settings != null && !string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                // Đồng bộ lại Data Source và Initial Catalog theo appSettings (giúp đổi máy Lab cực nhanh)
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(settings.ConnectionString)
                {
                    DataSource = ServerName,
                    InitialCatalog = DatabaseName,
                    TrustServerCertificate = true,
                    MultipleActiveResultSets = true
                };
                ConnectionString = builder.ConnectionString;
                return ConnectionString;
            }

            // 2. Fallback an toàn trong trường hợp file App.config bị thiếu connectionStrings
            SqlConnectionStringBuilder fallbackBuilder = new SqlConnectionStringBuilder
            {
                DataSource = ServerName,
                InitialCatalog = DatabaseName,
                UserID = string.Equals(selectedRole, "Admin", StringComparison.OrdinalIgnoreCase)
                    ? "Login_CineStar_Admin"
                    : "Login_CineStar_User",
                Password = string.Equals(selectedRole, "Admin", StringComparison.OrdinalIgnoreCase)
                    ? "AdminPassword@2026!"
                    : "UserPassword@2026!",
                TrustServerCertificate = true,
                ConnectTimeout = 15,
                MultipleActiveResultSets = true
            };

            ConnectionString = fallbackBuilder.ConnectionString;
            return ConnectionString;
        }

        /// <summary>
        /// Xóa sạch thông tin phiên làm việc khi người dùng bấm "Đăng xuất"
        /// </summary>
        public static void ClearSession()
        {
            MaNV = string.Empty;
            HoTen = string.Empty;
            ChucVu = string.Empty;
            Role = string.Empty;
            ConnectionString = string.Empty;
        }
    }
}