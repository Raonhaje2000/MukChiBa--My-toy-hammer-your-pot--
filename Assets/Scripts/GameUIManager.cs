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
    [SerializeField] Slider TimerBar;

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
        AddListeners();
    }

    void AddListeners()
    {
        attackButton.onClick.AddListener(ClickAttackButton);
        defenceButton.onClick.AddListener(ClickDefenceButton);

        mukButton.onClick.AddListener(ClickMukButton);
        chiButton.onClick.AddListener(ClickChiButton);
        baButton.onClick.AddListener(ClickBaButton);
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
    }

    void ClickChiButton()
    {
        playerSelectionImage.sprite = chiImage;
    }

    void ClickBaButton()
    {
        playerSelectionImage.sprite = baImage;
    }

    // 전체 플레이 시간 텍스트 업데이트
    public void UpdatePlayTimeText(float time)
    {
        int min = Mathf.FloorToInt(time / 60.0f);
        int sec = Mathf.FloorToInt(time % 60.0f);

        playTimeText.text = min.ToString("00") + ":" + sec.ToString("00");
    }
}
