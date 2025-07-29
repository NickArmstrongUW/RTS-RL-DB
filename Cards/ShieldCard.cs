using UnityEngine;
using System;
using CardSystem;
using System.Threading.Tasks;

// 3

namespace Cards
{
    [CreateAssetMenu(menuName = "Card/Shield")]
    public class ShieldCard: CardData
    {
        public float baseShield = 8f;

        public override void Awake() {
            name = "Shield";
            // TODO update to show exact amount of health restored
            description = $"Gain shields";
            base.Awake();
        }

         public override async Task PreCast(Card cardInstance) {
            Debug.Log("Shield pre-cast");
            // await WaitForUserClick();
        }

        // no longer using, keeping code in case it gets brought back. Maybe as a setting
        private async Task WaitForUserClick() {
            var tcs = new TaskCompletionSource<bool>();
            
            SimpleClickSelector.Instance.WaitForClick(() => tcs.SetResult(true));
            
            await tcs.Task;
        }

        public override void Activate(Card cardInstance) {
            Debug.Log("Shield activated");
            Player.Instance.GainShields(baseShield + (2 * cardInstance.data.level));
        }
    }
}