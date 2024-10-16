using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    public int saveCount;                   // 저장된 횟수
    public List<GamePlayData> playDataList; // 저장된 게임 데이터

    // 게임 진행 시간 우선 순위 (오름차순 정렬)

    // 게임 이긴 횟수 우선 순위 (내림차순 정렬)

    // 해당 데이터 찾기
}
