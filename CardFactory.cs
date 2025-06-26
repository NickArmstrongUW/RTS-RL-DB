using UnityEngine;
using System.Collections.Generic;
using Cards;

public class CardFactory : MonoBehaviour
{
    [System.Serializable]
    public class CardTypeData
    {
        public CardType type;
        public CardData data;
    }
    
    public List<CardTypeData> cardTypes = new List<CardTypeData>();
    
    public Card CreateCard(CardType type)
    {
        CardTypeData typeData = cardTypes.Find(x => x.type == type);
        if (typeData != null)
        {
            GameObject cardObject = Instantiate(typeData.data.cardPrefab);
            Card card = cardObject.GetComponent<Card>();
            
            // Set the card properties from the ScriptableObject
            card.name = typeData.data.cardName;
            card.cost = typeData.data.cost;
            
            return card;
        }
        return null;
    }
} 