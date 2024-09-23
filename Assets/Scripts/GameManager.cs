using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �÷��̿� ���õ� �κе��� ó���ϴ� Ŭ����
public class GameManager : MonoBehaviour
{
    const int MAX_HP = 6; // �ִ� ü��

    const float INIT_TIME = 3.0f;           // ���� ù �� ���� �ð�
    const float DECREASE_TIME = 0.2f;      // �� �� �����ϴ� �ð�
    const float DECREASE_LIMIT_TIME = 0.4f; // �� �� �����ϴ� �ð��� �Ѱ�ġ
    const float ATK_DEF_TIME = 0.4f;        // ���� �Ǵ� ��� ���� �ð�

    public enum MukChiBa { None = -1, Muk = 0, Chi = 1, Ba = 2 } // ����� ����
    public enum Result { Win = 1, Draw = 0, Lose = -1 } // ����� ���

    public static GameManager instance;

    // ���� ü��
    [SerializeField] int playerCurrentHp;
    [SerializeField] int computerCurrentHp;

    float playTime;    // ��ü �÷��� �ð�

    [SerializeField] int count;         // �� �ǿ��� ������� �� Ƚ�� (��ǻ�Ͱ� �� Ƚ����� �����ϸ� ����)

    float maxTime;         // �ִ� ���� �ð�
    float remainingTime;   // ���� ���� �ð�

    [SerializeField] bool isAttacker;   // �÷��̾��� ���ݱ� ����

    // ����� �� ������ ��
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
        Initialize(); // �ʱ�ȭ

        StartGame(); // ���� ����
    }

    void Update()
    {

    }

    // �ʱ�ȭ
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

            GameUIManager.instance.UpdatePlayTimeText(playTime);

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

        GameUIManager.instance.ChangeComputerSelectionImage(computerSelection);
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

            // ���� ��� ����
            Debug.Log("���� ��� ����");

            if (playerCurrentHp == 0 || computerCurrentHp == 0) break;
        }

        // ü�� ���� ���� ó��
    }

    // ���������� ����
    IEnumerator DoRockPaperScissors()
    {
        // ���а� �������� ������ ��� ���������� ����
        while (true)
        {
            // 2�� ���
            yield return new WaitForSeconds(2.0f);

            // ���� ���� �ʱ�ȭ
            InitializeGameOptions();

            // ���� ���� �ð� ������Ʈ
            yield return UpdateRemainingTime();

            // ���� ������ �Ǽ� ����
            count++;

            // ��ư Ȱ��ȭ ����
            GameUIManager.instance.ActiveMukChiBaButton(false);

            // ��ǻ�� ���� ȭ�鿡 ���̱�
            ChangeComputerSelection();

            // ��� ó��
            if (playerSelection == MukChiBa.None) // �÷��̾ ������ �� ���� ���
            {
                GameUIManager.instance.ChanageCharacterImage(Character.State.NotSelect);

                // �÷��̾��� ü�� ����
                playerCurrentHp--;
                GameUIManager.instance.ChangeHpImage(playerCurrentHp, computerCurrentHp);

                // �÷��̾� ü���� 0�� �Ǿ��� ��� ���������� ����
                if (playerCurrentHp == 0) break;
            }
            else // �÷��̾ ������ �� ���
            {
                Result result = ReturnResult();

                if (result != Result.Draw)
                {
                    // �÷��̾ �������������� �̱� ��� ���ݱ��� ������
                    isAttacker = (result == Result.Win) ? true : false;

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
            // 2�� ���
            yield return new WaitForSeconds(2.0f);

            // ���� ���� �ʱ�ȭ
            InitializeGameOptions();

            // ���� ���� �ð� ������Ʈ
            yield return UpdateRemainingTime();

            // ���� ������ �Ǽ� ����
            count++;

            // ��ư Ȱ��ȭ ����
            GameUIManager.instance.ActiveMukChiBaButton(false);

            // ��ǻ�� ���� ȭ�鿡 ���̱�
            ChangeComputerSelection();

            // ��� ó��
            if (playerSelection == MukChiBa.None) // �÷��̾ ������ �� ���� ���
            {
                GameUIManager.instance.ChanageCharacterImage(Character.State.NotSelect);

                // �÷��̾��� ü�� ����
                playerCurrentHp--;
                GameUIManager.instance.ChangeHpImage(playerCurrentHp, computerCurrentHp);

                // �÷��̾� ü���� 0�� �Ǿ��� ��� ���������� ����
                if (playerCurrentHp == 0) break;
            }
            else // �÷��̾ ������ �� ���
            {
                Result result = ReturnResult();

                if (result != Result.Draw)
                {
                    // �÷��̾ �̱� ��� ���ݱ��� ������
                    isAttacker = (result == Result.Win) ? true : false;
                }
                else
                {
                    // �Ȱ��� �� �� ��� ���� ��� ����
                    break;
                }
            }
        }
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
        GameUIManager.instance.InitializeSelectionImage();
        GameUIManager.instance.ChanageCharacterImage(Character.State.Initial);

        // ��ư Ȱ��ȭ ����
        GameUIManager.instance.ActiveAttackDefenceButton(false);
        GameUIManager.instance.ActiveMukChiBaButton(true);
    }

    // ���� ���� �ð� ������Ʈ
    IEnumerator UpdateRemainingTime()
    {
        while (remainingTime > 0.0f)
        {
            remainingTime -= Time.deltaTime;

            GameUIManager.instance.UpdateTimerBar(maxTime, remainingTime);

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