using System.Collections.Generic;
using UnityEngine;

public class EquipmentPool : MonoBehaviour
{
    public static EquipmentPool Instance { get; private set; }

    [SerializeField] private WeaponEquipmentBonus[] equipmentPrefabs; // Префабы оружия
    private WeaponEquipmentBonus[] equipments; // Экземпляры оружия

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        equipments = new WeaponEquipmentBonus[equipmentPrefabs.Length];
        for (int i = 0; i < equipmentPrefabs.Length; i++)
        {
            equipments[i] = Instantiate(equipmentPrefabs[i]);
            equipments[i].gameObject.SetActive(false);
        }
    }

    public void SetEquipment(int index, Vector3 position)
    {
        if (index >= 0 && index < equipments.Length)
        {
            equipments[index].transform.position = position;
            equipments[index].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Invalid equipment index");
        }
    }

    public void SetRandomEquipment(Vector3 position)
    {
        IPlayerModel playerModel = PlayerManager.Instance.GetPlayerModel();
        if (playerModel == null)
        {
            return;
        }

        // Получаем список оборудования, которого нет у игрока
        List<int> availableEquipments = new List<int>();
        for (int i = 0; i < equipments.Length; i++)
        {
            if (playerModel.GetWeapon() != equipments[i].GetComponent<WeaponEquipmentBonus>().Weapon)
            {
                availableEquipments.Add(i);
            }
        }

        if (availableEquipments.Count == 0)
        {
            return;
        }

        int randomIndex = availableEquipments[Random.Range(0, availableEquipments.Count)];
        SetEquipment(randomIndex, position);
    }
}
