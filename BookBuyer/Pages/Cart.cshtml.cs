using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBuyer.Infastructure;
using BookBuyer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBuyer.Pages
{
    public class CartModel : PageModel
    {
        private IBookBuyerRepository repo { get; set; }
        public Basket basket { get; set; }

        //creates an instance of the data to use 
        public CartModel (IBookBuyerRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        public string ReturnUrl { get; set; }

        public void OnGet(string url)
        {
            //if url is empty, will take to main page
            ReturnUrl = url ?? "/";
        }

        public IActionResult OnPost(int bookId, string returnurl)
        {
            Books b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            //if basket exists, use that basket, if not create and use new Basket
            basket.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnurl });
        }

        public IActionResult OnPostRemove(int bookid, string returnurl)
        {
            //go to basket, find list item with id passed in, remove using Basket class method
            basket.RemoveItem(basket.Items.First(x => x.Book.BookId == bookid).Book);

            return RedirectToPage(new { ReturnUrl = returnurl });
        }
    }
}
