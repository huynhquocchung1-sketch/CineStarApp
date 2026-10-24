using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace CineStarApp
{
    public partial class FrmQuanLyPhim : Form
    {
        private DataTable _dtPhim = new DataTable();

        public FrmQuanLyPhim()
        {
            InitializeComponent();
        }

        private void FrmQuanLyPhim_Load(object sender, EventArgs e)
        {
            cboDoTuoi.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 1; // "Đang chiếu"
            LoadDanhSachPhim();
        }

        /// <summary>
        /// Tải danh sách phim từ CSDL lên DataGridView qua SqlDataAdapter
        /// </summary>
        private void LoadDanhSachPhim()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    string query = @"
                        SELECT 
                            MaPhim, 
                            TenPhim, 
                            TheLoai, 
                            ThoiLuong, 
                            DaoDien, 
                            DoTuoiQuyDinh, 
                            NgayKhoiChieu, 
                            TrangThaiPhim 
                        FROM dbo.Phim 
                        ORDER BY NgayKhoiChieu DESC;";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        _dtPhim = new DataTable();
                        da.Fill(_dtPhim);
                        dgvPhim.DataSource = _dtPhim;
                    }
                }

                FormatGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách phim: {ex.Message}",
                    "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGridColumns()
        {
            if (dgvPhim.Columns.Count == 0) return;

            dgvPhim.Columns["MaPhim"].HeaderText = "Mã Phim";
            dgvPhim.Columns["TenPhim"].HeaderText = "Tên Bộ Phim";
            dgvPhim.Columns["TheLoai"].HeaderText = "Thể Loại";
            dgvPhim.Columns["ThoiLuong"].HeaderText = "Thời Lượng (Phút)";
            dgvPhim.Columns["DaoDien"].HeaderText = "Đạo Diễn";
            dgvPhim.Columns["DoTuoiQuyDinh"].HeaderText = "Độ Tuổi";
            dgvPhim.Columns["NgayKhoiChieu"].HeaderText = "Ngày Khởi Chiếu";
            dgvPhim.Columns["TrangThaiPhim"].HeaderText = "Trạng Thái";
        }

        /// <summary>
        /// Đổ dữ liệu từ dòng được chọn trên lưới lên các ô nhập liệu
        /// </summary>
        private void dgvPhim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPhim.Rows.Count) return;

            DataGridViewRow row = dgvPhim.Rows[e.RowIndex];
            txtMaPhim.Text = row.Cells["MaPhim"].Value?.ToString();
            txtMaPhim.ReadOnly = true; // Không cho sửa khóa chính khi đã chọn dòng
            txtTenPhim.Text = row.Cells["TenPhim"].Value?.ToString();
            txtTheLoai.Text = row.Cells["TheLoai"].Value?.ToString();
            txtDaoDien.Text = row.Cells["DaoDien"].Value?.ToString();

            if (int.TryParse(row.Cells["ThoiLuong"].Value?.ToString(), out int thoiLuong))
            {
                nudThoiLuong.Value = thoiLuong;
            }

            cboDoTuoi.Text = row.Cells["DoTuoiQuyDinh"].Value?.ToString();
            cboTrangThai.Text = row.Cells["TrangThaiPhim"].Value?.ToString();

            if (DateTime.TryParse(row.Cells["NgayKhoiChieu"].Value?.ToString(), out DateTime ngayKC))
            {
                dtpNgayKhoiChieu.Value = ngayKC;
            }
        }

        /// <summary>
        /// Thực thi Stored Procedure dbo.P_ThemSuaXoa_Phim và hứng tham số OUTPUT
        /// </summary>
        private void ThucThiNghiepVuPhim(string action)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.P_ThemSuaXoa_Phim", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Action", action);
                    cmd.Parameters.AddWithValue("@MaPhim", txtMaPhim.Text.Trim());
                    cmd.Parameters.AddWithValue("@TenPhim", txtTenPhim.Text.Trim());
                    cmd.Parameters.AddWithValue("@TheLoai", txtTheLoai.Text.Trim());
                    cmd.Parameters.AddWithValue("@ThoiLuong", (int)nudThoiLuong.Value);
                    cmd.Parameters.AddWithValue("@DaoDien",
                        string.IsNullOrWhiteSpace(txtDaoDien.Text) ? (object)DBNull.Value : txtDaoDien.Text.Trim());
                    cmd.Parameters.AddWithValue("@DoTuoiQuyDinh", cboDoTuoi.Text);
                    cmd.Parameters.AddWithValue("@NgayKhoiChieu", dtpNgayKhoiChieu.Value.Date);
                    cmd.Parameters.AddWithValue("@TrangThaiPhim", cboTrangThai.Text);

                    // Khai báo 2 tham số OUTPUT từ Stored Procedure
                    SqlParameter paramErrCode = new SqlParameter("@ErrorCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter paramErrMsg = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(paramErrCode);
                    cmd.Parameters.Add(paramErrMsg);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    int errorCode = paramErrCode.Value != DBNull.Value ? Convert.ToInt32(paramErrCode.Value) : -1;
                    string errorMessage = paramErrMsg.Value?.ToString() ?? "Không xác định";

                    if (errorCode == 0)
                    {
                        MessageBox.Show(errorMessage, "Thông báo thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachPhim();
                        LamMoiForm();
                    }
                    else
                    {
                        MessageBox.Show($"Từ chối nghiệp vụ (Mã lỗi {errorCode}):\n{errorMessage}",
                            "Cảnh báo Ràng buộc CSDL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi bảo mật/Phân quyền SQL Server:\n{sqlEx.Message}",
                    "Lỗi SQL Server", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool KiemTraHopLeDauVao()
        {
            if (string.IsNullOrWhiteSpace(txtMaPhim.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Phim!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhim.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenPhim.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Bộ Phim!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPhim.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTheLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập Thể Loại phim!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTheLoai.Focus();
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLeDauVao()) return;
            ThucThiNghiepVuPhim("INSERT");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLeDauVao()) return;
            ThucThiNghiepVuPhim("UPDATE");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhim.Text))
            {
                MessageBox.Show("Vui lòng chọn bộ phim cần xóa trên lưới!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa bộ phim [{txtMaPhim.Text} - {txtTenPhim.Text}] không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                ThucThiNghiepVuPhim("DELETE");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
            LoadDanhSachPhim();
        }

        private void LamMoiForm()
        {
            txtMaPhim.Clear();
            txtMaPhim.ReadOnly = false;
            txtTenPhim.Clear();
            txtTheLoai.Clear();
            txtDaoDien.Clear();
            nudThoiLuong.Value = 120;
            cboDoTuoi.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 1;
            dtpNgayKhoiChieu.Value = DateTime.Today;
            txtTimKiem.Clear();
            txtMaPhim.Focus();
        }

        /// <summary>
        /// Lọc nhanh trên DataView theo Tên phim hoặc Thể loại
        /// </summary>
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (_dtPhim == null || _dtPhim.Rows.Count == 0) return;

            string keyword = txtTimKiem.Text.Trim().Replace("'", "''");
            DataView dv = _dtPhim.DefaultView;
            if (string.IsNullOrEmpty(keyword))
            {
                dv.RowFilter = string.Empty;
            }
            else
            {
                dv.RowFilter = $"TenPhim LIKE '%{keyword}%' OR TheLoai LIKE '%{keyword}%' OR MaPhim LIKE '%{keyword}%'";
            }
        }
    }
}