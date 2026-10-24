using System.Drawing;
using System.Windows.Forms;

namespace CineStarApp
{
    partial class FrmGiaoDichBanVe
    {
        private System.ComponentModel.IContainer components = null;

        private SplitContainer splitTop;
        private GroupBox grpMaster;
        private Label lblTitleMaVe;
        private TextBox txtMaVe;
        private Label lblTitleKH;
        private ComboBox cboKhachHang;
        private Label lblTitlePhim;
        private ComboBox cboPhim;
        private Label lblTitleSuat;
        private ComboBox cboSuatChieu;
        private Label lblTitlePhong;
        private Label lblPhong;
        private Label lblTitleGia;
        private Label lblGia;
        private Label lblTitleKM;
        private ComboBox cboKhuyenMai;
        private Label lblTitlePTTT;
        private ComboBox cboPhuongThucTT;

        private GroupBox grpSoDoGhe;
        private Label lblManHinhChieu;
        private FlowLayoutPanel flpSoDoGhe;
        private Panel pnlChuThich;
        private Label lblLegendTrong;
        private Label lblLegendDangChon;
        private Label lblLegendDaBan;

        private GroupBox grpSubForm;
        private DataGridView dgvChiTietGhe;
        private Panel pnlBottomSummary;
        private Label lblTongTienGoc;
        private Label lblTienGiamGia;
        private Label lblThanhTien;
        private Button btnLuuGiaoDich;
        private Button btnHuyLamMoi;

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
            this.splitTop = new SplitContainer();
            this.grpMaster = new GroupBox();
            this.lblTitleMaVe = new Label();
            this.txtMaVe = new TextBox();
            this.lblTitleKH = new Label();
            this.cboKhachHang = new ComboBox();
            this.lblTitlePhim = new Label();
            this.cboPhim = new ComboBox();
            this.lblTitleSuat = new Label();
            this.cboSuatChieu = new ComboBox();
            this.lblTitlePhong = new Label();
            this.lblPhong = new Label();
            this.lblTitleGia = new Label();
            this.lblGia = new Label();
            this.lblTitleKM = new Label();
            this.cboKhuyenMai = new ComboBox();
            this.lblTitlePTTT = new Label();
            this.cboPhuongThucTT = new ComboBox();

            this.grpSoDoGhe = new GroupBox();
            this.lblManHinhChieu = new Label();
            this.flpSoDoGhe = new FlowLayoutPanel();
            this.pnlChuThich = new Panel();
            this.lblLegendTrong = new Label();
            this.lblLegendDangChon = new Label();
            this.lblLegendDaBan = new Label();

