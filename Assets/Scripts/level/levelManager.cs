using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
    #region 
    public static levelManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public GameObject pauseMenu;
    public GameObject playerHud;
    public GameObject deathMenu;
    bool isPaused;

    public bool isDead;    
    public float deathWait = 0.3f;
    // public GameObject thePlayer;

    public bool levelComplete;
    public float endLevelTime = 0.6f;
    public GameObject endLevelMenu;

    public bool allSecrets;
    public bool allEnemies;
    public bool allTime;
    public string[] scoreDesc;
    public Text scoreText;
    public GameObject[] stars;
    int collectedPoints = 0;
    bool starCountDone;
    public float scoreTimer;
    float scoreTimerReset;
    int starLim = 0;

    public GameObject thePlayer;
    checkPointManager cm;

    public Animator transition_anim;
    public bool levelStart;
    public adManager adMan;
    bool isAd;
    public int adCount;
    bool setAd;
    public bool isRestarting;

    bool isTrans; //called when transitioning

    bool hasPaid;

    public int deadLimit;

    // Start is called before the first frame update
    void Start()
    {
        adCount = PlayerPrefs.GetInt("ad_counter", 0);
        scoreTimerReset = scoreTimer;
        cm = checkPointManager.instance;
        if(cm.checkPointUsed)
        {
            thePlayer.transform.position = cm.lastCheckPointPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(isDead)
        {
            StartCoroutine(deathCo());
            // if(isAd)
            // {
            //     StartCoroutine(deathAd());
            // } else 
            // {
            //     StartCoroutine(deathCo());
            // }
            // showAd();       
        }

        if(levelComplete)
        {
            StartCoroutine(finishLevel());
        }

        
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        playerHud.SetActive(false);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void continueGame()
    {
        pauseMenu.SetActive(false);
        playerHud.SetActive(true);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void quitGame()
    {
        // Application.Quit();
        SceneManager.LoadScene(0);
    }

    public void restartTrans()
    {
        isRestarting = true;
        transition_anim.SetBool("level close", true);
    }

    public void restartLevel()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void nextLevel()
    {
        if((SceneManager.GetActiveScene().buildIndex + 1) < SceneManager.sceneCountInBuildSettings)
        {
            Destroy(cm.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }

    private IEnumerator deathCo()
    {
        if(!setAd)
        {
            adCount++;
            PlayerPrefs.SetInt("ad_counter", adCount);
            setAd = true;            
            isAd =  adCount > 2;
            cm.deadCount++;
        }
        
        yield return new WaitForSeconds(deathWait);        
        if(!isTrans)
        {
            if(hasPaid || cm.deadCount > deadLimit)
            {
                isAd = false;
            }
            if(isAd)
            {
                adCount = 0;
                PlayerPrefs.SetInt("ad_counter", adCount);            
                adMan.displayInterstitial();

            } else 
            {
                transition_anim.SetBool("level close", true);
            }
            isTrans = true;
        }
        
        // deathMenu.SetActive(true);        
    }

    // private IEnumerator deathAd()
    // {
    //     adCount = 0;
    //     PlayerPrefs.SetInt("ad_counter", adCount);        
    //     yield return new WaitForSeconds(deathWait);
    //     adMan.displayInterstitial();

    // }
    
    private IEnumerator finishLevel()
    {
        playerHud.SetActive(false);
        string level_complete_name = "level " + SceneManager.GetActiveScene().buildIndex.ToString() + " complete";
        PlayerPrefs.SetInt(level_complete_name, 1);
        
        if(allSecrets)
        {
            string level_star_name = "level " + SceneManager.GetActiveScene().buildIndex.ToString() + " star";
            PlayerPrefs.SetInt(level_star_name, 1);
        } 
        if(!setAd)
        {
            adCount++;
            PlayerPrefs.SetInt("ad_counter", adCount);
            setAd = true;
            isAd =  adCount > 2;
        }
        yield return new WaitForSeconds(endLevelTime);
        // endLevelMenu.SetActive(true);
        if(!isTrans)
        {            
            
            if(hasPaid || cm.deadCount > deadLimit)
            {
                isAd = false;
            }
            if(isAd)
            {
                adCount = 0;
                PlayerPrefs.SetInt("ad_counter", adCount);        
                adMan.displayInterstitial();
            } 
            else 
            {            
                transition_anim.SetBool("level close", true);
            }
            isTrans = true;
        }
        
        
        // playerHud.SetActive(false);
        // if(!starCountDone)
        // {               
        //     scoreCounter();
        // }    
        
    }

    public void startLevel()
    {
        levelStart = true;
    }

    public void closeLevel()
    {
        transition_anim.SetBool("level close", true);
    }    

}

// void scoreCounter()
//     {
//         scoreTimer -= Time.deltaTime;
//         if(scoreTimer < 0 && !starCountDone)
//         {
//             if (allSecrets && starLim == 0)
//             {
//                 scoreText.text = scoreDesc[0];
//                 stars[collectedPoints].SetActive(true);
//                 collectedPoints++;                                      
                
//             }
//             else if (allEnemies && starLim == 1)
//             {
//                 scoreText.text = scoreDesc[1];
//                 stars[collectedPoints].SetActive(true);
//                 collectedPoints++;                                      
                
//             }
//             else if (allTime && starLim == 2)
//             {
//                 scoreText.text = scoreDesc[2];
//                 stars[collectedPoints].SetActive(true);
//                 collectedPoints++;
//                 starCountDone = true;                                      
                
//             }

//             if (starLim >= 2)
//             {
//                 starCountDone = true;
//             }else 
//             {
//                 starLim++;
//                 scoreTimer = scoreTimerReset;
//             }
            
//         }
//     }

