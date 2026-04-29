using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public string playerName = "Player1";
    public int maxHealth = 100;
    public int currentHealth;
    public int maxLives = 3;
    public int currentLives;
    public Transform respawnPoint;
    public float spawnProtectionTime = 3f;
    private bool isInvincible = false;
    private bool gameOverTriggered = false;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnProtection());
    }

    public void TakeDamage(int damage, Vector2 knockback)
    {
        if (gameOverTriggered) return;
        if (isInvincible) return;

        currentHealth -= damage;
        if (rb != null)
        {
            rb.linearVelocity = knockback;
        }
        Debug.Log(playerName + " took " + damage + " damage");
        if (currentHealth <= 0)
        {
            LoseLife();
        }
    }

    public void TakeDamage(int damage)
    {
        TakeDamage(damage, Vector2.zero);
    }

    public void InstantKill()
    {
        if (gameOverTriggered) return;
        currentHealth = 0;
        LoseLife();
    }

    private IEnumerator SpawnProtection()
    {
        isInvincible = true;
        yield return new WaitForSeconds(spawnProtectionTime);
        isInvincible = false;
    }

    void LoseLife()
    {
        currentLives--;
        if (currentLives <= 0)
        {
            TriggerGameOver();
        }
        else
        {
            Respawn();
        }
    }

    void Respawn()
    {
        currentHealth = maxHealth;
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
        StartCoroutine(SpawnProtection());
    }

    void TriggerGameOver()
    {
        gameOverTriggered = true;
        string winnerName = (playerName == "Player1") ? "Player2" : "Player1";
        GameOverManager gameOver = FindAnyObjectByType<GameOverManager>();
        if (gameOver != null)
        {
            gameOver.ShowGameOver(winnerName);
        }
        else
        {
            Debug.LogError("GameOverManager NOT FOUND");
        }
        gameObject.SetActive(false);
    }
}