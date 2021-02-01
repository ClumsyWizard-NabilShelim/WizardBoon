using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public string weaponTypeName;
    PlayerManager PM;

    public string cardName;

    private float value;
    public float damageMultiplier;
    public float effectRange;

    public Sprite cardSprite;

    [TextArea(3, 3)]
    public string description;

    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI cardDescription;
    public TextMeshProUGUI valueText;
    public Image cardImage;

    CardEffect effectScript;

    bool selected = false;

    RectTransform rectTransform;
    Vector2 defaultScale;
    Vector2 defaultPos;
    bool enlarged;
    bool pointerOnTop;
    [HideInInspector] public Deck deck;
    public bool requiredConfirmation = true;

    GameMaster gameMaster;
    public GameObject border;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultScale = rectTransform.localScale;
        defaultPos = rectTransform.anchoredPosition;
        PM = GameObject.FindGameObjectWithTag("PM").GetComponent<PlayerManager>();
        deck = GetComponentInParent<Deck>();
        gameMaster = FindObjectOfType<GameMaster>();
        border.SetActive(false);
    }

    public void ActivateCard()
    {
        if (gameMaster.gameTurn == Turn.Player)
        {
            if (selected)
            {
                AudioManager.instance.PlayAudio("CardSelect");
                selected = false;
                border.SetActive(false);
                PM.DeselectCardToUse();
            }
            else
            {
                AudioManager.instance.PlayAudio("CardSelect");
                selected = true;
                border.SetActive(true);
                value = PM.GetDamage() * damageMultiplier;
                PM.SetCardToUse(this);
            }
        }
    }

    public void DeselectCard()
    {
        border.SetActive(false);
        selected = false;
    }

    void UpdateUI()
    {
        cardNameText.text = cardName;
        cardImage.sprite = cardSprite;
        
        cardDescription.text = description;
        valueText.text = (PM.GetDamage() * damageMultiplier).ToString();

        effectScript = GetComponent<CardEffect>();
    }

    public void UseCard(List<GridSquare> gridSquares, GridSquare selectedSquare)
    {
        effectScript.effectedGridSquares = gridSquares;
        effectScript.selectedGridSquare = selectedSquare;
        effectScript.CardActivated(value);
        Destroy(gameObject);
    }

    private void Update()
    {
        UpdateUI();   
        if(pointerOnTop)
        {
            if (Input.GetMouseButtonDown(1))
            {
                AudioManager.instance.PlayAudio("CardSelect");
                if (!enlarged)
                {
                    deck.EnlargeCard(this);
                    enlarged = true;
                    defaultPos = rectTransform.anchoredPosition;
                    rectTransform.localScale = new Vector2(3, 3);
                    rectTransform.anchoredPosition = new Vector2(320, 65);
                }
                else
                {
                    deck.NormalizeCard();
                    rectTransform.localScale = defaultScale;
                    rectTransform.anchoredPosition = defaultPos;
                    enlarged = false;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerOnTop = true;   
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerOnTop = false;    
    }
}
