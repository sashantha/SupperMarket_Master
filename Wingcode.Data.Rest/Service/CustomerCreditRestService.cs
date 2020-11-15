using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class CustomerCreditRestService
    {
        public static async Task<CustomerCreditAccount> CreateCustomerCreditAccountAsync(IRestDataMapper mapper, CustomerCreditAccount data)
        {
            CustomerCreditAccount reds = new CustomerCreditAccount();
            if (mapper == null)
                return reds;
            string url = "customercreditac/api/v1/customercac";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<CustomerCreditAccount> updateCustomerCreditAccountAsync(IRestDataMapper mapper, CustomerCreditAccount data)
        {
            CustomerCreditAccount reds = new CustomerCreditAccount();
            if (mapper == null)
                return reds;
            string url = $"customercreditac/api/v1/customercac/{data.id}";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }
    }
}
