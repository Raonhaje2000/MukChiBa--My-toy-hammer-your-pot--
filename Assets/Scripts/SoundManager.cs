using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BGM �Ǵ� ȿ������ ���õ� ó���� �ϴ� Ŭ����
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Range(0, 100)]
    public int volume; // BGM ����

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
