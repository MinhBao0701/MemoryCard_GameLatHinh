using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Đổi namespace cho khớp với các file DTO khác
namespace Game_Lat_Hinh.DTO
{
    // Bắt buộc đổi thành public
    public class UserDTO
    {
        public int PlayerID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        // Constructor mặc định
        public UserDTO()
        {
        }

        // Constructor có tham số để dễ dàng khởi tạo tài khoản mới
        public UserDTO(string username, string password, string fullName)
        {
            Username = username;
            Password = password;
            FullName = fullName;
        }
    }
}