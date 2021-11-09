using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudMover : MonoBehaviour
{
    public float topSpeed = 5f;
    float speed;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        float size = Random.Range(0.3f, 1.0f);
        transform.localScale = new Vector3(size, size, 1);
        speed = Random.Range(0.5f, topSpeed);

        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(255f, 255f, 255f, Random.Range(0.4f, 0.9f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += (new Vector3(-speed, 0, 0) * Time.fixedDeltaTime);
    }
}
