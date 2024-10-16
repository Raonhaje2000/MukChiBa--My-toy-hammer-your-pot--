using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GamePlayData
{
    public int dataIndex; // ������ �ε���

    public DateTime playDate; // ������ �� ��¥

    public float playTime; // ���� ���� �ð�
    public int playTotalCount; // ���� ��ü �Ǽ� (���������� + ����� Ƚ��)

    public int atkDefCount; // ���� ��� Ƚ��
    public int atkPossibleCount; // ���� Ƚ�� (���� ���� ����)

    public int winCount; // �̱� Ƚ�� (���������� + ��������� �̱� Ƚ��)

    public GamePlayData()
    {
        dataIndex = -1;

        playDate = DateTime.Now;

        playTime = 0.0f;
        playTotalCount = 0;
        atkDefCount = 0;

        atkPossibleCount = 0;
        winCount = 0;
    }

    // ����������+����� �� �� �̱�� ������Ʈ
    public void UpdateWinCount(int winCount)
    {
        this.winCount += winCount;
    }

    // ���� �� ������ ������Ʈ
    public void UpdateAtkDefCount(int atkDefCount, int atkPossibleCount, int playTotalCount)
    {
        this.atkDefCount += atkDefCount;
        this.atkPossibleCount += atkPossibleCount;

        this.playTotalCount += playTotalCount;
    }

    // ������ ����Ǹ� ������Ʈ
    public void UpdatePlayTime(float playTime)
    {
        this.playTime = playTime;
    }
}
