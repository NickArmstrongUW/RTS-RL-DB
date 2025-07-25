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
    public List<CardType> getStartingDeck() {
        if (playerData != null) {
            return playerData.currentDeck;
        }
        return null;
    }
}