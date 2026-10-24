using System.Drawing;
using System.Windows.Forms;

namespace CineStarApp
{
    partial class FrmQuanLyKhachHang
    {
        private System.ComponentModel.IContainer? components = null;

        private GroupBox grpInfoKH = null!;
        private Label lblMaKH = null!;
        private TextBox txtMaKH = null!;
        private Label lblHoTenKH = null!;
        private TextBox txtHoTenKH = null!;
        private Label lblSDT = null!;
        private TextBox txtSDT = null!;
        private Label lblEmailKH = null!;
        private TextBox txtEmailKH = null!;
        private Label lblNgaySinhKH = null!;
        private DateTimePicker dtpNgaySinhKH = null!;
        private Label lblDiem = null!;
        private NumericUpDown nudDiem = null!;
        private Label lblHangThe = null!;
        private ComboBox cboHangThe = null!;

        private Panel pnlActionsKH = null!;
        private Button btnSuaKH = null!;
        private Button btnXoaKH = null!;
        private Button btnLamMoiKH = null!;
        private Label lblTimKiemKH = null!;
        private TextBox txtTimKiemKH = null!;
        private DataGridView dgvKhachHang = null!;

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
            this.grpInfoKH = new GroupBox();
            this.lblMaKH = new Label();
            this.txtMaKH = new TextBox();
            this.lblHoTenKH = new Label();
            this.txtHoTenKH = new TextBox();
            this.lblSDT = new Label();
            this.txtSDT = new TextBox();
            this.lblEmailKH = new Label();
            this.txtEmailKH = new TextBox();
            this.lblNgaySinhKH = new Label();
            this.dtpNgaySinhKH = new DateTimePicker();
            this.lblDiem = new Label();
            this.nudDiem = new NumericUpDown();
            this.lblHangThe = new Label();
            this.cboHangThe = new ComboBox();

            this.pnlActionsKH = new Panel();
            this.btnSuaKH = new Button();
            this.btnXoaKH = new Button();
            this.btnLamMoiKH = new Button();
            this.lblTimKiemKH = new Label();
            this.txtTimKiemKH = new TextBox();
            this.dgvKhachHang = new DataGridView();

