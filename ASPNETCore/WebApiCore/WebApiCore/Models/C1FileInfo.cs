using System.Collections.Generic;
#if !ASPNETCORE
using System.Web;
#endif

namespace WebApi.Models
{
    public class C1FileInfo
    {
        public const string SLASH = "/";

        public IEnumerable<string> Directories { get; set; }

        public string Name { get; set; }

        public string GetFilePath()
        {
            var path =
            Startup.Environment.WebRootPath;
            path += SLASH;
            foreach (var directory in Directories)
            {
                path += directory + SLASH;
            }
            path += Name;
            return path;
        }
    }
}
