using FashionPicker.FileRepository.Objects;

namespace FashionPicker.FileRepository.Interfaces;

public interface IImageOptimizer
{
    ResizedResults ResizeImage(MemoryStream originalImage);
}