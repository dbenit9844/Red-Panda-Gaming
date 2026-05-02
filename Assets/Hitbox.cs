using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage = 10;
    public float knockbackForce = 5f;
    private Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    public void EnableHitbox()
    {
        col.enabled = true;
    }

    public void DisableHitbox()
    {
        col.enabled = false;
    }
}