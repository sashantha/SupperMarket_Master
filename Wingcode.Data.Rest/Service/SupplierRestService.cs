using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class SupplierRestService
    {

        #region Constant Value

        public readonly string SUPPLIER_CODE_FLAG = "sc";
        public readonly string SUPPLIER_NAME_FLAG = "sn";

        #endregion

        #region Supplier Rest Service

        public static async Task<Supplier> CreateSupplierAsync(IRestDataMapper mapper, Supplier data)
        {
            Supplier reds = new Supplier();
            if (mapper == null)
                return reds;
            string url = "supplier/api/v1/suppliers";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<Supplier> UpdateSupplierAsync(IRestDataMapper mapper, Supplier data)
        {
            Supplier reds = new Supplier();
            if (mapper == null)
                return reds;
            string url = $"supplier/api/v1/suppliers/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeleteSupplierAsync(IRestDataMapper mapper, long SupplierId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"supplier/api/v1/suppliers/{SupplierId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<ObservableCollection<Supplier>> GetAllSupplierAsync(IRestDataMapper mapper)
        {
            ObservableCollection<Supplier> data = new ObservableCollection<Supplier>();
            if (mapper == null)
                return data;
            string url = "supplier/api/v1/suppliers";
            data = await mapper.GetDataAsync<ObservableCollection<Supplier>>(url);
            return data;
        }

        public static async Task<ObservableCollection<Supplier>> GetSupplierByBranchIdAsync(IRestDataMapper mapper, int branchId)
        {
            ObservableCollection<Supplier> data = new ObservableCollection<Supplier>();
            if (mapper == null)
                return data;
            string url = $"supplier/api/v1/suppliers/{branchId}";
            data = await mapper.GetDataAsync<ObservableCollection<Supplier>>(url);
            return data;
        }

        public static async Task<ObservableCollection<SupplierCriteria>> GetSupplierAttribsByBranchIdAsync(IRestDataMapper mapper, int branchId)
        {
            ObservableCollection<SupplierCriteria> data = new ObservableCollection<SupplierCriteria>();
            if (mapper == null)
                return data;
            string url = $"supplier/api/v1/suppliers/attribs/{branchId}";
            data = await mapper.GetDataAsync<ObservableCollection<SupplierCriteria>>(url);
            return data;
        }

        public static async Task<Supplier> GetSupplierByIdAndBranchIdAsync(IRestDataMapper mapper, long supId, int branchId)
        {
            Supplier reds = new Supplier();
            if (mapper == null)
                return reds;
            string url = $"supplier/api/v1/suppliers/{supId}/{branchId}";
            reds = await mapper.GetDataAsync<Supplier>(url);
            return reds;
        }

        public static async Task<Supplier> GetSupplierByParamAndBranchIdAsync(IRestDataMapper mapper, string flag, string codeOrName, int branchId)
        {
            Supplier reds = new Supplier();
            if (mapper == null)
                return reds;
            string url = $"supplier/api/v1/suppliers/{flag}/{codeOrName}/{branchId}";
            reds = await mapper.GetDataAsync<Supplier>(url);
            return reds;
        } 

        #endregion
    }
}
