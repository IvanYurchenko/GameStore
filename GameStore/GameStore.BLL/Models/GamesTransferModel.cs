using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Models
{
    public class GamesTransferModel
    {
        public IEnumerable<GameModel> Games { get; set; }

        public PaginationModel PaginationModel { get; set; }
    }
}
