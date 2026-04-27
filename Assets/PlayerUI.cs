using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public PlayerHealth player1Health;
    public PlayerHealth player2Health;

    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;

    void Update()
    {
        if (player1Health != null)
        {
            player1Text.text = "P1 HP: " + player1Health.currentHealth +
                               "\nLives: " + player1Health.currentLives;
        }

        if (player2Health != null)
        {
            player2Text.text = "P2 HP: " + player2Health.currentHealth +
                               "\nLives: " + player2Health.currentLives;
        }
    }
}