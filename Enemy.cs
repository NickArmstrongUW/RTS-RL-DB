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



    public virtual void Die() {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }
}