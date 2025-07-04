// handles overall game state
using UnityEngine;
using System.Collections.Generic;
using CardSystem;

public class GameController: MonoBehaviour
{
    public CardManager cardManager;
    public Deck deck;
    public Discard discardPile;
    public CardFactory cardFactory;

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Debug.Log("Starting game");
        
        List<Card> cardList = new List<Card>();
        
        // Create cards using the factory
        cardList.Add(cardFactory.CreateCard(CardType.Fireball));
        cardList.Add(cardFactory.CreateCard(CardType.Fireball));
        // cardList.Add(cardFactory.CreateCard(CardType.LightningBolt));
        // cardList.Add(cardFactory.CreateCard(CardType.Heal));
        
        deck.Initialize(cardList);
        deck.Shuffle();
    }
}