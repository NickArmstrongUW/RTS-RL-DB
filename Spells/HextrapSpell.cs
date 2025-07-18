using UnityEngine;

public class HextrapSpell: MonoBehaviour
{
    public float maxDuration = 60f; // Set a default duration
    public float lifetime;
    public float stunDuration;
    public float damage;


    void Update() {
        lifetime += Time.deltaTime;
        if (lifetime >= maxDuration) {
            Destroy(gameObject);
        }
    }

    public void Cast(float duration, float damage) {
        stunDuration = duration;
        this.damage = damage;
        Debug.Log($"Hextrap cast with stun duration: {stunDuration}, damage: {damage}");
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"Hextrap triggered by: {other.name}");
        
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null) {
            Debug.Log($"Stunning enemy: {enemy.name} for {stunDuration} seconds");
            enemy.Stun(stunDuration);
            
            if(damage > 0) {
                enemy.TakeDamage(damage);
            }
            
            // Don't destroy the trap immediately - let it stay for the full duration
            // Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}