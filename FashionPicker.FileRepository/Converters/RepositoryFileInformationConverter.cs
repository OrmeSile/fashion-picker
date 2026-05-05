using FashionPicker.FileRepository.Entities;
using FileRepository.Api;

namespace FashionPicker.FileRepository.Converters;

public static class RepositoryFileInformationExtensions
{
    public static RepositoryFileInformationDto ToDto(this RepositoryFileInformation repoFileInfo)
    {
        return new RepositoryFileInformationDto(
            repoFileInfo.MimeType,
            repoFileInfo.PhysicalFileName,
            repoFileInfo.LogicalFileName ?? repoFileInfo.PhysicalFileName,
            repoFileInfo.Extension,
            repoFileInfo.Tags,
            repoFileInfo.PathSmall,
            repoFileInfo.PathMedium,
            repoFileInfo.PathBig,
            repoFileInfo.PathOriginal
        );
    }
}