using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookBuyer.Models;

namespace BookBuyer.Controllers
{
    public class HomeController : Controller
    {

        private IBookBuyerRepository repo;


        public HomeController(IBookBuyerRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            var test = repo.Books.ToList();

            return View(test);
        }

        public IActionResult Privacy()
        {
            return View();
        }
     
 

    }
}
