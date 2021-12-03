using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgSwitch : MonoBehaviour
{
    public GameObject[] bg;
    public int bgIndex = 0;
    void Start()
    {
        for (int i = 0; i < bg.Length; i++)
        {
            bg[i].SetActive(false);
        }
        if(checkPointManager.instance.bgSet)
            bgIndex = checkPointManager.instance.bgIndex;
        
        bg[bgIndex].SetActive(true);
    }

    public void bgToSet(int bgInt)
    {
        bg[bgIndex].SetActive(false);
        bg[bgInt].SetActive(true);
        bgIndex = bgInt;
    }
}
