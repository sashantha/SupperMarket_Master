using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class UserRestService
    {

        #region User Rest Service

        public static async Task<User> CreateUserAsync(IRestDataMapper mapper, User data)
        {
            User reds = new User();
            if (mapper == null)
                return reds;
            string url = "user/api/v1/users";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<User> UpdateUserAsync(IRestDataMapper mapper, User data)
        {
            User reds = new User();
            if (mapper == null)
                return reds;
            string url = $"user/api/v1/users/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeleteUserAsync(IRestDataMapper mapper, int userId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"user/api/v1/users/{userId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<ObservableCollection<User>> GetAllUserAsync(IRestDataMapper mapper)
        {
            ObservableCollection<User> data = new ObservableCollection<User>();
            if (mapper == null)
                return data;
            string url = "user/api/v1/users";
            data = await mapper.GetDataAsync<ObservableCollection<User>>(url);
            return data;
        }

        public static async Task<ObservableCollection<User>> GetUserByBranchIdAsync(IRestDataMapper mapper, int branchId)
        {
            ObservableCollection<User> data = new ObservableCollection<User>();
            if (mapper == null)
                return data;
            string url = $"user/api/v1/users/{branchId}";
            data = await mapper.GetDataAsync<ObservableCollection<User>>(url);
            return data;
        }

        public static async Task<User> GetUserByParamAndBranchIdAsync(IRestDataMapper mapper, string namOrMail, int branchId)
        {
            User reds = new User();
            if (mapper == null)
                return reds;
            string url = $"user/api/v1/users/{namOrMail}/{branchId}";
            reds = await mapper.GetDataAsync<User>(url);
            return reds;
        } 

        #endregion
    }
}
