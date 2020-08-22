using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Logic.Middleware
{
    public class LogUserNameMiddleware
    {
        private readonly RequestDelegate next;

        public LogUserNameMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            LogContext.PushProperty("UserName", context.User.Identity.Name);
            return next(context);
        }
    }
}