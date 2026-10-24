using System.Drawing;
using System.Windows.Forms;

namespace CineStarApp
{
    partial class FrmQuanLyPhim
    {
        private System.ComponentModel.IContainer components = null;

        private GroupBox grpThongTinPhim;
        private Label lblMaPhim;
        private TextBox txtMaPhim;
        private Label lblTenPhim;
        private TextBox txtTenPhim;
        private Label lblTheLoai;
        private TextBox txtTheLoai;
        private Label lblThoiLuong;
        private NumericUpDown nudThoiLuong;
        private Label lblDaoDien;
        private TextBox txtDaoDien;
        private Label lblDoTuoi;
        private ComboBox cboDoTuoi;
        private Label lblNgayKhoiChieu;
        private DateTimePicker dtpNgayKhoiChieu;
        private Label lblTrangThai;
        private ComboBox cboTrangThai;

        private Panel pnlActions;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private DataGridView dgvPhim;

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
            this.grpThongTinPhim = new GroupBox();
            this.lblMaPhim = new Label();
            this.txtMaPhim = new TextBox();
            this.lblTenPhim = new Label();
            this.txtTenPhim = new TextBox();
            this.lblTheLoai = new Label();
            this.txtTheLoai = new TextBox();
            this.lblThoiLuong = new Label();
            this.nudThoiLuong = new NumericUpDown();
            this.lblDaoDien = new Label();
            this.txtDaoDien = new TextBox();
            this.lblDoTuoi = new Label();
            this.cboDoTuoi = new ComboBox();
            this.lblNgayKhoiChieu = new Label();
            this.dtpNgayKhoiChieu = new DateTimePicker();
            this.lblTrangThai = new Label();
            this.cboTrangThai = new ComboBox();

            this.pnlActions = new Panel();
            this.btnThem = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();
            this.btnLamMoi = new Button();
            this.lblTimKiem = new Label();
            this.txtTimKiem = new TextBox();
            this.dgvPhim = new DataGridView();

