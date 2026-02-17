using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health _health;

    [SerializeField] private Text _healthText;

    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _smoothHealthBar;

    [SerializeField] private float _smoothSpeed = 5f;

    private float _targetNormalizedHealth;
    private float _currentSmoothNormalizedHealth;

    private void Start()
    {
        if (_health == null)
        {
            Debug.LogError("Health component is not assigned in HealthDisplay.", this);

            return;
        }

        _targetNormalizedHealth = (float)_health.CurrentHealth / _health.MaxHealth;
        _currentSmoothNormalizedHealth = _targetNormalizedHealth;
        UpdateTextAndBar();
    }

    private void OnEnable()
    {
        if (_health != null)
        {
            _health.HealthChanged += OnHealthChanged;
        }
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.HealthChanged -= OnHealthChanged;
        }
    }

    private void OnHealthChanged(int currentHealth)
    {
        _targetNormalizedHealth = (float)currentHealth / _health.MaxHealth;
        UpdateTextAndBar();
    }

    private void UpdateTextAndBar()
    {
        if (_healthText != null)
        {
            _healthText.text = $"{_health.CurrentHealth}/{_health.MaxHealth}";
        }

        if (_healthBar != null)
        {
            _healthBar.value = _targetNormalizedHealth;
        }
    }

    private void Update()
    {
        if (_smoothHealthBar == null || _health == null)
        {
            return;
        }

        _currentSmoothNormalizedHealth = Mathf.MoveTowards(
            _currentSmoothNormalizedHealth,
            _targetNormalizedHealth,
            _smoothSpeed * Time.deltaTime
        );

        _smoothHealthBar.value = _currentSmoothNormalizedHealth;
    }
}