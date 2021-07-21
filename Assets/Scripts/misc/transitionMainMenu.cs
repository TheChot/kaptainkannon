using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionMainMenu : MonoBehaviour
{
    public void openLevel()
    {
        SceneManager.LoadScene(mainMenu.instance.levelIndex);
    }
}
