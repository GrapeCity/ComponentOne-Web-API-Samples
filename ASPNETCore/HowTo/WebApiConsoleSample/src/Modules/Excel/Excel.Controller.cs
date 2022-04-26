using System;

namespace WebApiConsoleSample
{

    public class ExcelController : BaseController<ExcelService>
    {

        public ExcelController(ExcelService service) : base(service)
        {

        }

        public override void Run()
        {
            //SplitExcel();
            //FindText();
            //ReplaceText();
            //GenerateExcelFromJSON();
            GenerateExcelFromXML();
        }

        private void SplitExcel()
        {
            Console.WriteLine("\n========= SplitExcel started ===========\n");

            SplitExcelDto dto = new SplitExcelDto();
            dto.ExcelPath = "ExcelRoot/FlexGrid.xlsx";
            dto.OutputPath = "ExcelRoot/OutputFiles";
            dto.OutputNames = new string[2] { "Sheet1.xlsx", "Sheet2.xlsx" };
            service.Split(dto).Wait();

            Console.WriteLine("\n========= SplitExcel ended =============\n");
        }

        private void FindText()
        {

            Console.WriteLine("\n========= FindText started ===========\n");

            FindTextDto dto = new FindTextDto();
            dto.ExcelPath = "ExcelRoot/FlexGrid.xlsx";
            dto.Text = "Japan";
            dto.WholeCell = false;
            dto.MatchCase = false;
            dto.SheetName = null;

            service.FindText(dto).Wait();

            Console.WriteLine("\n========= FindText ended =============\n");
        }

        private void ReplaceText()
        {
            Console.WriteLine("\n========= ReplaceText started ===========\n");

            ReplaceTextDto dto = new ReplaceTextDto();
            dto.ExcelPath = "ExcelRoot/FlexGrid.xlsx";
            dto.Text = "Japan";
            dto.NewText = "JAPAN";
            dto.WholeCell = false;
            dto.MatchCase = false;
            dto.SheetName = null;

            service.ReplaceText(dto).Wait();

            Console.WriteLine("\n========= ReplaceText ended =============\n");
        }

        private void GenerateExcelFromJSON()
        {
            Console.WriteLine("\n========= GenerateExcelFromJSON started ===========\n");

            GenerateExcelFromJSONDto dto = new GenerateExcelFromJSONDto();
            dto.Data = "[{\"id\":0,\"date\":\"2015-06-24T16:00:00.000Z\",\"time\":\"2015-08-18T18:50:00.000Z\",\"country\":\"Japan\",\"countryMapped\":3,\"downloads\":436,\"sales\":4314.523264765739,\"expenses\":4330.620157998055,\"checked\":true},{\"id\":1,\"date\":\"2015-02-28T16:00:00.000Z\",\"time\":\"2015-05-19T21:01:00.000Z\",\"country\":\"Italy\",\"countryMapped\":4,\"downloads\":676,\"sales\":2558.132621925324,\"expenses\":1959.2562899924815,\"checked\":false},{\"id\":2,\"date\":\"2015-07-04T16:00:00.000Z\",\"time\":\"2015-03-20T02:19:00.000Z\",\"country\":\"Germany\",\"countryMapped\":1,\"downloads\":488,\"sales\":6382.134975865483,\"expenses\":4719.182880362496,\"checked\":false},{\"id\":3,\"date\":\"2015-02-08T16:00:00.000Z\",\"time\":\"2015-10-05T08:52:00.000Z\",\"country\":\"Italy\",\"countryMapped\":4,\"downloads\":964,\"sales\":7840.539840981364,\"expenses\":375.3066179342568,\"checked\":false},{\"id\":4,\"date\":\"2015-03-14T16:00:00.000Z\",\"time\":\"2015-10-04T09:20:00.000Z\",\"country\":\"Italy\",\"countryMapped\":4,\"downloads\":706,\"sales\":6771.57775266096,\"expenses\":4210.299032274634,\"checked\":false}]";
            dto.FileName = "excel";
            dto.Type = "xlsx";
            dto.TemplateFileName = "ExcelRoot\\Templates\\JSONDataTemplate.xlsx";
            service.GenerateExcelFromJSON(dto).Wait();

            Console.WriteLine("\n========= GenerateExcelFromJSON ended =============\n");
        }

        private void GenerateExcelFromXML()
        {
            Console.WriteLine("\n========= GenerateExcelFromXML started ===========\n");

            GenerateExcelFromXMLDto dto = new GenerateExcelFromXMLDto();
            dto.DataFileName = "ExcelRoot\\10rowsdata.xml";
            dto.FileName = "excel";
            dto.Type = "xlsx";
            dto.TemplateFileName = "ExcelRoot\\Templates\\XmlDataTemplate.xlsx";
            service.GenerateExcelFromXML(dto).Wait();

            Console.WriteLine("\n========= GenerateExcelFromXML ended =============\n");
        }

    }

}