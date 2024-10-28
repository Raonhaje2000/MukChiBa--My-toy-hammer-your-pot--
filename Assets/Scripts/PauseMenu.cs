using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // 일시 정지 메뉴 버튼들
    [SerializeField] Button titleButton;    // 타이틀로 버튼
    [SerializeField] Button restartButton;  // 다시하기 버튼
    [SerializeField] Button continueButton; // 이어하기 버튼

    void Start()
    {
        AddListeners(); // 리스너 추가
    }

    // 리스너 추가
    void AddListeners()
    {
        titleButton.onClick.AddListener(ClickTitleButton);
        restartButton.onClick.AddListener(ClickRestartButton);
        continueButton.onClick.AddListener(ClickContinueButton);
    }

    // 타이틀로 버튼 클릭
    void ClickTitleButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        GameUIManager.Instance.SetPauseState(false);

        SceneManager.LoadScene("TitleScene");
    }

    // 다시하기 버튼 클릭
    void ClickRestartButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        SoundManager.Instance.PlayBgm(SoundManager.BGM.Game, true); // BGM 처음부터 다시 시작

        GameUIManager.Instance.SetPauseState(false);

        SceneManager.LoadScene("GameScene");
    }

    // 이어하기 버튼 클릭
    void ClickContinueButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        GameUIManager.Instance.SetPauseState(false);
    }
}
