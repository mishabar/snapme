using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string[]> CreateCustomer(string email, string description, string sourceToken);

        Task<string> CreateCharge(string customerId, string sourceId, int amount, string description);
    }
}
