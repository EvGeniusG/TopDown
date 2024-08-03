using UnityEngine;
using System.Collections.Generic;

public class PlayerModel : MonoBehaviour, IPlayerModel
{
    [SerializeField] float baseSpeed = 4f; // Базовая скорость в unit/с
    [SerializeField] float rotationSpeed = 180f; // Скорость поворота в градусах/с
    [SerializeField] Weapon weapon;

    // Список модификаторов скорости
    private List<SpeedModifier> speedModifiers = new List<SpeedModifier>();

    // Список модификаторов неуязвимости (вдруг их может быть несколько)
    private List<InvincibilityModifier> invincibilityModifiers = new List<InvincibilityModifier>();

    public float shootCooldownTimer {get; private set;} = 0;

    // Метод для получения текущей скорости с учетом всех модификаторов
    public float GetCurrentSpeed()
    {
        float currentSpeed = baseSpeed;

        foreach (var modifier in speedModifiers)
        {
            currentSpeed *= modifier.Value;
        }

        return currentSpeed;
    }

    // Метод для получения скорости поворота
    public float GetRotationSpeed()
    {
        return rotationSpeed;
    }

    // Метод для проверки, неуязвим ли игрок
    public bool IsInvincible()
    {
        return invincibilityModifiers.Count > 0;
    }

    // Метод для добавления модификатора скорости
    public void AddSpeedModifier(SpeedModifier modifier)
    {
        speedModifiers.Add(modifier);
    }

    // Метод для удаления модификатора скорости
    public void RemoveSpeedModifier(SpeedModifier modifier)
    {
        speedModifiers.Remove(modifier);
    }

    // Метод для добавления модификатора неуязвимости
    public void AddInvincibilityModifier(InvincibilityModifier modifier)
    {
        invincibilityModifiers.Add(modifier);
    }

    // Метод для удаления модификатора неуязвимости
    public void RemoveInvincibilityModifier(InvincibilityModifier modifier)
    {
        invincibilityModifiers.Remove(modifier);
    }

    private void FixedUpdate()
    {
        UpdateSpeedModifiers();
        UpdateInvincibilityModifiers();
        UpdateTimer();
        
    }

    // Метод для обновления модификаторов скорости
    private void UpdateSpeedModifiers()
    {
        for (int i = speedModifiers.Count - 1; i >= 0; i--)
        {
            if (speedModifiers[i].Duration > 0)
            {
                speedModifiers[i].Duration -= Time.fixedDeltaTime;
                if (speedModifiers[i].Duration <= 0)
                {
                    speedModifiers.RemoveAt(i);
                }
            }
        }
    }

    // Метод для обновления модификаторов неуязвимости
    private void UpdateInvincibilityModifiers()
    {
        for (int i = invincibilityModifiers.Count - 1; i >= 0; i--)
        {
            if (invincibilityModifiers[i].Duration > 0)
            {
                invincibilityModifiers[i].Duration -= Time.fixedDeltaTime;
                if (invincibilityModifiers[i].Duration <= 0)
                {
                    invincibilityModifiers.RemoveAt(i);
                }
            }
        }
    }

    void UpdateTimer(){
        shootCooldownTimer -= Time.fixedDeltaTime;
    }

    public Weapon GetWeapon() => weapon;
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        SetShootCooldownTimer(0);
    }

    public void SetShootCooldownTimer(float value) => shootCooldownTimer = value;
    public bool HasSpeedBoost()
    {
        for(int i = 0; i < speedModifiers.Count; i++){
            if(speedModifiers[i].Value > 1){
                return true;
            }
        }
        return false;
    }
}
