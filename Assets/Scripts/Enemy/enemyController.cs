using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    enemyManager em;
    public int damageGive;// how much damage an enemy gives
    // Start is called before the first frame update
    void Start()
    {
        em = enemyManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeDamage()
    {
        Instantiate(em.explosion, transform.position, transform.rotation);
        scoreManager.instance.enemiesKilled++;
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            collision.gameObject.GetComponent<playerController>().takeDamage(damageGive);
        }
    }
}
