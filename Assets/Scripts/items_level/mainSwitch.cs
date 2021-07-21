using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainSwitch : MonoBehaviour
{
    /* 
    Attach this script to the parent switch which
    will store all the switches including the parent switch if its a switch
    it will activate any doors or platforms once all switches are activated    
    */
    public singleSwitch[] switches;
    public int switchCount;

    /*
    Add an array for platforms
    and check if they are empty before activating them
    */
    public doorControl[] doorCont;
    public platformControlled[] platformCont;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // modify to accept platforms
        if (switchCount >= switches.Length)
        {
            if(doorCont.Length > 0)
            {
                for (int i = 0; i < doorCont.Length; i++)
                {
                    doorCont[i].activateDoor = true;
                }
            }

            if(platformCont.Length > 0)
            {
                for (int i = 0; i < platformCont.Length; i++)
                {
                    platformCont[i].activatePlatform = true;
                }
            }

            
        }

    }
}
