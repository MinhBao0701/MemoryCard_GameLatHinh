using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Game_Lat_Hinh.DTO; // Thống nhất Namespace chung của nhóm

namespace Game_Lat_Hinh.DAL
{
    public class MovesDAL
    {
        // Nhóm có thể đưa chuỗi này vào file cấu riêng (App.config) để bảo mật hơn
        private readonly string _connectionString = @"Data Source=.;Initial Catalog=GameLatHinh;Integrated Security=True";

        /// <summary>
        /// Lưu lại lịch sử lượt lật bài. Đã tích hợp thêm trường DurationSeconds.
        /// </summary>
        public bool LuuLichSuMove(MoveDTO move)
        {
            const string sql = @"INSERT INTO Moves (GameID, Card1ID, Card2ID, IsMatch, DurationSeconds, MoveTime) 
                                 VALUES (@gameId, @c1, @c2, @match, @duration, GETDATE())";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Sử dụng Add để xác định rõ kiểu dữ liệu, tránh lỗi ép kiểu trong SQL
                    cmd.Parameters.Add("@gameId", SqlDbType.Int).Value = move.GameId;
                    cmd.Parameters.Add("@c1", SqlDbType.Int).Value = move.Card1Id;
                    cmd.Parameters.Add("@c2", SqlDbType.Int).Value = move.Card2Id;
                    cmd.Parameters.Add("@match", SqlDbType.Bit).Value = move.IsMatch;
                    cmd.Parameters.Add("@duration", SqlDbType.Float).Value = move.DurationSeconds;

                    try
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException ex)
                    {
                        // Ghi log lỗi SQL cụ thể thay vì throw Exception chung chung
                        Console.WriteLine("Database Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Lấy toàn bộ lịch sử lượt lật bài của một ván chơi.
        /// </summary>
        public List<MoveDTO> LayLichSuTheoGame(int gameId)
        {
            List<MoveDTO> dsMoves = new List<MoveDTO>();
            const string sql = "SELECT * FROM Moves WHERE GameID = @gid ORDER BY MoveTime ASC";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", gameId);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dsMoves.Add(new MoveDTO
                                {
                                    MoveId = Convert.ToInt32(reader["MoveID"]),
                                    GameId = Convert.ToInt32(reader["GameID"]),
                                    Card1Id = Convert.ToInt32(reader["Card1ID"]),
                                    Card2Id = Convert.ToInt32(reader["Card2ID"]),
                                    IsMatch = Convert.ToBoolean(reader["IsMatch"]),
                                    DurationSeconds = Convert.ToDouble(reader["DurationSeconds"]),
                                    MoveTime = Convert.ToDateTime(reader["MoveTime"])
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Fetch Error: " + ex.Message);
                    }
                }
            }
            return dsMoves;
        }

        /// <summary>
        /// Xóa lịch sử các lượt đi của một ván (Dùng khi người chơi Restart ván đó).
        /// </summary>
        public bool XoaLichSuTheoGame(int gameId)
        {
            const string sql = "DELETE FROM Moves WHERE GameID = @gid";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", gameId);
                    conn.Open();
                    return cmd.ExecuteNonQuery() >= 0;
                }
            }
        }
    }
}