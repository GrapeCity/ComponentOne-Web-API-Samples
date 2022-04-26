using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace SalesAnalysis
{
    internal class IgnoreControllerTypeResolver : DefaultHttpControllerTypeResolver
    {
        public IgnoreControllerTypeResolver(IEnumerable<Type> controllerTypes) 
            : base(t => IsControllerType(t, controllerTypes))
        { }

        private static bool IsControllerType(Type t, IEnumerable<Type> controllerTypes)
        {
            if (t != null
                && t.IsClass
                && (t.IsVisible && !t.IsAbstract)
                && typeof(IHttpController).IsAssignableFrom(t)
                && HasValidControllerName(t))
                return !controllerTypes.Contains(t);

            return false;
        }

        private static bool HasValidControllerName(Type controllerType)
        {
            string str = DefaultHttpControllerSelector.ControllerSuffix;
            if (controllerType.Name.Length > str.Length)
                return controllerType.Name.EndsWith(str, StringComparison.OrdinalIgnoreCase);
            return false;
        }
    }
}