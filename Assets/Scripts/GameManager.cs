using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 플레이와 관련된 부분들을 처리하는 클래스
public class GameManager : MonoBehaviour
{
    const int MAX_HP = 6; // 최대 체력

    const float INIT_TIME = 3.0f;           // 게임 첫 판 제한 시간
    const float DECREASE_TIME = 0.2f;      // 판 당 감소하는 시간
    const float DECREASE_LIMIT_TIME = 0.4f; // 판 당 감소하는 시간의 한계치
    const float ATK_DEF_TIME = 0.4f;        // 공격 또는 방어 제한 시간

    public enum MukChiBa { None = -1, Muk = 0, Chi = 1, Ba = 2 } // 묵찌빠
    public enum Result { Win = 1, Draw = 0, Lose = -1 }

    public static GameManager instance;

    // 현재 체력
    int playerCurrentHp;
    int computerCurrentHp;

    float playTime;    // 전체 플레이 시간

    [SerializeField] int count;         // 한 판에서 묵찌빠를 낸 횟수 (컴퓨터가 낸 횟수라고 생각하면 편함)

    [SerializeField] float maxTime;         // 최대 제한 시간
    float remainingTime;   // 남은 제한 시간

    bool isAttacker;   // 플레이어의 공격권 여부

    // 묵찌빠 중 선택한 것
    [SerializeField] MukChiBa playerSelection;
    [SerializeField] MukChiBa computerSelection;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Initialize(); // 초기화

        PlayGame(); // 게임 시작
    }

    void Update()
    {

    }

    // 초기화
    void Initialize()
    {
        playerCurrentHp = MAX_HP;
        computerCurrentHp = MAX_HP;

        count = 0;

        maxTime = INIT_TIME - count * DECREASE_TIME;
        remainingTime = maxTime;

        isAttacker = false;

        playerSelection = MukChiBa.None;
        computerSelection = MukChiBa.None;
    }

    // 게임 시작
    public void PlayGame()
    {
        playTime = 0.0f;

        StartCoroutine(UpdatePlayTime()); // 전체 플레이 시간 업데이트
        StartCoroutine(DoRockPaperScissors()); // 가위바위보로 첫 판 시작
    }

    // 전체 플레이 시간 변경
    IEnumerator UpdatePlayTime()
    {
        while (true)
        {
            playTime += Time.deltaTime;

            GameUIManager.instance.UpdatePlayTimeText(playTime);

            yield return null;
        }
    }

    // 플레이어의 선택 변경 (클릭한 버튼에 따라 변경)
    public void ChangePlayerSelection(MukChiBa selection)
    {
        playerSelection = selection;
    }

    // 컴퓨터의 선택 변경 (랜덤으로 하나 뽑아 변경)
    void ChangeComputerSelection()
    {
        int index = Random.Range(0, 3); // 묵찌빠 중 선택 // int는 최댓값 포함이 안되므로 3까지

        computerSelection = (MukChiBa) index; // 정수를 enum 으로 변경

        GameUIManager.instance.ChangeComputerSelectionImage(computerSelection);
    }

    // 가위바위보로 첫 판 시작
    // 가위바위보 테스트 중 ###############
    IEnumerator DoRockPaperScissors()
    {
        while (true)
        {
            // 대기 시간
            yield return new WaitForSecondsRealtime(2.0f);

            // 초기화
            maxTime = INIT_TIME - count * DECREASE_TIME;
            if (maxTime < DECREASE_LIMIT_TIME) maxTime = DECREASE_LIMIT_TIME;

            remainingTime = maxTime;

            isAttacker = false;

            playerSelection = MukChiBa.None;
            computerSelection = MukChiBa.None;

            GameUIManager.instance.InitializeSelectionImage();

            GameUIManager.instance.ChanageCharacterImage(Character.State.Initial);

            // 버튼 활성화 설정
            GameUIManager.instance.ActiveAttackDefenceButton(false);
            GameUIManager.instance.ActiveMukChiBaButton(true);

            // 남은 제한 시간 업데이트
            while (remainingTime > 0.0f)
            {
                remainingTime -= Time.deltaTime;

                GameUIManager.instance.UpdateTimerBar(maxTime, remainingTime);

                yield return null;
            }

            // 버튼 활성화 설정
            GameUIManager.instance.ActiveMukChiBaButton(false);

            count++;

            // 컴퓨터 설정 화면에 보이기
            ChangeComputerSelection();

            // 결과 처리
            if (playerSelection == MukChiBa.None) // 플레이어가 선택을 안 했을 경우
            {
                GameUIManager.instance.ChanageCharacterImage(Character.State.NotSelect);

                // 플레이어의 체력 감소
                playerCurrentHp--;
                GameUIManager.instance.ChangeHpImage(playerCurrentHp, computerCurrentHp);

                // 플레이어 체력이 0이 되었을 경우 플레이어 패배
                if (playerCurrentHp == 0) GameUIManager.instance.ChanageCharacterImage(Character.State.Lose);
            }
            else // 플레이어가 선택을 한 경우
            {
                Result result = ReturnResult();

                // 테스트용 ############
                if (result == Result.Win) GameUIManager.instance.ChanageCharacterImage(Character.State.Win);
                if (result == Result.Lose) GameUIManager.instance.ChanageCharacterImage(Character.State.Lose);
            }
        }
    }

    Result ReturnResult()
    {
        // 승패 확인은 선택값(enum의 정수값)의 차를 이용
        int subtraction = computerSelection - playerSelection;

        // Ex 1) 컴퓨터가 묵, 플레이어가 찌를 냄 (플레이어 패배) --> 묵의 enum = 0, 찌의 enum = 1 --> 0 - 1 = -1
        // Ex 2) 컴퓨터가 빠, 플레이어가 묵을 냄 (플레이어 패배) --> 빠의 enum = 2, 묵의 enum = 0 --> 2 - 0 = 2
        if (subtraction == -1 || subtraction == 2) return Result.Lose;
        else if (subtraction == 0) return Result.Draw;
        else return Result.Win;
    }
}