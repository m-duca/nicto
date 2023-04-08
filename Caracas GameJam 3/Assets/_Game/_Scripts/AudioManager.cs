using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private Music music;
    [SerializeField] private SFX[] sfxs;

    [SerializeField, Range(0f, 1f)] private float masterVolume;

    // Musics
    private float musicCurTime;

    private AudioSource curMusicAudioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AudioListener.volume = masterVolume;
    }

    private void Start()
    {
        AudioListener.volume = masterVolume;
    }

    private void Update()
    {
        if (curMusicAudioSource != null) musicCurTime = curMusicAudioSource.time;
    }

    public void PlaySFX(string name)
    {
        foreach (SFX s in sfxs)
        {
            if (s.Clip.name == name)
            {

                var sfx = new GameObject("SFX " + s.Clip.name);
                var sAudioSource = sfx.AddComponent<AudioSource>();
                sAudioSource.clip = s.Clip;
                sAudioSource.volume = s.Volume;
                sAudioSource.Play();
                Destroy(sfx, 5f);
                break;
            }
        }
    }

    public void PlayMusic(string name)
    {
        var mObj = new GameObject("Music " + music.Clip.name);
        var mAudioSource = mObj.AddComponent<AudioSource>();
        mAudioSource.clip = music.Clip;
        mAudioSource.volume = music.Volume;
        if (musicCurTime != 0) mAudioSource.time = musicCurTime;
        mAudioSource.Play();
        mAudioSource.loop = true;

        curMusicAudioSource = mAudioSource;
    }
}
