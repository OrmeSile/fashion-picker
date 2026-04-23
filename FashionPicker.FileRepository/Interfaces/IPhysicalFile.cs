
namespace FileRepository.Interfaces;

public interface IPhysicalFile
{
    string PathSmall { get; init; }
    string? PathMedium { get; init; }
    string? PathBig { get; init; }
    string PathOriginal { get; init; }
}