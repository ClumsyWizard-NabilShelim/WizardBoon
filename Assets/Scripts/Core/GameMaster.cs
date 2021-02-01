using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public enum Turn
{
    Player,
    Enemy
}

public class GameMaster : MonoBehaviour
{
    public bool lastLevel = false;

    public static System.Action<float> updateCardValues;

    public Turn gameTurn = Turn.Player;

    [Header("Enemies")]

    [HideInInspector] public List<EnemyAI> enemies = new List<EnemyAI>();
    public Transform enemyHolder;

    bool updateEnemy;
    int updatedEnemyIndex = 0;

    GameManager GM;

    [Header("Player")]
    public TextMeshProUGUI actionText;
    PlayerStats playerStats;
    Deck deck;
    bool drawCard = false;
    public int playerActionsAmount;
    int playerActions;

    float confusionDebuffPercentage;
    float damageIncreasePercentage;
    float movementRangeIncreasePercentage;

    bool gameWon;
    bool changeScene;

    public Animator playerAnimator;

    [HideInInspector] public int deadCount;
    private void Start()
    {
        ResetGameState();
        GM = FindObjectOfType<GameManager>();
        deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        for (int i = 0; i < enemyHolder.childCount; i++)
        {
            enemies.Add(enemyHolder.GetChild(i).GetComponent<EnemyAI>());
        }
    }

    private void Update()
    {
        if (deadCount == enemies.Count && !gameWon)
        {
            gameWon = true;
            PlayerWon();
            gameTurn = Turn.Player;
            return;
        }

        if (gameTurn == Turn.Enemy)
        {
            drawCard = true;
            if(updateEnemy)
            {
                updateEnemy = false;
                if (!enemies[updatedEnemyIndex].enemyStats.dead)
                    enemies[updatedEnemyIndex].ActivateEnemy();
                else
                    EnemyUpdateDone();
            }
        }
        else
        {
            if(drawCard)
            {
                UpdateCardValues();
                drawCard = false;
                deck.Draw();
            }
        }
    }

    void PlayerWon()
    {
        GM.LoadLevel();
    }

    public void PlayerLost()
    {
        if(!lastLevel)
        {
            AudioManager.instance.StopAudio("Theme");
            AudioManager.instance.PlayAudio("GameOver");
            DataSaver.instance.levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            GM.LoadLevel("GameOver");
        }
        else
        {
            FindObjectOfType<Boss>().Laugh();
            DialogueManager.conversationEnded += EndGame;
        }
    }

    void EndGame()
    {
        GM.LoadLevel("EndScene");
    }

    public void EnemyUpdateDone()
    {
        enemies[updatedEnemyIndex].DeactivateEnemy();
        if (updatedEnemyIndex >= enemies.Count - 1)
        {
            ResetGameState();
            return;
        }
        else
        {
            updatedEnemyIndex++;
            updateEnemy = true;
        }
    }

    void ResetGameState()
    {
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        playerAnimator.SetBool("In", true);
        yield return new WaitForSeconds(1.0f);
        gameTurn = Turn.Player;
        playerActions = playerActionsAmount;
        UpdateActionTextUI();
        updatedEnemyIndex = 0;
        updateEnemy = true;
    }

    public void UseAction(int amount = 1)
    {
        playerActions -= amount;
        UpdateActionTextUI();
        if (playerActions <= 0)
        {
            gameTurn = Turn.Enemy;
            StartCoroutine(EnemyTurn());
        }
    }

    void UpdateActionTextUI()
    {
        actionText.text = "x" + playerActions.ToString();
    }

    public void UpdateCardValues()
    {
        float count = deck.transform.childCount - deck.startingHandCardNum;
        if(deck.transform.childCount > 0)
        {
            confusionDebuffPercentage = (count / 10);
            updateCardValues(confusionDebuffPercentage);
        }
    }

    public void EndPlayerTurn()
    {
        AudioManager.instance.PlayAudio("ButtonClick");
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        playerAnimator.SetBool("In", false);
        yield return new WaitForSeconds(1.0f);
        gameTurn = Turn.Enemy;
    }

    public bool HasEnoughActionsLeft(int amount)
    {
        return !(playerActions < amount);
    }
}
