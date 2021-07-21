using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMain : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player") || collision.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            // Debug.Log("THe player collided with me");
            collision.gameObject.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player") || collision.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            // Debug.Log("THe player collided with me");
            collision.gameObject.transform.parent = null;
        }
    }
}
