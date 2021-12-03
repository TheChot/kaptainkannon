using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // public GameObject ;
    public int health; 
    levelManager lm;
    public GameObject[] healthIcons;
    public float bounce;
    playerMovement pM;
    public bool godMode;
    public SpriteRenderer sr;
    public int blinkCount = 5;
    public float blinkTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        lm = levelManager.instance;
        pM = GetComponent<playerMovement>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            if(health > i){
                healthIcons[i].SetActive(true);
            } else{
                healthIcons[i].SetActive(false);
            }
        }
        
        if(health < 0)
        {
            lm.isDead = true;
            // gameObject.SetActive(false);
        }
    }

    public void takeDamage(int dmg)
    {
        // gameObject.SetActive(false);
        if(!godMode){
            health -= dmg;
        }
        StartCoroutine(playerHurt());
        pM.hurtRecoil(bounce);
    }

    public void turnOnGod()
    {
        godMode = !godMode;
    }

    IEnumerator playerHurt()
    {
        
        for (int i = 0; i < blinkCount; i++)
        {
            yield return new WaitForSeconds(blinkTime);
            if (sr.color == Color.red)
            {
                sr.color = Color.white;
            }
            else
            {
                sr.color = Color.red;
            }    
        }

        sr.color = Color.white;
             
    }
}
