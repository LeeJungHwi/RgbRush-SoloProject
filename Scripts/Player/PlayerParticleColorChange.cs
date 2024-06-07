using UnityEngine;

public class PlayerParticleColorChange : MonoBehaviour
{
    [SerializeField] [Header ("플레이어 파티클 이펙트")] private ParticleSystem playerEffect;
    private ParticleSystem.MainModule main; // 파티클 시스템 메인

    // 초기화
    private void Awake() { main = playerEffect.main; }

    // 플레이어 파티클 색상 변경
    private void Update() { main.startColor = GetComponent<SpriteRenderer>().color; }
}
