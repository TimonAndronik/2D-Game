using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogUI; // Вікно діалогу
    public Text dialogText; // Поле для тексту
    public Button nextButton; // Кнопка "Далі"

    [Header("Dialog Data")]
    [TextArea(2, 5)]
    public string[] dialogLines; // Масив реплік
    private int currentLineIndex = 0; // Поточний індекс репліки

    private void Start()
    {
        dialogUI.SetActive(false); // Сховати діалог спочатку
        nextButton.onClick.AddListener(NextLine); // Прив'язати функцію до кнопки
    }

    public void StartDialog(string[] lines)
    {
        dialogLines = lines;
        currentLineIndex = 0;
        dialogUI.SetActive(true); // Показати діалогове вікно
        ShowCurrentLine(); // Відобразити перший рядок
    }

    private void ShowCurrentLine()
    {
        if (currentLineIndex < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLineIndex]; // Відобразити текст
        }
        else
        {
            EndDialog(); // Завершити діалог
        }
    }

    private void NextLine()
    {
        currentLineIndex++; // Перейти до наступного рядка
        ShowCurrentLine(); // Оновити текст
    }

    private void EndDialog()
    {
        dialogUI.SetActive(false); // Сховати діалогове вікно
    }
}
