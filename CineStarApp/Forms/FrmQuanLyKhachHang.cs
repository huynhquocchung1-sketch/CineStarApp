using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CineStarApp
{
    public partial class FrmQuanLyKhachHang : Form
    {
        private DataTable _dtKhachHang = new DataTable();

        public FrmQuanLyKhachHang()
        {
            InitializeComponent();
        }

        private void FrmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            cboHangThe.SelectedIndex = 0;
            TaiDanhSachKhachHang();
        }

        private void TaiDanhSachKhachHang()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    string sql = "SELECT MaKH, HoTen, SoDienThoai, Email, NgaySinh, DiemTichLuy, HangThanhVien FROM dbo.KhachHang ORDER BY DiemTichLuy DESC;";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        _dtKhachHang = new DataTable();
                        da.Fill(_dtKhachHang);
                        dgvKhachHang.DataSource = _dtKhachHang;
                    }
                }
                DinhDangCotKhachHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangCotKhachHang()
        {
            if (dgvKhachHang.Columns.Count == 0) return;

            dgvKhachHang.Columns["MaKH"].HeaderText = "Mã KH";
            dgvKhachHang.Columns["HoTen"].HeaderText = "Họ và Tên";
            dgvKhachHang.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            dgvKhachHang.Columns["Email"].HeaderText = "Địa Chỉ Email";
            dgvKhachHang.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvKhachHang.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvKhachHang.Columns["DiemTichLuy"].HeaderText = "Điểm Tích Lũy";
            dgvKhachHang.Columns["HangThanhVien"].HeaderText = "Hạng Thẻ";
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKhachHang.Rows.Count) return;

            DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
            txtMaKH.Text = row.Cells["MaKH"].Value?.ToString();
            txtHoTenKH.Text = row.Cells["HoTen"].Value?.ToString();
            txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString();
            txtEmailKH.Text = row.Cells["Email"].Value?.ToString();
            cboHangThe.Text = row.Cells["HangThanhVien"].Value?.ToString();

            if (int.TryParse(row.Cells["DiemTichLuy"].Value?.ToString(), out int diem))
            {
                nudDiem.Value = diem;
            }

            if (DateTime.TryParse(row.Cells["NgaySinh"].Value?.ToString(), out DateTime ns))
            {
                dtpNgaySinhKH.Value = ns;
            }
        }

        private void ThucThiKhachHang(string action)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text)) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    conn.Open();
                    string sql = "";

                    if (action == "UPDATE")
                    {
                        sql = @"
                            UPDATE dbo.KhachHang
                            SET HoTen = @HoTen, SoDienThoai = @SDT, Email = @Email, 
                                NgaySinh = @NgaySinh, DiemTichLuy = @Diem, HangThanhVien = @Hang
                            WHERE MaKH = @MaKH;";
                    }
                    else if (action == "DELETE")
                    {
                        sql = "DELETE FROM dbo.KhachHang WHERE MaKH = @MaKH;";
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaKH", SqlDbType.VarChar, 15).Value = txtMaKH.Text.Trim();
                        cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = txtHoTenKH.Text.Trim();
                        cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 15).Value = txtSDT.Text.Trim();
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = string.IsNullOrWhiteSpace(txtEmailKH.Text) ? (object)DBNull.Value : txtEmailKH.Text.Trim();
                        cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = dtpNgaySinhKH.Value.Date;
                        cmd.Parameters.Add("@Diem", SqlDbType.Int).Value = (int)nudDiem.Value;
                        cmd.Parameters.Add("@Hang", SqlDbType.NVarChar, 30).Value = cboHangThe.Text;

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thao tác thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TaiDanhSachKhachHang();
                        LamMoiFormKH();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi CSDL khi thao tác khách hàng:\n{sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaKH_Click(object sender, EventArgs e) => ThucThiKhachHang("UPDATE");

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text)) return;
            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng [{txtMaKH.Text} - {txtHoTenKH.Text}] không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes) ThucThiKhachHang("DELETE");
        }

        private void btnLamMoiKH_Click(object sender, EventArgs e)
        {
            LamMoiFormKH();
            TaiDanhSachKhachHang();
        }

        private void LamMoiFormKH()
        {
            txtMaKH.Clear();
            txtHoTenKH.Clear();
            txtSDT.Clear();
            txtEmailKH.Clear();
            nudDiem.Value = 0;
            cboHangThe.SelectedIndex = 0;
            dtpNgaySinhKH.Value = DateTime.Today;
            txtTimKiemKH.Clear();
        }

        private void txtTimKiemKH_TextChanged(object sender, EventArgs e)
        {
            if (_dtKhachHang == null) return;
            string keyword = txtTimKiemKH.Text.Trim().Replace("'", "''");
            DataView dv = _dtKhachHang.DefaultView;
            dv.RowFilter = string.IsNullOrEmpty(keyword) ? string.Empty : $"HoTen LIKE '%{keyword}%' OR SoDienThoai LIKE '%{keyword}%' OR MaKH LIKE '%{keyword}%'";
        }
    }
}