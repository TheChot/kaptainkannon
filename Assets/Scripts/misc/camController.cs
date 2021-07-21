using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camController : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    public Transform right;
    public Transform left;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.y > top.position.y)
        {
            float distance = player.position.y - top.position.y;
            transform.position = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
        }

        if(player.position.y < bottom.position.y)
        {
            float distance = bottom.position.y - player.position.y;
            transform.position = new Vector3(transform.position.x, transform.position.y - distance, transform.position.z);
        }

        if(player.position.x > right.position.x)
        {
            float distance = player.position.x - right.position.x;
            transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        }

        if(player.position.x < left.position.x)
        {
            float distance = left.position.x - player.position.x;
            transform.position = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
        }
    }
}
