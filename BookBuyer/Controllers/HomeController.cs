using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookBuyer.Models;
using BookBuyer.Models.ViewModels;

namespace BookBuyer.Controllers
{
    public class HomeController : Controller
    {

        private IBookBuyerRepository repo;


        public HomeController(IBookBuyerRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string bookCategory, int pageNum = 1)
        {
            int pageSize = 5;

            var viewmod = new BookBuyerViewModel
            {
                //where > filters to where category = category passed in OR | if no category specified
                Books = repo.Books
                .Where(p => p.Category == bookCategory || bookCategory == null)
                .OrderBy(p => p.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    Currentpage = pageNum,
                    TotalNumBooks = (bookCategory == null ? repo.Books.Count()
                        : repo.Books.Where(x => x.Category == bookCategory).Count()),
                    BooksPerPage = pageSize
                }

            };

            return View(viewmod);
        }
     
 

    }
}
