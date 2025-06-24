// logic for drawing cards
using UnityEngine;
using System.Collections.Generic;

class Deck: MonoBehaviour
{
    public int size;
    public List<Card> cards;

    public void Initialize(List<Card> cards)
    {
        this.cards = cards;
    }

    public void OnClick()
    {
        Debug.Log("Card clicked");
    }

    public void DrawCard()
    {
        Debug.Log("Drawing card");
    }
}