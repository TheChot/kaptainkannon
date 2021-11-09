using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballDirect : MonoBehaviour
{
    Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("player").transform;
        directBullet();
    }

    // Update is called once per frame
    void directBullet()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
    }
}
