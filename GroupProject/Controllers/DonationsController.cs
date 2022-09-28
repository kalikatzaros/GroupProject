using System;
using System.Collections.Generic;
using Stripe;
using Stripe.Checkout;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class DonationsController : Controller
    {
        public DonationsController()
        {
            StripeConfiguration.ApiKey = "sk_test_51LIzfBLnJgNQDNAUt4xP3AxpzNojqjlmfXp4Q7DloVyI3ts3mXrNthL2ujkGIAj8nZcyxZNLhnpeOATsZsDZ4KMC00w0yWxbg1";
        }

        [HttpPost]
        public ActionResult CreateCheckoutSession()
        {
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = 100,
                            Currency = "eur",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Donation",
                            },

                        },
                        Quantity = 1,
                        },
                    },
                Mode = "payment",
                SuccessUrl = "https://localhost:44310/Checkout/Success",
                CancelUrl = "https://example.com/cancel",
            };

            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new HttpStatusCodeResult(303);
        }

    }
}