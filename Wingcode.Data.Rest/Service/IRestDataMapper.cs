using System.Threading.Tasks;

namespace Wingcode.Data.Rest.Service
{
    public interface IRestDataMapper
    {
        Task<T> GetDataAsync<T>(string url) where T : class;

        Task<T> PostDataAsync<T>(string url, T data) where T : class;

        Task<T> PutDataAsync<T>(string url, T data) where T : class;

        Task<T> DeleteDataAsync<T>(string url) where T : struct;
    }
}
