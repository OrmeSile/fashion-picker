using System.ComponentModel.DataAnnotations;

namespace FashionPicker.Infra.Models;

public class Outfit
{
    public Guid Id { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }
    [MaxLength(1024)]
    public required string ImageUrl { get; set; }
    public required List<OutfitTag> Tags { get; set; }
    public required DateTime CreationDate { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }
    [MaxLength(16)]
    public string? Season { get; set; }
    public List<string>? Colors { get; set; }

}