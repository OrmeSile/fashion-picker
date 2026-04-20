using System.Net.Http.Json;
using Infrastructure.FileRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FashionPicker.Core.Infra.Adapters.LocalCMS;

public class LocalCmsAdapter
{
    private static readonly HttpClient httpClient;

    static LocalCmsAdapter()
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7043");
    }

    public async Task<List<RepositoryFileInformation>> UploadFileAsync(HttpRequest request)
    {
        var target = new HttpRequestMessage(
            method: new HttpMethod(request.Method),
            requestUri: "upload"
        );

        target.Content = request.StreamContent();
        target.AddHeaders(request.Headers.Except("Host"));

        using var res = await httpClient.SendAsync(target);

        var successfulResponse = res.EnsureSuccessStatusCode();
        if (!successfulResponse.IsSuccessStatusCode)
            throw new ApplicationException("fileInformation is null");

        var responseBody = await successfulResponse.Content.ReadFromJsonAsync<FileUploadUploadResponse>(CancellationToken.None);
        return responseBody?.RepositoryFileInformation.Select(fileInformation => fileInformation.ToInternal()).ToList() ?? throw new ApplicationException("fileInformation is null");
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