using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 플레이와 관련된 부분들을 처리하는 클래스
public class GameManager : MonoBehaviour
{
    const int MAX_HP = 2; // 최대 체력

    const float INIT_TIME = 2.0f;           // 게임 첫 판 제한 시간
    const float DECREASE_TIME = 0.2f;      // 판 당 감소하는 시간
    const float DECREASE_LIMIT_TIME = 0.4f; // 판 당 감소하는 시간의 한계치
    const float ATK_DEF_TIME = 0.4f;        // 공격 또는 방어 제한 시간

    WaitForSeconds SelectionLatencyTime = new WaitForSeconds(2.0f); // 묵찌빠 선택 이미지를 보여주기 위한 대기 시간
    WaitForSeconds AtkDefLatencyTime = new WaitForSeconds(3.0f); // 공격 방어 결과 이미지를 보여주기 위한 대기 시간

    public enum MukChiBa { None = -1, Muk = 0, Chi = 1, Ba = 2 } // 묵찌빠 선택
    public enum Result { Win = 1, Draw = 0, Lose = -1 } // 묵찌빠 결과
    public enum AtkDef { Attack = 1, None = 0, Defence = -1 } // 공격 방어

    static GameManager instance;

    // 현재 체력
    [SerializeField] int playerCurrentHp;
    [SerializeField] int computerCurrentHp;

    float playTime;    // 전체 플레이 시간

    [SerializeField] int count;         // 한 판에서 묵찌빠를 낸 횟수, 횟수에 따라 제한 시간 조절 (컴퓨터가 낸 횟수라고 생각하면 편함)

    float maxTime;         // 최대 제한 시간
    float remainingTime;   // 남은 제한 시간

    [SerializeField] bool isAttacker;   // 플레이어의 공격권 여부

    // 묵찌빠 중 선택한 것
    [SerializeField] MukChiBa playerSelection;
    [SerializeField] MukChiBa computerSelection;

    // 공격과 방어 중 선택한 것
    [SerializeField] AtkDef atkDefSelection;

    GamePlayData playData; // 저장할 데이터

    public static GameManager Instance
    {
        get { return instance; }
    }

    public int MaxHp
    {
        get { return MAX_HP; }
    }

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

