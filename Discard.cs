using UnityEngine;
using System.Collections.Generic;
using CardSystem;

public class Discard: MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public Card RemoveCard(Card card)
    {
        if (cards.Remove(card))
        {
            return card;
        }
        return null;
    }

    // shows you what's in your graveyard
    public void OnClick()
    {
        Debug.Log($"Discard pile contains {cards.Count} cards");
        foreach (Card card in cards)
        {
            Debug.Log($"- {card.name}");
        }
    }
}