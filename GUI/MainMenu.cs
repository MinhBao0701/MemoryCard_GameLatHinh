using Game_Lat_Hinh.BLL;
using Game_Lat_Hinh.DTO;
using MemoryCard_GameLatHinh_.BLL;
using MemoryCard_GameLatHinh_.DTO;
using MemoryCardGame.BLL;
using MemoryCardGame.DAL;
using MemoryCardGame.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MemoryCard_GameLatHinh_.GUI
{
    public class MainMenu : Form
    {
        private readonly Color BG = Color.FromArgb(26, 26, 46);
        private readonly Color CARD_BG = Color.FromArgb(22, 33, 62);
        private readonly Color ACCENT = Color.FromArgb(233, 69, 96);
        private readonly Color TEXT = Color.White;
        private readonly Color GOLD = Color.FromArgb(255, 215, 0);

        private LevelBLL _levelBLL = new LevelBLL();
        private PlayerDAL _playerDAL = new PlayerDAL();
        private List<LevelDTO> _levels;
        private MemoryCardGame.DTO.PlayerDTO _player;

        private Panel pnlLeft, pnlRight;
        private Label lblWelcome, lblHighScore, lblWins, lblGames, lblCoins;
        private ListBox lstLevels;
        private Label lblLevelDetail;
        private Button btnPlay, btnLeader, btnLogout;

        public MainMenu()
        {
            _player = _playerDAL.GetPlayerByID(UserSession.PlayerID);

            InitForm();
            BuildUI();
            LoadLevels();
            LoadPlayerStats();
        }

        private void InitForm()
        {
            this.Text = "Game Lật Hình";
            this.Size = new Size(800, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = BG;
        }

        private void BuildUI()
        {
            // ── Tiêu đề ──
            var lblTitle = new Label()
            {
                Text = "🃏 GAME LẬT HÌNH",
                Font = new Font("Arial", 22, FontStyle.Bold),
                ForeColor = GOLD,
                AutoSize = true,
                Location = new Point(220, 15)
            };

            // ── Panel trái: Thông tin người chơi ──
            pnlLeft = new Panel()
            {
                Location = new Point(20, 60),
                Size = new Size(220, 430),
                BackColor = CARD_BG
            };
            pnlLeft.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(ACCENT, 2), 0, 0,
                    pnlLeft.Width - 1, pnlLeft.Height - 1);
            };

            var lblProfile = new Label()
            {
                Text = "👤 HỒ SƠ",
                Font = new Font("Arial", 11, FontStyle.Bold),
                ForeColor = ACCENT,
                AutoSize = true,
                Location = new Point(60, 15)
            };

            lblWelcome = new Label()
            {
                Font = new Font("Arial", 11, FontStyle.Bold),
                ForeColor = GOLD,
                AutoSize = true,
                Location = new Point(15, 50)
            };

            lblHighScore = new Label()
            {
                Font = new Font("Arial", 10),
                ForeColor = TEXT,
                AutoSize = true,
                Location = new Point(15, 90)
            };

            lblWins = new Label()
            {
                Font = new Font("Arial", 10),
                ForeColor = TEXT,
                AutoSize = true,
                Location = new Point(15, 120)
            };

            lblGames = new Label()
            {
                Font = new Font("Arial", 10),
                ForeColor = TEXT,
                AutoSize = true,
                Location = new Point(15, 150)
            };

            lblCoins = new Label()
            {
                Font = new Font("Arial", 10),
                ForeColor = GOLD,
                AutoSize = true,
                Location = new Point(15, 180)
            };

            // Nút bảng xếp hạng
            btnLeader = TaoNut("🏆  Xếp Hạng", new Point(15, 240),
                Color.FromArgb(255, 140, 0));
            btnLeader.Width = 190;

            // Nút đăng xuất
            btnLogout = TaoNut("🚪  Đăng Xuất", new Point(15, 295),
                Color.FromArgb(100, 100, 100));
            btnLogout.Width = 190;

            pnlLeft.Controls.AddRange(new Control[] {
                lblProfile, lblWelcome, lblHighScore,
                lblWins, lblGames, lblCoins,
                btnLeader, btnLogout
            });

            // ── Panel phải: Chọn màn chơi ──
            pnlRight = new Panel()
            {
                Location = new Point(260, 60),
                Size = new Size(510, 430),
                BackColor = CARD_BG
            };
            pnlRight.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(ACCENT, 2), 0, 0,
                    pnlRight.Width - 1, pnlRight.Height - 1);
            };

            var lblChooseLevel = new Label()
            {
                Text = "🎮 CHỌN MÀN CHƠI",
                Font = new Font("Arial", 11, FontStyle.Bold),
                ForeColor = ACCENT,
                AutoSize = true,
                Location = new Point(150, 15)
            };

            lstLevels = new ListBox()
            {
                Location = new Point(15, 50),
                Size = new Size(480, 230),
                BackColor = Color.FromArgb(15, 20, 40),
                ForeColor = TEXT,
                Font = new Font("Arial", 10),
                BorderStyle = BorderStyle.FixedSingle,
                ItemHeight = 30
            };
            lstLevels.SelectedIndexChanged += LstLevels_SelectedIndexChanged;

            lblLevelDetail = new Label()
            {
                Location = new Point(15, 295),
                Size = new Size(480, 60),
                ForeColor = Color.LightCyan,
                Font = new Font("Arial", 10),
                Text = "← Chọn màn để xem chi tiết"
            };

            btnPlay = TaoNut("▶   CHƠI NGAY", new Point(170, 370), ACCENT);
            btnPlay.Width = 160;
            btnPlay.Height = 45;
            btnPlay.Font = new Font("Arial", 12, FontStyle.Bold);

            pnlRight.Controls.AddRange(new Control[] {
                lblChooseLevel, lstLevels, lblLevelDetail, btnPlay
            });

            // Gắn sự kiện
            btnPlay.Click += BtnPlay_Click;
            btnLeader.Click += BtnLeader_Click;
            btnLogout.Click += BtnLogout_Click;

            this.Controls.AddRange(new Control[] {
                lblTitle, pnlLeft, pnlRight
            });
        }

        private Button TaoNut(string text, Point location, Color color)
        {
            return new Button()
            {
                Text = text,
                Location = location,
                Width = 120,
                Height = 38,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private void LoadPlayerStats()
        {
            if (_player == null) return;
            lblWelcome.Text = "👋 " + _player.PlayerName;
            lblHighScore.Text = "🏅 Điểm cao: " + _player.HighScore;
            lblWins.Text = "✅ Thắng:     " + (_player.HighestLevel > 0 ? _player.HighestLevel - 1 : 0);
            lblGames.Text = "🎮 Level đạt: " + _player.HighestLevel;
            lblCoins.Text = "💰 Xu:        " + _player.TotalCoins;
        }

        private void LoadLevels()
        {
            _levels = _levelBLL.LayDanhSachManChoi();
            lstLevels.Items.Clear();

            int maxLevel = _player?.HighestLevel ?? 1;

            foreach (var lv in _levels)
            {
                bool unlocked = lv.LevelId <= maxLevel + 1;
                string icon = unlocked ? "🔓" : "🔒";
                lstLevels.Items.Add($"{icon}  Màn {lv.LevelId}: {lv.LevelName}  " +
                                    $"({lv.Rows * lv.Columns} thẻ | {lv.TimeLimit}s)");
            }

            if (lstLevels.Items.Count > 0)
                lstLevels.SelectedIndex = 0;
        }

        private void LstLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = lstLevels.SelectedIndex;
            if (idx < 0 || idx >= _levels.Count) return;

            var lv = _levels[idx];
            int maxLevel = _player?.HighestLevel ?? 1;
            bool unlocked = lv.LevelId <= maxLevel + 1;

            lblLevelDetail.Text = unlocked
                ? $"📋 {lv.LevelName}  |  🃏 {lv.Rows * lv.Columns} thẻ  " +
                  $"|  ⏱ {lv.TimeLimit} giây  |  📐 {lv.Rows}×{lv.Columns}"
                : "🔒 Màn này chưa mở khóa! Hãy hoàn thành màn trước.";

            lblLevelDetail.ForeColor = unlocked ? Color.LightCyan : ACCENT;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            int idx = lstLevels.SelectedIndex;
            if (idx < 0) { MessageBox.Show("Vui lòng chọn màn!"); return; }

            var lv = _levels[idx];
            int maxLevel = _player?.HighestLevel ?? 1;

            if (lv.LevelId > maxLevel + 1)
            {
                MessageBox.Show("Màn này chưa mở khóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Hide();
            new frmGame(_player, lv).ShowDialog();
            // Reload stats sau khi chơi xong
            _player = _playerDAL.GetPlayerByID(UserSession.PlayerID);
            LoadPlayerStats();
            LoadLevels();
            this.Show();
        }

        private void BtnLeader_Click(object sender, EventArgs e)
        {
            new frmXepHang().ShowDialog();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
                new TrangChu().Show();
            }
        }
    }
}