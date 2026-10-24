using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CineStarApp
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            if (cboRole.Items.Count > 0)
            {
                cboRole.SelectedIndex = 0; // Mặc định chọn phân quyền đầu tiên (Admin)
            }
            lblErrorMessage.Visible = false;
            txtUsername.Focus();
        }

        /// <summary>
        /// Xử lý ẩn/hiện ký tự mật khẩu khi người dùng check/uncheck.
        /// </summary>
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        /// <summary>
        /// Xử lý xác thực tài khoản, mã hóa mật khẩu SHA-256 và kết nối SQL Server theo Role phân quyền.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;

            string username = txtUsername.Text.Trim();
            string rawPassword = txtPassword.Text.Trim();
            string selectedRole = cboRole.SelectedItem?.ToString() ?? "User";

            // 1. Kiểm tra dữ liệu đầu vào trống
            if (string.IsNullOrWhiteSpace(username))
            {
                ShowInlineError("Vui lòng nhập Tên đăng nhập!");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(rawPassword))
            {
                ShowInlineError("Vui lòng nhập Mật khẩu!");
                txtPassword.Focus();
                return;
            }

            try
            {
                // 2. Băm mật khẩu người dùng nhập sang chuẩn SHA-256 (64 ký tự Hex viết hoa)
                string passwordHash = AppSession.ComputeSha256Hash(rawPassword);

                // 3. Khởi tạo chuỗi kết nối động dựa trên Role đã chọn (Login_CineStar_Admin / Login_CineStar_User)
                string roleConnString = AppSession.BuildRoleConnectionString(selectedRole);

                using (SqlConnection conn = new SqlConnection(roleConnString))
                {
                    conn.Open();

                    // 4. Truy vấn đối chiếu tài khoản (Hỗ trợ kiểm tra cả chuẩn SHA-256 và tương thích ngược mật khẩu thô)
                    string sql = @"
                        SELECT MaNV, HoTen, VaiTro 
                        FROM dbo.NhanVien 
                        WHERE TenDangNhap = @TenDangNhap 
                          AND (MatKhauHash = @MatKhauHash OR MatKhauHash = @MatKhauRaw)
                          AND VaiTro = @VaiTro 
                          AND TrangThaiLamViec = 1;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@TenDangNhap", SqlDbType.VarChar, 50).Value = username;
                        cmd.Parameters.Add("@MatKhauHash", SqlDbType.VarChar, 256).Value = passwordHash;
                        cmd.Parameters.Add("@MatKhauRaw", SqlDbType.VarChar, 256).Value = rawPassword;
                        cmd.Parameters.Add("@VaiTro", SqlDbType.VarChar, 20).Value = selectedRole;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 5. Lưu thông tin phiên làm việc vào AppSession toàn cục
                                AppSession.MaNV = reader["MaNV"].ToString() ?? "";
                                AppSession.HoTen = reader["HoTen"].ToString() ?? "";
                                AppSession.Role = reader["VaiTro"].ToString() ?? selectedRole;
                                AppSession.ConnectionString = roleConnString;

                                // 6. Mở Form Điều hướng chính (MDI Container / Dashboard)
                                this.Hide();
                                using (FrmMainDashboard dashboard = new FrmMainDashboard())
                                {
                                    DialogResult result = dashboard.ShowDialog();
                                    if (result == DialogResult.Retry)
                                    {
                                        // Người dùng bấm Đăng xuất từ Dashboard -> Reset và hiển thị lại Form Login
                                        txtPassword.Clear();
                                        lblErrorMessage.Visible = false;
                                        this.Show();
                                        txtUsername.Focus();
                                    }
                                    else
                                    {
                                        this.Close();
                                    }
                                }
                            }
                            else
                            {
                                ShowInlineError("Sai tài khoản, mật khẩu hoặc không đúng nhóm quyền (Role)!");
                                txtPassword.Focus();
                                txtPassword.SelectAll();
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                ShowInlineError($"Lỗi kết nối SQL Server ({sqlEx.Number}): {sqlEx.Message}");
                MessageBox.Show($"Không thể kết nối CSDL với quyền [{selectedRole}].\nChi tiết: {sqlEx.Message}",
                    "Lỗi Phân Quyền CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowInlineError($"Lỗi hệ thống: {ex.Message}");
            }
        }

        private void ShowInlineError(string message)
        {
            lblErrorMessage.Text = message;
            lblErrorMessage.Visible = true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (FrmRegister regForm = new FrmRegister())
            {
                regForm.ShowDialog();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát ứng dụng CineStar?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}