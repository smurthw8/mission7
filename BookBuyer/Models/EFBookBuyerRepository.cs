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

        public void SaveBook(Books b)
        {
            context.SaveChanges();
        }

        public void CreateBook(Books b)
        {
            context.Add(b);
            context.SaveChanges();
        }

        public void DeleteBook(Books b)
        {
            context.Remove(b);
            context.SaveChanges();
        }
    }
}
