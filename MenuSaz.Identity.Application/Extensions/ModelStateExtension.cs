using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MenuSaz.Identity.Application.Extensions
{
    public static class ModelStateExtension
    {
        public static Dictionary<string, string> ToDictionary(this ModelStateDictionary state)
        {
            return new Dictionary<string, string>(
                state.Select(x => new KeyValuePair<string, string>(x.Key, string.Join(Environment.NewLine,
                x.Value.Errors.Select(v => v.ErrorMessage))
                )));
        }
    }
}
