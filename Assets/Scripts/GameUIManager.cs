using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ���� UI�� ���õ� �κ��� ó���ϴ� Ŭ����
public class GameUIManager : MonoBehaviour
{
    static GameUIManager instance;

    // ü�� �̹���
    [SerializeField] Image[] playerHpImages = new Image[3]; // �÷��̾� ü��
    [SerializeField] Image[] computerHpImages = new Image[3]; // ��ǻ�� ü��

    // ��ü �÷��� �ð� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI playTimeText;

    // �Ͻ� ���� ��ư
    [SerializeField] Button pauseButton;

    // �Ͻ� ���� �޴�
    [SerializeField] GameObject pauseMenuObject;

    // ��� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI resultText;

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

    // ü�� �̹���
    [SerializeField] Sprite[] hpSprites = new Sprite[6];

    // ����� �̹���
    [SerializeField] Sprite mukSprite;
    [SerializeField] Sprite chiSprite;
    [SerializeField] Sprite baSprite;

    Character characterComponent;

    // ��ư Ȱ�� ����
    bool atkDefButtonState;
    bool mukChiBaButtonState;

    public static GameUIManager Instance
    {
        get { return instance; }
    }

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
        pauseMenuObject.SetActive(false);

        InitializeSelectionImage();

        ChanageCharacterImage(Character.State.Initial);

        ActiveSpeechBubble(true);
        SetResultText(false);
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

    // �Ͻ����� ��ư Ŭ��
    void ClickPauseButton()
    {
        SetPauseState(true);
    }

    // ���� ��ư Ŭ��
    void ClickAttackButton()
    {
        GameManager.Instance.ChangeAtkDefSelection(GameManager.AtkDef.Attack);
    }

    // ��� ��ư Ŭ��
    void ClickDefenceButton()
    {
        GameManager.Instance.ChangeAtkDefSelection(GameManager.AtkDef.Defence);
    }

    // �� ��ư Ŭ��
    void ClickMukButton()
    {
        playerSelectionImage.sprite = mukSprite;

        GameManager.Instance.ChangePlayerSelection(GameManager.MukChiBa.Muk);
    }

    // �� ��ư Ŭ��
    void ClickChiButton()
    {
        playerSelectionImage.sprite = chiSprite;

        GameManager.Instance.ChangePlayerSelection(GameManager.MukChiBa.Chi);
    }

    // �� ��ư Ŭ��
    void ClickBaButton()
    {
        playerSelectionImage.sprite = baSprite;

        GameManager.Instance.ChangePlayerSelection(GameManager.MukChiBa.Ba);
    }

    // ��ü �÷��� �ð� �ؽ�Ʈ ������Ʈ
    public void UpdatePlayTimeText(float time)
    {
        int min = Mathf.FloorToInt(time / 60.0f);
        int sec = Mathf.FloorToInt(time % 60.0f);

        // 60���� �ѱ� ��� 59:59�� ���� ǥ��
        playTimeText.text = (min < 60) ? min.ToString("00") + ":" + sec.ToString("00") : "59:59";
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

    // ü�� �̹��� ����
    public void ChangeHpImage(int playerHp, int computerHp)
    {
        // �� ������ ���� ü�� �̹��� �ε���
        // - ü���� 5�� ��� : 3 - 2 - 1 = 0���� 0��° ü�� �̹����� Ǯ ��Ʈ�� �ƴ�
        int playerNotFullHpIndex = (GameManager.Instance.MaxHp / 2) - (playerHp / 2) - 1;
        int computerNotFullHpIndex = (GameManager.Instance.MaxHp / 2) - (computerHp / 2) - 1;

        // �÷��̾� ü�� �̹��� ����
        for (int i = playerHpImages.Length - 1; i >= 0; i--)
        {
            if (i > playerNotFullHpIndex)
                playerHpImages[i].sprite = hpSprites[i * 2];
            else if (i == playerNotFullHpIndex && playerHp % 2 != 0) 
                playerHpImages[i].sprite = hpSprites[i * 2 + 1];
            else
                playerHpImages[i].sprite = null;
        }

        // ��ǻ�� ü�� �̹��� ����
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

    // ���� ��ư Ȱ�� ���� ����
    public void ActiveAttackDefenceButton(bool active, bool isPause = false)
    {
        attackButton.interactable = active;
        defenceButton.interactable = active;

        if(!isPause) atkDefButtonState = active;
    }

    // ����� ��ư Ȱ�� ���� ����
    public void ActiveMukChiBaButton(bool active, bool isPause = false)
    {
        mukButton.interactable = active;
        chiButton.interactable = active;
        baButton.interactable = active;

        if (!isPause) mukChiBaButtonState = active;
    }

    // ���� �̹��� �ʱ�ȭ
    public void InitializeSelectionImage()
    {
        playerSelectionImage.sprite = null;
        computerSelectionImage.sprite = null;
    }

    // ��ǳ�� �̹��� Ȱ�� ���� ����
    public void ActiveSpeechBubble(bool active)
    {
        playerSpeechBubbleImage.gameObject.SetActive(active);
        computerSpeechBubbleImage.gameObject.SetActive(active);
    }

    // ��� �ؽ�Ʈ ����
    public void SetResultText(bool active, string text = "")
    {
        resultText.gameObject.SetActive(active);

        resultText.text = text;
    }

    // �Ͻ� ���� ���� ����
    public void SetPauseState(bool active)
    {
        pauseMenuObject.SetActive(active);

        // �Ͻ� ���� ��ư ��ȣ�ۿ� ���� ����
        pauseButton.interactable = !active;

        if(active) // �Ͻ� ���� ������ ���
        {
            // ��ư ��Ȱ��ȭ
            ActiveAttackDefenceButton(false, true);
            ActiveMukChiBaButton(false, true);
        }
        else // �Ͻ� ���� ���� ������ ���
        {
            // ��ư�� ������ �� Ȱ�� ���·� ����
            ActiveAttackDefenceButton(atkDefButtonState, true);
            ActiveMukChiBaButton(mukChiBaButtonState, true);
        }

        Time.timeScale = (active) ? 0 : 1;
    }
}