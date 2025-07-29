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
        // Clear previous children, this seems wasteful is there a better way?
        // maybe in the loop where we generate if the child already exists we can just edit it instead
        foreach (Transform child in cardListContainer)
        {
            Destroy(child.gameObject);
        }

        // Create a new row for each card
        foreach (CardType type in deck.contents.Keys)
        {
            // don't have a way to display stars yet but we will
            foreach(int stars in deck.contents[type].Keys) {
                int amount = deck.contents[type][stars];
                if(amount > 0) {
                    GameObject row = Instantiate(cardRowPrefab, cardListContainer);
                    TextMeshProUGUI[] texts = row.GetComponentsInChildren<TextMeshProUGUI>();

                    // when we change card lookup to a dictionary we can use the set name for a card instead
                    texts[0].text = type.ToString(); // Card name
                    texts[1].text = $"x{amount}"; // Count
                }
            }
        }
    }
}