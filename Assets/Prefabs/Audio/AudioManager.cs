using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _MusicChanels;
    private int _CurMusicChanInd;
    private static AudioManager _Instance;

    public static AudioManager Instance => _Instance;

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(gameObject);
        }

        _Instance = this;
        _CurMusicChanInd = 0;
    }

    public void PlaySFX(AudioClip sfxClip, float volume = 1.0f, Vector3 position = default)
    {
        AudioSource.PlayClipAtPoint(sfxClip, position, volume);
    }

    public void PlaySFXRandom(List<AudioClip> sfxClips, float volume = 1.0f, Vector3 position = default)
    {
        if (sfxClips == null || sfxClips.Count <= 0)
        {
            return;
        }

        AudioSource.PlayClipAtPoint(sfxClips[Random.Range(0, sfxClips.Count)], position, volume);
    }

    public void PlayMusic(AudioClip musicClip, float transitionDuration = 1.0f, float volume = 1.0f, bool loop = true)
    {
        StartCoroutine(TransitionMusicCr(_MusicChanels[_CurMusicChanInd], 1, 0, transitionDuration));
        ++_CurMusicChanInd;
        _CurMusicChanInd %= _MusicChanels.Count;
        _MusicChanels[_CurMusicChanInd].clip = musicClip;
        _MusicChanels[_CurMusicChanInd].loop = loop;
        _MusicChanels[_CurMusicChanInd].Play();
        StartCoroutine(TransitionMusicCr(_MusicChanels[_CurMusicChanInd], 0, 1, transitionDuration));
    }

    IEnumerator TransitionMusicCr(AudioSource src, float from, float to, float duration)
    {
        if (duration <= 0)
        {
            src.volume = to;
        }
        else
        {
            float delta = (to - from) / duration;
            src.volume = from;
            while (!Mathf.Approximately(src.volume, to))
            {
                src.volume += delta * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            src.volume = to;
        }
    }

    public void PauseMusic()
    {
        _MusicChanels[_CurMusicChanInd].Pause();
    }

    public void UnPauseMusic()
    {
        _MusicChanels[_CurMusicChanInd].UnPause();
    }
}