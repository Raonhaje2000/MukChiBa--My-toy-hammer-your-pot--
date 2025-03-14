using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// �÷��� ����� �����ϴ� Ŭ����
public class RecordManager : MonoBehaviour
{
    static RecordManager instance;

    [SerializeField] TextMeshProUGUI recordExplanationText;

    // ��ư��
    [SerializeField] Button timeShortButton;
    [SerializeField] Button timeLongButton;
    [SerializeField] Button winningPercentButton;
    [SerializeField] Button attackPercentageButton;

    [SerializeField] Button titleButton; // Ÿ��Ʋ�� ��ư
    [SerializeField] Button resetButton; // ��� �ʱ�ȭ ��ư

    [SerializeField] GameObject noRecordMessageText;

    [SerializeField] Transform recordElementParent; // ��� ����� �θ� ������Ʈ (�ش� ������Ʈ ������ �����ǵ���)
    [SerializeField] GameObject recordElementPrefab; // ��� ��� ������Ʈ ������

    List<RecordElement> recordElementList;

    [SerializeField] GameObject resetMessageBox; // �ʱ�ȭ ��ư Ŭ�� �� �ߴ� �޼��� �ڽ�

    [SerializeField] Color32 selectedColor;
    [SerializeField] Color32 notSelectedColor;

    public static RecordManager Instance
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
        recordExplanationText.text = string.Format("����� �׸񺰷� �ִ� {0}���� ǥ��˴ϴ�.", JsonManager.Instance.SaveData.MaxData);

        recordElementList = new List<RecordElement>();

        // ��� ��� ������Ʈ ����
        for (int i = 0; i < JsonManager.Instance.SaveData.MaxData; i++)
        {
            GameObject newElement = Instantiate(recordElementPrefab, recordElementParent);
            recordElementList.Add(newElement.GetComponent<RecordElement>());
        }

        AddListeners(); // ������ �߰�

        resetMessageBox.SetActive(false);
        noRecordMessageText.SetActive(false);

        ShowInitialScreen(); // �ʱ� ȭ�� �����ֱ�
    }

    // ������ �߰�
    void AddListeners()
    {
        timeShortButton.onClick.AddListener(ClickTimeShortButton);
        timeLongButton.onClick.AddListener(ClickTimeLongButton);
        winningPercentButton.onClick.AddListener(ClickWinningPercentButton);
        attackPercentageButton.onClick.AddListener(ClickAttackPercentageButton);

        titleButton.onClick.AddListener(ClickTitleButton);
        resetButton.onClick.AddListener(ClickResetButton);
    }

    // �ʱ� ȭ�� �����ֱ�
    public void ShowInitialScreen()
    {
        // �ʱ� ȭ���� �ð� �������� ��ư�� ������ ���� ����
        ChangeTebButtonsNotSelectedColor();
        timeShortButton.image.color = selectedColor;

        SetRecordElements(JsonManager.Instance.SaveData.TimeShortSort);
    }

    void ClickTimeShortButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        ChangeTebButtonsNotSelectedColor();
        timeShortButton.image.color = selectedColor;

        SetRecordElements(JsonManager.Instance.SaveData.TimeShortSort);
    }

    void ClickTimeLongButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        ChangeTebButtonsNotSelectedColor();
        timeLongButton.image.color = selectedColor;

        SetRecordElements(JsonManager.Instance.SaveData.TimeLongSort);
    }

    void ClickWinningPercentButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        ChangeTebButtonsNotSelectedColor();
        winningPercentButton.image.color = selectedColor;

        SetRecordElements(JsonManager.Instance.SaveData.WinPercentSort);
    }

    void ClickAttackPercentageButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        ChangeTebButtonsNotSelectedColor();
        attackPercentageButton.image.color = selectedColor;

        SetRecordElements(JsonManager.Instance.SaveData.AttackPercentSort);
    }

    // Ÿ��Ʋ�� ��ư Ŭ��
    void ClickTitleButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        SceneManager.LoadScene("TitleScene");
    }

    // ��� �ʱ�ȭ ��ư Ŭ��
    void ClickResetButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        resetMessageBox.SetActive(true);
    }

    // �� ��ư���� ���õ��� �ʾ��� ���� ������ ����
    void ChangeTebButtonsNotSelectedColor()
    {
        timeShortButton.image.color = notSelectedColor;
        timeLongButton.image.color = notSelectedColor;
        winningPercentButton.image.color = notSelectedColor;
        attackPercentageButton.image.color = notSelectedColor;
    }

    // ��� ��ҵ� ����
    void SetRecordElements(List<GamePlayData> gameData)
    {
        // �����Ͱ� ���� ��� ��� ���� �޼��� Ȱ��ȭ
        if (gameData.Count == 0)
            noRecordMessageText.SetActive(true);
        else
            noRecordMessageText.SetActive(false);

        // ��� ��� ����
        for (int i = 0; i < recordElementList.Count; i++)
        {
            // �ε����� 0���� �����̹Ƿ� +1�� ����
            if (i < gameData.Count)
                recordElementList[i].SetRecordElement(gameData[i], i + 1);
            else
                recordElementList[i].SetRecordElement(null, i + 1);
        }
    }
}
