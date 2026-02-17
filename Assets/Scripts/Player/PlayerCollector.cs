using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(Health))]
public class PlayerCollector : MonoBehaviour
{
    private PlayerInventory _inventory;
    private Health _health;

    private void Awake()
    {
        _inventory = GetComponent<PlayerInventory>();
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collectable collectable = other.GetComponent<Collectable>();

        if (collectable == null) return;

        switch (collectable.Type)
        {
            case CollectableType.Coin:
                _inventory.AddCoin(collectable.Value);
                break;

            case CollectableType.HealthPotion:
                _health.Heal(collectable.Value);
                break;
        }

        Destroy(other.gameObject);
    }
}