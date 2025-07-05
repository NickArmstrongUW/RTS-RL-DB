using UnityEngine;
using System;
using CardSystem;

namespace Cards
{
    [CreateAssetMenu(menuName = "Cards/Fireball")]
    public class FireballCard: CardData
    {
        public FireballSpell fireballSpellPrefab;
        public Vector2 direction;
        public MouseDirectionSelector directionSelector;

        public override void Activate(Card cardInstance) {
            Debug.Log("Fireball played");
            FireballSpell spell = Instantiate(fireballSpellPrefab, Player.instance.location.position, Quaternion.identity);
            spell.Cast(direction);
        }

        public override void PreCast(Card cardInstance) {
            Debug.Log("Fireball pre-cast");
            directionSelector.BeginSelection(Player.instance.location, OnDirectionSelected);
        }

        private void OnDirectionSelected(Vector2 dir) {
            this.direction = dir;
        }
    }
}