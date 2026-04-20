using FileRepository.Api;
using FileRepository.Entities;

namespace FileRepository;

public static class RepositoryFileInformationExtensions
{
    public static RepositoryFileInformationDto ToDto(this RepositoryFileInformation repoFileInfo)
    {
        return new RepositoryFileInformationDto(
            MimeType: repoFileInfo.MimeType,
            PhysicalFileName: repoFileInfo.PhysicalFileName,
            LogicalFileName: repoFileInfo.LogicalFileName ?? repoFileInfo.PhysicalFileName,
            Extension: repoFileInfo.Extension,
            Tags: repoFileInfo.Tags,
            PathSmall: repoFileInfo.PathSmall,
            PathMedium:  repoFileInfo.PathMedium,
            PathBig: repoFileInfo.PathBig,
            PathOriginal: repoFileInfo.PathOriginal
        );
    }
}
