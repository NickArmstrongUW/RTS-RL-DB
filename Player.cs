// controls the state of the player
using UnityEngine;
using TMPro;

class Player: Damageable
{
    public static Player Instance;
    public int damageModifier;
    public int speed;
    public int attackRange;
    public int attackCooldown;
    public int attackDamage;
    public Transform location;
    public int currentMana;
    public int maxMana = 10;
    public float manaRegenTime = 2f; // seconds per mana
    public float manaRegenTimer;
    public int startingMana = 0;

    public TextMeshProUGUI manaText;

    void Awake() {
        Instance = this;
        health = 100;
        maxHealth = 100;
        shield = 0;
        speed = 5;
        attackRange = 1;
        attackCooldown = 1;
        attackDamage = 10;
        currentMana = startingMana;
        location = transform;
        manaRegenTimer = 0;

        UpdateHealthBar();
        UpdateShieldBar();
        manaText.text = currentMana.ToString();
    }

    void Update() {
        manaRegenTimer += Time.deltaTime;
        if (manaRegenTimer >= manaRegenTime) {
            gainMana(1);
            manaRegenTimer = 0;
        }
    }

    public Collider2D GetCollisionBody() => GetComponent<Collider2D>();

    // player has no limit to their max shield
    public override void GainShields(float amount) {
        shield += amount;
        if (shield > maxShield) {
            maxShield = shield;
        }
        UpdateShieldBar();
    }

    public void spendMana(int amount) {
        currentMana -= amount;
        if (currentMana < 0) {
            currentMana = 0;
        }
        manaText.text = currentMana.ToString();
    }

    public void gainMana(int amount) {
        if (currentMana + amount > maxMana) {
            currentMana = maxMana;
            // Debug.Log("Player set to maxMana");
        } else {
            currentMana += amount;
            // Debug.Log("Player gained " + amount + " mana");
        }
        manaText.text = currentMana.ToString();
    }

}