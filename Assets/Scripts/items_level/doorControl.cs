using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorControl : MonoBehaviour
{
    public float speed;
    public Transform doorTrans;
    public bool activateDoor;
    public bool doorUp;

    public Transform disappearPoint;

    // public GameObject[] doorLight;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(activateDoor)
        {
            // for (int i = 0; i < doorLight.Length; i++)
            // {
            //     doorLight[i].SetActive(true);
            // }          
            
            if(doorUp)
            {
                doorTrans.position += (new Vector3(0, speed, 0) * Time.fixedDeltaTime);
            } else 
            {
                doorTrans.position += (new Vector3(0, -speed, 0) * Time.fixedDeltaTime);
            }
        }

        if(doorUp)
        {
            if(doorTrans.position.y >= disappearPoint.position.y)
            {
                doorTrans.gameObject.SetActive(false);
            }
        } else 
        {
            if(doorTrans.position.y <= disappearPoint.position.y)
            {
                doorTrans.gameObject.SetActive(false);
            }
        }
    }
}
