using UnityEngine;
using System.Collections.Generic;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab; // Префаб скрині
    public List<GameObject> enemyPrefabs; // Список префабів ворогів (слайм, вовк, кажан)

    public List<GameObject> spawnPoints; // Список об'єктів для спавну
    public float enemyMinDistance = 1f; // Мінімальна відстань ворога від скрині
    public float enemyMaxDistance = 3f; // Максимальна відстань ворога від скрині
    public int minChests = 2; // Мінімальна кількість скринь
    public int minEnemiesPerChest = 1; // Мінімальна кількість ворогів біля однієї скрині
    public int maxEnemiesPerChest = 5; // Максимальна кількість ворогів біля однієї скрині

    private void Start()
    {
        SpawnChestsAndEnemies();
    }

    private void SpawnChestsAndEnemies()
    {
        // Перевіряємо, чи є достатньо точок для мінімум 2 скринь
        if (spawnPoints.Count < minChests)
        {
            Debug.LogError("Недостатньо точок для мінімальної кількості скринь!");
            return;
        }

        // Визначаємо рандомну кількість скринь (мінімум 2, максимум — кількість доступних точок)
        int chestCount = Random.Range(minChests, spawnPoints.Count + 1);

        // Список скопійованих точок спавну
        List<GameObject> availableSpawnPoints = new List<GameObject>(spawnPoints);

        for (int i = 0; i < chestCount; i++)
        {
            if (availableSpawnPoints.Count == 0)
            {
                Debug.Log("Закінчилися точки для спавну.");
                return;
            }

            // Вибираємо рандомну точку зі списку
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            GameObject spawnPoint = availableSpawnPoints[randomIndex];

            // Спавнимо скриню
            GameObject chest = Instantiate(chestPrefab, spawnPoint.transform.position, Quaternion.identity);

            // Спавнимо рандомну кількість ворогів біля цієї скрині
            SpawnRandomEnemiesAroundChest(chest.transform.position);

            // Видаляємо використану точку
            availableSpawnPoints.RemoveAt(randomIndex);
        }
    }

    private void SpawnRandomEnemiesAroundChest(Vector3 chestPosition)
    {
        // Визначаємо рандомну кількість ворогів
        int enemyCount = Random.Range(minEnemiesPerChest, maxEnemiesPerChest + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            // Вибираємо випадкового ворога зі списку префабів
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            // Генеруємо випадкову відстань і кут
            float randomDistance = Random.Range(enemyMinDistance, enemyMaxDistance); // Випадкова відстань
            float randomAngle = Random.Range(0f, 360f); // Випадковий кут у градусах

            // Розраховуємо позицію ворога на основі кута і відстані
            Vector3 enemyPosition = chestPosition + new Vector3(
                Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomDistance, // X
                Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomDistance, // Y
                0f // Z для 2D
            );

            // Спавнимо ворога
            Instantiate(randomEnemyPrefab, enemyPosition, Quaternion.identity);
        }

        Debug.Log($"Біля скрині заспавнилося {enemyCount} ворогів.");
    }
}
