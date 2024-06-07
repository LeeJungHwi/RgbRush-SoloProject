using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    // 버튼 엔터 사운드
    public void OnPointerEnter(PointerEventData eventData) { PlayerSound.instance.PlaySFX(PlayerSFXType.버튼엔터); }

    // 버튼 클릭 사운드
    public void OnPointerClick(PointerEventData eventData) { PlayerSound.instance.PlaySFX(PlayerSFXType.버튼클릭); }
}
