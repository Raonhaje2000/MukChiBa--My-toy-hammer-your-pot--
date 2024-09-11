using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��� ����� �����ϴ� Ŭ����
public class RecordManager : MonoBehaviour
{
    public static RecordManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
