namespace FashionPicker.FileRepository.Dto;

public record FileInformation(
    FileType Type,
    string Name,
    string MimeType,
    string Path
);