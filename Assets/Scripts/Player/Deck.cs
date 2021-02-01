using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct CardData
{
    public string cardWeaponName;
    public List<Card> weaponCards;
}
public class Deck : MonoBehaviour
{
    private GridLayoutGroup layoutGroup;
    public int startingHandCardNum;
    public int drawPerTurn;

    public List<CardData> cards = new List<CardData>();

    public float extraCards;

    GameMaster gameMaster;

    public int actionCost;

    public void Start()
    {
        layoutGroup = GetComponent<GridLayoutGroup>();

        for (int i = 0; i < startingHandCardNum; i++)
        {
            AddCards();
        }

        gameMaster = GameObject.FindGameObjectWithTag("PM").GetComponent<GameMaster>();
    }

    public void EnlargeCard(Card card)
    {
        layoutGroup.enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject g = transform.GetChild(i).gameObject;
            if (card.gameObject != g)
                g.SetActive(false);
        }
    }

    public void NormalizeCard()
    {
        layoutGroup.enabled = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void Draw()
    {
        for (int i = 0; i < drawPerTurn; i++)
        {
            if(transform.childCount < 10)
                AddCards();
        }
    }

    void AddCards()
    {
        int choice = Random.Range(0, cards.Count);
        Card card = Instantiate(cards[choice].weaponCards[Random.Range(0, cards[choice].weaponCards.Count)].gameObject, transform).GetComponent<Card>();
        card.weaponTypeName = cards[choice].cardWeaponName;
    }

    public void RemoveCards(string type)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Card card = transform.GetChild(i).GetComponent<Card>();
            if (card.weaponTypeName == type)
            {
                Destroy(card.gameObject);
            }
        }
    }
}
