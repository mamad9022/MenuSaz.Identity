namespace MenuSaz.Identity.Domain.Common;
    public record EnvelopError(ErrorCode ErrorCode, string CustomMessage, Exception? Exception = null);
