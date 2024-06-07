using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() { instance = this; }
    [Header ("레벨 설정 맵")] public GameObject levelSetMap;
    [Header ("레벨 설정 후 활성화 할 스테이지 맵")] public GameObject lastRealMap;
    [HideInInspector] public bool isHard; // 어려운 모드인지 체크
    [Header ("난이도 텍스트")] public GameObject levelText;
    [SerializeField] [Header ("스테이지 표시 텍스트")] private TextMeshProUGUI stageText;
    [Header ("플레이어 색상 변화")] public ColorChange playerColor;
    [SerializeField] [Header ("스테이지 리스트")] private List<GameObject> stageList;
    private int curStage; // 현재 스테이지
    private bool isClear; // 게임 클리어했는지 체크
    public bool IsClear
    {
        get { return isClear; }
        set
        {
            // 게임클리어가 되면 게임클리어 호출
            isClear = value;
            ClearGame();
        }
    }
    [HideInInspector] public bool isStart; // 게임이 시작되었는지 체크
    private bool isOver; // 게임이 끝났는지 체크
    public bool IsOver
    {
        get { return isOver; }
        set
        {
            // 게임오버가 되면 게임오버 호출
            isOver = value;
            GameOver();
        }
    }
    [SerializeField] [Header ("게임오버 패널")] GameObject overPanel;
    [SerializeField] [Header ("게임메뉴 패널")] GameObject menuPanel;
    [SerializeField] [Header ("게임클리어 패널")] GameObject clearPanel;
    [SerializeField] [Header ("게임옵션 패널")] GameObject optionPanel;

    // 게임 오버
    public void GameOver() { overPanel.SetActive(true); }

    // 게임 재시작
    public void Restart() { SceneManager.LoadScene("GameScene"); }

    // 게임 시작
    public void StartGame()
    {
        isStart = true;
        menuPanel.SetActive(false);
    }

    // 게임 설정
    public void Option(bool isOption) { optionPanel.SetActive(isOption); }

    // 게임 종료
    public void Exit() { Application.Quit(); }

    // 게임 클리어
    public void ClearGame()
    {
        // 마지막 스테이지면 게임 클리어 패널 활성화
        if(curStage == stageList.Count - 1)
        {
            isStart = false;
            clearPanel.SetActive(true);
            PlayerSound.instance.PlaySFX(PlayerSFXType.클리어);
            return;
        }

        // 아직 스테이지가 남아 있으면 다음 스테이지로
        // 3번쩨 스테이지에 레벨 텍스트 띄워줌
        stageList[curStage++].SetActive(false);
        stageList[curStage].SetActive(true);
        if(curStage == stageList.Count - 2) levelText.SetActive(true);

        // 플레이어 색상 초기화 후 시작 지점으로
        playerColor.rend.color = Color.red;
        playerColor.curTime = 0f;
        playerColor.curIdx = 0;
        playerColor.gameObject.transform.position = new Vector3(-6.5f, -8.5f, 0);

        // 사운드
        PlayerSound.instance.PlaySFX(PlayerSFXType.클리어);
    }

    // 스테이지 표시 갱신
    private void LateUpdate() { stageText.text = "stage : " + (curStage + 1); }

    // 다음 스테이지 이동 시 플레이어 잠시 이동 막음
    public IEnumerator PlayerStop()
    {
        // 이전 이동 중지
        if(playerColor.GetComponent<PlayerController>().moveCo != null)
        {
            playerColor.GetComponent<PlayerController>().StopCoroutine(playerColor.GetComponent<PlayerController>().moveCo);
            playerColor.GetComponent<PlayerController>().moveCo = null;
        }

        // 1초 동안 이동 막음
        playerColor.GetComponent<PlayerController>().isMove = true;
        yield return new WaitForSeconds(1f);
        playerColor.GetComponent<PlayerController>().isMove = false;
    }
}
