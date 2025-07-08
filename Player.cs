// controls the state of the player
using UnityEngine;

class Player: Damageable
{
    public static Player Instance;
    public int damageModifier;
    public int speed;
    public int attackRange;
    public int attackCooldown;
    public int attackDamage;
    public Transform location;

    void Awake() {
        Instance = this;
        health = 100;
        maxHealth = 100;
        shield = 0;
        speed = 5;
        attackRange = 1;
        attackCooldown = 1;
        attackDamage = 10;
        location = transform;

        UpdateHealthBar();
        UpdateShieldBar();
    }

    public Collider2D GetCollisionBody() => GetComponent<Collider2D>();

    // player has no limit to their max shield
    public override void RestoreShield(float amount) {
        shield += amount;
        if (shield > maxShield) {
            maxShield = shield;
        }
        UpdateShieldBar();
    }
}