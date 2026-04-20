
namespace FashionPicker.Core.Infra.Adapters.LocalCMS;

internal record RepositoryFileInformationDto(
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

internal static class RepositoryFileInformationDtoExtensions
{
    internal static RepositoryFileInformation ToInternal(this RepositoryFileInformationDto dto)
    {
        return new RepositoryFileInformation(
            //TODO
            FileType.Jpg,
            dto.PhysicalFileName,
            dto.LogicalFileName,
            dto.Extension,
            dto.Tags,
            dto.PathSmall,
            dto.PathMedium,
            dto.PathBig,
            dto.PathOriginal
        );
    }
}