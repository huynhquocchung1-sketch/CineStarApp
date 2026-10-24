using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace CineStarApp
{
    /// <summary>
    /// Lớp mô hình hóa dòng chi tiết ghế ngồi hiển thị trên Sub-Form DataGridView
    /// </summary>
    public class ChiTietGheItem
    {
        public string MaGhe { get; set; } = string.Empty;
        public string ViTriGhe { get; set; } = string.Empty;
        public string LoaiGhe { get; set; } = string.Empty;
        public decimal PhuThuGhe { get; set; }
        public decimal DonGiaBan { get; set; }
    }

    /// <summary>
    /// Metadata đính kèm vào thuộc tính Tag của từng Button ghế trên sơ đồ
    /// </summary>
    public class SeatButtonMeta
    {
        public string MaGhe { get; set; } = string.Empty;
        public string ViTriGhe { get; set; } = string.Empty;
        public string LoaiGhe { get; set; } = string.Empty;
        public decimal PhuThuGhe { get; set; }
    }

    public partial class FrmGiaoDichBanVe : Form
    {
        // Giới hạn số ghế tối đa cho phép đặt trong 1 lần giao dịch
        private const int MaxSeatAllowed = 8;

        private readonly BindingList<ChiTietGheItem> _danhSachGheChon = new BindingList<ChiTietGheItem>();
        private DataTable _dtKhuyenMai = new DataTable();

        private decimal _giaVeCoBanHienTai = 0;
        private decimal _tyLeGiamHienTai = 0;
        private decimal _giamToiDaHienTai = 0;
        private bool _isLoadingCombo = false;

        // Các Control bổ sung cho phân hệ Hủy vé & Hoàn tác ghế trống
        private readonly DataGridView dgvVeTheoSuat = new DataGridView();
        private readonly Label lblTieuDeVeTheoSuat = new Label();
        private readonly Button btnHuyVe = new Button();
        private bool _daKhoiTaoUIHuyVe = false;

        public FrmGiaoDichBanVe()
        {
            InitializeComponent();
            dgvChiTietGhe.DataSource = _danhSachGheChon;
        }

        private void FrmGiaoDichBanVe_Load(object sender, EventArgs e)
        {
            // 1. Đảm bảo nối sự kiện an toàn (Tránh lỗi mất kết nối sự kiện trong Designer)
            GanKetSuKienAnToan();

            // 2. Khởi tạo giao diện chia đôi vùng dưới cho bảng Hủy vé và định dạng cột
            KhoiTaoGiaoDienHuyVe();
            CauHinhCotSubGrid();

            // 3. Khởi tạo thông tin mặc định và nạp danh mục từ CSDL
            SinhMaVeTuDong();
            if (cboPhuongThucTT.Items.Count > 0)
            {
                cboPhuongThucTT.SelectedIndex = 0;
            }

            TaiDanhMucKhachHang();
            TaiDanhMucKhuyenMai();
            TaiDanhMucPhimDangChieu();
        }

        /// <summary>
        /// Đảm bảo các sự kiện SelectedIndexChanged và Click luôn được kết nối đúng chuẩn
        /// </summary>
        private void GanKetSuKienAnToan()
        {
            cboPhim.SelectedIndexChanged -= cboPhim_SelectedIndexChanged;
            cboPhim.SelectedIndexChanged += cboPhim_SelectedIndexChanged;

            cboSuatChieu.SelectedIndexChanged -= cboSuatChieu_SelectedIndexChanged;
            cboSuatChieu.SelectedIndexChanged += cboSuatChieu_SelectedIndexChanged;

            cboKhuyenMai.SelectedIndexChanged -= cboKhuyenMai_SelectedIndexChanged;
            cboKhuyenMai.SelectedIndexChanged += cboKhuyenMai_SelectedIndexChanged;
        }

        #region KHỞI TẠO GIAO DIỆN ĐỘNG CHO PHÂN HỆ HỦY VÉ & CO GIÃN TỰ ĐỘNG

        /// <summary>
        /// Chia đôi vùng Sub-Form phía dưới để hiển thị song song:
        /// - Bên trái: Danh sách ghế đang chọn (dgvChiTietGhe)
        /// - Bên phải: Danh sách đơn vé của suất chiếu (dgvVeTheoSuat) + Nút Hủy vé (btnHuyVe)
        /// </summary>
        private void KhoiTaoGiaoDienHuyVe()
        {
            if (_daKhoiTaoUIHuyVe) return;
            _daKhoiTaoUIHuyVe = true;

            Control parentContainer = dgvChiTietGhe.Parent ?? this;

            // 1. Cấu hình Nhãn tiêu đề mục 4 (Danh sách đơn vé của suất chiếu)
            lblTieuDeVeTheoSuat.AutoSize = true;
            lblTieuDeVeTheoSuat.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTieuDeVeTheoSuat.ForeColor = Color.FromArgb(24, 30, 54);
            lblTieuDeVeTheoSuat.Text = "4. DANH SÁCH ĐƠN VÉ CỦA SUẤT CHIẾU (CHỌN ĐỂ HỦY / HOÀN GHẾ)";
            parentContainer.Controls.Add(lblTieuDeVeTheoSuat);

            // 2. Cấu hình DataGridView hiển thị các vé đã đặt theo suất chiếu
            dgvVeTheoSuat.AllowUserToAddRows = false;
            dgvVeTheoSuat.AllowUserToDeleteRows = false;
            dgvVeTheoSuat.ReadOnly = true;
            dgvVeTheoSuat.MultiSelect = false;
            dgvVeTheoSuat.RowHeadersVisible = false;
            dgvVeTheoSuat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVeTheoSuat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVeTheoSuat.BackgroundColor = Color.White;
            dgvVeTheoSuat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVeTheoSuat.CellFormatting += dgvVeTheoSuat_CellFormatting;
            parentContainer.Controls.Add(dgvVeTheoSuat);

            // 3. Cấu hình nút Hủy Vé Đã Chọn nằm ngay bên trái nút Lưu Giao Dịch Vé
            Control buttonParent = btnLuuGiaoDich.Parent ?? this;
            btnHuyVe.Text = "🗑️ HỦY VÉ ĐÃ CHỌN";
            btnHuyVe.BackColor = Color.FromArgb(220, 53, 69);
            btnHuyVe.ForeColor = Color.White;
            btnHuyVe.FlatStyle = FlatStyle.Flat;
            btnHuyVe.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnHuyVe.Size = new Size(165, btnLuuGiaoDich.Height);
            btnHuyVe.Cursor = Cursors.Hand;
            btnHuyVe.Click += btnHuyVe_Click;
            buttonParent.Controls.Add(btnHuyVe);
            btnHuyVe.BringToFront();

            // 4. Canh chỉnh tọa độ ban đầu và đăng ký sự kiện Resize để tự động chia đôi màn hình
            CapNhatBoCucSubForm();
            parentContainer.Resize += (s, args) => CapNhatBoCucSubForm();
        }

        /// <summary>
        /// Tự động tính toán lại kích thước 2 bảng dưới và vị trí nút Hủy vé khi phóng to/thu nhỏ Form
        /// </summary>
        private void CapNhatBoCucSubForm()
        {
            Control parentContainer = dgvChiTietGhe.Parent ?? this;
            int padding = 12;
            int totalWidth = parentContainer.ClientSize.Width;
            int halfWidth = (totalWidth - (padding * 3)) / 2;
            if (halfWidth < 200) halfWidth = 200;

            int topY = dgvChiTietGhe.Top;
            int gridHeight = dgvChiTietGhe.Height;

            // Cố định bảng ghế đang chọn ở nửa bên trái
            dgvChiTietGhe.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvChiTietGhe.Width = halfWidth;

            // Đặt tiêu đề và bảng danh sách vé đã đặt ở nửa bên phải
            int rightStartX = dgvChiTietGhe.Left + halfWidth + padding;
            lblTieuDeVeTheoSuat.Location = new Point(rightStartX, Math.Max(4, topY - 22));

            dgvVeTheoSuat.Location = new Point(rightStartX, topY);
            dgvVeTheoSuat.Size = new Size(halfWidth, gridHeight);
            dgvVeTheoSuat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Đặt nút Hủy vé luôn nằm sát bên trái nút Lưu Giao Dịch Vé
            btnHuyVe.Location = new Point(btnLuuGiaoDich.Left - btnHuyVe.Width - 10, btnLuuGiaoDich.Top);
            btnHuyVe.Anchor = btnLuuGiaoDich.Anchor;
        }

        #endregion

        #region CẤU HÌNH LƯỚI & TẢI DANH MỤC CƠ BẢN

        private void CauHinhCotSubGrid()
        {
            if (dgvChiTietGhe.Columns.Count == 0) return;

            dgvChiTietGhe.Columns["MaGhe"].HeaderText = "Mã Ghế";
            dgvChiTietGhe.Columns["ViTriGhe"].HeaderText = "Vị Trí Ghế";
            dgvChiTietGhe.Columns["LoaiGhe"].HeaderText = "Hạng Ghế";
            dgvChiTietGhe.Columns["PhuThuGhe"].HeaderText = "Phụ Thu (VNĐ)";
            dgvChiTietGhe.Columns["PhuThuGhe"].DefaultCellStyle.Format = "N0";
            dgvChiTietGhe.Columns["DonGiaBan"].HeaderText = "Đơn Giá Bán (VNĐ)";
            dgvChiTietGhe.Columns["DonGiaBan"].DefaultCellStyle.Format = "N0";
        }

        private void SinhMaVeTuDong()
        {
            txtMaVe.Text = "VE" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// Tải danh sách Khách hàng (kèm mục Khách vãng lai)
        /// </summary>
        private void TaiDanhMucKhachHang()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    string sql = @"
                        SELECT LTRIM(RTRIM(MaKH)) AS MaKH, 
                               (HoTen + ' - ' + LTRIM(RTRIM(SoDienThoai))) AS HienThi 
                        FROM dbo.KhachHang 
                        ORDER BY HoTen;";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataRow guestRow = dt.NewRow();
                    guestRow["MaKH"] = "";
                    guestRow["HienThi"] = "--- Khách vãng lai (Không tích điểm) ---";
                    dt.Rows.InsertAt(guestRow, 0);

                    cboKhachHang.DataSource = dt;
                    cboKhachHang.DisplayMember = "HienThi";
                    cboKhachHang.ValueMember = "MaKH";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải danh sách Khuyến mãi còn hiệu lực và còn lượt sử dụng
        /// </summary>
        private void TaiDanhMucKhuyenMai()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    string sql = @"
                        SELECT LTRIM(RTRIM(MaKM)) AS MaKM, 
                               (TenChuongTrinh + ' (-' + CAST(TyLeGiam AS VARCHAR(10)) + '%)') AS HienThi,
                               TyLeGiam, 
                               GiamToiDa
                        FROM dbo.KhuyenMai
                        WHERE GETDATE() BETWEEN NgayBatDau AND NgayKetThuc
                          AND SoLuongDaDung < SoLuongPhatHanh;";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    _dtKhuyenMai = new DataTable();
                    da.Fill(_dtKhuyenMai);

                    DataRow noneRow = _dtKhuyenMai.NewRow();
                    noneRow["MaKM"] = "";
                    noneRow["HienThi"] = "--- Không áp dụng khuyến mãi ---";
                    noneRow["TyLeGiam"] = 0m;
                    noneRow["GiamToiDa"] = 0m;
                    _dtKhuyenMai.Rows.InsertAt(noneRow, 0);

                    cboKhuyenMai.DataSource = _dtKhuyenMai;
                    cboKhuyenMai.DisplayMember = "HienThi";
                    cboKhuyenMai.ValueMember = "MaKM";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục khuyến mãi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải danh sách Phim đang chiếu (Có fallback tải toàn bộ phim nếu chưa set trạng thái Đang chiếu)
        /// </summary>
        private void TaiDanhMucPhimDangChieu()
        {
            try
            {
                _isLoadingCombo = true;
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    string sql = @"
                        SELECT LTRIM(RTRIM(MaPhim)) AS MaPhim, 
                               (TenPhim + ' [' + ISNULL(DoTuoiQuyDinh, 'P') + ']') AS TenHienThi 
                        FROM dbo.Phim 
                        WHERE TrangThaiPhim = N'Đang chiếu'
                        ORDER BY TenPhim;";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Fallback: Nếu không có phim nào đang ở trạng thái 'Đang chiếu', nạp toàn bộ bảng Phim
                    if (dt.Rows.Count == 0)
                    {
                        string sqlAll = @"
                            SELECT LTRIM(RTRIM(MaPhim)) AS MaPhim, 
                                   (TenPhim + ' [' + ISNULL(DoTuoiQuyDinh, 'P') + ']') AS TenHienThi 
                            FROM dbo.Phim 
                            ORDER BY TenPhim;";
                        da = new SqlDataAdapter(sqlAll, conn);
                        da.Fill(dt);
                    }

                    cboPhim.DataSource = dt;
                    cboPhim.DisplayMember = "TenHienThi";
                    cboPhim.ValueMember = "MaPhim";
                }
                _isLoadingCombo = false;

                if (cboPhim.Items.Count > 0)
                {
                    cboPhim.SelectedIndex = 0;
                    cboPhim_SelectedIndexChanged(cboPhim, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                _isLoadingCombo = false;
                MessageBox.Show($"Lỗi tải danh sách phim: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Khi chọn Phim -> Tải các suất chiếu đang "Mở bán" của bộ phim đó
        /// </summary>
        private void cboPhim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingCombo) return;

            string maPhim = string.Empty;
            if (cboPhim.SelectedItem is DataRowView rowPhim)
            {
                maPhim = (rowPhim["MaPhim"]?.ToString() ?? string.Empty).Trim();
            }
            else if (cboPhim.SelectedValue != null)
            {
                maPhim = (cboPhim.SelectedValue.ToString() ?? string.Empty).Trim();
            }

            if (string.IsNullOrEmpty(maPhim)) return;

            try
            {
                _isLoadingCombo = true;
                DataTable dtSuat = new DataTable();

                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    // 1. Ưu tiên lấy các suất chiếu Mở bán trong tương lai (ThoiGianBatDau > GETDATE())
                    string sqlUpcoming = @"
                        SELECT LTRIM(RTRIM(MaSuatChieu)) AS MaSuatChieu, 
                               (LTRIM(RTRIM(MaSuatChieu)) + ' | Phòng ' + LTRIM(RTRIM(MaPhong)) + ' | ' + FORMAT(ThoiGianBatDau, 'dd/MM/yyyy HH:mm') + ' | ' + LTRIM(RTRIM(DinhDangChieu))) AS ThongTinSuat,
                               LTRIM(RTRIM(MaPhong)) AS MaPhong,
                               GiaVeCoBan
                        FROM dbo.SuatChieu
                        WHERE LTRIM(RTRIM(MaPhim)) = LTRIM(RTRIM(@MaPhim))
                          AND TrangThaiSuat = N'Mở bán'
                          AND ThoiGianBatDau > GETDATE()
                        ORDER BY ThoiGianBatDau ASC;";

                    using (SqlCommand cmd = new SqlCommand(sqlUpcoming, conn))
                    {
                        cmd.Parameters.Add("@MaPhim", SqlDbType.VarChar, 15).Value = maPhim;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtSuat);
                        }
                    }

                    // 2. Fallback an toàn khi demo: Nếu dữ liệu mẫu trong DB có ngày chiếu đã qua (< GETDATE()),
                    // vẫn nạp tất cả các suất chiếu không bị hủy của phim đó để sinh viên luôn demo chọn ghế được!
                    if (dtSuat.Rows.Count == 0)
                    {
                        string sqlAllActive = @"
                            SELECT LTRIM(RTRIM(MaSuatChieu)) AS MaSuatChieu, 
                                   (LTRIM(RTRIM(MaSuatChieu)) + ' | Phòng ' + LTRIM(RTRIM(MaPhong)) + ' | ' + FORMAT(ThoiGianBatDau, 'dd/MM/yyyy HH:mm') + ' | ' + LTRIM(RTRIM(DinhDangChieu))) AS ThongTinSuat,
                                   LTRIM(RTRIM(MaPhong)) AS MaPhong,
                                   GiaVeCoBan
                            FROM dbo.SuatChieu
                            WHERE LTRIM(RTRIM(MaPhim)) = LTRIM(RTRIM(@MaPhim))
                              AND TrangThaiSuat <> N'Đã hủy'
                            ORDER BY ThoiGianBatDau DESC;";

                        using (SqlCommand cmdFallback = new SqlCommand(sqlAllActive, conn))
                        {
                            cmdFallback.Parameters.Add("@MaPhim", SqlDbType.VarChar, 15).Value = maPhim;
                            using (SqlDataAdapter daFallback = new SqlDataAdapter(cmdFallback))
                            {
                                daFallback.Fill(dtSuat);
                            }
                        }
                    }
                }

                cboSuatChieu.DataSource = dtSuat;
                cboSuatChieu.DisplayMember = "ThongTinSuat";
                cboSuatChieu.ValueMember = "MaSuatChieu";

                _isLoadingCombo = false;

                if (dtSuat.Rows.Count > 0)
                {
                    // Ưu tiên chọn tự động suất chiếu thuộc phòng RAP02 (phòng đã có đủ 40 ghế) nếu có
                    int indexRap02 = -1;
                    for (int i = 0; i < dtSuat.Rows.Count; i++)
                    {
                        string phong = (dtSuat.Rows[i]["MaPhong"]?.ToString() ?? string.Empty).Trim();
                        if (string.Equals(phong, "RAP02", StringComparison.OrdinalIgnoreCase))
                        {
                            indexRap02 = i;
                            break;
                        }
                    }

                    cboSuatChieu.SelectedIndex = indexRap02 >= 0 ? indexRap02 : 0;
                    cboSuatChieu_SelectedIndexChanged(cboSuatChieu, EventArgs.Empty);
                }
                else
                {
                    lblPhong.Text = "---";
                    lblGia.Text = "0 VNĐ";
                    _giaVeCoBanHienTai = 0;
                    flpSoDoGhe.Controls.Clear();
                    _danhSachGheChon.Clear();
                    dgvVeTheoSuat.DataSource = null;
                    CapNhatTongTienRealtime();
                }
            }
            catch (Exception ex)
            {
                _isLoadingCombo = false;
                MessageBox.Show($"Lỗi tải suất chiếu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Khi chọn Suất chiếu -> Lấy thông tin Phòng (đã cắt khoảng trắng CHAR), Giá cơ bản,
        /// vẽ lại Sơ đồ 40 ghế và tải Danh sách đơn vé của suất chiếu đó
        /// </summary>
        private void cboSuatChieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingCombo) return;

            DataRowView? rowView = cboSuatChieu.SelectedItem as DataRowView;
            if (rowView == null && cboSuatChieu.Items.Count > 0 && cboSuatChieu.SelectedIndex >= 0)
            {
                rowView = cboSuatChieu.Items[cboSuatChieu.SelectedIndex] as DataRowView;
            }

            if (rowView == null)
            {
                lblPhong.Text = "---";
                lblGia.Text = "0 VNĐ";
                _giaVeCoBanHienTai = 0;
                flpSoDoGhe.Controls.Clear();
                _danhSachGheChon.Clear();
                dgvVeTheoSuat.DataSource = null;
                CapNhatTongTienRealtime();
                return;
            }

            // Dùng .Trim() để loại bỏ hoàn toàn khoảng trắng padding của kiểu CHAR trong SQL Server
            string maSuatChieu = (rowView["MaSuatChieu"]?.ToString() ?? string.Empty).Trim();
            string maPhong = (rowView["MaPhong"]?.ToString() ?? string.Empty).Trim();
            _giaVeCoBanHienTai = rowView["GiaVeCoBan"] != DBNull.Value
                ? Convert.ToDecimal(rowView["GiaVeCoBan"])
                : 0m;

            lblPhong.Text = maPhong;
            lblGia.Text = $"{_giaVeCoBanHienTai:N0} VNĐ";

            // Làm mới danh sách ghế đang chọn ở Sub-Form
            _danhSachGheChon.Clear();
            CapNhatTongTienRealtime();

            // 1. Vẽ sơ đồ ghế theo phòng và đánh dấu các ghế đã bán trong suất chiếu
            VeSoDoGheTheoSuatChieu(maSuatChieu, maPhong);

            // 2. Tải danh sách các đơn vé thuộc suất chiếu này để phục vụ tra cứu / hủy vé
            TaiDanhSachVeTheoSuat(maSuatChieu);
        }

        #endregion

        #region VẼ SƠ ĐỒ GHẾ TRỰC QUAN & TÍNH TIỀN REALTIME

        /// <summary>
        /// Truy vấn danh sách 40 ghế của Phòng chiếu và dựng nút ghế trực quan lên flpSoDoGhe
        /// </summary>
        private void VeSoDoGheTheoSuatChieu(string maSuatChieu, string maPhong)
        {
            maSuatChieu = (maSuatChieu ?? string.Empty).Trim();
            maPhong = (maPhong ?? string.Empty).Trim();

            flpSoDoGhe.SuspendLayout();
            flpSoDoGhe.Controls.Clear();

            // Đảm bảo cấu hình dàn trang chuẩn cho FlowLayoutPanel
            flpSoDoGhe.FlowDirection = FlowDirection.LeftToRight;
            flpSoDoGhe.WrapContents = true;
            flpSoDoGhe.AutoScroll = true;

            if (string.IsNullOrEmpty(maPhong))
            {
                flpSoDoGhe.ResumeLayout();
                return;
            }

            int demSoGhe = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    // Dùng LTRIM(RTRIM(...)) trong SQL để xử lý triệt để cả kiểu CHAR lẫn VARCHAR
                    string sql = @"
                        SELECT 
                            LTRIM(RTRIM(g.MaGhe)) AS MaGhe,
                            LTRIM(RTRIM(g.HangGhe)) AS HangGhe,
                            g.SoGhe,
                            LTRIM(RTRIM(g.LoaiGhe)) AS LoaiGhe,
                            ISNULL(g.PhuThuGhe, 0) AS PhuThuGhe,
                            ISNULL(g.TrangThaiBaoTri, 0) AS TrangThaiBaoTri,
                            CASE 
                                WHEN EXISTS (
                                    SELECT 1 
                                    FROM dbo.ChiTietVe ct
                                    INNER JOIN dbo.VeXemPhim v ON LTRIM(RTRIM(ct.MaVe)) = LTRIM(RTRIM(v.MaVe))
                                    WHERE LTRIM(RTRIM(v.MaSuatChieu)) = LTRIM(RTRIM(@MaSuatChieu))
                                      AND LTRIM(RTRIM(ct.MaGhe)) = LTRIM(RTRIM(g.MaGhe))
                                      AND v.TrangThaiThanhToan <> N'Đã hủy'
                                ) THEN 1 
                                ELSE 0 
                            END AS DaBan
                        FROM dbo.GheNgoi g
                        WHERE LTRIM(RTRIM(g.MaPhong)) = LTRIM(RTRIM(@MaPhong))
                        ORDER BY LTRIM(RTRIM(g.HangGhe)) ASC, g.SoGhe ASC;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaSuatChieu", SqlDbType.VarChar, 15).Value = maSuatChieu;
                        cmd.Parameters.Add("@MaPhong", SqlDbType.VarChar, 15).Value = maPhong;

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            string hangGheTruocDo = string.Empty;

                            while (reader.Read())
                            {
                                demSoGhe++;

                                string maGhe = (reader["MaGhe"]?.ToString() ?? string.Empty).Trim();
                                string hangGhe = (reader["HangGhe"]?.ToString() ?? string.Empty).Trim();
                                int soGhe = Convert.ToInt32(reader["SoGhe"]);
                                string loaiGhe = (reader["LoaiGhe"]?.ToString() ?? string.Empty).Trim();
                                decimal phuThu = Convert.ToDecimal(reader["PhuThuGhe"]);
                                bool baoTri = Convert.ToBoolean(reader["TrangThaiBaoTri"]);
                                bool daBan = Convert.ToInt32(reader["DaBan"]) == 1;

                                // Ngắt dòng tự động khi chuyển sang hàng ghế mới (A -> B -> C -> D)
                                if (!string.IsNullOrEmpty(hangGheTruocDo) &&
                                    !string.Equals(hangGheTruocDo, hangGhe, StringComparison.OrdinalIgnoreCase) &&
                                    flpSoDoGhe.Controls.Count > 0)
                                {
                                    Control lastSeat = flpSoDoGhe.Controls[flpSoDoGhe.Controls.Count - 1];
                                    flpSoDoGhe.SetFlowBreak(lastSeat, true);
                                }
                                hangGheTruocDo = hangGhe;

                                bool isVip = string.Equals(loaiGhe, "VIP", StringComparison.OrdinalIgnoreCase);

                                Button btnSeat = new Button
                                {
                                    Width = 78,
                                    Height = 50,
                                    Margin = new Padding(5),
                                    FlatStyle = FlatStyle.Flat,
                                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                                    Text = $"{hangGhe}{soGhe:00}\n({loaiGhe})",
                                    Tag = new SeatButtonMeta
                                    {
                                        MaGhe = maGhe,
                                        ViTriGhe = $"Hàng {hangGhe} - Số {soGhe:00}",
                                        LoaiGhe = loaiGhe,
                                        PhuThuGhe = phuThu
                                    }
                                };

                                if (daBan || baoTri)
                                {
                                    // Ghế đã bán hoặc đang bảo trì -> Tô đỏ và khóa
                                    btnSeat.BackColor = Color.Crimson;
                                    btnSeat.ForeColor = Color.White;
                                    btnSeat.FlatAppearance.BorderColor = Color.DarkRed;
                                    btnSeat.FlatAppearance.BorderSize = 1;
                                    btnSeat.Enabled = false;
                                }
                                else
                                {
                                    // Ghế còn trống -> Phân biệt màu VIP (Vàng nhạt) và Thường (Trắng)
                                    btnSeat.BackColor = isVip ? Color.FromArgb(255, 248, 220) : Color.White;
                                    btnSeat.ForeColor = Color.FromArgb(24, 30, 54);
                                    btnSeat.FlatAppearance.BorderColor = isVip ? Color.Goldenrod : Color.Silver;
                                    btnSeat.FlatAppearance.BorderSize = 2;
                                    btnSeat.Cursor = Cursors.Hand;
                                    btnSeat.Click += SeatButton_Click;
                                }

                                flpSoDoGhe.Controls.Add(btnSeat);
                            }
                        }
                    }
                }

                // Hiển thị thông báo hướng dẫn nếu phòng đang chọn chưa được cấu hình ghế trong dbo.GheNgoi
                if (demSoGhe == 0)
                {
                    Label lblEmpty = new Label
                    {
                        AutoSize = false,
                        Width = Math.Max(450, flpSoDoGhe.ClientSize.Width - 30),
                        Height = 80,
                        Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                        ForeColor = Color.Firebrick,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = $"⚠️ Phòng chiếu [{maPhong}] của suất [{maSuatChieu}] hiện chưa có dữ liệu ghế trong bảng dbo.GheNgoi!\n" +
                               "Vui lòng chọn suất chiếu thuộc phòng RAP02 hoặc thêm ghế cho phòng này."
                    };
                    flpSoDoGhe.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi dựng sơ đồ ghế cho phòng [{maPhong}]:\n{ex.Message}",
                    "Lỗi tải sơ đồ ghế", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                flpSoDoGhe.ResumeLayout(true);
                flpSoDoGhe.PerformLayout();
            }
        }

        /// <summary>
        /// Sự kiện Click vào 1 ghế trên sơ đồ: Đổi màu nút, Thêm/Xóa dòng trên BindingList Sub-Form
        /// </summary>
        private void SeatButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btnSeat || btnSeat.Tag is not SeatButtonMeta meta) return;

            ChiTietGheItem? existingItem = _danhSachGheChon.FirstOrDefault(x =>
                string.Equals(x.MaGhe, meta.MaGhe, StringComparison.OrdinalIgnoreCase));

            if (existingItem == null)
            {
                // Kiểm tra giới hạn tối đa 8 ghế / 1 lần giao dịch
                if (_danhSachGheChon.Count >= MaxSeatAllowed)
                {
                    MessageBox.Show(
                        $"Mỗi giao dịch đặt vé chỉ được phép chọn tối đa {MaxSeatAllowed} ghế để đảm bảo công bằng cho khách hàng khác!",
                        "Giới hạn số ghế", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chưa chọn -> Tích chọn (Đổi sang màu Xanh lá & thêm vào Sub-Form)
                btnSeat.BackColor = Color.MediumSeaGreen;
                btnSeat.ForeColor = Color.White;

                _danhSachGheChon.Add(new ChiTietGheItem
                {
                    MaGhe = meta.MaGhe,
                    ViTriGhe = meta.ViTriGhe,
                    LoaiGhe = meta.LoaiGhe,
                    PhuThuGhe = meta.PhuThuGhe,
                    DonGiaBan = _giaVeCoBanHienTai + meta.PhuThuGhe
                });
            }
            else
            {
                // Đang chọn -> Bỏ chọn (Trả về màu ban đầu & xóa khỏi Sub-Form)
                bool isVip = string.Equals(meta.LoaiGhe, "VIP", StringComparison.OrdinalIgnoreCase);
                btnSeat.BackColor = isVip ? Color.FromArgb(255, 248, 220) : Color.White;
                btnSeat.ForeColor = Color.FromArgb(24, 30, 54);

                _danhSachGheChon.Remove(existingItem);
            }

            CapNhatTongTienRealtime();
        }

        /// <summary>
        /// Khi đổi Mã khuyến mãi -> Cập nhật lại tỷ lệ giảm và mức giảm tối đa
        /// </summary>
        private void cboKhuyenMai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhuyenMai.SelectedItem is DataRowView rowView)
            {
                _tyLeGiamHienTai = rowView["TyLeGiam"] != DBNull.Value
                    ? Convert.ToDecimal(rowView["TyLeGiam"])
                    : 0m;
                _giamToiDaHienTai = rowView["GiamToiDa"] != DBNull.Value
                    ? Convert.ToDecimal(rowView["GiamToiDa"])
                    : 0m;
                CapNhatTongTienRealtime();
            }
        }

        /// <summary>
        /// Tính toán Tổng gốc, Tiền giảm khuyến mãi và Thành tiền theo thời gian thực
        /// </summary>
        private void CapNhatTongTienRealtime()
        {
            decimal tongTienGoc = _danhSachGheChon.Sum(item => item.DonGiaBan);
            decimal tienGiam = 0;

            if (_tyLeGiamHienTai > 0)
            {
                tienGiam = tongTienGoc * (_tyLeGiamHienTai / 100m);
                if (_giamToiDaHienTai > 0 && tienGiam > _giamToiDaHienTai)
                {
                    tienGiam = _giamToiDaHienTai;
                }
            }

            decimal thanhTien = tongTienGoc - tienGiam;
            if (thanhTien < 0) thanhTien = 0;

            lblTongTienGoc.Text = $"Tổng gốc: {tongTienGoc:N0} VNĐ";
            lblTienGiamGia.Text = $"Giảm giá: {tienGiam:N0} VNĐ";
            lblThanhTien.Text = $"THÀNH TIỀN: {thanhTien:N0} VNĐ";
        }

        #endregion

        #region LƯU GIAO DỊCH BÁN VÉ MỚI (SQL TRANSACTION - SERIALIZABLE)

        /// <summary>
        /// Lưu đồng bộ Giao dịch bảng Cha (VeXemPhim) và bảng Con (ChiTietVe) trong 1 khối SqlTransaction
        /// </summary>
        private void btnLuuGiaoDich_Click(object sender, EventArgs e)
        {
            if (cboSuatChieu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn một suất chiếu hợp lệ!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_danhSachGheChon.Count == 0)
            {
                MessageBox.Show("Vui lòng tích chọn ít nhất 1 ghế ngồi trên sơ đồ trước khi lưu!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SinhMaVeTuDong();
            string maVe = txtMaVe.Text.Trim();
            string maSuatChieu = (cboSuatChieu.SelectedValue.ToString() ?? string.Empty).Trim();
            string maKH = (cboKhachHang.SelectedValue?.ToString() ?? string.Empty).Trim();
            string maKM = (cboKhuyenMai.SelectedValue?.ToString() ?? string.Empty).Trim();
            string phuongThucTT = cboPhuongThucTT.Text.Trim();

            using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
            {
                conn.Open();

                // Sử dụng mức cô lập Serializable để khóa phạm vi đọc/ghi, chống đặt trùng ghế đồng thời
                using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        // BƯỚC 1: INSERT chứng từ gốc vào bảng Cha (dbo.VeXemPhim)
                        string sqlInsertMaster = @"
                            INSERT INTO dbo.VeXemPhim 
                                (MaVe, MaKH, MaNV, MaSuatChieu, MaKM, NgayLapVe, PhuongThucThanhToan, TrangThaiThanhToan)
                            VALUES 
                                (@MaVe, @MaKH, @MaNV, @MaSuatChieu, @MaKM, GETDATE(), @PhuongThucTT, N'Đã thanh toán');";

                        using (SqlCommand cmdMaster = new SqlCommand(sqlInsertMaster, conn, trans))
                        {
                            cmdMaster.Parameters.Add("@MaVe", SqlDbType.VarChar, 20).Value = maVe;
                            cmdMaster.Parameters.Add("@MaKH", SqlDbType.VarChar, 15).Value =
                                string.IsNullOrEmpty(maKH) ? DBNull.Value : maKH;
                            cmdMaster.Parameters.Add("@MaNV", SqlDbType.VarChar, 15).Value =
                                string.IsNullOrEmpty(AppSession.MaNV) ? "NV001" : AppSession.MaNV.Trim();
                            cmdMaster.Parameters.Add("@MaSuatChieu", SqlDbType.VarChar, 15).Value = maSuatChieu;
                            cmdMaster.Parameters.Add("@MaKM", SqlDbType.VarChar, 15).Value =
                                string.IsNullOrEmpty(maKM) ? DBNull.Value : maKM;
                            cmdMaster.Parameters.Add("@PhuongThucTT", SqlDbType.NVarChar, 50).Value = phuongThucTT;

                            cmdMaster.ExecuteNonQuery();
                        }

                        // BƯỚC 2: INSERT từng ghế vào bảng Con (dbo.ChiTietVe) - Tối ưu định nghĩa Parameter ngoài vòng lặp
                        string sqlInsertDetail = @"
                            INSERT INTO dbo.ChiTietVe (MaVe, MaGhe, DonGiaBan)
                            VALUES (@MaVe, @MaGhe, @DonGiaBan);";

                        using (SqlCommand cmdDetail = new SqlCommand(sqlInsertDetail, conn, trans))
                        {
                            cmdDetail.Parameters.Add("@MaVe", SqlDbType.VarChar, 20).Value = maVe;
                            SqlParameter pMaGhe = cmdDetail.Parameters.Add("@MaGhe", SqlDbType.VarChar, 15);
                            SqlParameter pDonGia = cmdDetail.Parameters.Add("@DonGiaBan", SqlDbType.Decimal);
                            pDonGia.Precision = 18;
                            pDonGia.Scale = 2;

                            foreach (ChiTietGheItem item in _danhSachGheChon)
                            {
                                pMaGhe.Value = item.MaGhe.Trim();
                                pDonGia.Value = item.DonGiaBan;
                                cmdDetail.ExecuteNonQuery();
                            }
                        }

                        // BƯỚC 3: Nếu có dùng Mã khuyến mãi, tăng SoLuongDaDung và kiểm tra tồn
                        if (!string.IsNullOrEmpty(maKM))
                        {
                            string sqlUpdateKM = @"
                                UPDATE dbo.KhuyenMai
                                SET SoLuongDaDung = SoLuongDaDung + 1
                                WHERE LTRIM(RTRIM(MaKM)) = LTRIM(RTRIM(@MaKM)) 
                                  AND SoLuongDaDung < SoLuongPhatHanh;";

                            using (SqlCommand cmdKM = new SqlCommand(sqlUpdateKM, conn, trans))
                            {
                                cmdKM.Parameters.Add("@MaKM", SqlDbType.VarChar, 15).Value = maKM;
                                int rowsAffected = cmdKM.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    throw new InvalidOperationException("Mã khuyến mãi đã hết lượt sử dụng!");
                                }
                            }
                        }

                        // BƯỚC 4: Cộng điểm tích lũy cho Khách hàng thành viên (10 điểm / ghế)
                        if (!string.IsNullOrEmpty(maKH))
                        {
                            string sqlUpdateKH = @"
                                UPDATE dbo.KhachHang
                                SET DiemTichLuy = ISNULL(DiemTichLuy, 0) + @DiemCong
                                WHERE LTRIM(RTRIM(MaKH)) = LTRIM(RTRIM(@MaKH));";

                            using (SqlCommand cmdKH = new SqlCommand(sqlUpdateKH, conn, trans))
                            {
                                cmdKH.Parameters.Add("@DiemCong", SqlDbType.Int).Value = _danhSachGheChon.Count * 10;
                                cmdKH.Parameters.Add("@MaKH", SqlDbType.VarChar, 15).Value = maKH;
                                cmdKH.ExecuteNonQuery();
                            }
                        }

                        // BƯỚC 5: Hoàn tất Transaction
                        trans.Commit();

                        MessageBox.Show(
                            $"Thanh toán và xuất vé thành công!\n" +
                            $"- Mã đơn vé: {maVe}\n" +
                            $"- Số lượng ghế: {_danhSachGheChon.Count}\n" +
                            $"- {lblThanhTien.Text}",
                            "Giao dịch thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Gọi xem trước bản in hóa đơn vé xem phim (GDI+)
                        InVeXemPhim(maVe, cboPhim.Text, cboSuatChieu.Text, lblPhong.Text, cboPhuongThucTT.Text);

                        // Tải lại sơ đồ ghế và danh sách vé để cập nhật trạng thái mới nhất
                        SinhMaVeTuDong();
                        TaiDanhMucKhuyenMai();
                        cboSuatChieu_SelectedIndexChanged(cboSuatChieu, EventArgs.Empty);
                    }
                    catch (SqlException sqlEx)
                    {
                        if (trans.Connection != null)
                        {
                            trans.Rollback();
                        }

                        MessageBox.Show(
                            $"Giao dịch đã được Rollback an toàn do vi phạm ràng buộc Trigger/CSDL:\n{sqlEx.Message}",
                            "Từ chối giao dịch (SQL Rollback)", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        cboSuatChieu_SelectedIndexChanged(cboSuatChieu, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                        if (trans.Connection != null)
                        {
                            trans.Rollback();
                        }

                        MessageBox.Show(
                            $"Đã hủy giao dịch (Rollback) để bảo toàn dữ liệu.\nChi tiết: {ex.Message}",
                            "Lỗi xử lý giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuyLamMoi_Click(object sender, EventArgs e)
        {
            SinhMaVeTuDong();
            if (cboKhachHang.Items.Count > 0) cboKhachHang.SelectedIndex = 0;
            if (cboKhuyenMai.Items.Count > 0) cboKhuyenMai.SelectedIndex = 0;
            if (cboPhuongThucTT.Items.Count > 0) cboPhuongThucTT.SelectedIndex = 0;
            cboSuatChieu_SelectedIndexChanged(cboSuatChieu, EventArgs.Empty);
        }

        #endregion

        #region NGHIỆP VỤ TẢI DANH SÁCH VÉ THEO SUẤT & HỦY VÉ HOÀN TÁC GHẾ TRỐNG

        /// <summary>
        /// Tải danh sách các đơn vé đã đặt thuộc suất chiếu đang chọn lên lưới bên phải (dgvVeTheoSuat)
        /// </summary>
        private void TaiDanhSachVeTheoSuat(string maSuatChieu)
        {
            maSuatChieu = (maSuatChieu ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(maSuatChieu))
            {
                dgvVeTheoSuat.DataSource = null;
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
                {
                    string sql = @"
                        SELECT 
                            LTRIM(RTRIM(v.MaVe)) AS MaVe,
                            ISNULL(kh.HoTen, N'Khách vãng lai') AS KhachHang,
                            ISNULL(STRING_AGG(LTRIM(RTRIM(g.HangGhe)) + RIGHT('00' + CAST(g.SoGhe AS VARCHAR(5)), 2), ', '), N'---') AS GheDaDat,
                            COUNT(ct.MaGhe) AS SoGhe,
                            ISNULL(SUM(ct.DonGiaBan), 0) AS TongTienGoc,
                            v.TrangThaiThanhToan,
                            v.NgayLapVe
                        FROM dbo.VeXemPhim v
                        LEFT JOIN dbo.KhachHang kh ON LTRIM(RTRIM(v.MaKH)) = LTRIM(RTRIM(kh.MaKH))
                        LEFT JOIN dbo.ChiTietVe ct ON LTRIM(RTRIM(v.MaVe)) = LTRIM(RTRIM(ct.MaVe))
                        LEFT JOIN dbo.GheNgoi g ON LTRIM(RTRIM(ct.MaGhe)) = LTRIM(RTRIM(g.MaGhe))
                        WHERE LTRIM(RTRIM(v.MaSuatChieu)) = LTRIM(RTRIM(@MaSuatChieu))
                        GROUP BY LTRIM(RTRIM(v.MaVe)), kh.HoTen, v.TrangThaiThanhToan, v.NgayLapVe
                        ORDER BY v.NgayLapVe DESC;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaSuatChieu", SqlDbType.VarChar, 15).Value = maSuatChieu;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dtVe = new DataTable();
                        da.Fill(dtVe);

                        dgvVeTheoSuat.DataSource = dtVe;
                    }
                }

                if (dgvVeTheoSuat.Columns.Count > 0)
                {
                    dgvVeTheoSuat.Columns["MaVe"].HeaderText = "Mã Vé";
                    dgvVeTheoSuat.Columns["KhachHang"].HeaderText = "Khách Hàng";
                    dgvVeTheoSuat.Columns["GheDaDat"].HeaderText = "Ghế Đã Đặt";
                    dgvVeTheoSuat.Columns["SoGhe"].HeaderText = "SL";
                    dgvVeTheoSuat.Columns["SoGhe"].FillWeight = 45;
                    dgvVeTheoSuat.Columns["TongTienGoc"].HeaderText = "Tổng Gốc";
                    dgvVeTheoSuat.Columns["TongTienGoc"].DefaultCellStyle.Format = "N0";
                    dgvVeTheoSuat.Columns["TrangThaiThanhToan"].HeaderText = "Trạng Thái";
                    dgvVeTheoSuat.Columns["NgayLapVe"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách vé theo suất chiếu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tô màu trực quan cho các dòng đơn vé trong dgvVeTheoSuat (Làm mờ các vé Đã hủy)
        /// </summary>
        private void dgvVeTheoSuat_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvVeTheoSuat.Rows.Count) return;

            DataGridViewRow row = dgvVeTheoSuat.Rows[e.RowIndex];
            string trangThai = (row.Cells["TrangThaiThanhToan"].Value?.ToString() ?? string.Empty).Trim();

            if (string.Equals(trangThai, "Đã hủy", StringComparison.OrdinalIgnoreCase))
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                row.DefaultCellStyle.ForeColor = Color.Gray;
                row.DefaultCellStyle.SelectionBackColor = Color.Silver;
                row.DefaultCellStyle.SelectionForeColor = Color.Black;
            }
            else if (string.Equals(trangThai, "Chưa thanh toán", StringComparison.OrdinalIgnoreCase))
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225);
                row.DefaultCellStyle.ForeColor = Color.DarkRed;
            }
        }

        /// <summary>
        /// Sự kiện bấm nút Hủy vé: Sử dụng SqlTransaction (Serializable) để cập nhật trạng thái 'Đã hủy',
        /// hoàn lại lượt khuyến mãi, trừ lại điểm tích lũy của khách hàng và giải phóng ghế trống
        /// </summary>
        private void btnHuyVe_Click(object? sender, EventArgs e)
        {
            if (dgvVeTheoSuat.CurrentRow == null || dgvVeTheoSuat.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn vé trong bảng '4. Danh sách đơn vé của suất chiếu' ở góc dưới bên phải để hủy!",
                    "Chưa chọn đơn vé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maVeHuy = (dgvVeTheoSuat.CurrentRow.Cells["MaVe"].Value?.ToString() ?? string.Empty).Trim();
            string trangThaiHienTai = (dgvVeTheoSuat.CurrentRow.Cells["TrangThaiThanhToan"].Value?.ToString() ?? string.Empty).Trim();
            string gheDaDat = (dgvVeTheoSuat.CurrentRow.Cells["GheDaDat"].Value?.ToString() ?? string.Empty).Trim();

            if (string.Equals(trangThaiHienTai, "Đã hủy", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Đơn vé [{maVeHuy}] đã ở trạng thái 'Đã hủy' trước đó!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn HỦY đơn vé [{maVeHuy}] (Ghế: {gheDaDat})?\n\n" +
                "- Trạng thái vé sẽ cập nhật thành 'Đã hủy'.\n" +
                $"- Các ghế ({gheDaDat}) sẽ được trả về trạng thái Ghế Trống trên sơ đồ.\n" +
                "- Điểm tích lũy và lượt khuyến mãi (nếu có) sẽ được hoàn tác tự động.",
                "Xác nhận hủy vé & hoàn tác ghế",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(AppSession.ConnectionString))
            {
                conn.Open();

                // Sử dụng IsolationLevel.Serializable để đảm bảo tính toàn vẹn ACID khi hoàn tác
                using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        string? maKH = null;
                        string? maKM = null;
                        string trangThaiDB = string.Empty;
                        int soLuongGhe = 0;

                        // BƯỚC 1: Khóa bản ghi vé cần hủy (UPDLOCK, HOLDLOCK) và kiểm tra thông tin gốc
                        string sqlCheck = @"
                            SELECT 
                                LTRIM(RTRIM(v.MaKH)) AS MaKH, 
                                LTRIM(RTRIM(v.MaKM)) AS MaKM, 
                                v.TrangThaiThanhToan,
                                (SELECT COUNT(*) FROM dbo.ChiTietVe ct WHERE LTRIM(RTRIM(ct.MaVe)) = LTRIM(RTRIM(v.MaVe))) AS SoGhe
                            FROM dbo.VeXemPhim v WITH (UPDLOCK, HOLDLOCK)
                            WHERE LTRIM(RTRIM(v.MaVe)) = LTRIM(RTRIM(@MaVe));";

                        using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, trans))
                        {
                            cmdCheck.Parameters.Add("@MaVe", SqlDbType.VarChar, 20).Value = maVeHuy;
                            using (SqlDataReader reader = cmdCheck.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    throw new InvalidOperationException("Không tìm thấy đơn vé trong cơ sở dữ liệu!");
                                }

                                maKH = reader["MaKH"] == DBNull.Value ? null : reader["MaKH"].ToString()?.Trim();
                                maKM = reader["MaKM"] == DBNull.Value ? null : reader["MaKM"].ToString()?.Trim();
                                trangThaiDB = (reader["TrangThaiThanhToan"]?.ToString() ?? string.Empty).Trim();
                                soLuongGhe = Convert.ToInt32(reader["SoGhe"]);
                            }
                        }

                        if (string.Equals(trangThaiDB, "Đã hủy", StringComparison.OrdinalIgnoreCase))
                        {
                            throw new InvalidOperationException("Đơn vé này vừa được hủy bởi một phiên giao dịch khác!");
                        }

                        // BƯỚC 2: Cập nhật trạng thái đơn vé thành N'Đã hủy' để giải phóng ghế trên sơ đồ
                        string sqlCancelTicket = @"
                            UPDATE dbo.VeXemPhim
                            SET TrangThaiThanhToan = N'Đã hủy'
                            WHERE LTRIM(RTRIM(MaVe)) = LTRIM(RTRIM(@MaVe));";

                        using (SqlCommand cmdCancel = new SqlCommand(sqlCancelTicket, conn, trans))
                        {
                            cmdCancel.Parameters.Add("@MaVe", SqlDbType.VarChar, 20).Value = maVeHuy;
                            cmdCancel.ExecuteNonQuery();
                        }

                        // BƯỚC 3: Hoàn lại 1 lượt sử dụng Khuyến mãi (nếu đơn vé có áp dụng khuyến mãi)
                        if (!string.IsNullOrEmpty(maKM))
                        {
                            string sqlRefundKM = @"
                                UPDATE dbo.KhuyenMai
                                SET SoLuongDaDung = CASE WHEN SoLuongDaDung > 0 THEN SoLuongDaDung - 1 ELSE 0 END
                                WHERE LTRIM(RTRIM(MaKM)) = LTRIM(RTRIM(@MaKM));";

                            using (SqlCommand cmdRefundKM = new SqlCommand(sqlRefundKM, conn, trans))
                            {
                                cmdRefundKM.Parameters.Add("@MaKM", SqlDbType.VarChar, 15).Value = maKM;
                                cmdRefundKM.ExecuteNonQuery();
                            }
                        }

                        // BƯỚC 4: Trừ lại điểm tích lũy của Khách hàng (10 điểm / ghế) nếu vé đã thanh toán trước đó
                        int diemBiTru = 0;
                        if (!string.IsNullOrEmpty(maKH) &&
                            string.Equals(trangThaiDB, "Đã thanh toán", StringComparison.OrdinalIgnoreCase) &&
                            soLuongGhe > 0)
                        {
                            diemBiTru = soLuongGhe * 10;
                            string sqlDeductPoints = @"
                                UPDATE dbo.KhachHang
                                SET DiemTichLuy = CASE WHEN DiemTichLuy >= @DiemTru THEN DiemTichLuy - @DiemTru ELSE 0 END
                                WHERE LTRIM(RTRIM(MaKH)) = LTRIM(RTRIM(@MaKH));";

                            using (SqlCommand cmdDeduct = new SqlCommand(sqlDeductPoints, conn, trans))
                            {
                                cmdDeduct.Parameters.Add("@DiemTru", SqlDbType.Int).Value = diemBiTru;
                                cmdDeduct.Parameters.Add("@MaKH", SqlDbType.VarChar, 15).Value = maKH;
                                cmdDeduct.ExecuteNonQuery();
                            }
                        }

                        // BƯỚC 5: Commit Transaction hoàn tất hủy vé
                        trans.Commit();

                        MessageBox.Show(
                            $"Đã hủy đơn vé [{maVeHuy}] thành công!\n" +
                            $"- Ghế được giải phóng về trạng thái Trống: {gheDaDat}\n" +
                            $"- Điểm tích lũy đã thu hồi: {diemBiTru} điểm",
                            "Hoàn tác vé thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // BƯỚC 6: Làm mới lại sơ đồ ghế, danh mục khuyến mãi và danh sách vé của suất chiếu
                        TaiDanhMucKhuyenMai();
                        cboSuatChieu_SelectedIndexChanged(cboSuatChieu, EventArgs.Empty);
                    }
                    catch (SqlException sqlEx)
                    {
                        if (trans.Connection != null)
                        {
                            trans.Rollback();
                        }

                        MessageBox.Show(
                            $"Không thể hủy vé (Đã Rollback an toàn do lỗi SQL Server):\n{sqlEx.Message}",
                            "Lỗi SQL khi hủy vé", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        if (trans.Connection != null)
                        {
                            trans.Rollback();
                        }

                        MessageBox.Show(
                            $"Không thể hủy vé (Transaction đã Rollback an toàn):\n{ex.Message}",
                            "Lỗi hủy vé", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region TÍNH NĂNG IN VÉ XEM PHIM (PRINT DOCUMENT & GDI+)

        private string _printMaVe = string.Empty;
        private string _printTenPhim = string.Empty;
        private string _printSuatChieu = string.Empty;
        private string _printPhong = string.Empty;
        private string _printThanhToan = string.Empty;

        private void InVeXemPhim(string maVe, string tenPhim, string suatChieu, string phong, string thanhToan)
        {
            _printMaVe = maVe;
            _printTenPhim = tenPhim;
            _printSuatChieu = suatChieu;
            _printPhong = phong;
            _printThanhToan = thanhToan;

            using (PrintDocument printDoc = new PrintDocument())
            {
                printDoc.PrintPage += PrintDoc_PrintPage;

                using (PrintPreviewDialog previewDlg = new PrintPreviewDialog())
                {
                    previewDlg.Document = printDoc;
                    previewDlg.Width = 650;
                    previewDlg.Height = 750;
                    previewDlg.Text = "Xem Trước Hóa Đơn Vé Xem Phim - CINESTAR";
                    previewDlg.ShowDialog();
                }
            }
        }

        private void PrintDoc_PrintPage(object? sender, PrintPageEventArgs e)
        {
            if (e.Graphics == null) return;
            Graphics g = e.Graphics;

            using Font fontTitle = new Font("Segoe UI", 16, FontStyle.Bold);
            using Font fontHeader = new Font("Segoe UI", 11, FontStyle.Bold);
            using Font fontBody = new Font("Segoe UI", 10, FontStyle.Regular);
            using Font fontBold = new Font("Segoe UI", 10, FontStyle.Bold);
            Brush brush = Brushes.Black;

            float startX = 20;
            float startY = 20;
            float offset = 25;

            g.DrawString("CINESTAR CINEMA SYSTEM", fontTitle, Brushes.Navy, startX + 40, startY);
            startY += offset + 10;
            g.DrawString("-------------------------------------------------------------------------", fontBody, brush, startX, startY);
            startY += offset;

            g.DrawString($"Mã Giao Dịch : {_printMaVe}", fontBold, brush, startX, startY);
            startY += offset;
            g.DrawString($"Thời gian lập : {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fontBody, brush, startX, startY);
            startY += offset;
            g.DrawString($"Thu ngân lập : {AppSession.HoTen} (NV: {AppSession.MaNV})", fontBody, brush, startX, startY);
            startY += offset;
            g.DrawString($"Hình thức TT : {_printThanhToan}", fontBody, brush, startX, startY);
            startY += offset;
            g.DrawString("-------------------------------------------------------------------------", fontBody, brush, startX, startY);
            startY += offset;

            g.DrawString($"Bộ Phim     : {_printTenPhim}", fontBold, brush, startX, startY);
            startY += offset;
            g.DrawString($"Suất Chiếu  : {_printSuatChieu}", fontBold, brush, startX, startY);
            startY += offset;
            g.DrawString($"Phòng Chiếu : Phòng {_printPhong}", fontBold, brush, startX, startY);
            startY += offset;
            g.DrawString("-------------------------------------------------------------------------", fontBody, brush, startX, startY);
            startY += offset;

            g.DrawString("DANH SÁCH GHẾ ĐÃ ĐẶT:", fontHeader, brush, startX, startY);
            startY += offset;

            g.DrawString("STT", fontBold, brush, startX, startY);
            g.DrawString("Mã Ghế", fontBold, brush, startX + 50, startY);
            g.DrawString("Vị Trí", fontBold, brush, startX + 130, startY);
            g.DrawString("Hạng", fontBold, brush, startX + 250, startY);
            g.DrawString("Đơn Giá (VNĐ)", fontBold, brush, startX + 340, startY);
            startY += 20;
            g.DrawString("-------------------------------------------------------------------------", fontBody, brush, startX, startY);
            startY += offset;

            int stt = 1;
            foreach (var item in _danhSachGheChon)
            {
                g.DrawString(stt.ToString(), fontBody, brush, startX, startY);
                g.DrawString(item.MaGhe, fontBody, brush, startX + 50, startY);
                g.DrawString(item.ViTriGhe, fontBody, brush, startX + 130, startY);
                g.DrawString(item.LoaiGhe, fontBody, brush, startX + 250, startY);
                g.DrawString($"{item.DonGiaBan:N0}", fontBody, brush, startX + 340, startY);
                startY += offset;
                stt++;
            }

            g.DrawString("-------------------------------------------------------------------------", fontBody, brush, startX, startY);
            startY += offset + 10;

            g.DrawString(lblTongTienGoc.Text, fontBody, brush, startX + 200, startY);
            startY += offset;
            g.DrawString(lblTienGiamGia.Text, fontBody, brush, startX + 200, startY);
            startY += offset;
            g.DrawString(lblThanhTien.Text, fontTitle, Brushes.DarkRed, startX + 120, startY);
            startY += offset + 20;

            g.DrawString("      Cảm ơn quý khách đã lựa chọn CineStar!", fontHeader, Brushes.DarkBlue, startX + 40, startY);
            startY += offset;
            g.DrawString("             Chúc quý khách xem phim vui vẻ!", fontBody, brush, startX + 50, startY);
        }

        #endregion
    }
}