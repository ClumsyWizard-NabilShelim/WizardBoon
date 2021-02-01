using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Play,
    Paused
}

public class GameManager : MonoBehaviour
{
    private GameState gameState = GameState.Play;

    private SceneManager sceneManager;

    [SerializeField] CameraShake cameraShake;

    private void Start()
    {
        sceneManager = GetComponentInChildren<SceneManager>();
    }

    public void LoadLevel(string scene)
    {
        sceneManager.LoadScene(scene);
    }

    public void LoadLevel()
    {
        sceneManager.LoadScene();
    }

    public void SwitchGameState(GameState state)
    {
        gameState = state;
        Time.timeScale = gameState == GameState.Paused ? 0.0f : 1.0f;
    }

    public bool CanPause()
    {
        return !(gameState == GameState.MainMenu);
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        cameraShake.ShakeCamera(duration, magnitude);
    }
}
