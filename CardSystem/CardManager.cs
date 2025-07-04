using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using CardSystem;

namespace CardSystem {
public class CardManager: MonoBehaviour
{
    public List<Card> hand;
    public int handSize=5;
    public Transform[] cardPositions;
    public Deck deck;
    public Discard discardPile;
    // public Button deckButton;

    private void Start()
    {
        hand = new List<Card>();
        for (int i = 0; i < handSize; i++)
        {
            hand.Add(null);
        }
        // deckButton.onClick.AddListener(DrawCard);
    }

    public void DrawCard()
    {
        if (deck.cards.Count == 0)
        {
            Debug.Log("Deck is empty!");
            return;
        }

        for (int i = 0; i < cardPositions.Length; i++)
        {
            Debug.Log($"Checking position {i}");
            if (hand[i] == null)
            {
                Card card = deck.DrawCard();
                // Move the card GameObject to the hand position
                card.transform.SetParent(cardPositions[i]);
                card.transform.localPosition = Vector3.zero;
                card.transform.localRotation = Quaternion.identity;
                
                // Update card state
                card.State = CardState.InHand;
                
                // Add to hand list
                hand[i] = card;
                
                Debug.Log($"Card {card.name} drawn to position {i}");
                return;
            }
        }

        Debug.Log("Hand is full!");
    }

    // public void PlayCard(int handIndex)
    // {
    //     if (handIndex < 0 || handIndex >= hand.Count || hand[handIndex] == null)
    //     {
    //         Debug.Log("Invalid card index or no card at position");
    //         return;
    //     }

    //     Card card = hand[handIndex];
    //     card.State = CardState.Casting;

    //     // wait for cast time
    //     yield return new WaitForSeconds(card.castTime);
        
    //     // Execute card effect
    //     card.Play();
        
    //     // Move to discard pile
    //     DiscardCard(handIndex);
    // }

    public void DiscardCard(int handIndex)
    {
        if (handIndex < 0 || handIndex >= hand.Count || hand[handIndex] == null)
        {
            Debug.Log("Invalid card index or no card at position");
            return;
        }

        Card card = hand[handIndex];
        
        // Move card to discard pile
        card.transform.SetParent(discardPile.transform);
        card.transform.localPosition = Vector3.zero;
        
        // Update card state
        card.State = CardState.InDiscard;
        
        // Add to discard pile
        discardPile.AddCard(card);
        
        // Remove from hand
        hand[handIndex] = null;
        
        Debug.Log($"Card {card.name} discarded");
    }

    public void MoveDiscardToDeck()
    {
        // move all cards from discard pile to deck
        foreach (Card card in discardPile.cards)
        {
            discardPile.RemoveCard(card);
            deck.AddToBottom(card);
        }  
        deck.Shuffle();
    }



    // currently unused but might be helpful later
    public void ReturnCardToDeck(int handIndex)
    {
        if (handIndex < 0 || handIndex >= hand.Count || hand[handIndex] == null)
        {
            Debug.Log("Invalid card index or no card at position");
            return;
        }

        Card card = hand[handIndex];
        
        // Move card back to deck
        card.transform.SetParent(deck.transform);
        card.transform.localPosition = Vector3.zero;
        
        // Update card state
        card.State = CardState.InDeck;
        
        // Add back to deck
        deck.AddToTop(card);
        
        // Remove from hand
        hand[handIndex] = null;
        
        Debug.Log($"Card {card.name} returned to deck");
    }
}
}