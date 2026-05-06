using System;
using System.Data.SqlClient;
using MemoryCardGame.DTO; // Chỉ giữ lại duy nhất namespace của nhóm

namespace MemoryCardGame.DAL
{
    public class PlayerDAL
    {
        private string connectionString = @"Data Source=.;Initial Catalog=GameLatHinh;Integrated Security=True";

        public PlayerDTO GetPlayerByID(int playerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Players WHERE PlayerID = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", playerId);
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    // PlayerDTO này phải thuộc namespace MemoryCardGame.DTO
                    return new PlayerDTO
                    {
                        PlayerID = (int)r["PlayerID"],
                        PlayerName = r["PlayerName"].ToString(),
                        TotalCoins = (int)r["TotalCoins"],
                        HighestLevel = (int)r["HighestLevel"],
                        HighScore = (int)r["HighScore"]
                    };
                }
            }
            return null;
        }

        public bool UpdateCoins(int playerId, int amount)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Players SET TotalCoins = TotalCoins + @amount WHERE PlayerID = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@id", playerId);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}