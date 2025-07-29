using UnityEngine;
using System.Threading.Tasks;

namespace CardSystem {
    public enum CardRarity {
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary
        }
    [CreateAssetMenu(fileName = "New Card", menuName = "Card/Card Data")]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public int cost = 1;
        public CardType cardType;
        public string description;
        public Sprite cardImage;
        public int level = 1; 
        public CardRarity rarity;
        public int stars = 0;

        public virtual void Awake() {
            // level 0 is just for cards the player doesn't have
            // default to whatever level the player has of the card
            // Note: Card level is now set by CardFactory when creating cards
        }
        // what the spell queue will call into to use the card
        public virtual void Activate(Card cardInstance) {
            Debug.Log($"Activating {cardName}");
        }

        // what happens when a user clicks the card
        public virtual async Task PreCast(Card cardInstance) {
            Debug.Log($"Pre-casting {cardName}");
        }
    }

    public enum CardType
    {
        Fireball,
        Restore,
        Shield,
        Hextrap,
        Blink
    } 
}