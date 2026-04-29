using Microsoft.AspNetCore.Http;

namespace FashionPicker.Core.Infra.Adapters.LocalCMS;

public interface ICmsAdapter
{
    Task<RepositoryFileInformation> UploadFileAsync(MultipartFormDataContent data);
    Task<IEnumerable<RepositoryFileInformation>?> GetAllFileInformationsAsync();
}