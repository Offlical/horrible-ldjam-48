using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    private static int instanceID = -1;
    private void Awake()
    {
        if (instanceID == -1)
            instanceID = gameObject.GetInstanceID();
        else
            Destroy(gameObject);

        foreach(Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.clip = s.clip;

        }

    }

    private void Start()
    {
        Play("Welcome");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogError("No sound found by the name of " + s.name);
            return;
        }
        s.source.Play();
    }
}
