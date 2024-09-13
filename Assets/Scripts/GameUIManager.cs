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
    [SerializeField] Slider timerBar;

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
        AddListeners(); // ������ �߰�

        Initialize(); // �ʱ�ȭ
    }

    // �ʱ�ȭ
    void Initialize()
    {
        InitializeSelectionImage();

        characterImage.sprite = characterComponent.ReturnImage(Character.State.Initial);
    }

    // ������ �߰�
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

    // ��ü �÷��� �ð� �ؽ�Ʈ ������Ʈ
    public void UpdatePlayTimeText(float time)
    {
        int min = Mathf.FloorToInt(time / 60.0f);
        int sec = Mathf.FloorToInt(time % 60.0f);

        playTimeText.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    // ĳ���� �̹��� ����
    public void ChanageCharacterImage(Character.State state)
    {
        characterImage.sprite = characterComponent.ReturnImage(state);
    }

    // ���� �ð� �� ������Ʈ
    public void UpdateTimerBar(float maxTime, float currentTime)
    {
        timerBar.maxValue = maxTime;
        timerBar.minValue = 0.0f;

        timerBar.value = currentTime;
    }

    // ��ǻ�� ���� �̹��� ����
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

    // ü�� �̹��� ����
    public void ChangeHpImage(int playerHp, int computerHp)
    {
        // �÷��̾� ü�� �̹��� ����
        for(int i = 0; i < playerHpImage.Length; i++)
        {
            
        }

        // ��ǻ�� ü�� �̹��� ����
        for(int i = 0; i < computerHpImage.Length; i++)
        {

        }
    }

    // ���� ��ư Ȱ�� ���� ����
    public void ActiveAttackDefenceButton(bool active)
    {
        attackButton.interactable = active;
        defenceButton.interactable = active;
    }

    // ����� ��ư Ȱ�� ���� ����
    public void ActiveMukChiBaButton(bool active)
    {
        mukButton.interactable = active;
        chiButton.interactable = active;
        baButton.interactable = active;
    }

    // ���� �̹��� �ʱ�ȭ
    public void InitializeSelectionImage()
    {
        playerSelectionImage.sprite = null;
        computerSelectionImage.sprite = null;
    }
}