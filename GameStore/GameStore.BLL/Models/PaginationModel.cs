using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
