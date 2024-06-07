using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [Header ("플레이어 렌더러")] public SpriteRenderer rend;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 다른색 플랫폼에 닿았을때 죽음
        if(other.gameObject.CompareTag("Platform"))
        {
            if(rend.color != other.gameObject.GetComponent<SpriteRenderer>().color && !GameManager.instance.IsOver)
            {
                // 죽은 상태
                GameManager.instance.IsOver = true;

                // 사운드
                PlayerSound.instance.PlaySFX(PlayerSFXType.죽음);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 가시에 닿았을때 죽음
        if(other.gameObject.CompareTag("Spike") && !GameManager.instance.IsOver)
        {
            // 죽은 상태
            GameManager.instance.IsOver = true;

            // 사운드
            PlayerSound.instance.PlaySFX(PlayerSFXType.죽음);
        }
        
        // 떨어지면 죽음
        if(other.gameObject.CompareTag("Fall") && !GameManager.instance.IsOver)
        {
            // 죽은 상태
            GameManager.instance.IsOver = true;

            // 사운드
            PlayerSound.instance.PlaySFX(PlayerSFXType.죽음);
        }
    }
}
