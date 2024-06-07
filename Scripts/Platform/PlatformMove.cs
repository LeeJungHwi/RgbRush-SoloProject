using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] [Header ("첫 위치")] private Transform first;
    [SerializeField] [Header ("둘 위치")] private Transform second;
    private Transform target; // 목표위치
    [SerializeField] [Header ("이동 속도")] private float speed;

    // 처음 초기화
    private void Awake()
    {
        transform.position = first.position;
        target = second;
    }

    private void FixedUpdate()
    {
        // 플랫폼 이동
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // 목표위치 변경
        if(Vector2.Distance(transform.position, target.position) <= 0.05f)
        {
            if(target == second)
            {
                target = first;
                return;
            }

            target = second;
        }
    }
}
