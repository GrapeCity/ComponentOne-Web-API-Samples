using System.Collections.Generic;
#if !ASPNETCORE
using System.Web;
#endif

namespace WebApi.Models
{
    public class C1FileInfo
    {
#if !ASPNETCORE
        public const string SLASH = "/";
#else
        public const string SLASH = "\\";
#endif

        public IEnumerable<string> Directories { get; set; }

        public string Name { get; set; }

        public string GetFilePath()
        {
            var path =
#if !ASPNETCORE
            "~";
#else
            Startup.Environment.WebRootPath;
#endif
            path += SLASH;
            foreach (var directory in Directories)
            {
                path += directory + SLASH;
            }
            path += Name;
#if !ASPNETCORE
            return HttpContext.Current.Server.MapPath(path);
#else
            return path;
#endif
        }
    }
}
