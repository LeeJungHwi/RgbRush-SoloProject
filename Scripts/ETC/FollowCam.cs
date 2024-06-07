using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private GameObject player; // 플레이어
    [SerializeField] private Vector3 offset; // 플레이어와 거리 조절

    private void Update() { transform.position = player.transform.position + offset; }
}
