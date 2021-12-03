using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingGround : MonoBehaviour
{
    public GameObject stableFloor;
    public GameObject warningFloor;
    public GameObject warningFloorLast;
    public GameObject dust;
    public float warningWait;
    public float warningMid;
    public float warningWaitLast;
    Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        warningFloor.SetActive(false);
        warningFloorLast.SetActive(false);
        col = GetComponent<Collider2D>();
    }    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            StartCoroutine(breakFloor());
        }
        
    }

    IEnumerator breakFloor()
    {
        yield return new WaitForSeconds(warningWait);
        stableFloor.SetActive(false);
        warningFloor.SetActive(true);
        yield return new WaitForSeconds(warningMid);
        warningFloor.SetActive(false);
        warningFloorLast.SetActive(true);
        yield return new WaitForSeconds(warningWaitLast);
        warningFloorLast.SetActive(false);
        dust.SetActive(true);
        col.enabled = false;
        yield return new WaitForSeconds(warningWaitLast);
        // gameObject.SetActive(false);
        Destroy(gameObject);
        
    }
}
