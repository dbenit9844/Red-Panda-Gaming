using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI winnerText;

    public string startMenuScene = "MainMenu";

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver(string winnerName)
    {
        gameOverPanel.SetActive(true);

        winnerText.text = winnerName + " Wins!";

        Time.timeScale = 0f;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(startMenuScene);
    }
}