            this.grpThongTinPhim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThoiLuong)).BeginInit();
            this.pnlActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhim)).BeginInit();
            this.SuspendLayout();

            // grpThongTinPhim
            this.grpThongTinPhim.Controls.Add(this.cboTrangThai);
            this.grpThongTinPhim.Controls.Add(this.lblTrangThai);
            this.grpThongTinPhim.Controls.Add(this.dtpNgayKhoiChieu);
            this.grpThongTinPhim.Controls.Add(this.lblNgayKhoiChieu);
            this.grpThongTinPhim.Controls.Add(this.cboDoTuoi);
            this.grpThongTinPhim.Controls.Add(this.lblDoTuoi);
            this.grpThongTinPhim.Controls.Add(this.txtDaoDien);
            this.grpThongTinPhim.Controls.Add(this.lblDaoDien);
            this.grpThongTinPhim.Controls.Add(this.nudThoiLuong);
            this.grpThongTinPhim.Controls.Add(this.lblThoiLuong);
            this.grpThongTinPhim.Controls.Add(this.txtTheLoai);
            this.grpThongTinPhim.Controls.Add(this.lblTheLoai);
            this.grpThongTinPhim.Controls.Add(this.txtTenPhim);
            this.grpThongTinPhim.Controls.Add(this.lblTenPhim);
            this.grpThongTinPhim.Controls.Add(this.txtMaPhim);
            this.grpThongTinPhim.Controls.Add(this.lblMaPhim);
            this.grpThongTinPhim.Dock = DockStyle.Top;
            this.grpThongTinPhim.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpThongTinPhim.Location = new Point(0, 0);
            this.grpThongTinPhim.Padding = new Padding(12);
            this.grpThongTinPhim.Size = new Size(1020, 155);
            this.grpThongTinPhim.Text = "THÔNG TIN CHI TIẾT BỘ PHIM";

            // Hàng 1: Mã Phim, Tên Phim, Độ Tuổi
            this.lblMaPhim.AutoSize = true;
            this.lblMaPhim.Font = new Font("Segoe UI", 9.5F);
            this.lblMaPhim.Location = new Point(20, 35);
            this.lblMaPhim.Text = "Mã Phim (*):";

            this.txtMaPhim.Font = new Font("Segoe UI", 10F);
            this.txtMaPhim.Location = new Point(115, 32);
            this.txtMaPhim.Size = new Size(140, 25);

            this.lblTenPhim.AutoSize = true;
            this.lblTenPhim.Font = new Font("Segoe UI", 9.5F);
            this.lblTenPhim.Location = new Point(280, 35);
            this.lblTenPhim.Text = "Tên Bộ Phim (*):";

            this.txtTenPhim.Font = new Font("Segoe UI", 10F);
            this.txtTenPhim.Location = new Point(395, 32);
            this.txtTenPhim.Size = new Size(340, 25);

            this.lblDoTuoi.AutoSize = true;
            this.lblDoTuoi.Font = new Font("Segoe UI", 9.5F);
            this.lblDoTuoi.Location = new Point(760, 35);
            this.lblDoTuoi.Text = "Độ tuổi:";

            this.cboDoTuoi.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDoTuoi.Font = new Font("Segoe UI", 10F);
            this.cboDoTuoi.Items.AddRange(new object[] { "P", "K", "T13", "T16", "T18" });
            this.cboDoTuoi.Location = new Point(855, 32);
            this.cboDoTuoi.Size = new Size(135, 25);

            // Hàng 2: Thể Loại, Đạo Diễn, Thời Lượng
            this.lblTheLoai.AutoSize = true;
            this.lblTheLoai.Font = new Font("Segoe UI", 9.5F);
            this.lblTheLoai.Location = new Point(20, 75);
            this.lblTheLoai.Text = "Thể Loại (*):";

            this.txtTheLoai.Font = new Font("Segoe UI", 10F);
            this.txtTheLoai.Location = new Point(115, 72);
            this.txtTheLoai.Size = new Size(250, 25);

            this.lblDaoDien.AutoSize = true;
            this.lblDaoDien.Font = new Font("Segoe UI", 9.5F);
            this.lblDaoDien.Location = new Point(395, 75);
            this.lblDaoDien.Text = "Đạo Diễn:";

            this.txtDaoDien.Font = new Font("Segoe UI", 10F);
            this.txtDaoDien.Location = new Point(475, 72);
            this.txtDaoDien.Size = new Size(260, 25);

            this.lblThoiLuong.AutoSize = true;
            this.lblThoiLuong.Font = new Font("Segoe UI", 9.5F);
            this.lblThoiLuong.Location = new Point(760, 75);
            this.lblThoiLuong.Text = "Thời lượng (p):";

            this.nudThoiLuong.Font = new Font("Segoe UI", 10F);
            this.nudThoiLuong.Location = new Point(865, 72);
            this.nudThoiLuong.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            this.nudThoiLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudThoiLuong.Value = new decimal(new int[] { 120, 0, 0, 0 });
            this.nudThoiLuong.Size = new Size(125, 25);

            // Hàng 3: Ngày Khởi Chiếu, Trạng Thái
            this.lblNgayKhoiChieu.AutoSize = true;
            this.lblNgayKhoiChieu.Font = new Font("Segoe UI", 9.5F);
            this.lblNgayKhoiChieu.Location = new Point(20, 115);
            this.lblNgayKhoiChieu.Text = "Khởi Chiếu:";

            this.dtpNgayKhoiChieu.Font = new Font("Segoe UI", 10F);
            this.dtpNgayKhoiChieu.Format = DateTimePickerFormat.Short;
            this.dtpNgayKhoiChieu.Location = new Point(115, 112);
            this.dtpNgayKhoiChieu.Size = new Size(140, 25);

            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new Font("Segoe UI", 9.5F);
            this.lblTrangThai.Location = new Point(280, 115);
            this.lblTrangThai.Text = "Trạng Thái:";

            this.cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new Font("Segoe UI", 10F);
            this.cboTrangThai.Items.AddRange(new object[] { "Sắp chiếu", "Đang chiếu", "Ngừng chiếu" });
            this.cboTrangThai.Location = new Point(395, 112);
            this.cboTrangThai.Size = new Size(200, 25);

            // pnlActions
            this.pnlActions.BackColor = Color.FromArgb(245, 247, 250);
            this.pnlActions.Controls.Add(this.txtTimKiem);
            this.pnlActions.Controls.Add(this.lblTimKiem);
            this.pnlActions.Controls.Add(this.btnLamMoi);
            this.pnlActions.Controls.Add(this.btnXoa);
            this.pnlActions.Controls.Add(this.btnSua);
            this.pnlActions.Controls.Add(this.btnThem);
            this.pnlActions.Dock = DockStyle.Top;
            this.pnlActions.Location = new Point(0, 155);
            this.pnlActions.Size = new Size(1020, 55);

            CreateActionButton(this.btnThem, "➕ Thêm Mới", Color.FromArgb(40, 167, 69), 20);
            CreateActionButton(this.btnSua, "✏️ Cập Nhật", Color.FromArgb(0, 123, 255), 155);
            CreateActionButton(this.btnXoa, "🗑️ Xóa Phim", Color.FromArgb(220, 53, 69), 290);
            CreateActionButton(this.btnLamMoi, "🔄 Làm Mới", Color.FromArgb(108, 117, 125), 425);

            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblTimKiem.Location = new Point(580, 18);
            this.lblTimKiem.Text = "🔍 Tìm kiếm nhanh:";

            this.txtTimKiem.Font = new Font("Segoe UI", 10F);
            this.txtTimKiem.Location = new Point(715, 14);
            this.txtTimKiem.Size = new Size(275, 25);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            // dgvPhim
            this.dgvPhim.AllowUserToAddRows = false;
            this.dgvPhim.AllowUserToDeleteRows = false;
            this.dgvPhim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhim.BackgroundColor = Color.White;
            this.dgvPhim.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhim.Dock = DockStyle.Fill;
            this.dgvPhim.Location = new Point(0, 210);
            this.dgvPhim.MultiSelect = false;
            this.dgvPhim.ReadOnly = true;
            this.dgvPhim.RowHeadersVisible = false;
            this.dgvPhim.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhim.Size = new Size(1020, 420);
            this.dgvPhim.CellClick += new DataGridViewCellEventHandler(this.dgvPhim_CellClick);

            // FrmQuanLyPhim
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(1020, 630);
            this.Controls.Add(this.dgvPhim);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.grpThongTinPhim);
            this.Text = "Quản Lý Danh Mục Phim";
            this.Load += new System.EventHandler(this.FrmQuanLyPhim_Load);

            this.grpThongTinPhim.ResumeLayout(false);
            this.grpThongTinPhim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThoiLuong)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhim)).EndInit();
            this.ResumeLayout(false);
        }

        private void CreateActionButton(Button btn, string text, Color backColor, int leftX)
        {
            btn.BackColor = backColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.Location = new Point(leftX, 10);
            btn.Size = new Size(125, 35);
            btn.Text = text;
            btn.UseVisualStyleBackColor = false;
        }
    }
}