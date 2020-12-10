using System;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;

namespace WebApi.Controllers
{
    internal class HTMLMediaTypeFormatter : OutputFormatter
    {
        public HTMLMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        protected override bool CanWriteType(Type type)
        {
            if (type == typeof(C1APIIntroduction))
            {
                return true;
            }
            else
            {
                return typeof(C1APIIntroduction).IsAssignableFrom(type);
            }
        }

        private string GetResponseContent(object value)
        {
            var c1APIIntroduction = value as C1APIIntroduction;
            if (c1APIIntroduction != null)
            {
                return PageTemplate.GetAPIPage(c1APIIntroduction);
            }
            return null;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            string content = GetResponseContent(context.Object);
            if (content != null)
            {
                return context.HttpContext.Response.WriteAsync(content, default(CancellationToken));
            }
#if NET452
            return Microsoft.AspNetCore.Mvc.Internal.TaskCache.CompletedTask;
#else
            return Task.CompletedTask;
#endif
        }
    }
}