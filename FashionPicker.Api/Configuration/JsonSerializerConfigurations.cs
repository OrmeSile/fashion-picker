using System.Text.Json;

namespace FashionPicker.Api.Configuration;

public static class JsonSerializerConfigurations
{

    public static JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
}