            this.grpSubForm = new GroupBox();
            this.dgvChiTietGhe = new DataGridView();
            this.pnlBottomSummary = new Panel();
            this.lblTongTienGoc = new Label();
            this.lblTienGiamGia = new Label();
            this.lblThanhTien = new Label();
            this.btnLuuGiaoDich = new Button();
            this.btnHuyLamMoi = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitTop)).BeginInit();
            this.splitTop.Panel1.SuspendLayout();
            this.splitTop.Panel2.SuspendLayout();
            this.splitTop.SuspendLayout();
            this.grpMaster.SuspendLayout();
            this.grpSoDoGhe.SuspendLayout();
            this.pnlChuThich.SuspendLayout();
            this.grpSubForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietGhe)).BeginInit();
            this.pnlBottomSummary.SuspendLayout();
            this.SuspendLayout();

            // splitTop
            this.splitTop.Dock = DockStyle.Top;
            this.splitTop.Location = new Point(0, 0);
            this.splitTop.Size = new Size(1040, 360);
            this.splitTop.SplitterDistance = 420;

            // grpMaster
            this.grpMaster.Controls.Add(this.cboPhuongThucTT);
            this.grpMaster.Controls.Add(this.lblTitlePTTT);
            this.grpMaster.Controls.Add(this.cboKhuyenMai);
            this.grpMaster.Controls.Add(this.lblTitleKM);
            this.grpMaster.Controls.Add(this.lblGia);
            this.grpMaster.Controls.Add(this.lblTitleGia);
            this.grpMaster.Controls.Add(this.lblPhong);
            this.grpMaster.Controls.Add(this.lblTitlePhong);
            this.grpMaster.Controls.Add(this.cboSuatChieu);
            this.grpMaster.Controls.Add(this.lblTitleSuat);
            this.grpMaster.Controls.Add(this.cboPhim);
            this.grpMaster.Controls.Add(this.lblTitlePhim);
            this.grpMaster.Controls.Add(this.cboKhachHang);
            this.grpMaster.Controls.Add(this.lblTitleKH);
            this.grpMaster.Controls.Add(this.txtMaVe);
            this.grpMaster.Controls.Add(this.lblTitleMaVe);
            this.grpMaster.Dock = DockStyle.Fill;
            this.grpMaster.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpMaster.Padding = new Padding(10);
            this.grpMaster.Text = "1. THÔNG TIN ĐƠN VÉ (MASTER)";

            this.lblTitleMaVe.AutoSize = true;
            this.lblTitleMaVe.Font = new Font("Segoe UI", 9.5F);
            this.lblTitleMaVe.Location = new Point(15, 32);
            this.lblTitleMaVe.Text = "Mã Giao Dịch:";

            this.txtMaVe.BackColor = Color.FromArgb(235, 238, 242);
            this.txtMaVe.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.txtMaVe.Location = new Point(135, 29);
            this.txtMaVe.ReadOnly = true;
            this.txtMaVe.Size = new Size(265, 24);

            this.lblTitleKH.AutoSize = true;
            this.lblTitleKH.Font = new Font("Segoe UI", 9.5F);
            this.lblTitleKH.Location = new Point(15, 72);
            this.lblTitleKH.Text = "Khách Hàng:";

            this.cboKhachHang.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboKhachHang.Font = new Font("Segoe UI", 9.5F);
            this.cboKhachHang.Location = new Point(135, 69);
            this.cboKhachHang.Size = new Size(265, 24);

            this.lblTitlePhim.AutoSize = true;
            this.lblTitlePhim.Font = new Font("Segoe UI", 9.5F);
            this.lblTitlePhim.Location = new Point(15, 112);
            this.lblTitlePhim.Text = "Chọn Phim:";

            this.cboPhim.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPhim.Font = new Font("Segoe UI", 9.5F);
            this.cboPhim.Location = new Point(135, 109);
            this.cboPhim.Size = new Size(265, 24);
            this.cboPhim.SelectedIndexChanged += new System.EventHandler(this.cboPhim_SelectedIndexChanged);

            this.lblTitleSuat.AutoSize = true;
            this.lblTitleSuat.Font = new Font("Segoe UI", 9.5F);
            this.lblTitleSuat.Location = new Point(15, 152);
            this.lblTitleSuat.Text = "Suất Chiếu:";

            this.cboSuatChieu.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboSuatChieu.Font = new Font("Segoe UI", 9.5F);
            this.cboSuatChieu.Location = new Point(135, 149);
            this.cboSuatChieu.Size = new Size(265, 24);
            this.cboSuatChieu.SelectedIndexChanged += new System.EventHandler(this.cboSuatChieu_SelectedIndexChanged);

            this.lblTitlePhong.AutoSize = true;
            this.lblTitlePhong.Font = new Font("Segoe UI", 9.5F);
            this.lblTitlePhong.Location = new Point(15, 195);
            this.lblTitlePhong.Text = "Phòng Chiếu:";

            this.lblPhong.AutoSize = true;
            this.lblPhong.ForeColor = Color.FromArgb(0, 102, 204);
            this.lblPhong.Location = new Point(135, 195);
            this.lblPhong.Text = "---";

            this.lblTitleGia.AutoSize = true;
            this.lblTitleGia.Font = new Font("Segoe UI", 9.5F);
            this.lblTitleGia.Location = new Point(220, 195);
            this.lblTitleGia.Text = "Giá gốc:";

            this.lblGia.AutoSize = true;
            this.lblGia.ForeColor = Color.FromArgb(211, 47, 47);
            this.lblGia.Location = new Point(285, 195);
            this.lblGia.Text = "0 VNĐ";

            this.lblTitleKM.AutoSize = true;
            this.lblTitleKM.Font = new Font("Segoe UI", 9.5F);
            this.lblTitleKM.Location = new Point(15, 235);
            this.lblTitleKM.Text = "Khuyến Mãi:";

            this.cboKhuyenMai.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboKhuyenMai.Font = new Font("Segoe UI", 9.5F);
            this.cboKhuyenMai.Location = new Point(135, 232);
            this.cboKhuyenMai.Size = new Size(265, 24);
            this.cboKhuyenMai.SelectedIndexChanged += new System.EventHandler(this.cboKhuyenMai_SelectedIndexChanged);

            this.lblTitlePTTT.AutoSize = true;
            this.lblTitlePTTT.Font = new Font("Segoe UI", 9.5F);
            this.lblTitlePTTT.Location = new Point(15, 275);
            this.lblTitlePTTT.Text = "Thanh Toán:";

            this.cboPhuongThucTT.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPhuongThucTT.Font = new Font("Segoe UI", 9.5F);
            this.cboPhuongThucTT.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ tín dụng", "Ví điện tử" });
            this.cboPhuongThucTT.Location = new Point(135, 272);
            this.cboPhuongThucTT.Size = new Size(265, 24);

            this.splitTop.Panel1.Controls.Add(this.grpMaster);

            // grpSoDoGhe
            this.grpSoDoGhe.Controls.Add(this.flpSoDoGhe);
            this.grpSoDoGhe.Controls.Add(this.pnlChuThich);
            this.grpSoDoGhe.Controls.Add(this.lblManHinhChieu);
            this.grpSoDoGhe.Dock = DockStyle.Fill;
            this.grpSoDoGhe.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpSoDoGhe.Padding = new Padding(10);
            this.grpSoDoGhe.Text = "2. SƠ ĐỒ CHỌN GHẾ TRỰC QUAN THEO SUẤT CHIẾU";

            this.lblManHinhChieu.BackColor = Color.FromArgb(45, 52, 70);
            this.lblManHinhChieu.Dock = DockStyle.Top;
            this.lblManHinhChieu.ForeColor = Color.Gold;
            this.lblManHinhChieu.Size = new Size(596, 28);
            this.lblManHinhChieu.Text = "================ MÀN HÌNH CHIẾU (SCREEN) ================";
            this.lblManHinhChieu.TextAlign = ContentAlignment.MiddleCenter;

            this.flpSoDoGhe.AutoScroll = true;
            this.flpSoDoGhe.BackColor = Color.FromArgb(248, 249, 250);
            this.flpSoDoGhe.Dock = DockStyle.Fill;
            this.flpSoDoGhe.Padding = new Padding(12);

            this.pnlChuThich.BackColor = Color.WhiteSmoke;
            this.pnlChuThich.Controls.Add(this.lblLegendDaBan);
            this.pnlChuThich.Controls.Add(this.lblLegendDangChon);
            this.pnlChuThich.Controls.Add(this.lblLegendTrong);
            this.pnlChuThich.Dock = DockStyle.Bottom;
            this.pnlChuThich.Size = new Size(596, 32);

            this.lblLegendTrong.AutoSize = true;
            this.lblLegendTrong.Font = new Font("Segoe UI", 9F);
            this.lblLegendTrong.Location = new Point(15, 8);
            this.lblLegendTrong.Text = "⬜ Ghế Trống";

            this.lblLegendDangChon.AutoSize = true;
            this.lblLegendDangChon.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblLegendDangChon.ForeColor = Color.SeaGreen;
            this.lblLegendDangChon.Location = new Point(160, 8);
            this.lblLegendDangChon.Text = "🟩 Đang Chọn";

            this.lblLegendDaBan.AutoSize = true;
            this.lblLegendDaBan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblLegendDaBan.ForeColor = Color.Crimson;
            this.lblLegendDaBan.Location = new Point(310, 8);
            this.lblLegendDaBan.Text = "🟥 Đã Bán / Bảo Trì";

            this.splitTop.Panel2.Controls.Add(this.grpSoDoGhe);

            // grpSubForm
            this.grpSubForm.Controls.Add(this.dgvChiTietGhe);
            this.grpSubForm.Controls.Add(this.pnlBottomSummary);
            this.grpSubForm.Dock = DockStyle.Fill;
            this.grpSubForm.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpSubForm.Location = new Point(0, 360);
            this.grpSubForm.Padding = new Padding(10);
            this.grpSubForm.Size = new Size(1040, 290);
            this.grpSubForm.Text = "3. DANH SÁCH GHẾ CHI TIẾT ĐANG ĐẶT (SUB-FORM - CHITIETVE)";

            this.dgvChiTietGhe.AllowUserToAddRows = false;
            this.dgvChiTietGhe.AllowUserToDeleteRows = false;
            this.dgvChiTietGhe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietGhe.BackgroundColor = Color.White;
            this.dgvChiTietGhe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietGhe.Dock = DockStyle.Fill;
            this.dgvChiTietGhe.ReadOnly = true;
            this.dgvChiTietGhe.RowHeadersVisible = false;
            this.dgvChiTietGhe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.pnlBottomSummary.BackColor = Color.FromArgb(240, 244, 248);
            this.pnlBottomSummary.Controls.Add(this.btnHuyLamMoi);
            this.pnlBottomSummary.Controls.Add(this.btnLuuGiaoDich);
            this.pnlBottomSummary.Controls.Add(this.lblThanhTien);
            this.pnlBottomSummary.Controls.Add(this.lblTienGiamGia);
            this.pnlBottomSummary.Controls.Add(this.lblTongTienGoc);
            this.pnlBottomSummary.Dock = DockStyle.Bottom;
            this.pnlBottomSummary.Size = new Size(1020, 60);

            this.lblTongTienGoc.AutoSize = true;
            this.lblTongTienGoc.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblTongTienGoc.Location = new Point(15, 20);
            this.lblTongTienGoc.Text = "Tổng gốc: 0 VNĐ";

            this.lblTienGiamGia.AutoSize = true;
            this.lblTienGiamGia.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblTienGiamGia.ForeColor = Color.FromArgb(0, 102, 204);
            this.lblTienGiamGia.Location = new Point(205, 20);
            this.lblTienGiamGia.Text = "Giảm giá: 0 VNĐ";

            this.lblThanhTien.AutoSize = true;
            this.lblThanhTien.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            this.lblThanhTien.ForeColor = Color.FromArgb(211, 47, 47);
            this.lblThanhTien.Location = new Point(390, 17);
            this.lblThanhTien.Text = "THÀNH TIỀN: 0 VNĐ";

            this.btnLuuGiaoDich.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnLuuGiaoDich.BackColor = Color.FromArgb(40, 167, 69);
            this.btnLuuGiaoDich.FlatStyle = FlatStyle.Flat;
            this.btnLuuGiaoDich.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLuuGiaoDich.ForeColor = Color.White;
            this.btnLuuGiaoDich.Location = new Point(685, 10);
            this.btnLuuGiaoDich.Size = new Size(185, 40);
            this.btnLuuGiaoDich.Text = "💾 LƯU GIAO DỊCH VÉ";
            this.btnLuuGiaoDich.UseVisualStyleBackColor = false;
            this.btnLuuGiaoDich.Click += new System.EventHandler(this.btnLuuGiaoDich_Click);

            this.btnHuyLamMoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnHuyLamMoi.BackColor = Color.FromArgb(108, 117, 125);
            this.btnHuyLamMoi.FlatStyle = FlatStyle.Flat;
            this.btnHuyLamMoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnHuyLamMoi.ForeColor = Color.White;
            this.btnHuyLamMoi.Location = new Point(880, 10);
            this.btnHuyLamMoi.Size = new Size(130, 40);
            this.btnHuyLamMoi.Text = "❌ Làm Mới";
            this.btnHuyLamMoi.UseVisualStyleBackColor = false;
            this.btnHuyLamMoi.Click += new System.EventHandler(this.btnHuyLamMoi_Click);

            // FrmGiaoDichBanVe
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(1040, 650);
            this.Controls.Add(this.grpSubForm);
            this.Controls.Add(this.splitTop);
            this.Text = "Giao Dịch Bán Vé Xem Phim (Main - Sub Form)";
            this.Load += new System.EventHandler(this.FrmGiaoDichBanVe_Load);

            this.splitTop.Panel1.ResumeLayout(false);
            this.splitTop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTop)).EndInit();
            this.splitTop.ResumeLayout(false);
            this.grpMaster.ResumeLayout(false);
            this.grpMaster.PerformLayout();
            this.grpSoDoGhe.ResumeLayout(false);
            this.pnlChuThich.ResumeLayout(false);
            this.pnlChuThich.PerformLayout();
            this.grpSubForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietGhe)).EndInit();
            this.pnlBottomSummary.ResumeLayout(false);
            this.pnlBottomSummary.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}