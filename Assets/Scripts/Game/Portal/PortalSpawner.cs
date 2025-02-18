using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject portalPrefab; // Префаб порталу

    [SerializeField]
    private GameObject[] enemyPrefabs; // Масив префабів ворогів

    [SerializeField]
    private Transform[] spawnPoints; // Масив точок спавну

    [SerializeField]
    private int enemiesPerPortal = 3; // Кількість ворогів, які спавняться разом із порталом

    [SerializeField]
    private float minSpawnDistance = 4f; // Мінімальна відстань від порталу
    [SerializeField]
    private float maxSpawnDistance = 7f; // Максимальна відстань від порталу

    private void Start()
    {
        SpawnPortalAndEnemies();
    }

    private void SpawnPortalAndEnemies()
    {
        if (portalPrefab == null || enemyPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogError("Префаб порталу, ворогів або точки спавну не налаштовані!");
            return;
        }

        // Вибираємо випадкову точку зі списку
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedPoint = spawnPoints[randomIndex];

        // Спавнимо портал на обраній точці
        Instantiate(portalPrefab, selectedPoint.position, Quaternion.identity);
        Debug.Log($"Портал заспавнився на точці {randomIndex + 1}");

        // Спавнимо ворогів поруч із порталом
        SpawnEnemies(selectedPoint.position);
    }

    private void SpawnEnemies(Vector3 portalPosition)
    {
        Vector2 directionMultiplier = GetDirectionMultiplier(portalPosition);

        for (int i = 0; i < enemiesPerPortal; i++)
        {
            Vector3 spawnPosition;

            // Генеруємо позицію ворога з урахуванням напрямків спавну
            spawnPosition = portalPosition + new Vector3(
                Random.Range(minSpawnDistance, maxSpawnDistance) * directionMultiplier.x,
                Random.Range(minSpawnDistance, maxSpawnDistance) * directionMultiplier.y,
                0f
            );

            // Вибираємо випадкового ворога з масиву префабів
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Спавнимо ворога
            Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
            Debug.Log($"Ворог {randomEnemyPrefab.name} заспавнився біля порталу на позиції {spawnPosition}");
        }
    }

    private Vector2 GetDirectionMultiplier(Vector3 portalPosition)
    {
        // Визначаємо множники напрямків на основі кута
        bool isRight = portalPosition.x > 0;
        bool isTop = portalPosition.y > 0;

        if (isRight && isTop)
        {
            // Верхній правий кут
            return new Vector2(-1, -1);
        }
        else if (!isRight && isTop)
        {
            // Верхній лівий кут
            return new Vector2(1, -1);
        }
        else if (isRight && !isTop)
        {
            // Нижній правий кут
            return new Vector2(-1, 1);
        }
        else
        {
            // Нижній лівий кут
            return new Vector2(1, 1);
        }
    }
}
