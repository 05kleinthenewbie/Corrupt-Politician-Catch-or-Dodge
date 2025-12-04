using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text moneyText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;

    [Header("Gameplay")]
    public float score = 0f;
    public float scoreSpeed = 5f;
    private int money = 0;
    private bool gameOver = false;

    [Header("Pause Menu")]
    public GameObject pausePanel;

    void Start()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        UpdateUI();
    }

    void Update()
    {
        if (Time.timeScale == 0f || gameOver)
            return;

        score += scoreSpeed * Time.deltaTime;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + Mathf.FloorToInt(score);

        if (moneyText != null)
            moneyText.text = "Money: " + money;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    // -------------------------
    // Pause
    // -------------------------
    public void PauseGame()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // -------------------------
    // GAME OVER
    // -------------------------
    public void GameOver()
    {
        gameOver = true;

        Time.timeScale = 0f; // Stop the game

        // Show the panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        int finalScore = Mathf.FloorToInt(score);

        // Display final score
        if (finalScoreText != null)
            finalScoreText.text = "Score: " + finalScore;

        // Save and show high score
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (finalScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
            highScore = finalScore;
        }

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }

    // -------------------------
    // Buttons
    // -------------------------
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
