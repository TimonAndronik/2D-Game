using UnityEngine;
using System.Collections.Generic;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab; // ������ �����
    public List<GameObject> enemyPrefabs; // ������ ������� ������ (�����, ����, �����)

    public List<GameObject> spawnPoints; // ������ ��'���� ��� ������
    public float enemyMinDistance = 1f; // ̳������� ������� ������ �� �����
    public float enemyMaxDistance = 3f; // ����������� ������� ������ �� �����
    public int minChests = 2; // ̳������� ������� ������
    public int minEnemiesPerChest = 1; // ̳������� ������� ������ ��� ���� �����
    public int maxEnemiesPerChest = 5; // ����������� ������� ������ ��� ���� �����

    private void Start()
    {
        SpawnChestsAndEnemies();
    }

    private void SpawnChestsAndEnemies()
    {
        // ����������, �� � ��������� ����� ��� ����� 2 ������
        if (spawnPoints.Count < minChests)
        {
            Debug.LogError("����������� ����� ��� �������� ������� ������!");
            return;
        }

        // ��������� �������� ������� ������ (����� 2, �������� � ������� ��������� �����)
        int chestCount = Random.Range(minChests, spawnPoints.Count + 1);

        // ������ ����������� ����� ������
        List<GameObject> availableSpawnPoints = new List<GameObject>(spawnPoints);

        for (int i = 0; i < chestCount; i++)
        {
            if (availableSpawnPoints.Count == 0)
            {
                Debug.Log("���������� ����� ��� ������.");
                return;
            }

            // �������� �������� ����� � ������
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            GameObject spawnPoint = availableSpawnPoints[randomIndex];

            // �������� ������
            GameObject chest = Instantiate(chestPrefab, spawnPoint.transform.position, Quaternion.identity);

            // �������� �������� ������� ������ ��� ���� �����
            SpawnRandomEnemiesAroundChest(chest.transform.position);

            // ��������� ����������� �����
            availableSpawnPoints.RemoveAt(randomIndex);
        }
    }

    private void SpawnRandomEnemiesAroundChest(Vector3 chestPosition)
    {
        // ��������� �������� ������� ������
        int enemyCount = Random.Range(minEnemiesPerChest, maxEnemiesPerChest + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            // �������� ����������� ������ � ������ �������
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            // �������� ��������� ������� � ���
            float randomDistance = Random.Range(enemyMinDistance, enemyMaxDistance); // ��������� �������
            float randomAngle = Random.Range(0f, 360f); // ���������� ��� � ��������

            // ����������� ������� ������ �� ����� ���� � ������
            Vector3 enemyPosition = chestPosition + new Vector3(
                Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomDistance, // X
                Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomDistance, // Y
                0f // Z ��� 2D
            );

            // �������� ������
            Instantiate(randomEnemyPrefab, enemyPosition, Quaternion.identity);
        }

        Debug.Log($"���� ����� ������������ {enemyCount} ������.");
    }
}
