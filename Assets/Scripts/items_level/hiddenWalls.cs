using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hiddenWalls : MonoBehaviour
{
    public Animator[] anim;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            for (int i = 0; i < anim.Length; i++)
            {
                anim[i].SetTrigger("disappear");
            }
        }
    }
    
}
