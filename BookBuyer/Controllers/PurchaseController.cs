using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBuyer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBuyer.Controllers
{
    public class PurchaseController : Controller
    {

        private IPurchaseRepository repo { get; set; }
        private Basket basket { get; set; }

        public PurchaseController(IPurchaseRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            //creates new purchase item when checkout
            return View( new Purchase());
        }

        [HttpPost]
        public IActionResult Checkout(Purchase purchase)
        {
            if (basket.Items.Count() == 0)//if cart empty
            {
                ModelState.AddModelError("", "Sorry, your cart is empty. Please select at least 1 book.");
            }
            if (ModelState.IsValid)
            {
                purchase.Lines = basket.Items.ToArray();
                repo.SavePurchase(purchase);
                basket.ClearBasket();

                return RedirectToPage("/PurchaseConfirm");
            }
            else
            {
                return View();
            }
            
        }
    }
}
