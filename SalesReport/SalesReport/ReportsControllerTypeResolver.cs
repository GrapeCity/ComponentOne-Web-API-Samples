﻿using System;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace SalesReport
{
    internal class ReportsControllerTypeResolver : DefaultHttpControllerTypeResolver
    {
        public ReportsControllerTypeResolver() : base(IsControllerType)
        { }

        private static bool IsControllerType(Type t)
        {
            if (t != null && t.IsClass && (t.IsVisible && !t.IsAbstract) && typeof(IHttpController).IsAssignableFrom(t))
                return HasValidControllerName(t) && t != typeof(C1.Web.Api.Report.ReportController);
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