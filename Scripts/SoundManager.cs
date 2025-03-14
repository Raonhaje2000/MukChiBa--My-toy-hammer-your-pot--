using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BGM �Ǵ� ȿ������ ���õ� ó���� �ϴ� Ŭ����
public class SoundManager : MonoBehaviour
{
    public enum BGM { Title = 0, Game = 1 }
    public enum SFX { Menu = 0, Game = 1 }

    const int MAX_VOLUME = 100;

    static SoundManager instance;

    [SerializeField] AudioClip titleBgm; // Ÿ��Ʋ���� ���̴� BGM
    [SerializeField] AudioClip gameBgm;  // ����(�����)���� ���̴� BGM

    [SerializeField] AudioClip menuButtonSfx; // �޴� ��ư Ŭ���� ������ ȿ����
    [SerializeField] AudioClip gameButtonSfx; // ���� ��ư(�����, ����/���) Ŭ���� ������ ȿ����

    int bgmVolume; // BGM ����
    int sfxVolume; // ȿ���� ����

    GameObject bgmObject; // BGM ������Ʈ
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

    // BGM ���丮 ���� (enum�� Ű ������ AudioClip�� ���� ã�� ����)
    void CreateBgmDictionary()
    {
        bgmDictionary = new Dictionary<BGM, AudioClip>();

        bgmDictionary.Add(BGM.Title, titleBgm);
        bgmDictionary.Add(BGM.Game, gameBgm);

        sfxDictionary = new Dictionary<SFX, AudioClip>();

        sfxDictionary.Add(SFX.Menu, menuButtonSfx);
        sfxDictionary.Add(SFX.Game, gameButtonSfx);
    }

    // BGM ������Ʈ ����
    void CreateBgmObject()
    {
        if (bgmObject == null)
        {
            bgmObject = new GameObject("BGM");     // BGM �̶�� �̸����� ������Ʈ ����
            bgmAudioSource = bgmObject.AddComponent<AudioSource>(); // AudioSource ������Ʈ �߰�

            DontDestroyOnLoad(bgmObject);
        }
    }

    // BGM ���
    public void PlayBgm(BGM bgm, bool isRestart = false)
    {
        // BGM�� ����ǰ� ���� �ʰų�, ���� BGM�� �ٸ� BGM�� Ʋ���� �Ҷ�, �Ǵ� ������ؼ� ó������ �ٽ� Ʋ��� �� ���� ���
        if (!bgmAudioSource.isPlaying || (bgmAudioSource.isPlaying && bgmAudioSource.clip != bgmDictionary[bgm]) || isRestart)
        {
            bgmAudioSource.clip = bgmDictionary[bgm];
            bgmAudioSource.volume = (float)bgmVolume / (float)MAX_VOLUME;
            bgmAudioSource.loop = true;

            bgmAudioSource.Play();
        }
    }

    // ȿ���� ���
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

    // BGM ���� ����
    public void ChangeBgmVolume(int volume)
    {
        bgmVolume = volume;
        bgmAudioSource.volume = (float)bgmVolume / (float)MAX_VOLUME;
    }
}
