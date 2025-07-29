// handles overall game state
using UnityEngine;
using System.Collections.Generic;
using CardSystem;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
    public CardManager cardManager;
    public Deck deck;
    public Graveyard graveyard;
    public CardFactory cardFactory;
    public EnemySpawner enemySpawner;

    // position selectors for easy access by cards
    public MouseDirectionSelector mouseDirectionSelector;
    public SimpleClickSelector simpleClickSelector;
    public MousePositionSelector MousePositionSelector;
    
    // round stats
    public int enemyKills;

    void Start()
    {
        StartGame();
        
    }

    public void StartGame()
    {
        Debug.Log("Starting game");
        
        // note with the current setup if you load into the game directly for testing without 
        // loading into the main menu it will not load the deck and break
        if(PlayerDataManager.Instance == null) {
            SceneManager.LoadScene("HomeMenu");
        }

        // load the player's current starting deck
        List<Card> cardList = cardFactory.CreateDeckFromDecklist(PlayerDataManager.Instance.GetStartingDeck());        
        deck.Initialize(cardList);
        deck.Shuffle();

        cardManager.DrawHand();
    }

    public void EndGame()
    {
        Debug.Log("Game Over");

        SceneManager.LoadScene("HomeMenu");
    }
}