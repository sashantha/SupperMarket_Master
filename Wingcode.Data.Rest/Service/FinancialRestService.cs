using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class FinancialRestService
    {

        #region Cash Transaction Rest Services

        public static async Task<CashBook> CreateCashBookAsync(IRestDataMapper mapper, CashBook data)
        {
            CashBook reds = new CashBook();
            if (mapper == null)
                return reds;
            string url = "cashbook/api/v1/cashbooks";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<CashBook> UpdateCashBookAsync(IRestDataMapper mapper, CashBook data)
        {
            CashBook reds = new CashBook();
            if (mapper == null)
                return reds;
            string url = $"cashbook/api/v1/cashbooks/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeleteCashBookAsync(IRestDataMapper mapper, long cbId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"cashbook/api/v1/cashbooks/{cbId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<ObservableCollection<CashBook>> GetAllCashBookAsync(IRestDataMapper mapper, DateTime trdate)
        {
            ObservableCollection<CashBook> data = new ObservableCollection<CashBook>();
            if (mapper == null)
                return data;
            string url = $"cashbook/api/v1/cashbooks/trd/{trdate}";
            data = await mapper.GetDataAsync<ObservableCollection<CashBook>>(url);
            return data;
        }

        public static async Task<ObservableCollection<CashBook>> GetCashBookByBranchIdAsync(IRestDataMapper mapper, string dect)
        {
            ObservableCollection<CashBook> data = new ObservableCollection<CashBook>();
            if (mapper == null)
                return data;
            string url = $"cashbook/api/v1/cashbooks/dec/{dect}";
            data = await mapper.GetDataAsync<ObservableCollection<CashBook>>(url);
            return data;
        }

        /*        public static async Task<CashBook> GetCashBookByIdAndBranchIdAsync(IRestDataMapper mapper, long cusId, int branchId)
                {
                    CashBook reds = new CashBook();
                    if (mapper == null)
                        return reds;
                    string url = $"cashbook/api/v1/cashbooks/{cusId}/{branchId}";
                    reds = await mapper.GetDataAsync<CashBook>(url);
                    return reds;
                }

                public static async Task<CashBook> GetCashBookByParamAndBranchIdAsync(IRestDataMapper mapper, string flag, string codeOrName, int branchId)
                {
                    CashBook reds = new CashBook();
                    if (mapper == null)
                        return reds;
                    string url = $"cashbook/api/v1/cashbooks/{flag}/{codeOrName}/{branchId}";
                    reds = await mapper.GetDataAsync<CashBook>(url);
                    return reds;
                }
        */
        #endregion

        #region Cheque Transaction Rest Services

        public static async Task<ChequeBook> CreateChequeBookAsync(IRestDataMapper mapper, ChequeBook data)
        {
            ChequeBook reds = new ChequeBook();
            if (mapper == null)
                return reds;
            string url = "chequebook/api/v1/chequebooks";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<ChequeBook> UpdateChequeBookAsync(IRestDataMapper mapper, ChequeBook data)
        {
            ChequeBook reds = new ChequeBook();
            if (mapper == null)
                return reds;
            string url = $"chequebook/api/v1/chequebooks/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeleteChequeBookAsync(IRestDataMapper mapper, long cbId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"chequebook/api/v1/chequebooks/{cbId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<ObservableCollection<ChequeBook>> GetAllChequeBookAsync(IRestDataMapper mapper, DateTime trdate)
        {
            ObservableCollection<ChequeBook> data = new ObservableCollection<ChequeBook>();
            if (mapper == null)
                return data;
            string url = $"chequebook/api/v1/chequebooks/trd/{trdate}";
            data = await mapper.GetDataAsync<ObservableCollection<ChequeBook>>(url);
            return data;
        }

        public static async Task<ObservableCollection<ChequeBook>> GetChequeBookByBranchIdAsync(IRestDataMapper mapper, string dect)
        {
            ObservableCollection<ChequeBook> data = new ObservableCollection<ChequeBook>();
            if (mapper == null)
                return data;
            string url = $"chequebook/api/v1/chequebooks/dec/{dect}";
            data = await mapper.GetDataAsync<ObservableCollection<ChequeBook>>(url);
            return data;
        }

        #endregion
    }
}
