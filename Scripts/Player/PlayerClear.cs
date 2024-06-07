using UnityEngine;

public class PlayerClear : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        // 다음 스테이지 및 게임 클리어
        if(other.gameObject.CompareTag("Clear"))
        {
            StartCoroutine(GameManager.instance.PlayerStop());
            GameManager.instance.IsClear = true;
        }
    }
}
