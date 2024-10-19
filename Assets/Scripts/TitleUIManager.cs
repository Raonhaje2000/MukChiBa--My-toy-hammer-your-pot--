using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// ���� Ÿ��Ʋ UI�� ���õ� �κ��� ó���ϴ� Ŭ����
public class TitleUIManager : MonoBehaviour
{
    public static TitleUIManager instance;

    // Ÿ��Ʋ ȭ�� ��ư��
    [SerializeField] Button soundButton;

    [SerializeField] Button startButton;
    [SerializeField] Button recordButton;
    [SerializeField] Button howToPlayButton;
    [SerializeField] Button quitButton;

    // ���� ��ư ������
    [SerializeField] Image soundButtonIcon;

    // ���� ������ �̹��� (0: �ּ� ~ 2: �ִ�)
    [SerializeField] Sprite[] soundIconImages = new Sprite[3];

    // ���� �� ����
    [SerializeField] GameObject soundBarObject;
    [SerializeField] Slider soundBar;
    [SerializeField] TextMeshProUGUI soundBarText;

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
        AddListeners(); // ������ �߰�

        soundBarObject.SetActive(false);

        SetSoundUI(SoundManager.instance.Volume);
    }

    // ������ �߰�
    void AddListeners()
    {
        soundButton.onClick.AddListener(ClickSoundButton);

        startButton.onClick.AddListener(ClickStartButton);
        recordButton.onClick.AddListener(ClickRecordButton);
        howToPlayButton.onClick.AddListener(ClickHowToPlayButton);
        quitButton.onClick.AddListener(ClickQuitButton);

        soundBar.onValueChanged.AddListener(ChangeSoundBarValue);
    }

    // ���� ��ư Ŭ��
    void ClickSoundButton()
    {
        soundBarObject.SetActive(!soundBarObject.activeSelf);
    }

    // ���� ���� ��ư Ŭ��
    void ClickStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    // ���� ��� ��ư Ŭ��
    void ClickRecordButton()
    {
        SceneManager.LoadScene("RecordScene");
    }

    // ���� ��� ��ư Ŭ��
    void ClickHowToPlayButton()
    {

    }

    // ���� ���� ��ư Ŭ��
    void ClickQuitButton()
    {
        Application.Quit();
    }

    // ���� �� �� ����
    void ChangeSoundBarValue(float currentValue)
    {
        int volume = Mathf.RoundToInt(currentValue);

        // ���� UI ����
        SetSoundUI(volume);

        SoundManager.instance.Volume = volume;
    }

    // ���� UI ����
    void SetSoundUI(int currentVolume)
    {
        // ��ư ������ ����
        int index = (currentVolume + 49) / 50;

        soundButtonIcon.sprite = soundIconImages[index];

        // �� �� ����
        soundBar.value = currentVolume;

        // �ؽ�Ʈ ����
        soundBarText.text = currentVolume.ToString();
    }
}
