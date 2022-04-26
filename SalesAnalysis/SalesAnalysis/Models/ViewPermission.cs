using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using C1.Web.Api.DataEngine.Models;

namespace SalesAnalysis.Models
{
    public class ViewPermission
    {
        private static readonly Lazy<IDictionary<string, ViewPermission>> _all = 
            new Lazy<IDictionary<string, ViewPermission>>(InitAll);

        public ViewPermission(string viewName)
        {
            ViewName = viewName;
            Roles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            FieldPermissions = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        }

        public string ViewName
        {
            get;
        }

        public IDictionary<string, HashSet<string>> FieldPermissions
        {
            get;
        }

        public HashSet<string> Roles
        {
            get;
        }

        public static IDictionary<string, ViewPermission> All
        {
            get
            {
                return _all.Value;
            }
        }

        internal static IEnumerable<Field> VerifyFields(string viewName, IEnumerable<Field> raw, IPrincipal principal)
        {
            if(principal == null || raw == null)
            {
                return null;
            }

            ViewPermission permission;
            if (!All.TryGetValue(viewName, out permission))
            {
                return null;
            }

            var resultFields = new List<Field>();
            foreach(var field in raw)
            {
                HashSet<string> roles;
                if (!permission.FieldPermissions.TryGetValue(field.Header, out roles))
                {
                    roles = permission.Roles;
                }

                if(roles.Any(r=>principal.IsInRole(r)))
                {
                    resultFields.Add(field);
                }
            }

            return resultFields.AsReadOnly();
        }

        private static IDictionary<string, ViewPermission> InitAll()
        {
            var all = new Dictionary<string, ViewPermission>(StringComparer.OrdinalIgnoreCase);
            var db = new SalesEntities();
            var aspNetRoles = new Dictionary<string, string>();
            foreach(var role in db.AspNetRoles)
            {
                aspNetRoles.Add(role.Id, role.Name);
            }

            foreach(var permission in db.RolePermissions)
            {
                var names = permission.ViewFieldName.Split('.');
                var viewName = names[0];
                ViewPermission viewPermission;
                if(!all.TryGetValue(viewName, out viewPermission))
                {
                    viewPermission = new ViewPermission(viewName);
                    all[viewName] = viewPermission;
                }

                HashSet<string> roles;
                if (names.Length > 1)
                {
                    roles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    viewPermission.FieldPermissions[names[1]] = roles;
                }
                else
                {
                    roles = viewPermission.Roles;
                    roles.Clear();
                }

                var pRoles = permission.Roles.Split(',').Select(p=>aspNetRoles[p]);
                foreach(var r in pRoles)
                {
                    roles.Add(r);
                }
            }

            return all;
        }
    }
}