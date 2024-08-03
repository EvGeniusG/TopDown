using UnityEngine;

public class InvincibilityBonus : ABonus
{
    [SerializeField] float duration; // Длительность бонуса в секундах
    protected override void ApplyBonus(IPlayerModel playerModel)
    {
        InvincibilityModifier invincibilityModifier = new InvincibilityModifier(duration);
        playerModel.AddInvincibilityModifier(invincibilityModifier);
    }
}
