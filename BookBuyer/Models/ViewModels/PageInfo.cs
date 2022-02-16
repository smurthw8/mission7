using System;
namespace BookBuyer.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumBooks { get; set; }
        public int BooksPerPage { get; set; }
        public int Currentpage { get; set; }
        //calculate number of pages needed, calc at runtime
        //Ceiling = round up, this casts it as double, rounds up, and then casts as int
        public int TotalPages => (int) Math.Ceiling((double) TotalNumBooks / BooksPerPage);
    }
}
