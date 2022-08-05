using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriLogSaving
{
    public class ExceptionHandlerMiddleWare
    {

        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleWare> logger;

        public ExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<ExceptionHandlerMiddleWare> Logger)
        {
            next = Next;
            logger = Logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //dışardan istek geldiğinde controllera düşmek yerine buradaki middleware içindeki ınvoke methoduna düşer sonra controllera gider
            //ınvoke içinde tüm aksiyonlar yakalanmış olur.yani burada try catch oluşturulduğunda uygulama içerisindeki bütün try catchleri yakalamış olur.
            try
            {
                await next.Invoke(httpContext);
            }
            catch(Exception ex)
            {
              
                //Hata yönetimi uygulamadaki herhangi bir controllerda oluşan hata burada yakalanır
                logger.LogError(ex.Message);
            }
           
        }
    }
}
