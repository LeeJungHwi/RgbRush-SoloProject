using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 이동 관련
    private float xAxis; // 좌우 입력값
    [SerializeField] [Header ("이동 속도")] private float moveSpeed;
    [HideInInspector] public bool isMove; // 이동중인지 체크
    private int moveCnt; // 점프 상태에서 움직인 횟수

    // 플레이어 점프 관련
    [SerializeField] [Header ("플레이어 물리")] private Rigidbody2D rigid;
    private bool isJump; // 점프중인지 체크
    [SerializeField] [Header ("점프력")] private float jumpPower;
    public Coroutine moveCo;

    private void Update()
    {
        // 플레이어 죽은 상태면 리턴
        if(GameManager.instance.IsOver) return;

        // 게임이 시작되지 않았으면 리턴
        if(!GameManager.instance.isStart) return;

        // 플레이어 이동
        // 좌우 입력값이 있고
        // 점프 상태에서 움직인 횟수가 2보다 작으면서
        // 진행방향에 플랫폼이 없으면
        // 플레이어 이동
        // 하드모드면 이동방향 반대로
        if(!isMove)
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            if(xAxis != 0 && moveCnt < 2 && !PlatformCheck(GameManager.instance.isHard ? -xAxis : xAxis))
            {
                // 이전 이동 중지
                if(moveCo != null)
                {
                    StopCoroutine(moveCo);
                    moveCo = null;
                }

                // 현재 이동
                moveCo = StartCoroutine(Move(GameManager.instance.isHard ? -xAxis : xAxis));
            }
        }

        // 끼었을때 1번을 누르면 아래 플랫폼 위치를 기준으로 플레이어 위치 재설정
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Transform downPlatformPos = DownPlatformPos();
            if(downPlatformPos != null) transform.position = downPlatformPos.position + new Vector3(0, 1f, 0);
        }
    }

    private void FixedUpdate()
    {
        // 플레이어 죽은 상태면 리턴
        if(GameManager.instance.IsOver) return;

        // 게임이 시작되지 않았으면 리턴
        if(!GameManager.instance.isStart) return;

        // 플레이어 점프
        if(!isJump && Input.GetKey(KeyCode.Space)) Jump();
    }

    // 플레이어 이동
    private IEnumerator Move(float direction)
    {
        // 이동중
        isMove = true;

        // 사운드
        PlayerSound.instance.PlaySFX(PlayerSFXType.이동);

        // 이동 할 위치
        Vector3 movePos = transform.position + Vector3.right * direction;

        // 플레이어 이동
        while(transform.position != movePos)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // 점프 상태에서 움직인 횟수 카운팅
        if(isJump) moveCnt++;

        // 이동끝
        isMove = false;
    }

    // 플레이어 점프
    private void Jump()
    {
        // 점프중
        isJump = true;

        // 점프속도 초기화
        rigid.velocity = new Vector2(rigid.velocity.x, 0);

        // 플레이어 점프
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        // 사운드
        PlayerSound.instance.PlaySFX(PlayerSFXType.점프);
    }

    // 플랫폼에 닿았는지 체크
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            // 플래그 초기화
            isJump = false;
            moveCnt = 0;

            // 무빙 플랫폼이면 부모로
            if(other.gameObject.name[8] == 'M') transform.SetParent(other.transform);
        }
    }

    // 무빙 플랫폼에 플레이어 고정
    private void OnCollisionStay2D(Collision2D other) { if(other.gameObject.CompareTag("Platform") && other.gameObject.name[8] == 'M' && !isJump) transform.position = other.transform.position + new Vector3(0, 1f, 0); }

    // 무빙 플랫폼 부모 해제
    private void OnCollisionExit2D(Collision2D other) { if(other.gameObject.CompareTag("Platform") && other.gameObject.name[8] == 'M') transform.parent = null; }

    // 진행 방향에 플랫폼이 있는지 체크
    private bool PlatformCheck(float direction)
    {
        Vector2 rayDir = new Vector2(direction, 0f); // 레이 방향
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, 0.4f, 0), rayDir, 1f, LayerMask.GetMask("Platform"));
        return hit.collider != null;
    }

    // 아래 방향 플랫폼 위치 체크
    private Transform DownPlatformPos()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Platform"));
        return hit.collider != null ? hit.collider.transform : null;
    }
}
