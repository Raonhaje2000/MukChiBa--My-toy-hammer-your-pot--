using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // �Ͻ� ���� �޴� ��ư��
    [SerializeField] Button titleButton;    // Ÿ��Ʋ�� ��ư
    [SerializeField] Button restartButton;  // �ٽ��ϱ� ��ư
    [SerializeField] Button continueButton; // �̾��ϱ� ��ư

    void Start()
    {
        AddListeners(); // ������ �߰�
    }

    // ������ �߰�
    void AddListeners()
    {
        titleButton.onClick.AddListener(ClickTitleButton);
        restartButton.onClick.AddListener(ClickRestartButton);
        continueButton.onClick.AddListener(ClickContinueButton);
    }

    // Ÿ��Ʋ�� ��ư Ŭ��
    void ClickTitleButton()
    {
        GameUIManager.instance.SetPauseState(false);

        SceneManager.LoadScene("TitleScene");
    }

    // �ٽ��ϱ� ��ư Ŭ��
    void ClickRestartButton()
    {
        GameUIManager.instance.SetPauseState(false);

        SceneManager.LoadScene("GameScene");
    }

    // �̾��ϱ� ��ư Ŭ��
    void ClickContinueButton()
    {
        GameUIManager.instance.SetPauseState(false);
    }
}
