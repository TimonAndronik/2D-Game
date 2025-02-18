using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen; // Екран завантаження
    [SerializeField] private GameObject mainMenu; // Основне меню

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider; // Слайдер для відображення прогресу завантаження

    [SerializeField] private float sliderSmoothSpeed = 3f; // Швидкість плавного заповнення слайдера
    [SerializeField] private float minimumLoadingTime = 3f; // Мінімальний час завантаження

    public void LoadLevelBtn(string levelToLoad)
    {
        mainMenu.SetActive(false); // Ховаємо основне меню
        loadingScreen.SetActive(true); // Показуємо екран завантаження
        StartCoroutine(LoadLevelASync(levelToLoad)); // Запускаємо асинхронне завантаження
    }

    IEnumerator LoadLevelASync(string levelToLoad)
    {
        float elapsedTime = 0f; // Час, що минув
        float targetSliderValue = 0f; // Поточна ціль для слайдера

        // Додаємо початкову затримку (можна налаштувати на бажану кількість секунд)
        yield return new WaitForSeconds(0.5f);

        // Починаємо асинхронне завантаження сцени
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        loadOperation.allowSceneActivation = false; // Забороняємо автоматичне перемикання сцени

        while (!loadOperation.isDone)
        {
            elapsedTime += Time.deltaTime;

            // Оновлюємо цільове значення слайдера
            targetSliderValue = Mathf.Clamp01(loadOperation.progress / 0.9f);

            // Плавне заповнення слайдера
            loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, targetSliderValue, sliderSmoothSpeed * Time.deltaTime);

            // Якщо час завантаження достатній і прогрес наближається до 90%, розблокуємо сцену
            if (elapsedTime >= minimumLoadingTime && loadOperation.progress >= 0.9f)
            {
                break;
            }

            yield return null;
        }

        // Завершальне плавне заповнення слайдера до 100%
        while (loadingSlider.value < 1f)
        {
            loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, 1f, sliderSmoothSpeed * Time.deltaTime);
            yield return null;
        }

        // Дозволяємо активацію сцени після того, як прогрес досяг 100%
        loadOperation.allowSceneActivation = true;
    }
}
