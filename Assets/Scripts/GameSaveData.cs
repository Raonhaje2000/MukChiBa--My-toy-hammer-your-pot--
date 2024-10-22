using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    const int MAX_DATA = 5; // 저장되는 데이터 최대치 (데이터가 많을 수록 처리가 오래 걸리는 것 방지용)

    public int saveCount;                        // 저장된 횟수

    // 저장된 게임 데이터들
    public List<GamePlayData> timeLongSort;      // 가장 오래 버틴 시간 정렬
    public List<GamePlayData> timeShortSort;     // 가장 적게 버틴 시간 정렬
    public List<GamePlayData> winPercentSort;    // 승률 높은 순으로 정렬
    public List<GamePlayData> attackPercentSort; // 공격 확률 높은 순으로 정렬

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

    // 플레이 데이터 저장
    public void Save(GamePlayData data)
    {
        data.dataIndex = saveCount;
        saveCount++;

        // 정렬된 데이터 저장
        SaveSortedData(data, GamePlayData.VariableName.Time, timeLongSort, false);        // 가장 오래 버틴 시간 순으로 정렬
        SaveSortedData(data, GamePlayData.VariableName.Time, timeShortSort, true);        // 가장 적게 버틴 시간 순으로 정렬
        SaveSortedData(data, GamePlayData.VariableName.WinPer, winPercentSort, false);    // 승률 높은 순으로 정렬
        SaveSortedData(data, GamePlayData.VariableName.atkPer, attackPercentSort, false); // 공격 확률 높은 순으로 정렬
    }

    // 정렬된 데이터 저장 (기본 오른차순 정렬)
    void SaveSortedData(GamePlayData data, GamePlayData.VariableName variable, List<GamePlayData> list, bool ascendingOrder = true)
    {
        if (list.Count != 0) // 기존에 저장된 데이터가 있는 경우
        {
            int index = (ascendingOrder) ? SearchAscendingOrderIndex(data, variable, list) : SearchDescendingOrderIndex(data, variable, list);
            list.Insert(index, data);

            // 저장 개수를 넘긴 경우 제일 마지막 인덱스 제거
            if (list.Count > MAX_DATA) list.RemoveAt(list.Count - 1);
        }
        else // 기존에 저장된 데이터가 없는 경우
        {
            list.Add(data);
        }
    }

    // 오름차순에서 인덱스 찾기
    int SearchAscendingOrderIndex(GamePlayData data, GamePlayData.VariableName variable, List<GamePlayData> list)
    {
        float searchValue = data.ReturnVariableValue(variable); // 찾으려는 값

        int leftIndex = 0;               // 가장 왼쪽 값의 인덱스
        int rightIndex = list.Count - 1; // 가장 오른쪽 값의 인덱스

        if (searchValue < list[leftIndex].ReturnVariableValue(variable)) // 최솟값보다 더 작은 경우
        {
            return 0;
        }
        else if (searchValue > list[rightIndex].ReturnVariableValue(variable)) // 최댓값보다 더 큰 경우
        {
            return list.Count;
        }
        else // 사이값인 경우 (이진 탐색 응용)
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

    // 내림차순에서 인덱스 찾기
    int SearchDescendingOrderIndex(GamePlayData data, GamePlayData.VariableName variable, List<GamePlayData> list)
    {
        float searchValue = data.ReturnVariableValue(variable); // 찾으려는 값

        int leftIndex = 0;               // 가장 왼쪽 값의 인덱스
        int rightIndex = list.Count - 1; // 가장 오른쪽 값의 인덱스

        if (searchValue > list[leftIndex].ReturnVariableValue(variable)) // 최댓값보다 더 큰 경우
        {
            return 0;
        }
        else if (searchValue < list[rightIndex].ReturnVariableValue(variable)) // 최솟값보다 더 작은 경우
        {
            return list.Count;
        }
        else // 사이값인 경우 (이진 탐색 응용)
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
