using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BGM 또는 효과음과 관련된 처리를 하는 클래스
public class SoundManager : MonoBehaviour
{
    public enum BGM { Title = 0, Game = 1 }
    public enum SFX { Menu = 0, Game = 1 }

    const int MAX_VOLUME = 100;

    static SoundManager instance;

    [SerializeField] AudioClip titleBgm; // 타이틀에서 쓰이는 BGM
    [SerializeField] AudioClip gameBgm;  // 게임(묵찌빠)에서 쓰이는 BGM

    [SerializeField] AudioClip menuButtonSfx; // 메뉴 버튼 클릭시 나오는 효과음
    [SerializeField] AudioClip gameButtonSfx; // 게임 버튼(묵찌빠, 공격/방어) 클릭시 나오는 효과음

    int bgmVolume; // BGM 볼륨
    int sfxVolume; // 효과음 볼륨

    GameObject bgmObject; // BGM 오브젝트
    AudioSource bgmAudioSource;

    Dictionary<BGM, AudioClip> bgmDictionary;
    Dictionary<SFX, AudioClip> sfxDictionary;

    public static SoundManager Instance
    {
        get { return instance; }
    }

    public int BgmVolume
    { 
        get { return bgmVolume; }
        set { bgmVolume = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            CreateBgmDictionary();
            CreateBgmObject();

            bgmVolume = 50;
            sfxVolume = 100;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM 디렉토리 생성 (enum을 키 값으로 AudioClip을 쉽게 찾기 위함)
    void CreateBgmDictionary()
    {
        bgmDictionary = new Dictionary<BGM, AudioClip>();

        bgmDictionary.Add(BGM.Title, titleBgm);
        bgmDictionary.Add(BGM.Game, gameBgm);

        sfxDictionary = new Dictionary<SFX, AudioClip>();

        sfxDictionary.Add(SFX.Menu, menuButtonSfx);
        sfxDictionary.Add(SFX.Game, gameButtonSfx);
    }

    // BGM 오브젝트 생성
    void CreateBgmObject()
    {
        if (bgmObject == null)
        {
            bgmObject = new GameObject("BGM");     // BGM 이라는 이름으로 오브젝트 생성
            bgmAudioSource = bgmObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트 추가

            DontDestroyOnLoad(bgmObject);
        }
    }

    // BGM 재생
    public void PlayBgm(BGM bgm, bool isRestart = false)
    {
        // BGM이 재생되고 있지 않거나, 현재 BGM과 다른 BGM을 틀려고 할때, 또는 재시작해서 처음부터 다시 틀어야 할 때만 재생
        if (!bgmAudioSource.isPlaying || (bgmAudioSource.isPlaying && bgmAudioSource.clip != bgmDictionary[bgm]) || isRestart)
        {
            bgmAudioSource.clip = bgmDictionary[bgm];
            bgmAudioSource.volume = (float)bgmVolume / (float)MAX_VOLUME;
            bgmAudioSource.loop = true;

            bgmAudioSource.Play();
        }
    }

    // 효과음 재생
    public void PlaySfx(SFX sfx)
    {
        GameObject sfxObject = new GameObject("SFX");
        AudioSource sfxAudioSource = sfxObject.AddComponent<AudioSource>();

        sfxAudioSource.clip = sfxDictionary[sfx];
        sfxAudioSource.volume = (float)sfxVolume / (float)MAX_VOLUME;

        sfxAudioSource.Play();

        DontDestroyOnLoad(sfxObject);
        Destroy(sfxObject, sfxAudioSource.clip.length);
    }

    // BGM 볼륨 변경
    public void ChangeBgmVolume(int volume)
    {
        bgmVolume = volume;
        bgmAudioSource.volume = (float)bgmVolume / (float)MAX_VOLUME;
    }
}
