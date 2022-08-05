using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriLogSaving
{
    public class RequestResponseMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestResponseMiddleWare> logger;

        public RequestResponseMiddleWare(RequestDelegate Next, ILogger<RequestResponseMiddleWare> logger) { 
            next = Next;
            this.logger = logger;
        }


        public async Task Invoke(HttpContext httpContext)
        {//orjinal streamin yedeği
            var OriginalBodyStream = httpContext.Response.Body;

            //REQUEST BURADA
            logger.LogInformation($"Query Keys:{ httpContext.Request.QueryString}");//get methodu ?parametre burada yakalanır

            MemoryStream requestBody = new MemoryStream();
            await httpContext.Request.Body.CopyToAsync(requestBody);
            requestBody.Seek(0, SeekOrigin.Begin);
            string requestText = await new StreamReader(requestBody).ReadToEndAsync();
            requestBody.Seek(0, SeekOrigin.Begin);

            var tempStream = new MemoryStream();
            httpContext.Response.Body = tempStream; //body ve temstreamın içi dolmuş olur burda 

            //response u okuyamadığımız için boş streamin içine veriyoruz ??

            //Stream burada oluştu
            await next.Invoke(httpContext); //RESPONSE BU SATIRDA


            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            String responseText = await new StreamReader(httpContext.Response.Body, Encoding.UTF8).ReadToEndAsync();
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin); //başa al tekrar okunabilmesi için

            //RESPONSE BURADA ALINACAK 

            await httpContext.Response.Body.CopyToAsync(OriginalBodyStream);
            //orjinalin içine fake stream tekrardan yazılır çünkü postman ya da google dan okunmasını sağlar

            logger.LogError($"Request:{requestText}");
            logger.LogError($"Response:{responseText}");
        }

    }
}
