using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMapSelector()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map Selector");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu"); // <-- FIXED (space added)
    }
}