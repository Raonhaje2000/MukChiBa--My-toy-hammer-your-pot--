using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// 플레이 기록을 관리하는 클래스
public class RecordManager : MonoBehaviour
{
    static RecordManager instance;

    [SerializeField] TextMeshProUGUI recordExplanationText;

    // 버튼들
    [SerializeField] Button timeShortButton;
    [SerializeField] Button timeLongButton;
    [SerializeField] Button winningPercentButton;
    [SerializeField] Button attackPercentageButton;

    [SerializeField] Button titleButton; // 타이틀로 버튼
    [SerializeField] Button resetButton; // 기록 초기화 버튼

    [SerializeField] GameObject noRecordMessageText;

    [SerializeField] Transform recordElementParent; // 기록 요소의 부모 오브젝트 (해당 오브젝트 하위에 생성되도록)
    [SerializeField] GameObject recordElementPrefab; // 기록 요소 오브젝트 프리팹

    List<RecordElement> recordElementList;

    [SerializeField] GameObject resetMessageBox; // 초기화 버튼 클릭 시 뜨는 메세지 박스

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
        recordExplanationText.text = string.Format("기록은 항목별로 최대 {0}개만 표기됩니다.", JsonManager.Instance.SaveData.MaxData);

        recordElementList = new List<RecordElement>();

        // 기록 요소 오브젝트 생성
        for (int i = 0; i < JsonManager.Instance.SaveData.MaxData; i++)
        {
            GameObject newElement = Instantiate(recordElementPrefab, recordElementParent);
            recordElementList.Add(newElement.GetComponent<RecordElement>());
        }

        AddListeners(); // 리스너 추가

        resetMessageBox.SetActive(false);
        noRecordMessageText.SetActive(false);

        ShowInitialScreen(); // 초기 화면 보여주기
    }

    // 리스너 추가
    void AddListeners()
    {
        timeShortButton.onClick.AddListener(ClickTimeShortButton);
        timeLongButton.onClick.AddListener(ClickTimeLongButton);
        winningPercentButton.onClick.AddListener(ClickWinningPercentButton);
        attackPercentageButton.onClick.AddListener(ClickAttackPercentageButton);

        titleButton.onClick.AddListener(ClickTitleButton);
        resetButton.onClick.AddListener(ClickResetButton);
    }

    // 초기 화면 보여주기
    public void ShowInitialScreen()
    {
        // 초기 화면은 시간 오름차순 버튼을 눌렀을 때로 설정
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

    // 타이틀로 버튼 클릭
    void ClickTitleButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        SceneManager.LoadScene("TitleScene");
    }

    // 기록 초기화 버튼 클릭
    void ClickResetButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        resetMessageBox.SetActive(true);
    }

    // 탭 버튼들을 선택되지 않았을 때의 색으로 변경
    void ChangeTebButtonsNotSelectedColor()
    {
        timeShortButton.image.color = notSelectedColor;
        timeLongButton.image.color = notSelectedColor;
        winningPercentButton.image.color = notSelectedColor;
        attackPercentageButton.image.color = notSelectedColor;
    }

    // 기록 요소들 세팅
    void SetRecordElements(List<GamePlayData> gameData)
    {
        // 데이터가 없는 경우 기록 없음 메세지 활성화
        if (gameData.Count == 0)
            noRecordMessageText.SetActive(true);
        else
            noRecordMessageText.SetActive(false);

        // 기록 요소 설정
        for (int i = 0; i < recordElementList.Count; i++)
        {
            // 인덱스는 0부터 시작이므로 +1을 해줌
            if (i < gameData.Count)
                recordElementList[i].SetRecordElement(gameData[i], i + 1);
            else
                recordElementList[i].SetRecordElement(null, i + 1);
        }
    }
}
