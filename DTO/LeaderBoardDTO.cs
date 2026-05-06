using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard_GameLatHinh_.DTO
{
    /// <summary>
    /// Lớp vận chuyển dữ liệu cho bảng xếp hạng (Mapping từ View vw_Leaderboard)
    /// </summary>
    public class LeaderBoardDTO
    {
        // Các thuộc tính khớp với định nghĩa trong View vw_Leaderboard
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public int HighScore { get; set; }
        public int HighestLevel { get; set; }
        public int TotalWins { get; set; }
        public double WinRate { get; set; }

        // Constructor mặc định
        public LeaderBoardDTO() { }

        // Constructor đầy đủ tham số để khởi tạo nhanh từ DAL
        public LeaderBoardDTO(int id, string name, int score, int level, int wins, double rate)
        {
            this.PlayerID = id;
            this.PlayerName = name;
            this.HighScore = score;
            this.HighestLevel = level;
            this.TotalWins = wins;
            this.WinRate = rate;
        }
    }
}