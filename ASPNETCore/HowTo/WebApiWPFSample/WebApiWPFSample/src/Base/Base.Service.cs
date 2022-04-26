using System.Net.Http;

namespace WebApiConsoleSample
{
    public class BaseService
    {
        public HttpClient httpClient;

        public BaseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

    }
}