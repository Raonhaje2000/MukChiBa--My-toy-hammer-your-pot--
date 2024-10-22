using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    const int MAX_DATA = 5; // ����Ǵ� ������ �ִ�ġ (�����Ͱ� ���� ���� ó���� ���� �ɸ��� �� ������)

    public int saveCount;                        // ����� Ƚ��

    // ����� ���� �����͵�
    public List<GamePlayData> timeLongSort;      // ���� ���� ��ƾ �ð� ����
    public List<GamePlayData> timeShortSort;     // ���� ���� ��ƾ �ð� ����
    public List<GamePlayData> winPercentSort;    // �·� ���� ������ ����
    public List<GamePlayData> attackPercentSort; // ���� Ȯ�� ���� ������ ����

    public int MaxData
    {
        get { return MAX_DATA; }
    }

    public GameSaveData()
    {
        saveCount = 0;

        timeLongSort = new List<GamePlayData>();
        timeShortSort = new List<GamePlayData>();
        winPercentSort = new List<GamePlayData>();
        attackPercentSort = new List<GamePlayData>();
    }

    // �÷��� ������ ����
    public void Save(GamePlayData data)
    {
        data.dataIndex = saveCount;
        saveCount++;

        // ���ĵ� ������ ����
        SaveSortedData(data, GamePlayData.VariableName.Time, timeLongSort, false);        // ���� ���� ��ƾ �ð� ������ ����
        SaveSortedData(data, GamePlayData.VariableName.Time, timeShortSort, true);        // ���� ���� ��ƾ �ð� ������ ����
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

        int leftIndex = 0;               // ���� ���� ���� �ε���
        int rightIndex = list.Count - 1; // ���� ������ ���� �ε���

        if (searchValue < list[leftIndex].ReturnVariableValue(variable)) // �ּڰ����� �� ���� ���
        {
            return 0;
        }
        else if (searchValue > list[rightIndex].ReturnVariableValue(variable)) // �ִ񰪺��� �� ū ���
        {
            return list.Count;
        }
        else // ���̰��� ��� (���� Ž�� ����)
        {
            while (leftIndex < rightIndex)
            {
                int midIndex = (rightIndex + leftIndex) / 2;

                if (searchValue > list[midIndex].ReturnVariableValue(variable))
                    leftIndex = midIndex + 1;
                else
                    rightIndex = midIndex - 1;
            }

            return rightIndex;
        }
    }

    // ������������ �ε��� ã��
    int SearchDescendingOrderIndex(GamePlayData data, GamePlayData.VariableName variable, List<GamePlayData> list)
    {
        float searchValue = data.ReturnVariableValue(variable); // ã������ ��

        int leftIndex = 0;               // ���� ���� ���� �ε���
        int rightIndex = list.Count - 1; // ���� ������ ���� �ε���

        if (searchValue > list[leftIndex].ReturnVariableValue(variable)) // �ִ񰪺��� �� ū ���
        {
            return 0;
        }
        else if (searchValue < list[rightIndex].ReturnVariableValue(variable)) // �ּڰ����� �� ���� ���
        {
            return list.Count;
        }
        else // ���̰��� ��� (���� Ž�� ����)
        {
            while (leftIndex < rightIndex)
            {
                int midIndex = (rightIndex + leftIndex) / 2;

                if (searchValue < list[midIndex].ReturnVariableValue(variable))
                    leftIndex = midIndex + 1;
                else
                    rightIndex = midIndex - 1;
            }

            return leftIndex;
        }
    }

}
