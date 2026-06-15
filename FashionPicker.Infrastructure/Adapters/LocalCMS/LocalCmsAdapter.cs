using System.Net.Http.Json;
using FashionPicker.Core.Adapters;
using FashionPicker.Core.Objects;
using Infrastructure.FileRepository;
using Microsoft.Extensions.Configuration;

namespace FashionPicker.Infrastructure.Adapters.LocalCMS;

public class LocalCmsAdapter : ICmsAdapter
{
    private readonly HttpClient httpClient;

    public LocalCmsAdapter(IConfiguration config)
    {
        httpClient = new HttpClient();
        var fileRepositoryUrl = config["FileRepositoryUrl"] ?? throw new ApplicationException("FileRepositoryUrl is null");
        httpClient.BaseAddress = new Uri(fileRepositoryUrl);
    }

    public async Task<RepositoryFileInformation> UploadFileAsync(MultipartFormDataContent formDataContent, Guid userId)
    {

        var res = await httpClient.PostAsync($"upload?userId={userId}", formDataContent);

        var successfulResponse = res.EnsureSuccessStatusCode();
        if (!successfulResponse.IsSuccessStatusCode)
            throw new ApplicationException("fileInformation is null");
        try
        {
            var responseBody = await successfulResponse.Content.ReadFromJsonAsync<FileUploadUploadResponse>(CancellationToken.None);
            return responseBody?.RepositoryFileInformation[0].ToInternal() ?? throw new ApplicationException("fileInformation is null");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("fileInformation is null", ex);
        }
    }

    public async Task<IEnumerable<RepositoryFileInformation>?> GetAllFileInformationsAsync(Guid userId)
    {
        using var res = await httpClient.GetAsync("?userId={userId}", CancellationToken.None);
        if (!res.IsSuccessStatusCode) return null;

        var responseBody = await res.Content.ReadFromJsonAsync<FileUploadGetResponse>(CancellationToken.None);
        var fileInformations = responseBody?.Data.Select(repositoryFileInformationDto => repositoryFileInformationDto.ToInternal());
        return fileInformations;
    }
}