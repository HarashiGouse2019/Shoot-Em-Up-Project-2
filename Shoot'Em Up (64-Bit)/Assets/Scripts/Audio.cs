﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool enableLoop;

    [HideInInspector] public AudioSource source;




}
