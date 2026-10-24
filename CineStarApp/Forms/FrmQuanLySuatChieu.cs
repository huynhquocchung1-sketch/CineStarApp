using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CineStarApp
{
    public partial class FrmQuanLySuatChieu : Form
    {
        private DataTable _dtSuatChieu = new DataTable();

        public FrmQuanLySuatChieu()
        {
            InitializeComponent();
        }

        private void FrmQuanLySuatChieu_Load(object sender, EventArgs e)
        {
            // Bảo vệ phân quyền: Chỉ Admin mới được quản lý suất chiếu
            if (!string.Equals(AppSession.Role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Tài khoản User không có quyền quản lý Suất Chiếu!",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            cboPhongChieu.SelectedIndex = 0;
            cboDinhDang.SelectedIndex = 0;
            cboTrangThaiSC.SelectedIndex = 0;
            dtpBatDau.Value = DateTime.Now.AddHours(1);
            dtpKetThuc.Value = DateTime.Now.AddHours(3);

            TaiDanhMucPhimChoSuatChieu();
            TaiDanhSachSuatChieu();
        }

        private void TaiDanhMucPhimChoSuatChieu()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    string sql = "SELECT MaPhim, (TenPhim + ' [' + DoTuoiQuyDinh + ']') AS TenHienThi FROM dbo.Phim ORDER BY TenPhim;";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboPhimSC.DataSource = dt;
                    cboPhimSC.DisplayMember = "TenHienThi";
                    cboPhimSC.ValueMember = "MaPhim";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục phim: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaiDanhSachSuatChieu()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    string sql = @"
                        SELECT 
                            sc.MaSuatChieu,
                            sc.MaPhim,
                            p.TenPhim,
                            sc.MaPhong,
                            sc.ThoiGianBatDau,
                            sc.ThoiGianKetThuc,
                            sc.DinhDangChieu,
                            sc.GiaVeCoBan,
                            sc.TrangThaiSuat
                        FROM dbo.SuatChieu sc
                        INNER JOIN dbo.Phim p ON sc.MaPhim = p.MaPhim
                        ORDER BY sc.ThoiGianBatDau DESC;";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        _dtSuatChieu = new DataTable();
                        da.Fill(_dtSuatChieu);
                        dgvSuatChieu.DataSource = _dtSuatChieu;
                    }
                }

                DinhDangCotSuatChieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách suất chiếu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangCotSuatChieu()
        {
            if (dgvSuatChieu.Columns.Count == 0) return;

            dgvSuatChieu.Columns["MaSuatChieu"].HeaderText = "Mã Suất";
            dgvSuatChieu.Columns["MaPhim"].Visible = false;
            dgvSuatChieu.Columns["TenPhim"].HeaderText = "Tên Bộ Phim";
            dgvSuatChieu.Columns["MaPhong"].HeaderText = "Phòng Chiếu";
            dgvSuatChieu.Columns["ThoiGianBatDau"].HeaderText = "Bắt Đầu";
            dgvSuatChieu.Columns["ThoiGianBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvSuatChieu.Columns["ThoiGianKetThuc"].HeaderText = "Kết Thúc";
            dgvSuatChieu.Columns["ThoiGianKetThuc"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvSuatChieu.Columns["DinhDangChieu"].HeaderText = "Định Dạng";
            dgvSuatChieu.Columns["GiaVeCoBan"].HeaderText = "Giá Cơ Bản";
            dgvSuatChieu.Columns["GiaVeCoBan"].DefaultCellStyle.Format = "N0";
            dgvSuatChieu.Columns["TrangThaiSuat"].HeaderText = "Trạng Thái";
        }

        private void dgvSuatChieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvSuatChieu.Rows.Count) return;

            DataGridViewRow row = dgvSuatChieu.Rows[e.RowIndex];
            txtMaSuat.Text = row.Cells["MaSuatChieu"].Value?.ToString();
            txtMaSuat.ReadOnly = true;
            txtMaSuat.BackColor = Color.FromArgb(235, 238, 242);

            cboPhimSC.SelectedValue = row.Cells["MaPhim"].Value?.ToString();
            cboPhongChieu.Text = row.Cells["MaPhong"].Value?.ToString();
            cboDinhDang.Text = row.Cells["DinhDangChieu"].Value?.ToString();
            cboTrangThaiSC.Text = row.Cells["TrangThaiSuat"].Value?.ToString();

            if (decimal.TryParse(row.Cells["GiaVeCoBan"].Value?.ToString(), out decimal gia))
            {
                nudGiaVe.Value = gia;
            }

            if (DateTime.TryParse(row.Cells["ThoiGianBatDau"].Value?.ToString(), out DateTime start))
            {
                dtpBatDau.Value = start;
            }

            if (DateTime.TryParse(row.Cells["ThoiGianKetThuc"].Value?.ToString(), out DateTime end))
            {
                dtpKetThuc.Value = end;
            }
        }

        private void ThucThiSuatChieu(string action)
        {
            if (string.IsNullOrWhiteSpace(txtMaSuat.Text) || cboPhimSC.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ Mã suất và chọn Phim!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpKetThuc.Value <= dtpBatDau.Value)
            {
                MessageBox.Show("Thời gian kết thúc phải lớn hơn thời gian bắt đầu suất chiếu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    conn.Open();
                    string sql = "";

                    if (action == "INSERT")
                    {
                        sql = @"
                            INSERT INTO dbo.SuatChieu (MaSuatChieu, MaPhim, MaPhong, ThoiGianBatDau, ThoiGianKetThuc, DinhDangChieu, GiaVeCoBan, TrangThaiSuat)
                            VALUES (@MaSuat, @MaPhim, @MaPhong, @BatDau, @KetThuc, @DinhDang, @GiaVe, @TrangThai);";
                    }
                    else if (action == "UPDATE")
                    {
                        sql = @"
                            UPDATE dbo.SuatChieu
                            SET MaPhim = @MaPhim, MaPhong = @MaPhong, ThoiGianBatDau = @BatDau, 
                                ThoiGianKetThuc = @KetThuc, DinhDangChieu = @DinhDang, GiaVeCoBan = @GiaVe, TrangThaiSuat = @TrangThai
                            WHERE MaSuatChieu = @MaSuat;";
                    }
                    else if (action == "DELETE")
                    {
                        sql = "DELETE FROM dbo.SuatChieu WHERE MaSuatChieu = @MaSuat;";
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaSuat", SqlDbType.VarChar, 15).Value = txtMaSuat.Text.Trim();
                        cmd.Parameters.Add("@MaPhim", SqlDbType.VarChar, 15).Value = cboPhimSC.SelectedValue.ToString();
                        cmd.Parameters.Add("@MaPhong", SqlDbType.VarChar, 15).Value = cboPhongChieu.Text;
                        cmd.Parameters.Add("@BatDau", SqlDbType.DateTime).Value = dtpBatDau.Value;
                        cmd.Parameters.Add("@KetThuc", SqlDbType.DateTime).Value = dtpKetThuc.Value;
                        cmd.Parameters.Add("@DinhDang", SqlDbType.VarChar, 20).Value = cboDinhDang.Text;
                        cmd.Parameters.Add("@GiaVe", SqlDbType.Decimal).Value = nudGiaVe.Value;
                        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 30).Value = cboTrangThaiSC.Text;

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thực thi nghiệp vụ suất chiếu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TaiDanhSachSuatChieu();
                        LamMoiFormSC();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Bắt trực tiếp thông báo lỗi từ Trigger TRG_KiemTraSuatChieu_HopLe trên SQL Server
                MessageBox.Show($"CSDL từ chối (Trigger bắt lỗi):\n{sqlEx.Message}",
                    "Vi phạm Ràng buộc Suất Chiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemSC_Click(object sender, EventArgs e) => ThucThiSuatChieu("INSERT");
        private void btnSuaSC_Click(object sender, EventArgs e) => ThucThiSuatChieu("UPDATE");

        private void btnHuySC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSuat.Text)) return;
            DialogResult confirm = MessageBox.Show($"Bạn có muốn xóa suất chiếu [{txtMaSuat.Text}] không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes) ThucThiSuatChieu("DELETE");
        }

        private void btnLamMoiSC_Click(object sender, EventArgs e)
        {
            LamMoiFormSC();
            TaiDanhSachSuatChieu();
        }

        private void LamMoiFormSC()
        {
            txtMaSuat.Clear();
            txtMaSuat.ReadOnly = false;
            txtMaSuat.BackColor = Color.White;
            cboPhongChieu.SelectedIndex = 0;
            cboDinhDang.SelectedIndex = 0;
            cboTrangThaiSC.SelectedIndex = 0;
            nudGiaVe.Value = 90000;
            dtpBatDau.Value = DateTime.Now.AddHours(1);
            dtpKetThuc.Value = DateTime.Now.AddHours(3);
            txtTimKiemSC.Clear();
            txtMaSuat.Focus();
        }

        private void txtTimKiemSC_TextChanged(object sender, EventArgs e)
        {
            if (_dtSuatChieu == null) return;
            string keyword = txtTimKiemSC.Text.Trim().Replace("'", "''");
            DataView dv = _dtSuatChieu.DefaultView;
            dv.RowFilter = string.IsNullOrEmpty(keyword) ? string.Empty : $"TenPhim LIKE '%{keyword}%' OR MaPhong LIKE '%{keyword}%' OR MaSuatChieu LIKE '%{keyword}%'";
        }
    }
}