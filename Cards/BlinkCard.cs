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
        public Sprite selectorSprite;

        public override async Task PreCast(Card cardInstance) {
            position = await GetPositionFromUser(cardInstance);
        }

        private async Task<Vector2> GetPositionFromUser(Card cardInstance) {
            // Create a TaskCompletionSource to wait for the callback
            var tcs = new TaskCompletionSource<Vector2>();
            
            
            MousePositionSelector.Instance.BeginSelection(
                (spawnPos) => tcs.SetResult(spawnPos),
                customSprite: selectorSprite,
                maxDistance: baseMaxDistance + (.5f * cardInstance.data.level),
                referencePoint: Player.Instance.location.position
            );
            
            // Wait for the user to select a position
            return await tcs.Task;
        }
        public override void Activate(Card cardInstance) {
            if (position != null) {
                Player.Instance.transform.position = position;
            }
        }
    }
}