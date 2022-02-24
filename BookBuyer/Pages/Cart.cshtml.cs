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

        //creates an instance of the data to use 
        public CartModel (IBookBuyerRepository temp)
        {
            repo = temp;
        }

        public Basket basket { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string url)
        {
            //if url is empty, will take to main page
            ReturnUrl = url ?? "/";
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }

        public IActionResult OnPost(int bookId, string returnurl)
        {
            Books b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            //if basket exists, use that basket, if not create and use new Basket
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            basket.AddItem(b, 1);

            //code to set values of basket
            HttpContext.Session.SetJson("basket", basket);

            return RedirectToPage(new { ReturnUrl = returnurl });
        }
    }
}
