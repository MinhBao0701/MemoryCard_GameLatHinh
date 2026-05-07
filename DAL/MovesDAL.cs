using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Game_Lat_Hinh.DTO;
using MemoryCard_GameLatHinh_;

namespace Game_Lat_Hinh.DAL
{
    public class MovesDAL
    {
        private string connectionString = DBHelper.ConnectionString;

        public bool LuuLichSuMove(MoveDTO move)
        {
            const string sql = @"INSERT INTO Moves (GameID, MoveNumber, Card1ID, Card2ID, IsMatch, MoveTime)
                                  VALUES (@gameId, @moveNum, @c1, @c2, @match, GETDATE())";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@gameId", SqlDbType.Int).Value = move.GameId;
                    cmd.Parameters.Add("@moveNum", SqlDbType.Int).Value = move.MoveNumber ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@c1", SqlDbType.Int).Value = move.Card1Id;
                    cmd.Parameters.Add("@c2", SqlDbType.Int).Value = move.Card2Id;
                    cmd.Parameters.Add("@match", SqlDbType.Bit).Value = move.IsMatch;

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Lỗi DB: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public List<MoveDTO> LayLichSuTheoGame(int gameId)
        {
            List<MoveDTO> ds = new List<MoveDTO>();
            const string sql = "SELECT * FROM Moves WHERE GameID = @gid ORDER BY MoveTime ASC";

            using (SqlConnection conn = new SqlConnection(connectionString))
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
                                ds.Add(new MoveDTO
                                {
                                    MoveId = Convert.ToInt32(reader["MoveID"]),
                                    GameId = Convert.ToInt32(reader["GameID"]),
                                    MoveNumber = Convert.ToInt32(reader["MoveNumber"]),
                                    Card1Id = Convert.ToInt32(reader["Card1ID"]),
                                    Card2Id = Convert.ToInt32(reader["Card2ID"]),
                                    IsMatch = Convert.ToBoolean(reader["IsMatch"]),
                                    MoveTime = Convert.ToDateTime(reader["MoveTime"])
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                }
            }
            return ds;
        }
    }
}