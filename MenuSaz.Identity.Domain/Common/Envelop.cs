using MenuSaz.Identity.Domain.Common;

namespace Nitro.Wallet.Backend.Domain.Common;

    public record Envelop(object? Data = null, EnvelopError? Error = null, Dictionary<string, string>? ValidationErrors = null);

