// logic for drawing cards
using UnityEngine;
using System.Collections.Generic;
using Cards;

public class Deck: MonoBehaviour
{
    // public int size;
    public List<Card> cards;

    public void Initialize(List<Card> cards)
    {
        this.cards = cards;
    }

    // pulls the next card from the deck and returns it
    public Card DrawCard()
    {
        Debug.Log("Drawing card");
        if (cards == null)
        {
            Debug.Log("Deck is empty!");
            return null;
        }
        if (cards.Count > 0)
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            Debug.Log("Drew card: " + card.name);
            return card;
        }
        return null;
    }

    public void Shuffle()
    {
        Debug.Log("Shuffling deck");
        // shuffle the deck
    }

    public void addToBottom(Card card)
    {
        cards.Add(card);
    }

    public void addToTop(Card card)
    {
        cards.Insert(0, card);
    }

    // potentially change to scry to view x top cards
    public Card Peek() {
        if (cards != null && cards.Count > 0) {
            return cards[0];
        }
        return null;
    }
}