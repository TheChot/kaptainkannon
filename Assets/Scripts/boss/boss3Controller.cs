using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss3Controller : MonoBehaviour
{
    Animator anim;
    bossManager bM;
    bossController bC;

    public float fire1Timer;
    float timer1Reset;
    public Transform fireballPoint;
    public GameObject fireball;
    public int fireballLoop;
    int loop1Reset;
    bool hasFired;

    public GameObject shockwave;
    public float fire2Timer;
    float timer2Reset;
    public int shockLoop;
    int loop2Reset;
    public Transform shockPoint;

    public float retreatCount;
    float timer3Reset;

    public GameObject dragonWeak, dragonStrong;
    // public GameObject dragonStrong;
    public Transform dragonSpawn1, dragonSpawn2;
    public int dragonLoop = 3;
    int loop3Reset;
    public float posLeft, posRight;
    public bool isLeft;

    public Transform[] spawners1;
    public Transform[] spawners2;
    public float spawnerDelay;
    public float spawnerGap;
    public float spawnDragonsTime;
    public int rainLoop;
    public GameObject fireballMk2;
    // bool spawnDragons;
    bool switchD;


    void Start()
    {
        anim = GetComponent<Animator>(); 
        bC = GetComponent<bossController>();
        bM = bossManager.instance;
        timer1Reset = fire1Timer;
        timer2Reset = fire2Timer;
        timer3Reset = retreatCount;
        loop1Reset = fireballLoop;
        loop2Reset = shockLoop;
        loop3Reset = dragonLoop;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bM.bossStart)
        {
            phase1();
            
        }
    }

    void phase1()
    {
        if(fireballLoop > 0 && !hasFired)
        {
            fire1Timer -= Time.deltaTime;
            if(fire1Timer < 0)
            {
                hasFired = true;
                anim.SetTrigger("fire ball");
            }
        }

        if(fireballLoop <= 0 && !hasFired)
        {
            fire2Timer -= Time.deltaTime;
            if(fire2Timer < 0 && shockLoop > 0)
            {
                hasFired = true;
                anim.SetTrigger("slap_tail");
            }
        }

        if(fireballLoop <= 0 && shockLoop <= 0 && !hasFired)
        {
            retreatCount -= Time.deltaTime;

            if(retreatCount <= 0)
            {
                // if(!bC.phase2)
                // {
                    
                // }
                hasFired = true;                
                anim.SetBool("retreat", true);
            }            
        }
    }

    public void fireFireBall()
    {
        GameObject _fireball = (GameObject)Instantiate(fireball, fireballPoint.position, fireballPoint.rotation);
        fireballLoop -= 1;
        fire1Timer = timer1Reset;
        hasFired = false;        
    }

    public void slapTail()
    {
          
        if(!isLeft)
        {
            GameObject _shockwave = (GameObject)Instantiate(shockwave, shockPoint.position, shockPoint.rotation);
            
        } else 
        {
            GameObject _shockwave = (GameObject)Instantiate(shockwave, shockPoint.position, shockPoint.rotation);
            _shockwave.GetComponent<shockwave>().moveLeft = false;
        }  

        shockLoop -= 1;
        fire2Timer = timer2Reset;
        hasFired = false;    
    }

    public void spawnDragon()
    {
        bC.exposeWeak = true;
        if(!bC.phase2)
        {                
            if(!isLeft)
            {
                GameObject _dragon = (GameObject)Instantiate(dragonWeak, dragonSpawn2.position, dragonSpawn2.rotation);
                dragon dg = _dragon.GetComponent<dragon>();
                dg.bC3 = this;
                dg.moveLeft = true;
            } else 
            {
                GameObject _dragon = (GameObject)Instantiate(dragonWeak, dragonSpawn1.position, dragonSpawn2.rotation);
                dragon dg = _dragon.GetComponent<dragon>();
                dg.bC3 = this;
            }
        } else
        {
            // if(!spawnDragons)
            // {
            //     StartCoroutine(FireRain());
            // }
            StartCoroutine(FireRain());
        }
    }
    public void advanceDragon()
    {
        isLeft = !isLeft;
        if(!isLeft)
        {
            transform.position = new Vector3(posRight, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(1, 1, 1);
        } else 
        {
            transform.position = new Vector3(posLeft, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        anim.SetBool("advance", true);
        anim.SetBool("retreat", false);
    }

    public void resetDragon()
    {
        fireballLoop = loop1Reset;
        shockLoop = loop2Reset;
        hasFired = false;
        retreatCount = timer3Reset;
        anim.SetBool("advance", false);
        dragonLoop = loop3Reset;
        switchD = false;
    }

    IEnumerator FireRain()
    {
        yield return new WaitForSeconds(spawnerDelay);
        for (int i = 0; i < rainLoop; i++)
        {
            if(i == 0)
            {
                for (int j = 0; j < spawners1.Length; j++)
                {
                    GameObject _fireball = (GameObject)Instantiate(fireballMk2, spawners1[j].position, spawners1[j].rotation);
                }
            }
            yield return new WaitForSeconds(spawnerGap);
            if(i != 0)
            {
                if(i%2 != 0)
                {
                    for (int j = 0; j < spawners2.Length; j++)
                    {
                        GameObject _fireball = (GameObject)Instantiate(fireballMk2, spawners2[j].position, spawners2[j].rotation);
                    }
                    
                    
                } else 
                {
                    for (int j = 0; j < spawners1.Length; j++)
                    {
                        GameObject _fireball = (GameObject)Instantiate(fireballMk2, spawners1[j].position, spawners1[j].rotation);
                    }
                }
            }
        }
        yield return new WaitForSeconds(spawnDragonsTime);
        // spawnDragons = true;
        spawnNextDragon();

    }

    public void spawnNextDragon()
    {
        if(!isLeft)
        {
            if(dragonLoop > 0)
            {
                if(!switchD)
                {
                    GameObject _dragon = (GameObject)Instantiate(dragonStrong, dragonSpawn2.position, dragonSpawn2.rotation);
                    dragon dg = _dragon.GetComponent<dragon>();
                    dg.bC3 = this;
                    dg.moveLeft = true;
                    switchD = true;

                } else 
                {
                    GameObject _dragon = (GameObject)Instantiate(dragonStrong, dragonSpawn1.position, dragonSpawn2.rotation);
                    dragon dg = _dragon.GetComponent<dragon>();
                    dg.bC3 = this;
                    switchD = false;
                }
            } else 
            {
                if(!switchD)
                {
                    GameObject _dragon = (GameObject)Instantiate(dragonWeak, dragonSpawn2.position, dragonSpawn2.rotation);
                    dragon dg = _dragon.GetComponent<dragon>();
                    dg.bC3 = this;
                    dg.moveLeft = true;
                    switchD = true;

                } else 
                {
                    GameObject _dragon = (GameObject)Instantiate(dragonWeak, dragonSpawn1.position, dragonSpawn2.rotation);
                    dragon dg = _dragon.GetComponent<dragon>();
                    dg.bC3 = this;
                    switchD = false;
                }
            }

            dragonLoop--;
            
        } else 
        {
            if(dragonLoop > 0)
            {
                if(!switchD)
                {
                    GameObject _dragon = (GameObject)Instantiate(dragonStrong, dragonSpawn1.position, dragonSpawn2.rotation);
                    dragon dg = _dragon.GetComponent<dragon>();
                    dg.bC3 = this;
                    switchD = true;
                } else 
                {
                    GameObject _dragon = (GameObject)Instantiate(dragonStrong, dragonSpawn2.position, dragonSpawn2.rotation);
                    dragon dg = _dragon.GetComponent<dragon>();
                    dg.bC3 = this;
                    dg.moveLeft = true;
                    switchD = false;                    
                }

            } else 
            {
                if(!switchD)
                {
                    GameObject _dragon = (GameObject)Instantiate(dragonWeak, dragonSpawn1.position, dragonSpawn2.rotation);
                    dragon dg = _dragon.GetComponent<dragon>();
                    dg.bC3 = this;
                    switchD = true;
                } else 
                {
                    GameObject _dragon = (GameObject)Instantiate(dragonWeak, dragonSpawn2.position, dragonSpawn2.rotation);
                    dragon dg = _dragon.GetComponent<dragon>();
                    dg.bC3 = this;
                    dg.moveLeft = true;
                    switchD = false;
                    
                }
            }
            dragonLoop--;
        }
    }
}
