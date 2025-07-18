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
    } 
}