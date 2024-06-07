using UnityEngine;

public class SquareOutLine : MonoBehaviour
{
    [Header ("세로, 가로, 두께")] [SerializeField] private float n = 1.0f, m = 1.0f, k = 0.1f;
    private LineRenderer lineRenderer; // 라인렌더러

    // 라인렌더러 초기화
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 5;
        lineRenderer.startWidth = k;
        lineRenderer.endWidth = k;

        Vector3[] vertex = new Vector3[5];
        vertex[0] = new Vector3(0, 0, 0);
        vertex[1] = new Vector3(0, n, 0);
        vertex[2] = new Vector3(m, n, 0);
        vertex[3] = new Vector3(m, 0, 0);
        vertex[4] = vertex[0];

        lineRenderer.SetPositions(vertex);
    }

    // 부모 위치로 업데이트
    private void Update() { UpdateOutLine(); }
    private void UpdateOutLine()
    {
        Vector3 parentPos = GetComponentInParent<Transform>().position;

        Vector3[] vertex = new Vector3[5];
        vertex[0] = new Vector3(parentPos.x - m / 2, parentPos.y - n / 2, 0);
        vertex[1] = new Vector3(parentPos.x - m / 2, parentPos.y + n / 2, 0);
        vertex[2] = new Vector3(parentPos.x + m / 2, parentPos.y + n / 2, 0);
        vertex[3] = new Vector3(parentPos.x + m / 2, parentPos.y - n / 2, 0);
        vertex[4] = vertex[0];

        lineRenderer.SetPositions(vertex);
    }
}
