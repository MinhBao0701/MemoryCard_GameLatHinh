using System;

namespace MemoryCardGame.DTO // Đổi từ LeMinhBao sang MemoryCardGame
{
    public class PlayerDTO
    {
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public int TotalCoins { get; set; }
        public int HighestLevel { get; set; }
        public int HighScore { get; set; }
        public int Score { get; set; }
        public int TimeElapsed { get; set; }

        public PlayerDTO() { }

        public PlayerDTO(string name)
        {
            this.PlayerName = name;
            this.Score = 0;
            this.TotalCoins = 0;
            this.TimeElapsed = 0;
        }

        public PlayerDTO(int id, string name, int coins, int highLevel, int highScore)
        {
            this.PlayerID = id;
            this.PlayerName = name;
            this.TotalCoins = coins;
            this.HighestLevel = highLevel;
            this.HighScore = highScore;
        }
    }
}