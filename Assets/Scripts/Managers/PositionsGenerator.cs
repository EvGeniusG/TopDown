using UnityEngine;
using System.Collections.Generic;

public class PositionGenerator : MonoBehaviour
{
    public static PositionGenerator Instance { get; private set; }

    [SerializeField] Transform minArea, maxArea;
    [SerializeField] float gridSpacing = 1, minDistanceFromEdges = 1f; // Шаг сетки и минимальное расстояние от краев
    List<Vector3> positions = new List<Vector3>();

    void Awake()
    {
        Instance = this;
        InitializePositions();
    }

    void InitializePositions()
    {
        if (positions.Count == 0)
        {
            positions = GenerateGridPositions();
        }
    }

    public Vector3? GetRandomPositionOutsideCameraView()
    {
        List<Vector3> validPositions = new List<Vector3>(positions);

        validPositions.RemoveAll(pos => IsPointVisible(pos));

        if (validPositions.Count == 0)
        {
            Debug.LogWarning("Нет доступных позиций вне видимости камеры, удовлетворяющих условиям.");
            return null;
        }

        return validPositions[Random.Range(0, validPositions.Count)];
    }

    public Vector3? GetRandomPositionInsideCameraView()
    {
        List<Vector3> validPositions = new List<Vector3>(positions);

        validPositions.RemoveAll(pos => !IsPointVisible(pos));

        if (validPositions.Count == 0)
        {
            Debug.LogWarning("Нет доступных позиций внутри видимости камеры, удовлетворяющих условиям.");
            return null;
        }

        return validPositions[Random.Range(0, validPositions.Count)];
    }

    private List<Vector3> GenerateGridPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        var min = minArea.position;
        var max = maxArea.position;

        for (float x = min.x + minDistanceFromEdges; x < max.x - minDistanceFromEdges; x += gridSpacing)
        {
            for (float z = min.z + minDistanceFromEdges; z < max.z - minDistanceFromEdges; z += gridSpacing)
            {
                positions.Add(new Vector3(x, 0, z));
            }
        }

        return positions;
    }

    private bool IsPointVisible(Vector3 point)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point)< 0)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsFarEnoughFromEdges(Vector3 position, Bounds bounds, float minDistance)
    {
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(position.x, bounds.min.x + minDistance, bounds.max.x - minDistance),
            position.y,
            Mathf.Clamp(position.z, bounds.min.z + minDistance, bounds.max.z - minDistance)
        );

        return Vector3.Distance(position, clampedPosition) >= minDistance;
    }
}
