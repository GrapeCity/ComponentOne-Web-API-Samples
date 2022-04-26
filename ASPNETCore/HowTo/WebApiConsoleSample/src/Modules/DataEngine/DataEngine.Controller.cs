using System;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace WebApiConsoleSample
{

    public class DataEngineController : BaseController<DataEngineService>
    {
        public DataEngineController(DataEngineService service) : base(service)
        {

        }
        public override void Run()
        {
            // GetAllFields();
            AnalyzeData();
        }

        private void GetAllFields()
        {

            Console.WriteLine("\n========= GetAllFields started ===========\n");

            GetAllFieldDto dto = new GetAllFieldDto();
            dto.DataSource = "complex10";

            service.GetAllFields(dto).Wait();

            Console.WriteLine("\n========= GetAllFields ended =============\n");
        }

        private void AnalyzeData()
        {
            Console.WriteLine("\n========= AnalyzeData started ===========\n");
            AnalyzeDataDto dto = new AnalyzeDataDto();
            dto.DataSource = "complex10";
            JObject obj = new JObject();
            obj.Add("view", "{\"showZeros\":false,\"showColumnTotals\":0,\"showRowTotals\":0,\"defaultFilterType\":1,\"totalsBeforeData\":false,\"sortableGroups\":true,\"fields\":[{\"binding\":\"Active\",\"header\":\"Active\",\"dataType\":3,\"aggregate\":2,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Active\"},{\"binding\":\"Country\",\"header\":\"Country\",\"dataType\":1,\"aggregate\":2,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Country\"},{\"binding\":\"Date\",\"header\":\"Date\",\"dataType\":4,\"aggregate\":2,\"showAs\":0,\"descending\":false,\"format\":\"d\",\"isContentHtml\":false,\"key\":\"Date\"},{\"binding\":\"Discount\",\"header\":\"Discount\",\"dataType\":2,\"aggregate\":1,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Discount\"},{\"binding\":\"Downloads\",\"header\":\"Downloads\",\"dataType\":2,\"aggregate\":1,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Downloads\"},{\"binding\":\"ID\",\"header\":\"ID\",\"dataType\":2,\"aggregate\":1,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"ID\"},{\"binding\":\"Product\",\"header\":\"Product\",\"dataType\":1,\"aggregate\":2,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Product\"},{\"binding\":\"Sales\",\"header\":\"Sales\",\"dataType\":2,\"aggregate\":1,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Sales\"}],\"rowFields\":{\"items\":[\"Product\",\"Country\"]},\"columnFields\":{\"items\":[]},\"filterFields\":{\"items\":[]},\"valueFields\":{\"items\":[\"Sales\",\"Downloads\"]}}");
            dto.ViewDefinition = obj.ToString();
            var response = service.AnalyzeData(dto).Result;
            Console.WriteLine("AnalyzeData getting response: {0}", response);
            Console.WriteLine("\n========= AnalyzeData ended =============\n");

            bool IsCompleted = false;
            while (!IsCompleted)
            {
                Thread.Sleep(1000);
                string analysisStatus = AnalyzeStatus(response);
                try{
                     JObject analysisStatusObj = JObject.Parse(analysisStatus);
                     if(analysisStatusObj != null){
                         if(analysisStatusObj.ContainsKey("executingStatus") && analysisStatusObj.GetValue("executingStatus").ToString().Equals("Completed")){
                             IsCompleted = true;
                         }
                     }
                }catch(Exception e){
                    Console.WriteLine(e.StackTrace);
                    IsCompleted = true;
                }
            }

            AnalyzeResult(response);
            return;
        }


        private void AnalyzeResult(string analyDataResponse)
        {
            try
            {
                Console.WriteLine("\n========= AnalyzeResult started ===========\n");
                JObject obj = JObject.Parse(analyDataResponse);

                AnalyzeResultDto dto = new AnalyzeResultDto();
                dto.DataSource = "complex10";
                dto.Token = obj.GetValue("token").ToString();
                var result = service.AnalyzeResult(dto).Result;
                Console.WriteLine("Analyzed with result {0}", result);
                Console.WriteLine("\n========= AnalyzeResult ended =============\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private string AnalyzeStatus(string analyDataResponse)
        {
            try
            {
                Console.WriteLine("\n========= AnalyzeStatus started ===========\n");
                JObject obj = JObject.Parse(analyDataResponse);

                AnalyzeStatusDto dto = new AnalyzeStatusDto();
                dto.DataSource = "complex10";
                dto.Token = obj.GetValue("token").ToString();
                var result = service.AnalyzeStatus(dto).Result;
                Console.WriteLine("AnalyzeData getting response {0}", result);
                Console.WriteLine("\n========= AnalyzeStatus ended =============\n");
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}