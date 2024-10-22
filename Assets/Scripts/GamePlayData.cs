using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GamePlayData
{
    public enum VariableName { Time = 0, WinPer = 1, atkPer = 2 }

    public int dataIndex; // ������ �ε���

    public string playDate; // ������ �� ��¥

    public float playTime; // ���� ���� �ð�

    public int atkDefCount; // ���� ��� Ƚ��
    public int atkPossibleCount; // ���� Ƚ�� (���� ���� ����, ���� ������ ����� ��� ��� ü���� �� ��� 6ȸ�� ���� ����� ����)

    public int playTotalCount; // ���� ��ü �Ǽ� (���������� + ����� Ƚ��)
    public int winCount; // �̱� Ƚ�� (���������� + ��������� �̱� Ƚ��)

    public float winningPercentage; // �·� % (�̱� Ƚ�� / ���� ��ü �Ǽ�)
    public float attackPercentage; // ���� Ȯ�� % (���� Ƚ�� / ���� ��� Ƚ��)

    public GamePlayData()
    {
        dataIndex = -1;

        playDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");

        playTime = 0.0f;

        atkDefCount = 0;
        playTotalCount = 0;

        atkPossibleCount = 0;
        winCount = 0;

        winningPercentage = 0.0f;
        attackPercentage = 0.0f;
    }

    // ����������+����� �� �� �̱�� ������Ʈ
    public void UpdateWin()
    {
        this.winCount++;
    }

    // ���� �� ������ ������Ʈ
    public void UpdateAtkDef(bool atkPossible, int playTotalCount)
    {
        atkDefCount++;
        if(atkPossible) atkPossibleCount++;

        this.playTotalCount += playTotalCount;
    }

    // ������ ����Ǹ� ������Ʈ
    public void UpdateEnd(float playTime)
    {
        this.playTime = playTime;

        // �÷��̾ ���� ���ൿ�� �ƹ��͵� �����ʾ� ������ ����� ��� 0���� ó��
        winningPercentage = (playTotalCount == 0) ? 0.0f : (float)winCount / (float)playTotalCount * 100.0f;
        attackPercentage = (atkDefCount == 0) ? 0.0f : (float)atkPossibleCount / (float)atkDefCount * 100.0f;
    }

    // �� ��ȯ (�ش� �Լ����� ���� Ž�� �Լ� �ϳ��� �����ϱ� ����)
    public float ReturnVariableValue(VariableName variable)
    {
        switch(variable)
        {
            case VariableName.Time: 
                return playTime;
            case VariableName.WinPer: 
                return winningPercentage;
            case VariableName.atkPer:
                return attackPercentage;
        }

        return -1.0f;
    }
}
