using System.Drawing;
using System.Windows.Forms;

namespace CineStarApp
{
    partial class FrmThongKeDoanhThu
    {
        private System.ComponentModel.IContainer? components = null;

        private TabControl tabMain = null!;

        // ================= TAB 1: DOANH THU TỔNG HỢP & FUNCTION =================
        private TabPage tabDoanhThu = null!;
        private Panel pnlTopDoanhThu = null!;
        private Label lblChonThang = null!;
        private NumericUpDown nudThang = null!;
        private Label lblChonNam = null!;
        private NumericUpDown nudNam = null!;
        private Button btnLocDoanhThu = null!;
        private Button btnTatCaDoanhThu = null!;

        private GroupBox grpTinhTongUDF = null!;
        private Label lblHuongDanUDF = null!;
        private TextBox txtMaDinhDanhUDF = null!;
        private Button btnTinhTongUDF = null!;
        private Label lblKetQuaUDF = null!;

        private Panel pnlKpiSummary = null!;
        private Label lblKpiTongHoaDon = null!;
        private Label lblKpiTongGhe = null!;
        private Label lblKpiDoanhThuThuc = null!;
        private DataGridView dgvThongKeTongHop = null!;

        // ================= TAB 2: LỊCH SỬ GIAO DỊCH (SP PHÂN TRANG) =================
        private TabPage tabGiaoDich = null!;
        private Panel pnlFilterGiaoDich = null!;
        private Label lblTuNgay = null!;
        private DateTimePicker dtpTuNgay = null!;
        private Label lblDenNgay = null!;
        private DateTimePicker dtpDenNgay = null!;
        private Label lblKhachHangGD = null!;
        private ComboBox cboKhachHangGD = null!;
        private Label lblPageSize = null!;
        private NumericUpDown nudPageSize = null!;
        private Button btnTimGiaoDich = null!;
        private DataGridView dgvGiaoDich = null!;
        private Panel pnlPagination = null!;
        private Button btnTrangTruoc = null!;
        private Label lblThongTinTrang = null!;
        private Button btnTrangSau = null!;

        // ================= TAB 3: DANH SÁCH CHỜ XỬ LÝ (VIEW) =================
        private TabPage tabChuaXuLy = null!;
        private Panel pnlTopChuaXuLy = null!;
        private Label lblLoaiCanXuLy = null!;
        private ComboBox cboLoaiCanXuLy = null!;
        private Button btnTaiLaiChuaXuLy = null!;
        private DataGridView dgvChuaXuLy = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabMain = new TabControl();

            // Khởi tạo Tab 1
            this.tabDoanhThu = new TabPage();
            this.pnlTopDoanhThu = new Panel();
            this.lblChonThang = new Label();
            this.nudThang = new NumericUpDown();
            this.lblChonNam = new Label();
            this.nudNam = new NumericUpDown();
            this.btnLocDoanhThu = new Button();
            this.btnTatCaDoanhThu = new Button();
            this.grpTinhTongUDF = new GroupBox();
            this.lblHuongDanUDF = new Label();
            this.txtMaDinhDanhUDF = new TextBox();
            this.btnTinhTongUDF = new Button();
            this.lblKetQuaUDF = new Label();
            this.pnlKpiSummary = new Panel();
            this.lblKpiTongHoaDon = new Label();
            this.lblKpiTongGhe = new Label();
            this.lblKpiDoanhThuThuc = new Label();
            this.dgvThongKeTongHop = new DataGridView();

            // Khởi tạo Tab 2
            this.tabGiaoDich = new TabPage();
            this.pnlFilterGiaoDich = new Panel();
            this.lblTuNgay = new Label();
            this.dtpTuNgay = new DateTimePicker();
            this.lblDenNgay = new Label();
            this.dtpDenNgay = new DateTimePicker();
            this.lblKhachHangGD = new Label();
            this.cboKhachHangGD = new ComboBox();
            this.lblPageSize = new Label();
            this.nudPageSize = new NumericUpDown();
            this.btnTimGiaoDich = new Button();
            this.dgvGiaoDich = new DataGridView();
            this.pnlPagination = new Panel();
            this.btnTrangTruoc = new Button();
            this.lblThongTinTrang = new Label();
            this.btnTrangSau = new Button();

