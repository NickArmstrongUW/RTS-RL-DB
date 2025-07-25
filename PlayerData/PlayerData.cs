using UnityEngine;
using CardSystem;
using System.Collections.Generic;


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

    // cards
    public List<PlayerCardEntry> collection = new();
    public List<CardType> currentDeck = new();

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
    public void initFreshAccount() {
        // create a collection of the 5 starting cards we pick, 4 each
        collection.Add(new PlayerCardEntry {cardType = CardType.Fireball, countOwned = 4, level = 1 });
        collection.Add(new PlayerCardEntry {cardType = CardType.Restore, countOwned = 4, level = 1});
        collection.Add(new PlayerCardEntry {cardType = CardType.Shield, countOwned = 4, level = 1});
        collection.Add(new PlayerCardEntry {cardType = CardType.Hextrap, countOwned = 4, level = 1});
        collection.Add(new PlayerCardEntry {cardType = CardType.Blink, countOwned = 4, level = 1});

        // put each card in the starting deck
        foreach (PlayerCardEntry entry in collection) {
            for (int i = 0; i < entry.countOwned; i++) {
                currentDeck.Add(entry.cardType);
            }
        }
    }
}