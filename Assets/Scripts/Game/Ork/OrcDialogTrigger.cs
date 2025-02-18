using UnityEngine;

public class OrcDialogTrigger : MonoBehaviour
{
    [SerializeField]
    private Collider2D dialogTriggerCollider; // ��������� �� ����-�������� ��� ������
    [SerializeField]
    private DialogManager dialogManager; // ��������� �� �������� ������

    private bool isPlayerInTrigger = false; // �� ������� � ��� �������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����������, �� �� ������� � �� �������� ������� ��� ������
        if (collision.CompareTag("Player") && collision == dialogTriggerCollider)
        {
            isPlayerInTrigger = true; // ������� ������ � ���� ������
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ����������, �� ������� ������ �� ���� ������
        if (collision.CompareTag("Player") && collision == dialogTriggerCollider)
        {
            isPlayerInTrigger = false; // ������� ������ �� ���� ������
        }
    }

    private void Update()
    {
        // �������� ������ "E" � �� ������� � ������
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // ������ ������ ��� ����������� ���������
            string[] dialogLines = new string[]
            {
                "� ��� ����. �� ��� �������?",
                "�����: ��� ������� ��������, ��� ��������� �����.",
                "����: �� ����������� 50 �����. ������ ����?"
            };

            dialogManager.StartDialog(dialogLines); // ������ ������
        }
    }
}
