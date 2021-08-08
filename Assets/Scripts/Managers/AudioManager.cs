using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] Sound[] sounds;

    private Sound[] music;
    private Sound[] playedMusicBuffer;

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

        if (!musicIsPlaying)
            StartCoroutine(PlayMusicCoroutine());
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

    public IEnumerator PlayMusicCoroutine()
    {
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

        musicIsPlaying = false;
        PlaySound(music[random].name);

        yield return new WaitForSeconds(music[random].clip.length);

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
