namespace FileRepository.Objects;

public struct ScalingRatios
{
    public float SmallScalingRatio { get; }
    public float MediumScalingRatio { get; }
    public float BigScalingRatio { get; }

    public ScalingRatios(float smallScalingRatio, float mediumScalingRatio, float bigScalingRatio)
    {
        SmallScalingRatio = smallScalingRatio;
        MediumScalingRatio = mediumScalingRatio;
        BigScalingRatio = bigScalingRatio;
    }
}