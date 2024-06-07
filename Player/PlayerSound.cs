using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 사운드 타입
public enum PlayerSFXType { 이동, 점프, 죽음, 클리어, 스위치, 버튼엔터, 버튼클릭 }

public class PlayerSound : MonoBehaviour
{
    public static PlayerSound instance;
    private void Awake() { instance = this; }
    [SerializeField] [Header ("플레이어 오디오소스")] private AudioSource audioSource;
    [SerializeField] [Header ("SFX 리스트")] private List<AudioClip> sfxList;
    [Header ("메인카메라 오디오소스")] public AudioSource bgmAudioSource;
    [Header ("BGM 슬라이더")] [SerializeField] private Slider bgmSlider;
    [Header ("SFX 슬라이더")] [SerializeField] private Slider sfxSlider;
    private float bgmVolume, sfxVolume; // BGM, SFX 볼륨

    // 슬라이더에 볼륨 조절 세팅 함수 연결
    private void Start()
    {
        bgmVolume = 0.1f;
        sfxVolume = 1f;
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    } 

    // 사운드 재생
    public void PlaySFX(PlayerSFXType type) { audioSource.PlayOneShot(sfxList[(int)type], sfxVolume); }

    // BGM 볼륨 셋
    public void SetBgmVolume(float volume)
    {
        bgmAudioSource.volume = volume;
        bgmVolume = volume;
    }

    // SFX 볼륨 셋
    public void SetSfxVolume(float volume) { sfxVolume = volume; }
}
