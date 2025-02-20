using System;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _Musics;
    [SerializeField] private float _TransitionDuration;
    private int _CurMusicInd;


    [SerializeField] private List<AudioClip> _SFXs;
    private int _CurSFXInd;

    private void Awake()
    {
        _CurMusicInd = 0;
    }

    void Start()
    {
    }


    [ContextMenu("Test Music")]
    public void TestMusic()
    {
        AudioManager.Instance.PlayMusic(_Musics[_CurMusicInd], _TransitionDuration);
        ++_CurMusicInd;
        _CurMusicInd %= _Musics.Count;
    }

    [ContextMenu("Test SFX")]
    public void TestSFX()
    {
        AudioManager.Instance.PlaySFX(_SFXs[_CurSFXInd], 1);
        ++_CurSFXInd;
        _CurSFXInd %= _SFXs.Count;
    }
}