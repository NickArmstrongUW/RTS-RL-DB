using UnityEngine;
using UnityEngine.UI;
using TMPro;

// class to be extended by objects that can take damage
public class Damageable : MonoBehaviour
{
    public float health = 1f;
    public float maxHealth = 100f;
    public float shield = 0f;
    public float maxShield = 100f;
    public float damageResistance = 0f;
    public float damageNegation = 0f;

    public Slider healthBar;
    public TextMeshProUGUI healthText;
    public Slider shieldBar;
    public TextMeshProUGUI shieldText;

    public virtual void TakeDamage(float damage) {
        Debug.Log("Taking Damage: " + damage);
        float damageRemainder = (damage * (1-damageResistance)) - damageNegation;
        Debug.Log("Damage after resistance: " + damageRemainder);
        if (damageRemainder < 0) {
            damageRemainder = 0;
        }
        if (shield > 0) {
            float temp = shield;
            shield -= damageRemainder;
            damageRemainder -= temp;
            UpdateShieldBar();
        }
        if (damageRemainder > 0) {
            health -= damageRemainder;
            UpdateHealthBar();
            if (health <= 0) {
                Die();
            }
        }
        Debug.Log("Health Remainder: " + health + " Shield Remainder: " + shield);
    }

    public virtual void Die() {
        Debug.Log("Died");
        Destroy(gameObject);
    }

    protected virtual void UpdateHealthBar() {
        if (healthBar != null) {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }
        if (healthText != null) {
            healthText.text =  $"{health} / {maxHealth}";
        }
    }

    protected virtual void UpdateShieldBar() {
        if (shieldBar != null) {
            shieldBar.maxValue = maxShield;
            shieldBar.value = shield;
        }
        if (shieldText != null) {
            shieldText.text = $"{shield} / {maxShield}";
        }
    }

    public virtual void RestoreHealth(float amount) {
        health += amount;
        if (health > maxHealth) {
            health = maxHealth;
        }
        UpdateHealthBar();
    }
    
    public virtual void RestoreShield(float amount) {
        shield += amount;
        if (shield > maxShield) {
            shield = maxShield;
        }
        UpdateShieldBar();
    }
}
