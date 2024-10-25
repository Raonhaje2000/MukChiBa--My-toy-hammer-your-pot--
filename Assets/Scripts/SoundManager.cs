using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BGM �Ǵ� ȿ������ ���õ� ó���� �ϴ� Ŭ����
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    int volume; // BGM ����

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
