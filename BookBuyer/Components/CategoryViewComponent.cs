using System;
using System.Linq;
using BookBuyer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBuyer.Components
{
    //inherit from view components
    public class CategoryViewComponent : ViewComponent
    {
        private IBookBuyerRepository repo { get; set; }

        //when class build, need to bring in Irepository called temp
        public CategoryViewComponent(IBookBuyerRepository temp)
        {
            repo = temp;

        }
        public IViewComponentResult Invoke()
        {
            //? > makes something "nullable" > ok if it's null
            ViewBag.SelectedCategory = RouteData?.Values["bookCategory"];

            //bring in repo, select distinct colomns entries to filter by
            var categories = repo.Books.Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);
            //return thing to filterby to view
            return View(categories);
        }
    }
}
