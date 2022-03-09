using System;
using System.Linq;

namespace BookBuyer.Models
{
    public interface IBookBuyerRepository
    {
        IQueryable<Books> Books { get; }

        public void SaveBook(Books b);
        public void CreateBook(Books b);
        public void DeleteBook(Books b);
    }

}
