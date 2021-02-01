using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemyStats : MonoBehaviour
{
    public float damage;

    public float healthAmount;
    float health;

    public int defenceIncreaseAmount;
    private int defence;

    [HideInInspector] public EnemyAI enemyAI;
    public Image healthBar;
    public GameObject defenceBar;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI defenceText;

    public Image intentionImage;
    public Sprite attackIntention;
    public Sprite defencekIntention;

    public GameObject hurtEffect;
    public GameObject deatEffect;

    public GameObject statsCanvas;

    [Header("Effects")]
    bool freeze;
    float freezeTurnCountDown;
    Effect freezeEffect;

    public bool burn;
    float burnTurnCountDown;
    Effect burnEffect;

    public Animator intentionAnimator;
    public TextMeshProUGUI intentionText;

    [HideInInspector] public bool dead;
    GameManager GM;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        health = healthAmount;
        UpdateUI();
        CloseIntention();
        GM = FindObjectOfType<GameManager>();
    }

    public void Damage(float damage)
    {
        if (defence > 0)
        {
            defence -= (int)damage;
            if (defence < 0)
                health += defence;
        }
        else
        {
            health -= damage;
        }

        GM.ShakeCamera(0.2f, 0.5f);
        AudioManager.instance.PlayAudio("EnemyHurt");
        Instantiate(hurtEffect, transform.position, Quaternion.identity);
        if (health <= 0)
        {
            Death();
        }

        UpdateUI();
    }

    void Death()
    {
        Instantiate(deatEffect, transform.position, Quaternion.identity);
        if(enemyAI.selected)
            enemyAI.gameMaster.EnemyUpdateDone();

        dead = true;
        enemyAI.gameMaster.deadCount++;
        gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        healthBar.fillAmount = health / healthAmount;
        healthText.text = health.ToString() + " / " + healthAmount.ToString();
        if(defence > 0)
        {
            defenceBar.SetActive(true);
            defenceText.enabled = true;
            defenceText.text = defence.ToString();
        }
        else
        {
            defenceBar.SetActive(false);
            defenceText.enabled = false;
        }
    }

    public void ShowIntention()
    {
        statsCanvas.SetActive(true);

        if (enemyAI.intention == Intention.Attack)
        {
            intentionImage.sprite = attackIntention;
        }
        else
        {
            intentionImage.sprite = defencekIntention;
        }
    }

    public void CloseIntention()
    {
        statsCanvas.SetActive(false);
    }

    public void IncreaseDefence()
    {
        if (enemyAI.intention == Intention.Defend)
        {
            ShowPopUp("Block Added");
        }

        int minValue = (int)(defenceIncreaseAmount - (defenceIncreaseAmount / 2.0f));
        int maxValue = (int)(defenceIncreaseAmount + (defenceIncreaseAmount / 2.0f));

        float amount = Random.Range(minValue, maxValue);
        defence += (int)amount;
        UpdateUI();
    }


    void ShowPopUp(string text)
    {
        intentionText.text = text;

        if (intentionAnimator != null)
            intentionAnimator.SetTrigger("PopIn");
    }
}
