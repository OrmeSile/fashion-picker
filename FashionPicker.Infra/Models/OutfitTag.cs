using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Infra.Models;

[Index(nameof(Name), IsUnique=true)]
public class OutfitTag
{
    public Guid Id { get; init; }

    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }
}