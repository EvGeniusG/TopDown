using UnityEngine;

public class PCControl : MonoBehaviour
{
    private IPlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<IPlayerController>();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleShootingInput();
    }

    private void HandleMovementInput()
    {
        // Получаем значения ввода по горизонтали и вертикали
        float moveVertical = Input.GetAxisRaw("Vertical"); // W/S или Up/Down arrow keys
        float moveHorizontal = Input.GetAxisRaw("Horizontal"); // A/D или Left/Right arrow keys

        // Создаем вектор направления движения, учитывая горизонтальное и вертикальное направление
        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        // Устанавливаем направление движения в контроллере
        playerController.SetMoveDirection(moveDirection);
    }

    private void HandleShootingInput()
    {
        if (Input.GetMouseButton(0)) // Левая кнопка мыши
        {
            // Получаем направление на цель (положение курсора мыши в мировых координатах)
            Vector3 targetDirection = GetMouseWorldPosition() - playerController.playerTransform.position;
            targetDirection.y = 0; // Игнорируем вращение по оси Y

            // Устанавливаем направление стрельбы в контроллере
            playerController.SetTargetDirection(targetDirection);
        }
        else
        {
            // Останавливаем стрельбу, если кнопка мыши отпущена
            playerController.StopShooting();
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            return hitInfo.point;
        }
        return Vector3.zero;
    }
}
