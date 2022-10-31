using MenuSaz.Identity.Application.Exception;
using MenuSaz.Identity.Application.Extensions;
using Newtonsoft.Json;
using Nitro.Wallet.Backend.Domain.Common;
using System.Net;

namespace MenuSaz.Identity.Api.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (MenuSazValidationException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            var response = new Envelop(null, new Domain.Common.EnvelopError(exception.ErrorCode, exception.Message, exception.InnerException), exception.ValidationErrors);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
        catch (MenuSazException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            var response = new Envelop(null, new Domain.Common.EnvelopError(exception.ErrorCode, exception.Message, exception.InnerException));
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response,
              new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
        catch (Exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new Envelop(null, new Domain.Common.EnvelopError(Domain.Common.ErrorCode.UnknownServerError,
                ResponseExtension.GetMessage(x => x.UnknownServerError)));
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response,
           new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }
}

public static class MiddlewareExtensionsExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}

