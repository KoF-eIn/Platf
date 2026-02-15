using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Увеличиваем счёт (можно через GameManager или напрямую)
            CoinCounter.instance.AddCoins(coinValue);
            Destroy(gameObject);
        }
    }
}