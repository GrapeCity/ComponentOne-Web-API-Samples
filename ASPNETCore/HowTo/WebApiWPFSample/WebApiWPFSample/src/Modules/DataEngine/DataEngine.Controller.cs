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
            dto.ViewDefinition = "{fields:[{\"binding\":\"Active\",\"dataType\":3}, {\"binding\":\"Country\",\"dataType\":1},  {\"binding\":\"Date\",\"dataType\":4},   {\"binding\":\"Discount\",\"dataType\":2},   {\"binding\":\"Downloads\",\"dataType\":2},   {\"binding\":\"ID\",\"dataType\":2},   {\"binding\":\"Product\",\"dataType\":1},   {\"binding\":\"Sales\",\"dataType\":2}],rowFields:{items:[\"Product\"]},  columnFields:{items:[\"Country\"]},  valueFields:{items:[\"Sales\"]}}";            
            var response = service.AnalyzeData(dto).Result;
            Console.WriteLine("AnalyzeData getting response: {0}", response);
            Console.WriteLine("\n========= AnalyzeData ended =============\n");

            Console.WriteLine("Delay 10 seconds to get analyze result..");

            Thread.Sleep(10000);

            AnalyzeResult(response);
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

    }
}