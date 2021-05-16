using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ErrorHandlingMiddleware> _Logger;

        public ErrorHandlingMiddleware(RequestDelegate Next, ILogger<ErrorHandlingMiddleware> Logger)
        {
            _Next = Next;
            _Logger = Logger;
        }

        public async Task InvokeAsync(HttpContext Context)
        {
            try
            {
                await _Next(Context);
            }
            catch(Exception error)
            {
                HandleException(error, Context);
                throw;
            }
        }

        private void HandleException(Exception error, HttpContext context)
        {
            _Logger.LogError(error, "Ошибка при обработке запроса к {0}",
                context.Request.Path);
        }
    }
}
