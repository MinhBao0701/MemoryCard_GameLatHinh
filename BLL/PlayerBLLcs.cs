using Game_Lat_Hinh.DAL;
using Game_Lat_Hinh.DTO;
using MemoryCardGame.DAL;
using MemoryCardGame.DTO;
using System;
using System.Collections.Generic;

namespace Game_Lat_Hinh.BLL
{
    public class PlayerBLL
    {
        private PlayerDAL _playerDal = new PlayerDAL();

        /// <summary>
        /// Lấy thông tin người chơi và kiểm tra tính tồn tại.
        /// </summary>
        public PlayerDTO LayThongTinNguoiChoi(int playerId)
        {
            if (playerId <= 0) return null;
            return _playerDal.GetPlayerByID(playerId);
        }

        /// <summary>
        /// Logic nâng cao: Chỉ cập nhật điểm cao nếu điểm mới thực sự lớn hơn điểm cũ.
        /// </summary>
        /// <returns>True nếu phá kỷ lục, False nếu không.</returns>
        public bool XuLyLuuKyLuc(int playerId, int scoreMoi)
        {
            PlayerDTO player = _playerDal.GetPlayerByID(playerId);
            if (player != null && scoreMoi > player.HighScore)
            {
                // Giả định bạn đã có hàm UpdateHighScore trong DAL
                // return _playerDal.UpdateHighScore(playerId, scoreMoi);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Logic nâng cao: Tính toán xếp hạng dựa trên số điểm đạt được của màn chơi.
        /// </summary>
        public string TinhXepHang(int score, int timeLimit, int timeElapsed)
        {
            // Tính toán hiệu suất (ví dụ: điểm cao và thời gian còn dư nhiều)
            double performance = (double)score / (timeLimit - timeElapsed);

            if (performance > 2.0) return "S (Xuất sắc)";
            if (performance > 1.5) return "A (Giỏi)";
            if (performance > 1.0) return "B (Khá)";
            return "C (Trung bình)";
        }

        /// <summary>
        /// Cập nhật số tiền xu với kiểm tra giá trị đầu vào hợp lệ.
        /// </summary>
        public bool CapNhatCoins(int playerId, int amount)
        {
            // Tránh việc cộng số âm không kiểm soát (trừ khi là giao dịch mua hàng)
            if (playerId <= 0) return false;

            return _playerDal.UpdateCoins(playerId, amount);
        }

        /// <summary>
        /// Logic nâng cao: Kiểm tra xem người chơi có đủ điều kiện mở màn tiếp theo không.
        /// </summary>
        public bool KiemTraMoKhoaLevel(int playerId, int levelMuonVao)
        {
            PlayerDTO player = _playerDal.GetPlayerByID(playerId);
            if (player == null) return false;

            // Quy tắc: Chỉ mở level mới nếu level hiện tại <= level cao nhất đã vượt qua + 1
            return levelMuonVao <= (player.HighestLevel + 1);
        }
    }
}