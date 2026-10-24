using System.Drawing;
using System.Windows.Forms;

namespace CineStarApp
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlLeftBrand;
        private Label lblBrandTitle;
        private Label lblBrandSub;
        private Label lblFormHeader;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkShowPassword;
        private Label lblRole;
        private ComboBox cboRole;
        private Button btnLogin;
        private Button btnExit;
        private Button btnRegister; // Nút đăng ký thành viên mới bổ sung
        private Label lblErrorMessage;

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
            this.pnlLeftBrand = new Panel();
            this.lblBrandTitle = new Label();
            this.lblBrandSub = new Label();
            this.lblFormHeader = new Label();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.chkShowPassword = new CheckBox();
            this.lblRole = new Label();
            this.cboRole = new ComboBox();
            this.btnLogin = new Button();
            this.btnExit = new Button();
            this.btnRegister = new Button();
            this.lblErrorMessage = new Label();
            this.pnlLeftBrand.SuspendLayout();
            this.SuspendLayout();

            // pnlLeftBrand
            this.pnlLeftBrand.BackColor = Color.FromArgb(24, 30, 54);
            this.pnlLeftBrand.Controls.Add(this.lblBrandSub);
            this.pnlLeftBrand.Controls.Add(this.lblBrandTitle);
            this.pnlLeftBrand.Dock = DockStyle.Left;
            this.pnlLeftBrand.Size = new Size(230, 455); // Tăng chiều cao tương ứng Form

            // lblBrandTitle
            this.lblBrandTitle.ForeColor = Color.Gold;
            this.lblBrandTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblBrandTitle.Location = new Point(15, 145);
            this.lblBrandTitle.Size = new Size(200, 50);
            this.lblBrandTitle.Text = "CINESTAR";
            this.lblBrandTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblBrandSub
            this.lblBrandSub.ForeColor = Color.WhiteSmoke;
            this.lblBrandSub.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblBrandSub.Location = new Point(15, 200);
            this.lblBrandSub.Size = new Size(200, 60);
            this.lblBrandSub.Text = "HỆ THỐNG QUẢN LÝ\n& BÁN VÉ XEM PHIM";
            this.lblBrandSub.TextAlign = ContentAlignment.TopCenter;

            // lblFormHeader
            this.lblFormHeader.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            this.lblFormHeader.ForeColor = Color.FromArgb(24, 30, 54);
            this.lblFormHeader.Location = new Point(260, 25);
            this.lblFormHeader.Size = new Size(320, 35);
            this.lblFormHeader.Text = "ĐĂNG NHẬP HỆ THỐNG";

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblUsername.Location = new Point(260, 75);
            this.lblUsername.Text = "Tên đăng nhập:";

            // txtUsername
            this.txtUsername.Font = new Font("Segoe UI", 11F);
            this.txtUsername.Location = new Point(260, 100);
            this.txtUsername.Size = new Size(310, 27);
            this.txtUsername.TabIndex = 0;

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblPassword.Location = new Point(260, 140);
            this.lblPassword.Text = "Mật khẩu:";

            // txtPassword
            this.txtPassword.Font = new Font("Segoe UI", 11F);
            this.txtPassword.Location = new Point(260, 165);
            this.txtPassword.Size = new Size(310, 27);
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TabIndex = 1;

            // chkShowPassword
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Font = new Font("Segoe UI", 9.5F);
            this.chkShowPassword.Location = new Point(260, 200);
            this.chkShowPassword.Text = "Hiện mật khẩu";
            this.chkShowPassword.TabIndex = 2;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);

            // lblRole
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblRole.Location = new Point(260, 230);
            this.lblRole.Text = "Phân hệ kết nối (Role CSDL):";

            // cboRole
            this.cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRole.Font = new Font("Segoe UI", 11F);
            this.cboRole.Items.AddRange(new object[] { "Admin", "User" });
            this.cboRole.Location = new Point(260, 255);
            this.cboRole.Size = new Size(310, 28);
            this.cboRole.TabIndex = 3;

            // lblErrorMessage
            this.lblErrorMessage.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblErrorMessage.ForeColor = Color.Red;
            this.lblErrorMessage.Location = new Point(260, 290);
            this.lblErrorMessage.Size = new Size(320, 38);
            this.lblErrorMessage.Text = "";
            this.lblErrorMessage.Visible = false;

            // btnLogin
            this.btnLogin.BackColor = Color.FromArgb(0, 123, 255);
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(260, 335);
            this.btnLogin.Size = new Size(150, 38);
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // btnExit
            this.btnExit.BackColor = Color.FromArgb(108, 117, 125);
            this.btnExit.FlatStyle = FlatStyle.Flat;
            this.btnExit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnExit.ForeColor = Color.White;
            this.btnExit.Location = new Point(420, 335);
            this.btnExit.Size = new Size(150, 38);
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.TabIndex = 5;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // btnRegister (Nút đăng ký thành viên mới)
            this.btnRegister.BackColor = Color.FromArgb(40, 167, 69);
            this.btnRegister.FlatStyle = FlatStyle.Flat;
            this.btnRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRegister.ForeColor = Color.White;
            this.btnRegister.Location = new Point(260, 383);
            this.btnRegister.Size = new Size(310, 38);
            this.btnRegister.Text = "✨ Đăng ký thành viên mới";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.TabIndex = 6;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // FrmLogin
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(605, 455);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.lblErrorMessage);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.cboRole);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblFormHeader);
            this.Controls.Add(this.pnlLeftBrand);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "CineStar Management System - Đăng nhập";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.pnlLeftBrand.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}