using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace CineStarApp
{
    public partial class FrmRegister : Form
    {
        public FrmRegister()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string email = txtEmail.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value.Date;

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Họ tên và Số điện thoại!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy chuỗi kết nối động từ App.config (nếu chưa đăng nhập thì dùng kênh kết nối User)
                string connString = !string.IsNullOrEmpty(AppSession.ConnectionString)
                    ? AppSession.ConnectionString
                    : AppSession.BuildRoleConnectionString("User");

                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("dbo.P_DangKyKhachHang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Khai báo tường minh kiểu dữ liệu và kích thước khớp 100% với tham số của Stored Procedure
                    cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = hoTen;
                    cmd.Parameters.Add("@SoDienThoai", SqlDbType.VarChar, 15).Value = sdt;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value =
                        string.IsNullOrEmpty(email) ? DBNull.Value : email;
                    cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = ngaySinh;

                    SqlParameter pErrCode = cmd.Parameters.Add("@ErrorCode", SqlDbType.Int);
                    pErrCode.Direction = ParameterDirection.Output;

                    SqlParameter pErrMsg = cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 500);
                    pErrMsg.Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    int errCode = (pErrCode.Value != DBNull.Value) ? Convert.ToInt32(pErrCode.Value) : -1;
                    string errMsg = pErrMsg.Value?.ToString() ?? "Không có phản hồi từ máy chủ.";

                    if (errCode == 0)
                    {
                        MessageBox.Show(errMsg, "Đăng ký thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(errMsg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối hoặc thực thi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}