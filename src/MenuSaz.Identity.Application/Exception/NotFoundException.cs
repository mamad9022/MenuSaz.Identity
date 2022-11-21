using MenuSaz.Identity.Application.Extensions;

namespace MenuSaz.Identity.Application.Exception;
public class NotFoundException : MenuSazException
{
    public NotFoundException() : base(Domain.Common.ErrorCode.NotFound, ResponseExtension.GetMessage(x => x.NotFound))
    {

    }
}
