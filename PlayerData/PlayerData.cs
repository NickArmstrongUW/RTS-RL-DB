using UnityEngine;
using CardSystem;
using System.Collections.Generic;
using System;



[System.Serializable]
public class PlayerData
{
    [System.Serializable]
    public class PlayerCardEntry {
        public CardType cardType;
        public int countOwned;
        public int level = 1;
        // public int stars; // will be adding this later
    }

    public static readonly List<CardType> initialCards = new List<CardType> {
        CardType.Fireball,
        CardType.Restore,
        CardType.Shield,
        CardType.Hextrap,
        CardType.Blink
    };

    // cards
    public List<PlayerCardEntry> collection = new(); // showcases every card in the collection
    public int currentDeck = 0; // deck the player currently has selected
    public Dictionary<CardType, int> cardLevels = new(); // quick lookup for what levl the player's cards are
    public List<Decklist> decks = new();
    // stats
    public float longestSurvived = 0;
    public int totalKills = 0;

    // currencies
    public int essence = 0;
    // // public int gold;
    // public int commonWildcards;
    // public int uncommonWildcards;
    // public int rareWildcards;

    // loads in what cards a fresh account would have
    public void InitFreshAccount() {

        // init all cardtypes to 0
        foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
        {
            cardLevels[cardType] = 0;
        }
        // create a collection of the 5 starting cards we pick, 4 each
        foreach (CardType entry in initialCards) {
            collection.Add(new PlayerCardEntry {cardType = entry, countOwned = 4, level = 1});
            cardLevels[entry] = 1;
        }
        // collection.Add(new PlayerCardEntry {cardType = CardType.Fireball, countOwned = 4, level = 1 });
        // collection.Add(new PlayerCardEntry {cardType = CardType.Restore, countOwned = 4, level = 1});
        // collection.Add(new PlayerCardEntry {cardType = CardType.Shield, countOwned = 4, level = 1});
        // collection.Add(new PlayerCardEntry {cardType = CardType.Hextrap, countOwned = 4, level = 1});
        // collection.Add(new PlayerCardEntry {cardType = CardType.Blink, countOwned = 4, level = 1});


        // put each card in the starting deck
        Decklist deck = new Decklist();
        foreach (PlayerCardEntry entry in collection) {
            for (int i = 0; i < entry.countOwned; i++) {
                deck.AddCard(entry.cardType);
            }
        }
        decks.Add(deck);
    }
}