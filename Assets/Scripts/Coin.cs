using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _coinValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        if (inventory != null)
        {
            inventory.AddCoin(_coinValue);
            Destroy(gameObject);
        }
    }
}