namespace FileRepository.Api;

public record FileUploadGetResponse(IEnumerable<RepositoryFileInformationDto> Data)
    : ListResponse<RepositoryFileInformationDto>(Data);