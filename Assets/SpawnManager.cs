using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Color player1Color = Color.blue;
    public Color player2Color = Color.red;
    public PlayerUI playerUI;

    private PlayerHealth player1Health;
    private PlayerHealth player2Health;

    void Start()
    {
        PlayerInputManager.instance.JoinPlayer(0);
        PlayerInputManager.instance.JoinPlayer(1);
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        if (player.playerIndex == 0)
        {
            player.transform.position = spawnPoint1.position;
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            health.respawnPoint = spawnPoint1;
            player1Health = health;
            SetPlayerColor(player, player1Color);
        }
        else
        {
            player.transform.position = spawnPoint2.position;
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            health.respawnPoint = spawnPoint2;
            player2Health = health;
            SetPlayerColor(player, player2Color);
        }

        if (player1Health != null && player2Health != null && playerUI != null)
        {
            playerUI.player1Health = player1Health;
            playerUI.player2Health = player2Health;
        }
    }

    private void SetPlayerColor(PlayerInput player, Color color)
    {
        SpriteRenderer sr = player.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = color;
        }
    }
}