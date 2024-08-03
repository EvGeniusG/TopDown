using UnityEngine;

public class BonusPool : MonoBehaviour
{
    public static BonusPool Instance { get; private set; }

    [SerializeField] private GameObject[] bonusPrefabs;
    private GameObject[] bonuses;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bonuses = new GameObject[bonusPrefabs.Length];
        for (int i = 0; i < bonusPrefabs.Length; i++)
        {
            bonuses[i] = Instantiate(bonusPrefabs[i]);
            bonuses[i].SetActive(false);
        }
    }

    public void SetBonus(int index, Vector3 position)
    {
        if (index >= 0 && index < bonuses.Length)
        {
            bonuses[index].transform.position = position;
            bonuses[index].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Invalid bonus index");
        }
    }

    public void SetRandomBonus(Vector3 position)
    {
        int randomBonus = Random.Range(0, bonuses.Length);
        SetBonus(randomBonus, position);
    }
}
