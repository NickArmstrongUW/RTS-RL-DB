using UnityEngine;
using System.Collections;

public class Enemy : Damageable
{
    public int speed;
    public int attackRange;
    public float attackCooldown;
    public float baseDamage;
    public float damageModifier = 1f;
    public float attackSpeed;
    public bool isStunned;
    public float stunDuration;

    // current logic allows for stacking of stuns, but could change it so they only stack with diminishing returns or sets the stun to the max of current vs new duration
    public virtual void Stun(float duration) {
        isStunned = true;
        stunDuration += duration;
    }

    // TODO move logic for movement, attacking, and handling stuns here so enemies all follow the behavior of move into attack range and then swing
    // virtual void Update() {

    // }

    // public virtual void Attack() {
    //     // do nothing, override this
    // }

    public virtual void Die() {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }
}