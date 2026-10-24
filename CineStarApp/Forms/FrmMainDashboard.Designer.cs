using System.Drawing;
using System.Windows.Forms;

namespace CineStarApp
{
    partial class FrmMainDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblSystemTitle;
        private Label lblUserInfo;
        private Button btnLogout;
        private Panel pnlSidebar;
        private Button btnQuanLyPhim;
        private Button btnQuanLySuatChieu;
        private Button btnKhachHang;
        private Button btnBanVePOS;
        private Button btnThongKe;
        private StatusStrip statusStripMain;
        private ToolStripStatusLabel lblStatusConnection;
        private ToolStripStatusLabel lblStatusServer;
        private ToolStripStatusLabel lblStatusTime;
        private System.Windows.Forms.Timer timerClock;
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
            this.components = new System.ComponentModel.Container();
            this.pnlHeader = new Panel();
            this.lblSystemTitle = new Label();
            this.lblUserInfo = new Label();
            this.btnLogout = new Button();
            this.pnlSidebar = new Panel();
            this.btnQuanLyPhim = new Button();
            this.btnQuanLySuatChieu = new Button();
            this.btnKhachHang = new Button();
            this.btnBanVePOS = new Button();
            this.btnThongKe = new Button();
            this.statusStripMain = new StatusStrip();
            this.lblStatusConnection = new ToolStripStatusLabel();
            this.lblStatusServer = new ToolStripStatusLabel();
            this.lblStatusTime = new ToolStripStatusLabel();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.pnlHeader.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = Color.FromArgb(20, 25, 45);
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Controls.Add(this.lblUserInfo);
            this.pnlHeader.Controls.Add(this.lblSystemTitle);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Size = new Size(1280, 55);

            // lblSystemTitle
            this.lblSystemTitle.AutoSize = true;
            this.lblSystemTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblSystemTitle.ForeColor = Color.Gold;
            this.lblSystemTitle.Location = new Point(15, 13);
            this.lblSystemTitle.Text = "🎬 CINESTAR POS & MANAGEMENT SYSTEM";

            // lblUserInfo
            this.lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblUserInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblUserInfo.ForeColor = Color.White;
            this.lblUserInfo.Location = new Point(750, 17);
            this.lblUserInfo.Size = new Size(380, 25);
            this.lblUserInfo.Text = "Xin chào: ...";
            this.lblUserInfo.TextAlign = ContentAlignment.MiddleRight;

            // btnLogout
            this.btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnLogout.BackColor = Color.FromArgb(220, 53, 69);
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnLogout.ForeColor = Color.White;
            this.btnLogout.Location = new Point(1145, 11);
            this.btnLogout.Size = new Size(115, 33);
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // pnlSidebar
            this.pnlSidebar.BackColor = Color.FromArgb(32, 40, 65);
            this.pnlSidebar.Controls.Add(this.btnThongKe);
            this.pnlSidebar.Controls.Add(this.btnBanVePOS);
            this.pnlSidebar.Controls.Add(this.btnKhachHang);
            this.pnlSidebar.Controls.Add(this.btnQuanLySuatChieu);
            this.pnlSidebar.Controls.Add(this.btnQuanLyPhim);
            this.pnlSidebar.Dock = DockStyle.Left;
            this.pnlSidebar.Size = new Size(230, 680);

            // Cấu hình chung cho các nút trên Sidebar
            ConfigureSidebarButton(this.btnBanVePOS, "🎟️  BÁN VÉ (POS)", 20);
            ConfigureSidebarButton(this.btnQuanLyPhim, "🎬  Quản lý Phim", 80);
            ConfigureSidebarButton(this.btnQuanLySuatChieu, "🕒  Quản lý Suất Chiếu", 140);
            ConfigureSidebarButton(this.btnKhachHang, "👥  Quản lý Khách Hàng", 200);
            ConfigureSidebarButton(this.btnThongKe, "📊  Thống Kê Doanh Thu", 260);

            this.btnBanVePOS.Click += new System.EventHandler(this.btnBanVePOS_Click);
            this.btnQuanLyPhim.Click += new System.EventHandler(this.btnQuanLyPhim_Click);
            this.btnQuanLySuatChieu.Click += new System.EventHandler(this.btnQuanLySuatChieu_Click);
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);

            // statusStripMain
            this.statusStripMain.BackColor = Color.FromArgb(240, 240, 240);
            this.statusStripMain.Items.AddRange(new ToolStripItem[] {
                this.lblStatusConnection,
                this.lblStatusServer,
                this.lblStatusTime
            });
            this.statusStripMain.Location = new Point(230, 710);
            this.statusStripMain.Size = new Size(1050, 25);

            // lblStatusConnection
            this.lblStatusConnection.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblStatusConnection.ForeColor = Color.DarkGreen;
            this.lblStatusConnection.Text = "● Trạng thái: Đã kết nối CineStarDB";
            this.lblStatusConnection.BorderSides = ToolStripStatusLabelBorderSides.Right;

            // lblStatusServer
            this.lblStatusServer.Font = new Font("Segoe UI", 9F);
            this.lblStatusServer.Text = "Máy chủ: .\\SQLEXPRESS";
            this.lblStatusServer.Spring = true;
            this.lblStatusServer.TextAlign = ContentAlignment.MiddleLeft;

            // lblStatusTime
            this.lblStatusTime.Font = new Font("Segoe UI", 9F);
            this.lblStatusTime.Text = "Thời gian: --:--:--";

            // timerClock
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);

            // FrmMainDashboard
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1280, 735);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlHeader);
            this.IsMdiContainer = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "CineStar Management System - Bảng Điều Khiển Chính";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMainDashboard_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSidebar.ResumeLayout(false);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ConfigureSidebarButton(Button btn, string text, int topY)
        {
            btn.BackColor = Color.FromArgb(42, 52, 82);
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.Location = new Point(10, topY);
            btn.Size = new Size(210, 48);
            btn.Text = text;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(12, 0, 0, 0);
            btn.UseVisualStyleBackColor = false;
        }
    }
}