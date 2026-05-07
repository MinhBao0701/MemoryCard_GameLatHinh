using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Game_Lat_Hinh.DTO; // Đảm bảo đúng namespace của tầng DTO

namespace MemoryCard_GameLatHinh_.DAL
{
    public class UserDAL
    {
        // Chuỗi kết nối đến SQL Server (Hãy thay đổi Server Name cho đúng với máy của bạn)
        private string connectionString = DBHelper.ConnectionString;

        /// <summary>
        /// Kiểm tra đăng nhập bằng cách truy vấn bảng Login và lấy thông tin Player đi kèm.
        /// </summary>
        public UserDTO CheckLogin(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Truy vấn kết hợp bảng Login và Players để lấy đầy đủ thông tin
                string sql = @"SELECT L.Username, P.PlayerName, P.PlayerID 
                               FROM Login L 
                               INNER JOIN Players P ON L.PlayerID = P.PlayerID 
                               WHERE L.Username = @user AND L.Password = @pass AND L.IsActive = 1";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new UserDTO
                        {
                            PlayerID = (int)reader["PlayerID"],  
                            Username = reader["Username"].ToString(),
                            Password = "",
                            FullName = reader["PlayerName"].ToString()
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kết nối CSDL: " + ex.Message);
                }
            }
            return null; // Trả về null nếu sai tài khoản/mật khẩu
        }

        /// <summary>
        /// Đăng ký tài khoản mới. 
        /// Theo yêu cầu, mật khẩu sẽ được lưu trực tiếp mà không có ràng buộc phức tạp.
        /// </summary>
        public bool RegisterAccount(string username, string password, string displayName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(); // Dùng Transaction vì phải insert vào 2 bảng

                try
                {
                    // 1. Tạo Player mới trước để lấy PlayerID
                    string sqlPlayer = "INSERT INTO Players (PlayerName) OUTPUT INSERTED.PlayerID VALUES (@name)";
                    SqlCommand cmdPlayer = new SqlCommand(sqlPlayer, conn, trans);
                    cmdPlayer.Parameters.AddWithValue("@name", displayName);
                    int newPlayerId = (int)cmdPlayer.ExecuteScalar();

                    // 2. Tạo tài khoản Login liên kết với PlayerID vừa tạo
                    string sqlLogin = "INSERT INTO Login (Username, Password, PlayerID) VALUES (@user, @pass, @pid)";
                    SqlCommand cmdLogin = new SqlCommand(sqlLogin, conn, trans);
                    cmdLogin.Parameters.AddWithValue("@user", username);
                    cmdLogin.Parameters.AddWithValue("@pass", password); //
                    cmdLogin.Parameters.AddWithValue("@pid", newPlayerId);

                    cmdLogin.ExecuteNonQuery();
                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
    }
}