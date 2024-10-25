using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecordElement : MonoBehaviour
{
    GamePlayData playData;

    int elementIndex;

    [SerializeField] TextMeshProUGUI elementIndexText;

    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] TextMeshProUGUI resultText;

    [SerializeField] TextMeshProUGUI playTimeText;

    [SerializeField] TextMeshProUGUI winningPercentageText;
    [SerializeField] TextMeshProUGUI winningPercentageDetailText;

    [SerializeField] TextMeshProUGUI attackPercentageText;
    [SerializeField] TextMeshProUGUI attackPercentageDetailText;

    // 기록 요소 세팅
    public void SetRecordElement(GamePlayData data, int index)
    {
        if (data != null)
        {
            gameObject.SetActive(true);

            playData = data;
            elementIndex = index;

            SetText(); // 텍스트 세팅
        }
        else
        {
            gameObject.SetActive(false);

            playData = null;
        }
    }

    // 텍스트 세팅
    void SetText()
    {
        elementIndexText.text = string.Format("[{0:D2}]", elementIndex);

        dateText.text = playData.playDate;
        resultText.text = (playData.playResult == (int)GamePlayData.Result.Win) ? "승리" : "패배";

        playTimeText.text = FloatTimeToString(playData.playTime);

        // 소수점 4번째 자리에서 반올림 후 '소수점 3자리 %' 형태로 출력
        float winningPercentage = Mathf.Round(playData.winningPercentage * 10000) / 10000;
        winningPercentageText.text = string.Format("{0:F3} %", winningPercentage);

        winningPercentageDetailText.text = string.Format("({0} / {1})", playData.winCount, playData.playTotalCount);

        float attackPercentage = Mathf.Round(playData.attackPercentage * 10000) / 10000;
        attackPercentageText.text = string.Format("{0:F3} %", attackPercentage);

        attackPercentageDetailText.text = string.Format("({0} / {1})", playData.atkPossibleCount, playData.atkDefCount);
    }

    // 실수형 시간을 문자열로 변경
    string FloatTimeToString(float time)
    {
        int min = Mathf.FloorToInt(time / 60.0f);
        int sec = Mathf.FloorToInt(time % 60.0f);
        int msec = Mathf.FloorToInt(((time % 60.0f) - sec) * 100);

        // 60분을 넘길 경우 59:59.99로 고정 표기
        return (min < 60) ? min.ToString("00") + ":" + sec.ToString("00") + "." + msec.ToString("00") : "59:59.99";
    }
}
