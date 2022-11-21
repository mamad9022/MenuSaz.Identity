using MenuSaz.Identity.Domain.Common;

namespace MenuSaz.Identity.Application.Exception
{
    [Serializable]
    public class MenuSazException : System.Exception
    {
        public MenuSazException()
        {

        }

        public MenuSazException(ErrorCode errorCode) => ErrorCode = errorCode;
        public MenuSazException(ErrorCode errorCode, string? message): base(message) => ErrorCode = errorCode;
        public MenuSazException(ErrorCode errorCode, string message, System.Exception inner) : base(message, inner) =>
            ErrorCode = errorCode;

        public ErrorCode ErrorCode { get; set; }
    }
}
