using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateItem : MonoBehaviour
{
    public GameObject[] objs;
    public bool turnOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(turnOn)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].SetActive(true);
            }
        } else 
        {
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].SetActive(false);
            }
        }
    }
}
