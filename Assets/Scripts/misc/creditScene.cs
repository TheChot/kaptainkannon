using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditScene : MonoBehaviour
{
    public void endTheGame()
    {
        levelManager.instance.closeLevel();
    }
}
