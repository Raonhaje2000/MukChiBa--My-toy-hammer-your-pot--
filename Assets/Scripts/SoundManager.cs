using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BGM 또는 효과음과 관련된 처리를 하는 클래스
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    int volume; // BGM 볼륨

    public static SoundManager Instance
    {
        get { return instance; }
    }

    public int Volume
    { 
        get { return volume; }
        set { volume = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            volume = 50;
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
