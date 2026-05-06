using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MemoryCard_GameLatHinh_.DTO; // Đảm bảo gọi đúng namespace của tầng DTO

namespace MemoryCard_GameLatHinh_.DAL
{
    public class ItemDAL
    {
        // Chuỗi kết nối đến Database GameLatHinh
        // Bạn có thể dùng DatabaseHelper.ConnectionString nếu đã tạo lớp dùng chung
        private string connectionString = @"Data Source=.;Initial Catalog=GameLatHinh;Integrated Security=True";

        /// <summary>
        /// Lấy toàn bộ danh sách vật phẩm hỗ trợ từ bảng Items trong Database.
        /// </summary>
        public List<ItemDTO> LayDanhSachItems()
        {
            List<ItemDTO> dsItems = new List<ItemDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Truy vấn các cột: ItemID, ItemName, EffectType, Price, Description từ bảng Items
                string sql = "SELECT ItemID, ItemName, EffectType, Price, Description FROM Items";
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ItemDTO item = new ItemDTO
                        {
                            ItemID = (int)reader["ItemID"],
                            ItemName = reader["ItemName"].ToString(),
                            EffectType = reader["EffectType"].ToString(),
                            Price = (int)reader["Price"],
                            Description = reader["Description"].ToString()
                        };
                        dsItems.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    // Ném lỗi để tầng BLL hoặc GUI xử lý hiển thị thông báo
                    throw new Exception("Lỗi khi lấy danh sách vật phẩm: " + ex.Message);
                }
            }

            return dsItems;
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một vật phẩm dựa trên ID.
        /// </summary>
        public ItemDTO LayItemTheoID(int itemId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Items WHERE ItemID = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", itemId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new ItemDTO(
                        (int)reader["ItemID"],
                        reader["ItemName"].ToString(),
                        reader["EffectType"].ToString(),
                        (int)reader["Price"],
                        reader["Description"].ToString()
                    );
                }
            }
            return null;
        }
    }
}