using SNAPME.Live.Services.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<string> CreateCustomer(string email, string description, string sourceToken)
        {
            var stripeCustomer = await Task.Run<StripeCustomer>(() =>
            {
                StripeCustomerService stripeService = new StripeCustomerService();
                return stripeService.Create(new StripeCustomerCreateOptions { Email = email, Description = description, Source = new StripeSourceOptions { TokenId = sourceToken } });
            });

            return stripeCustomer.Id;
        }
    }
}
