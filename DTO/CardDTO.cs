using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Lat_Hinh.DTO
{
    public class CardDTO
    {
        public int Id { get; set; }
        public string ImageValue { get; set; }
        public bool IsFlipped { get; set; }
        public bool IsMatched { get; set; }

        public CardDTO()
        {
            IsFlipped = false;
            IsMatched = false;
        }

        public CardDTO(int id, string imageValue)
        {
            Id = id;
            ImageValue = imageValue;
            IsFlipped = false;
            IsMatched = false;
        }
    }
}