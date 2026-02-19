using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SmoothHealthBarView : HealthView
{
    [SerializeField] private Slider _smoothHealthBar;

    [SerializeField] private float _smoothTime = 0.3f;

    private Coroutine _smoothCoroutine;

    private void Awake()
    {
        if (_health == null)
            _health = GetComponentInParent<Health>();

        if (_smoothHealthBar == null)
            _smoothHealthBar = GetComponent<Slider>();
    }

    private void Start()
    {
        if (_health != null)
        {
            float initial = (float)_health.CurrentHealth / _health.MaxHealth;
            _smoothHealthBar.value = initial;
        }
    }

    protected override void OnHealthChanged(int currentHealth)
    {
        if (_smoothCoroutine != null)
            StopCoroutine(_smoothCoroutine);

        _smoothCoroutine = StartCoroutine(SmoothChange(currentHealth));
    }

    private IEnumerator SmoothChange(int targetHealth)
    {
        float targetNormalized = (float)targetHealth / _health.MaxHealth;
        float startNormalized = _smoothHealthBar.value;
        float elapsed = 0f;

        while (elapsed < _smoothTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _smoothTime;
            _smoothHealthBar.value = Mathf.Lerp(startNormalized, targetNormalized, t);

            yield return null;
        }
        _smoothHealthBar.value = targetNormalized;
        _smoothCoroutine = null;
    }
}