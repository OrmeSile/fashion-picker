using System.Runtime.InteropServices;
using System.Security.Principal;
using FileRepository.ConfigurationOptions;
using Microsoft.Extensions.FileProviders;

namespace FileRepository;

public class StaticFileRepositoryOptions : StaticFileOptions
{
    public StaticFileRepositoryOptions(string path)
    {
        FileProvider = new PhysicalFileProvider(path);
        RequestPath = "/static";
    }
}

/// <summary>
/// Provides extension methods for configuring and managing static file repositories within a web application startup pipeline.
/// </summary>
public static class StaticFileRepositoryOptionsManager
{
    private static string? FullFilePath { get; set; }
    private static string? BasePath { get; set; }

    public static IApplicationBuilder UseStaticFileRepositoryFiles(this IApplicationBuilder app)
    {
        var path = FullFilePath ?? throw new Exception("Default save path is missing and was not created at startup.");
        var provider = new StaticFileRepositoryOptions(path);
        return app.UseStaticFiles(provider);
    }

    /// <summaryd
    /// Creates the necessary directory structure for the static file repository if it does not already exist.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance used to configure services.</param>
    /// <returns>The WebApplicationBuilder instance, allowing method chaining.</returns>
    public static WebApplicationBuilder CreateStaticFileFolderIfNotExists(this WebApplicationBuilder builder)
    {
        BasePath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            : "/var/lib/filerepository";

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
            Directory.CreateDirectory(FullFilePath, UnixFileMode.UserRead | UnixFileMode.UserWrite| UnixFileMode.GroupWrite | UnixFileMode.GroupRead);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            throw new NotImplementedException("No OSX Support.");

        return builder;
    }

    /// <summary>
    /// Adds the specified directory path to the application's configuration, making it available throughout the web application.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance being configured.</param>
    /// <param name="path">The absolute path that should be registered in the application configuration.</param>
    private static void RegisterPathInConfiguration(this WebApplicationBuilder builder, string path)
    {
        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            { StaticPathProvider.BASE_PATH_KEY, path }
        });
    }
}