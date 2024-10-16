using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GamePlayData
{
    public int dataIndex; // 데이터 인덱스

    public DateTime playDate; // 게임을 한 날짜

    public float playTime; // 게임 진행 시간
    public int playTotalCount; // 게임 전체 판수 (가위바위보 + 묵찌빠 횟수)

    public int atkDefCount; // 공격 방어 횟수
    public int atkPossibleCount; // 공격 횟수 (공격 실패 포함)

    public int winCount; // 이긴 횟수 (가위바위보 + 묵찌빠에서 이긴 횟수)

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

    // 가위바위보+묵찌빠 할 때 이기면 업데이트
    public void UpdateWinCount(int winCount)
    {
        this.winCount += winCount;
    }

    // 공격 방어가 끝나면 업데이트
    public void UpdateAtkDefCount(int atkDefCount, int atkPossibleCount, int playTotalCount)
    {
        this.atkDefCount += atkDefCount;
        this.atkPossibleCount += atkPossibleCount;

        this.playTotalCount += playTotalCount;
    }

    // 게임이 종료되면 업데이트
    public void UpdatePlayTime(float playTime)
    {
        this.playTime = playTime;
    }
}
