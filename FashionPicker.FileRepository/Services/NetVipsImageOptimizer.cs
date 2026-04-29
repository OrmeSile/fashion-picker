using FileRepository.Objects;
using NetVips;

namespace FileRepository.Services;

public class NetVipsImageOptimizer : IImageOptimizer
{
    private const int SMALL_SIZE = 300;
    private const int MEDIUM_SIZE = 600;
    private const int BIG_SIZE = 1200;


    public ResizedResults ResizeImage(MemoryStream originalImage)
    {
        //TODO Stream is not recognized as an image(mimetype is not matching known data)
        originalImage.Position = 0;
        using var image = Image.NewFromStream(originalImage);

        var smallScalingRatio = GetScaleRatio(image.Width, image.Height, SMALL_SIZE);
        var mediumScalingRatio = GetScaleRatio(image.Width, image.Height, MEDIUM_SIZE);
        var bigScalingRatio = GetScaleRatio(image.Width, image.Height, BIG_SIZE);

        var resizeOperations = new List<(string size, Image image, float resizeRatio)>
        {
            ("original", image, 1)
        };

        if (smallScalingRatio != 0)
            resizeOperations.Add(("small", image, smallScalingRatio));
        if (mediumScalingRatio != 0)
            resizeOperations.Add(("medium", image, mediumScalingRatio));
        if (bigScalingRatio != 0)
            resizeOperations.Add(("big", image, bigScalingRatio));

        var resizedFiles = new Dictionary<string, byte[]>();

        Parallel.ForEach(resizeOperations,
            async (operation, _) =>
            {
                var resizedImage = Resize(operation.image, operation.resizeRatio);
                resizedFiles[operation.size] = resizedImage;
            });

        return new ResizedResults(
            resizedFiles.GetValueOrDefault("small"),
            resizedFiles.GetValueOrDefault("medium"),
            resizedFiles.GetValueOrDefault("big"),
            resizedFiles["original"]
        );
    }

    private static byte[] Resize(Image sourceImage, float resizeRatio)
    {
        using var resizedImage = sourceImage.Resize(resizeRatio);
        return resizedImage.JpegsaveBuffer();
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