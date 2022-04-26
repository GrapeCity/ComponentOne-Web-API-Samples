using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiConsoleSample
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {

            ExcelController ExcelController = new ExcelController(new ExcelService(client));
            ExcelController.Run();

            // BarCodeController BarCodeController = new BarCodeController(new BarCodeService(client));
            // BarCodeController.Run();

            // DataEngineController DataEngineController = new DataEngineController(new DataEngineService(client));
            // DataEngineController.Run();
        }

    }
}
