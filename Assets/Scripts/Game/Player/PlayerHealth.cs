using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] hearts; // ����� ��'���� �������
    private int currentHealth;  // ������� ������� �����
    public Animator playerAnimator; // ��������� �� Animator ��� ������� �����
    public string gameOverSceneName = "GameOver"; // ����� ����� ��� ���������� ���

    private void Start()
    {
        currentHealth = hearts.Length;
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;

            // ��������� ����� � ����������
            if (hearts[currentHealth] != null)
            {
                Destroy(hearts[currentHealth]);
            }
        }

        // ���� ������� ����� ����
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        // ��������� ������� �����
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Die");
        }

        // �������� ����� ������, ���� ������� �� ����������
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        // ������ ������ ��� (����� �����������)
        yield return new WaitForSeconds(1.2f); // �������� ��� ������� ����� (������ �� �����������)

        // ���������� �� �������� ����� (GameOver)
        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
        else
        {
            // ���� ����� �� �������, �������� � ���
            Debug.Log("��� ���������!");
            Application.Quit();
        }
    }
}
