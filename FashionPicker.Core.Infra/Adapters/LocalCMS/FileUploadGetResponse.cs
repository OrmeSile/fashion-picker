namespace FashionPicker.Core.Infra.Adapters.LocalCMS;

internal record FileUploadGetResponse(IEnumerable<RepositoryFileInformationDto> Data)
    : ListResponse<RepositoryFileInformationDto>(Data);