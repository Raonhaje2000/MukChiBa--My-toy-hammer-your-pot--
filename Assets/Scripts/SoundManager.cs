using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BGM 또는 효과음과 관련된 처리를 하는 클래스
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Range(0, 100)]
    public int volume; // BGM 볼륨

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
