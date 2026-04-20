namespace FileRepository;

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