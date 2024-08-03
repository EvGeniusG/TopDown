public interface IPlayerModel
{
    float GetCurrentSpeed();
    float GetRotationSpeed();
    bool IsInvincible();
    void AddSpeedModifier(SpeedModifier modifier);
    void RemoveSpeedModifier(SpeedModifier modifier);
    void AddInvincibilityModifier(InvincibilityModifier modifier);
    bool HasSpeedBoost();
    void RemoveInvincibilityModifier(InvincibilityModifier modifier);
    Weapon GetWeapon();
    void SetWeapon(Weapon weapon);

    float shootCooldownTimer {get;}

    void SetShootCooldownTimer(float value);
}
