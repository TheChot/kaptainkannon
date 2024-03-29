﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgMusicSearch : MonoBehaviour
{
    public AudioSource[] bgmusic;
    public int songIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bgmusic.Length; i++)
        {
            bgmusic[i].Stop();
        }
        if(checkPointManager.instance.songSet)
            songIndex = checkPointManager.instance.songIndex;
        bgmusic[songIndex].Play();
    }

    public void songToSet(int songInt)
    {
        bgmusic[songIndex].Stop();
        bgmusic[songInt].Play();
        songIndex = songInt;
    }
}
