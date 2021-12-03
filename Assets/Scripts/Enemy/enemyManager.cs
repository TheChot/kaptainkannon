using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    // mock singleton
    #region 
    public static enemyManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public GameObject explosion;
    public GameObject bomberExplosion;
    public playerController pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
