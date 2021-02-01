using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public string SceneName;
    GameManager GM;

    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    public void MainMenu()
    {
        AudioManager.instance.PlayAudio("ButtonClick");
        GM.SwitchGameState(GameState.MainMenu);
        GM.LoadLevel(SceneName);
    }
}
