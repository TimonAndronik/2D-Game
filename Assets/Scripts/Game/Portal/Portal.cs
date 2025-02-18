using UnityEngine;
using UnityEngine.SceneManagement; // Для перезапуску або завершення гри

public class Portal : MonoBehaviour
{
    [SerializeField]
    private Animator portalAnimator; // Аніматор порталу

    [SerializeField]
    private float closeAnimationDuration = 2f; // Тривалість анімації закриття порталу

    [SerializeField]
    private string gameOverSceneName; // Ім'я сцени завершення гри (або залиште порожнім для виходу з гри)

    private bool _isTriggered = false; // Щоб уникнути повторного входу

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTriggered) return; // Перевірка, щоб портал не спрацьовував кілька разів

        if (collision.CompareTag("Player")) // Якщо герой входить у портал
        {
            _isTriggered = true; // Позначаємо, що портал активовано

            // Викликаємо анімацію закриття порталу
            if (portalAnimator != null && portalAnimator.GetCurrentAnimatorStateInfo(0).IsName("Portal_Idle"))
            {
                portalAnimator.SetTrigger("Close");
            }

            // Ховаємо героя
            collision.gameObject.SetActive(false);

            // Викликаємо завершення гри після анімації
            Invoke(nameof(EndGame), closeAnimationDuration);
        }
    }

    private void EndGame()
    {
        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            // Завантаження сцени завершення гри
            SceneManager.LoadScene(gameOverSceneName);
        }
        else
        {
            // Завершення гри (тільки в збірці)
            Debug.Log("Гра завершена!");
            Application.Quit();
        }

        // Знищуємо портал після завершення гри
        Destroy(gameObject);
    }
}
