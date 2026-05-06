using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard_GameLatHinh_.DTO
{
    /// <summary>
    /// Lớp vận chuyển dữ liệu cho các vật phẩm hỗ trợ trong game (Đồng hồ cát, Mắt thần, Gợi ý).
    /// </summary>
    public class ItemDTO
    {
        // Khai báo các thuộc tính tương ứng với các cột trong bảng Items của Database
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string EffectType { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        // Constructor mặc định (Không tham số)
        public ItemDTO()
        {
        }

        // Constructor đầy đủ tham số để khởi tạo nhanh đối tượng khi lấy dữ liệu từ DAL
        public ItemDTO(int itemId, string itemName, string effectType, int price, string description)
        {
            this.ItemID = itemId;
            this.ItemName = itemName;
            this.EffectType = effectType;
            this.Price = price;
            this.Description = description;
        }
    }
}