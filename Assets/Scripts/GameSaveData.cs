using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    const int MAX_DATA = 10; // 저장되는 데이터 최대치 (데이터가 많을 수록 처리가 오래 걸리는 것 방지용)

    public int saveCount;                   // 저장된 횟수
    public List<GamePlayData> playDataList; // 저장된 게임 데이터

    public int MaxData
    {
        get { return MAX_DATA; }
    }

    public GameSaveData()
    {
        saveCount = 0;
        playDataList = new List<GamePlayData>();
    }

    public void Save(GamePlayData data)
    {
        saveCount++;
        playDataList.Add(data);
    }
}
