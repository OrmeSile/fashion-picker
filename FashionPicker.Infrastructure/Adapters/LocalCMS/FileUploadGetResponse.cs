namespace FashionPicker.Infrastructure.Adapters.LocalCMS;

internal record FileUploadGetResponse(IEnumerable<RepositoryFileInformationDto> Data)
    : ListResponse<RepositoryFileInformationDto>(Data);