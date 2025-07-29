using UnityEngine;
using System.Collections.Generic;
using TMPro;
using CardSystem;

public class DecklistUI: MonoBehaviour
{
    public GameObject cardRowPrefab;
    public Transform cardListContainer;

    public void DisplayDeck(Decklist deck)
    {
        Debug.Log("displaying deck");
        Debug.Log($"Deck size {deck.size}");
        
        // Add debug logging to check if deck is null or contents is empty
        if (deck == null) {
            Debug.LogError("Deck is null!");
            return;
        }
        
        if (deck.contents == null) {
            Debug.LogError("Deck.contents is null!");
            return;
        }
        
        Debug.Log($"Deck.contents.Count: {deck.contents.Count}");
        Debug.Log($"Deck.contents.Keys: {string.Join(", ", deck.contents.Keys)}");
        
        // Clear previous children, this seems wasteful is there a better way?
        // maybe in the loop where we generate if the child already exists we can just edit it instead
        foreach (Transform child in cardListContainer)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("Finished deleting children");
        // Create a new row for each card
        foreach (CardType type in deck.contents.Keys)
        {
            Debug.Log($"{type.ToString()} displaying in deck");
            // don't have a way to display stars yet but we will
            foreach(int stars in deck.contents[type].Keys) {
                int amount = deck.contents[type][stars];
                Debug.Log($"  Stars: {stars}, Amount: {amount}");
                if(amount > 0) {
                    GameObject row = Instantiate(cardRowPrefab, cardListContainer);
                    TextMeshProUGUI[] texts = row.GetComponentsInChildren<TextMeshProUGUI>();

                    // when we change card lookup to a dictionary we can use the set name for a card instead
                    texts[0].text = type.ToString(); // Card name
                    texts[1].text = $"x{amount}"; // Count
                    Debug.Log($"Created row for {type} with {amount} cards");
                }
            }
        }
    }
}