            this.grpInfoKH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiem)).BeginInit();
            this.pnlActionsKH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            this.SuspendLayout();

            // grpInfoKH
            this.grpInfoKH.Controls.Add(this.cboHangThe);
            this.grpInfoKH.Controls.Add(this.lblHangThe);
            this.grpInfoKH.Controls.Add(this.nudDiem);
            this.grpInfoKH.Controls.Add(this.lblDiem);
            this.grpInfoKH.Controls.Add(this.dtpNgaySinhKH);
            this.grpInfoKH.Controls.Add(this.lblNgaySinhKH);
            this.grpInfoKH.Controls.Add(this.txtEmailKH);
            this.grpInfoKH.Controls.Add(this.lblEmailKH);
            this.grpInfoKH.Controls.Add(this.txtSDT);
            this.grpInfoKH.Controls.Add(this.lblSDT);
            this.grpInfoKH.Controls.Add(this.txtHoTenKH);
            this.grpInfoKH.Controls.Add(this.lblHoTenKH);
            this.grpInfoKH.Controls.Add(this.txtMaKH);
            this.grpInfoKH.Controls.Add(this.lblMaKH);
            this.grpInfoKH.Dock = DockStyle.Top;
            this.grpInfoKH.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpInfoKH.Location = new Point(0, 0);
            this.grpInfoKH.Padding = new Padding(10);
            this.grpInfoKH.Size = new Size(1050, 125);
            this.grpInfoKH.Text = "QUẢN LÝ THÔNG TIN KHÁCH HÀNG THÀNH VIÊN";

            // Hàng 1
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Font = new Font("Segoe UI", 9.5F);
            this.lblMaKH.Location = new Point(20, 35);
            this.lblMaKH.Text = "Mã KH:";

            this.txtMaKH.BackColor = Color.FromArgb(235, 238, 242);
            this.txtMaKH.Font = new Font("Segoe UI", 10F);
            this.txtMaKH.Location = new Point(95, 32);
            this.txtMaKH.ReadOnly = true;
            this.txtMaKH.Size = new Size(110, 25);

            this.lblHoTenKH.AutoSize = true;
            this.lblHoTenKH.Font = new Font("Segoe UI", 9.5F);
            this.lblHoTenKH.Location = new Point(230, 35);
            this.lblHoTenKH.Text = "Họ và tên (*):";

            this.txtHoTenKH.Font = new Font("Segoe UI", 10F);
            this.txtHoTenKH.Location = new Point(335, 32);
            this.txtHoTenKH.Size = new Size(240, 25);

            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new Font("Segoe UI", 9.5F);
            this.lblSDT.Location = new Point(600, 35);
            this.lblSDT.Text = "Số điện thoại (*):";

            this.txtSDT.Font = new Font("Segoe UI", 10F);
            this.txtSDT.Location = new Point(725, 32);
            this.txtSDT.Size = new Size(290, 25);

            // Hàng 2
            this.lblEmailKH.AutoSize = true;
            this.lblEmailKH.Font = new Font("Segoe UI", 9.5F);
            this.lblEmailKH.Location = new Point(20, 78);
            this.lblEmailKH.Text = "Email:";

            this.txtEmailKH.Font = new Font("Segoe UI", 10F);
            this.txtEmailKH.Location = new Point(95, 75);
            this.txtEmailKH.Size = new Size(220, 25);

            this.lblNgaySinhKH.AutoSize = true;
            this.lblNgaySinhKH.Font = new Font("Segoe UI", 9.5F);
            this.lblNgaySinhKH.Location = new Point(335, 78);
            this.lblNgaySinhKH.Text = "Ngày sinh:";

            this.dtpNgaySinhKH.Font = new Font("Segoe UI", 9.5F);
            this.dtpNgaySinhKH.Format = DateTimePickerFormat.Short;
            this.dtpNgaySinhKH.Location = new Point(415, 75);
            this.dtpNgaySinhKH.Size = new Size(160, 24);

            this.lblDiem.AutoSize = true;
            this.lblDiem.Font = new Font("Segoe UI", 9.5F);
            this.lblDiem.Location = new Point(600, 78);
            this.lblDiem.Text = "Điểm tích lũy:";

            this.nudDiem.Font = new Font("Segoe UI", 10F);
            this.nudDiem.Location = new Point(700, 75);
            this.nudDiem.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.nudDiem.Size = new Size(100, 25);

            this.lblHangThe.AutoSize = true;
            this.lblHangThe.Font = new Font("Segoe UI", 9.5F);
            this.lblHangThe.Location = new Point(820, 78);
            this.lblHangThe.Text = "Hạng thẻ:";

            this.cboHangThe.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboHangThe.Font = new Font("Segoe UI", 9.5F);
            this.cboHangThe.Items.AddRange(new object[] { "Member", "VIP", "VVIP" });
            this.cboHangThe.Location = new Point(895, 75);
            this.cboHangThe.Size = new Size(120, 24);

            // pnlActionsKH
            this.pnlActionsKH.BackColor = Color.FromArgb(245, 247, 250);
            this.pnlActionsKH.Controls.Add(this.txtTimKiemKH);
            this.pnlActionsKH.Controls.Add(this.lblTimKiemKH);
            this.pnlActionsKH.Controls.Add(this.btnLamMoiKH);
            this.pnlActionsKH.Controls.Add(this.btnXoaKH);
            this.pnlActionsKH.Controls.Add(this.btnSuaKH);
            this.pnlActionsKH.Dock = DockStyle.Top;
            this.pnlActionsKH.Height = 55;

            CreateActionButton(this.btnSuaKH, "✏️ Cập Nhật KH", Color.FromArgb(0, 123, 255), 20);
            CreateActionButton(this.btnXoaKH, "🗑️ Xóa Khách Hàng", Color.FromArgb(220, 53, 69), 160);
            CreateActionButton(this.btnLamMoiKH, "🔄 Làm Mới", Color.FromArgb(108, 117, 125), 305);

            this.btnSuaKH.Click += new System.EventHandler(this.btnSuaKH_Click);
            this.btnXoaKH.Click += new System.EventHandler(this.btnXoaKH_Click);
            this.btnLamMoiKH.Click += new System.EventHandler(this.btnLamMoiKH_Click);

            this.lblTimKiemKH.AutoSize = true;
            this.lblTimKiemKH.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblTimKiemKH.Location = new Point(590, 18);
            this.lblTimKiemKH.Text = "🔍 Tìm kiếm tên/SĐT:";

            this.txtTimKiemKH.Font = new Font("Segoe UI", 10F);
            this.txtTimKiemKH.Location = new Point(755, 14);
            this.txtTimKiemKH.Size = new Size(260, 25);
            this.txtTimKiemKH.TextChanged += new System.EventHandler(this.txtTimKiemKH_TextChanged);

            // dgvKhachHang
            this.dgvKhachHang.AllowUserToAddRows = false;
            this.dgvKhachHang.AllowUserToDeleteRows = false;
            this.dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhachHang.BackgroundColor = Color.White;
            this.dgvKhachHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhachHang.Dock = DockStyle.Fill;
            this.dgvKhachHang.MultiSelect = false;
            this.dgvKhachHang.ReadOnly = true;
            this.dgvKhachHang.RowHeadersVisible = false;
            this.dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhachHang.CellClick += new DataGridViewCellEventHandler(this.dgvKhachHang_CellClick);

            // FrmQuanLyKhachHang
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(1050, 630);
            this.Controls.Add(this.dgvKhachHang);
            this.Controls.Add(this.pnlActionsKH);
            this.Controls.Add(this.grpInfoKH);
            this.Text = "Quản Lý Khách Hàng Thành Viên";
            this.Load += new System.EventHandler(this.FrmQuanLyKhachHang_Load);

            this.grpInfoKH.ResumeLayout(false);
            this.grpInfoKH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiem)).EndInit();
            this.pnlActionsKH.ResumeLayout(false);
            this.pnlActionsKH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            this.ResumeLayout(false);
        }

        private void CreateActionButton(Button btn, string text, Color backColor, int leftX)
        {
            btn.BackColor = backColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.Location = new Point(leftX, 10);
            btn.Size = new Size(130, 35);
            btn.Text = text;
            btn.UseVisualStyleBackColor = false;
        }
    }
}