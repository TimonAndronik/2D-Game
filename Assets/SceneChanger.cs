using UnityEngine;
using UnityEngine.SceneManagement; // ��� ������ � ������

public class SceneChanger : MonoBehaviour
{
    // ����� ��� �������� �� ����� �� �� ������
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // ����������� ����� �� ������
    }

    // ����� ��� �������� �� �������� ����� (�� ������� � Build Settings)
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // �������� �������� ������ �����
        int nextSceneIndex = currentSceneIndex + 1; // �������� �����
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // ����������� �������� �����
        }
        else
        {
            Debug.Log("�� ���� ������� ����� � Build Settings.");
        }
    }

    // ����� ��� �������� �� ���������� �����
    public void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // �������� �������� ������ �����
        int previousSceneIndex = currentSceneIndex - 1; // ��������� �����
        if (previousSceneIndex >= 0)
        {
            SceneManager.LoadScene(previousSceneIndex); // ����������� ��������� �����
        }
        else
        {
            Debug.Log("�� ���� ����� ����� � Build Settings.");
        }
    }
}
