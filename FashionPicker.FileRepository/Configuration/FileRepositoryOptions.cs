namespace FashionPicker.FileRepository.Configuration;

public class FileRepositoryOptions
{
    public const string Section = "FileRepository";

    public string SaveLocation { get; set; } = string.Empty;
}