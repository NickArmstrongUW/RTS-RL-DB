using UnityEngine;

namespace CardSystem
{
    public enum CardState
    {
        InDeck,
        InHand,
        Casting,
        InDiscard,
        Burned
    }

    public class Card: MonoBehaviour
    {
        public CardState State { get; set; }
        public Transform transform => base.transform;
        
        // Add missing properties that CardFactory expects
        public string name { get; set; }
        public int cost { get; set; }
        
        public CardData data;

        public void Initialize(CardData data) {
            this.data = data;
            this.name = data.cardName;
            this.cost = data.cost;
        }

        public void Play() {
            Debug.Log($"Playing {name}");
            data.Activate(this);
        }
    }
}