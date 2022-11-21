using MenuSaz.Identity.Domain.Common;

namespace MenuSaz.Identity.Application.Exception
{
    public class MenuSazValidationException : MenuSazException
    {
        public MenuSazValidationException() : base()
        {
            ErrorCode = ErrorCode.BadRequest;
        }

        public MenuSazValidationException(Dictionary<string, string> validationErrors) : this()
        {
            ValidationErrors = validationErrors;
        }

        public Dictionary<string, string> ValidationErrors { get; private set; }
    }
}
