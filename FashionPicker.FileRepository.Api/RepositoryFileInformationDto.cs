namespace FileRepository.Api;

public record RepositoryFileInformationDto(
    string MimeType,
    string PhysicalFileName,
    string LogicalFileName,
    string Extension,
    string[] Tags,
    string PathSmall,
    string? PathMedium,
    string? PathBig,
    string PathOriginal
);