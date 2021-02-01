using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using System;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image speakerImage;
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;
    Queue<DialogueInfo> sentences = new Queue<DialogueInfo>();

    public static Action conversationEnded;
    public Animator dialogueAnimator;

    private void Start()
    {
        dialogueAnimator = GetComponent<Animator>();
    }

    public void StartConversation(Dialogue dialogue)
    {
        dialogueAnimator.SetBool("In", true);
        speakerImage.enabled = true;
        sentences.Clear();
        foreach (DialogueInfo s in dialogue.dialogues)
        {
            sentences.Enqueue(s);
        }

        NextDialogue();
    }

    public void NextDialogue()
    {
        if(sentences.Count == 0)
        {
            EndConversation();
            return;
        }

        AudioManager.instance.PlayAudio("ButtonClick");
        DialogueInfo text = sentences.Dequeue();
        StopAllCoroutines();
        speakerImage.sprite = text.speakerImage;
        speakerNameText.text = text.speakerName;
        StartCoroutine(TypeOutText(text.dialogue));
    }

    IEnumerator TypeOutText(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void EndConversation()
    {
        conversationEnded?.Invoke();
        dialogueAnimator.SetBool("In", false);
        speakerImage.enabled = false;
    }
}
