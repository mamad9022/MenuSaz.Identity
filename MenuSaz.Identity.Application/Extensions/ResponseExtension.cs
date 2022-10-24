using MenuSaz.Identity.Domain.Common;
using Newtonsoft.Json;

namespace MenuSaz.Identity.Application.Extensions;
public static class ResponseExtension
{
    public static string? GetMessage(Func<ResponseMessage, string> responseMessage)
    {
        var filepath = Path.Combine(Directory.GetCurrentDirectory() + $"/Resources/MenSaz-Identity.json");
        var jsonData = File.ReadAllText(filepath);
        var data = JsonConvert.DeserializeObject<Resources>(jsonData);
        if (data == null)
            return "خطای ناشناخته سمت سرور";

        var message = data.Data.FirstOrDefault(x => x.Key == responseMessage.Invoke(null));
        return message?.Template;
    }
}
