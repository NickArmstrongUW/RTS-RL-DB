// Handles creating cards and finding card data for other objects


using UnityEngine;
using System.Collections.Generic;
using CardSystem;
using System;

namespace CardSystem {
    public class CardFactory : MonoBehaviour
    {
        public static CardFactory Instance { get; private set; }

        [System.Serializable]
        public class CardTypeData
        {
            public CardType type;
            public CardData data;
        }
        
        // Inspector-friendly list for setting up card types
        [SerializeField] private List<CardTypeData> cardTypeSetup = new List<CardTypeData>();
        
        // Public property for inspector access
        public List<CardTypeData> CardTypeSetup => cardTypeSetup;
        
        // Runtime dictionary for efficient lookups
        private Dictionary<CardType, CardData> cardTypes = new Dictionary<CardType, CardData>();
        
        public GameObject cardPrefab;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Populate the dictionary from the inspector list
            PopulateCardTypesDictionary();
        }

        private void PopulateCardTypesDictionary() {
            cardTypes.Clear();
            foreach (CardTypeData typeData in cardTypeSetup) {
                if (typeData.data != null) {
                    cardTypes[typeData.type] = typeData.data;
                }
            }
        }

        public Card CreateCard(CardType type, int level) {
            Card card = CreateCard(type);
            if (card != null) {
                card.data.level = level;
            }
            return card;
        }
        
        public Card CreateCard(CardType type)
        {
            if (cardTypes.TryGetValue(type, out CardData cardData))
            {
                GameObject cardObject = Instantiate(cardPrefab);
                Card card = cardObject.GetComponent<Card>();
                
                // Initialize the card with data
                card.Initialize(cardData);
                
                // Set the card level from player data if available
                if (PlayerDataManager.Instance != null) {
                    card.data.level = Math.Max(1, PlayerDataManager.Instance.GetCardLevel(type));
                }
                
                return card;
            }
            return null;
        }

        public CardData GetCardData(CardType type) {
            cardTypes.TryGetValue(type, out CardData cardData);
            return cardData;
        }

        public List<Card> CreateDeckFromDecklist(Decklist deckList) {
            List<Card> ret = new List<Card>();
            foreach(CardType type in deckList.contents.Keys) {
                foreach(int stars in deckList.contents[type].Keys) {
                    for(int i = 0; i < deckList.contents[type][stars]; i++) {
                        Card card = CreateCard(type);
                        card.data.stars = stars;
                        ret.Add(card);
                    }
                }
            }
            return ret;
        }
    } 
}