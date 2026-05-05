using System.ComponentModel.DataAnnotations;
using FashionPicker.FileRepository.Interfaces;

namespace FashionPicker.FileRepository.Entities;

public class RepositoryFileInformation : IPhysicalFile
{
    public Guid Id { get; init; }
    [MaxLength(20)] public required string MimeType { get; init; }
    [MaxLength(100)] public required string PhysicalFileName { get; init; }
    [MaxLength(300)] public required string? LogicalFileName { get; set; }
    [MaxLength(6)] public required string Extension { get; init; }
    public required string[] Tags { get; init; }
    [MaxLength(200)] public required string PathSmall { get; init; }
    [MaxLength(200)] public required string? PathMedium { get; init; }
    [MaxLength(200)] public required string? PathBig { get; init; }
    [MaxLength(200)] public required string PathOriginal { get; init; }
}