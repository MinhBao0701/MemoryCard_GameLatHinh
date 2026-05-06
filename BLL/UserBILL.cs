using System;
using MemoryCard_GameLatHinh_.DAL;
using Game_Lat_Hinh.DTO;

namespace MemoryCard_GameLatHinh_.BLL
{
    public class UserBLL
    {
        private UserDAL _userDal = new UserDAL();

        /// <summary>
        /// Xử lý nghiệp vụ đăng nhập: Kiểm tra đầu vào và gọi DAL để xác thực
        /// </summary>
        public UserDTO DangNhap(string username, string password)
        {
            // Kiểm tra các ràng buộc cơ bản trước khi truy vấn CSDL
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            // Gọi xuống tầng DAL để kiểm tra tài khoản trong SQL Server
            return _userDal.CheckLogin(username, password);
        }

        /// <summary>
        /// Xử lý nghiệp vụ đăng ký tài khoản mới
        /// </summary>
        public string DangKy(string username, string password, string displayName)
        {
            // 1. Kiểm tra không được để trống thông tin
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(displayName))
            {
                return "Vui lòng nhập đầy đủ thông tin đăng ký!";
            }

            // 2. Gọi DAL để thực hiện Insert vào database
            bool result = _userDal.RegisterAccount(username, password, displayName);

            if (result)
            {
                return "Đăng ký thành công!";
            }
            else
            {
                return "Đăng ký thất bại! Tên đăng nhập có thể đã tồn tại.";
            }
        }
    }
}