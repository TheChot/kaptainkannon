using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class gunController : MonoBehaviour
{
    public bool shotGunEquipped = true;
    public GameObject shotGunGFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shotGunEquipped)
        {
            shotGunGFX.SetActive(true);
        }
    }
}
