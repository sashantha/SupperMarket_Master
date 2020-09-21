using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class BranchRestService
    {

        public static async Task<Branch> CreateBranchAsync(IRestDataMapper mapper, Branch data)
        {
            Branch reds = new Branch();
            if (mapper == null)
                return reds;
            string url = "branch/api/v1/branches";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<Branch> UpdateBranchAsync(IRestDataMapper mapper, Branch data)
        {
            Branch reds = new Branch();
            if (mapper == null)
                return reds;
            string url = $"branch/api/v1/branches/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<Branch> GetBranchByIdAsync(IRestDataMapper mapper, int branchId)
        {
            Branch reds = new Branch();
            if (mapper == null)
                return reds;
            string url = $"branch/api/v1/branches/{branchId}";
            reds = await mapper.GetDataAsync<Branch>(url);
            return reds;
        }

        public static async Task<Branch> GetBranchByCodeOrNameAsync(IRestDataMapper mapper, string flag, string paramVal)
        {
            Branch reds = new Branch();
            if (mapper == null)
                return reds;
            string url = $"branch/api/v1/branches/{flag}/{paramVal}";
            reds = await mapper.GetDataAsync<Branch>(url);
            return reds;
        }

        public static async Task<ObservableCollection<Branch>> GetAllBranchAsync(IRestDataMapper mapper)
        {
            ObservableCollection<Branch> data = new ObservableCollection<Branch>();
            if (mapper == null)
                return data;
            string url = "branch/api/v1/branches";
            data = await mapper.GetDataAsync<ObservableCollection<Branch>>(url);
            return data;
        }
    }
}
