using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        AddButtonListener(); // ��ư ������ �߰�
    }

    // ��ư ������ �߰�
    void AddButtonListener()
    {
        soundButton.onClick.AddListener(ClickSoundButtonButton);

        startButton.onClick.AddListener(ClickStartButton);
        recordButton.onClick.AddListener(ClickRecordButton);
        howToPlayButton.onClick.AddListener(ClickHowToPlayButton);
        quitButton.onClick.AddListener(ClickQuitButton);
    }

    void Update()
    {
        // �׽�Ʈ�� (�Ŀ� �����̵�� �������� ����)
        int index = (SoundManager.instance.volume + 49) / 50;

        soundButtonIcon.sprite = soundIconImages[index];
    }

    void ClickSoundButtonButton()
    {
        // Ŭ���� �����̵�� ������
    }

    // ���� ���� ��ư Ŭ��
    void ClickStartButton()
    {

    }

    // ���� ��� ��ư Ŭ��
    void ClickRecordButton()
    {

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
}
