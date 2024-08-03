using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{

    [Header("Weapon Stats")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireDistance = Mathf.Infinity;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int bulletsCountInShot = 1;

    public string Name => name;
    public float Damage => damage;
    public float FireRate => fireRate;
    public float FireDistance => fireDistance;
    public float BulletSpeed => bulletSpeed;
    public int BulletsCountInShot => bulletsCountInShot;
}
