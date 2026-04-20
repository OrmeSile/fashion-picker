using FileRepository.Objects;

namespace FileRepository.Services;

public interface IImageOptimizer
{
    ResizedResults ResizeImage(MemoryStream originalImage);
}