using System;
using System.Collections.Generic;
using System.Linq;
using Game_Lat_Hinh.DTO;

namespace Game_Lat_Hinh.DAL
{
    public class LevelDAL
    {
        /// <summary>
        /// Khởi tạo danh sách 10 cấp độ từ cực dễ đến huyền thoại.
        /// </summary>
        public List<LevelDTO> GetLevels()
        {
            List<LevelDTO> ds = new List<LevelDTO>();

            // Định dạng: (ID, Tên màn, Số hàng, Số cột, Thời gian giây)

            // --- Giai đoạn: Khởi đầu ---
            ds.Add(new LevelDTO(1, "Khởi động", 2, 2, 30));   // 4 thẻ
            ds.Add(new LevelDTO(2, "Tập sự", 2, 3, 45));     // 6 thẻ
            ds.Add(new LevelDTO(3, "Dễ", 3, 4, 60));         // 12 thẻ

            // --- Giai đoạn: Trung cấp ---
            ds.Add(new LevelDTO(4, "Tiêu chuẩn", 4, 4, 80));  // 16 thẻ
            ds.Add(new LevelDTO(5, "Thử thách", 4, 5, 100));  // 20 thẻ
            ds.Add(new LevelDTO(6, "Trung bình", 4, 6, 120)); // 24 thẻ

            // --- Giai đoạn: Cao cấp ---
            ds.Add(new LevelDTO(7, "Khó", 5, 6, 150));        // 30 thẻ
            ds.Add(new LevelDTO(8, "Chuyên gia", 6, 6, 180));  // 36 thẻ
            ds.Add(new LevelDTO(9, "Bậc thầy", 6, 7, 210));   // 42 thẻ

            // --- Giai đoạn: Huyền thoại ---
            ds.Add(new LevelDTO(10, "Huyền thoại", 8, 8, 300)); // 64 thẻ

            return ds;
        }

        public LevelDTO GetLevelByID(int id)
        {
            return GetLevels().FirstOrDefault(x => x.LevelId == id);
        }
    }
}