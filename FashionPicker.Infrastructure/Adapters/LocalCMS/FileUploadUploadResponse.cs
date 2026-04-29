using System.Text.Json.Serialization;
using FashionPicker.Infrastructure.Adapters.LocalCMS;

namespace Infrastructure.FileRepository;

[method: JsonConstructor]
internal record FileUploadUploadResponse(
    List<RepositoryFileInformationDto> RepositoryFileInformation
);