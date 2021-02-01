using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public enum PlayerMode
{
    Move,
    Attack
}
public class PlayerManager : MonoBehaviour
{
    public PlayerMode playerMode = PlayerMode.Move;
    [SerializeField] private float playerDamage;
    private float damage;
    Card cardToUse;

    [HideInInspector] public GameMaster gameMaster;
    PlayerController playerController;
    Transform playerTransform;
    GridSquare selectedGridSquare;
    public LayerMask hitAble;

    public int actionCost = 1;

    [Header("Cursor")]
    bool pointerOverUI = false;

    Camera cam;

    bool detectSelectableSquare;

    Collider2D[] hit;
    public float selectableRange;
    List<GridSquare> selectableTiles = new List<GridSquare>();

    GridSquare hoveredSquare;

    bool playerSelected = false;

    public TextMeshProUGUI popUpText;
    Animator popUpAnimator;

    [Header("Enemy intention")]
    public LayerMask enemyLayer;
    EnemyStats stat;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("PM").GetComponent<GameMaster>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.PM = this;
        playerTransform = playerController.transform;

        detectSelectableSquare = false;

        cam = Camera.main;

        GameMaster.updateCardValues += updateCardValues;
        damage = playerDamage;
        popUpAnimator = popUpText.GetComponentInParent<Animator>();
    }

    private void Update()
    {
        UpdateCursor();

        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = Physics2D.OverlapCircle(pos, 0.5f, enemyLayer);
        if(col != null)
        {
            if (stat == null)
            {
                Debug.Log("show intention");
                stat = col.GetComponent<EnemyStats>();
                stat.ShowIntention();
            }
        }
        else
        {
            if (stat != null)
            {
                Debug.Log("close out intention");
                stat.CloseIntention();
                stat = null;
            }
        }

        if (detectSelectableSquare)
        {
            if (playerMode == PlayerMode.Move)
            {
                selectableRange = playerController.moveAbleRange;
            }
            
            hit = Physics2D.OverlapCircleAll(playerTransform.position, selectableRange, hitAble);
            if (hit.Length != 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    GridSquare square = hit[i].GetComponent<GridSquare>();
                    if (!square.hasPlayer)
                    {
                        selectableTiles.Add(square);

                        if (playerMode == PlayerMode.Move)
                            square.ChangeColor(ColorType.Selection);
                        else if (playerMode == PlayerMode.Attack)
                            square.ChangeColor(ColorType.Danger);

                    }
                }
                detectSelectableSquare = false;
            }
        }
    }
    void UpdateCursor()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            pointerOverUI = true;
        }
        else
        {
            pointerOverUI = false;
        }

        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (!pointerOverUI)
        {
            Collider2D col = Physics2D.OverlapCircle(pos, 0.5f, hitAble);
            if (col != null)
            {
                GridSquare square = col.GetComponent<GridSquare>();
                if (Input.GetMouseButtonDown(0))
                {
                    if (square.hasPlayer)
                    {
                        if(!playerSelected)
                        {
                            if (cardToUse != null)
                                ResetSelectionState();

                            AudioManager.instance.PlayAudio("PlayerSelect");
                            detectSelectableSquare = true;
                            playerSelected = true;
                            return;
                        }
                        else
                        {
                            AudioManager.instance.PlayAudio("PlayerSelect");
                            ResetSelectionState();
                            return;
                        }
                    }

                    if (selectableTiles.Contains(square))
                    {
                        if (gameMaster.HasEnoughActionsLeft(actionCost))
                        {
                            selectedGridSquare = square;
                            if (playerMode == PlayerMode.Attack)
                            {
                                Attack(selectedGridSquare);
                            }
                            else if (playerMode == PlayerMode.Move && square.enemyStats == null)
                            {
                                playerController.target = selectedGridSquare.transform;
                            }
                        }
                    }
                }
            }

        }
    }

    public float GetDamage()
    {
        return damage;
    }

    public void updateCardValues(float debuffPercentage)
    {
        if(debuffPercentage > 0)
        {
            damage -= damage * debuffPercentage;
            damage = Mathf.FloorToInt(damage);

            ShowPopUp("Damage -");
        }
        else if (debuffPercentage < 0)
        {
            damage -= damage * debuffPercentage;
            damage = Mathf.FloorToInt(damage);

            ShowPopUp("Damage +");
        }
    }

    public void UpdateDamage(float damageIncreasePercentage)
    {
        damage += playerDamage * damageIncreasePercentage;
        ShowPopUp("Damage +");
    }

    public void ShowPopUp(string text)
    {
        popUpText.text = text;

        if (popUpAnimator != null)
            popUpAnimator.SetTrigger("PopIn");
    }

    public void SetCardToUse(Card card)
    {
        if (playerMode == PlayerMode.Move)
            ResetSelectionGrid();

        if (cardToUse != null)
        {
            cardToUse.DeselectCard();
            playerSelected = false;
            ResetSelectionGrid();
        }

        if(card.requiredConfirmation)
        {
            cardToUse = card;
            selectableRange = cardToUse.effectRange;
            playerMode = PlayerMode.Attack;
            detectSelectableSquare = true;
        }
        else
        {
            card.UseCard(selectableTiles, null);
            gameMaster.UseAction();
            ResetSelectionState();
        }

    }

    public void DeselectCardToUse()
    {
        cardToUse = null;
        selectableRange = 0;
        detectSelectableSquare = false;
        ResetSelectionState();
    }

    void Attack(GridSquare spawnPos)
    {
        cardToUse.UseCard(selectableTiles, spawnPos);
        gameMaster.UseAction();
        ResetSelectionState();
    }

    public void MovementDone()
    {
        ResetSelectionState();
        gameMaster.UseAction();
    }

    private void ResetSelectionState()
    {
        playerSelected = false;
        ResetSelectionGrid();
        playerMode = PlayerMode.Move;
    }

    void ResetSelectionGrid()
    {
        selectedGridSquare = null;
        foreach (GridSquare grid in selectableTiles)
        {
            grid.ChangeColor(ColorType.Idel);
        }
        selectableTiles.Clear();
    }
}
