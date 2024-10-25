using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �÷��̿� ���õ� �κе��� ó���ϴ� Ŭ����
public class GameManager : MonoBehaviour
{
    const int MAX_HP = 2; // �ִ� ü��

    const float INIT_TIME = 2.0f;           // ���� ù �� ���� �ð�
    const float DECREASE_TIME = 0.2f;      // �� �� �����ϴ� �ð�
    const float DECREASE_LIMIT_TIME = 0.4f; // �� �� �����ϴ� �ð��� �Ѱ�ġ
    const float ATK_DEF_TIME = 0.4f;        // ���� �Ǵ� ��� ���� �ð�

    WaitForSeconds SelectionLatencyTime = new WaitForSeconds(2.0f); // ����� ���� �̹����� �����ֱ� ���� ��� �ð�
    WaitForSeconds AtkDefLatencyTime = new WaitForSeconds(3.0f); // ���� ��� ��� �̹����� �����ֱ� ���� ��� �ð�

    public enum MukChiBa { None = -1, Muk = 0, Chi = 1, Ba = 2 } // ����� ����
    public enum Result { Win = 1, Draw = 0, Lose = -1 } // ����� ���
    public enum AtkDef { Attack = 1, None = 0, Defence = -1 } // ���� ���

    static GameManager instance;

    // ���� ü��
    [SerializeField] int playerCurrentHp;
    [SerializeField] int computerCurrentHp;

    float playTime;    // ��ü �÷��� �ð�

    [SerializeField] int count;         // �� �ǿ��� ������� �� Ƚ��, Ƚ���� ���� ���� �ð� ���� (��ǻ�Ͱ� �� Ƚ����� �����ϸ� ����)

    float maxTime;         // �ִ� ���� �ð�
    float remainingTime;   // ���� ���� �ð�

    [SerializeField] bool isAttacker;   // �÷��̾��� ���ݱ� ����

    // ����� �� ������ ��
    [SerializeField] MukChiBa playerSelection;
    [SerializeField] MukChiBa computerSelection;

    // ���ݰ� ��� �� ������ ��
    [SerializeField] AtkDef atkDefSelection;

    GamePlayData playData; // ������ ������

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
        Initialize(); // �ʱ�ȭ

