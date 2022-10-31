using MenuSaz.Identity.Application.Exception;
using MenuSaz.Identity.Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nitro.Wallet.Backend.Domain.Common;

namespace MenuSaz.Identity.Api.Fillter;
public class EnvelopFillter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var response = context.Result as ObjectResult;

        if (response?.Value == null)
            return;

        context.Result = new ObjectResult(new Envelop(response.Value));
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid == false)
        {
            throw new MenuSazValidationException(context.ModelState.ToDictionary());
        }
    }
}

public static class EnvelopFillterExtensions
{
    public static IApplicationBuilder UseEnvelopFillter(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<EnvelopFillter>();
    }
}
