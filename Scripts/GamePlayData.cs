using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GamePlayData
{
    public enum Result { None = 0, Win = 1, Lose = 2 }
    public enum VariableName { Time = 0, WinPer = 1, atkPer = 2 }

    public int dataIndex; // 데이터 인덱스

    public string playDate; // 게임을 한 날짜

    public int playResult; // 게임 결과     // 버전 문제로 System.Text.Json 을 사용 못해 Json으로 변환 불가능 -> enum을 정수값으로 변경하여 저장하는 방식 사용

    public float playTime; // 게임 진행 시간

    public int atkDefCount; // 공격 방어 횟수
    public int atkPossibleCount; // 공격 횟수 (공격 실패 포함, 공격 성공만 취급할 경우 상대 체력을 다 깎는 6회로 같은 결과가 나옴)

    public int playTotalCount; // 게임 전체 판수 (가위바위보 + 묵찌빠 횟수)
    public int winCount; // 이긴 횟수 (가위바위보 + 묵찌빠에서 이긴 횟수)

    public float winningPercentage; // 승률 % (이긴 횟수 / 게임 전체 판수)
    public float attackPercentage; // 공격 확률 % (공격 횟수 / 공격 방어 횟수)

    public GamePlayData()
    {
        dataIndex = -1;

        playDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");

        playResult = (int)Result.None; // 강제 형변환

        playTime = 0.0f;

        atkDefCount = 0;
        playTotalCount = 0;

        atkPossibleCount = 0;
        winCount = 0;

        winningPercentage = 0.0f;
        attackPercentage = 0.0f;
    }

    // 가위바위보 할 때 플레이어가 비기면 업데이트
    public void UpdateDraw()
    {
        this.playTotalCount++;
    }

    // 가위바위보+묵찌빠 할 때 이기면 업데이트
    public void UpdateWin()
    {
        this.winCount++;
    }

    // 가위바위보+묵찌빠 할 때 플레이어가 아무것도 선택하지 않았으면 업데이트
    public void UpdateNone(int count = 1)
    {
        this.playTotalCount += count;
    }

    // 공격 방어가 끝나면 업데이트
    public void UpdateAtkDef(bool atkPossible, int count)
    {
        this.atkDefCount++;
        if(atkPossible) this.atkPossibleCount++;

        this.playTotalCount += count;
    }

    // 게임이 종료되면 업데이트
    public void UpdateEnd(Result result, float playTime)
    {
        this.playResult = (int)result;
        this.playTime = playTime;

        // 플레이어가 게임 진행동안 아무것도 내지않아 공격 방어 없이 게임이 종료된 경우 0으로 처리
        attackPercentage = (atkDefCount == 0) ? 0.0f : (float)atkPossibleCount / (float)atkDefCount * 100.0f;
        winningPercentage = (winCount == 0) ? 0.0f : (float)winCount / (float)playTotalCount * 100.0f;
    }

    // 값 반환 (해당 함수값을 통해 탐색 함수 하나만 구현하기 위함)
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
