using System;
using System.Net.Http;

namespace WebApiConsoleSample
{

    public class BarCodeController : BaseController<BarCodeService>
    {
        public BarCodeController(BarCodeService service) : base(service)
        {

        }
        public override void Run()
        {
            GenerateBarCode();
        }

        private void GenerateBarCode()
        {
            Console.WriteLine("\n========= GenerateBarCode started ===========\n");

            GenerateBarCodeDto dto = new GenerateBarCodeDto();
            dto.Type = BarCodeImageType.PNG;
            dto.Text = "1234567890";
            dto.CodeType = "Code39";
            dto.BackColor = "White";
            dto.ForeColor = "Black";
            dto.CaptionAlignment = CaptionAlignment.CENTER;
            dto.CaptionPosition = CaptionPosition.BELOW;
            dto.CheckSumEnabled = false;

            service.GenerateBarCode(dto).Wait();

            Console.WriteLine("\n========= GenerateBarCode ended =============\n");
        }
    }
}