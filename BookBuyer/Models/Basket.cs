using System;
using System.Collections.Generic;
using System.Linq;

namespace BookBuyer.Models
{
    public class Basket
    {
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public void AddItem (Books book, int qty)
        {
            BasketItem line = Items
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();

            //if that listing isn't in cart yet, add as new line item to List of Items
            if (line == null)
            {
                Items.Add(new BasketItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }
       public double CalculateTotal()
        {
            double total = Items.Sum(x => x.Quantity * x.Book.Price);

            return total;
        }
 }


    public class BasketItem
    {
        public int LineID { get; set; }
        public Books Book { get; set; }
        public int Quantity { get; set; }
    }
}
