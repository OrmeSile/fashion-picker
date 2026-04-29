using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Core.Infra.Models;

[Index(nameof(Value), IsUnique=true)]
public class OutfitTag
{
    public Guid Id { get; init; }

    [MaxLength(100)]
    public required string Value { get; set; }

    public List<Outfit> Outfits { get; } = [];
}