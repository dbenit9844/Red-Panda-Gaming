using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private PlayerHealth health;

    void Awake()
    {
        health = GetComponentInParent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hitbox hitbox = other.GetComponent<Hitbox>();
        if (hitbox == null) return;

        // Don't hit yourself
        if (other.transform.root == transform.root) return;

        if (health != null)
        {
            Vector2 direction = (transform.position - other.transform.position).normalized;
            health.TakeDamage(hitbox.damage, direction * hitbox.knockbackForce);
        }
    }
}
