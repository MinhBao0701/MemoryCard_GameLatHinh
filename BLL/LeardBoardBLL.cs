using System;
using System.Collections.Generic;
using MemoryCard_GameLatHinh_.DAL;
using MemoryCard_GameLatHinh_.DTO;

namespace MemoryCard_GameLatHinh_.BLL
{
    public class LeaderBoardBLL
    {
        private LeaderBoardDAL _leaderBoardDal = new LeaderBoardDAL();

        /// <summary>
        /// Lấy toàn bộ danh sách bảng xếp hạng từ tầng DAL.
        /// </summary>
        public List<LeaderBoardDTO> LayDanhSachXepHang()
        {
            try
            {
                // Thực hiện gọi hàm từ DAL để lấy dữ liệu từ View vw_Leaderboard
                return _leaderBoardDal.LayBangXepHang();
            }
            catch (Exception ex)
            {
                // Bạn có thể xử lý thêm logic ghi log lỗi tại đây
                throw new Exception("Lỗi nghiệp vụ khi tải bảng xếp hạng: " + ex.Message);
            }
        }

        /// <summary>
        /// Logic bổ sung: Chỉ lấy Top 10 người chơi xuất sắc nhất.
        /// </summary>
        public List<LeaderBoardDTO> LayTop10()
        {
            List<LeaderBoardDTO> fullList = LayDanhSachXepHang();

            // Nếu danh sách ít hơn 10 thì lấy hết, ngược lại chỉ lấy 10 phần tử đầu
            if (fullList.Count > 10)
            {
                return fullList.GetRange(0, 10);
            }
            return fullList;
        }
    }
}