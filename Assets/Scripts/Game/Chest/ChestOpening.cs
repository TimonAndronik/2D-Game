using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Sprite closedChestSprite;  // Спрайт для закритої скрині
    [SerializeField] private Sprite openChestSprite;   // Спрайт для відкритої скрині
    private SpriteRenderer spriteRenderer;              // Для зміни спрайта
    private bool isPlayerInRange = false;               // Чи знаходиться персонаж поруч
    public GameObject coinPrefab; // Префаб монетки
    public int minCoinCount = 1; // Мінімальна кількість монет
    public int maxCoinCount = 5; // Максимальна кількість монет

    private void Start()
    {
        // Отримуємо компонент SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Спочатку скриня закрита
        spriteRenderer.sprite = closedChestSprite;
    }

    private void Update()
    {
        // Якщо персонаж в межах колайдера і натиснута клавіша 'E'
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();  // Відкриваємо скриню
            DropCoins();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Перевіряємо, чи це персонаж
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;  // Персонаж в межах
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Перевіряємо, чи це персонаж
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;  // Персонаж вийшов з колайдера
        }
    }

    // Метод для відкриття скрині
    private void OpenChest()
    {
        // Зміна спрайту на відкриту скриню
        spriteRenderer.sprite = openChestSprite;

        // Додатково можна додати інші дії, наприклад, додавання предметів в інвентар
        Debug.Log("Chest opened!");
    }

    private void DropCoins()
    {
        // Визначаємо випадкову кількість монет
        int coinCount = Random.Range(minCoinCount, maxCoinCount);

        for (int i = 0; i < coinCount; i++)
        {
            // Додаємо рандомну позицію навколо скрині в межах радіусу
            float radius = 1.0f; // Визначаємо радіус (можна змінити)
            Vector3 randomOffset = new Vector3(
                Random.Range(-radius, radius),  // Випадкове зміщення по X в межах радіусу
                Random.Range(-radius, radius),  // Випадкове зміщення по Y в межах радіусу
                0f                             // Без зміщення по Z (для 2D)
            );

            // Використовуємо позицію скрині як базову
            Vector3 spawnPosition = transform.position + randomOffset;

            // Створюємо монету у випадковій позиції
            GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

            // Додаємо випадкову силу для розлітання
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Сила в горизонтальному і вертикальному напрямках
                Vector2 randomForce = new Vector2(
                    Random.Range(-0.5f, 0.5f), // Випадкова сила по X
                    Random.Range(0.5f, 1f)    // Випадкова сила по Y
                );
                rb.AddForce(randomForce, ForceMode2D.Impulse);
            }
        }

        Debug.Log($"Випало {coinCount} монет!");
    }
}
