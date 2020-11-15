using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class SupplierCreditRestService
    {

        #region Supplier Credit Account Rest Services

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

        #endregion

        #region Supplier Credit Invoice Rest Services

        public static async Task<SupplierCreditInvoice> CreateSupplierCreditInvoiceAsync(IRestDataMapper mapper, SupplierCreditInvoice data)
        {
            SupplierCreditInvoice reds = new SupplierCreditInvoice();
            if (mapper == null)
                return reds;
            string url = "suppliercreditac/api/v1/suppliercri";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<SupplierCreditInvoice> updateSupplierCreditInvoiceAsync(IRestDataMapper mapper, SupplierCreditInvoice data)
        {
            SupplierCreditInvoice reds = new SupplierCreditInvoice();
            if (mapper == null)
                return reds;
            string url = $"suppliercreditac/api/v1/suppliercri/{data.id}";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<SupplierCreditInvoice> GetCustomerByIdAndBranchIdAsync(IRestDataMapper mapper, long puid)
        {
            SupplierCreditInvoice reds = new SupplierCreditInvoice();
            if (mapper == null)
                return reds;
            string url = $"suppliercreditac/api/v1/suppliercri/inv/{puid}";
            reds = await mapper.GetDataAsync<SupplierCreditInvoice>(url);
            return reds;
        }

        #endregion
    }
}
