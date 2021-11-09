using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteTrans : MonoBehaviour
{
    public dialogueManager dm;

    public void nextD()
    {
        dm.nextDialogue();
    }
}
