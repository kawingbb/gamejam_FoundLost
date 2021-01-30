using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }    
    }

    public List<AudioSource> clipList;

    public int currentClipIndex = 0;
    public float currentTime;

    private void Start()
    {
        clipList[currentClipIndex].Play();
        currentClipIndex++;
    }

    public void nextClip()
    {
        if (currentClipIndex < clipList.Capacity)
        {
            clipList[currentClipIndex].time = clipList[currentClipIndex-1].time;
            clipList[currentClipIndex-1].Stop();
            clipList[currentClipIndex].Play();
            currentClipIndex++;
        }
    }

    public void stopAllClip()
    {
        clipList[currentClipIndex-1].Stop();
    }
}
