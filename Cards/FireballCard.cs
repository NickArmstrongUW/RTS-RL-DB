using UnityEngine;
using System;
using CardSystem;
using System.Threading.Tasks;

namespace Cards
{
    [CreateAssetMenu(menuName = "Cards/Fireball")]
    public class FireballCard: CardData
    {
        public FireballSpell fireballSpellPrefab;
        public Vector2 direction;
        public float baseDamage = 5f;
        public Vector2 spawnOffset = new Vector2(0.5f, 0.5f);


        public override void Activate(Card cardInstance) {
            Debug.Log("Fireball played");
            Vector2 spawnPos = Player.Instance.location.position + (Vector3)spawnOffset;
            FireballSpell spell = Instantiate(fireballSpellPrefab, spawnPos, Quaternion.identity);
            Debug.Log("base damage: " + baseDamage);
            float damage = baseDamage + (2 * cardInstance.data.level);
            Debug.Log("Fireball damage: " + damage);
            spell.Cast(direction, damage);
        }

        public override async Task PreCast(Card cardInstance) {
            Debug.Log("Fireball pre-cast");
            direction = await GetDirectionFromUser();
            Debug.Log("Fireball direction set: " + direction);
        }

        private async Task<Vector2> GetDirectionFromUser() {
            // Create a TaskCompletionSource to wait for the callback
            var tcs = new TaskCompletionSource<Vector2>();
            
            MouseDirectionSelector.Instance.BeginSelection(
                Player.Instance.location, 
                (direction) => tcs.SetResult(direction)
            );
            
            // Wait for the user to select a direction
            return await tcs.Task;
        }
    }
}