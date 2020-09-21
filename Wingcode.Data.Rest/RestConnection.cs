using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Wingcode.Data.Rest
{
    public class RestConnection
    {
        private static readonly RestConnection connection = new RestConnection();

        public string BaseUrl { get; set; }
        public string TestUrl { get; set; }

        public RestConnection()
        {
            BaseUrl = "http://localhost:8080/";            
            TestUrl = $"{BaseUrl}api/v1";
        }

        public static RestConnection GetRestConnection()
        {
            return connection;
        }

        public bool TestConnectionAsync()
        {
            try
            {
                var requ = WebRequest.Create(TestUrl) as HttpWebRequest;
                var res = requ.GetResponse() as HttpWebResponse;
                return res.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
