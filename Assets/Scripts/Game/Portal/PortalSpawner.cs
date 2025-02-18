using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject portalPrefab; // ������ �������

    [SerializeField]
    private GameObject[] enemyPrefabs; // ����� ������� ������

    [SerializeField]
    private Transform[] spawnPoints; // ����� ����� ������

    [SerializeField]
    private int enemiesPerPortal = 3; // ʳ������ ������, �� ���������� ����� �� ��������

    [SerializeField]
    private float minSpawnDistance = 4f; // ̳������� ������� �� �������
    [SerializeField]
    private float maxSpawnDistance = 7f; // ����������� ������� �� �������

    private void Start()
    {
        SpawnPortalAndEnemies();
    }

    private void SpawnPortalAndEnemies()
    {
        if (portalPrefab == null || enemyPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogError("������ �������, ������ ��� ����� ������ �� ����������!");
            return;
        }

        // �������� ��������� ����� � ������
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedPoint = spawnPoints[randomIndex];

        // �������� ������ �� ������ �����
        Instantiate(portalPrefab, selectedPoint.position, Quaternion.identity);
        Debug.Log($"������ ����������� �� ����� {randomIndex + 1}");

        // �������� ������ ����� �� ��������
        SpawnEnemies(selectedPoint.position);
    }

    private void SpawnEnemies(Vector3 portalPosition)
    {
        Vector2 directionMultiplier = GetDirectionMultiplier(portalPosition);

        for (int i = 0; i < enemiesPerPortal; i++)
        {
            Vector3 spawnPosition;

            // �������� ������� ������ � ����������� �������� ������
            spawnPosition = portalPosition + new Vector3(
                Random.Range(minSpawnDistance, maxSpawnDistance) * directionMultiplier.x,
                Random.Range(minSpawnDistance, maxSpawnDistance) * directionMultiplier.y,
                0f
            );

            // �������� ����������� ������ � ������ �������
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // �������� ������
            Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
            Debug.Log($"����� {randomEnemyPrefab.name} ����������� ��� ������� �� ������� {spawnPosition}");
        }
    }

    private Vector2 GetDirectionMultiplier(Vector3 portalPosition)
    {
        // ��������� �������� �������� �� ����� ����
        bool isRight = portalPosition.x > 0;
        bool isTop = portalPosition.y > 0;

        if (isRight && isTop)
        {
            // ������ ������ ���
            return new Vector2(-1, -1);
        }
        else if (!isRight && isTop)
        {
            // ������ ���� ���
            return new Vector2(1, -1);
        }
        else if (isRight && !isTop)
        {
            // ����� ������ ���
            return new Vector2(-1, 1);
        }
        else
        {
            // ����� ���� ���
            return new Vector2(1, 1);
        }
    }
}
