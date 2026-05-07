using System;
using System.Drawing;
using System.Windows.Forms;
using MemoryCard_GameLatHinh_.BLL;
using Game_Lat_Hinh.DTO;
using MemoryCard_GameLatHinh_;
namespace MemoryCard_GameLatHinh_.GUI
{
    public partial class TrangChu : Form
    {
        private TextBox txtUsername, txtPassword;
        private Button btnLogin, btnRegister;
        private Label lblMessage;
        private CheckBox chkShowPass;
        private UserBLL _userBLL = new UserBLL();

        public TrangChu()
        {
            InitializeComponent();
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            this.Text = "Game Lật Hình - Đăng Nhập";
            this.Size = new System.Drawing.Size(420, 320);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            var lblTitle = new Label()
            {
                Text = "GAME LẬT HÌNH",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,
                Location = new System.Drawing.Point(100, 20)
            };

            var lblUser = new Label()
            {
                Text = "Tên đăng nhập:",
                AutoSize = true,
                Location = new System.Drawing.Point(50, 90)
            };
            txtUsername = new TextBox()
            {
                Location = new System.Drawing.Point(180, 87),
                Width = 180
            };

            var lblPass = new Label()
            {
                Text = "Mật khẩu:",
                AutoSize = true,
                Location = new System.Drawing.Point(50, 130)
            };
            txtPassword = new TextBox()
            {
                Location = new System.Drawing.Point(180, 127),
                Width = 180,
                PasswordChar = '*'
            };

            chkShowPass = new CheckBox()
            {
                Text = "Hiện mật khẩu",
                Location = new System.Drawing.Point(180, 158),
                AutoSize = true
            };
            chkShowPass.CheckedChanged += (s, e) =>
                txtPassword.PasswordChar = chkShowPass.Checked ? '\0' : '*';

            btnLogin = new Button()
            {
                Text = "Đăng Nhập",
                Location = new System.Drawing.Point(80, 190),
                Width = 120,
                Height = 35,
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnRegister = new Button()
            {
                Text = "Đăng Ký",
                Location = new System.Drawing.Point(220, 190),
                Width = 120,
                Height = 35,
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            lblMessage = new Label()
            {
                Location = new System.Drawing.Point(50, 235),
                AutoSize = true,
                ForeColor = Color.Red
            };

            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;

            this.Controls.AddRange(new Control[] {
                lblTitle, lblUser, txtUsername,
                lblPass, txtPassword, chkShowPass,
                btnLogin, btnRegister, lblMessage
            });
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            UserDTO user = _userBLL.DangNhap(username, password);

            if (user != null)
            {
                UserSession.PlayerID = user.PlayerID;  
                UserSession.Username = user.Username;
                UserSession.PlayerName = user.FullName;

                this.Hide();
                new MainMenu().ShowDialog();
                this.Close();
            }
            else
            {
                lblMessage.Text = "Sai tên đăng nhập hoặc mật khẩu!";
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đăng ký sẽ làm sau!", "Thông báo");
        }
    }
}