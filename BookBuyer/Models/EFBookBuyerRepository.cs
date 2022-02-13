using System;
using System.Linq;

namespace BookBuyer.Models
{
    public class EFBookBuyerRepository: IBookBuyerRepository
    {
        private BookstoreContext context { get; set; }

        public EFBookBuyerRepository (BookstoreContext datdata)
        {
            context = datdata;

        }
        public IQueryable<Books> Books => context.Books;
    }
}
