using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMapSelector()
    {
        SceneManager.LoadScene("Map Selector");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); // <-- FIXED (space added)
    }
}