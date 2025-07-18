using UnityEngine;
using System;
using CardSystem;
using System.Threading.Tasks;

// 2

namespace Cards
{
    [CreateAssetMenu(menuName = "Card/Restore")]
    public class RestoreCard: CardData
    {
        public float baseHealing = 5f;

        public void Awake() {
            name = "Restore";
            // TODO update to show exact amount of health restored
            description = $"Restore health";
        }

         public override async Task PreCast(Card cardInstance) {
            Debug.Log("Restore pre-cast");
            // await WaitForUserClick();
        }

        // no longer using, keeping code in case it gets brought back. Maybe as a setting
        private async Task WaitForUserClick() {
            var tcs = new TaskCompletionSource<bool>();
            
            SimpleClickSelector.Instance.WaitForClick(() => tcs.SetResult(true));
            
            await tcs.Task;
        }

        public override void Activate(Card cardInstance) 
        {
            Debug.Log("Restore activated");
            Player.Instance.RestoreHealth(baseHealing + (5 * cardInstance.data.level));
        }
    }
}