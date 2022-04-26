using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace SalesAnalysis
{
    public class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            // inherit route attributes decorated on base class controller's actions
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(true);
        }

        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            var prefix = base.GetRoutePrefix(controllerDescriptor);
            if (string.IsNullOrEmpty(prefix))
            {
                var prefixAttr = controllerDescriptor.GetCustomAttributes<IRoutePrefix>(true).FirstOrDefault();
                if (prefixAttr != null)
                {
                    return prefixAttr.Prefix;
                }
            }

            return prefix;
        }
    }
}