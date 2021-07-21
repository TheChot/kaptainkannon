using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animDialogue : MonoBehaviour
{
    
    public void endDialogue()
    {
        dialogueManager.instance.nextDialogue();
    }
}
