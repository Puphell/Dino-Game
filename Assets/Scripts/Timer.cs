using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController playerController;

    private float elapsedTime = 0f;
    private float speedIncreaseInterval = 30f;

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // Her saniye bir puan ekle
        if (elapsedTime >= 1f)
        {
            elapsedTime -= 1f;
            gameManager.AddScore(1); // Burada eksik arg�man tamamland�
        }

        // Her 30 saniyede bir h�z� art�r
        if (elapsedTime >= speedIncreaseInterval)
        {
            elapsedTime -= speedIncreaseInterval;
            Time.timeScale += 0.1f;
            playerController.animator.speed += 0.1f;
        }
    }
}
