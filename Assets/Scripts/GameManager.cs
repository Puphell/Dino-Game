using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text currentScoreText;
    public Text highScoreText;
    public Text scoreText;

    private int currentScore = 0;
    private int highScore = 0;
    private int score = 0;

    public GameObject gameOverPanel;

    public PlayerController playerController;

    public AudioSource milestoneSound;

    private float milestoneTime = 60f;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();

        gameOverPanel.SetActive(false);
        InvokeRepeating(nameof(PlayMilestoneSound), milestoneTime, milestoneTime);
    }

    private void Update()
    {
        scoreText.text = $"Score: {currentScore}";
    }

    public void AddScore(int score)
    {
        currentScore += score;
        UpdateCurrentScoreText();

        // Eðer mevcut skor en yüksek skoru geçerse, güncelle
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore); // En yüksek skoru kaydet
            UpdateHighScoreText();
        }
    }

    public void GameOver()
    {
        // Oyunu durdur
        Time.timeScale = 0f;

        // En yüksek skor kaydýný kontrol et
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        gameOverPanel.SetActive(true);

        // Eðer bir Game Over paneliniz varsa buraya devreye sokabilirsiniz
        Debug.Log("Game Over!");
    }

    private void UpdateCurrentScoreText()
    {
        currentScoreText.text = "Score: " + currentScore.ToString();
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    private void PlayMilestoneSound()
    {
        if (playerController != null && playerController.animator.GetBool("isRunning"))
        {
            milestoneSound.Play();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Game");
    }
}
