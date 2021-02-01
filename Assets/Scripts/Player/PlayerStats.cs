using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    Animator playerAnimator;

    public float healthAmount;
    float health;

    public TextMeshProUGUI healthText;
    //public TextMeshProUGUI damageText;
    //public float armourAmount;
    //float armour;

    public Image healthBar;
    //public Image ArmourBar;

    GameMaster gameMaster;
    GameManager GM;
    PlayerManager PM;

    public int actionCost = 1;

    public GameObject hurtEffect;
    public GameObject deathEffect;

    public GameObject gfx;
    private void Start()
    {
        health = healthAmount;
        //armour = armourAmount;
        playerAnimator = GetComponent<Animator>();
        gameMaster = GameObject.FindGameObjectWithTag("PM").GetComponent<GameMaster>();
        PM = GameObject.FindGameObjectWithTag("PM").GetComponent<PlayerManager>();
        GM = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void Damage(float amount)
    {
        //if(armour > 0)
        //{
        //    armour -= amount;
        //    if (armour < 0)
        //        health += armour;
        //}
        //else
        //{

        //}

        health -= amount;
        Instantiate(hurtEffect, transform.position, Quaternion.identity);
        GM.ShakeCamera(0.2f, 0.5f);
        AudioManager.instance.PlayAudio("PlayerHit");
        playerAnimator.SetTrigger("Hurt");

        if (health <= 0)
            Death();

    }

    void UpdateUI()
    {
        healthBar.fillAmount = health / healthAmount;
        healthText.text = health.ToString() + " / " + healthAmount.ToString();
        //damageText.text = PM.GetDamage().ToString();
    }
    void Death()
    {
        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        gameMaster.PlayerLost();
    }

    public float HealthDifference()
    {
        return healthAmount - health;
    }

    public void sacrificeHealth()
    {
        AudioManager.instance.PlayAudio("ButtonClick");
        if (gameMaster.HasEnoughActionsLeft(actionCost))
        {
            Damage(20);
            gameMaster.UseAction();
            float diff = HealthDifference() / 100.0f;
            PM.UpdateDamage(diff);
        }
    }

    public void Heal(float amount)
    {
        AudioManager.instance.PlayAudio("Heal");
        health += amount;
        PM.ShowPopUp("Healed");
    }
}
