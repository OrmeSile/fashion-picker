using FileRepository.Objects;
using NetVips;

namespace FileRepository.Services;

public class NetVipsImageOptimizer: IImageOptimizer
{
    private const int SMALL_SIZE = 300;
    private const int MEDIUM_SIZE = 600;
    private const int BIG_SIZE = 1200;


    public ResizedResults ResizeImage(MemoryStream originalImage)
    {
        originalImage.Position = 0;
        using var image = Image.NewFromStream(originalImage);

        var smallScalingRatio = GetScaleRatio(image.Width, image.Height, SMALL_SIZE);
        var mediumScalingRatio = GetScaleRatio(image.Width, image.Height, MEDIUM_SIZE);
        var bigScalingRatio = GetScaleRatio(image.Width, image.Height, BIG_SIZE);

        var resizeTasks = new Dictionary<string, Task<byte[]>>();

        resizeTasks.Add("original",ResizeAsync(image, 1));

        if(smallScalingRatio != 0)
            resizeTasks.Add("small", ResizeAsync(image, smallScalingRatio ));
        if (mediumScalingRatio != 0)
            resizeTasks.Add("medium", ResizeAsync(image, mediumScalingRatio));
        if (bigScalingRatio != 0)
            resizeTasks.Add("big", ResizeAsync(image, bigScalingRatio));
        Task.WaitAll(resizeTasks.Values);

        return new ResizedResults(
            resizeTasks["small"].Result,
            resizeTasks.GetValueOrDefault("medium")?.Result,
            resizeTasks.GetValueOrDefault("big")?.Result,
            resizeTasks["original"].Result
        );
    }

    private static Task<byte[]> ResizeAsync(Image sourceImage, float resizeRatio)
    {
        return Task.Run(() =>
        {
            using var resizedImage = sourceImage.Resize(resizeRatio);
            return resizedImage.JpegsaveBuffer();
        });
    }

    private static float GetScaleRatio(int initialWidth, int initialHeight, int desiredBoxSize, bool upscale = false)
    {
        var ratioX = (float)desiredBoxSize / initialWidth;
        var ratioY = (float)desiredBoxSize / initialHeight;

        if (!upscale && (ratioX > 1 || ratioY > 1))
            return 0;

        return Math.Min(ratioX, ratioY);
    }

}