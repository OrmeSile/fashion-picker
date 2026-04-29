using System.Text.Json.Serialization;

namespace FileRepository.Api;

[method: JsonConstructor]
public record FileUploadUploadResponse(
    List<RepositoryFileInformationDto> RepositoryFileInformation
);