using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [SerializeField] private float slowModifier = 0.6f; // Замедление до 60%

    SpeedModifier speedModifier;

    void Awake(){
        speedModifier = new SpeedModifier(slowModifier, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, входит ли игрок в зону замедления
        IPlayerModel playerModel = other.GetComponent<IPlayerModel>();
        if (playerModel != null)
        {
            // Добавляем модификатор замедления
            playerModel.AddSpeedModifier(speedModifier); // 0f для перманентного замедления пока в зоне
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, выходит ли игрок из зоны замедления
        IPlayerModel playerModel = other.GetComponent<IPlayerModel>();
        if (playerModel != null)
        {
            // Удаляем модификатор замедления
            playerModel.RemoveSpeedModifier(speedModifier); // Удаление замедления
        }
    }
}
