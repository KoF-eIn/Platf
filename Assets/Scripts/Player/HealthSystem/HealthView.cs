using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected virtual void OnEnable()
    {
        if (_health != null)
        {
            _health.HealthChanged += OnHealthChanged;
        }
    }

    protected virtual void OnDisable()
    {
        if (_health != null)
        {
            _health.HealthChanged -= OnHealthChanged;
        }
    }

    protected abstract void OnHealthChanged(int currentHealth);
}