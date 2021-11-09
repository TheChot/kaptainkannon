using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mountainSpawner : MonoBehaviour
{
    public Transform mountainSpawned;
    public GameObject mountain;
    public Transform theParent;
   

    // Update is called once per frame
    void Update()
    {
        if(mountainSpawned != null)
        {
            if(mountainSpawned.GetChild(1).transform.position.x < transform.position.x)
            {
                GameObject _mountain = (GameObject)Instantiate(mountain, transform.position, transform.rotation);
                mountainSpawned = _mountain.transform;
                mountainSpawned.SetParent(theParent);
            }
        }
    }
}
