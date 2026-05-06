using MemoryCard_GameLatHinh_.DAL;
using MemoryCard_GameLatHinh_.DTO;
using MemoryCardGame.DAL; // Chỉ giữ lại Namespace chung của nhóm
using MemoryCardGame.DTO;
using System;
using System.Collections.Generic;

namespace MemoryCardGame.BLL
{
    public class ItemBLL
    {
        private ItemDAL _itemDal = new ItemDAL();
        private PlayerDAL _playerDal = new PlayerDAL();

        /// <summary>
        /// Lấy toàn bộ danh sách vật phẩm từ DAL.
        /// </summary>
        public List<ItemDTO> LayDanhSachVatPham()
        {
            return _itemDal.LayDanhSachItems();
        }

        /// <summary>
        /// Xử lý nghiệp vụ mua vật phẩm: Kiểm tra tiền -> Trừ tiền -> Thông báo.
        /// </summary>
        public string MuaVatPham(int playerId, ItemDTO item)
        {
            // 1. Kiểm tra thông tin người chơi
            PlayerDTO player = _playerDal.GetPlayerByID(playerId);

            if (player == null)
                return "Lỗi: Không tìm thấy thông tin người chơi!";

            // 2. Kiểm tra số dư tài khoản (Sử dụng thuộc tính TotalCoins đã thêm vào DTO)
            if (player.TotalCoins < item.Price)
            {
                return $"Bạn không đủ tiền! Còn thiếu {item.Price - player.TotalCoins} xu.";
            }

            // 3. Gọi DAL thực hiện trừ tiền trong SQL Server
            bool result = _playerDal.UpdateCoins(playerId, -item.Price);

            if (result)
            {
                return $"Mua thành công {item.ItemName}!";
            }
            return "Giao dịch thất bại do lỗi hệ thống.";
        }

        public ItemDTO LayThongTinVatPham(int itemId)
        {
            return _itemDal.LayItemTheoID(itemId);
        }
    }
}