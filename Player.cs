// controls the state of the player
using UnityEngine;

class Player: MonoBehaviour
{
    public static Player instance;
    public int health;
    public int damage;
    public int speed;
    public int attackRange;
    public int attackCooldown;
    public int attackDamage;
    public Transform location;

    void Awake() {
        instance = this;
        health = 100;
        damage = 10;
        speed = 5;
        attackRange = 1;
        attackCooldown = 1;
        attackDamage = 10;
        location = transform;
    }
}