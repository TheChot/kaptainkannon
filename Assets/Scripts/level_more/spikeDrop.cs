using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeDrop : MonoBehaviour
{
    public GameObject spike;
    Transform spikeT;

    // RaycastHit2D fall;
    bool fall;
    public float fallSpeed;

    public Transform detector;
    public float detectRange;
    public LayerMask whatIsPlayer;
    


    // Start is called before the first frame update
    void Start()
    {
        spikeT = spike.transform;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D _fall = Physics2D.Raycast(detector.position, detector.up, detectRange, whatIsPlayer);    
        
        if(_fall.collider != null)
        {
            fall = true;
        }
        
        if(fall && spike.activeInHierarchy)
        {
            spikeT.position += (transform.up * Time.fixedDeltaTime * fallSpeed); 
        }
    }

    // public void spikeFall()
    // {
        
    // }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 endPos = detector.position;
        endPos.y = detector.position.y + detectRange;
        Gizmos.DrawLine(detector.position, endPos);
        // Gizmos.DrawWireSphere(fist.position, fistRange);
    }
}
