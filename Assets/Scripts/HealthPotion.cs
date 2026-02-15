using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int _healAmount = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == null) return;

        Health playerHealth = other.GetComponent<Health>();

        if (playerHealth != null)
        {
            playerHealth.Heal(_healAmount);
            Destroy(gameObject);
        }
    }
}