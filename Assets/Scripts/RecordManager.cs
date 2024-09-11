using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이 기록을 관리하는 클래스
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
