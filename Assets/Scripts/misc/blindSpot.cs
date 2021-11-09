using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blindSpot : MonoBehaviour
{
    Transform cam;
    public Vector2 newCamPos;
    public float speed;
    bool startMove;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
    }

    void Update() 
    {
        if(startMove)
        {
            Vector3 target = new Vector3(newCamPos.x, newCamPos.y, cam.position.z);
            cam.position = Vector3.MoveTowards(cam.position, target, speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log("Player entered");
            startMove = true;
            // cam.position = new Vector3(newCamPos.x, newCamPos.y, cam.position.z);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            startMove = false;            
        }
    }
}
