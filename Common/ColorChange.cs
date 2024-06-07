using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [Header ("색 바꿀 렌더러")] public SpriteRenderer rend;
    [Header ("R G B")] [SerializeField] private List<Color> colorList;
    [Header ("색상 변화 간격")] [SerializeField] private float interval; // 색상 변화 간격
    [HideInInspector] public float curTime; // 현재 시간
    [HideInInspector] public int curIdx; // 현재 색상 인덱스

    private void Update()
    {
        // 플레이어 죽은 상태면 리턴
        if(GameManager.instance.IsOver) return;

        // 게임이 시작되지 않았으면 리턴
        if(!GameManager.instance.isStart) return;

        // 색상 변화
        curTime += Time.deltaTime;

        if(curTime > interval)
        {
            curTime = 0;
            curIdx = (curIdx + 1) % colorList.Count;
            rend.color = colorList[curIdx];
        }
    }
}
