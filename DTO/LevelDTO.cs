using System;

namespace Game_Lat_Hinh.DTO
{
    public class LevelDTO
    {
        public int LevelId { get; set; } // Dùng Id để đồng bộ
        public string LevelName { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int TimeLimit { get; set; }

        public LevelDTO() { }

        public LevelDTO(int id, string name, int rows, int cols, int time)
        {
            this.LevelId = id;
            this.LevelName = name;
            this.Rows = rows;
            this.Columns = cols;
            this.TimeLimit = time;
        }
    }
}