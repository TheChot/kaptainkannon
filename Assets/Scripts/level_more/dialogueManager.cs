using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    #region 
    public static dialogueManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public bool isDialogue;
    public dialogueController dc;

    public GameObject dialogueArea;
    public Transform charPoint;
    public Text dText;
    public camController cam;
    public GameObject thePlayer;
    public float camDelay = 1f;
    float camDelayReset;
    bool camCount;

    bool startGap;
    float gapCount;

    public Animator whiteTrans;
    bool fadeWhite;

    GameObject tempIcon;

    public bossManager bM;

    // Start is called before the first frame update
    void Start()
    {
        camDelayReset = camDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDialogue)
        {
            levelManager.instance.playerHud.SetActive(false);
            cam.storyMode = true;
            if(dc.deactivatePlayer)
            {
                thePlayer.gameObject.SetActive(false);
            }
            // activates a gameobject that has been animated
            if(dc.activateAnimObj)
            {
                dc.animObj.SetActive(true);
            }
            


            if(dc.dialogueIndex == 0)
            {
                if(dc.dialogueLines[0].isAnim)
                {
                    dc.dialogueAnim.SetBool(dc.dialogueLines[0].animName, true);
                } else if(dc.dialogueLines[0].moveCam)
                {
                    cam.transform.position = new Vector3(dc.dialogueLines[0].camPosition.x, dc.dialogueLines[0].camPosition.y, cam.transform.position.z);
                    camCount = true;
                } else if(dc.dialogueLines[0].gap)
                {
                    startGap = true;
                    gapCount = dc.dialogueLines[0].gapTime;
                } else if(dc.dialogueLines[0].fadeToWhite)
                {
                    fadeWhite = !fadeWhite;
                    whiteTrans.SetBool("fade baby", fadeWhite);
                } else
                {
                    dialogueArea.SetActive(true);
                    dText.text = dc.dialogueLines[0].dialogueLine;
                    Instantiate(dc.dialogueLines[0].charIcon, charPoint, false);
                }
            }

            if(camCount)
            {
                camDelay -= Time.deltaTime;
                if(camDelay <= 0)
                {
                    camCount = false;
                    camDelay = camDelayReset;
                    nextDialogue();
                    
                }
            }

            if(startGap)
            {
                gapCount -= Time.deltaTime;
                if(gapCount < 0)
                {
                    startGap = false;
                    nextDialogue();
                }
            }
            
        } else
        {
            if(dc != null)
            {
                dialogueArea.SetActive(false);
                levelManager.instance.playerHud.SetActive(true);
            }
        }
    }

    public void nextDialogue()
    {   
        
        if(dc == null)
            return;

        dc.dialogueIndex++;
        int _dIndex = dc.dialogueIndex;        
        // dc.dialogueLines[_dIndex].dialogueLine

        if(_dIndex >= dc.dialogueLines.Length)
        {
            endDialogue();
            return;
        }

        if(dc.dialogueLines[_dIndex].isAnim)
        {
            dialogueArea.SetActive(false);
            dc.dialogueAnim.SetBool(dc.dialogueLines[_dIndex].animName, true);
        } else if(dc.dialogueLines[_dIndex].moveCam)
        {
            dialogueArea.SetActive(false);
            cam.transform.position = new Vector3(dc.dialogueLines[_dIndex].camPosition.x, dc.dialogueLines[_dIndex].camPosition.y, cam.transform.position.z);
            camCount = true;
        }else if(dc.dialogueLines[_dIndex].gap)
        {
            dialogueArea.SetActive(false);
            gapCount = dc.dialogueLines[_dIndex].gapTime;
            startGap = true;
        }else if(dc.dialogueLines[_dIndex].fadeToWhite)
        {
            dialogueArea.SetActive(false);
            fadeWhite = !fadeWhite;
            whiteTrans.SetBool("fade baby", fadeWhite);
        } else 
        {
            if(tempIcon != null)
            {
                Destroy(tempIcon);
                tempIcon = null;
            }
            dialogueArea.SetActive(false);            
            dialogueArea.SetActive(true);
            tempIcon = (GameObject)Instantiate(dc.dialogueLines[_dIndex].charIcon, charPoint, false);
            dText.text = dc.dialogueLines[_dIndex].dialogueLine;
        }

    }

    public void endDialogue()
    {
        if(dc.movePlayer)
        {
            thePlayer.transform.position = new Vector3(dc.playerEndPosition.x, dc.playerEndPosition.y, thePlayer.transform.position.z);
        }

        if(dc.deactivatePlayer)
        {
            thePlayer.gameObject.SetActive(true);
        }
        if(dc.activateAnimObj)
        {
            dc.animObj.SetActive(false);
        }

        if(!dc.isBossDialogue)
        {
            cam.storyMode = false;
        } else 
        {
            bM.centerBossCam();
        }
        
        isDialogue = false;
        dc = null;
        dialogueArea.SetActive(false);
        levelManager.instance.playerHud.SetActive(true);
        
    }

    // IEnumerator moveCam()
    // {
    //     yield return new WaitForSeconds(camDelay);
    //     nextDialogue();
    //     StopCoroutine(moveCam());
    // }
}
