using System;
using System.Linq;

namespace BookBuyer.Models.ViewModels
{
    public class BookBuyerViewModel
    {
        public IQueryable<Books> Books { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
