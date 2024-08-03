using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance { get; private set; }
    
    [SerializeField] List<Weapon> weapons = new List<Weapon>();
    [SerializeField] List<BulletPool> bulletPools= new List<BulletPool>();

    private Dictionary<Weapon, BulletPool> poolsMap = new Dictionary<Weapon, BulletPool>();

    private void Awake()
    {
        Instance = this;
        InitializeBulletPoolsMap();
    }

    private void InitializeBulletPoolsMap()
    {
        // Убедитесь, что списки не пусты и имеют одинаковую длину
        if (weapons.Count != bulletPools.Count)
        {
            Debug.LogError("Weapons and bullet pools lists must have the same number of elements.");
            return;
        }

        // Очистка словаря перед инициализацией
        poolsMap.Clear();

        for (int i = 0; i < weapons.Count; i++)
        {
            Weapon weapon = weapons[i];
            BulletPool bulletPool = bulletPools[i];

            // Добавляем соответствие в словарь
            poolsMap[weapon] = bulletPool;
        }
    }

    public void Shoot(Weapon weapon, Vector3 startPosition, Vector3 direction)
    {
        int bulletsCount = weapon.BulletsCountInShot;
        float spreadAngle = 10f; // Угол разброса в градусах

        if (bulletsCount <= 0)
        {
            Debug.LogWarning("BulletsCountInShot must be greater than zero.");
            return;
        }

        if (bulletsCount == 1)
        {
            // Если только одна пуля, стреляем по основному направлению
            ShootSingleBullet(weapon, startPosition, direction);
        }
        else
        {
            // Если несколько пуль, применяем разброс
            ShootBulletsWithSpread(weapon, startPosition, direction, bulletsCount, spreadAngle);
        }
    }

    private void ShootSingleBullet(Weapon weapon, Vector3 startPosition, Vector3 direction)
    {
        ABullet bullet = null;

        if(poolsMap.ContainsKey(weapon)){
            bullet = poolsMap[weapon].GetBullet();
        }

        if (bullet != null)
        {
            bullet.Initialize(startPosition, direction);
        }
    }

    private void ShootBulletsWithSpread(Weapon weapon, Vector3 startPosition, Vector3 direction, int bulletsCount, float spreadAngle)
    {
        // Создаем временный Transform для расчета направления пули
        Transform tempTransform = new GameObject("TempTransform").transform;
        tempTransform.position = startPosition;

        // Распределяем угол разброса по количеству пуль
        float halfSpread = spreadAngle / 2f;
        float angleStep = spreadAngle / (bulletsCount - 1);


        if(poolsMap.ContainsKey(weapon))
        {
            for (int i = 0; i < bulletsCount; i++)
            {
                ABullet bullet = null;

                bullet = poolsMap[weapon].GetBullet();

                // Вычисляем угол для текущей пули
                float angle = -halfSpread + (angleStep * i);

                // Устанавливаем начальное направление
                tempTransform.rotation = Quaternion.LookRotation(direction);

                // Поворачиваем временный Transform влево на текущий угол
                tempTransform.Rotate(0, angle, 0);

                // Получаем направление из временного Transform
                Vector3 spreadDirection = tempTransform.forward;
                // Инициализируем пулю
                bullet.Initialize(startPosition, spreadDirection);
            }     
        }
        

        // Удаляем временный объект после использования
        Destroy(tempTransform.gameObject);
    }





    public void ReturnBullet(ABullet bullet)
    {
        var weapon = bullet.Weapon;
        if(poolsMap.ContainsKey(weapon)){
            poolsMap[weapon].ReturnBullet(bullet);
        }
    }
}
