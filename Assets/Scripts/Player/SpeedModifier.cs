public class SpeedModifier
{
    public float Value { get; }
    public float Duration { get; set; } // Duration in seconds, 0 for permanent

    public SpeedModifier(float value, float duration)
    {
        Value = value;
        Duration = duration;
    }
}
