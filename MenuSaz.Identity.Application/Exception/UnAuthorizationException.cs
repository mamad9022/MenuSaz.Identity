using MenuSaz.Identity.Application.Extensions;

namespace MenuSaz.Identity.Application.Exception;
public class UnAuthorizationException : MenuSazException
{
    public UnAuthorizationException() : base(Domain.Common.ErrorCode.Unauthorized, ResponseExtension.GetMessage(x => x.UnAuthorize))
    {

    }
}
