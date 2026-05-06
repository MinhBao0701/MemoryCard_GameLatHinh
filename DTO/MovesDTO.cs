using System;

namespace Game_Lat_Hinh.DTO
{
    /// <summary>
    /// Đối tượng vận chuyển dữ liệu cho mỗi lượt di chuyển (lật cặp thẻ).
    /// </summary>
    public class MoveDTO
    {
        #region Properties

        public int MoveId { get; set; }
        public int GameId { get; set; }

        /// <summary>
        /// Thứ tự của lượt đi (Lượt 1, 2, 3...).
        /// </summary>
        public int? MoveNumber { get; set; }

        /// <summary>
        /// Vị trí/ID của thẻ bài thứ nhất và thứ hai.
        /// </summary>
        public int Card1Id { get; set; }
        public int Card2Id { get; set; }

        /// <summary>
        /// Giá trị hoặc tên ảnh của thẻ bài (VD: "King", "Image_01").
        /// Giúp bạn biết người chơi vừa lật trúng hình gì.
        /// </summary>
        public string CardValue { get; set; }

        /// <summary>
        /// Kết quả: True nếu khớp cặp.
        /// </summary>
        public bool IsMatch { get; set; }

        /// <summary>
        /// Thời điểm lật bài.
        /// </summary>
        public DateTime MoveTime { get; set; }

        /// <summary>
        /// Thời gian suy nghĩ (giây).
        /// </summary>
        public double DurationSeconds { get; set; }

        /// <summary>
        /// Đánh dấu đây có phải là lượt đi giúp người chơi nhận thêm điểm thưởng không.
        /// </summary>
        public bool IsBonusMove { get; set; }

        #endregion

        #region Constructors

        public MoveDTO()
        {
            this.MoveTime = DateTime.Now;
        }

        /// <summary>
        /// Constructor đầy đủ để khởi tạo nhanh từ tầng BLL.
        /// </summary>
        public MoveDTO(int gameId, int c1, int c2, string val, bool match, int? moveNum = null, double duration = 0)
        {
            this.GameId = gameId;
            this.Card1Id = c1;
            this.Card2Id = c2;
            this.CardValue = val;
            this.IsMatch = match;
            this.MoveNumber = moveNum;
            this.DurationSeconds = duration;
            this.MoveTime = DateTime.Now;

            // Tự động tính Bonus nếu lật đúng trong vòng dưới 1.5 giây
            this.IsBonusMove = (match && duration > 0 && duration <= 1.5);
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Trả về mô tả chi tiết lượt đi để hiển thị lên bảng tin (History Log).
        /// </summary>
        public string GetDetailedLog()
        {
            string status = IsMatch ? "KHỚP" : "SAI";
            string bonus = IsBonusMove ? " (+BONUS!)" : "";
            return $"[#{MoveNumber}] {MoveTime:HH:mm:ss}: Cặp {status} {CardValue} ({DurationSeconds}s){bonus}";
        }

        #endregion
    }
}