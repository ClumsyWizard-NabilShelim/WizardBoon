using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public string mainMenuSceneName;
    GameManager GM;

    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    public void MainMenu()
    {
        AudioManager.instance.PlayAudio("ButtonClick");
        GM.SwitchGameState(GameState.MainMenu);
        GM.LoadLevel(mainMenuSceneName);
    }

    public void Restart()
    {
        AudioManager.instance.PlayAudio("ButtonClick");
        AudioManager.instance.PlayAudio("Theme");
        GM.SwitchGameState(GameState.Play);
        GM.LoadLevel(DataSaver.instance.levelName);
    }
}
