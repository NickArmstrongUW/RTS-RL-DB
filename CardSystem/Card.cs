using UnityEngine;
using System;
using System.Threading.Tasks;
using TMPro;

namespace CardSystem
{
    public enum CardState
    {
        InDeck,
        InHand,
        InGraveyard,
        InBurn
    }



    public class Card: MonoBehaviour
    {
        public CardState State { get; set; }
        public Transform transform => base.transform;
        
        // Add missing properties that CardFactory expects
        public string name { get; set; }
        public int cost { get; set; }
        
        public CardData data;
        public int handIndex = -1;
        
        private CardView cardView; // Reference to CardView component

        public void Initialize(CardData data) {
            this.data = data;
            this.name = data.cardName;
            this.cost = data.cost;
            
            // Get or create CardView reference
            if (cardView == null) {
                cardView = GetComponent<CardView>();
            }
            
            // Initialize CardView
            if (cardView != null) {
                cardView.Initialize(data, this);
            } else {
                Debug.LogError("Card.Initialize: No CardView component found!");
            }
        }

        public void Play(Action<Card> callback) {
            Debug.Log($"Playing {name}");
            data.Activate(this);
            callback(this);
        }

        public void changeCost(int newCost) {
            cost = newCost;
            
            // Update the UI through CardView
            if (cardView != null) {
                cardView.UpdateCostDisplay(newCost);
            }
        }
        

        // user selects a card to play
        // Handle card targeting
        //Place in spell queue if there is space
        // should most of this be in the card manager?
        public async void OnClick() {
            if (State == CardState.InHand) {
                if (Player.Instance.currentMana < cost) {
                    Debug.Log("Not enough mana");
                    return;
                }

                try {
                    await data.PreCast(this);
                    if (CardManager.Instance != null) {
                        CardManager.Instance.QueueCard(handIndex);
                    } else {
                        Debug.Log("CardManager instance not found");
                    }
                    Player.Instance.spendMana(cost);
                } finally {
                    Debug.Log("PreCast finished");
                }
            } else {
                Debug.Log($"Cannot play {name} in state {State}");
            }
        }
    }
}