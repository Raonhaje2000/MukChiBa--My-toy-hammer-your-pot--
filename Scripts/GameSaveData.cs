using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    const int MAX_DATA = 10; // ����Ǵ� ������ �ִ�ġ (�����Ͱ� ���� ���� ó���� ���� �ɸ��� �� ������)

    [SerializeField] int saveCount;                        // ����� Ƚ��

    // ����� ���� �����͵�
    [SerializeField] List<GamePlayData> timeShortSort;     // ���� ���� ��ƾ �ð� ����
    [SerializeField] List<GamePlayData> timeLongSort;      // ���� ���� ��ƾ �ð� ����
    [SerializeField] List<GamePlayData> winPercentSort;    // �·� ���� ������ ����
    [SerializeField] List<GamePlayData> attackPercentSort; // ���� Ȯ�� ���� ������ ����

    public int MaxData
    {
        get { return MAX_DATA; }
    }

    public int SaveCount
    {
        get { return saveCount; }
    }

    public List<GamePlayData> TimeShortSort
    {
        get { return timeShortSort; }
    }

    public List<GamePlayData> TimeLongSort
    { 
        get { return timeLongSort; }
    }

    public List<GamePlayData> WinPercentSort
    {
        get { return winPercentSort; }
    }

    public List<GamePlayData> AttackPercentSort
    {
        get { return attackPercentSort; }
    }

    public GameSaveData()
    {
        saveCount = 0;

        timeShortSort = new List<GamePlayData>();
        timeLongSort = new List<GamePlayData>();
        winPercentSort = new List<GamePlayData>();
        attackPercentSort = new List<GamePlayData>();
    }

    // �÷��� ������ ����
    public void Save(GamePlayData data)
    {
        data.dataIndex = saveCount;
        saveCount++;

        // ���ĵ� ������ ����
        SaveSortedData(data, GamePlayData.VariableName.Time, timeShortSort, true);        // ���� ���� ��ƾ �ð� ������ ����
        SaveSortedData(data, GamePlayData.VariableName.Time, timeLongSort, false);        // ���� ���� ��ƾ �ð� ������ ����
        SaveSortedData(data, GamePlayData.VariableName.WinPer, winPercentSort, false);    // �·� ���� ������ ����
        SaveSortedData(data, GamePlayData.VariableName.atkPer, attackPercentSort, false); // ���� Ȯ�� ���� ������ ����
    }

    // ���ĵ� ������ ���� (�⺻ �������� ����)
    void SaveSortedData(GamePlayData data, GamePlayData.VariableName variable, List<GamePlayData> list, bool ascendingOrder = true)
    {
        if (list.Count != 0) // ������ ����� �����Ͱ� �ִ� ���
        {
            int index = (ascendingOrder) ? SearchAscendingOrderIndex(data, variable, list) : SearchDescendingOrderIndex(data, variable, list);
            list.Insert(index, data);

            // ���� ������ �ѱ� ��� ���� ������ �ε��� ����
            if (list.Count > MAX_DATA) list.RemoveAt(list.Count - 1);
        }
        else // ������ ����� �����Ͱ� ���� ���
        {
            list.Add(data);
        }
    }

    // ������������ �ε��� ã��
    int SearchAscendingOrderIndex(GamePlayData data, GamePlayData.VariableName variable, List<GamePlayData> list)
    {
        float searchValue = data.ReturnVariableValue(variable); // ã������ ��

        int startIndex = 0;               // ���� �ε���
        int endIndex = list.Count - 1;    // �� �ε���

        if (searchValue < list[startIndex].ReturnVariableValue(variable)) // �ּڰ����� �� ���� ���
        {
            return 0;
        }
        else if (searchValue > list[endIndex].ReturnVariableValue(variable)) // �ִ񰪺��� �� ū ���
        {
            return list.Count; // �� �ε��� �ڿ� ���� �ؾ� �ϹǷ�
        }
        else // ���̰��� ���
        {
            int index = -1;

            for(index = startIndex + 1; index < endIndex ; index++)
            {
                if (searchValue <= list[index].ReturnVariableValue(variable)) break;
            }

            return index;
        }
    }

    // ������������ �ε��� ã��
    int SearchDescendingOrderIndex(GamePlayData data, GamePlayData.VariableName variable, List<GamePlayData> list)
    {
        float searchValue = data.ReturnVariableValue(variable); // ã������ ��

        int startIndex = 0;            // ���� �ε���
        int endIndex = list.Count - 1; // �� �ε���

        if (searchValue > list[startIndex].ReturnVariableValue(variable)) // �ִ񰪺��� �� ū ���
        {
            return 0;
        }
        else if (searchValue < list[endIndex].ReturnVariableValue(variable)) // �ּڰ����� �� ���� ���
        {
            return list.Count;
        }
        else // ���̰��� ���
        {
            int index = -1;

            for (index = startIndex + 1; index < endIndex; index++)
            {
                if (searchValue >= list[index].ReturnVariableValue(variable)) break;
            }

            return index;
        }
    }

}
