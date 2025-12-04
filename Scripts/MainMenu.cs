using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the Game scene
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // This line is for testing inside Unity Editor
    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #endif

    }
}   
