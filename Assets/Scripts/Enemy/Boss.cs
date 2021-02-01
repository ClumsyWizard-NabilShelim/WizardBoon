using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public DialogueTrigger dialogue;

    public void Laugh()
    {
        StartCoroutine(WaitBeforeLaugh());
    }

    IEnumerator WaitBeforeLaugh()
    {
        yield return new WaitForSeconds(2.0f);
        dialogue.TriggerDialogue();
    }
}
