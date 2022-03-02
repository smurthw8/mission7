using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookBuyer.Models
{
    public class EFPurchaseRepository : IPurchaseRepository
    {
        //private instance of context to use
        private BookstoreContext context;

        //recieve data from
        public EFPurchaseRepository(BookstoreContext temp)
        {
            context = temp;
        }

        //join multiple tables
        public IQueryable<Purchase> Purchases => context.Purchases.Include(x => x.Lines).ThenInclude(x => x.Book);

        public void SavePurchase(Purchase purchase)
        {
            context.AttachRange(purchase.Lines.Select(x => x.Book));

            if(purchase.PurchaseID == 0)
            {
                context.Purchases.Add(purchase);
            }

            context.SaveChanges();
        }
    }
}
