using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetMessageBox : MonoBehaviour
{
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    void Start()
    {
        AddListeners(); // ������ �߰�
    }

    // ������ �߰�
    void AddListeners()
    {
        yesButton.onClick.AddListener(ClickYesButton);
        noButton.onClick.AddListener(ClickNoButton);
    }

    void ClickYesButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        // ��� �ʱ�ȭ
        JsonManager.Instance.ResetGameData();

        // �ʱ�ȭ�� �����ֱ�
        RecordManager.Instance.ShowInitialScreen();

        gameObject.SetActive(false);
    }

    void ClickNoButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        gameObject.SetActive(false);
    }
}
