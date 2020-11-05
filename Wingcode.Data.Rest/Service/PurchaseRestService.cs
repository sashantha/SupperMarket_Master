using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class PurchaseRestService
    {

        #region ConstValues

        public static readonly string PI_PID_FLAG = "pur";
        public static readonly string PI_IID_FLAG = "pit";

        #endregion

        #region Purchase Rest Service

        public static async Task<Purchase> CreatePurchaseAsync(IRestDataMapper mapper, Purchase data)
        {
            Purchase reds = new Purchase();
            if (mapper == null)
                return reds;
            string url = "purchasing/api/v1/purchases";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<Purchase> UpdatePurchaseAsync(IRestDataMapper mapper, Purchase data)
        {
            Purchase reds = new Purchase();
            if (mapper == null)
                return reds;
            string url = $"purchasing/api/v1/purchases/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeletePurchaseAsync(IRestDataMapper mapper, int piId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"purchasing/api/v1/purchases/{piId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<Purchase> GetAllPurchaseByIdAsync(IRestDataMapper mapper, int bid, long id)
        {
            Purchase data = new Purchase();
            if (mapper == null)
                return data;
            string url = $"purchasing/api/v1/purchases/{bid}/{id}";
            data = await mapper.GetDataAsync<Purchase>(url);
            return data;
        }

        public static async Task<Purchase> GetAllPurchaseByInvoiceNoAsync(IRestDataMapper mapper, int bid, string inv)
        {
            Purchase data = new Purchase();
            if (mapper == null)
                return data;
            string url = $"purchasing/api/v1/purchases/Invoices/{bid}/{inv}";
            data = await mapper.GetDataAsync<Purchase>(url);
            return data;
        }

        public static async Task<ObservableCollection<Purchase>> GetAllPurchaseBySupplierIdAsync(IRestDataMapper mapper, int bid, long id)
        {
            ObservableCollection<Purchase> data = new ObservableCollection<Purchase>();
            if (mapper == null)
                return data;
            string url = $"purchasing/api/v1/purchases/suppliers/{bid}/{id}";
            data = await mapper.GetDataAsync<ObservableCollection<Purchase>>(url);
            return data;
        }

        public static async Task<ObservableCollection<Purchase>> GetPurchaseByPurchaseDateAsync(IRestDataMapper mapper, int bid, DateTime date)
        {
            ObservableCollection<Purchase> reds = new ObservableCollection<Purchase>();
            if (mapper == null)
                return reds;
            string url = $"purchasing/api/v1/purchases/dates/{bid}/{date}";
            reds = await mapper.GetDataAsync<ObservableCollection<Purchase>>(url);
            return reds;
        }

        public static async Task<ObservableCollection<Purchase>> GetPurchaseByDayAndMonthAsync(IRestDataMapper mapper, int bid, int day, int month)
        {
            ObservableCollection<Purchase> reds = new ObservableCollection<Purchase>();
            if (mapper == null)
                return reds;
            string url = $"purchasing/api/v1/purchases/daymonths/{bid}/{day}/{month}";
            reds = await mapper.GetDataAsync<ObservableCollection<Purchase>>(url);
            return reds;
        }

        public static async Task<ObservableCollection<Purchase>> GetPurchaseByMonthAndYearAsync(IRestDataMapper mapper, int bid, int month, int year)
        {
            ObservableCollection<Purchase> reds = new ObservableCollection<Purchase>();
            if (mapper == null)
                return reds;
            string url = $"purchasing/api/v1/purchases/monthyears/{bid}/{month}/{year}";
            reds = await mapper.GetDataAsync<ObservableCollection<Purchase>>(url);
            return reds;
        }

        public static async Task<ObservableCollection<Purchase>> GetPurchaseByYearAsync(IRestDataMapper mapper, int bid, int year)
        {
            ObservableCollection<Purchase> reds = new ObservableCollection<Purchase>();
            if (mapper == null)
                return reds;
            string url = $"purchasing/api/v1/purchases/years/{bid}/{year}";
            reds = await mapper.GetDataAsync<ObservableCollection<Purchase>>(url);
            return reds;
        }

        #endregion

        #region Purchase Item Rest Service

        public static async Task<PurchaseItem> CreatePurchaseItemAsync(IRestDataMapper mapper, PurchaseItem data)
        {
            PurchaseItem reds = new PurchaseItem();
            if (mapper == null)
                return reds;
            string url = "purchasing/api/v1/purchases/items";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<PurchaseItem> UpdatePurchaseItemAsync(IRestDataMapper mapper, PurchaseItem data)
        {
            PurchaseItem reds = new PurchaseItem();
            if (mapper == null)
                return reds;
            string url = $"purchasing/api/v1/purchases/items/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeletePurchaseItemAsync(IRestDataMapper mapper, int piId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"purchasing/api/v1/purchases/items/{piId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<ObservableCollection<PurchaseItem>> GetAllPurchaseItemByParamAsync(IRestDataMapper mapper, int bid, string flag, long id)
        {
            ObservableCollection<PurchaseItem> data = new ObservableCollection<PurchaseItem>();
            if (mapper == null)
                return data;
            string url = $"purchasing/api/v1/purchases/items/{bid}/{flag}/{id}";
            data = await mapper.GetDataAsync<ObservableCollection<PurchaseItem>>(url);
            return data;
        }

        public static async Task<ObservableCollection<PurchaseItem>> GetPurchaseItemByPurchaseDateAsync(IRestDataMapper mapper, int bid, DateTime purDate)
        {
            ObservableCollection<PurchaseItem> reds = new ObservableCollection<PurchaseItem>();
            if (mapper == null)
                return reds;
            string url = $"purchasing/api/v1/purchases/items/date/{bid}/{purDate}";
            reds = await mapper.GetDataAsync<ObservableCollection<PurchaseItem>>(url);
            return reds;
        }

        public static async Task<ObservableCollection<PurchaseItem>> GetPurchaseItemByExpireDateRangeAsync(IRestDataMapper mapper, int bid, DateTime sDate, DateTime eDate)
        {
            ObservableCollection<PurchaseItem> reds = new ObservableCollection<PurchaseItem>();
            if (mapper == null)
                return reds;
            string url = $"purchasing/api/v1/purchases/items/date/{bid}/{sDate}/{eDate}";
            reds = await mapper.GetDataAsync<ObservableCollection<PurchaseItem>>(url);
            return reds;
        }

        #endregion
    }
}
