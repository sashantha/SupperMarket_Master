using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class BankRestService
    {

        #region Bank Rest Service

        public static async Task<Bank> CreateBankAsync(IRestDataMapper mapper, Bank data)
        {
            Bank reds = new Bank();
            if (mapper == null)
                return reds;
            string url = "bank/api/v1/banks";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<Bank> UpdateBankAsync(IRestDataMapper mapper, Bank data)
        {
            Bank reds = new Bank();
            if (mapper == null)
                return reds;
            string url = $"bank/api/v1/banks/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<ObservableCollection<Bank>> GetAllBankAsync(IRestDataMapper mapper)
        {
            ObservableCollection<Bank> data = new ObservableCollection<Bank>();
            if (mapper == null)
                return data;
            string url = "bank/api/v1/banks";
            data = await mapper.GetDataAsync<ObservableCollection<Bank>>(url);
            return data;
        }

        #endregion

    }
}
