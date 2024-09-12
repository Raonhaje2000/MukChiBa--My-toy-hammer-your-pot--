using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ���� UI�� ���õ� �κ��� ó���ϴ� Ŭ����
public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    // ü��
    [SerializeField] Image[] playerHpImage = new Image[3]; // �÷��̾� ü��
    [SerializeField] Image[] computerHpImage = new Image[3]; // ��ǻ�� ü��

    // ��ü �÷��� �ð� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI playTimeText;

    // �Ͻ� ���� ��ư
    [SerializeField] Button pauseButton;

    // ĳ���� ��ǳ�� �̹���
    [SerializeField] Image playerSpeechBubbleImage;
    [SerializeField] Image computerSpeechBubbleImage;

    // ĳ���� ���� �̹���
    [SerializeField] Image playerSelectionImage;
    [SerializeField] Image computerSelectionImage;

    // ĳ���� �̹���
    [SerializeField] Image characterImage;

    // ���� �ð�
    [SerializeField] Slider TimerBar;

    // ���� ��� ��ư
    [SerializeField] Button attackButton;
    [SerializeField] Button defenceButton;

    // ����� ��ư
    [SerializeField] Button mukButton;
    [SerializeField] Button chiButton;
    [SerializeField] Button baButton;

    // ����� �̹���
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

    // ��ü �÷��� �ð� �ؽ�Ʈ ������Ʈ
    public void UpdatePlayTimeText(float time)
    {
        int min = Mathf.FloorToInt(time / 60.0f);
        int sec = Mathf.FloorToInt(time % 60.0f);

        playTimeText.text = min.ToString("00") + ":" + sec.ToString("00");
    }
}
