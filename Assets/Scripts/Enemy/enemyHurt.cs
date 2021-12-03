using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHurt : MonoBehaviour
{
    public int damageGive;    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            enemyManager.instance.pc.takeDamage(damageGive);
            Debug.Log("Im hurt");
        }
    }
}
