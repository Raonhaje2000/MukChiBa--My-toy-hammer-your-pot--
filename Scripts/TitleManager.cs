using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// ���� Ÿ��Ʋ UI�� ���õ� �κ��� ó���ϴ� Ŭ����
public class TitleManager : MonoBehaviour
{
    static TitleManager instance;

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
        AddListeners(); // ������ �߰�

        soundBarObject.SetActive(false);

        SetSoundUI(SoundManager.Instance.BgmVolume);

        SoundManager.Instance.PlayBgm(SoundManager.BGM.Title); // BGM ���
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
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        soundBarObject.SetActive(!soundBarObject.activeSelf);
    }

    // ���� ���� ��ư Ŭ��
    void ClickStartButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        SceneManager.LoadScene("GameScene");
    }

    // ���� ��� ��ư Ŭ��
    void ClickRecordButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        SceneManager.LoadScene("RecordScene");
    }

    // ���� ��� ��ư Ŭ��
    void ClickHowToPlayButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);
    }

    // ���� ���� ��ư Ŭ��
    void ClickQuitButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        Application.Quit();
    }

    // ���� �� �� ����
    void ChangeSoundBarValue(float currentValue)
    {
        int volume = Mathf.RoundToInt(currentValue);

        // ���� UI ����
        SetSoundUI(volume);

        SoundManager.Instance.ChangeBgmVolume(volume);
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
