using System.Diagnostics;
using FileRepository.Objects;
using SkiaSharp;

namespace FileRepository.Services;

public class SkiaSharpImageOptimizer : IImageOptimizer
{
    private const int SMALL_SIZE = 300;
    private const int MEDIUM_SIZE = 600;
    private const int BIG_SIZE = 1200;

    public ResizedResults ResizeImage(MemoryStream originalImage)
    {
        originalImage.Position = 0;
        using var image = SKImage.FromEncodedData(originalImage);

        var smallScalingRatio = GetScaleRatio(image.Width, image.Height, SMALL_SIZE);
        var mediumScalingRatio = GetScaleRatio(image.Width, image.Height, MEDIUM_SIZE);
        var bigScalingRatio = GetScaleRatio(image.Width, image.Height, BIG_SIZE);

        var resizeTasks = new Dictionary<string, Task<byte[]>>();
        var samplingOptions = new SKSamplingOptions(SKFilterMode.Linear);

        resizeTasks.Add("small", ResizeAsync(image, smallScalingRatio, samplingOptions));
        resizeTasks.Add("original",ResizeAsync(image, 1,  samplingOptions));
        if (mediumScalingRatio != 0)
            resizeTasks.Add("medium", ResizeAsync(image, mediumScalingRatio, samplingOptions));
        if (bigScalingRatio != 0)
            resizeTasks.Add("big", ResizeAsync(image, bigScalingRatio, samplingOptions));

        Task.WaitAll(resizeTasks.Values);

        return new ResizedResults(
            resizeTasks["small"].Result,
            resizeTasks.GetValueOrDefault("medium")?.Result,
            resizeTasks.GetValueOrDefault("big")?.Result,
            resizeTasks["original"].Result
        );
    }


    private float GetScaleRatio(int initialWidth, int initialHeight, int desiredBoxSize, bool upscale = false)
    {
        var ratioX = (float)desiredBoxSize / initialWidth;
        var ratioY = (float)desiredBoxSize / initialHeight;

        if (!upscale && (ratioX > 1 || ratioY > 1))
            return 0;

        return Math.Min(ratioX, ratioY);
    }

    private Task<byte[]> ResizeAsync(SKImage sourceImage, float resizeRatio, SKSamplingOptions samplingOptions)
    {
        return Task.Run(() =>
        {
            var newWidth = (int)(sourceImage.Width * resizeRatio);
            var newHeight = (int)(sourceImage.Height * resizeRatio);
            using var targetBitmap = new SKBitmap(newWidth, newHeight);
            using var canvas = new SKCanvas(targetBitmap);
            var destRect = new SKRect(0, 0, newWidth, newHeight);
            canvas.DrawImage(sourceImage, destRect, samplingOptions);
            using var image = SKImage.FromBitmap(targetBitmap);
            using var encodedImage = image.Encode(SKEncodedImageFormat.Jpeg, 85);
            return encodedImage.ToArray();
        });
    }
}