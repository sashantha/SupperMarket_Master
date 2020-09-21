using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class CustomerRestService
    {

        public static async Task<Customer> CreateCustomerAsync(IRestDataMapper mapper, Customer data)
        {
            Customer reds = new Customer();
            if (mapper == null)
                return reds;
            string url = "customer/api/v1/customers";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<Customer> UpdateCustomerAsync(IRestDataMapper mapper, Customer data)
        {
            Customer reds = new Customer();
            if (mapper == null)
                return reds;
            string url = $"customer/api/v1/customers/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeleteCustomerAsync(IRestDataMapper mapper, long customerId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"customer/api/v1/customers/{customerId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<ObservableCollection<Customer>> GetAllCustomerAsync(IRestDataMapper mapper)
        {
            ObservableCollection<Customer> data = new ObservableCollection<Customer>();
            if (mapper == null)
                return data;
            string url = "customer/api/v1/customers";
            data = await mapper.GetDataAsync<ObservableCollection<Customer>>(url);
            return data;
        }

        public static async Task<ObservableCollection<Customer>> GetCustomerByBranchIdAsync(IRestDataMapper mapper, int branchId)
        {
            ObservableCollection<Customer> data = new ObservableCollection<Customer>();
            if (mapper == null)
                return data;
            string url = $"customer/api/v1/customers/{branchId}";
            data = await mapper.GetDataAsync<ObservableCollection<Customer>>(url);
            return data;
        }

        public static async Task<ObservableCollection<CustomerCriteria>> GetCustomerAttribsByBranchIdAsync(IRestDataMapper mapper, int branchId)
        {
            ObservableCollection<CustomerCriteria> data = new ObservableCollection<CustomerCriteria>();
            if (mapper == null)
                return data;
            string url = $"customer/api/v1/customers/attribs/{branchId}";
            data = await mapper.GetDataAsync<ObservableCollection<CustomerCriteria>>(url);
            return data;
        }

        public static async Task<Customer> GetCustomerByIdAndBranchIdAsync(IRestDataMapper mapper, long cusId, int branchId)
        {
            Customer reds = new Customer();
            if (mapper == null)
                return reds;
            string url = $"customer/api/v1/customers/{cusId}/{branchId}";
            reds = await mapper.GetDataAsync<Customer>(url);
            return reds;
        }

        public static async Task<Customer> GetCustomerByParamAndBranchIdAsync(IRestDataMapper mapper, string flag, string codeOrName, int branchId)
        {
            Customer reds = new Customer();
            if (mapper == null)
                return reds;
            string url = $"customer/api/v1/customers/{flag}/{codeOrName}/{branchId}";
            reds = await mapper.GetDataAsync<Customer>(url);
            return reds;
        }
    }
}
