using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SistemaTC.Core.Extensions;
public static class ObjectExtension
{
    public static string AsJson(this object value)
    {
        return JsonConvert.SerializeObject(value, Formatting.Indented);
    }
}
