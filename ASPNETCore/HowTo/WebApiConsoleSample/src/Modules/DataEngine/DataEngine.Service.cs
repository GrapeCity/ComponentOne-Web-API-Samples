using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApiConsoleSample
{
    public class DataEngineService : BaseService
    {
        public DataEngineService(HttpClient httpClient) : base(httpClient)
        {

        }


        /* Gets all the fields in the data. */
        public async Task<string> GetAllFields(GetAllFieldDto dto)
        {

            httpClient.DefaultRequestHeaders.Accept.Clear();
            string url = String.Join("", ConfigService.Instance().WebApiServiceUrl, String.Format(Cmd.DataEngine.GET_ALL_FIELD, dto.DataSource));
            var stringTask = httpClient.GetStringAsync(new UriBuilder(url).Uri);
            var msg = await stringTask;
            Console.WriteLine("Response: ");
            Console.WriteLine(msg);
            return msg;
        }
        
        /* Analyze the data from the specified data source. */
        public async Task<string> AnalyzeData(AnalyzeDataDto dto){
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            string url = String.Join("", ConfigService.Instance().WebApiServiceUrl, String.Format(Cmd.DataEngine.ANALYZE_DATA, dto.DataSource));
            var content = new StringContent(dto.ViewDefinition + "\n", Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync(new UriBuilder(url).Uri, content);
            return await res.Content.ReadAsStringAsync();
        }

        /* Get the analysis result data. */
        public async Task<string> AnalyzeResult(AnalyzeResultDto dto)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            string url = String.Join("", ConfigService.Instance().WebApiServiceUrl, String.Format(Cmd.DataEngine.ANALYZE_RESULT, dto.DataSource, dto.Token));
            return await httpClient.GetStringAsync(new UriBuilder(url).Uri);
        }
        /**Get the status of the analysis. */
        public async Task<string> AnalyzeStatus(AnalyzeStatusDto dto)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            string url = String.Join("", ConfigService.Instance().WebApiServiceUrl, String.Format(Cmd.DataEngine.ANALYZE_STATUS, dto.DataSource, dto.Token));
            return await httpClient.GetStringAsync(new UriBuilder(url).Uri);
        }

    }
}