using UnityEngine;
using UnityEngine.SceneManagement; // Для роботи з сценою

public class SceneChanger : MonoBehaviour
{
    // Метод для переходу до сцени за її назвою
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Завантажуємо сцену за назвою
    }

    // Метод для переходу до наступної сцени (по порядку в Build Settings)
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Отримуємо поточний індекс сцени
        int nextSceneIndex = currentSceneIndex + 1; // Наступна сцена
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Завантажуємо наступну сцену
        }
        else
        {
            Debug.Log("Це була остання сцена в Build Settings.");
        }
    }

    // Метод для переходу до попередньої сцени
    public void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Отримуємо поточний індекс сцени
        int previousSceneIndex = currentSceneIndex - 1; // Попередня сцена
        if (previousSceneIndex >= 0)
        {
            SceneManager.LoadScene(previousSceneIndex); // Завантажуємо попередню сцену
        }
        else
        {
            Debug.Log("Це була перша сцена в Build Settings.");
        }
    }
}
