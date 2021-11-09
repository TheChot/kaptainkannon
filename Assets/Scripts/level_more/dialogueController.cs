using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueController : MonoBehaviour
{
    [System.Serializable]
    public class dialogue
    {
        public GameObject charIcon;
        public string dialogueLine;
        public bool isAnim;
        public string animName;
        public bool moveCam;
        public Vector2 camPosition;
        public bool fadeToWhite;
        public bool gap;
        public float gapTime;
    }
    
    public dialogue[] dialogueLines;
    public int dialogueIndex;
    dialogueManager dm;
    public Animator dialogueAnim;
    bool isActivated;
    public bool deactivatePlayer;
    public bool movePlayer;
    public Vector2 playerEndPosition;
    public bool activateAnimObj;
    public GameObject animObj;
    public bool isAutoD;
    public bool isBossDialogue;
    

    void Start()
    {
        dm = dialogueManager.instance;
        if(isAutoD)
        {
            dm.isDialogue = true;
            dm.dc = this;
            isActivated = true;
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(isActivated)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            dm.isDialogue = true;
            dm.dc = this;
            isActivated = true;
            
        }
        
    }
}
