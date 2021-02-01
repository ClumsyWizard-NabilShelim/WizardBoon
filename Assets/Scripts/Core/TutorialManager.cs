using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> tutorialImages = new List<GameObject>();
    public int imageIndex = 0;
    Animator tutorialAnimator;
    public DialogueTrigger FightStartTrigger;
    private void Start()
    {
        tutorialAnimator = GetComponent<Animator>();
        UpdateImage();
        DialogueManager.conversationEnded += StartTutorial;

        foreach (GameObject g in tutorialImages)
        {
            g.SetActive(false);
        }
        tutorialImages[imageIndex].SetActive(true);
    }

    public void StartTutorial()
    {
        tutorialAnimator.SetBool("In", true);
    }

    public void Next()
    {
        AudioManager.instance.PlayAudio("ButtonClick");
        if (imageIndex < tutorialImages.Count - 1)
        {
            tutorialImages[imageIndex].SetActive(false);
            imageIndex++;
            UpdateImage();
        }
        else
        {
            tutorialAnimator.SetBool("In", false);
            FightStartTrigger.TriggerDialogue();
        }
    }

    public void Previous()
    {
        AudioManager.instance.PlayAudio("ButtonClick");
        if (imageIndex > 0)
        {
            tutorialImages[imageIndex].SetActive(false);
            imageIndex--;
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        tutorialImages[imageIndex].SetActive(true);
    }

    public void DestroyTutorialManager()
    {
        DialogueManager.conversationEnded -= StartTutorial;
       // Destroy(gameObject);
    }
}
