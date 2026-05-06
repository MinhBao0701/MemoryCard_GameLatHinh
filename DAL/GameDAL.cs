using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard_GameLatHinh_.DAL
{
    // Đã đổi từ internal thành public
    public class GameDAL
    {
        /// <summary>
        /// Hàm này lấy danh sách tên các hình ảnh để nạp vào game.
        /// Giả sử chúng ta có 8 hình khác nhau cho 1 game gồm 16 ô (8 cặp).
        /// </summary>
        public List<string> LayDanhSachHinhAnh()
        {
            // Tạo một danh sách rỗng để chứa tên hình
            List<string> danhSachHinh = new List<string>();

            // Thêm tên các hình vào danh sách.
            // LƯU Ý: Tên ở đây ("hinh1", "hinh2"...) lát nữa phải ĐÚNG với 
            // tên file hình ảnh mà bạn import vào thư mục Resources của project.
            danhSachHinh.Add("hinh1");
            danhSachHinh.Add("hinh2");
            danhSachHinh.Add("hinh3");
            danhSachHinh.Add("hinh4");
            danhSachHinh.Add("hinh5");
            danhSachHinh.Add("hinh6");
            danhSachHinh.Add("hinh7");
            danhSachHinh.Add("hinh8");

            return danhSachHinh;
        }
    }
}