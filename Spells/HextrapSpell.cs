using UnityEngine;

public class HextrapSpell: MonoBehaviour
{
    public float maxDuration;
    public float lifetime;
    public float stunDuration;
    public float damage;

    public void update() {
        lifetime += Time.deltaTime;
        if (lifetime >= maxDuration) {
            Destroy(this);
        }
    }

    public void Cast(float duration, float damage) {
        stunDuration = duration;
        this.damage = damage;
    }

     public void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("Fireball hit " + other.name);
        other.GetComponent<Enemy>().Stun(stunDuration);
        if(damage > 0) {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}