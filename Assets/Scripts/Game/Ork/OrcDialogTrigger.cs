using UnityEngine;

public class OrcDialogTrigger : MonoBehaviour
{
    [SerializeField]
    private Collider2D dialogTriggerCollider; // Посилання на бокс-колайдер для діалогу
    [SerializeField]
    private DialogManager dialogManager; // Посилання на менеджер діалогу

    private bool isPlayerInTrigger = false; // Чи гравець у зоні тригера

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Перевіряємо, чи це гравець і чи колайдер відповідає зоні діалогу
        if (collision.CompareTag("Player") && collision == dialogTriggerCollider)
        {
            isPlayerInTrigger = true; // Гравець увійшов у зону діалогу
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Перевіряємо, чи гравець вийшов із зони діалогу
        if (collision.CompareTag("Player") && collision == dialogTriggerCollider)
        {
            isPlayerInTrigger = false; // Гравець вийшов із зони діалогу
        }
    }

    private void Update()
    {
        // Перевірка кнопки "E" і чи гравець у тригері
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Репліки діалогу для конкретного персонажа
            string[] dialogLines = new string[]
            {
                "Я орк Грок. Що тобі потрібно?",
                "Герой: Мені потрібна допомога, щоб вибратися звідси.",
                "Грок: Це коштуватиме 50 монет. Наймаєш мене?"
            };

            dialogManager.StartDialog(dialogLines); // Запуск діалогу
        }
    }
}
