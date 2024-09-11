using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 게임 타이틀 UI와 관련된 부분을 처리하는 클래스
public class TitleUIManager : MonoBehaviour
{
    public static TitleUIManager instance;

    // 타이틀 화면 버튼들
    [SerializeField] Button soundButton;

    [SerializeField] Button startButton;
    [SerializeField] Button recordButton;
    [SerializeField] Button howToPlayButton;
    [SerializeField] Button quitButton;

    // 사운드 버튼 아이콘
    [SerializeField] Image soundButtonIcon;

    // 사운드 아이콘 이미지 (0: 최소 ~ 2: 최대)
    [SerializeField] Sprite[] soundIconImages = new Sprite[3];

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AddButtonListener(); // 버튼 리스너 추가
    }

    // 버튼 리스너 추가
    void AddButtonListener()
    {
        soundButton.onClick.AddListener(ClickSoundButtonButton);

        startButton.onClick.AddListener(ClickStartButton);
        recordButton.onClick.AddListener(ClickRecordButton);
        howToPlayButton.onClick.AddListener(ClickHowToPlayButton);
        quitButton.onClick.AddListener(ClickQuitButton);
    }

    void Update()
    {
        // 테스트용 (후에 슬라이드바 내용으로 변경)
        int index = (SoundManager.instance.volume + 49) / 50;

        soundButtonIcon.sprite = soundIconImages[index];
    }

    void ClickSoundButtonButton()
    {
        // 클릭시 슬라이드바 나오게
    }

    // 게임 시작 버튼 클릭
    void ClickStartButton()
    {

    }

    // 게임 기록 버튼 클릭
    void ClickRecordButton()
    {

    }

    // 게임 방법 버튼 클릭
    void ClickHowToPlayButton()
    {

    }

    // 게임 종료 버튼 클릭
    void ClickQuitButton()
    {
        Application.Quit();
    }
}
