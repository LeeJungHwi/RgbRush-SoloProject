using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] [Header ("하드모드인지 체크")] private bool isHard;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // 레벨 설정 맵 비활성화
            GameManager.instance.levelSetMap.SetActive(false);

            // 레벨 텍스트 비활성화
            GameManager.instance.levelText.SetActive(false);

            // 실제 스테이지 3 활성화
            GameManager.instance.lastRealMap.SetActive(true);

            // 플레이어 색상 초기화 후 시작 지점으로
            GameManager.instance.playerColor.rend.color = Color.red;
            GameManager.instance.playerColor.curTime = 0f;
            GameManager.instance.playerColor.curIdx = 0;
            GameManager.instance.playerColor.gameObject.transform.position = new Vector3(-6.5f, -6.5f, 0);

            // 사운드
            PlayerSound.instance.PlaySFX(PlayerSFXType.클리어);

            // 하드모드라면 이동방향 반대로 설정
            if(isHard) GameManager.instance.isHard = true;
        }
    }
}
