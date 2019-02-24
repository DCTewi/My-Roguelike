using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundEffecter : MonoBehaviour
{
    public string[] nameList;
    public AudioClip[] clipList;

    private Dictionary<string, AudioClip> sounds;
    private AudioSource soundPlayer;

    private void Awake()
    {
        int len = Mathf.Min(nameList.Length, clipList.Length);
        for (int i = 0; i < len; i++)
        {
            sounds.Add(nameList[i], clipList[i]);
        }
    }

    public void Play(string soundName)
    {
        if (sounds.ContainsKey(soundName))
        {
            if (soundPlayer.isPlaying)
            {
                soundPlayer.Pause();
            }

            soundPlayer.clip = sounds[soundName];
            soundPlayer.Play();
        }
    }
}
