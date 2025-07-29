using UnityEngine;
using System.Collections.Generic;
using CardSystem;
using UnityEngine.SceneManagement;


// probably will want to make sure logic works for different screen sizes
public class EditDeckUI: MonoBehaviour
{
    public GameObject cardSlotPrefab;
    public Transform contentContainer;
    public DecklistUI deckDisplay;
    public Decklist workingDeck;

    // on start load in our current deck and the player's collection
    private void Awake() {
        LoadAndDisplayDeck();
        LoadAndDisplayCollection();
    }

    // TODO: refactor to take a deckslot and display that
    private void LoadAndDisplayDeck() {
        // load in the player's current deck and display it on wakeup
        if (PlayerDataManager.Instance == null) {
            Debug.Log("Player not loaded in");
            SceneManager.LoadScene("HomeMenu");
            return; // for some reason the rest of this function still runs otherwise anyway??
        }
        workingDeck = PlayerDataManager.Instance.GetStartingDeck();
        if (deckDisplay != null) {
            deckDisplay.DisplayDeck(workingDeck);
        } else {
            Debug.Log("No deck display UI found");
        }

    }

    // should display every card the player owns and their count
    private void LoadAndDisplayCollection() {
        List<PlayerData.PlayerCardEntry> collection = PlayerDataManager.Instance.GetCollection();
        // Clear previous cards
        foreach (Transform child in contentContainer)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a prefab for each card in the collection
        foreach (PlayerData.PlayerCardEntry entry in collection)
        {
            // Debug.Log($"Displaying entry: {entry.cardType.ToString()}");
            if (entry.countOwned > 0) {
                GameObject cardGO = Instantiate(cardSlotPrefab, contentContainer);
                
                // Set up the card visuals using PlayerCardEntry data
                CardDisplay cardDisplay = cardGO.GetComponent<CardDisplay>();
                if (cardDisplay != null) {
                    cardDisplay.SetupFromPlayerEntry(entry, this); // Pass 'this' as parent reference
                } else {
                    Debug.Log($"MISSING CARD DISPLAY {entry.cardType.ToString()}");
                }
            }
        }
    }


    public void AddCard(PlayerData.PlayerCardEntry cardEntry) {
        // Add card to working deck (assuming 0 stars for now, you can modify this)
        workingDeck.AddCard(cardEntry.cardType, cardEntry.stars, 1);
        
        // Update the deck display
        if (deckDisplay != null) {
            deckDisplay.DisplayDeck(workingDeck);
        }
        
        Debug.Log($"Added {cardEntry.cardType} to deck. Deck size: {workingDeck.size}");
    }

    public void RemoveCard(PlayerData.PlayerCardEntry cardEntry) {
        // Remove card from working deck
        workingDeck.RemoveCard(cardEntry.cardType, cardEntry.stars);
        
        // Update the deck display
        if (deckDisplay != null) {
            deckDisplay.DisplayDeck(workingDeck);
        }
        
        Debug.Log($"Removed {cardEntry.cardType} from deck. Deck size: {workingDeck.size}");
    }

    // updates the player's deck in the current slot
    public void SaveDeck() {
        PlayerDataManager.Instance.UpdateDeck(workingDeck);
    }

    public void Return() {
        // pop up do you want to save deck?
        // SaveDeck();
        SceneManager.LoadScene("HomeMenu");
    }
}