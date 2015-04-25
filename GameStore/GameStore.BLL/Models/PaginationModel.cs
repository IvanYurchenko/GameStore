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

        public int PagesNumber
        {
            get { return (int)Math.Ceiling((decimal)(ItemsNumber/((int)PageCapacity))); }
        }

        public bool IsFirstPage
        {
            get { return CurrentPage == 1; }
        }

        public bool IsLastPage
        {
            get { return CurrentPage == PagesNumber; }
        }


    }
}
