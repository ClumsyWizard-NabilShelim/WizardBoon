using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public Animator endTrap;
    void Start()
    {
        DialogueManager.conversationEnded += KillPlayer;
    }

    void KillPlayer()
    {
        endTrap.SetTrigger("Play");
    }
}
