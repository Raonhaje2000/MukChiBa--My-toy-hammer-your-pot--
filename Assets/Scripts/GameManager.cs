using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 플레이와 관련된 부분들을 처리하는 클래스
public class GameManager : MonoBehaviour
{
    public enum State { Title = 0, Game = 1 }

    public static GameManager instance;

    State gameState;

    float playTime;    // 전체 플레이 시간

    float initTime;          // 게임 첫 판 제한 시간
    float currentTime;       // 현재 제한 시간
    float attackDefenceTime; // 공격 또는 방어 제한 시간

    bool isAttacker;   // 플레이어의 공격권 여부

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            gameState = State.Title;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(GameUIManager.instance != null && gameState == State.Game)
        {
            playTime += Time.deltaTime;

            GameUIManager.instance.UpdatePlayTimeText(playTime);
        }
    }

    // 게임 상태 변경
    public void ChanageGameState(State currentState)
    {
        gameState = currentState;

        // 게임을 시작할 경우 전체 플레이 시간 초기화
        if(currentState == State.Game) playTime = 0.0f;
    }
}
