using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class SalesRestService
    {

        #region Sale Invoice Rest Services

        public static async Task<SaleInvoice> CreateSaleAsync(IRestDataMapper mapper, SaleInvoice data)
        {
            SaleInvoice reds = new SaleInvoice();
            if (mapper == null)
                return reds;
            string url = "selling/api/v1/sales";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<SaleInvoice> UpdateSaleAsync(IRestDataMapper mapper, SaleInvoice data)
        {
            SaleInvoice reds = new SaleInvoice();
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeleteSaleAsync(IRestDataMapper mapper, int piId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/{piId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<SaleInvoice> GetAllSaleByIdAsync(IRestDataMapper mapper, int bid, long id)
        {
            SaleInvoice data = new SaleInvoice();
            if (mapper == null)
                return data;
            string url = $"selling/api/v1/sales/inv/{bid}/{id}";
            data = await mapper.GetDataAsync<SaleInvoice>(url);
            return data;
        }

        public static async Task<SaleInvoice> GetAllSaleByInvoiceNoAsync(IRestDataMapper mapper, int bid, string inv)
        {
            SaleInvoice data = new SaleInvoice();
            if (mapper == null)
                return data;
            string url = $"selling/api/v1/sales/invNo/{bid}/{inv}";
            data = await mapper.GetDataAsync<SaleInvoice>(url);
            return data;
        }

        public static async Task<ObservableCollection<SaleInvoice>> GetSaleBySaleDateAsync(IRestDataMapper mapper, int bid, DateTime date)
        {
            ObservableCollection<SaleInvoice> reds = new ObservableCollection<SaleInvoice>();
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/invDate/{bid}/{date}";
            reds = await mapper.GetDataAsync<ObservableCollection<SaleInvoice>>(url);
            return reds;
        }

        public static async Task<ObservableCollection<SaleInvoice>> GetSaleBySaleDateAndUserIdAsync(IRestDataMapper mapper, int bid, DateTime date, int uid)
        {
            ObservableCollection<SaleInvoice> reds = new ObservableCollection<SaleInvoice>();
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/invDate/{bid}/{date}/{uid}";
            reds = await mapper.GetDataAsync<ObservableCollection<SaleInvoice>>(url);
            return reds;
        }

        public static async Task<ObservableCollection<SaleInvoice>> GetSaleBySaleTypeAsync(IRestDataMapper mapper, int bid, string stype)
        {
            ObservableCollection<SaleInvoice> reds = new ObservableCollection<SaleInvoice>();
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/invType/{bid}/{stype}";
            reds = await mapper.GetDataAsync<ObservableCollection<SaleInvoice>>(url);
            return reds;
        }

        public static async Task<ObservableCollection<SaleInvoice>> GetAllSaleByCustomerIdAsync(IRestDataMapper mapper, int bid, long id)
        {
            ObservableCollection<SaleInvoice> data = new ObservableCollection<SaleInvoice>();
            if (mapper == null)
                return data;
            string url = $"selling/api/v1/sales/customer/{bid}/{id}";
            data = await mapper.GetDataAsync<ObservableCollection<SaleInvoice>>(url);
            return data;
        }

        public static async Task<ObservableCollection<SaleInvoice>> GetAllSaleByCustomerIdAndDateAsync(IRestDataMapper mapper, int bid, long cid, DateTime date)
        {
            ObservableCollection<SaleInvoice> reds = new ObservableCollection<SaleInvoice>();
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/customer/{bid}/{date}/{cid}";
            reds = await mapper.GetDataAsync<ObservableCollection<SaleInvoice>>(url);
            return reds;
        }

        public static async Task<ObservableCollection<SaleInvoice>> GetSaleByDayAndMonthAsync(IRestDataMapper mapper, int bid, int day, int month)
        {
            ObservableCollection<SaleInvoice> reds = new ObservableCollection<SaleInvoice>();
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/daymonths/{bid}/{day}/{month}";
            reds = await mapper.GetDataAsync<ObservableCollection<SaleInvoice>>(url);
            return reds;
        }

        public static async Task<ObservableCollection<SaleInvoice>> GetSaleByMonthAndYearAsync(IRestDataMapper mapper, int bid, int month, int year)
        {
            ObservableCollection<SaleInvoice> reds = new ObservableCollection<SaleInvoice>();
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/monthyears/{bid}/{month}/{year}";
            reds = await mapper.GetDataAsync<ObservableCollection<SaleInvoice>>(url);
            return reds;
        }

        public static async Task<ObservableCollection<SaleInvoice>> GetSaleByYearAsync(IRestDataMapper mapper, int bid, int year)
        {
            ObservableCollection<SaleInvoice> reds = new ObservableCollection<SaleInvoice>();
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/years/{bid}/{year}";
            reds = await mapper.GetDataAsync<ObservableCollection<SaleInvoice>>(url);
            return reds;
        }

        #endregion

        #region Sale Item Rest Services

        public static async Task<SaleItem> CreateSaleItemAsync(IRestDataMapper mapper, SaleItem data)
        {
            SaleItem reds = new SaleItem();
            if (mapper == null)
                return reds;
            string url = "selling/api/v1/sales/item";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<SaleItem> UpdateSaleItemAsync(IRestDataMapper mapper, SaleItem data)
        {
            SaleItem reds = new SaleItem();
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/item/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeleteSaleItemAsync(IRestDataMapper mapper, int siId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/item/{siId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<ObservableCollection<SaleItem>> GetAllSaleItemByInvoiceIdAsync(IRestDataMapper mapper, int bid, long invId)
        {
            ObservableCollection<SaleItem> data = new ObservableCollection<SaleItem>();
            if (mapper == null)
                return data;
            string url = $"selling/api/v1/sales/item/inv/{bid}/{invId}";
            data = await mapper.GetDataAsync<ObservableCollection<SaleItem>>(url);
            return data;
        }

        public static async Task<ObservableCollection<SaleItem>> GetSaleItemBySaleDateAsync(IRestDataMapper mapper, int bid, DateTime slDate)
        {
            ObservableCollection<SaleItem> reds = new ObservableCollection<SaleItem>();
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/item/{bid}/{slDate}";
            reds = await mapper.GetDataAsync<ObservableCollection<SaleItem>>(url);
            return reds;
        }

        public static async Task<ObservableCollection<SaleItem>> GetSaleItemByItemIdAsync(IRestDataMapper mapper, int bid, long itemId)
        {
            ObservableCollection<SaleItem> reds = new ObservableCollection<SaleItem>();
            if (mapper == null)
                return reds;
            string url = $"selling/api/v1/sales/item/items/{bid}/{itemId}";
            reds = await mapper.GetDataAsync<ObservableCollection<SaleItem>>(url);
            return reds;
        }

        #endregion
    }
}
