using FashionPicker.FileRepository.Configuration;

namespace FashionPicker.FileRepository.Providers;

/// <summary>
/// Provides a mechanism to retrieve the base path for static files from configuration injected at startup.
///<br/>
/// View : <see cref="StaticFileRepositoryOptionsManager"/>
/// </summary>
public class StaticPathProvider
{
    public static readonly string BASE_PATH_KEY = "FileSaveBasePath";

    private readonly IConfiguration _configuration;

    public StaticPathProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetFilePath()
    {
        return _configuration[BASE_PATH_KEY] ?? "static";
    }
}