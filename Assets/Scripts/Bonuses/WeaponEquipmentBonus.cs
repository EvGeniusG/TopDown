using UnityEngine;

public class WeaponEquipmentBonus : ABonus
{
    [SerializeField] private Weapon weapon;
    public Weapon Weapon => weapon;

    protected override void ApplyBonus(IPlayerModel playerModel)
    {
        playerModel.SetWeapon(weapon);
    }
}
