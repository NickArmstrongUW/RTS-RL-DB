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
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card Data")]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public int cost;
        public CardType cardType;
        public string description;
        public Sprite cardImage;
        public int level = 1; 
        public CardRarity rarity;

        public virtual void Activate(Card cardInstance) {
            Debug.Log($"Activating {cardName}");
        }

        public virtual async Task PreCast(Card cardInstance) {
            Debug.Log($"Pre-casting {cardName}");
        }
    }

    public enum CardType
    {
        Fireball,
        Restore,
    } 
}