using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    #region 
    public static mainMenu instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public GameObject titleScreen;
    public GameObject levelSelect;
    public GameObject[] levelSelects;
    int levelSelectIndex = 0;

    public Animator transition_anim;
    public int levelIndex;


    
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;            
        }
        
        levelSelects[levelSelectIndex].SetActive(true);
    }

    public void playGame()
    {
        // add code that directs to
        // tutorial first before level select
        titleScreen.SetActive(false);
        levelSelect.SetActive(true);
        levelSelectIndex = 0;
        // add code that opens the 
        // first member of the level select array
        // and an index for hoping through menus
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void backButton()
    {
        titleScreen.SetActive(true);
        levelSelect.SetActive(false);
    }

    // hand level unlocking to level transition
    // transitionMainMenu script
    public void openLevel(int level)
    {
        levelIndex = level;
        transition_anim.ResetTrigger("toLevel");
        transition_anim.SetTrigger("toLevel");
        // SceneManager.LoadScene(level);
    }

    public void nextScreen()
    {
        int ls = levelSelects.Length - 1;
        // Debug.Log(ls);

        if(levelSelectIndex < ls)
        {
            levelSelects[levelSelectIndex].SetActive(false);
            levelSelectIndex++;

        }
    }
    
    public void prevScreen()
    {
        if(levelSelectIndex > 0)
        {
            levelSelects[levelSelectIndex].SetActive(false);
            levelSelectIndex--;
        }
    }
}
