using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Service;

namespace Wingcode.Data.Rest
{
    public class RestDataMapper : IRestDataMapper
    {
        private HttpClient httpClient;
        private RestConnection connection;

        public RestDataMapper()
        {
            connection = RestConnection.GetRestConnection();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(connection.BaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<T> DeleteDataAsync<T>(string url) where T : struct
        {

            T reads = default;
            if (!connection.TestConnectionAsync())
                return reads;
            var response = await httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                reads = await response.Content.ReadAsAsync<T>();
            }            
            return reads;
        }

        public async Task<T> GetDataAsync<T>(string url) where T : class
        {
            T reads = default;
            if (!connection.TestConnectionAsync())
                return reads;
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                reads = await response.Content.ReadAsAsync<T>();
            }          
            return reads;
        }
               

        public async Task<T> PostDataAsync<T>(string url, T data) where T : class
        {
            T reads = default;
            if (!connection.TestConnectionAsync())
                return reads;
            var response = await httpClient.PostAsJsonAsync<T>(url, data);
            if (response.IsSuccessStatusCode)
            {
                reads = await response.Content.ReadAsAsync<T>();
            } 
            return reads;
        }

        public async Task<T> PutDataAsync<T>(string url, T data) where T : class
        {
            T reads = default;
            if (!connection.TestConnectionAsync())
                return reads;
            var response = await httpClient.PutAsJsonAsync<T>(url, data);
            if (response.IsSuccessStatusCode)
            {
                reads = await response.Content.ReadAsAsync<T>();
            }           
            return reads;
        }
    }
}
