using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MemoryCard_GameLatHinh_.DTO;

namespace MemoryCard_GameLatHinh_.DAL
{
    public class LeaderBoardDAL
    {
        private string connectionString = DBHelper.ConnectionString;

        public List<LeaderBoardDTO> LayBangXepHang()
        {
            List<LeaderBoardDTO> ds = new List<LeaderBoardDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM vw_Leaderboard";
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ds.Add(new LeaderBoardDTO
                        {
                            PlayerID = (int)reader["PlayerID"],
                            PlayerName = reader["PlayerName"].ToString(),
                            HighScore = (int)reader["HighScore"],
                            HighestLevel = (int)reader["HighestGameLevel"],  // ✅ sửa ở đây
                            TotalWins = (int)reader["TotalWins"],
                            WinRate = reader["WinRate"] != DBNull.Value
                                           ? Convert.ToDouble(reader["WinRate"]) : 0
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi tải bảng xếp hạng: " + ex.Message);
                }
            }
            return ds;
        }
    }
}