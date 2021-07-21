using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    #region 
    public static scoreManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion


    int totalEnemyCount;
    int totalSecretsCount;
    // public float totalStarTime;
    public float levelTimer;    
    public int enemiesKilled;
    public int secretsCollected;
    levelManager lm;
    checkPointManager cm;


    // Start is called before the first frame update
    void Start()
    {
        cm = checkPointManager.instance;
        enemyController[] enemiesInLevel = FindObjectsOfType<enemyController>();
        secretController[] secretsInLevel = FindObjectsOfType<secretController>();
        totalEnemyCount = enemiesInLevel.Length;
        totalSecretsCount = secretsInLevel.Length;
        lm = levelManager.instance;
        // starTimer = totalStarTime;
        
        if(cm.checkPointUsed)
        {
            levelTimer = cm.levelTime;
        }
        
        if(cm.enemiesDefeated > 0)
        {
            totalEnemyCount += cm.enemiesDefeated;
            enemiesKilled = cm.enemiesDefeated;
            for (int i = 0; i < cm.enemiesDefeated; i++)
            {
                enemiesInLevel[i].gameObject.SetActive(false);
            }
        }
        
        // Debug.Log(enemiesInLevel.Length);
        // Debug.Log(secretsInLevel.Length);
    }

    // Update is called once per frame
    void Update()
    {
        lm.allSecrets = secretsCollected == totalSecretsCount;
        lm.allEnemies = enemiesKilled == totalEnemyCount;
    }

    void FixedUpdate()
    {
        if(!lm.levelComplete)
        {
            if(lm.levelStart)
            {
                levelTimer += Time.deltaTime;
            }
            
        } 
        
    }

    
}
