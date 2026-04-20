using System.Runtime.InteropServices;
using FileRepository.ConfigurationOptions;
using Microsoft.Extensions.FileProviders;

namespace FileRepository;

public class StaticFileRepositoryOptions: StaticFileOptions
{
    public StaticFileRepositoryOptions(string path)
    {
        FileProvider = new PhysicalFileProvider(path);
        RequestPath = "/static";
    }
}

public static class StaticFileRepositoryOptioManager
{
    private static string? FullFilePath { get; set; }
    private static string? BasePath { get; set; }

    public static IApplicationBuilder UseStaticFileRepositoryFiles(this IApplicationBuilder app )
    {
        var path = FullFilePath ?? throw new Exception("Default save path is missing and was not created at startup.");
        var provider = new StaticFileRepositoryOptions(path);
        return app.UseStaticFiles(provider);
    }

    public static WebApplicationBuilder CreateStaticFileFolderIfNotExists(this WebApplicationBuilder builder)
    {
        BasePath =  RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            : builder.Environment.WebRootPath;

        builder.RegisterPathInConfiguration(BasePath);

        FullFilePath = Path.Combine(
            BasePath,
            builder.Configuration.GetSection(FileRepositoryOptions.Section).Get<FileRepositoryOptions>()?.SaveLocation ?? "static"
        );

        if (Directory.Exists(FullFilePath))
            return builder;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Directory.CreateDirectory(FullFilePath);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            Directory.CreateDirectory(FullFilePath, UnixFileMode.GroupWrite | UnixFileMode.GroupRead);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            throw new NotImplementedException("No OSX Support.");

        return builder;
    }

    private static void RegisterPathInConfiguration(this WebApplicationBuilder builder, string path)
    {
        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            {StaticPathProvider.BASE_PATH_KEY, path}
        });
    }
}