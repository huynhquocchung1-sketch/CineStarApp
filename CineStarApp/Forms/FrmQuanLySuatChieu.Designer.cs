using System.Drawing;
using System.Windows.Forms;

namespace CineStarApp
{
    partial class FrmQuanLySuatChieu
    {
        private System.ComponentModel.IContainer? components = null;

        private GroupBox grpThongTinSuat = null!;
        private Label lblMaSuat = null!;
        private TextBox txtMaSuat = null!;
        private Label lblChonPhim = null!;
        private ComboBox cboPhimSC = null!;
        private Label lblPhongChieu = null!;
        private ComboBox cboPhongChieu = null!;
        private Label lblDinhDang = null!;
        private ComboBox cboDinhDang = null!;
        private Label lblGiaVe = null!;
        private NumericUpDown nudGiaVe = null!;
        private Label lblThoiGianBatDau = null!;
        private DateTimePicker dtpBatDau = null!;
        private Label lblThoiGianKetThuc = null!;
        private DateTimePicker dtpKetThuc = null!;
        private Label lblTrangThaiSC = null!;
        private ComboBox cboTrangThaiSC = null!;

        private Panel pnlActionsSC = null!;
        private Button btnThemSC = null!;
        private Button btnSuaSC = null!;
        private Button btnHuySC = null!;
        private Button btnLamMoiSC = null!;
        private Label lblTimKiemSC = null!;
        private TextBox txtTimKiemSC = null!;
        private DataGridView dgvSuatChieu = null!;

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
            this.grpThongTinSuat = new GroupBox();
            this.lblMaSuat = new Label();
            this.txtMaSuat = new TextBox();
            this.lblChonPhim = new Label();
            this.cboPhimSC = new ComboBox();
            this.lblPhongChieu = new Label();
            this.cboPhongChieu = new ComboBox();
            this.lblDinhDang = new Label();
            this.cboDinhDang = new ComboBox();
            this.lblGiaVe = new Label();
            this.nudGiaVe = new NumericUpDown();
            this.lblThoiGianBatDau = new Label();
            this.dtpBatDau = new DateTimePicker();
            this.lblThoiGianKetThuc = new Label();
            this.dtpKetThuc = new DateTimePicker();
            this.lblTrangThaiSC = new Label();
            this.cboTrangThaiSC = new ComboBox();

            this.pnlActionsSC = new Panel();
            this.btnThemSC = new Button();
            this.btnSuaSC = new Button();
            this.btnHuySC = new Button();
            this.btnLamMoiSC = new Button();
            this.lblTimKiemSC = new Label();
            this.txtTimKiemSC = new TextBox();
            this.dgvSuatChieu = new DataGridView();

