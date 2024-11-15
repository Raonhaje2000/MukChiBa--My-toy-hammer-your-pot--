using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// 게임 타이틀 UI와 관련된 부분을 처리하는 클래스
public class TitleManager : MonoBehaviour
{
    static TitleManager instance;

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

    // 사운드 바 관련
    [SerializeField] GameObject soundBarObject;
    [SerializeField] Slider soundBar;
    [SerializeField] TextMeshProUGUI soundBarText;

    public static TitleManager Instance
    {
        get { return instance; }
    }

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
        AddListeners(); // 리스너 추가

        soundBarObject.SetActive(false);

        SetSoundUI(SoundManager.Instance.BgmVolume);

        SoundManager.Instance.PlayBgm(SoundManager.BGM.Title); // BGM 재생
    }

    // 리스너 추가
    void AddListeners()
    {
        soundButton.onClick.AddListener(ClickSoundButton);

        startButton.onClick.AddListener(ClickStartButton);
        recordButton.onClick.AddListener(ClickRecordButton);
        howToPlayButton.onClick.AddListener(ClickHowToPlayButton);
        quitButton.onClick.AddListener(ClickQuitButton);

        soundBar.onValueChanged.AddListener(ChangeSoundBarValue);
    }

    // 사운드 버튼 클릭
    void ClickSoundButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        soundBarObject.SetActive(!soundBarObject.activeSelf);
    }

    // 게임 시작 버튼 클릭
    void ClickStartButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        SceneManager.LoadScene("GameScene");
    }

    // 게임 기록 버튼 클릭
    void ClickRecordButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        SceneManager.LoadScene("RecordScene");
    }

    // 게임 방법 버튼 클릭
    void ClickHowToPlayButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);
    }

    // 게임 종료 버튼 클릭
    void ClickQuitButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        Application.Quit();
    }

    // 사운드 바 값 변경
    void ChangeSoundBarValue(float currentValue)
    {
        int volume = Mathf.RoundToInt(currentValue);

        // 사운드 UI 설정
        SetSoundUI(volume);

        SoundManager.Instance.ChangeBgmVolume(volume);
    }

    // 사운드 UI 설정
    void SetSoundUI(int currentVolume)
    {
        // 버튼 아이콘 변경
        int index = (currentVolume + 49) / 50;

        soundButtonIcon.sprite = soundIconImages[index];

        // 바 값 변경
        soundBar.value = currentVolume;

        // 텍스트 변경
        soundBarText.text = currentVolume.ToString();
    }
}
