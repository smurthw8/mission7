using System;
using System.Text.Json.Serialization;
using BookBuyer.Infastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BookBuyer.Models
{
    public class SessionBasket : Basket
    {

        public static Basket GetBasket(IServiceProvider services)
        {
            //? means nullable feild, session
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //if session is started check for and use, if not, create new session
            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket();

            basket.Session = session;

            return basket;
        }

        [JsonIgnore]
        public ISession Session {get; set;}

        public override void AddItem(Books book, int qty)
        {
            base.AddItem(book, qty);
            //use this instance that has been created
            Session.SetJson("Basket", this);
        }

        public override void RemoveItem(Books book)
        {
            base.RemoveItem(book);
            Session.SetJson("Basket", this);
        }

        public override void ClearBasket()
        {
            base.ClearBasket();
            //release from session basket and json
            Session.Remove("Basket");
        }

    }
}
