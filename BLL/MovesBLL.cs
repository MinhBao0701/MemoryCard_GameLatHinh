using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Game_Lat_Hinh.DTO;

namespace Game_Lat_Hinh.DAL
{
    public class MovesDAL
    {
        private readonly string _connectionString = @"Data Source=.;Initial Catalog=GameLatHinh;Integrated Security=True";

        // Sửa tên thành InsertMove để khớp với BLL
        public bool InsertMove(MoveDTO move)
        {
            string sql = @"INSERT INTO Moves (GameID, Card1ID, Card2ID, IsMatch, DurationSeconds, MoveTime) 
                           VALUES (@gameId, @c1, @c2, @match, @duration, GETDATE())";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gameId", move.GameId);
                    cmd.Parameters.AddWithValue("@c1", move.Card1Id);
                    cmd.Parameters.AddWithValue("@c2", move.Card2Id);
                    cmd.Parameters.AddWithValue("@match", move.IsMatch);
                    cmd.Parameters.AddWithValue("@duration", move.DurationSeconds);

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch { return false; }
                }
            }
        }

        // Sửa tên thành GetMovesBySession để khớp với BLL
        public List<MoveDTO> GetMovesBySession(int gameId)
        {
            List<MoveDTO> dsMoves = new List<MoveDTO>();
            string sql = "SELECT * FROM Moves WHERE GameID = @gid ORDER BY MoveTime ASC";

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
                    catch { }
                }
            }
            return dsMoves;
        }
    }
}