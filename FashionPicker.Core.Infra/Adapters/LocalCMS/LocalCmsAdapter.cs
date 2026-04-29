using System.Net.Http.Headers;
using System.Net.Http.Json;
using Infrastructure.FileRepository;
using Microsoft.AspNetCore.Http;

namespace FashionPicker.Core.Infra.Adapters.LocalCMS;

public class LocalCmsAdapter : ICmsAdapter
{
    private static readonly HttpClient httpClient;

    static LocalCmsAdapter()
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7043");
    }

    public async Task<RepositoryFileInformation> UploadFileAsync(MultipartFormDataContent formDataContent)
    {
        var res = await httpClient.PostAsync("upload", formDataContent);

        var successfulResponse = res.EnsureSuccessStatusCode();
        if (!successfulResponse.IsSuccessStatusCode)
            throw new ApplicationException("fileInformation is null");
        try
        {
            var responseBody = await successfulResponse.Content.ReadFromJsonAsync<FileUploadUploadResponse>(CancellationToken.None);
            return responseBody?.RepositoryFileInformation[0].ToInternal() ?? throw new ApplicationException("fileInformation is null");
        }catch(Exception ex)
        {
            throw new ApplicationException("fileInformation is null", ex);
        }
    }

    public async Task<IEnumerable<RepositoryFileInformation>?> GetAllFileInformationsAsync()
    {
        using var res = await httpClient.GetAsync("", CancellationToken.None);
        if (!res.IsSuccessStatusCode) return null;

        var responseBody = await res.Content.ReadFromJsonAsync<FileUploadGetResponse>(CancellationToken.None);
        var fileInformations = responseBody?.Data.Select(repositoryFileInformationDto => repositoryFileInformationDto.ToInternal());
        return fileInformations;
    }
}