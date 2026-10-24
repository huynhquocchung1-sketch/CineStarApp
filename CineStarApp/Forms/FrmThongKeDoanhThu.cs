using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace CineStarApp
{
    public partial class FrmThongKeDoanhThu : Form
    {
        private DataTable _dtDoanhThu = new DataTable();
        private DataTable _dtChuaXuLy = new DataTable();

        // Biến quản lý trạng thái phân trang cho Tab 2 (P_DanhSachGiaoDich)
        private int _currentPage = 1;
        private int _totalPages = 1;
        private int _totalRecords = 0;

        public FrmThongKeDoanhThu()
        {
            InitializeComponent();
        }

        private void FrmThongKeDoanhThu_Load(object sender, EventArgs e)
        {
            // Bảo vệ phân quyền 2 lớp (Defense-in-Depth): Chặn nếu không phải Admin
            if (!string.Equals(AppSession.Role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Tài khoản của bạn không có quyền xem Báo cáo Thống kê Doanh thu!",
                    "Từ chối truy cập (RBAC)", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            // Thiết lập giá trị mặc định cho bộ lọc ngày
            nudThang.Value = DateTime.Today.Month;
            nudNam.Value = DateTime.Today.Year;
            dtpTuNgay.Value = DateTime.Today.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Today.AddDays(1);
            cboLoaiCanXuLy.SelectedIndex = 0;

            // Tải toàn bộ dữ liệu cho cả 3 Tab
            TaiViewThongKeTongHop(locTheoThang: false);
            TaiDanhMucKhachHangChoBoLoc();
            TaiDanhSachGiaoDichPhanTrang(1);
            TaiViewDanhSachChuaXuLy();
        }

        #region TAB 1: VIEW V_ThongKeTongHop & FUNCTION F_TinhTongGiaTri

        /// <summary>
        /// Truy vấn dữ liệu từ View dbo.V_ThongKeTongHop và tính tổng KPI
        /// </summary>
        private void TaiViewThongKeTongHop(bool locTheoThang)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    string sql = @"
                        SELECT 
                            NgayGiaoDich,
                            ThangGiaoDich,
                            NamGiaoDich,
                            MaPhim,
                            TenPhim,
                            MaSuatChieu,
                            MaPhong,
                            DinhDangChieu,
                            TenNhanVien,
                            TongSoHoaDonVe,
                            TongSoGheBanRa,
                            DoanhThuTruocGiam,
                            TongTienGiamGia,
                            DoanhThuThucThu
                        FROM dbo.V_ThongKeTongHop ";

                    if (locTheoThang)
                    {
                        sql += "WHERE ThangGiaoDich = @Thang AND NamGiaoDich = @Nam ";
                    }

                    sql += "ORDER BY NgayGiaoDich DESC, DoanhThuThucThu DESC;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (locTheoThang)
                        {
                            cmd.Parameters.Add("@Thang", SqlDbType.Int).Value = (int)nudThang.Value;
                            cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = (int)nudNam.Value;
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            _dtDoanhThu = new DataTable();
                            da.Fill(_dtDoanhThu);
                            dgvThongKeTongHop.DataSource = _dtDoanhThu;
                        }
                    }
                }

                DinhDangCotThongKe();
                CapNhatTheKPI();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi truy vấn View V_ThongKeTongHop:\n{sqlEx.Message}",
                    "Lỗi SQL Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangCotThongKe()
        {
            if (dgvThongKeTongHop.Columns.Count == 0) return;

            dgvThongKeTongHop.Columns["NgayGiaoDich"].HeaderText = "Ngày GD";
            dgvThongKeTongHop.Columns["NgayGiaoDich"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvThongKeTongHop.Columns["ThangGiaoDich"].HeaderText = "Tháng";
            dgvThongKeTongHop.Columns["NamGiaoDich"].HeaderText = "Năm";
            dgvThongKeTongHop.Columns["MaPhim"].HeaderText = "Mã Phim";
            dgvThongKeTongHop.Columns["TenPhim"].HeaderText = "Tên Bộ Phim";
            dgvThongKeTongHop.Columns["MaSuatChieu"].HeaderText = "Mã Suất";
            dgvThongKeTongHop.Columns["MaPhong"].HeaderText = "Phòng";
            dgvThongKeTongHop.Columns["DinhDangChieu"].HeaderText = "Định Dạng";
            dgvThongKeTongHop.Columns["TenNhanVien"].HeaderText = "Nhân Viên Lập";
            dgvThongKeTongHop.Columns["TongSoHoaDonVe"].HeaderText = "Số Đơn Vé";
            dgvThongKeTongHop.Columns["TongSoGheBanRa"].HeaderText = "Số Ghế Bán";
            dgvThongKeTongHop.Columns["DoanhThuTruocGiam"].HeaderText = "Tổng Trước Giảm";
            dgvThongKeTongHop.Columns["DoanhThuTruocGiam"].DefaultCellStyle.Format = "N0";
            dgvThongKeTongHop.Columns["TongTienGiamGia"].HeaderText = "Tiền Giảm Giá";
            dgvThongKeTongHop.Columns["TongTienGiamGia"].DefaultCellStyle.Format = "N0";
            dgvThongKeTongHop.Columns["DoanhThuThucThu"].HeaderText = "Doanh Thu Thực Thu";
            dgvThongKeTongHop.Columns["DoanhThuThucThu"].DefaultCellStyle.Format = "N0";
        }

        private void CapNhatTheKPI()
        {
            int tongHoaDon = 0;
            int tongGhe = 0;
            decimal tongDoanhThu = 0;

            foreach (DataRow row in _dtDoanhThu.Rows)
            {
                tongHoaDon += Convert.ToInt32(row["TongSoHoaDonVe"]);
                tongGhe += Convert.ToInt32(row["TongSoGheBanRa"]);
                tongDoanhThu += Convert.ToDecimal(row["DoanhThuThucThu"]);
            }

            lblKpiTongHoaDon.Text = $"🧾 Tổng số hóa đơn vé: {tongHoaDon:N0}";
            lblKpiTongGhe.Text = $"💺 Tổng số ghế bán ra: {tongGhe:N0}";
            lblKpiDoanhThuThuc.Text = $"💰 TỔNG DOANH THU THỰC THU: {tongDoanhThu:N0} VNĐ";
        }

        private void btnLocDoanhThu_Click(object sender, EventArgs e)
        {
            TaiViewThongKeTongHop(locTheoThang: true);
        }

        private void btnTatCaDoanhThu_Click(object sender, EventArgs e)
        {
            TaiViewThongKeTongHop(locTheoThang: false);
        }

        /// <summary>
        /// Gọi Scalar Function dbo.F_TinhTongGiaTri(@MaDinhDanh) để tính tổng tiền theo Mã KH hoặc Mã Suất Chiếu
        /// </summary>
        private void btnTinhTongUDF_Click(object sender, EventArgs e)
        {
            string maDinhDanh = txtMaDinhDanhUDF.Text.Trim();
            if (string.IsNullOrEmpty(maDinhDanh))
            {
                MessageBox.Show("Vui lòng nhập Mã Khách Hàng (VD: KH001) hoặc Mã Suất Chiếu (VD: SC001)!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDinhDanhUDF.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.F_TinhTongGiaTri(@MaDinhDanh);", conn))
                {
                    cmd.Parameters.Add("@MaDinhDanh", SqlDbType.VarChar, 20).Value = maDinhDanh;

                    conn.Open();
                    object? result = cmd.ExecuteScalar();
                    decimal tongGiaTri = (result != null && result != DBNull.Value)
                        ? Convert.ToDecimal(result)
                        : 0m;

                    lblKetQuaUDF.Text = $"= {tongGiaTri:N0} VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gọi hàm F_TinhTongGiaTri:\n{ex.Message}",
                    "Lỗi thực thi Function", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region TAB 2: STORED PROCEDURE P_DanhSachGiaoDich (PHÂN TRANG & OUTPUT PARAM)

        private void TaiDanhMucKhachHangChoBoLoc()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    string sql = "SELECT MaKH, (MaKH + ' - ' + HoTen) AS HienThi FROM dbo.KhachHang ORDER BY MaKH;";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataRow allRow = dt.NewRow();
                    allRow["MaKH"] = "";
                    allRow["HienThi"] = "--- Tất cả khách hàng ---";
                    dt.Rows.InsertAt(allRow, 0);

                    cboKhachHangGD.DataSource = dt;
                    cboKhachHangGD.DisplayMember = "HienThi";
                    cboKhachHangGD.ValueMember = "MaKH";
                }
            }
            catch
            {
                // Bỏ qua nếu danh mục khách hàng trống
            }
        }

        /// <summary>
        /// Gọi Stored Procedure dbo.P_DanhSachGiaoDich có phân trang và nhận @TotalRecords OUTPUT
        /// </summary>
        private void TaiDanhSachGiaoDichPhanTrang(int pageNumber)
        {
            try
            {
                int pageSize = (int)nudPageSize.Value;
                string? maKH = cboKhachHangGD.SelectedValue?.ToString();

                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.P_DanhSachGiaoDich", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@TuNgay", SqlDbType.DateTime).Value = dtpTuNgay.Value.Date;
                    cmd.Parameters.Add("@DenNgay", SqlDbType.DateTime).Value = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);
                    cmd.Parameters.Add("@MaKH", SqlDbType.VarChar, 15).Value =
                        string.IsNullOrEmpty(maKH) ? (object)DBNull.Value : maKH;
                    cmd.Parameters.Add("@PageNumber", SqlDbType.Int).Value = pageNumber;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

                    SqlParameter paramTotal = new SqlParameter("@TotalRecords", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramTotal);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dtGD = new DataTable();
                        da.Fill(dtGD);
                        dgvGiaoDich.DataSource = dtGD;
                    }

                    _totalRecords = (paramTotal.Value != DBNull.Value) ? Convert.ToInt32(paramTotal.Value) : 0;
                    _totalPages = (int)Math.Ceiling((double)_totalRecords / pageSize);
                    if (_totalPages < 1) _totalPages = 1;
                    _currentPage = pageNumber;

                    lblThongTinTrang.Text = $"Trang {_currentPage} / {_totalPages} (Tổng: {_totalRecords} giao dịch)";
                    btnTrangTruoc.Enabled = (_currentPage > 1);
                    btnTrangSau.Enabled = (_currentPage < _totalPages);
                }

                DinhDangCotGiaoDich();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gọi SP P_DanhSachGiaoDich:\n{ex.Message}",
                    "Lỗi phân trang", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangCotGiaoDich()
        {
            if (dgvGiaoDich.Columns.Count == 0) return;

            dgvGiaoDich.Columns["MaVe"].HeaderText = "Mã Đơn Vé";
            dgvGiaoDich.Columns["NgayLapVe"].HeaderText = "Ngày Lập Vé";
            dgvGiaoDich.Columns["NgayLapVe"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvGiaoDich.Columns["MaKH"].HeaderText = "Mã KH";
            dgvGiaoDich.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
            dgvGiaoDich.Columns["NhanVienBan"].HeaderText = "Nhân Viên Bán";
            dgvGiaoDich.Columns["TenPhim"].HeaderText = "Tên Bộ Phim";
            dgvGiaoDich.Columns["MaSuatChieu"].HeaderText = "Suất Chiếu";
            dgvGiaoDich.Columns["MaPhong"].HeaderText = "Phòng";
            dgvGiaoDich.Columns["ThoiGianBatDau"].HeaderText = "Giờ Chiếu";
            dgvGiaoDich.Columns["ThoiGianBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvGiaoDich.Columns["SoGheDat"].HeaderText = "Số Ghế";
            dgvGiaoDich.Columns["DanhSachGhe"].HeaderText = "Danh Sách Ghế";
            dgvGiaoDich.Columns["TongTienGoc"].HeaderText = "Tổng Tiền Gốc";
            dgvGiaoDich.Columns["TongTienGoc"].DefaultCellStyle.Format = "N0";
            dgvGiaoDich.Columns["PhuongThucThanhToan"].HeaderText = "Thanh Toán";
            dgvGiaoDich.Columns["TrangThaiThanhToan"].HeaderText = "Trạng Thái";
        }

        private void btnTimGiaoDich_Click(object sender, EventArgs e)
        {
            TaiDanhSachGiaoDichPhanTrang(1);
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                TaiDanhSachGiaoDichPhanTrang(_currentPage - 1);
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                TaiDanhSachGiaoDichPhanTrang(_currentPage + 1);
            }
        }

        #endregion

        #region TAB 3: VIEW V_DanhSachChuaXuLy

        /// <summary>
        /// Tải dữ liệu từ View dbo.V_DanhSachChuaXuLy
        /// </summary>
        private void TaiViewDanhSachChuaXuLy()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    string sql = @"
                        SELECT 
                            LoaiCanXuLy,
                            MaDoiTuong,
                            TenKhachHang,
                            TenPhim,
                            MaSuatChieu,
                            MaPhong,
                            ThoiGianBatDau,
                            ThoiDiemPhatSinh,
                            SoGheLienQuan,
                            GiaTriTamTinh,
                            TrangThaiHienTai
                        FROM dbo.V_DanhSachChuaXuLy
                        ORDER BY ThoiGianBatDau ASC;";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        _dtChuaXuLy = new DataTable();
                        da.Fill(_dtChuaXuLy);
                        dgvChuaXuLy.DataSource = _dtChuaXuLy;
                    }
                }

                DinhDangCotChuaXuLy();
                LocDanhSachChuaXuLy();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải View V_DanhSachChuaXuLy:\n{ex.Message}",
                    "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangCotChuaXuLy()
        {
            if (dgvChuaXuLy.Columns.Count == 0) return;

            dgvChuaXuLy.Columns["LoaiCanXuLy"].HeaderText = "Loại Cần Xử Lý";
            dgvChuaXuLy.Columns["MaDoiTuong"].HeaderText = "Mã Đối Tượng";
            dgvChuaXuLy.Columns["TenKhachHang"].HeaderText = "Khách Hàng";
            dgvChuaXuLy.Columns["TenPhim"].HeaderText = "Tên Phim";
            dgvChuaXuLy.Columns["MaSuatChieu"].HeaderText = "Mã Suất";
            dgvChuaXuLy.Columns["MaPhong"].HeaderText = "Phòng";
            dgvChuaXuLy.Columns["ThoiGianBatDau"].HeaderText = "Giờ Chiếu";
            dgvChuaXuLy.Columns["ThoiGianBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvChuaXuLy.Columns["ThoiDiemPhatSinh"].HeaderText = "Thời Điểm Phát Sinh";
            dgvChuaXuLy.Columns["ThoiDiemPhatSinh"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvChuaXuLy.Columns["SoGheLienQuan"].HeaderText = "Số Ghế";
            dgvChuaXuLy.Columns["GiaTriTamTinh"].HeaderText = "Giá Trị Tạm Tính";
            dgvChuaXuLy.Columns["GiaTriTamTinh"].DefaultCellStyle.Format = "N0";
            dgvChuaXuLy.Columns["TrangThaiHienTai"].HeaderText = "Trạng Thái Hiện Tại";
        }

        private void LocDanhSachChuaXuLy()
        {
            if (_dtChuaXuLy == null || _dtChuaXuLy.Rows.Count == 0) return;

            DataView dv = _dtChuaXuLy.DefaultView;
            if (cboLoaiCanXuLy.SelectedIndex <= 0)
            {
                dv.RowFilter = string.Empty;
            }
            else
            {
                string selectedType = cboLoaiCanXuLy.Text.Replace("'", "''");
                dv.RowFilter = $"LoaiCanXuLy = '{selectedType}'";
            }
        }

        private void cboLoaiCanXuLy_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocDanhSachChuaXuLy();
        }

        private void btnTaiLaiChuaXuLy_Click(object sender, EventArgs e)
        {
            TaiViewDanhSachChuaXuLy();
        }

        #endregion
    }
}