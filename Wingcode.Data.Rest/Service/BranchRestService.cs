using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class BranchRestService
    {

        #region Constant Value

        public readonly string BRANCH_CODE_FLAG = "bc";
        public readonly string BRANCH_NAME_FLAG = "bn";

        #endregion

        #region Branch Rest Service

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

        #endregion

        #region Branch Account Rest

        public static async Task<BranchAccount> CreateBranchAccountAsync(IRestDataMapper mapper, BranchAccount data)
        {
            BranchAccount reds = new BranchAccount();
            if (mapper == null)
                return reds;
            string url = "branch/api/v1/brancheacs";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<BranchAccount> UpdateBranchAccountAsync(IRestDataMapper mapper, BranchAccount data)
        {
            BranchAccount reds = new BranchAccount();
            if (mapper == null)
                return reds;
            string url = $"branch/api/v1/brancheacs/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }
        
        public static async Task<ObservableCollection<BranchAccount>> GetAllBranchAccountAsync(IRestDataMapper mapper, int brId)
        {
            ObservableCollection<BranchAccount> data = new ObservableCollection<BranchAccount>();
            if (mapper == null)
                return data;
            string url = $"branch/api/v1/brancheacs/{brId}";
            data = await mapper.GetDataAsync<ObservableCollection<BranchAccount>>(url);
            return data;
        }
        #endregion
    }
}
