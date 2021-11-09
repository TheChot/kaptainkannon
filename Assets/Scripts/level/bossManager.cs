using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossManager : MonoBehaviour
{
    #region 
    public static bossManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public Vector2 camPos;
    public GameObject[] blockers;
    dialogueManager dm;
    public bool bossStart;
    public GameObject boss;
    public bossController bC;
    public AudioSource bgmusic;
    public AudioSource bossmusic;

    // Start is called before the first frame update
    void Start()
    {
        dm = dialogueManager.instance;
    }
    
    public void centerBossCam()
    {
        dm.cam.transform.position = new Vector3(camPos.x, camPos.y, dm.cam.transform.position.z);
        bossStart = true;
        boss.SetActive(true);
        bgmusic.Stop();
        bossmusic.Play();
        for (int i = 0; i < blockers.Length; i++)
        {
            blockers[i].SetActive(true);
        }
    }

    public void deadBoss()
    {
        bossStart = false;
        dm.cam.storyMode = false;
        bossmusic.Stop();
        bgmusic.Play();
        for (int i = 0; i < blockers.Length; i++)
        {
            blockers[i].SetActive(false);
        }
    }

    // public void bossFightStart()
}
