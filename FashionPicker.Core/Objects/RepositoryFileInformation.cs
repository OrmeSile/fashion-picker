namespace FashionPicker.Core.Objects;

public class RepositoryFileInformation
{
    public FileType FileType { get; }
    public string MimeType { get; }
    public string PhysicalFileName { get; }
    public string LogicalFileName { get; }
    public string Extension { get; }
    public string[] Tags { get; }
    public string PathSmall { get; }
    public string? PathMedium { get; set; }
    public string? PathBig { get; set; }
    public string PathOriginal { get; }
    public int Order { get; set; }

    public RepositoryFileInformation(
        FileType fileType,
        string physicalFileName,
        string logicalFileName,
        string extension,
        string[] tags,
        string pathSmall,
        string? pathMedium,
        string? pathBig,
        string pathOriginal
    )
    {
        FileType = fileType;
        MimeType = fileType.GetMimeType();
        PhysicalFileName = physicalFileName;
        LogicalFileName = logicalFileName;
        Extension = extension;
        Tags = tags;
        PathSmall = pathSmall;
        PathMedium = pathMedium;
        PathBig = pathBig;
        PathOriginal = pathOriginal;
    }

    public RepositoryFileInformation(FileType fileType, string physicalFileName, string logicalFileName, string extension, string[] tags, string pathSmall, string? pathMedium, string? pathBig, string pathOriginal, int order)
    {
        FileType = fileType;
        MimeType = FileType.GetMimeType();
        PhysicalFileName = physicalFileName;
        LogicalFileName = logicalFileName;
        Extension = extension;
        Tags = tags;
        PathSmall = pathSmall;
        PathMedium = pathMedium;
        PathBig = pathBig;
        PathOriginal = pathOriginal;
        Order = order;
    }
}