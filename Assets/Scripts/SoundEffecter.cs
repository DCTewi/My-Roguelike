using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundEffecter : MonoBehaviour
{
    public string[] nameList;
    public AudioClip[] clipList;

    private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    private AudioSource soundPlayer;
    private Queue<AudioClip> waitQueue = new Queue<AudioClip>();

    private void OnEnable()
    {
        soundPlayer = GetComponent<AudioSource>();

        int len = Mathf.Min(nameList.Length, clipList.Length);
        for (int i = 0; i < len; i++)
        {
            sounds.Add(nameList[i], clipList[i]);
        }
    }

    public void Play(string soundName, bool tryForcePlayNow = true)
    {
        if (sounds.ContainsKey(soundName))
        {
            if (tryForcePlayNow)
            {
                if (soundPlayer.isPlaying)
                {
                    soundPlayer.Pause();
                }
                soundPlayer.clip = sounds[soundName];
                soundPlayer.Play();
            }
            else
            {
                waitQueue.Enqueue(sounds[soundName]);
            }
        }
        else
        {
            Debug.LogError("No such clip named " + soundName + "!");
        }
    }

    public void Update()
    {
        if (!soundPlayer.isPlaying && waitQueue.Count != 0)
        {
            soundPlayer.clip = waitQueue.Dequeue();
            soundPlayer.Play();
        }
    }
}
