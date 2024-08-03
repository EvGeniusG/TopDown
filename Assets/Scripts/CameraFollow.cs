using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform minBound;
    [SerializeField] Transform maxBound;

    private Transform playerTransform;
    private Camera cam;

    private float halfHeight;
    private float halfWidth;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam.orthographic)
        {
            halfHeight = cam.orthographicSize;
            halfWidth = cam.orthographicSize * cam.aspect;
        }
        else
        {
            Debug.LogError("Этот скрипт предназначен только для ортографической камеры.");
        }
    }

    private void LateUpdate()
    {
        if (PlayerManager.Instance.GetPlayer() != null)
        {
            playerTransform = PlayerManager.Instance.GetPlayer().transform;
            Vector3 targetPosition = playerTransform.position;

            // Ограничение позиции камеры в пределах границ карты с учетом размера камеры
            float clampedX = Mathf.Clamp(targetPosition.x, minBound.position.x + halfWidth, maxBound.position.x - halfWidth);
            float clampedZ = Mathf.Clamp(targetPosition.z, minBound.position.z + halfHeight, maxBound.position.z - halfHeight);

            transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
        }
    }
}
