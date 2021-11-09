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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
