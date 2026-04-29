using FashionPicker.Core.Objects;

namespace FashionPicker.Core.Adapters;

public interface ICmsAdapter
{
    Task<RepositoryFileInformation> UploadFileAsync(MultipartFormDataContent data);
    Task<IEnumerable<RepositoryFileInformation>?> GetAllFileInformationsAsync();
}