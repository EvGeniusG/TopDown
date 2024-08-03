using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, IPlayerController
{
    private IPlayerModel model;
    private IPlayerView view;
    private NavMeshAgent navMeshAgent;
    public Transform playerTransform { get; private set; }
    public Vector3 move { get; private set; }
    private Vector3 moveDirection;
    private Vector3 targetDirection;
    [SerializeField] Transform bulletSpawn;

    private void Start()
    {
        model = GetComponent<IPlayerModel>();
        view = GetComponent<IPlayerView>();
        playerTransform = GetComponent<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Установка начальной скорости
        navMeshAgent.speed = model.GetCurrentSpeed();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleShootingRotation();
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    public void SetTargetDirection(Vector3 direction)
    {
        targetDirection = direction;
    }

    public void StopShooting()
    {
        targetDirection = Vector3.zero;
    }

    private void HandleMovement()
    {
        // Рассчитываем движение
        move = moveDirection * model.GetCurrentSpeed() * Time.fixedDeltaTime;

        // Если есть движение, перемещаем агента напрямую
        if (moveDirection != Vector3.zero)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.Move(move);
        }
        else
        {
            // Останавливаем агента, если нет ввода
            navMeshAgent.isStopped = true;
        }
    }

    private void HandleShootingRotation()
    {
        if (targetDirection != Vector3.zero)
        {
            // Рассчитываем целевое вращение
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            // Поворачиваем игрока к целевому вращению
            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, targetRotation, model.GetRotationSpeed() * Time.fixedDeltaTime);

            // Проверяем, достиг ли игрок нужного угла поворота
            if (Quaternion.Angle(playerTransform.rotation, targetRotation) < 1f) // 1 градус допуска
            {
                // Если таймер перезарядки истек, стреляем
                if (model.shootCooldownTimer <= 0)
                {
                    Shoot();
                    model.SetShootCooldownTimer(1 / model.GetWeapon().FireRate); // Устанавливаем таймер перезарядки
                }
            }
        }
        else //Пусть игрок без цели поворачивается в сторону ходьбы
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            // Поворачиваем игрока к целевому вращению
            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, targetRotation, model.GetRotationSpeed() * Time.fixedDeltaTime);

        }
    }

    public void Shoot()
    {
        // Получаем данные об оружии
        Weapon weapon = model.GetWeapon();
        Vector3 fireDirection = targetDirection.normalized;
        // Вызываем метод стрельбы через пул
        BulletPoolManager.Instance.Shoot(weapon, bulletSpawn.position, fireDirection);
    }

    // Метод для обработки смерти игрока
    public void Hit(IPlayerDamageSource source)
    {
        if(model.IsInvincible() && !source.IsDamageUnavoidable()) return; // Урон не проходит, если есть неуязвимость

        // Активируем рэгдолл
        view.ActivateRagdoll();

        PlayerManager.Instance.DestroyPlayer();

        //Выход из игры
        GameManager.Instance.SetState(new EndMenuState());
    }

    public IPlayerModel GetModel() => model;
}
