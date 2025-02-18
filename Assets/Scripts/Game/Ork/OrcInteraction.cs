using UnityEngine;
using UnityEngine.UI;

public class OrcInteraction : MonoBehaviour
{
    public GameObject dialogUI; // Вікно діалогу
    public Text dialogText; // Текстове поле для реплік
    public string[] dialogLines; // Репліки діалогу
    public int costToHire = 5; // Вартість найму орка

    private PlayerMovement player; // Посилання на PlayerMovement
    private int currentLine = 0; // Номер поточної репліки
    private bool isFollowing = false; // Чи орк вже слідує за героєм

    [Header("Choice Buttons")]
    public GameObject hireButton; // Кнопка для найму орка
    public GameObject dismissButton; // Кнопка для відхилення орка

    [Header("Interaction Settings")]
    public float interactionDistance = 5f; // Відстань, на якій починається взаємодія з орком
    public Transform playerTransform; // Посилання на об'єкт гравця

    private bool isPlayerInRange = false; // Чи знаходиться герой в зоні взаємодії

    private void Start()
    {
        hireButton.SetActive(false); // Спочатку кнопки неактивні
        dismissButton.SetActive(false);

        // Знаходимо PlayerMovement
        player = playerTransform.GetComponent<PlayerMovement>();

        // Додаємо слухача для кнопки найму
        Button hireBtn = hireButton.GetComponent<Button>();
        hireBtn.onClick.AddListener(HireOrc); // Викликає метод HireOrc при натисканні
    }

    private void Update()
    {
        // Перевірка на відстань до героя
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer <= interactionDistance && !isFollowing && !isPlayerInRange)
        {
            isPlayerInRange = true; // Герой знаходиться в зоні взаємодії
            StartDialog();
        }
        else if (distanceToPlayer > interactionDistance && isPlayerInRange)
        {
            isPlayerInRange = false; // Герой покинув зону взаємодії
            EndDialog();
        }

        // Перевірка натискання ЛКМ для перемикання реплік
        if (isPlayerInRange && Input.GetMouseButtonDown(0)) // ЛКМ для продовження діалогу
        {
            ShowNextDialogLine(); // Показує наступну репліку
        }
    }

    private void StartDialog()
    {
        currentLine = 0; // Починаємо діалог з першої репліки
        ShowNextDialogLine(); // Відображаємо першу репліку
        dialogUI.SetActive(true); // Відображаємо діалогове вікно
    }

    private void ShowNextDialogLine()
    {
        if (currentLine < dialogLines.Length)
        {
            // Відображаємо поточну репліку
            dialogText.text = dialogLines[currentLine];
            currentLine++;
        }
        else
        {
            // Коли всі репліки показано, показуємо кнопки для вибору
            ShowChoiceButtons();
        }
    }

    private void ShowChoiceButtons()
    {
        hireButton.SetActive(true); // Показуємо кнопки
        dismissButton.SetActive(true);
    }

    private void EndDialog()
    {
        dialogUI.SetActive(false); // Ховаємо інтерфейс діалогу
        currentLine = 0; // Скидаємо діалог
    }

    // Викликається, коли натискають кнопку "Найняти"
    public void HireOrc()
    {
        int playerCoins = player.GetCoins(); // Отримуємо кількість монет у гравця

        if (playerCoins >= costToHire)
        {
            player.AddCoins(-costToHire); // Знімаємо монети у гравця
            GetComponent<OrcFollower>().StartFollowing(); // Починає слідувати за героєм
            isFollowing = true; // Встановлюємо статус слідування
            EndDialog(); // Завершуємо діалог
        }
        else
        {
            Debug.Log("Недостатньо монет!");
        }
    }

    // Викликається, коли натискають кнопку "Відхилити"
    public void DismissOrc()
    {
        EndDialog(); // Завершуємо діалог
    }
}
