using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiConsoleSample
{
    public class BarCodeService : BaseService
    {
        public BarCodeService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task GenerateBarCode(GenerateBarCodeDto dto)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            string url = String.Join("", ConfigService.Instance().WebApiServiceUrl, Cmd.BarCode.GENERATE);
            url += "?" + new HttpParams()
                .Append("type", dto.Type)
                .Append("text", dto.Text)
                .Append("BackColor", dto.BackColor)
                .Append("ForeColor", dto.ForeColor)
                .Append("CodeType", dto.CodeType)
                .Append("CaptionAlignment", dto.CaptionAlignment)
                .Append("CaptionPosition", dto.CaptionPosition)
                .Append("CheckSumEnabled", dto.CheckSumEnabled ? "true" : "false")
                .ToString();


            var response = await httpClient.GetAsync(new UriBuilder(url).Uri);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                Console.WriteLine("Operation completed with error {0}", await response.Content.ReadAsStringAsync());
                return;
            }

            var path = Path.GetFullPath(String.Format("{0}barcode.{1}", "Output\\BarCode\\", dto.Type));
            if (File.Exists(path))
            {
                int index = 1;
                while (File.Exists(path))
                {
                    path = Path.GetFullPath(String.Format("{0}barcode({1}).{2}", "Output\\BarCode\\", index, dto.Type));
                    index += 1;
                }
            }

            var directoryPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                await response.Content.CopyToAsync(fileStream).ContinueWith(
                    (copyTask) =>
                    {
                        fileStream.Close();
                        Console.WriteLine("BarCode successfuly generated at {0}", path);
                    });
            }
            catch
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }

                throw;
            }

        }
    }
}