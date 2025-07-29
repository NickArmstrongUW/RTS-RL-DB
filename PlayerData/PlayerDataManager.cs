using UnityEngine;
using System;
using System.Collections.Generic;
using CardSystem;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance {get; private set;}

    public PlayerData playerData;

    private void Awake() {
        if (Instance != null ) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        playerData = SaveSystem.Load();
    }

    public void Save()
    {
        SaveSystem.Save(playerData);
    }

    // in case data seems wrong, reload. This would overrite any data since last save
    public void Reload()
    {
        playerData = SaveSystem.Load();
    }

    // returns whatever starting deck the player currently should be using
    public Decklist GetStartingDeck() {
        if (playerData != null) {
            return playerData.decks[playerData.currentDeck];
        }
        return null;
    }

    // returns 0 if the player doesn't have the card
    public int GetCardLevel(CardType cardType) {
        if (playerData.cardLevels.ContainsKey(cardType)) {
            return playerData.cardLevels[cardType];
        } else {
            playerData.cardLevels[cardType] = 0;
            return 0;
        }
    }

    // returns the player's card collection
    public List<PlayerData.PlayerCardEntry> GetCollection() {
        return playerData?.collection ?? new List<PlayerData.PlayerCardEntry>();
    }
}