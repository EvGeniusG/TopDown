using UnityEngine;

public class RifleBullet : ABullet
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