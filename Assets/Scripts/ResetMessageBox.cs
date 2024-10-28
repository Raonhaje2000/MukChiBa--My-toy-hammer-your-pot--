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
        AddListeners(); // 리스너 추가
    }

    // 리스너 추가
    void AddListeners()
    {
        yesButton.onClick.AddListener(ClickYesButton);
        noButton.onClick.AddListener(ClickNoButton);
    }

    void ClickYesButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        // 기록 초기화
        JsonManager.Instance.ResetGameData();

        // 초기화면 보여주기
        RecordManager.Instance.ShowInitialScreen();

        gameObject.SetActive(false);
    }

    void ClickNoButton()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.Menu);

        gameObject.SetActive(false);
    }
}
