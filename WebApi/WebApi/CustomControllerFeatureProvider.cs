using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace WebApi
{
    public class CustomControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            var result = base.IsController(typeInfo);
            if (result)
            {
                if(typeof(C1.Web.Api.Report.ReportController) == typeInfo
                    || typeof(C1.Web.Api.Excel.ExcelController) == typeInfo
                    || typeof(C1.Web.Api.BarCode.BarCodeController) == typeInfo)
                {
                    return false;
                }
            }
            return result;
        }
    }
}
