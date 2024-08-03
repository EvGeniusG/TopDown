using UnityEngine;

public abstract class ABonus : MonoBehaviour
{
    [SerializeField] protected float activeTime; // Время, после которого бонус исчезнет, если не активирован

    private float timer;
    private bool isActivated;

    protected virtual void OnEnable()
    {
        // Запускаем таймер на исчезновение бонуса
        timer = activeTime;
        isActivated = false;
    }

    private void Update()
    {
        if (!isActivated)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                gameObject.SetActive(false); // Деактивируем бонус, если время истекло
            }
        }
    }

    protected abstract void ApplyBonus(IPlayerModel playerModel);

    private void OnTriggerEnter(Collider other)
    {
        IPlayerModel player = other.GetComponent<IPlayerModel>();
        if (player != null)
        {
            ApplyBonus(player);
            isActivated = true; // Устанавливаем флаг активации бонуса
            timer = 0; // Устанавливаем таймер в 0, чтобы объект исчез сразу
            gameObject.SetActive(false); // Деактивируем бонус после использования
        }
    }
}
