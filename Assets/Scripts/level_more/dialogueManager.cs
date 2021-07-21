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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDialogue)
        {
            levelManager.instance.playerHud.SetActive(false);
            if(dc.dialogueIndex == 0)
            {
                if(dc.dialogueLines[0].isAnim)
                {
                    dc.dialogueAnim.SetBool(dc.dialogueLines[0].animName, true);
                } else 
                {
                    dialogueArea.SetActive(true);
                    dText.text = dc.dialogueLines[0].dialogueLine;
                    Instantiate(dc.dialogueLines[0].charIcon, charPoint, false);
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
        } else 
        {
            dialogueArea.SetActive(false);            
            dialogueArea.SetActive(true);
            Instantiate(dc.dialogueLines[_dIndex].charIcon, charPoint, false);
            dText.text = dc.dialogueLines[_dIndex].dialogueLine;
        }

    }

    public void endDialogue()
    {
        isDialogue = false;
        dc = null;
        dialogueArea.SetActive(false);
        levelManager.instance.playerHud.SetActive(true);
    }
}
