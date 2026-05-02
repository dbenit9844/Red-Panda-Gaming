using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private PlayerHealth health;

    void Awake()
    {
        health = GetComponentInParent<PlayerHealth>();
        if (health == null)
        {
            Debug.LogError($"[Hurtbox] No PlayerHealth found in parent of {gameObject.name}!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hitbox hitbox = other.GetComponent<Hitbox>();
        if (hitbox == null) return;

        if (other.transform.root == transform.root) return;

        if (health == null) return;

        Vector2 direction = (transform.position - other.transform.position).normalized;
        health.TakeDamage(hitbox.damage, direction * hitbox.knockbackForce);
    }
}