using System.Diagnostics;
using FileRepository.Objects;
using SkiaSharp;

namespace FileRepository.Services;

public  class ImageOptimizer
{
    private const int SMALL_SIZE = 300;
    private const int MEDIUM_SIZE = 600;
    private const int BIG_SIZE = 1200;

    public ResizedResults ResizeImage(MemoryStream originalImage)
    {
        using var managedStream = new SKManagedStream(originalImage, true);
        // using var inputData = SKData.Create(managedStream);
        var bitmap = SKBitmap.Decode(managedStream);

        var isWider = bitmap.Width >= bitmap.Height;

        var smallScalingRatio = isWider ? bitmap.Width / (float)SMALL_SIZE : bitmap.Height / (float)SMALL_SIZE;
        var mediumScalingRatio = isWider ? bitmap.Width / (float)MEDIUM_SIZE : bitmap.Height / (float)MEDIUM_SIZE;
        var bigScalingRatio = isWider ? bitmap.Width / (float)BIG_SIZE : bitmap.Height / (float)BIG_SIZE;

        var smallSize = isWider
            ? new SKSizeI(SMALL_SIZE, (int)Math.Round(bitmap.Height / smallScalingRatio))
            : new SKSizeI((int)Math.Round(bitmap.Width / smallScalingRatio), SMALL_SIZE);
        var mediumSize = isWider
            ? new SKSizeI( MEDIUM_SIZE, (int)Math.Round(bitmap.Height / smallScalingRatio))
            : new SKSizeI((int)Math.Round(bitmap.Width / mediumScalingRatio),  MEDIUM_SIZE);
        var bigSize = isWider
            ? new SKSizeI(BIG_SIZE, (int)Math.Round(bitmap.Height / bigScalingRatio) )
            : new SKSizeI((int)Math.Round(bitmap.Width / bigScalingRatio), BIG_SIZE);

        var samplingOptions = new SKSamplingOptions(SKFilterMode.Nearest);

        var smallBitmap = Task.Run(() => bitmap.Resize(smallSize, samplingOptions));
        var mediumBitmap = Task.Run(() => mediumScalingRatio > 1 ? bitmap.Resize(mediumSize,  samplingOptions) : null);
        var bigBitmap = Task.Run(() => bigScalingRatio > 1 ?  bitmap.Resize(bigSize, samplingOptions) : null);
        Task.WaitAll(smallBitmap, mediumBitmap, bigBitmap);

        var startTime = Stopwatch.GetTimestamp();
        var smallEncoded = EncodeToJpegAsync(smallBitmap.Result);
        var mediumEncoded = EncodeToJpegAsync(mediumBitmap.Result);
        var bigEncoded = EncodeToJpegAsync(bigBitmap.Result);
        var originalEncoded = EncodeToJpegAsync(bitmap);
        Task.WaitAll(smallEncoded, mediumEncoded, bigEncoded, originalEncoded);
        var endTime = Stopwatch.GetElapsedTime(startTime);

        return new ResizedResults(
            smallEncoded.Result,
            mediumEncoded.Result,
            bigEncoded.Result,
            originalEncoded.Result
            );
    }

    private Task<byte[]?> EncodeToJpegAsync(SKBitmap? bitmap)
    {
        return Task.Run(() => bitmap != null ? SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Webp, 90).ToArray() : null);
    }
}