using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int maxLives = 3;
    public int currentLives;

    public Transform respawnPoint;

    void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;

        Debug.Log(gameObject.name + " Health: " + currentHealth);
        Debug.Log(gameObject.name + " Lives: " + currentLives);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log(gameObject.name + " took damage. Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            LoseLife();
        }
    }

    public void InstantKill()
    {
        currentHealth = 0;
        LoseLife();
    }

    void LoseLife()
    {
        currentLives--;

        Debug.Log(gameObject.name + " lost a life. Lives left: " + currentLives);

        if (currentLives <= 0)
        {
            Debug.Log(gameObject.name + " lost the game!");
            gameObject.SetActive(false); // player disappears
        }
        else
        {
            Respawn();
        }
    }

    void Respawn()
    {
        currentHealth = maxHealth;

        // Move player to spawn point
        transform.position = respawnPoint.position;

        Debug.Log(gameObject.name + " respawned with full health!");
    }
}