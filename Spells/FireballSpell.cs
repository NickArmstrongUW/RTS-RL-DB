using UnityEngine;


public class FireballSpell: MonoBehaviour
{
    public float travelSpeed = 5f;
    private Rigidbody2D rb;
    public float damage;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 0f;           // Linear drag
        rb.angularDamping = 0f;  
    }

    public void Cast(Vector2 dir, float damage) {
        this.damage = damage;
        // Debug.Log("Casting fireball with damage: " + damage);
        if (rb == null) {
            Debug.LogError("Rigidbody2D is null!");
            return;
        }
        
        rb.linearVelocity = dir.normalized * travelSpeed;  // Use velocity instead of linearVelocity
        
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // void Update()
    // {
    // }

    public void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("Fireball hit " + other.name);
        other.GetComponent<Damageable>().TakeDamage(damage);
        Destroy(gameObject);
    }
}