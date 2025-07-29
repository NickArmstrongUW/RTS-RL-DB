using UnityEngine;
using System.Collections;

public class Enemy : Damageable
{
    public float attackRange;
    public float baseDamage;
    public float damageModifier = 1f;
    public float attackSpeed;
    public float attackTimer;

    public bool isStunned;
    public float stunDuration;
    public float moveSpeed = 5f;

    // current logic allows for stacking of stuns, but could change it so they only stack with diminishing returns or sets the stun to the max of current vs new duration
    public virtual void Stun(float duration) {
        isStunned = true;
        stunDuration += duration;
    }

    public virtual void Awake() {
        attackTimer = 0;
    }

    // TODO move logic for movement, attacking, and handling stuns here so enemies all follow the behavior of move into attack range and then swing
    public virtual void Update() {
        if (isStunned) {
            stunDuration -= Time.deltaTime;

            if (stunDuration <= 0) {
                stunDuration = 0;
                isStunned = false;
            } else {
                return;
            }
        }

        float distance = Vector3.Distance(transform.position, Player.Instance.location.position);
        if (distance > attackRange) {
            MoveTowardsPlayer();
        } else {
        // TODO: handle attack range and movement
            if (attackSpeed == 0) {
                return;
            }
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0) {
                Attack();
                attackTimer = 1 / attackSpeed;
            }
        }
    }

    public virtual void MoveTowardsPlayer() {
        
        Vector3 direction = (Player.Instance.location.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        
        // // Make enemy face the direction it's moving
        // if (direction != Vector3.zero) {
        //     transform.up = direction; // Assuming the enemy's "forward" is up
        // }
    }

    public virtual void Attack() {
        // do nothing, override this
        Debug.Log("Attack call not overriden by enemy");
    }

    public override void Die() {
        Debug.Log("Enemy Died");
        EnemySpawner.Instance.enemyCount--;
        Destroy(gameObject);
    }
}