        StartGame(); // ���� ����
    }

    // �ʱ�ȭ
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

    // ���� ����
    public void StartGame()
    {
        playTime = 0.0f;

        StartCoroutine(UpdatePlayTime()); // ��ü �÷��� �ð� ������Ʈ
        StartCoroutine(PlayGame()); // ���� ����
    }

    // ��ü �÷��� �ð� ����
    IEnumerator UpdatePlayTime()
    {
        while (true)
        {
            playTime += Time.deltaTime;

            GameUIManager.Instance.UpdatePlayTimeText(playTime);

            yield return null;
        }
    }

    // �÷��̾��� ���� ���� (Ŭ���� ��ư�� ���� ����)
    public void ChangePlayerSelection(MukChiBa selection)
    {
        playerSelection = selection;
    }

    // ��ǻ���� ���� ���� (�������� �ϳ� �̾� ����)
    void ChangeComputerSelection()
    {
        int index = Random.Range(0, 3); // ����� �� ���� // int�� �ִ� ������ �ȵǹǷ� 3����

        computerSelection = (MukChiBa) index; // ������ enum ���� ����

        GameUIManager.Instance.ChangeComputerSelectionImage(computerSelection);
    }

    // ���� ��� ���� ����
    public void ChangeAtkDefSelection(AtkDef selection)
    {
        atkDefSelection = selection;
    }

    // ���� ����
    IEnumerator PlayGame()
    {
        // �÷��̾�� ��ǻ���� ü���� ��� ������������ ���� ����
        while (true)
        {
            // �ʱ�ȭ
            isAttacker = false;
            count = 0;

            // ���������� ���� (������������ ����)
            yield return DoRockPaperScissors();

            if (playerCurrentHp == 0) break;

            // ����� ����
            yield return DoMukChiBa();

            if (playerCurrentHp == 0) break;

            // ���� ��� ����
            yield return DoAttackDefence();

            if (playerCurrentHp == 0 || computerCurrentHp == 0) break;
        }

        // ���� ����
        EndGame();
    }

    // ���������� ����
    IEnumerator DoRockPaperScissors()
    {
        // ���а� �������� ������ ��� ���������� ����
        while (true)
        {
            // ���� ���� �ʱ�ȭ
            InitializeGameOptions();

            // ���� ���� �ð� ������Ʈ
            yield return UpdateRemainingTime();

            // ���� ������ �Ǽ� ����
            count++;

            // ��ư Ȱ��ȭ ����
            GameUIManager.Instance.ActiveMukChiBaButton(false);

            // ��ǻ�� ���� ȭ�鿡 ���̱�
            ChangeComputerSelection();

            // ��� ó��
            if (playerSelection == MukChiBa.None) // �÷��̾ ������ �� ���� ���
            {
                GameUIManager.Instance.ChanageCharacterImage(Character.State.NotSelect);

                // �÷��̾��� ü�� ����
                playerCurrentHp--;
                GameUIManager.Instance.ChangeHpImage(playerCurrentHp, computerCurrentHp);

                // ���� �̹����� �����ִ� ���� ���
                yield return SelectionLatencyTime;

                // �÷��̾� ü���� 0�� �Ǿ��� ��� ���������� ����
                if (playerCurrentHp == 0) break;
            }
            else // �÷��̾ ������ �� ���
            {
                Result result = ReturnResult();

                // ���� �̹����� �����ִ� ���� ���
                yield return SelectionLatencyTime;

                if (result != Result.Draw)
                {
                    if (result == Result.Win) // �÷��̾ �������������� �̱� ���
                    {
                        // ���ݱ��� ������
                        isAttacker = true;

                        // �÷��� ������ ������Ʈ
                        playData.UpdateWin();
                    }
                    else
                    {
                        isAttacker = false;
                    }

                    // ����� ����
                    break;
                }
            }

            // ���� ������ �Ѿ�� �ƴϹǷ� �Ǽ� �ѹ�
            count--;
        }
    }

    // ����� ����
    IEnumerator DoMukChiBa()
    {
        while (true)
        {
            // ���� ���� �ʱ�ȭ
            InitializeGameOptions();

            // ���� ���� �ð� ������Ʈ
            yield return UpdateRemainingTime();

            // ���� ������ �Ǽ� ����
            count++;

            // ��ư Ȱ��ȭ ����
            GameUIManager.Instance.ActiveMukChiBaButton(false);

            // ��ǻ�� ���� ȭ�鿡 ���̱�
            ChangeComputerSelection();

            // ��� ó��
            if (playerSelection == MukChiBa.None) // �÷��̾ ������ �� ���� ���
            {
                GameUIManager.Instance.ChanageCharacterImage(Character.State.NotSelect);

                // �÷��̾��� ü�� ����
                playerCurrentHp--;
                GameUIManager.Instance.ChangeHpImage(playerCurrentHp, computerCurrentHp);

                // ���� �̹����� �����ִ� ���� ���
                yield return SelectionLatencyTime;

                // �÷��̾� ü���� 0�� �Ǿ��� ��� ���������� ����
                if (playerCurrentHp == 0) break;
            }
            else // �÷��̾ ������ �� ���
            {
                Result result = ReturnResult();

                // ���� �̹����� �����ִ� ���� ���
                yield return SelectionLatencyTime;

                if (result != Result.Draw)
                {
                    if (result == Result.Win) // �÷��̾ �������������� �̱� ���
                    {
                        // ���ݱ��� ������
                        isAttacker = true;

                        // �÷��� ������ ������Ʈ
                        playData.UpdateWin();
                    }
                    else
                    {
                        isAttacker = false;
                    }
                }
                else
                {
                    // �Ȱ��� �� �� ��� ���� ��� ����
                    break;
                }
            }
        }
    }

    // ���� ��� ����
    IEnumerator DoAttackDefence()
    {
        // ���� �ð� �ʱ�ȭ
        maxTime = ATK_DEF_TIME;
        remainingTime = maxTime;

        // ���� �ʱ�ȭ
        atkDefSelection = AtkDef.None;

        // ȭ�� �̹��� �ʱ�ȭ
        GameUIManager.Instance.InitializeSelectionImage();
        GameUIManager.Instance.ChanageCharacterImage(Character.State.Initial);

        // ��ư Ȱ��ȭ ����
        GameUIManager.Instance.ActiveAttackDefenceButton(true);
        GameUIManager.Instance.ActiveMukChiBaButton(false);

        // ���� ���� �ð� ������Ʈ
        yield return UpdateRemainingTime();

        // ��ư Ȱ��ȭ ����
        GameUIManager.Instance.ActiveAttackDefenceButton(false);

        if (isAttacker) // �÷��̾ ���ݱ��� ������ �ִ� ���
        {
            if(atkDefSelection == AtkDef.Attack) // ������ ���� ���
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
        else // �÷��̾ ���ݱ��� ������ ���� ���� ���
        {
            if(atkDefSelection == AtkDef.Defence) // �� ���� ���
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

        // �÷��� ������ ������Ʈ
        playData.UpdateAtkDef(isAttacker, count);

        // ��� �̹����� �����ִ� ���� ���
        yield return AtkDefLatencyTime;
    }

    // ���� ����
    void EndGame()
    {
        Time.timeScale = 0;

        GameUIManager.Instance.ActiveSpeechBubble(false);

        // ü�� ���� ���� ó��
        if(computerCurrentHp == 0) // �÷��̾ �̱� ���
        {
            GameUIManager.Instance.ChanageCharacterImage(Character.State.Win);
            GameUIManager.Instance.SetResultText(true, "You Win!");

            // �÷��� ������ ������Ʈ
            playData.UpdateEnd(GamePlayData.Result.Win, playTime);
        }
        else
        {
            GameUIManager.Instance.ChanageCharacterImage(Character.State.Lose);
            GameUIManager.Instance.SetResultText(true, "You Lose.");

            // �÷��� ������ ������Ʈ
            playData.UpdateEnd(GamePlayData.Result.Lose, playTime);
        }

        // �÷��� ������ ����
        JsonManager.Instance.SaveGameData(playData);
    }

    // ���� ���� �ʱ�ȭ
    void InitializeGameOptions()
    {
        // ���� �ð� �ʱ�ȭ
        maxTime = INIT_TIME - count * DECREASE_TIME;
        if (maxTime < DECREASE_LIMIT_TIME) maxTime = DECREASE_LIMIT_TIME;

        remainingTime = maxTime;

        // ���� �ʱ�ȭ
        playerSelection = MukChiBa.None;
        computerSelection = MukChiBa.None;

        // ȭ�� �̹��� �ʱ�ȭ
        GameUIManager.Instance.InitializeSelectionImage();
        GameUIManager.Instance.ChanageCharacterImage(Character.State.Initial);

        // ��ư Ȱ��ȭ ����
        GameUIManager.Instance.ActiveAttackDefenceButton(false);
        GameUIManager.Instance.ActiveMukChiBaButton(true);
    }

    // ���� ���� �ð� ������Ʈ
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
        // ���� Ȯ���� ���ð�(enum�� ������)�� ���� �̿�
        int subtraction = computerSelection - playerSelection;

        // Ex 1) ��ǻ�Ͱ� ��, �÷��̾ � �� (�÷��̾� �й�) --> ���� enum = 0, ���� enum = 1 --> 0 - 1 = -1
        // Ex 2) ��ǻ�Ͱ� ��, �÷��̾ ���� �� (�÷��̾� �й�) --> ���� enum = 2, ���� enum = 0 --> 2 - 0 = 2
        if (subtraction == -1 || subtraction == 2) return Result.Lose;
        else if (subtraction == 0) return Result.Draw;
        else return Result.Win;
    }
}