using GameStore.BLL.Enums;

namespace GameStore.BLL.Models
{
    public class PaginationModel
    {
        public PageCapacity PageCapacity { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsNumber { get; set; }
    }
}