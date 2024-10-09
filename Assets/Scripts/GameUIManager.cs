using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 게임 UI와 관련된 부분을 처리하는 클래스
public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    // 체력 이미지
    [SerializeField] Image[] playerHpImages = new Image[3]; // 플레이어 체력
    [SerializeField] Image[] computerHpImages = new Image[3]; // 컴퓨터 체력

    // 전체 플레이 시간 텍스트
    [SerializeField] TextMeshProUGUI playTimeText;

    // 일시 정지 버튼
    [SerializeField] Button pauseButton;

    // 결과 텍스트
    [SerializeField] TextMeshProUGUI resultText;

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

    // 체력 이미지
    [SerializeField] Sprite[] hpSprites = new Sprite[6];

    // 묵찌빠 이미지
    [SerializeField] Sprite mukSprite;
    [SerializeField] Sprite chiSprite;
    [SerializeField] Sprite baSprite;

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

        ChanageCharacterImage(Character.State.Initial);

        ActiveSpeechBubble(true);
        SetResultText(false);
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
        GameManager.instance.ChangeAtkDefSelection(GameManager.AtkDef.Attack);
    }

    void ClickDefenceButton()
    {
        GameManager.instance.ChangeAtkDefSelection(GameManager.AtkDef.Defence);
    }

    void ClickMukButton()
    {
        playerSelectionImage.sprite = mukSprite;

        GameManager.instance.ChangePlayerSelection(GameManager.MukChiBa.Muk);
    }

    void ClickChiButton()
    {
        playerSelectionImage.sprite = chiSprite;

        GameManager.instance.ChangePlayerSelection(GameManager.MukChiBa.Chi);
    }

    void ClickBaButton()
    {
        playerSelectionImage.sprite = baSprite;

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
                    computerSelectionImage.sprite = mukSprite;
                    break;
                }
            case GameManager.MukChiBa.Chi:
                {
                    computerSelectionImage.sprite = chiSprite;
                    break;
                }
            case GameManager.MukChiBa.Ba:
                {
                    computerSelectionImage.sprite = baSprite;
                    break;
                }
        }
    }

    // 체력 이미지 변경
    public void ChangeHpImage(int playerHp, int computerHp)
    {
        // 다 차있지 않은 체력 이미지 인덱스
        // - 체력이 5인 경우 : 3 - 2 - 1 = 0으로 0번째 체력 이미지는 풀 하트가 아님
        int playerNotFullHpIndex = (GameManager.instance.MaxHp / 2) - (playerHp / 2) - 1;
        int computerNotFullHpIndex = (GameManager.instance.MaxHp / 2) - (computerHp / 2) - 1;

        // 플레이어 체력 이미지 변경
        for (int i = playerHpImages.Length - 1; i >= 0; i--)
        {
            if (i > playerNotFullHpIndex)
                playerHpImages[i].sprite = hpSprites[i * 2];
            else if (i == playerNotFullHpIndex && playerHp % 2 != 0) 
                playerHpImages[i].sprite = hpSprites[i * 2 + 1];
            else
                playerHpImages[i].sprite = null;
        }

        // 컴퓨터 체력 이미지 변경
        for (int i = computerHpImages.Length - 1; i >= 0; i--)
        {
            if (i > computerNotFullHpIndex)
                computerHpImages[i].sprite = hpSprites[i * 2];
            else if (i == computerNotFullHpIndex && computerHp % 2 != 0)
                computerHpImages[i].sprite = hpSprites[i * 2 + 1];
            else
                computerHpImages[i].sprite = null;
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

    // 말풍선 이미지 활성 상태 변경
    public void ActiveSpeechBubble(bool active)
    {
        playerSpeechBubbleImage.gameObject.SetActive(active);
        computerSpeechBubbleImage.gameObject.SetActive(active);
    }

    // 결과 텍스트 변경
    public void SetResultText(bool active, string text = "")
    {
        resultText.gameObject.SetActive(active);

        resultText.text = text;
    }
}