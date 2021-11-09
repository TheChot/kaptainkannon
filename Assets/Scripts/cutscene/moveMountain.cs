using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveMountain : MonoBehaviour
{
    public float speed;
    
    void FixedUpdate()
    {
        transform.position -= (new Vector3(speed, 0, 0) * Time.fixedDeltaTime);
    }
}
