using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public Animator transitionMenuAnimator;

    public void LoadScene(string scene)
    {
        StartCoroutine(Load(scene));
    }

    public void LoadScene()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load(string scene)
    {
        AudioManager.instance.PlayAudio("Transition");
        transitionMenuAnimator.SetTrigger("LoadScene");
        yield return new WaitForSeconds(1.2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    IEnumerator Load()
    {
        AudioManager.instance.PlayAudio("Transition");
        transitionMenuAnimator.SetTrigger("LoadScene");
        yield return new WaitForSeconds(1.2f);
        int i = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(++i);
    }
}
