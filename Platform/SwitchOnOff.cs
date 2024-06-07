using UnityEngine;

public class SwitchOnOff : MonoBehaviour
{
    [SerializeField] [Header ("벽")] private GameObject wall;
    private SpriteRenderer wallRend; // 벽 렌더러
    private BoxCollider2D wallCollider; // 벽 콜라이더
    private bool isOn; // 스위치가 켜진 상태인지 체크

    // 초기화
    private void Awake()
    {
        wallRend = wall.GetComponent<SpriteRenderer>();
        wallCollider = wall.GetComponent<BoxCollider2D>();
    }

    // 스위치 온/오프
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // isOn == false일 때 트리거되면 : isOn = true, alpha = 10, collider = false, switch scaleY .2
            // isOn == true일 때 트리거되면 : isOn = false, alpha = 255, collider = true, switch scaleY .5
            Color wallColor = wallRend.color;
            wallColor.a = isOn ? 1.0f : 0.1f;
            wallRend.color = wallColor;
            wallCollider.enabled = isOn;
            Vector3 scale = transform.localScale;
            scale.y = isOn ? 0.5f : 0.2f;
            transform.localScale = scale;
            isOn = !isOn;
            PlayerSound.instance.PlaySFX(PlayerSFXType.스위치);
        }
    }
}
