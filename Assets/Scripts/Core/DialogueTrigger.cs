using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool startOnStart;
    private void Start()
    {
        if(startOnStart)
            TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartConversation(dialogue);
    }
}
