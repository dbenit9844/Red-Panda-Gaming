using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public Transform player1Spawn;
    public Transform player2Spawn;

    public PlayerUI playerUI;

    void Start()
    {
        GameObject p1 = SpawnPlayer(player1Prefab, player1Spawn);
        GameObject p2 = SpawnPlayer(player2Prefab, player2Spawn);

        playerUI.player1Health = p1.GetComponent<PlayerHealth>();
        playerUI.player2Health = p2.GetComponent<PlayerHealth>();
    }

    GameObject SpawnPlayer(GameObject playerPrefab, Transform spawnPoint)
    {
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

        PlayerHealth health = newPlayer.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.respawnPoint = spawnPoint;
        }

        return newPlayer;
    }
}