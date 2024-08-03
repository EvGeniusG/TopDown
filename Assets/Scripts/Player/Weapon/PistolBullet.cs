using UnityEngine;

public class PistolBullet : ABullet
{

    void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<IEnemyController>();

        if(enemy != null)
        {
            enemy.Hit(Weapon.Damage);
        }
        BulletPoolManager.Instance.ReturnBullet(this);
    }
}
