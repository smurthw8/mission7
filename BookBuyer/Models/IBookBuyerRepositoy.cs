using System;
using System.Linq;

namespace BookBuyer.Models
{
    public interface IBookBuyerRepository
    {
        IQueryable<Books> Books { get; }
    }

}
