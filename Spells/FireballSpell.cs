using UnityEngine;


public class FireballSpell: MonoBehaviour
{
    public Vector2 fireDirection;
    public float travelSpeed = 5f;

    public void Cast(Vector2 dir) {
        fireDirection = dir.normalized;
        Debug.Log("Casting fireball");
    }

    void Update()
    {
        transform.Translate(fireDirection * travelSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Fireball hit " + other.name);
    }
}