using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    #region 
    public static soundManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public bool isSound;

    // Start is called before the first frame update
    void Start()
    {
        isSound = PlayerPrefs.GetInt("isSound", 0) > 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
