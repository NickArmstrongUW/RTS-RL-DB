using UnityEngine;
using System;
using System.Threading.Tasks;


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



        // user selects a card to play
        // Handle card targeting
        //Place in spell queue if there is space
        public async void OnClick() {
            if (State == CardState.InHand) {
                // TODO impliment this
                // SetHighlight(true);
                // DisableButton();

                try {
                    await data.PreCast(this);
                    if (CardManager.Instance != null) {
                        CardManager.Instance.QueueCard(handIndex);
                    } else {
                        Debug.Log("CardManager instance not found");
                    }
                } finally {
                    Debug.Log("PreCast finished");
                //     SetHighlight(false);
                //     EnableButton();
                }
            } else {
                Debug.Log($"Cannot play {name} in state {State}");
            }
        }
    }
}