using UnityEngine;
using System.Collections.Generic;
using CardSystem;
using System;

[System.Serializable]
public class Decklist : ISerializationCallbackReceiver {
    public Dictionary<CardType, Dictionary<int, int>> contents = new();
    public int size;

    // Serialization helpers for Unity's JsonUtility
    [System.Serializable]
    public class CardEntry {
        public CardType cardType;
        public int stars;
        public int amount;
    }
    
    [SerializeField] private List<CardEntry> serializedContents = new();

    public void OnBeforeSerialize() {
        serializedContents.Clear();
        foreach (var cardEntry in contents) {
            foreach (var starEntry in cardEntry.Value) {
                serializedContents.Add(new CardEntry {
                    cardType = cardEntry.Key,
                    stars = starEntry.Key,
                    amount = starEntry.Value
                });
            }
        }
    }

    public void OnAfterDeserialize() {
        contents.Clear();
        size = 0;
        foreach (var entry in serializedContents) {
            if (!contents.ContainsKey(entry.cardType)) {
                contents[entry.cardType] = new Dictionary<int, int>();
            }
            contents[entry.cardType][entry.stars] = entry.amount;
            size++;
        }
    }

    public bool ContainsCard(CardType card) {
        return contents.ContainsKey(card);
    }

    public int CountOf(PlayerData.PlayerCardEntry entry) {
        return CountOf(entry.cardType, entry.stars);
    }

    public int CountOf(CardType card, int stars) {
        if (contents.ContainsKey(card)) {
            if (contents[card].ContainsKey(stars)) {
                return contents[card][stars];
            }
        }
        return 0;
    }

    public void AddCard(CardType card) {
        AddCard(card, 1, 1);
    }

    public void AddCard(CardType card, int stars, int amount) {
        if(contents.ContainsKey(card)) {
            if (contents[card].ContainsKey(stars)) {
                contents[card][stars] += amount;
            } else {
                contents[card][stars] = amount;
            }
        } else {
            contents[card] = new Dictionary<int, int>();
            contents[card][stars] = amount;
        }
        size += amount;
    }

    public void RemoveCard(CardType card) {
        RemoveCard(card, 1);
    }

    public void RemoveCard(CardType card, int stars) {
        if(contents.ContainsKey(card) && contents[card].ContainsKey(stars)) {
            contents[card][stars] -= 1;
            
            // Remove the star level entry if count goes to 0 or below
            if (contents[card][stars] <= 0) {
                contents[card].Remove(stars);
                
                // Remove the card entry entirely if no star levels remain
                if (contents[card].Count == 0) {
                    contents.Remove(card);
                }
            }
        }
        size--;
    }
}
