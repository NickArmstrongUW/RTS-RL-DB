// logic for drawing cards
using UnityEngine;
using System.Collections.Generic;
using CardSystem;

public class Deck: MonoBehaviour
{
    // public int size;
    public List<Card> cards;
    public Transform deckCardsPosition;

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
        
        if (cards == null)
        {
            Debug.LogError("Cannot shuffle: cards list is null!");
            return;
        }
        
        if (cards.Count == 0)
        {
            Debug.Log("Deck is empty - no need to shuffle");
            return;
        }
        
        int nullCardsRemoved = 0;
        
        // Fisher-Yates shuffle with null removal
        for (int i = cards.Count - 1; i >= 0; i--)
        {
            // Remove null cards as we encounter them
            if (cards[i] == null)
            {
                cards.RemoveAt(i);
                nullCardsRemoved++;
                continue;
            }
            
            // Only shuffle if we have more than one card left
            if (i > 0)
            {
                // Find a valid random index (non-null card)
                int randomIndex = Random.Range(0, i + 1);
                
                // If the randomly selected card is null, remove it and adjust
                if (cards[randomIndex] == null)
                {
                    cards.RemoveAt(randomIndex);
                    nullCardsRemoved++;
                    
                    // Adjust our current index if needed
                    if (randomIndex <= i) i++;
                    
                    continue;
                }
                
                // Swap cards (both should be non-null now)
                Card temp = cards[i];
                cards[i] = cards[randomIndex];
                cards[randomIndex] = temp;
            }
        }
        
        if (nullCardsRemoved > 0)
        {
            Debug.Log($"Removed {nullCardsRemoved} null cards during shuffle");
        }
        
        Debug.Log($"Deck shuffled successfully! {cards.Count} cards in deck");
        
        // Optional: Log the new order
        for (int i = 0; i < Mathf.Min(5, cards.Count); i++)
        {
            Debug.Log($"Top {i + 1}: {cards[i].name}");
        }
    }

    public void AddToBottom(Card card)
    {
        cards.Add(card);
        card.transform.SetParent(deckCardsPosition);
        card.transform.localPosition = Vector3.zero;
        card.transform.localRotation = Quaternion.identity;
    }

    public void AddToTop(Card card)
    {
        cards.Insert(0, card);
        card.transform.SetParent(deckCardsPosition);
        card.transform.localPosition = Vector3.zero;
        card.transform.localRotation = Quaternion.identity;
    }

    // potentially change to scry to view x top cards
    public Card Peek() {
        if (cards != null && cards.Count > 0) {
            return cards[0];
        }
        return null;
    }
}