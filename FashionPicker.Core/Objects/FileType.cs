namespace FashionPicker.Core.Objects;

public enum FileType
{
    Jpg,
    Png,
    Bmp,
    Gif,
    Tiff,
    Ico,
    Svg,
    Webp,
    Webm,
    Avi,
    Mpeg,
    Mp4,
    Mp3,
    Ogg,
    Wav,
    Md,
    Txt,
    Ppt,
    Pptx,
    Doc,
    Docx,
    NotFound
}

public static class FileTypeExtensions
{
    public static string GetMimeType(this FileType fileType)
    {
        return fileType switch
        {
            FileType.Jpg => "image/jpeg",
            FileType.Png => "image/png",
            FileType.Bmp => "image/bmp",
            FileType.Gif => "image/gif",
            FileType.Tiff => "image/tiff",
            FileType.Ico => "image/vnd.microsoft.icon",
            FileType.Svg => "image/svg+xml",
            FileType.Webp => "image/webp",
            FileType.Webm => "video/webm",
            FileType.Avi => "video/x-msvideo",
            FileType.Mpeg => "audio/mpeg",
            FileType.Mp4 => "video/mp4",
            FileType.Mp3 => "audio/mpeg",
            FileType.Ogg => "audio/ogg",
            FileType.Wav => "audio/wav",
            FileType.Md => "text/markdown",
            FileType.Txt => "text/plain",
            FileType.Ppt => "application/vnd.ms-powerpoint",
            FileType.Pptx => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            FileType.Doc => "application/msword",
            FileType.Docx => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            _ => throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null)
        };
    }
}