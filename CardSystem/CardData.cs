using UnityEngine;

namespace CardSystem {
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card Data")]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public int cost;
        public CardType cardType;
        public string description;
        public Sprite cardImage;

        public virtual void Activate(Card cardInstance) {
            Debug.Log($"Activating {cardName}");
        }

        public virtual void PreCast(Card cardInstance) {
            Debug.Log($"Pre-casting {cardName}");
        }
    }

    public enum CardType
    {
        Fireball,
    } 
}