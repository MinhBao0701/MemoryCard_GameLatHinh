using MemoryCard_GameLatHinh_;
using MemoryCardGame.DTO;
using System;
using System.Data.SqlClient;

namespace MemoryCardGame.DAL
{
    public class PlayerDAL
    {
        private string connectionString = DBHelper.ConnectionString;

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
                    return new PlayerDTO
                    {
                        PlayerID = (int)r["PlayerID"],
                        PlayerName = r["PlayerName"].ToString(),
                        TotalCoins = (int)r["TotalCoins"],
                        HighestLevel = (int)r["CurrentLevel"],  // ✅ sửa ở đây
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