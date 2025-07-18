using UnityEngine;
using System;
using CardSystem;
using System.Threading.Tasks;

// 5

namespace Cards
{
    [CreateAssetMenu(menuName = "Card/Blink")]
    public class Blink: CardData
    {

        public float baseMaxDistance = 5f;
        public Vector2 position;

        public override async Task PreCast(Card cardInstance) {
            position = await GetPositionFromUser();
        }

        private async Task<Vector2> GetPositionFromUser() {
            // Create a TaskCompletionSource to wait for the callback
            var tcs = new TaskCompletionSource<Vector2>();
            
            
            MousePositionSelector.Instance.BeginSelection(
                (spawnPos) => tcs.SetResult(spawnPos)
            );
            
            // Wait for the user to select a position
            return await tcs.Task;
        }
        public override void Activate(Card cardInstance) {
        }
    }
}