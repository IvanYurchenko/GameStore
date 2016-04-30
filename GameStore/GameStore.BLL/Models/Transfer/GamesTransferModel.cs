using System.Collections.Generic;

namespace GameStore.BLL.Models.Transfer
{
    public class GamesTransferModel
    {
        public IEnumerable<GameModel> Games { get; set; }

        public PaginationModel PaginationModel { get; set; }
    }
}