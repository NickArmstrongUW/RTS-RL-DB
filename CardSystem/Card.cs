using UnityEngine;
using System;

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

        public void Initialize(CardData data) {
            this.data = data;
            this.name = data.cardName;
            this.cost = data.cost;
        }

        public void Play(Action<Card> callback) {
            Debug.Log($"Playing {name}");
            data.Activate(this);
            callback(this);
        }

        // built to be overriden by card on
        // handles card targeting and other specific effects
        public void PreCast() {
            Debug.Log($"PreCast {name}");
            data.PreCast(this);
        }

        // user selects a card to play
        // Handle card targeting
        //Place in spell queue if there is space
        public void OnClick() {
            if (State == CardState.InHand) {
                PreCast();
                if (CardManager.instance != null) {
                    CardManager.instance.QueueCard(handIndex);
                } else {
                    Debug.Log("CardManager instance not found");
                }
            } else {
                Debug.Log($"Cannot play {name} in state {State}");
            }
        }
    }
}