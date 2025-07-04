using UnityEngine;
using System;
using CardSystem;

namespace Cards
{
    [CreateAssetMenu(menuName = "Cards/Fireball")]
    public class FireballCard: CardData
    {
        public override void Activate(Card cardInstance) {
            Debug.Log("Fireball played");
        }
    }
}