using UnityEngine;
using UnityEngine.SceneManagement; // ��� ����������� ��� ���������� ���

public class Portal : MonoBehaviour
{
    [SerializeField]
    private Animator portalAnimator; // ������� �������

    [SerializeField]
    private float closeAnimationDuration = 2f; // ��������� ������� �������� �������

    [SerializeField]
    private string gameOverSceneName; // ��'� ����� ���������� ��� (��� ������� ������� ��� ������ � ���)

    private bool _isTriggered = false; // ��� �������� ���������� �����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTriggered) return; // ��������, ��� ������ �� ������������ ����� ����

        if (collision.CompareTag("Player")) // ���� ����� ������� � ������
        {
            _isTriggered = true; // ���������, �� ������ ����������

            // ��������� ������� �������� �������
            if (portalAnimator != null && portalAnimator.GetCurrentAnimatorStateInfo(0).IsName("Portal_Idle"))
            {
                portalAnimator.SetTrigger("Close");
            }

            // ������ �����
            collision.gameObject.SetActive(false);

            // ��������� ���������� ��� ���� �������
            Invoke(nameof(EndGame), closeAnimationDuration);
        }
    }

    private void EndGame()
    {
        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            // ������������ ����� ���������� ���
            SceneManager.LoadScene(gameOverSceneName);
        }
        else
        {
            // ���������� ��� (����� � �����)
            Debug.Log("��� ���������!");
            Application.Quit();
        }

        // ������� ������ ���� ���������� ���
        Destroy(gameObject);
    }
}
