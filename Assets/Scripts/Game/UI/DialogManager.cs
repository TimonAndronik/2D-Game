using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogUI; // ³��� ������
    public Text dialogText; // ���� ��� ������
    public Button nextButton; // ������ "���"

    [Header("Dialog Data")]
    [TextArea(2, 5)]
    public string[] dialogLines; // ����� �����
    private int currentLineIndex = 0; // �������� ������ ������

    private void Start()
    {
        dialogUI.SetActive(false); // ������� ����� ��������
        nextButton.onClick.AddListener(NextLine); // ����'����� ������� �� ������
    }

    public void StartDialog(string[] lines)
    {
        dialogLines = lines;
        currentLineIndex = 0;
        dialogUI.SetActive(true); // �������� �������� ����
        ShowCurrentLine(); // ³��������� ������ �����
    }

    private void ShowCurrentLine()
    {
        if (currentLineIndex < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLineIndex]; // ³��������� �����
        }
        else
        {
            EndDialog(); // ��������� �����
        }
    }

    private void NextLine()
    {
        currentLineIndex++; // ������� �� ���������� �����
        ShowCurrentLine(); // ������� �����
    }

    private void EndDialog()
    {
        dialogUI.SetActive(false); // ������� �������� ����
    }
}
