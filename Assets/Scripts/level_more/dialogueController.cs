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
    }
    
    public dialogue[] dialogueLines;
    public int dialogueIndex;
    dialogueManager dm;
    public Animator dialogueAnim;
    bool isActivated;

    void Start()
    {
        dm = dialogueManager.instance;
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
