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
        public async Task<string[]> CreateCustomer(string email, string description, string sourceToken)
        {
            var stripeCustomer = await Task.Run<StripeCustomer>(() =>
            {
                StripeCustomerService stripeService = new StripeCustomerService();
                return stripeService.Create(new StripeCustomerCreateOptions { Email = email, Description = description, Source = new StripeSourceOptions { TokenId = sourceToken } });
            });

            return new string[] { stripeCustomer.Id, stripeCustomer.SourceList.Data.First().Id };
        }


        public async Task<string> CreateCharge(string customerId, string sourceId, int amount, string description)
        {
            var charge = await Task.Run<StripeCharge>(() => 
            {
                var chargeService = new StripeChargeService();
                StripeChargeCreateOptions options = new StripeChargeCreateOptions
                {
                    Amount = amount,
                    Currency = "AUD",
                    CustomerId = customerId,
                    Capture = false,
                    Description = description,
                    Source = new StripeSourceOptions { TokenId = sourceId }
                };
                return chargeService.Create(options);
            });
            

            return charge.Id;
        }
    }
}
