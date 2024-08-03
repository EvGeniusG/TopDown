public class InvincibilityModifier
{
    public float Duration { get; set; } // Duration in seconds, 0 for permanent

    public InvincibilityModifier(float duration)
    {
        Duration = duration;
    }

    // Переопределяем метод Equals для корректного сравнения модификаторов
    public override bool Equals(object obj)
    {
        if (obj is InvincibilityModifier other)
        {
            return Duration == other.Duration;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Duration.GetHashCode();
    }
}