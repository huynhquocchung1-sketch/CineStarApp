using System.Drawing;
using System.Windows.Forms;

namespace CineStarApp
{
    partial class FrmRegister
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblHeader;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblSoDienThoai;
        private TextBox txtSoDienThoai;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblNgaySinh;
        private DateTimePicker dtpNgaySinh;
        private Button btnDangKy;
        private Button btnThoat;

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
            this.lblHeader = new Label();
            this.lblHoTen = new Label();
            this.txtHoTen = new TextBox();
            this.lblSoDienThoai = new Label();
            this.txtSoDienThoai = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblNgaySinh = new Label();
            this.dtpNgaySinh = new DateTimePicker();
            this.btnDangKy = new Button();
            this.btnThoat = new Button();
            this.SuspendLayout();

            // lblHeader
            this.lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblHeader.ForeColor = Color.FromArgb(24, 30, 54);
            this.lblHeader.Location = new Point(30, 20);
            this.lblHeader.Size = new Size(390, 35);
            this.lblHeader.Text = "ĐĂNG KÝ TÀI KHOẢN THÀNH VIÊN";
            this.lblHeader.TextAlign = ContentAlignment.MiddleCenter;

            // lblHoTen
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblHoTen.Location = new Point(30, 75);
            this.lblHoTen.Text = "Họ và tên (*):";

            // txtHoTen
            this.txtHoTen.Font = new Font("Segoe UI", 10F);
            this.txtHoTen.Location = new Point(160, 72);
            this.txtHoTen.Size = new Size(260, 25);
            this.txtHoTen.TabIndex = 0;

            // lblSoDienThoai
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblSoDienThoai.Location = new Point(30, 120);
            this.lblSoDienThoai.Text = "Số điện thoại (*):";

            // txtSoDienThoai
            this.txtSoDienThoai.Font = new Font("Segoe UI", 10F);
            this.txtSoDienThoai.Location = new Point(160, 117);
            this.txtSoDienThoai.Size = new Size(260, 25);
            this.txtSoDienThoai.TabIndex = 1;

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblEmail.Location = new Point(30, 165);
            this.lblEmail.Text = "Địa chỉ Email:";

            // txtEmail
            this.txtEmail.Font = new Font("Segoe UI", 10F);
            this.txtEmail.Location = new Point(160, 162);
            this.txtEmail.Size = new Size(260, 25);
            this.txtEmail.TabIndex = 2;

            // lblNgaySinh
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblNgaySinh.Location = new Point(30, 210);
            this.lblNgaySinh.Text = "Ngày sinh:";

            // dtpNgaySinh
            this.dtpNgaySinh.Font = new Font("Segoe UI", 10F);
            this.dtpNgaySinh.Format = DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new Point(160, 207);
            this.dtpNgaySinh.Size = new Size(260, 25);
            this.dtpNgaySinh.TabIndex = 3;

            // btnDangKy
            this.btnDangKy.BackColor = Color.FromArgb(40, 167, 69);
            this.btnDangKy.FlatStyle = FlatStyle.Flat;
            this.btnDangKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnDangKy.ForeColor = Color.White;
            this.btnDangKy.Location = new Point(160, 260);
            this.btnDangKy.Size = new Size(120, 38);
            this.btnDangKy.Text = "Đăng ký";
            this.btnDangKy.UseVisualStyleBackColor = false;
            this.btnDangKy.TabIndex = 4;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);

            // btnThoat
            this.btnThoat.BackColor = Color.FromArgb(108, 117, 125);
            this.btnThoat.FlatStyle = FlatStyle.Flat;
            this.btnThoat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnThoat.ForeColor = Color.White;
            this.btnThoat.Location = new Point(300, 260);
            this.btnThoat.Size = new Size(120, 38);
            this.btnThoat.Text = "Hủy bỏ";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            // FrmRegister
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(460, 335);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.lblNgaySinh);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.lblSoDienThoai);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "CineStar - Đăng ký thành viên mới";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}