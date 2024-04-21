using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] Sound[] sounds;

    private Sound[] music;
    private Sound[] playedMusicBuffer;

    private bool isCoroutineActive = false;
    private bool musicIsPlaying = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;

            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
        }

        int k = 0;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].soundType == SoundType.Music)
                k++;
        }
        music = new Sound[k];
        playedMusicBuffer = new Sound[k];
        k = 0;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].soundType == SoundType.Music)
            {
                music[k] = sounds[i];
                k++;
            }
        }
        for (int i = 0; i < playedMusicBuffer.Length; i++)
        {
            playedMusicBuffer[i] = null;
        }

        isCoroutineActive = false;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].soundType == SoundType.SFX)
                sounds[i].source.volume = sounds[i].volume * SerializeManager.Instance.GetFloat(FloatType.SfxVolume) * SerializeManager.Instance.GetFloat(FloatType.MasterVolume);
            else if (sounds[i].soundType == SoundType.Music)
                sounds[i].source.volume = sounds[i].volume * SerializeManager.Instance.GetFloat(FloatType.MusicVolume) * SerializeManager.Instance.GetFloat(FloatType.MasterVolume);
        }

        if(!isCoroutineActive & !musicIsPlaying)
            StartCoroutine(PlayMusicCoroutine());
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
                sounds[i].source.Play();
        }
    }

    public void StopSound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
                sounds[i].source.Stop();
        }
    }

    public bool IsMusicPlaying()
    {
        for (int i = 0; i < music.Length; i++)
        {
            if (music[i] != null)
            {
                if (music[i].source.isPlaying)
                    return true;
            }
            else if (playedMusicBuffer[i] != null)
            {
                if (playedMusicBuffer[i].source.isPlaying)
                    return true;
            }
        }
        return false;
    }

    public IEnumerator PlayMusicCoroutine()
    {
        isCoroutineActive = true;

        int k = 0;
        for (int i = 0; i < music.Length; i++)
        {
            if (music[i] == null)
                k++;
        }
        if (k == music.Length)
        {
            for (int i = 0; i < music.Length; i++)
            {
                music[i] = playedMusicBuffer[i];
                playedMusicBuffer[i] = null;
            }
        }

        int random = Random.Range(0, music.Length);
        while (music[random] == null)
        {
            random = Random.Range(0, music.Length);
        }

        musicIsPlaying = true;
        PlaySound(music[random].name);

        yield return new WaitForSeconds(music[random].clip.length);

        musicIsPlaying = false;

        for(int i = 0; i < playedMusicBuffer.Length; i++)
        {
            if (playedMusicBuffer[i] == null)
            {
                playedMusicBuffer[i] = music[random];
                break;
            }
        }

        music[random] = null;

        for (int i = 0; i < music.Length; i++)
        {
            if (music[i] == null & music.Length > i + 1)
            {
                if(music[i + 1] != null)
                {
                    music[i] = music[i + 1];
                    music[i + 1] = null;
                }
            }
        }

        StartCoroutine(PlayMusicCoroutine());
    }
}
