using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleSwitch : MonoBehaviour
{
    public bool switchActivated;
    public mainSwitch mS;
    public GameObject switchLight;
    public AudioSource switchSound;
    public void activateSwitch()
    {
        if(!switchActivated)
        {
            switchSound.Play();
            switchActivated = true;
            mS.switchCount++;
            switchLight.SetActive(true);
        }
        
    }
}
