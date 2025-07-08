using UnityEngine;
using System.Collections;

public class EvilWizard : Enemy
{
    // Use this for initialization
    public FireballSpell fireballSpellPrefab;
    public Transform location;
    public Vector2 spawnOffset = new Vector2(-0.5f, -0.5f);
    private float attackTimer = 0f;

    void Start ()
    {
        health = 20;
        shield = 12;
        damageResistance = 0.1f;
        damageNegation = 1;
        attackSpeed = 0.5f;
        attackRange = 5;
        baseDamage = 5;
        location = transform;
        damageModifier = 1f;
        attackTimer = 1 / attackSpeed;
    }


    // Update is called once per frame
    void Update ()
    {
        // TODO: handle attack range and movement
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0) {
            if (Player.Instance.location != null) {
                Vector2 spawnPos = location.position + (Vector3)spawnOffset;
                FireballSpell spell = Instantiate(fireballSpellPrefab, spawnPos, Quaternion.identity);
                spell.Cast(Player.Instance.location.position - (Vector3)spawnPos, baseDamage * damageModifier);
            }
            attackTimer = 1 / attackSpeed;
        }
    }
}