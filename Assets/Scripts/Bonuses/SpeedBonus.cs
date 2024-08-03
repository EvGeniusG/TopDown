using UnityEngine;

public class SpeedBonus : ABonus
{
    [SerializeField] float duration; // Длительность бонуса в секундах
    [SerializeField] float speedMultiplier = 1.5f; // Множитель скорости

    protected override void ApplyBonus(IPlayerModel playerModel)
    {
        SpeedModifier speedModifier = new SpeedModifier(speedMultiplier, duration);
        playerModel.AddSpeedModifier(speedModifier);
    }
}
