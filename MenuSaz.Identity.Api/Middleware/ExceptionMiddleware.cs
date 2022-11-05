using MenuSaz.Identity.Application.Exception;
using MenuSaz.Identity.Application.Extensions;
using Newtonsoft.Json;
using Nitro.Wallet.Backend.Domain.Common;
using System;
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
            await HandleExceptionAsync(context, Domain.Common.ErrorCode.BadRequest, exception, null);
        }
        catch (MenuSazException exception)
        {
            await HandleExceptionAsync(context, Domain.Common.ErrorCode.BadRequest, null, exception);
        }
        catch (Exception)
        {
            await HandleExceptionAsync(context, Domain.Common.ErrorCode.UnknownServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Domain.Common.ErrorCode errorCode,
        MenuSazValidationException validationException = null, MenuSazException exception = null)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)errorCode;

        Envelop response = null;
        if (errorCode == Domain.Common.ErrorCode.UnknownServerError)
        {
            response = new Envelop(null, new Domain.Common.EnvelopError(Domain.Common.ErrorCode.UnknownServerError,
                      ResponseExtension.GetMessage(x => x.UnknownServerError)));
        }
        else if (validationException != null)
        {
            response = new Envelop(null, new Domain.Common.EnvelopError(validationException.ErrorCode, validationException.Message,
                validationException.InnerException), validationException.ValidationErrors);
        }
        else
        {
            response = new Envelop(null, new Domain.Common.EnvelopError(exception.ErrorCode, exception.Message, exception.InnerException));
        }
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response,
        new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
    }
}
public static class MiddlewareExtensionsExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}

