using System;
using WebApi.Models;
#if !ASPNETCORE
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.IO;
#else
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using System.Threading.Tasks;
using System.Threading;
#endif

namespace WebApi.Controllers
{
    internal class HTMLMediaTypeFormatter
#if !ASPNETCORE
        : BufferedMediaTypeFormatter
#else
        : OutputFormatter
#endif
    {
        public HTMLMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

#if !ASPNETCORE
        public
#else
        protected
#endif
        override bool CanWriteType(Type type)
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

#if !ASPNETCORE

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            var c1APIIntroduction = value as C1APIIntroduction;
            if (c1APIIntroduction != null)
            {
                using (var wrt = new StreamWriter(writeStream))
                {
                    wrt.Write(PageTemplate.GetAPIPage(c1APIIntroduction));
                }
            }

            writeStream.Close();
        }
#else
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            string content = GetResponseContent(context.Object);
            if(content != null)
            {
                return context.HttpContext.Response.WriteAsync(content, default(CancellationToken));
            }
            return TaskCache.CompletedTask;
        }
#endif
    }
}