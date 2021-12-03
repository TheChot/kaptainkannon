using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudSpawner : MonoBehaviour
{
    public float timer;
    float resetTimer;
    // public float minPoint, maxPoint;
    public GameObject cloud;
    // Start is called before the first frame update
    void Start()
    {
        resetTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = resetTimer;
            GameObject _cloud = (GameObject)Instantiate(cloud, transform.position, transform.rotation);
            // transform.position = new Vector3(transform.position.x, Random.Range(minPoint,maxPoint), transform.position.z);
        }
    }
}
