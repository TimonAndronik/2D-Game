using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    // �����, ���� ����������� ��� ��������� ������
    public void ExitGame()
    {
        Debug.Log("Game is exiting..."); // ��� ����������� � ������

        // ���� ��� �������� � ��������, �� ������������� EditorApplication ��� ������� ���
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else       
        Application.Quit(); // ��� ����� ��� ����� � ���
#endif

    }
}
