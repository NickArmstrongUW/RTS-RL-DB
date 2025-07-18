using UnityEngine;

public class Slime: Enemy
{

    public override void Attack() {
        Player.Instance.TakeDamage(baseDamage);
    }
}