using System.Text.Json.Serialization;
using FashionPicker.Core.Infra.Adapters.LocalCMS;

namespace Infrastructure.FileRepository;

[method: JsonConstructor]
internal record FileUploadUploadResponse(
    List<RepositoryFileInformationDto> RepositoryFileInformation
);