        StartGame(); // 게임 시작
    }

    // 초기화
    void Initialize()
    {
        playData = new GamePlayData();

        playerCurrentHp = MAX_HP;
        computerCurrentHp = MAX_HP;

        GameUIManager.Instance.ChangeHpImage(playerCurrentHp, computerCurrentHp);

        count = 0;

        maxTime = INIT_TIME - count * DECREASE_TIME;
        remainingTime = maxTime;

        isAttacker = false;

        playerSelection = MukChiBa.None;
        computerSelection = MukChiBa.None;

        atkDefSelection = AtkDef.None;
    }

    // 게임 시작
    public void StartGame()
    {
        playTime = 0.0f;

        StartCoroutine(UpdatePlayTime()); // 전체 플레이 시간 업데이트
        StartCoroutine(PlayGame()); // 게임 진행
    }

    // 전체 플레이 시간 변경
    IEnumerator UpdatePlayTime()
    {
        while (true)
        {
            playTime += Time.deltaTime;

            GameUIManager.Instance.UpdatePlayTimeText(playTime);

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

        GameUIManager.Instance.ChangeComputerSelectionImage(computerSelection);
    }

    // 공격 방어 선택 변경
    public void ChangeAtkDefSelection(AtkDef selection)
    {
        atkDefSelection = selection;
    }

    // 게임 진행
    IEnumerator PlayGame()
    {
        // 플레이어와 컴퓨터의 체력이 모두 남아있을때만 게임 진행
        while (true)
        {
            // 초기화
            isAttacker = false;
            count = 0;

            // 가위바위보 진행 (가위바위보로 시작)
            yield return DoRockPaperScissors();

            if (playerCurrentHp == 0) break;

            // 묵찌빠 진행
            yield return DoMukChiBa();

            if (playerCurrentHp == 0) break;

            // 공격 방어 진행
            yield return DoAttackDefence();

            if (playerCurrentHp == 0 || computerCurrentHp == 0) break;
        }

        // 게임 종료
        EndGame();
    }

    // 가위바위보 진행
    IEnumerator DoRockPaperScissors()
    {
        // 승패가 결정나기 전까지 계속 가위바위보 진행
        while (true)
        {
            // 게임 설정 초기화
            InitializeGameOptions();

            // 남은 제한 시간 업데이트
            yield return UpdateRemainingTime();

            // 현재 진행한 판수 증가
            count++;

            // 버튼 활성화 설정
            GameUIManager.Instance.ActiveMukChiBaButton(false);

            // 컴퓨터 선택 화면에 보이기
            ChangeComputerSelection();

            // 결과 처리
            if (playerSelection == MukChiBa.None) // 플레이어가 선택을 안 했을 경우
            {
                GameUIManager.Instance.ChanageCharacterImage(Character.State.NotSelect);

                // 플레이어의 체력 감소
                playerCurrentHp--;
                GameUIManager.Instance.ChangeHpImage(playerCurrentHp, computerCurrentHp);

                // 선택 이미지를 보여주는 동안 대기
                yield return SelectionLatencyTime;

                // 플레이어 체력이 0이 되었을 경우 가위바위보 종료
                if (playerCurrentHp == 0) break;
            }
            else // 플레이어가 선택을 한 경우
            {
                Result result = ReturnResult();

                // 선택 이미지를 보여주는 동안 대기
                yield return SelectionLatencyTime;

                if (result != Result.Draw)
                {
                    if (result == Result.Win) // 플레이어가 가위바위보에서 이긴 경우
                    {
                        // 공격권을 가져감
                        isAttacker = true;

                        // 플레이 데이터 업데이트
                        playData.UpdateWin();
                    }
                    else
                    {
                        isAttacker = false;
                    }

                    // 묵찌빠 진행
                    break;
                }
            }

            // 다음 판으로 넘어간게 아니므로 판수 롤백
            count--;
        }
    }

    // 묵찌빠 진행
    IEnumerator DoMukChiBa()
    {
        while (true)
        {
            // 게임 설정 초기화
            InitializeGameOptions();

            // 남은 제한 시간 업데이트
            yield return UpdateRemainingTime();

            // 현재 진행한 판수 증가
            count++;

            // 버튼 활성화 설정
            GameUIManager.Instance.ActiveMukChiBaButton(false);

            // 컴퓨터 선택 화면에 보이기
            ChangeComputerSelection();

            // 결과 처리
            if (playerSelection == MukChiBa.None) // 플레이어가 선택을 안 했을 경우
            {
                GameUIManager.Instance.ChanageCharacterImage(Character.State.NotSelect);

                // 플레이어의 체력 감소
                playerCurrentHp--;
                GameUIManager.Instance.ChangeHpImage(playerCurrentHp, computerCurrentHp);

                // 선택 이미지를 보여주는 동안 대기
                yield return SelectionLatencyTime;

                // 플레이어 체력이 0이 되었을 경우 가위바위보 종료
                if (playerCurrentHp == 0) break;
            }
            else // 플레이어가 선택을 한 경우
            {
                Result result = ReturnResult();

                // 선택 이미지를 보여주는 동안 대기
                yield return SelectionLatencyTime;

                if (result != Result.Draw)
                {
                    if (result == Result.Win) // 플레이어가 가위바위보에서 이긴 경우
                    {
                        // 공격권을 가져감
                        isAttacker = true;

                        // 플레이 데이터 업데이트
                        playData.UpdateWin();
                    }
                    else
                    {
                        isAttacker = false;
                    }
                }
                else
                {
                    // 똑같은 걸 낸 경우 공격 방어 진행
                    break;
                }
            }
        }
    }

    // 공격 방어 진행
    IEnumerator DoAttackDefence()
    {
        // 제한 시간 초기화
        maxTime = ATK_DEF_TIME;
        remainingTime = maxTime;

        // 선택 초기화
        atkDefSelection = AtkDef.None;

        // 화면 이미지 초기화
        GameUIManager.Instance.InitializeSelectionImage();
        GameUIManager.Instance.ChanageCharacterImage(Character.State.Initial);

        // 버튼 활성화 설정
        GameUIManager.Instance.ActiveAttackDefenceButton(true);
        GameUIManager.Instance.ActiveMukChiBaButton(false);

        // 남은 제한 시간 업데이트
        yield return UpdateRemainingTime();

        // 버튼 활성화 설정
        GameUIManager.Instance.ActiveAttackDefenceButton(false);

        if (isAttacker) // 플레이어가 공격권을 가지고 있는 경우
        {
            if(atkDefSelection == AtkDef.Attack) // 공격을 누른 경우
            {
                GameUIManager.Instance.ChanageCharacterImage(Character.State.AttackSuccess);

                computerCurrentHp--;
                GameUIManager.Instance.ChangeHpImage(playerCurrentHp, computerCurrentHp);
            }
            else
            {
                GameUIManager.Instance.ChanageCharacterImage(Character.State.AttackFailure);
            }
        }
        else // 플레이어가 공격권을 가지고 있지 않은 경우
        {
            if(atkDefSelection == AtkDef.Defence) // 방어를 누른 경우
            {
                GameUIManager.Instance.ChanageCharacterImage(Character.State.DefenceSuccess);
            }
            else
            {
                GameUIManager.Instance.ChanageCharacterImage(Character.State.DefenceFailure);

                playerCurrentHp--;
                GameUIManager.Instance.ChangeHpImage(playerCurrentHp, computerCurrentHp);
            }
        }

        // 플레이 데이터 업데이트
        playData.UpdateAtkDef(isAttacker, count);

        // 결과 이미지를 보여주는 동안 대기
        yield return AtkDefLatencyTime;
    }

    // 게임 종료
    void EndGame()
    {
        Time.timeScale = 0;

        GameUIManager.Instance.ActiveSpeechBubble(false);

        // 체력 관련 승패 처리
        if(computerCurrentHp == 0) // 플레이어가 이긴 경우
        {
            GameUIManager.Instance.ChanageCharacterImage(Character.State.Win);
            GameUIManager.Instance.SetResultText(true, "You Win!");

            // 플레이 데이터 업데이트
            playData.UpdateEnd(GamePlayData.Result.Win, playTime);
        }
        else
        {
            GameUIManager.Instance.ChanageCharacterImage(Character.State.Lose);
            GameUIManager.Instance.SetResultText(true, "You Lose.");

            // 플레이 데이터 업데이트
            playData.UpdateEnd(GamePlayData.Result.Lose, playTime);
        }

        // 플레이 데이터 저장
        JsonManager.Instance.SaveGameData(playData);
    }

    // 게임 설정 초기화
    void InitializeGameOptions()
    {
        // 제한 시간 초기화
        maxTime = INIT_TIME - count * DECREASE_TIME;
        if (maxTime < DECREASE_LIMIT_TIME) maxTime = DECREASE_LIMIT_TIME;

        remainingTime = maxTime;

        // 선택 초기화
        playerSelection = MukChiBa.None;
        computerSelection = MukChiBa.None;

        // 화면 이미지 초기화
        GameUIManager.Instance.InitializeSelectionImage();
        GameUIManager.Instance.ChanageCharacterImage(Character.State.Initial);

        // 버튼 활성화 설정
        GameUIManager.Instance.ActiveAttackDefenceButton(false);
        GameUIManager.Instance.ActiveMukChiBaButton(true);
    }

    // 남은 제한 시간 업데이트
    IEnumerator UpdateRemainingTime()
    {
        while (remainingTime > 0.0f)
        {
            remainingTime -= Time.deltaTime;

            GameUIManager.Instance.UpdateTimerBar(maxTime, remainingTime);

            yield return null;
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