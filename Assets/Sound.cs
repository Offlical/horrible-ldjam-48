using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Sound 
{
    public string name;
    public float pitch = 1f;
    public float volume = 1f;
    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;
    
}