            this.grpThongTinSuat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiaVe)).BeginInit();
            this.pnlActionsSC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuatChieu)).BeginInit();
            this.SuspendLayout();

            // grpThongTinSuat
            this.grpThongTinSuat.Controls.Add(this.cboTrangThaiSC);
            this.grpThongTinSuat.Controls.Add(this.lblTrangThaiSC);
            this.grpThongTinSuat.Controls.Add(this.dtpKetThuc);
            this.grpThongTinSuat.Controls.Add(this.lblThoiGianKetThuc);
            this.grpThongTinSuat.Controls.Add(this.dtpBatDau);
            this.grpThongTinSuat.Controls.Add(this.lblThoiGianBatDau);
            this.grpThongTinSuat.Controls.Add(this.nudGiaVe);
            this.grpThongTinSuat.Controls.Add(this.lblGiaVe);
            this.grpThongTinSuat.Controls.Add(this.cboDinhDang);
            this.grpThongTinSuat.Controls.Add(this.lblDinhDang);
            this.grpThongTinSuat.Controls.Add(this.cboPhongChieu);
            this.grpThongTinSuat.Controls.Add(this.lblPhongChieu);
            this.grpThongTinSuat.Controls.Add(this.cboPhimSC);
            this.grpThongTinSuat.Controls.Add(this.lblChonPhim);
            this.grpThongTinSuat.Controls.Add(this.txtMaSuat);
            this.grpThongTinSuat.Controls.Add(this.lblMaSuat);
            this.grpThongTinSuat.Dock = DockStyle.Top;
            this.grpThongTinSuat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpThongTinSuat.Location = new Point(0, 0);
            this.grpThongTinSuat.Padding = new Padding(10);
            this.grpThongTinSuat.Size = new Size(1050, 160);
            this.grpThongTinSuat.Text = "THÔNG TIN CHI TIẾT SUẤT CHIẾU";

            // Hàng 1
            this.lblMaSuat.AutoSize = true;
            this.lblMaSuat.Font = new Font("Segoe UI", 9.5F);
            this.lblMaSuat.Location = new Point(20, 35);
            this.lblMaSuat.Text = "Mã Suất Chiếu (*):";

            this.txtMaSuat.Font = new Font("Segoe UI", 10F);
            this.txtMaSuat.Location = new Point(145, 32);
            this.txtMaSuat.Size = new Size(140, 25);

            this.lblChonPhim.AutoSize = true;
            this.lblChonPhim.Font = new Font("Segoe UI", 9.5F);
            this.lblChonPhim.Location = new Point(305, 35);
            this.lblChonPhim.Text = "Bộ Phim (*):";

            this.cboPhimSC.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPhimSC.Font = new Font("Segoe UI", 9.5F);
            this.cboPhimSC.Location = new Point(395, 32);
            this.cboPhimSC.Size = new Size(330, 25);

            this.lblPhongChieu.AutoSize = true;
            this.lblPhongChieu.Font = new Font("Segoe UI", 9.5F);
            this.lblPhongChieu.Location = new Point(750, 35);
            this.lblPhongChieu.Text = "Phòng chiếu:";

            this.cboPhongChieu.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPhongChieu.Font = new Font("Segoe UI", 9.5F);
            this.cboPhongChieu.Items.AddRange(new object[] { "RAP01", "RAP02", "RAP03", "RAP04", "RAP05" });
            this.cboPhongChieu.Location = new Point(855, 32);
            this.cboPhongChieu.Size = new Size(165, 25);

            // Hàng 2
            this.lblDinhDang.AutoSize = true;
            this.lblDinhDang.Font = new Font("Segoe UI", 9.5F);
            this.lblDinhDang.Location = new Point(20, 75);
            this.lblDinhDang.Text = "Định dạng:";

            this.cboDinhDang.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDinhDang.Font = new Font("Segoe UI", 9.5F);
            this.cboDinhDang.Items.AddRange(new object[] { "2D", "3D", "IMAX" });
            this.cboDinhDang.Location = new Point(145, 72);
            this.cboDinhDang.Size = new Size(140, 25);

            this.lblGiaVe.AutoSize = true;
            this.lblGiaVe.Font = new Font("Segoe UI", 9.5F);
            this.lblGiaVe.Location = new Point(305, 75);
            this.lblGiaVe.Text = "Giá vé cơ bản:";

            this.nudGiaVe.Font = new Font("Segoe UI", 10F);
            this.nudGiaVe.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
            this.nudGiaVe.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudGiaVe.Minimum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.nudGiaVe.Value = new decimal(new int[] { 90000, 0, 0, 0 });
            this.nudGiaVe.Location = new Point(395, 72);
            this.nudGiaVe.Size = new Size(150, 25);

            this.lblTrangThaiSC.AutoSize = true;
            this.lblTrangThaiSC.Font = new Font("Segoe UI", 9.5F);
            this.lblTrangThaiSC.Location = new Point(570, 75);
            this.lblTrangThaiSC.Text = "Trạng thái suất:";

            this.cboTrangThaiSC.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboTrangThaiSC.Font = new Font("Segoe UI", 9.5F);
            this.cboTrangThaiSC.Items.AddRange(new object[] { "Mở bán", "Đóng bán", "Đã hủy" });
            this.cboTrangThaiSC.Location = new Point(685, 72);
            this.cboTrangThaiSC.Size = new Size(335, 25);

            // Hàng 3
            this.lblThoiGianBatDau.AutoSize = true;
            this.lblThoiGianBatDau.Font = new Font("Segoe UI", 9.5F);
            this.lblThoiGianBatDau.Location = new Point(20, 115);
            this.lblThoiGianBatDau.Text = "Bắt đầu chiếu:";

            this.dtpBatDau.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpBatDau.Font = new Font("Segoe UI", 9.5F);
            this.dtpBatDau.Format = DateTimePickerFormat.Custom;
            this.dtpBatDau.Location = new Point(145, 112);
            this.dtpBatDau.Size = new Size(180, 25);

            this.lblThoiGianKetThuc.AutoSize = true;
            this.lblThoiGianKetThuc.Font = new Font("Segoe UI", 9.5F);
            this.lblThoiGianKetThuc.Location = new Point(350, 115);
            this.lblThoiGianKetThuc.Text = "Kết thúc dự kiến:";

            this.dtpKetThuc.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpKetThuc.Font = new Font("Segoe UI", 9.5F);
            this.dtpKetThuc.Format = DateTimePickerFormat.Custom;
            this.dtpKetThuc.Location = new Point(475, 112);
            this.dtpKetThuc.Size = new Size(180, 25);

            // pnlActionsSC
            this.pnlActionsSC.BackColor = Color.FromArgb(245, 247, 250);
            this.pnlActionsSC.Controls.Add(this.txtTimKiemSC);
            this.pnlActionsSC.Controls.Add(this.lblTimKiemSC);
            this.pnlActionsSC.Controls.Add(this.btnLamMoiSC);
            this.pnlActionsSC.Controls.Add(this.btnHuySC);
            this.pnlActionsSC.Controls.Add(this.btnSuaSC);
            this.pnlActionsSC.Controls.Add(this.btnThemSC);
            this.pnlActionsSC.Dock = DockStyle.Top;
            this.pnlActionsSC.Height = 55;

            CreateActionButton(this.btnThemSC, "➕ Thêm Suất", Color.FromArgb(40, 167, 69), 20);
            CreateActionButton(this.btnSuaSC, "✏️ Cập Nhật", Color.FromArgb(0, 123, 255), 150);
            CreateActionButton(this.btnHuySC, "🗑️ Hủy Suất", Color.FromArgb(220, 53, 69), 280);
            CreateActionButton(this.btnLamMoiSC, "🔄 Làm Mới", Color.FromArgb(108, 117, 125), 410);

            this.btnThemSC.Click += new System.EventHandler(this.btnThemSC_Click);
            this.btnSuaSC.Click += new System.EventHandler(this.btnSuaSC_Click);
            this.btnHuySC.Click += new System.EventHandler(this.btnHuySC_Click);
            this.btnLamMoiSC.Click += new System.EventHandler(this.btnLamMoiSC_Click);

            this.lblTimKiemSC.AutoSize = true;
            this.lblTimKiemSC.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblTimKiemSC.Location = new Point(590, 18);
            this.lblTimKiemSC.Text = "🔍 Tìm kiếm phòng/phim:";

            this.txtTimKiemSC.Font = new Font("Segoe UI", 10F);
            this.txtTimKiemSC.Location = new Point(770, 14);
            this.txtTimKiemSC.Size = new Size(250, 25);
            this.txtTimKiemSC.TextChanged += new System.EventHandler(this.txtTimKiemSC_TextChanged);

            // dgvSuatChieu
            this.dgvSuatChieu.AllowUserToAddRows = false;
            this.dgvSuatChieu.AllowUserToDeleteRows = false;
            this.dgvSuatChieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuatChieu.BackgroundColor = Color.White;
            this.dgvSuatChieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSuatChieu.Dock = DockStyle.Fill;
            this.dgvSuatChieu.MultiSelect = false;
            this.dgvSuatChieu.ReadOnly = true;
            this.dgvSuatChieu.RowHeadersVisible = false;
            this.dgvSuatChieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuatChieu.CellClick += new DataGridViewCellEventHandler(this.dgvSuatChieu_CellClick);

            // FrmQuanLySuatChieu
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(1050, 630);
            this.Controls.Add(this.dgvSuatChieu);
            this.Controls.Add(this.pnlActionsSC);
            this.Controls.Add(this.grpThongTinSuat);
            this.Text = "Quản Lý Suất Chiếu & Lịch Chiếu";
            this.Load += new System.EventHandler(this.FrmQuanLySuatChieu_Load);

            this.grpThongTinSuat.ResumeLayout(false);
            this.grpThongTinSuat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiaVe)).EndInit();
            this.pnlActionsSC.ResumeLayout(false);
            this.pnlActionsSC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuatChieu)).EndInit();
            this.ResumeLayout(false);
        }

        private void CreateActionButton(Button btn, string text, Color backColor, int leftX)
        {
            btn.BackColor = backColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.Location = new Point(leftX, 10);
            btn.Size = new Size(120, 35);
            btn.Text = text;
            btn.UseVisualStyleBackColor = false;
        }
    }
}