using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public ABullet bulletPrefab;
    [SerializeField] int initialSize = 10;

    private Queue<ABullet> poolQueue;

    private void Awake()
    {
        poolQueue = new Queue<ABullet>();

        for (int i = 0; i < initialSize; i++)
        {
            ABullet newBullet = Instantiate(bulletPrefab);
            newBullet.gameObject.SetActive(false);
            poolQueue.Enqueue(newBullet);
        }
    }

    public ABullet GetBullet()
    {
        if (poolQueue.Count > 0)
        {

            ABullet bullet = poolQueue.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            ABullet newBullet = Instantiate(bulletPrefab);
            return newBullet;
        }
    }

    public void ReturnBullet(ABullet bullet)
    {
        bullet.gameObject.SetActive(false);
        poolQueue.Enqueue(bullet);
    }
}
