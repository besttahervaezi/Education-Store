using System.Net;
using System.Text;

namespace Application.Helpers;
using Microsoft.AspNetCore.Http;
public class IdGenerator
{
    public static string GenerateCacheKeyFromRequest(HttpRequest request)
    {
        var keybuilder = new StringBuilder();
        keybuilder.Append($"{request.Path}");
        foreach (var (key,value) in request.Query.OrderBy(x=>x.Key))
        {
            keybuilder.Append($"|{key}-{value}");
        }

        return keybuilder.ToString();
    }
}