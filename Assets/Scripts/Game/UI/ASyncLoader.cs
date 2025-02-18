using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen; // ����� ������������
    [SerializeField] private GameObject mainMenu; // ������� ����

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider; // ������� ��� ����������� �������� ������������

    [SerializeField] private float sliderSmoothSpeed = 3f; // �������� �������� ���������� ��������
    [SerializeField] private float minimumLoadingTime = 3f; // ̳�������� ��� ������������

    public void LoadLevelBtn(string levelToLoad)
    {
        mainMenu.SetActive(false); // ������ ������� ����
        loadingScreen.SetActive(true); // �������� ����� ������������
        StartCoroutine(LoadLevelASync(levelToLoad)); // ��������� ���������� ������������
    }

    IEnumerator LoadLevelASync(string levelToLoad)
    {
        float elapsedTime = 0f; // ���, �� �����
        float targetSliderValue = 0f; // ������� ���� ��� ��������

        // ������ ��������� �������� (����� ����������� �� ������ ������� ������)
        yield return new WaitForSeconds(0.5f);

        // �������� ���������� ������������ �����
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        loadOperation.allowSceneActivation = false; // ����������� ����������� ����������� �����

        while (!loadOperation.isDone)
        {
            elapsedTime += Time.deltaTime;

            // ��������� ������� �������� ��������
            targetSliderValue = Mathf.Clamp01(loadOperation.progress / 0.9f);

            // ������ ���������� ��������
            loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, targetSliderValue, sliderSmoothSpeed * Time.deltaTime);

            // ���� ��� ������������ �������� � ������� ����������� �� 90%, ���������� �����
            if (elapsedTime >= minimumLoadingTime && loadOperation.progress >= 0.9f)
            {
                break;
            }

            yield return null;
        }

        // ����������� ������ ���������� �������� �� 100%
        while (loadingSlider.value < 1f)
        {
            loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, 1f, sliderSmoothSpeed * Time.deltaTime);
            yield return null;
        }

        // ���������� ��������� ����� ���� ����, �� ������� ����� 100%
        loadOperation.allowSceneActivation = true;
    }
}
