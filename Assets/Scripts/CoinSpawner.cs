using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;

    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        if (_coinPrefab == null) return;

        foreach (Transform point in _spawnPoints)
        {
            Instantiate(_coinPrefab, point.position, Quaternion.identity);
        }
    }
}