using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using CardSystem;

namespace CardSystem {
public class CardManager: MonoBehaviour
{

    public static CardSystem.CardManager Instance;

    public List<Card> hand;
    public int handSize=5;
    public Transform[] cardPositions;
    public Transform offScreenPosition;
    public Deck deck;
    public Graveyard graveyard;
    public Queue<Card> spellQueue;
    public int maxSpellQueueSize = 3;
    // public Button deckButton;

    public void Awake() {
        Instance = this;
        spellQueue = new Queue<Card>();
        hand = new List<Card>();
        for (int i = 0; i < handSize; i++) {
            hand.Add(null);
        }
    }

    public void DrawHand(){
        ReturnHandToDeck();
        for (int i = 0; i < handSize; i++)
        {
            DrawCard();
        }
    }

    public void ReturnHandToDeck(){
        for (int i = 0; i < handSize; i++)
        {
            if (hand[i] != null) {
                ReturnCardToDeck(i);
            }
        }
        deck.Shuffle();
    }

    // callback for when a spell is played, queues the next spell
    public void SpellCallback(Card card) {
        spellQueue.Dequeue();
        // play next spell in queue if any
        if (spellQueue.Count > 0) {
            spellQueue.Peek().Play(SpellCallback);
        } else {
            Debug.Log("No more spells to play");
        }
    }

    // queues a card from the hand
    public void QueueCard(int handIndex)
    {
        if (spellQueue.Count >= maxSpellQueueSize) {
            Debug.Log("Spell queue is full!");
            return;
        }
        // add spell to queue
        Card card = hand[handIndex];
        Debug.Log($"Queueing card {card.name}");
        spellQueue.Enqueue(card);
        // move card to discard pile
        DiscardCard(handIndex);
        // if this is the first card in the queue, play it
        if (spellQueue.Count == 1) {
            spellQueue.Peek().Play(SpellCallback);
        }
    }

    public void DrawCard()
    {
        // find open slot in hand if any
        for (int i = 0; i < handSize; i++)
        {
            if (hand[i] == null)
            {
                // empty deck logic
                if (deck.cards.Count == 0)
                {
                    // shuffle discard into deck
                    if (graveyard.cards.Count > 0) {
                        MoveGraveyardToDeck();
                    } else {
                        Debug.Log("No cards left to draw");
                        return;
                    }

                }
                Card card = deck.DrawCard();
                // Move the card GameObject to the hand position
                card.transform.SetParent(cardPositions[i]);
                card.transform.localPosition = Vector3.zero;
                card.transform.localRotation = Quaternion.identity;
                
                // Update card state
                card.State = CardState.InHand;
                card.handIndex = i;
                
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
        card.transform.SetParent(graveyard.transform);
        card.transform.localPosition = Vector3.zero;
        
        // Update card state
        card.State = CardState.InGraveyard;
        
        // Add to discard pile
        graveyard.AddCard(card);
        
        // Remove from hand
        hand[handIndex] = null;
        
        Debug.Log($"Card {card.name} discarded");
    }

    public void MoveGraveyardToDeck()
    {
        Debug.Log("Shuffling graveyard into deck");
        // move all cards from discard pile to deck
        // TODO: move logic to graveyard for removing all
        while (graveyard.cards.Count > 0)
        {
            Card card = graveyard.cards[graveyard.cards.Count - 1];
            graveyard.cards.RemoveAt(graveyard.cards.Count - 1);
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