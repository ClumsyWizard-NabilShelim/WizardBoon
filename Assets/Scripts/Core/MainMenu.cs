using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string PlaySceneName;
    GameManager GM;

    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        GM.SwitchGameState(GameState.MainMenu);
    }

    public void Play()
    {
        AudioManager.instance.PlayAudio("ButtonClick");
        GM.SwitchGameState(GameState.Play);
        GM.LoadLevel(PlaySceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
