using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �÷��̿� ���õ� �κе��� ó���ϴ� Ŭ����
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    float playTime;    // ��ü �÷��� �ð�

    float initTime;    // ���� ù �� ���� �ð�
    float currentTime; // ���� ���� �ð�

    bool isAttacker;   // �÷��̾��� ���ݱ� ����

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
