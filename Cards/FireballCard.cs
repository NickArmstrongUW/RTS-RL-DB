using UnityEngine;

namespace Cards
{
    public class FireballCard : MonoBehaviour, Card
    {
        [field: NonSerialized]
        public int cost { get; set; }
        
        [field: NonSerialized]
        public string name { get; set; }

        [field: NonSerialized]
        public CardState State { get; set; } = CardState.InDeck;
        
        public int damage;
        public float castTime;
        
        public void Play() {
            Debug.Log("Fireball played");
        }
    }
}