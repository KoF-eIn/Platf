using UnityEngine;

public class HealthPotionSpawner : MonoBehaviour
{
    [SerializeField] private HealthPotion _potionPrefab;

    [SerializeField] private int _count = 3;
    [SerializeField] private int _maxAttempts = 50;

    [SerializeField] private Vector2 _spawnAreaMin;
    [SerializeField] private Vector2 _spawnAreaMax;

    [SerializeField] private LayerMask _obstacleLayers;

    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            Vector2 spawnPosition = GetRandomFreePosition();

            if (spawnPosition != Vector2.zero)
            {
                Instantiate(_potionPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private Vector2 GetRandomFreePosition()
    {
        for (int attempt = 0; attempt < _maxAttempts; attempt++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(_spawnAreaMin.x, _spawnAreaMax.x),
                Random.Range(_spawnAreaMin.y, _spawnAreaMax.y)
            );

            Collider2D hit = Physics2D.OverlapPoint(randomPosition, _obstacleLayers);

            if (hit == null)
                return randomPosition;
        }

        return Vector2.zero;
    }
}