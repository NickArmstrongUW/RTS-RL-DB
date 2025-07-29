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

    private void Awake() {
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

        // load in the player's collection
        LoadAndDisplayCollection();
    }


    private void LoadAndDisplayCollection() {
        List<PlayerData.PlayerCardEntry> playerCollection = PlayerDataManager.Instance.GetCollection();
        PopulateCollection(playerCollection);
    }

    // display our collection
    public void PopulateCollection(List<PlayerData.PlayerCardEntry> collection) {
        // Clear previous cards
        foreach (Transform child in contentContainer)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a prefab for each card in the collection
        foreach (PlayerData.PlayerCardEntry entry in collection)
        {
            if (entry.countOwned > 0) {
                GameObject cardGO = Instantiate(cardSlotPrefab, contentContainer);
                
                // Set up the card visuals using PlayerCardEntry data
                CardDisplay cardDisplay = cardGO.GetComponent<CardDisplay>();
                if (cardDisplay != null) {
                    // You'll need to update CardDisplay to handle PlayerCardEntry
                    // For now, we'll pass the entry data
                    cardDisplay.SetupFromPlayerEntry(entry);
                }
            }
        }
    }


    public void AddCard() {

    }

    public void RemoveCard() {

    }

    // updates the player's deck in the current slot
    public void SaveDeck() {

    }

    public void Return() {
        // pop up do you want to save deck?
        // SaveDeck();
        SceneManager.LoadScene("HomeMenu");
    }
}