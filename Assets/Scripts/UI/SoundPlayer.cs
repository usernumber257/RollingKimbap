using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : Singleton<SoundPlayer>
{
    [SerializeField] AudioSource[] sounds;

    Dictionary<MyEnum.Sound, AudioSource> soundDic = new Dictionary<MyEnum.Sound, AudioSource>();


    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < sounds.Length; i++)
        {
            soundDic.Add((MyEnum.Sound)Enum.Parse(typeof(MyEnum.Sound), (sounds[i].gameObject.name)), sounds[i]);
        }
    }

    public void Play(MyEnum.Sound sound)
    {
        soundDic[sound].Play();
    }
}
