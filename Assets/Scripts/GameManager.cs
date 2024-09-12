using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �÷��̿� ���õ� �κе��� ó���ϴ� Ŭ����
public class GameManager : MonoBehaviour
{
    public enum State { Title = 0, Game = 1 }

    public static GameManager instance;

    State gameState;

    float playTime;    // ��ü �÷��� �ð�

    float initTime;          // ���� ù �� ���� �ð�
    float currentTime;       // ���� ���� �ð�
    float attackDefenceTime; // ���� �Ǵ� ��� ���� �ð�

    bool isAttacker;   // �÷��̾��� ���ݱ� ����

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

    // ���� ���� ����
    public void ChanageGameState(State currentState)
    {
        gameState = currentState;

        // ������ ������ ��� ��ü �÷��� �ð� �ʱ�ȭ
        if(currentState == State.Game) playTime = 0.0f;
    }
}
