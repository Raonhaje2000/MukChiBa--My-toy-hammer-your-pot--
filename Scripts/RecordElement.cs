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

    // ��� ��� ����
    public void SetRecordElement(GamePlayData data, int index)
    {
        if (data != null)
        {
            gameObject.SetActive(true);

            playData = data;
            elementIndex = index;

            SetText(); // �ؽ�Ʈ ����
        }
        else
        {
            gameObject.SetActive(false);

            playData = null;
        }
    }

    // �ؽ�Ʈ ����
    void SetText()
    {
        elementIndexText.text = string.Format("[{0:D2}]", elementIndex);

        dateText.text = playData.playDate;
        resultText.text = (playData.playResult == (int)GamePlayData.Result.Win) ? "�¸�" : "�й�";

        playTimeText.text = FloatTimeToString(playData.playTime);

        // �Ҽ��� 4��° �ڸ����� �ݿø� �� '�Ҽ��� 3�ڸ� %' ���·� ���
        float winningPercentage = Mathf.Round(playData.winningPercentage * 10000) / 10000;
        winningPercentageText.text = string.Format("{0:F3} %", winningPercentage);

        winningPercentageDetailText.text = string.Format("({0} / {1})", playData.winCount, playData.playTotalCount);

        float attackPercentage = Mathf.Round(playData.attackPercentage * 10000) / 10000;
        attackPercentageText.text = string.Format("{0:F3} %", attackPercentage);

        attackPercentageDetailText.text = string.Format("({0} / {1})", playData.atkPossibleCount, playData.atkDefCount);
    }

    // �Ǽ��� �ð��� ���ڿ��� ����
    string FloatTimeToString(float time)
    {
        int min = Mathf.FloorToInt(time / 60.0f);
        int sec = Mathf.FloorToInt(time % 60.0f);
        int msec = Mathf.FloorToInt(((time % 60.0f) - sec) * 100);

        // 60���� �ѱ� ��� 59:59.99�� ���� ǥ��
        return (min < 60) ? min.ToString("00") + ":" + sec.ToString("00") + "." + msec.ToString("00") : "59:59.99";
    }
}
