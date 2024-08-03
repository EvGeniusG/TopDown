using Unity.VisualScripting;
using UnityEngine;

public abstract class ABullet : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    public Weapon Weapon => _weapon;

    protected Vector3 startPosition;
    protected Vector3 direction;
    protected float traveledDistance;

    public virtual void Initialize(Vector3 startPosition, Vector3 direction)
    {
        this.startPosition = startPosition;
        this.direction = direction.normalized;
        traveledDistance = 0f;

        transform.position = startPosition;
    }

    public virtual void OnEnable()
    {
        traveledDistance = 0f;
    }

    protected virtual void FixedUpdate()
    {
        float distance = Weapon.BulletSpeed * Time.fixedDeltaTime;
        transform.position += direction * distance;
        traveledDistance += distance;

        if (traveledDistance >= Weapon.FireDistance)
        {
            gameObject.SetActive(false);
        }
    }

    void OnBecameInvisible(){
        BulletPoolManager.Instance.ReturnBullet(this);
    }
}
