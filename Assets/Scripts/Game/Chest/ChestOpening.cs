using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Sprite closedChestSprite;  // ������ ��� ������� �����
    [SerializeField] private Sprite openChestSprite;   // ������ ��� ������� �����
    private SpriteRenderer spriteRenderer;              // ��� ���� �������
    private bool isPlayerInRange = false;               // �� ����������� �������� �����
    public GameObject coinPrefab; // ������ �������
    public int minCoinCount = 1; // ̳������� ������� �����
    public int maxCoinCount = 5; // ����������� ������� �����

    private void Start()
    {
        // �������� ��������� SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �������� ������ �������
        spriteRenderer.sprite = closedChestSprite;
    }

    private void Update()
    {
        // ���� �������� � ����� ��������� � ��������� ������ 'E'
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();  // ³�������� ������
            DropCoins();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����������, �� �� ��������
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;  // �������� � �����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ����������, �� �� ��������
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;  // �������� ������ � ���������
        }
    }

    // ����� ��� �������� �����
    private void OpenChest()
    {
        // ���� ������� �� ������� ������
        spriteRenderer.sprite = openChestSprite;

        // ��������� ����� ������ ���� 䳿, ���������, ��������� �������� � ��������
        Debug.Log("Chest opened!");
    }

    private void DropCoins()
    {
        // ��������� ��������� ������� �����
        int coinCount = Random.Range(minCoinCount, maxCoinCount);

        for (int i = 0; i < coinCount; i++)
        {
            // ������ �������� ������� ������� ����� � ����� ������
            float radius = 1.0f; // ��������� ����� (����� ������)
            Vector3 randomOffset = new Vector3(
                Random.Range(-radius, radius),  // ��������� ������� �� X � ����� ������
                Random.Range(-radius, radius),  // ��������� ������� �� Y � ����� ������
                0f                             // ��� ������� �� Z (��� 2D)
            );

            // ������������� ������� ����� �� ������
            Vector3 spawnPosition = transform.position + randomOffset;

            // ��������� ������ � ��������� �������
            GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

            // ������ ��������� ���� ��� ���������
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // ���� � ��������������� � ������������� ���������
                Vector2 randomForce = new Vector2(
                    Random.Range(-0.5f, 0.5f), // ��������� ���� �� X
                    Random.Range(0.5f, 1f)    // ��������� ���� �� Y
                );
                rb.AddForce(randomForce, ForceMode2D.Impulse);
            }
        }

        Debug.Log($"������ {coinCount} �����!");
    }
}
