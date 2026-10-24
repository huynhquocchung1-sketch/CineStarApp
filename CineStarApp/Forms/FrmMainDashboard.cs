using System;
using System.Drawing;
using System.Windows.Forms;

namespace CineStarApp
{
    public partial class FrmMainDashboard : Form
    {
        public FrmMainDashboard()
        {
            InitializeComponent();
        }

        private void FrmMainDashboard_Load(object sender, EventArgs e)
        {
            // 1. Hiển thị thông tin nhân viên và trạng thái kết nối
            lblUserInfo.Text = $"Xin chào: {AppSession.HoTen} (Mã: {AppSession.MaNV} | Quyền: {AppSession.Role})";
            lblStatusConnection.Text = $"● Kết nối: {AppSession.DatabaseName} (SQL Login: Role_{AppSession.Role})";
            lblStatusServer.Text = $"   |   Máy chủ: {AppSession.ServerName}";
            lblStatusTime.Text = $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

            // 2. Logic Phân quyền (RBAC): Nếu là "User", vô hiệu hóa các phân hệ quản trị
            if (string.Equals(AppSession.Role, "User", StringComparison.OrdinalIgnoreCase))
            {
                btnQuanLyPhim.Enabled = false;
                btnQuanLySuatChieu.Enabled = false;
                btnThongKe.Enabled = false;

                // Đổi màu trực quan cho các nút bị khóa
                btnQuanLyPhim.BackColor = Color.FromArgb(60, 65, 75);
                btnQuanLySuatChieu.BackColor = Color.FromArgb(60, 65, 75);
                btnThongKe.BackColor = Color.FromArgb(60, 65, 75);
            }

            // 3. Mặc định mở sẵn màn hình Giao dịch Bán Vé (POS) khi vào hệ thống
            OpenMdiChildForm<FrmGiaoDichBanVe>();
        }

        /// <summary>
        /// Hàm Generic mở hoặc kích hoạt Form con bên trong MDI Container (tránh mở trùng nhiều cửa sổ)
        /// </summary>
        private void OpenMdiChildForm<T>() where T : Form, new()
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is T)
                {
                    child.Activate();
                    child.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            T newChild = new T
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            newChild.Show();
        }

        private void btnBanVePOS_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm<FrmGiaoDichBanVe>();
        }

        private void btnQuanLyPhim_Click(object sender, EventArgs e)
        {
            if (string.Equals(AppSession.Role, "User", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Tài khoản nhân viên bán vé không có quyền truy cập Danh mục Phim!",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenMdiChildForm<FrmQuanLyPhim>();
        }

        private void btnQuanLySuatChieu_Click(object sender, EventArgs e)
        {
            if (string.Equals(AppSession.Role, "User", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Tài khoản nhân viên bán vé không có quyền quản lý Suất Chiếu!",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenMdiChildForm<FrmQuanLySuatChieu>();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            // Nhân viên bán vé và Admin đều có quyền quản lý/tra cứu khách hàng tại quầy
            OpenMdiChildForm<FrmQuanLyKhachHang>();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (string.Equals(AppSession.Role, "User", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Tài khoản nhân viên bán vé không có quyền xem Báo cáo Thống kê Doanh thu!",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenMdiChildForm<FrmThongKeDoanhThu>();
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lblStatusTime.Text = $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất khỏi phiên làm việc hiện tại?",
                "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AppSession.ClearSession();
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }
    }
}