using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickup : MonoBehaviour
{
    public GameObject secretExplosion;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            other.gameObject.GetComponent<gunController>().shotGunEquipped = true;
            GameObject _explosion = (GameObject)Instantiate(secretExplosion, transform.position, transform.rotation);
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    } 
}
