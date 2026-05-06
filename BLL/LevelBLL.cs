using Game_Lat_Hinh.DAL;
using Game_Lat_Hinh.DTO;
using MemoryCardGame.DAL; // Kết nối xuống tầng dữ liệu
using MemoryCardGame.DTO; // Sử dụng các đối tượng vận chuyển dữ liệu
using System;
using System.Collections.Generic;

namespace MemoryCardGame.BLL
{
    public class LevelBLL
    {
        private LevelDAL _levelDal = new LevelDAL();
        private PlayerDAL _playerDal = new PlayerDAL();

        /// <summary>
        /// Lấy danh sách tất cả các màn chơi để hiển thị lên Form chọn màn.
        /// </summary>
        public List<LevelDTO> LayDanhSachManChoi()
        {
            try
            {
                return _levelDal.GetLevels(); // Giả định LevelDAL đã có hàm GetLevels
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách màn chơi: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiểm tra xem một màn chơi cụ thể có đang bị khóa đối với người chơi này không.
        /// </summary>
        /// <param name="playerId">ID người chơi hiện tại</param>
        /// <param name="levelId">ID màn chơi muốn kiểm tra</param>
        /// <returns>True nếu đã mở khóa, False nếu còn bị khóa</returns>
        public bool KiemTraMoKhoa(int playerId, int levelId)
        {
            // Màn chơi đầu tiên (Level 1) luôn luôn mở khóa
            if (levelId == 1) return true;

            // Lấy thông tin tiến trình của người chơi từ PlayerDAL
            PlayerDTO player = _playerDal.GetPlayerByID(playerId);

            if (player == null) return false;

            // Logic: Người chơi được phép chơi màn (HighestLevel + 1)
            // Ví dụ: Đã vượt màn 3 (HighestLevel = 3) thì được chơi màn 4.
            return levelId <= (player.HighestLevel + 1);
        }

        /// <summary>
        /// Lấy thông tin chi tiết (số lượng thẻ, thời gian) của một màn chơi.
        /// </summary>
        public LevelDTO LayChiTietManChoi(int levelId)
        {
            return _levelDal.GetLevelByID(levelId);
        }
    }
}