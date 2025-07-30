using UnityEngine;


public class ShadowMage: Enemy
{
    void Start ()
    {
        health = 40;
        shield = 0;
        damageResistance = 0.1f;
        damageNegation = 1;
        attackSpeed = 0.25f;
        attackRange = 5;
        baseDamage = 5;
        // location = transform; 
    }

    public override void Attack() {
        Debug.Log("Attacking");
    }

}