            // Khởi tạo Tab 3
            this.tabChuaXuLy = new TabPage();
            this.pnlTopChuaXuLy = new Panel();
            this.lblLoaiCanXuLy = new Label();
            this.cboLoaiCanXuLy = new ComboBox();
            this.btnTaiLaiChuaXuLy = new Button();
            this.dgvChuaXuLy = new DataGridView();

            this.tabMain.SuspendLayout();
            this.tabDoanhThu.SuspendLayout();
            this.pnlTopDoanhThu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).BeginInit();
            this.grpTinhTongUDF.SuspendLayout();
            this.pnlKpiSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeTongHop)).BeginInit();

            this.tabGiaoDich.SuspendLayout();
            this.pnlFilterGiaoDich.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).BeginInit();
            this.pnlPagination.SuspendLayout();

            this.tabChuaXuLy.SuspendLayout();
            this.pnlTopChuaXuLy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuaXuLy)).BeginInit();
            this.SuspendLayout();

            // tabMain
            this.tabMain.Controls.Add(this.tabDoanhThu);
            this.tabMain.Controls.Add(this.tabGiaoDich);
            this.tabMain.Controls.Add(this.tabChuaXuLy);
            this.tabMain.Dock = DockStyle.Fill;
            this.tabMain.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.tabMain.ItemSize = new Size(280, 34);
            this.tabMain.Location = new Point(0, 0);
            this.tabMain.Size = new Size(1050, 660);

            // =========================================================================
            // TAB 1: THỐNG KÊ DOANH THU TỔNG HỢP & HÀM F_TinhTongGiaTri
            // =========================================================================
            this.tabDoanhThu.BackColor = Color.White;
            this.tabDoanhThu.Controls.Add(this.dgvThongKeTongHop);
            this.tabDoanhThu.Controls.Add(this.pnlKpiSummary);
            this.tabDoanhThu.Controls.Add(this.pnlTopDoanhThu);
            this.tabDoanhThu.Padding = new Padding(8);
            this.tabDoanhThu.Text = "📊 1. Doanh Thu Tổng Hợp & UDF";

            // pnlTopDoanhThu
            this.pnlTopDoanhThu.BackColor = Color.FromArgb(245, 247, 250);
            this.pnlTopDoanhThu.Controls.Add(this.grpTinhTongUDF);
            this.pnlTopDoanhThu.Controls.Add(this.btnTatCaDoanhThu);
            this.pnlTopDoanhThu.Controls.Add(this.btnLocDoanhThu);
            this.pnlTopDoanhThu.Controls.Add(this.nudNam);
            this.pnlTopDoanhThu.Controls.Add(this.lblChonNam);
            this.pnlTopDoanhThu.Controls.Add(this.nudThang);
            this.pnlTopDoanhThu.Controls.Add(this.lblChonThang);
            this.pnlTopDoanhThu.Dock = DockStyle.Top;
            this.pnlTopDoanhThu.Height = 95;

            this.lblChonThang.AutoSize = true;
            this.lblChonThang.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblChonThang.Location = new Point(15, 20);
            this.lblChonThang.Text = "Tháng:";

            this.nudThang.Font = new Font("Segoe UI", 10F);
            this.nudThang.Location = new Point(75, 17);
            this.nudThang.Minimum = 1;
            this.nudThang.Maximum = 12;
            this.nudThang.Value = 10;
            this.nudThang.Size = new Size(65, 25);

            this.lblChonNam.AutoSize = true;
            this.lblChonNam.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblChonNam.Location = new Point(155, 20);
            this.lblChonNam.Text = "Năm:";

            this.nudNam.Font = new Font("Segoe UI", 10F);
            this.nudNam.Location = new Point(205, 17);
            this.nudNam.Minimum = 2020;
            this.nudNam.Maximum = 2035;
            this.nudNam.Value = 2026;
            this.nudNam.Size = new Size(85, 25);

            this.btnLocDoanhThu.BackColor = Color.FromArgb(0, 123, 255);
            this.btnLocDoanhThu.FlatStyle = FlatStyle.Flat;
            this.btnLocDoanhThu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnLocDoanhThu.ForeColor = Color.White;
            this.btnLocDoanhThu.Location = new Point(15, 52);
            this.btnLocDoanhThu.Size = new Size(135, 34);
            this.btnLocDoanhThu.Text = "🔍 Lọc Theo Tháng";
            this.btnLocDoanhThu.UseVisualStyleBackColor = false;
            this.btnLocDoanhThu.Click += new System.EventHandler(this.btnLocDoanhThu_Click);

            this.btnTatCaDoanhThu.BackColor = Color.FromArgb(108, 117, 125);
            this.btnTatCaDoanhThu.FlatStyle = FlatStyle.Flat;
            this.btnTatCaDoanhThu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnTatCaDoanhThu.ForeColor = Color.White;
            this.btnTatCaDoanhThu.Location = new Point(160, 52);
            this.btnTatCaDoanhThu.Size = new Size(130, 34);
            this.btnTatCaDoanhThu.Text = "🔄 Xem Tất Cả";
            this.btnTatCaDoanhThu.UseVisualStyleBackColor = false;
            this.btnTatCaDoanhThu.Click += new System.EventHandler(this.btnTatCaDoanhThu_Click);

            // grpTinhTongUDF (Gọi hàm F_TinhTongGiaTri)
            this.grpTinhTongUDF.Controls.Add(this.lblKetQuaUDF);
            this.grpTinhTongUDF.Controls.Add(this.btnTinhTongUDF);
            this.grpTinhTongUDF.Controls.Add(this.txtMaDinhDanhUDF);
            this.grpTinhTongUDF.Controls.Add(this.lblHuongDanUDF);
            this.grpTinhTongUDF.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpTinhTongUDF.Location = new Point(320, 6);
            this.grpTinhTongUDF.Size = new Size(695, 82);
            this.grpTinhTongUDF.Text = "TRA CỨU NHANH BẰNG FUNCTION: dbo.F_TinhTongGiaTri(@MaDinhDanh)";

            this.lblHuongDanUDF.AutoSize = true;
            this.lblHuongDanUDF.Font = new Font("Segoe UI", 9F);
            this.lblHuongDanUDF.Location = new Point(12, 32);
            this.lblHuongDanUDF.Text = "Nhập Mã KH (VD: KH001) hoặc Mã Suất (VD: SC001):";

            this.txtMaDinhDanhUDF.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.txtMaDinhDanhUDF.Location = new Point(320, 28);
            this.txtMaDinhDanhUDF.Size = new Size(110, 25);
            this.txtMaDinhDanhUDF.Text = "SC001";

            this.btnTinhTongUDF.BackColor = Color.FromArgb(40, 167, 69);
            this.btnTinhTongUDF.FlatStyle = FlatStyle.Flat;
            this.btnTinhTongUDF.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnTinhTongUDF.ForeColor = Color.White;
            this.btnTinhTongUDF.Location = new Point(440, 25);
            this.btnTinhTongUDF.Size = new Size(105, 31);
            this.btnTinhTongUDF.Text = "⚡ Tính Tổng";
            this.btnTinhTongUDF.UseVisualStyleBackColor = false;
            this.btnTinhTongUDF.Click += new System.EventHandler(this.btnTinhTongUDF_Click);

            this.lblKetQuaUDF.AutoSize = true;
            this.lblKetQuaUDF.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.lblKetQuaUDF.ForeColor = Color.FromArgb(211, 47, 47);
            this.lblKetQuaUDF.Location = new Point(555, 30);
            this.lblKetQuaUDF.Text = "= 0 VNĐ";

            // pnlKpiSummary
            this.pnlKpiSummary.BackColor = Color.FromArgb(24, 30, 54);
            this.pnlKpiSummary.Controls.Add(this.lblKpiDoanhThuThuc);
            this.pnlKpiSummary.Controls.Add(this.lblKpiTongGhe);
            this.pnlKpiSummary.Controls.Add(this.lblKpiTongHoaDon);
            this.pnlKpiSummary.Dock = DockStyle.Top;
            this.pnlKpiSummary.Height = 52;

            this.lblKpiTongHoaDon.AutoSize = true;
            this.lblKpiTongHoaDon.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblKpiTongHoaDon.ForeColor = Color.White;
            this.lblKpiTongHoaDon.Location = new Point(20, 14);
            this.lblKpiTongHoaDon.Text = "🧾 Tổng số hóa đơn vé: 0";

            this.lblKpiTongGhe.AutoSize = true;
            this.lblKpiTongGhe.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblKpiTongGhe.ForeColor = Color.Cyan;
            this.lblKpiTongGhe.Location = new Point(330, 14);
            this.lblKpiTongGhe.Text = "💺 Tổng số ghế bán ra: 0";

            this.lblKpiDoanhThuThuc.AutoSize = true;
            this.lblKpiDoanhThuThuc.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblKpiDoanhThuThuc.ForeColor = Color.Gold;
            this.lblKpiDoanhThuThuc.Location = new Point(640, 13);
            this.lblKpiDoanhThuThuc.Text = "💰 TỔNG DOANH THU THỰC THU: 0 VNĐ";

            // dgvThongKeTongHop
            ConfigureStandardGrid(this.dgvThongKeTongHop);

            // =========================================================================
            // TAB 2: LỊCH SỬ GIAO DỊCH & PHÂN TRANG (P_DanhSachGiaoDich)
            // =========================================================================
            this.tabGiaoDich.BackColor = Color.White;
            this.tabGiaoDich.Controls.Add(this.dgvGiaoDich);
            this.tabGiaoDich.Controls.Add(this.pnlPagination);
            this.tabGiaoDich.Controls.Add(this.pnlFilterGiaoDich);
            this.tabGiaoDich.Padding = new Padding(8);
            this.tabGiaoDich.Text = "🧾 2. Lịch Sử Giao Dịch (SP Phân Trang)";

            // pnlFilterGiaoDich
            this.pnlFilterGiaoDich.BackColor = Color.FromArgb(245, 247, 250);
            this.pnlFilterGiaoDich.Controls.Add(this.btnTimGiaoDich);
            this.pnlFilterGiaoDich.Controls.Add(this.nudPageSize);
            this.pnlFilterGiaoDich.Controls.Add(this.lblPageSize);
            this.pnlFilterGiaoDich.Controls.Add(this.cboKhachHangGD);
            this.pnlFilterGiaoDich.Controls.Add(this.lblKhachHangGD);
            this.pnlFilterGiaoDich.Controls.Add(this.dtpDenNgay);
            this.pnlFilterGiaoDich.Controls.Add(this.lblDenNgay);
            this.pnlFilterGiaoDich.Controls.Add(this.dtpTuNgay);
            this.pnlFilterGiaoDich.Controls.Add(this.lblTuNgay);
            this.pnlFilterGiaoDich.Dock = DockStyle.Top;
            this.pnlFilterGiaoDich.Height = 60;

            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblTuNgay.Location = new Point(15, 20);
            this.lblTuNgay.Text = "Từ ngày:";

            this.dtpTuNgay.Font = new Font("Segoe UI", 9.5F);
            this.dtpTuNgay.Format = DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new Point(80, 16);
            this.dtpTuNgay.Size = new Size(115, 24);

            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblDenNgay.Location = new Point(210, 20);
            this.lblDenNgay.Text = "Đến ngày:";

            this.dtpDenNgay.Font = new Font("Segoe UI", 9.5F);
            this.dtpDenNgay.Format = DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new Point(285, 16);
            this.dtpDenNgay.Size = new Size(115, 24);

            this.lblKhachHangGD.AutoSize = true;
            this.lblKhachHangGD.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblKhachHangGD.Location = new Point(415, 20);
            this.lblKhachHangGD.Text = "Khách hàng:";

            this.cboKhachHangGD.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboKhachHangGD.Font = new Font("Segoe UI", 9.5F);
            this.cboKhachHangGD.Location = new Point(505, 16);
            this.cboKhachHangGD.Size = new Size(210, 24);

            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblPageSize.Location = new Point(730, 20);
            this.lblPageSize.Text = "Số dòng/trang:";

            this.nudPageSize.Font = new Font("Segoe UI", 9.5F);
            this.nudPageSize.Location = new Point(835, 16);
            this.nudPageSize.Minimum = 5;
            this.nudPageSize.Maximum = 100;
            this.nudPageSize.Value = 10;
            this.nudPageSize.Size = new Size(55, 24);

            this.btnTimGiaoDich.BackColor = Color.FromArgb(0, 123, 255);
            this.btnTimGiaoDich.FlatStyle = FlatStyle.Flat;
            this.btnTimGiaoDich.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnTimGiaoDich.ForeColor = Color.White;
            this.btnTimGiaoDich.Location = new Point(905, 12);
            this.btnTimGiaoDich.Size = new Size(110, 34);
            this.btnTimGiaoDich.Text = "🔍 Tra Cứu";
            this.btnTimGiaoDich.UseVisualStyleBackColor = false;
            this.btnTimGiaoDich.Click += new System.EventHandler(this.btnTimGiaoDich_Click);

            // pnlPagination
            this.pnlPagination.BackColor = Color.FromArgb(240, 244, 248);
            this.pnlPagination.Controls.Add(this.btnTrangSau);
            this.pnlPagination.Controls.Add(this.lblThongTinTrang);
            this.pnlPagination.Controls.Add(this.btnTrangTruoc);
            this.pnlPagination.Dock = DockStyle.Bottom;
            this.pnlPagination.Height = 50;

            this.btnTrangTruoc.BackColor = Color.FromArgb(52, 58, 64);
            this.btnTrangTruoc.FlatStyle = FlatStyle.Flat;
            this.btnTrangTruoc.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnTrangTruoc.ForeColor = Color.White;
            this.btnTrangTruoc.Location = new Point(310, 8);
            this.btnTrangTruoc.Size = new Size(120, 34);
            this.btnTrangTruoc.Text = "◀ Trang Trước";
            this.btnTrangTruoc.UseVisualStyleBackColor = false;
            this.btnTrangTruoc.Click += new System.EventHandler(this.btnTrangTruoc_Click);

            this.lblThongTinTrang.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblThongTinTrang.ForeColor = Color.FromArgb(24, 30, 54);
            this.lblThongTinTrang.Location = new Point(440, 12);
            this.lblThongTinTrang.Size = new Size(260, 25);
            this.lblThongTinTrang.Text = "Trang 1 / 1 (Tổng: 0 giao dịch)";
            this.lblThongTinTrang.TextAlign = ContentAlignment.MiddleCenter;

            this.btnTrangSau.BackColor = Color.FromArgb(52, 58, 64);
            this.btnTrangSau.FlatStyle = FlatStyle.Flat;
            this.btnTrangSau.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnTrangSau.ForeColor = Color.White;
            this.btnTrangSau.Location = new Point(710, 8);
            this.btnTrangSau.Size = new Size(120, 34);
            this.btnTrangSau.Text = "Trang Sau ▶";
            this.btnTrangSau.UseVisualStyleBackColor = false;
            this.btnTrangSau.Click += new System.EventHandler(this.btnTrangSau_Click);

            // dgvGiaoDich
            ConfigureStandardGrid(this.dgvGiaoDich);

            // =========================================================================
            // TAB 3: DANH SÁCH CHỜ XỬ LÝ (V_DanhSachChuaXuLy)
            // =========================================================================
            this.tabChuaXuLy.BackColor = Color.White;
            this.tabChuaXuLy.Controls.Add(this.dgvChuaXuLy);
            this.tabChuaXuLy.Controls.Add(this.pnlTopChuaXuLy);
            this.tabChuaXuLy.Padding = new Padding(8);
            this.tabChuaXuLy.Text = "⏳ 3. Danh Sách Chờ Xử Lý (View)";

            this.pnlTopChuaXuLy.BackColor = Color.FromArgb(245, 247, 250);
            this.pnlTopChuaXuLy.Controls.Add(this.btnTaiLaiChuaXuLy);
            this.pnlTopChuaXuLy.Controls.Add(this.cboLoaiCanXuLy);
            this.pnlTopChuaXuLy.Controls.Add(this.lblLoaiCanXuLy);
            this.pnlTopChuaXuLy.Dock = DockStyle.Top;
            this.pnlTopChuaXuLy.Height = 55;

            this.lblLoaiCanXuLy.AutoSize = true;
            this.lblLoaiCanXuLy.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblLoaiCanXuLy.Location = new Point(15, 17);
            this.lblLoaiCanXuLy.Text = "Phân loại đối tượng cần xử lý:";

            this.cboLoaiCanXuLy.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboLoaiCanXuLy.Font = new Font("Segoe UI", 10F);
            this.cboLoaiCanXuLy.Items.AddRange(new object[] {
                "--- Tất cả đối tượng chờ xử lý ---",
                "Đơn vé chưa thanh toán",
                "Suất chiếu chưa có khách đặt"
            });
            this.cboLoaiCanXuLy.Location = new Point(225, 13);
            this.cboLoaiCanXuLy.Size = new Size(300, 26);
            this.cboLoaiCanXuLy.SelectedIndexChanged += new System.EventHandler(this.cboLoaiCanXuLy_SelectedIndexChanged);

            this.btnTaiLaiChuaXuLy.BackColor = Color.FromArgb(40, 167, 69);
            this.btnTaiLaiChuaXuLy.FlatStyle = FlatStyle.Flat;
            this.btnTaiLaiChuaXuLy.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnTaiLaiChuaXuLy.ForeColor = Color.White;
            this.btnTaiLaiChuaXuLy.Location = new Point(545, 10);
            this.btnTaiLaiChuaXuLy.Size = new Size(150, 34);
            this.btnTaiLaiChuaXuLy.Text = "🔄 Làm Mới Dữ Liệu";
            this.btnTaiLaiChuaXuLy.UseVisualStyleBackColor = false;
            this.btnTaiLaiChuaXuLy.Click += new System.EventHandler(this.btnTaiLaiChuaXuLy_Click);

            // dgvChuaXuLy
            ConfigureStandardGrid(this.dgvChuaXuLy);

            // FrmThongKeDoanhThu
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(1050, 660);
            this.Controls.Add(this.tabMain);
            this.Text = "Báo Cáo Thống Kê Tổng Hợp & Tra Cứu Hệ Thống";
            this.Load += new System.EventHandler(this.FrmThongKeDoanhThu_Load);

            this.tabMain.ResumeLayout(false);
            this.tabDoanhThu.ResumeLayout(false);
            this.pnlTopDoanhThu.ResumeLayout(false);
            this.pnlTopDoanhThu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).EndInit();
            this.grpTinhTongUDF.ResumeLayout(false);
            this.grpTinhTongUDF.PerformLayout();
            this.pnlKpiSummary.ResumeLayout(false);
            this.pnlKpiSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeTongHop)).EndInit();

            this.tabGiaoDich.ResumeLayout(false);
            this.pnlFilterGiaoDich.ResumeLayout(false);
            this.pnlFilterGiaoDich.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).EndInit();
            this.pnlPagination.ResumeLayout(false);

            this.tabChuaXuLy.ResumeLayout(false);
            this.pnlTopChuaXuLy.ResumeLayout(false);
            this.pnlTopChuaXuLy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuaXuLy)).EndInit();
            this.ResumeLayout(false);
        }

        private void ConfigureStandardGrid(DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Dock = DockStyle.Fill;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}