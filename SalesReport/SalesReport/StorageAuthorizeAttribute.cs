using C1.Web.Api.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using C1.Web.Api.Storage;

namespace SalesReport.Controllers
{
    internal class StorageAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var principal = actionContext.ControllerContext.RequestContext.Principal;
            if (principal == null || principal.Identity == null || !principal.Identity.IsAuthenticated)
            {
                Unauthorize(actionContext);
                return;
            }

            var values = actionContext.RequestContext.RouteData.Values;
            object pathObj;
            if (!values.TryGetValue("path", out pathObj))
            {
                return;
            }

            var path = (pathObj as string) ?? string.Empty;
            var defaultProvider = ReportProviderManager.Current.Get("") as FlexReportProvider;
            if (defaultProvider == null)
            {
                return;
            }

            var roles = GetRoles(defaultProvider.Storage, path);
            if(!roles.Any())
            {
                return;
            }

            if (!roles.Any(r => principal.IsInRole(r)))
            {
                Unauthorize(actionContext);
            }
        }

        private static readonly object _locker = new object();
        private static readonly IDictionary<string, IEnumerable<string>> _folderRoles = 
            new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase);
        private static IEnumerable<string> GetRoles(IStorageProvider storage, string path)
        {
            string folder = path;
            var fileStorage = storage.GetFileStorage(path);
            if (fileStorage.Exists)
            {
                var pathParts = path.Split('/');
                pathParts = pathParts.Take(pathParts.Length - 1).ToArray();
                folder = string.Join("/", pathParts);
            }

            lock (_locker)
            {
                IEnumerable<string> roles;
                if (_folderRoles.TryGetValue(folder, out roles))
                {
                    return roles;
                }

                var roleList = GetFolderRoles("", storage);
                var folderParts = folder.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                var currentFolder = "";
                foreach (var part in folderParts)
                {
                    currentFolder += part;
                    var current = GetFolderRoles(currentFolder, storage);
                    if(current != null && current.Any())
                    {
                        roleList = current;
                    }

                    currentFolder += "/";
                }

                return roleList;
            }
        }

        private static IEnumerable<string> GetFolderRoles(string path, IStorageProvider storage)
        {
            IEnumerable<string> roles;
            if (_folderRoles.TryGetValue(path, out roles))
            {
                return roles;
            }

            var roleList = new List<string>();
            var rolesFile = storage.GetFileStorage(path + "/_.roles");
            if(rolesFile.Exists)
            {
                using (var reader = new StreamReader(rolesFile.Read()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            roleList.Add(line);
                        }
                    }
                }
            }

            _folderRoles.Add(path, roleList);
            return roleList;
        }

        private static void Unauthorize(HttpActionContext actionContext)
        {
            actionContext.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}