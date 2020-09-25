using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class SupplierCreditAccountRestService
    {

        public static async Task<SupplierCreditAccount> CreateSupplierCreditAccountAsync(IRestDataMapper mapper, SupplierCreditAccount data)
        {
            SupplierCreditAccount reds = new SupplierCreditAccount();
            if (mapper == null)
                return reds;
            string url = "suppliercreditac/api/v1/suppliercac";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<SupplierCreditAccount> updateSupplierCreditAccountAsync(IRestDataMapper mapper, SupplierCreditAccount data)
        {
            SupplierCreditAccount reds = new SupplierCreditAccount();
            if (mapper == null)
                return reds;
            string url = $"suppliercreditac/api/v1/suppliercac/{data.id}";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }
    }
}
