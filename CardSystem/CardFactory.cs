using UnityEngine;
using System.Collections.Generic;
using CardSystem;

namespace CardSystem {
    public class CardFactory : MonoBehaviour
    {
        [System.Serializable]
        public class CardTypeData
        {
            public CardType type;
            public CardData data;
        }
        
        //TODO: change to dictionary
        public List<CardTypeData> cardTypes = new List<CardTypeData>();
        public GameObject cardPrefab;
        
        public Card CreateCard(CardType type)
        {
            CardTypeData typeData = cardTypes.Find(x => x.type == type);
            if (typeData != null)
            {
                GameObject cardObject = Instantiate(cardPrefab);
                Card card = cardObject.GetComponent<Card>();
                
                // Initialize the card with data
                card.Initialize(typeData.data);
                
                return card;
            }
            return null;
        }
    } 
}