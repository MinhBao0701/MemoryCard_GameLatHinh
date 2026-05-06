using System;
using System.Collections.Generic;
using Game_Lat_Hinh.DTO; // Đảm bảo dùng đúng namespace mới của bạn
using MemoryCard_GameLatHinh_.DAL;

namespace MemoryCard_GameLatHinh_.BLL
{
    public class GameBLL
    {
        private Random _random = new Random();

        public List<CardDTO> KhoiTaoTheBai(int soLuongThe)
        {
            List<CardDTO> dsThe = new List<CardDTO>();
            int soCap = soLuongThe / 2;

            for (int i = 1; i <= soCap; i++)
            {
                // Đổi từ ImagePath thành ImageValue cho khớp với DTO của bạn
                dsThe.Add(new CardDTO(i, $"img_{i}.png"));
                dsThe.Add(new CardDTO(i, $"img_{i}.png"));
            }

            // Thuật toán xáo trộn Fisher-Yates
            int n = dsThe.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                CardDTO value = dsThe[k];
                dsThe[k] = dsThe[n];
                dsThe[n] = value;
            }
            return dsThe;
        }

        public bool KiemTraTrungKhop(CardDTO card1, CardDTO card2)
        {
            if (card1 == null || card2 == null) return false;
            return card1.Id == card2.Id;
        }
    }
}