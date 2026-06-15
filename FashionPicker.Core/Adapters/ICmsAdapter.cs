using FashionPicker.Core.Objects;

namespace FashionPicker.Core.Adapters;

public interface ICmsAdapter
{
    Task<RepositoryFileInformation> UploadFileAsync(MultipartFormDataContent data, Guid userId);
    Task<IEnumerable<RepositoryFileInformation>?> GetAllFileInformationsAsync(Guid userId);
}