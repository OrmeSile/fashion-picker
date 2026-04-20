using FashionPicker.Core.Api.Dto.Inbound;

namespace FashionPicker.Core.Api.Dto.Outbound.Outfit;

public record OutfitPostResponse(List<OutfitMetadata> outfits);