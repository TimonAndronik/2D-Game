using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] hearts; // Масив об'єктів сердець
    private int currentHealth;  // Поточна кількість життів
    public Animator playerAnimator; // Посилання на Animator для анімації смерті
    public string gameOverSceneName = "GameOver"; // Назва сцени для завершення гри

    private void Start()
    {
        currentHealth = hearts.Length;
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;

            // Видаляємо серце з інтерфейсу
            if (hearts[currentHealth] != null)
            {
                Destroy(hearts[currentHealth]);
            }
        }

        // Якщо сердець більше немає
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        // Запускаємо анімацію смерті
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Die");
        }

        // Почекати кілька секунд, поки анімація не закінчиться
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        // Чекаємо певний час (можна налаштувати)
        yield return new WaitForSeconds(1.2f); // Затримка для анімації смерті (змінити за необхідності)

        // Переходимо на наступну сцену (GameOver)
        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
        else
        {
            // Якщо сцена не вказана, виходимо з гри
            Debug.Log("Гра завершена!");
            Application.Quit();
        }
    }
}
