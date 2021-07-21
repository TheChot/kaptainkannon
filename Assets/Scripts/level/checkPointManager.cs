using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointManager : MonoBehaviour
{
    #region 
    public static checkPointManager instance;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } 
        else
        {
            Destroy(gameObject);
        }
        
    }
    #endregion
    
    // 1 check point system 
    public Vector2 lastCheckPointPos;
    public bool checkPointUsed;
    // and 1 secret
    public bool secretCollected;
    public float levelTime;
    public int enemiesDefeated;
    public int checkPointIndex;
    public int deadCount;
     
    
}
