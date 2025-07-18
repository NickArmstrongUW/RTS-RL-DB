using UnityEngine;
using System;
using CardSystem;
using System.Threading.Tasks;

// 4

namespace Cards
{
    [CreateAssetMenu(menuName = "Card/Hextrap")]
    public class HextrapCard: CardData
    {
        public HextrapSpell HextrapSpellPrefab;
        public Vector2 CastLocation;
        public float baseStunDuration = 2.5f;
        public float baseDamage = 0f; // normally does no damage, but maybe could be modified to do so
        public Vector2 spawnPos;

        public override void Activate(Card cardInstance) {
            Debug.Log("Hextrap played");
            HextrapSpell spell = Instantiate(HextrapSpellPrefab, spawnPos, Quaternion.identity);
            float damage = baseDamage + (2 * cardInstance.data.level);
            float stunDuration = baseStunDuration + (0.25f * cardInstance.data.level);
            spell.Cast(stunDuration, damage);
        }

        // picks a location for hextrap
        public override async Task PreCast(Card cardInstance) {
            spawnPos = await GetPositionFromUser();
        }

        private async Task<Vector2> GetPositionFromUser() {
            // Create a TaskCompletionSource to wait for the callback
            var tcs = new TaskCompletionSource<Vector2>();
            
            MousePositionSelector.Instance.BeginSelection(
                (spawnPos) => tcs.SetResult(spawnPos)
            );
            
            // Wait for the user to select a direction
            return await tcs.Task;
        }


    }
}