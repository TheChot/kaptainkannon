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
            if(prerequisiteLevel != 0)
            {
                levelButton.enabled = true;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
            }

            
            
        }

        // checks for a player pref that matches this
        // and if a player pref matches then and is != 0
        // unlocks star
        string level_star_name = "level " + levelIndex + " star";

        int hasStar = PlayerPrefs.GetInt(level_star_name, 0);

        if(hasStar != 0)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
            
    }
    
}
