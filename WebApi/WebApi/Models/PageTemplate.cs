using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;

namespace WebApi.Models
{
    internal class PageTemplate
    {
        static PageTemplate()
        {
            var templateFile = IsJPCulture ? "IntroPageTemplate.ja.html" : "IntroPageTemplate.html";
            PageTemplateFile = new C1FileInfo
            {
                Directories = new List<string> { "Content" },
                Name = templateFile
            }.GetFilePath();
        }

        private static string PageTemplateFile;

        private static string _content;

        internal static bool IsJPCulture
        {
            get
            {
#if NETCOREAPP1_0
                var culture = System.Globalization.CultureInfo.CurrentCulture;
#else
                var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
#endif
                return culture.TwoLetterISOLanguageName == "ja";
            }
        }

        private static void Read()
        {
            if(_content != null)
            {
                return;
            }

            _content = File.ReadAllText(PageTemplateFile);
        }

        public static string GetAPIPage(C1APIIntroduction api)
        {
            Read();
            var content = _content;
            content = content.Replace("@Name", api.Name);
            content = content.Replace("@Abstract", api.Abstract);
            content = content.Replace("@DetailList", api.Details);
            content = content.Replace("@ServiceList", GetServiceList(api.Name));
            return content;
        }

        private static string GetServiceList(string name)
        {
            var cat = ControlsClass.ControlCatList.First(ct => ct.CatName == name);
            if (cat != null)
            {
                return GetServiceList(cat.FunctionGroups);
            }
            return string.Empty;
        }

        private static string GetServiceList(IEnumerable<FunctionGroup> functionGroups)
        {
            return GetTree(functionGroups, "serviceTree", "FunctionDescription", "Functions");
        }

        internal static string GetTree(IEnumerable source, string treeName, string binding, 
            string childItemsPath, bool needCreatdTag = false)
        {
            var sbContent = new StringBuilder();
            if (needCreatdTag)
            {
                sbContent.AppendLine(string.Format("<div id=\"{0}\"></div>", treeName));
            }
            sbContent.AppendLine("<script type=\"text/javascript\">");
            sbContent.AppendLine(string.Format("var {0}Data = {1};", treeName, JsonConvert.SerializeObject(source)));
            sbContent.AppendLine(string.Format("$(document).ready(function(){{createTree('#{0}', {0}Data, '{1}', '{2}');}});", treeName, binding, childItemsPath));
            sbContent.AppendLine("</script>");
            return sbContent.ToString();
        }
    }

    internal class C1APIIntroduction
    {
        public string Name { get; set; }

        public string Abstract { get; set; }

        public string Details { get; set; }
    }

    internal class DataEngineKey
    {
        public string Name { get; set; }
    }
}