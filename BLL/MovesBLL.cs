using Game_Lat_Hinh.DTO;
using Game_Lat_Hinh.DAL;
using System.Collections.Generic;

namespace MemoryCard_GameLatHinh_.BLL
{
    public class MovesBLL
    {
        private MovesDAL _movesDAL = new MovesDAL();

        public bool LuuMove(MoveDTO move)
        {
            return _movesDAL.LuuLichSuMove(move);
        }

        public List<MoveDTO> LayLichSuVan(int gameId)
        {
            return _movesDAL.LayLichSuTheoGame(gameId);
        }
    }
}