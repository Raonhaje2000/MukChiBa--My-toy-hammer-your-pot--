using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 게임 UI와 관련된 부분을 처리하는 클래스
public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    // 체력
    [SerializeField] Image[] playerHpImage = new Image[3]; // 플레이어 체력
    [SerializeField] Image[] computerHpImage = new Image[3]; // 컴퓨터 체력

    // 전체 플레이 시간 텍스트
    [SerializeField] TextMeshProUGUI playTimeText;

    // 일시 정지 버튼
    [SerializeField] Button pauseButton;

    // 캐릭터 말풍선 이미지
    [SerializeField] Image playerSpeechBubbleImage;
    [SerializeField] Image computerSpeechBubbleImage;

    // 캐릭터 선택 이미지
    [SerializeField] Image playerSelectionImage;
    [SerializeField] Image computerSelectionImage;

    // 캐릭터 이미지
    [SerializeField] Image characterImage;

    // 제한 시간
    [SerializeField] Slider timerBar;

    // 공격 방어 버튼
    [SerializeField] Button attackButton;
    [SerializeField] Button defenceButton;

    // 묵찌빠 버튼
    [SerializeField] Button mukButton;
    [SerializeField] Button chiButton;
    [SerializeField] Button baButton;

    // 묵찌빠 이미지
    [SerializeField] Sprite mukImage;
    [SerializeField] Sprite chiImage;
    [SerializeField] Sprite baImage;

    Character characterComponent;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            characterComponent = gameObject.GetComponent<Character>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AddListeners(); // 리스너 추가

        Initialize(); // 초기화
    }

    // 초기화
    void Initialize()
    {
        InitializeSelectionImage();

        characterImage.sprite = characterComponent.ReturnImage(Character.State.Initial);
    }

    // 리스너 추가
    void AddListeners()
    {
        pauseButton.onClick.AddListener(ClickPauseButton);

        attackButton.onClick.AddListener(ClickAttackButton);
        defenceButton.onClick.AddListener(ClickDefenceButton);

        mukButton.onClick.AddListener(ClickMukButton);
        chiButton.onClick.AddListener(ClickChiButton);
        baButton.onClick.AddListener(ClickBaButton);
    }

    void ClickPauseButton()
    {

    }

    void ClickAttackButton()
    {

    }

    void ClickDefenceButton()
    {
        
    }

    void ClickMukButton()
    {
        playerSelectionImage.sprite = mukImage;

        GameManager.instance.ChangePlayerSelection(GameManager.MukChiBa.Muk);
    }

    void ClickChiButton()
    {
        playerSelectionImage.sprite = chiImage;

        GameManager.instance.ChangePlayerSelection(GameManager.MukChiBa.Chi);
    }

    void ClickBaButton()
    {
        playerSelectionImage.sprite = baImage;

        GameManager.instance.ChangePlayerSelection(GameManager.MukChiBa.Ba);
    }

    // 전체 플레이 시간 텍스트 업데이트
    public void UpdatePlayTimeText(float time)
    {
        int min = Mathf.FloorToInt(time / 60.0f);
        int sec = Mathf.FloorToInt(time % 60.0f);

        playTimeText.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    // 캐릭터 이미지 변경
    public void ChanageCharacterImage(Character.State state)
    {
        characterImage.sprite = characterComponent.ReturnImage(state);
    }

    // 제한 시간 바 업데이트
    public void UpdateTimerBar(float maxTime, float currentTime)
    {
        timerBar.maxValue = maxTime;
        timerBar.minValue = 0.0f;

        timerBar.value = currentTime;
    }

    // 컴퓨터 선택 이미지 변경
    public void ChangeComputerSelectionImage(GameManager.MukChiBa selection)
    {
        switch (selection) 
        {
            case GameManager.MukChiBa.Muk:
                {
                    computerSelectionImage.sprite = mukImage;
                    break;
                }
            case GameManager.MukChiBa.Chi:
                {
                    computerSelectionImage.sprite = chiImage;
                    break;
                }
            case GameManager.MukChiBa.Ba:
                {
                    computerSelectionImage.sprite = baImage;
                    break;
                }
        }
    }

    // 체력 이미지 변경
    public void ChangeHpImage(int playerHp, int computerHp)
    {
        // 플레이어 체력 이미지 변경
        for(int i = 0; i < playerHpImage.Length; i++)
        {
            
        }

        // 컴퓨터 체력 이미지 변경
        for(int i = 0; i < computerHpImage.Length; i++)
        {

        }
    }

    // 공격 버튼 활성 상태 변경
    public void ActiveAttackDefenceButton(bool active)
    {
        attackButton.interactable = active;
        defenceButton.interactable = active;
    }

    // 묵찌빠 버튼 활성 상태 변경
    public void ActiveMukChiBaButton(bool active)
    {
        mukButton.interactable = active;
        chiButton.interactable = active;
        baButton.interactable = active;
    }

    // 선택 이미지 초기화
    public void InitializeSelectionImage()
    {
        playerSelectionImage.sprite = null;
        computerSelectionImage.sprite = null;
    }
}