using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public string playerName = "Player1";

    public int maxHealth = 100;
    public int currentHealth;

    public int maxLives = 3;
    public int currentLives;

    public Transform respawnPoint;

    private bool gameOverTriggered = false;

    void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;
    }

    public void TakeDamage(int damage)
    {
        if (gameOverTriggered) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            LoseLife();
        }
    }

  
    public void InstantKill()
    {
        if (gameOverTriggered) return;

        currentHealth = 0;
        LoseLife();
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
    }

    void TriggerGameOver()
    {
        gameOverTriggered = true;

        string winnerName;

        if (playerName == "Player1")
        {
            winnerName = "Player2";
        }
        else
        {
            winnerName = "Player1";
        }

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