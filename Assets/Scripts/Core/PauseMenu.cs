using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool animDone = true;
    bool paused = false;
    public Animator pauseMenuAnimator;
    GameManager GM;

    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    void Pause()
    {
        if (GM.CanPause() && animDone)
        {
            AudioManager.instance.PlayAudio("ButtonClick");
            paused = true;
            animDone = false;
            pauseMenuAnimator.SetBool("PauseGame", true);
            GM.SwitchGameState(GameState.Paused);
        }
    }

    public void Resume()
    {
        if (animDone)
        {
            AudioManager.instance.PlayAudio("ButtonClick");
            paused = false;
            animDone = false;
            pauseMenuAnimator.SetBool("PauseGame", false);
            GM.SwitchGameState(GameState.Play);
        }
    }

    public void MainMenu()
    {
        AudioManager.instance.PlayAudio("ButtonClick");
        GM.SwitchGameState(GameState.MainMenu);
        GM.LoadLevel("MainMenu");
    }

    public void AnimDone()
    {
        animDone = true;
    }
}
