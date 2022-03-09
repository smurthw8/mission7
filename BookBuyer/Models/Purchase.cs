using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookBuyer.Models
{
    public class Purchase
    {
        [Key]
        [BindNever]//doens't get passed trough url, can't be bound to form
        public int PurchaseID { get; set; }

        [BindNever]
        public ICollection<BasketItem> Lines { get; set; }

        [Required(ErrorMessage = "Please Enter a Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter an address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter an City Name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter an State Name")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Please Enter an Country Name")]
        public string Country { get; set; }

        //BindNeverAttribute means not in form/passed through url
        [BindNever]
        public bool Shipped { get; set; }
    }
}
