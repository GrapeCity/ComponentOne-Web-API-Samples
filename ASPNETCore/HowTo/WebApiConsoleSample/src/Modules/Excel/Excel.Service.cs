using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;

namespace WebApiConsoleSample
{
    public class ExcelService : BaseService
    {
        public ExcelService(HttpClient httpClient) : base(httpClient)
        {

        }

        /**Split an excel file from storage to multiple excel files and save it into storage. */
        public async Task Split(SplitExcelDto dto)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            string url = String.Join("", ConfigService.Instance().WebApiServiceUrl, String.Format(Cmd.Excel.SPLIT, dto.ExcelPath));
            var pms = new HttpParams()
                .Append("outputpath", dto.OutputPath);
            foreach (string outputname in dto.OutputNames)
            {
                pms.Append("outputnames", outputname);
            }
            url += "?" + pms.ToString();
            var stringTask = httpClient.GetStringAsync(new UriBuilder(url).Uri);
            var msg = await stringTask;
            Console.WriteLine(msg);
        }

        /**Find text in excel, return all matches info. */
        public async Task FindText(FindTextDto dto)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            string url = String.Join("", ConfigService.Instance().WebApiServiceUrl, String.Format(Cmd.Excel.FIND_TEXT, dto.ExcelPath + (dto.SheetName != null ? ("/" + dto.SheetName) : "")));
            url += "?" + new HttpParams()
                .Append("text", dto.Text)
                .Append("wholecell", dto.WholeCell ? "true" : "false")
                .Append("matchcase", dto.MatchCase ? "true" : "false")
                .ToString();

            var stringTask = httpClient.GetStringAsync(new UriBuilder(url).Uri);
            var msg = await stringTask;
            Console.WriteLine(msg);
        }

        /**Replace text in excel. */
        public async Task ReplaceText(ReplaceTextDto dto)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            string url = String.Join("", ConfigService.Instance().WebApiServiceUrl, String.Format(Cmd.Excel.REPLACE_TEXT, dto.ExcelPath + (dto.SheetName != null ? ("/" + dto.SheetName) : "")));
            url += "?" + new HttpParams()
                .Append("text", dto.Text)
                .Append("newtext", dto.NewText)
                .Append("wholecell", dto.WholeCell ? "true" : "false")
                .Append("matchcase", dto.MatchCase ? "true" : "false")
                .ToString();

            var stringTask = httpClient.GetStringAsync(new UriBuilder(url).Uri);
            var msg = await stringTask;
            Console.WriteLine(msg);
        }


        /**Generate excel file from JSON data posted from client. */
        public async Task GenerateExcelFromJSON(GenerateExcelFromJSONDto dto)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            string url = String.Join("", ConfigService.Instance().WebApiServiceUrl, Cmd.Excel.GENERATE);
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(dto.FileName), "FileName");
            form.Add(new StringContent(dto.TemplateFileName), "TemplateFileName");
            form.Add(new StringContent(dto.Type), "type");
            form.Add(new StringContent(dto.Data), "data");

            var response = await httpClient.PostAsync(new UriBuilder(url).Uri, form);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                Console.WriteLine("Operation completed with error {0}", await response.Content.ReadAsStringAsync());
                return;
            }

            var path = Path.GetFullPath(String.Format("{0}{1}.{2}", "Output\\Excel\\", dto.FileName, dto.Type));
            if (File.Exists(path))
            {
                int index = 1;
                while (File.Exists(path))
                {
                    path = Path.GetFullPath(String.Format("{0}{1}({2}).{3}", "Output\\Excel\\", dto.FileName, index, dto.Type));
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
                        Console.WriteLine("ExcelFile successfully generated at {0}", path);
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

        /**Generate excel file from XML file posted from client. */
        public async Task GenerateExcelFromXML(GenerateExcelFromXMLDto dto)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            string url = String.Join("", ConfigService.Instance().WebApiServiceUrl, Cmd.Excel.GENERATE);
            url += "?" + new HttpParams()
                .Append("FileName", dto.FileName)
                .Append("type", dto.Type)
                .Append("TemplateFileName", dto.TemplateFileName)
                .Append("DataFileName", dto.DataFileName)
                .ToString();

            var response = await httpClient.GetAsync(new UriBuilder(url).Uri);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                Console.WriteLine("Operation completed with error {0}", await response.Content.ReadAsStringAsync());
                return;
            }

            var path = Path.GetFullPath(String.Format("{0}{1}.{2}", "Output\\Excel\\", dto.FileName, dto.Type));
            if (File.Exists(path))
            {
                int index = 1;
                while (File.Exists(path))
                {
                    path = Path.GetFullPath(String.Format("{0}{1}({2}).{3}", "Output\\Excel\\", dto.FileName, index, dto.Type));
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
                        Console.WriteLine("ExcelFile successfully generated at {0}", path);
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