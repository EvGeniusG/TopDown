using UnityEngine;

public class GLauncherBullet : ABullet
{
    public float ExplosionRadius = 2f;
    private float fireDistance = 0;
    [SerializeField] GameObject explosionPrefab;
    
    public override void Initialize(Vector3 startPosition, Vector3 direction)
    {
        base.Initialize(startPosition, direction);
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.y = startPosition.y;
        fireDistance = (target - startPosition).magnitude;
        transform.LookAt(target);
    }

    protected override void FixedUpdate()
    {
        float distance = Weapon.BulletSpeed * Time.fixedDeltaTime;
        transform.position += direction * distance;
        traveledDistance += distance;

        if (traveledDistance >= fireDistance)
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Instantiate explosion effect
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Find all enemies in the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            IEnemyController enemy = hitCollider.GetComponent<IEnemyController>();
            if (enemy != null)
            {
                enemy.Hit(Weapon.Damage);
            }
        }

        // Return bullet to the pool
        BulletPoolManager.Instance.ReturnBullet(this);
    }

    private void OnDisable()
    {
        Explode();
    }
}
