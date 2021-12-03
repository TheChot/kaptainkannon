using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelIcon : MonoBehaviour
{
    public int levelIndex; // build index of level
    public int levelPre; //the requisite level
    public bool noPre; // for levels with no prereq
    Button levelButton;
    public GameObject lockItem;
    public GameObject levelNum;
    public GameObject star;
    
    // Start is called before the first frame update
    void Start()
    {
        // checks for a player pref that matches this
        // and if a player pref matches then and is != 0
        // unlocks level
        string level_complete_name = "level " + levelPre + " complete";
        levelButton = GetComponent<Button>();
        
        if(!noPre)
        {
            
            int prerequisiteLevel = PlayerPrefs.GetInt(level_complete_name, 0);
            // Debug.Log(level_complete_name + " " + prerequisiteLevel.ToString());
            if(prerequisiteLevel != 0)
            {
                levelButton.enabled = true;
                levelNum.SetActive(true);
                lockItem.SetActive(false);
            } else 
            {
                levelNum.SetActive(false);
                lockItem.SetActive(true);
                levelButton.enabled = false;
            }

            
            
        } else 
        {
            levelNum.SetActive(true);
            lockItem.SetActive(false);
            levelButton.enabled = true;
        }

        // checks for a player pref that matches this
        // and if a player pref matches then and is != 0
        // unlocks star
        string level_star_name = "level " + levelIndex + " star";

        int hasStar = PlayerPrefs.GetInt(level_star_name, 0);

        if(hasStar != 0)
        {
            star.SetActive(true);
        } else 
        {
            star.SetActive(false);
        }
            
    }
    
}
