// handles overall game state
using UnityEngine;
using System.Collections.Generic;
using CardSystem;

public class GameController: MonoBehaviour
{
    public CardManager cardManager;
    public Deck deck;
    public Graveyard graveyard;
    public CardFactory cardFactory;

    // position selectors for easy access by cards
    public MouseDirectionSelector mouseDirectionSelector;
    public SimpleClickSelector simpleClickSelector;
    public MousePositionSelector MousePositionSelector;

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
        cardList.Add(cardFactory.CreateCard(CardType.Fireball));
        cardList.Add(cardFactory.CreateCard(CardType.Fireball));
        cardList.Add(cardFactory.CreateCard(CardType.Fireball));
        cardList.Add(cardFactory.CreateCard(CardType.Fireball));
        cardList.Add(cardFactory.CreateCard(CardType.Restore));
        cardList.Add(cardFactory.CreateCard(CardType.Restore));
        cardList.Add(cardFactory.CreateCard(CardType.Restore));
        cardList.Add(cardFactory.CreateCard(CardType.Shield));
        cardList.Add(cardFactory.CreateCard(CardType.Shield));
        cardList.Add(cardFactory.CreateCard(CardType.Shield));
        cardList.Add(cardFactory.CreateCard(CardType.Shield));
        cardList.Add(cardFactory.CreateCard(CardType.Hextrap));
        cardList.Add(cardFactory.CreateCard(CardType.Hextrap));
        cardList.Add(cardFactory.CreateCard(CardType.Hextrap));
        // cardList.Add(cardFactory.CreateCard(CardType.LightningBolt));
        // cardList.Add(cardFactory.CreateCard(CardType.Heal));
        
        deck.Initialize(cardList);
        deck.Shuffle();

        cardManager.DrawHand();
    }
}