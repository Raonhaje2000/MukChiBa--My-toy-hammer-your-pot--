using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 플레이와 관련된 부분들을 처리하는 클래스
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    float playTime;    // 전체 플레이 시간

    float initTime;    // 게임 첫 판 제한 시간
    float currentTime; // 현재 제한 시간

    bool isAttacker;   // 플레이어의 공격권 여부

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
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
        
    }
}
