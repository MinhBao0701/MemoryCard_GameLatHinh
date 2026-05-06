using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MemoryCard_GameLatHinh_.DTO; // Sử dụng đúng namespace DTO của bạn

namespace MemoryCard_GameLatHinh_.DAL
{
    public class LeaderBoardDAL
    {
        // Chuỗi kết nối đến cơ sở dữ liệu GameLatHinh
        private string connectionString = @"Data Source=.;Initial Catalog=GameLatHinh;Integrated Security=True";

        /// <summary>
        /// Lấy danh sách Top 100 người chơi có điểm cao nhất từ View vw_Leaderboard.
        /// </summary>
        public List<LeaderBoardDTO> LayBangXepHang()
        {
            List<LeaderBoardDTO> dsLeaderBoard = new List<LeaderBoardDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Truy vấn trực tiếp từ View đã được định nghĩa trong SQL
                string sql = "SELECT * FROM vw_Leaderboard";
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        LeaderBoardDTO topPlayer = new LeaderBoardDTO
                        {
                            PlayerID = (int)reader["PlayerID"],
                            PlayerName = reader["PlayerName"].ToString(),
                            HighScore = (int)reader["HighScore"],
                            HighestLevel = (int)reader["HighestLevel"],
                            TotalWins = (int)reader["TotalWins"],
                            // Xử lý giá trị WinRate từ SQL (kiểu FLOAT/DOUBLE)
                            WinRate = reader["WinRate"] != DBNull.Value ? Convert.ToDouble(reader["WinRate"]) : 0
                        };
                        dsLeaderBoard.Add(topPlayer);
                    }
                }
                catch (Exception ex)
                {
                    // Ném lỗi để tầng BLL xử lý hoặc hiển thị lên GUI
                    throw new Exception("Lỗi khi tải bảng xếp hạng: " + ex.Message);
                }
            }

            return dsLeaderBoard;
        }
    }
}