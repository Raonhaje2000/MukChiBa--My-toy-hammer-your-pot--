using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    const int MAX_DATA = 10; // 저장되는 데이터 최대치 (데이터가 많을 수록 처리가 오래 걸리는 것 방지용)

    [SerializeField] int saveCount;                        // 저장된 횟수

    // 저장된 게임 데이터들
    [SerializeField] List<GamePlayData> timeShortSort;     // 가장 적게 버틴 시간 정렬
    [SerializeField] List<GamePlayData> timeLongSort;      // 가장 오래 버틴 시간 정렬
    [SerializeField] List<GamePlayData> winPercentSort;    // 승률 높은 순으로 정렬
    [SerializeField] List<GamePlayData> attackPercentSort; // 공격 확률 높은 순으로 정렬

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

    // 플레이 데이터 저장
    public void Save(GamePlayData data)
    {
        data.dataIndex = saveCount;
        saveCount++;

        // 정렬된 데이터 저장
        SaveSortedData(data, GamePlayData.VariableName.Time, timeShortSort, true);        // 가장 적게 버틴 시간 순으로 정렬
        SaveSortedData(data, GamePlayData.VariableName.Time, timeLongSort, false);        // 가장 오래 버틴 시간 순으로 정렬
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

        int startIndex = 0;               // 시작 인덱스
        int endIndex = list.Count - 1;    // 끝 인덱스

        if (searchValue < list[startIndex].ReturnVariableValue(variable)) // 최솟값보다 더 작은 경우
        {
            return 0;
        }
        else if (searchValue > list[endIndex].ReturnVariableValue(variable)) // 최댓값보다 더 큰 경우
        {
            return list.Count; // 끝 인덱스 뒤에 삽입 해야 하므로
        }
        else // 사이값인 경우
        {
            int index = -1;

            for(index = startIndex + 1; index < endIndex ; index++)
            {
                if (searchValue <= list[index].ReturnVariableValue(variable)) break;
            }

            return index;
        }
    }

    // 내림차순에서 인덱스 찾기
    int SearchDescendingOrderIndex(GamePlayData data, GamePlayData.VariableName variable, List<GamePlayData> list)
    {
        float searchValue = data.ReturnVariableValue(variable); // 찾으려는 값

        int startIndex = 0;            // 시작 인덱스
        int endIndex = list.Count - 1; // 끝 인덱스

        if (searchValue > list[startIndex].ReturnVariableValue(variable)) // 최댓값보다 더 큰 경우
        {
            return 0;
        }
        else if (searchValue < list[endIndex].ReturnVariableValue(variable)) // 최솟값보다 더 작은 경우
        {
            return list.Count;
        }
        else // 사이값인 경우
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
