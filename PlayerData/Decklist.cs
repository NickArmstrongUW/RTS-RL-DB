using UnityEngine;
using System.Collections.Generic;
using CardSystem;

[System.Serializable]
public class Decklist {
    public Dictionary<CardType, Dictionary<int, int>> contents = new();
    public int size;

    public bool ContainsCard(CardType card) {
        return contents.ContainsKey(card);
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
        size+= amount;
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
